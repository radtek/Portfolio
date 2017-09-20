using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;
using System.Drawing;
using System.IO;

using System.Net.Json;
using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class KaixinBase : IDisposable
    {
        private bool IsDisposed = false; 
        private HttpHelper _hh;
        private TaskInfo _task;
        private OperationInfo _operation;
        private string _caption;
        private string _key;
        private AccountInfo _account;
        private ProxyInfo _proxy;
        private DelayInfo _delay;
        private StringBuilder _sblog;
        private Collection<FriendInfo> _allMyFriendsList;
        //image validation code
        private string _validationCode;
        private bool _resetUserInfo = false;

        protected string _module;
        protected Thread _threadMain;
        protected string _verifyCode;

        public delegate void MessageChangedEventHandler(string caption, string key, string message);
        public event MessageChangedEventHandler MessageChanged;

        public delegate void ValidateCodeNeededEventHandler(byte[] image, string taskid, string taskname);
        public event ValidateCodeNeededEventHandler ValidateCodeNeeded;

        public delegate void LoginFailedEventHandler();
        public event LoginFailedEventHandler LoginFailed;

        public delegate void AllMyFriendsFetchedEventHandler(Collection<FriendInfo> allmyfriends);
        public event AllMyFriendsFetchedEventHandler AllMyFriendsFetched;

        public delegate void OperationFailedEventHandler();
        public event OperationFailedEventHandler OperationFailed;

        #region Ctor
        public KaixinBase()
        {
            _sblog = new StringBuilder();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }

        protected void Dispose(bool Disposing)
        {
            if (!IsDisposed)
            {
                if (Disposing)
                {
                    //�����й���Դ
                }
                //������й���Դ
            }
            IsDisposed = true;
        }

        ~KaixinBase()
        {
            Dispose(false);
        }
        #endregion

        #region Clone
        public void Clone(KaixinBase source)
        {
            Clone(source, false);
        }
        public void Clone(KaixinBase source, bool ignoreValidateEvent)
        {
            try
            {
                this.HH = source.HH;
                this.CurrentAccount = source.CurrentAccount;
                this.MessageChanged += new MessageChangedEventHandler(source.SetMessageByParam);
                if (!ignoreValidateEvent)
                    this.ValidateCodeNeeded += new ValidateCodeNeededEventHandler(source.SetValidateCodeByParam);
                this.Operation = source.Operation;
                this.Task = source.Task;
                this.AllMyFriendsList = source.AllMyFriendsList;
                this.Caption = source.Caption;
                this.Key = source.Key;
                this.ExecutionLog = source.ExecutionLog;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (ThreadInterruptedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Write("KaixinBase.Clone", ex);
                throw;
            }
        }
        #endregion

        #region Initial
        public void Initial()
        {
            Initial(true);
        }
        public void Initial(bool newhttp)
        {
            try
            {
                this._allMyFriendsList = new Collection<FriendInfo>();
                if (newhttp)
                    this._hh = new HttpHelper();
                this._hh.SetProxy(this.Proxy.Server, this.Proxy.Port, this.Proxy.UserName, this.Proxy.Password);
                if (this.Proxy.Enable)
                    this._hh.EnableProxy();
                this._hh.SetDelay(this.Delay.DelayedTime, this.Delay.TimeOut, this.Delay.TryTimes);
                if (newhttp)
                    this._hh.messageChanged += new HttpHelper.MessageChangedEventHandler(_hh_messageChanged);
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (ThreadInterruptedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Write("KaixinBase.Initial", ex);
                throw;
            }
        }
        #endregion

        #region StopThread
        public void StopThread()
        {
            try
            {
                if (_threadMain != null && _threadMain.ThreadState != ThreadState.Stopped)
                {
                    _threadMain.Abort();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Write("KaixinBase.StopThread", ex);
                throw;
            }
        }
        #endregion

        #region _hh_messageChanged
        private void _hh_messageChanged(string message)
        {
            SetMessage(message);
        }
        #endregion

        #region SetMessageByParam
        public void SetMessageByParam(string caption, string key, string msg)
        {
            if (MessageChanged != null)
                MessageChanged(caption, key, msg);
        }
        #endregion

        #region SetValidateCodeByParam
        public void SetValidateCodeByParam(byte[] image, string taskid, string taskname)
        {
            if (ValidateCodeNeeded != null)
                ValidateCodeNeeded(image, taskid, taskname);
        }
        #endregion

        #region GetFriendNameById
        protected string GetFriendNameById(int id)
        {
            foreach (FriendInfo info in this._allMyFriendsList)
            {
                if (info.Id == id)
                {
                    return info.Name;
                }
            }
            return id.ToString();
        }
        protected string GetFriendNameById(string id)
        {
            foreach (FriendInfo info in this._allMyFriendsList)
            {
                if (info.Id.ToString() == id)
                {
                    return info.Name;
                }
            }
            return id.ToString();
        }
        #endregion

        #region GetFriendById
        protected FriendInfo GetFriendById(int id)
        {
            foreach (FriendInfo info in this._allMyFriendsList)
            {
                if (info.Id == id)
                {
                    return info;
                }
            }
            return null;
        }
        protected FriendInfo GetFriendById(string id)
        {
            foreach (FriendInfo info in this._allMyFriendsList)
            {
                if (info.Id.ToString() == id)
                {
                    return info;
                }
            }
            return null;
        }
        #endregion

        #region IsAlreadyMyFriend
        protected bool IsAlreadyMyFriend(string userid)
        {
            if (String.IsNullOrEmpty(userid))
                return false;

            if (this._allMyFriendsList == null || this._allMyFriendsList.Count == 0)
                return false;

            foreach (FriendInfo friend in this._allMyFriendsList)
            {
                if (friend.Id.ToString() == userid)
                    return true;
            }

            return false;
        }
        #endregion

        #region SetMessageLn
        protected void SetMessageLn(string msg)
        {
            msg = TraceLog.SetMessageLn(_module, msg);

            if (MessageChanged != null)
                MessageChanged(_caption, _key, msg);

            if (Task != null && (Task.SendLog || Task.WriteLogToFile) && msg != Constants.COMMAND_CLEARLOG)
                _sblog.Append(msg);
        }
        protected void SetMessage(string msg)
        {
            msg = TraceLog.SetMessage(msg);

            if (MessageChanged != null)
                MessageChanged(_caption, _key, msg);
            if (Task != null && (Task.SendLog || Task.WriteLogToFile) && msg != Constants.COMMAND_CLEARLOG)
                _sblog.Append(msg);
        }
        #endregion

        #region Login && Log out
        private bool Login(bool printMessage)
        {
            return Login(_account, printMessage);
        }

        private bool Login(AccountInfo account, bool printMessage)
        {
            if (account == null)
                account = _account;

            if (printMessage)
                SetMessageLn(account.UserName + "(" + account.Email + ")" + "���ڵ�¼...");

            string content = RequestLogin(account);

            if (content.IndexOf("security.kaixin001.com/js/sso.js") != -1) 
            {
                if (_resetUserInfo)
                {
                    content = _hh.Get("http://www.kaixin001.com/home/index.php");
                    if (content.IndexOf("<title>�ҵ���ҳ - ������</title>") != -1)
                    {
                        _account.UserId = JsonHelper.GetMid(content, "�ҵĿ�����ID:", "\"></a>");
                        _account.UserName = JsonHelper.GetMid(content, _account.UserId + "\" class=\"sl2\">", "</a></p>");
                        return true;
                    }
                }
                if (printMessage)
                    SetMessageLn("��¼�ɹ���");
                return true;
            }
            else if (content.IndexOf("<title>��¼ - ������</title>") != -1)
            {
                if (printMessage)
                    SetMessageLn("���������֤�����������...");
                //��Ҫ��֤��
                /********************************************************/
                /* ȡͼƬʱ�õ�randnum��ֵ�������¼ʱ�õ���rcodeֵ��ͬ
                /********************************************************/
                byte[] image = this._hh.GetImage("http://www.kaixin001.com/interface/regcreatepng.php?randnum=0.03706184340980051_1253091176687&norect=1", "http://www.kaixin001.com/login/login.php");
                //invode the validation code dialog
                if (_task != null && !String.IsNullOrEmpty(_task.TaskId))
                {
                    if (!_task.SkipValidation)
                        SetValidateCodeByParam(image, _task.TaskId, _task.TaskName);
                    else
                    {
                        if (printMessage)
                            SetMessageLn("�������ʺš�");
                    }
                }
                else
                    SetValidateCodeByParam(image, "", "");
                return false;
            }

            if (LoginFailed != null)
                LoginFailed();

            if (printMessage)
                SetMessageLn(account.UserName + "(" + account.Email + ")" + "��¼ʧ�ܣ�");
            LogHelper.Write("KaixinBase.Login", account.UserName + "(" + account.Email + ")(" + account.Password + ")��¼ʧ�ܣ�", LogSeverity.Warn);
            return false;
        }

        protected bool LogOut(bool printMessage)
        {
            this._hh.Get("http://www.kaixin001.com/login/logout.php");
            if (printMessage)
                SetMessageLn("�ǳ��ɹ���");
            return false;
        }
        #endregion

        #region ValidationLogin
        protected bool ValidationLogin()
        {
            return ValidationLogin(true);
        }
        protected bool ValidationLogin(bool printMessage)
        {
            return ValidationLogin(null, printMessage);            
        }
        protected bool ValidationLogin(AccountInfo account)
        {
            return ValidationLogin(account, true);
        }
        protected bool ValidationLogin(AccountInfo account, bool printMessage)
        {            
            if (!this.Login(account, printMessage))
            {
                if (_task != null && _task.SkipValidation == true)
                    return false;

                //this.ValidationCodeΪnull����ʾȡ����¼����
                while (this.ValidationCode != null)
                {
                    if (!this.Login(account, printMessage))
                    {
                        continue;
                    }
                    else
                        break;
                }
                if (this.ValidationCode == null)
                {
                    if (printMessage)
                        SetMessageLn("��ֹ��¼��");
                    return false;
                }
            }

            return true;
        }

        
        #endregion
        
        #region GetAllMyFriends
        public void GetAllMyFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAllMyFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetAllMyFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_MYKAIXIN;
                SetMessageLn("ˢ��[���ڿ������ϵ����к���]...");
                //login
                if (!this.ValidationLogin())
                {
                    if (AllMyFriendsFetched != null)
                        AllMyFriendsFetched(_allMyFriendsList);
                    return;
                }

                string content = RequestAllMyFriends();
                ReadAllMyFriends(content, true);
                SetMessageLn("[���ڿ������ϵ����к���]��Ϣˢ�³ɹ���");

                //invoke event
                if (AllMyFriendsFetched != null)
                    AllMyFriendsFetched(_allMyFriendsList);
            });
            ExecuteTryCatchBlock(th, "[���ڿ������ϵ����к���]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region ReadAllMyFriends
        public void ReadAllMyFriends(string content, bool printMessage)
        {
            if (printMessage)
                SetMessageLn("��ȡ�ҵ�����������Ϣ...");

            this._allMyFriendsList.Clear();

            //�ҵ����к���
            JsonTextParser parser = new JsonTextParser();
            JsonArrayCollection arrayAllMyFriends = parser.Parse(content) as JsonArrayCollection;
            foreach (JsonObjectCollection item in arrayAllMyFriends)
            {
                FriendInfo friend = new FriendInfo();
                friend.Id = JsonHelper.GetIntegerValue(item["uid"]);
                friend.Name = JsonHelper.GetStringValue(item["real_name"]);
                this._allMyFriendsList.Add(friend);
                if (printMessage)
                    SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
            }

            if (printMessage)
                SetMessageLn(string.Format("����{0}������", new object[] { this._allMyFriendsList.Count }));
        }
        #endregion

        #region RequestLogin
        private string RequestLogin(AccountInfo account)
        {
            this._hh.Get("http://www.kaixin001.com/");
            this._hh.DelayedTime = Constants.DELAY_2SECONDS;

            //string loginUrl = "http://www.kaixin001.com/login/login.php";
            string loginUrl = "https://security.kaixin001.com/login/login_auth.php";
            string param = "";
            if (string.IsNullOrEmpty(this.ValidationCode))
                //param = "url=%2Fhome%2F&email=" + DataConvert.GetEncodeData(email) + "&password=" + DataConvert.GetEncodeData(password);
                param = "rcode=&url=http%3A%2F%2Fwww.kaixin001.com%2F%3F647383871%3D342757378&email=" + DataConvert.GetEncodeData(account.Email) + "&password=" + DataConvert.GetEncodeData(account.Password) + "&code=";
            else
                param = "rcode=0.03706184340980051_1253091176687&url=%2Fhome%2F&rpkey=&diarykey=&invisible_mode=0&email=" + DataConvert.GetEncodeData(account.Email) + "&password=" + DataConvert.GetEncodeData(account.Password) + "&code=" + DataConvert.GetEncodeData(this.ValidationCode);
            /********************************************************/
            /* ȡͼƬʱ�õ�randnum��ֵ�������¼ʱ�õ���rcodeֵ��ͬ
            /********************************************************/
            return this._hh.Post(loginUrl, "http://www.kaixin001.com/", param);
        }
        #endregion

        #region RequestAllMyFriends
        protected string RequestAllMyFriends()
        {
            return this._hh.Get("http://www.kaixin001.com/interface/suggestfriend.php?pars=&type=all");
        }
        #endregion

        #region RequestMessageSending
        protected void RequestMessageSending(string friendid, string msg)
        {
            this._hh.DelayedTime = Constants.DELAY_2SECONDS;
            this._hh.Post("http://www.kaixin001.com/msg/post.php", "uids=" + DataConvert.GetEncodeData(friendid) + "&group=&content=" + DataConvert.GetEncodeData(msg) + "&texttype=html");
            //uids=10752908%2C10755959%2C10752309%2C10755623&group=&content=%3CP%3E%E4%BD%A0%E5%A5%BD%E5%95%8A%3C%2FP%3E%0D%0A%3CP%3E%E6%88%91%E5%B8%82%E6%B0%B4%E5%95%8A%3C%2FP%3E%0D%0A%3CP%3E%E9%82%A3%E4%B8%AA%E3%80%82%3C%2FP%3E%0D%0A%3CP%3E%E4%BD%A0%E7%9F%A5%E9%81%93%E4%B9%88%EF%BC%9F%EF%BC%9F%EF%BC%9F%3C%2FP%3E&texttype=html
        }
        #endregion

        #region GetAccCode
        protected string GetAccCode(string content)
        {
            try
            {
                if (String.IsNullOrEmpty(content))
                {
                    SetMessageLn("��ȡAccCodeʧ�ܣ�contentΪ�գ�");                    
                }
                int posFunction = content.IndexOf("function  acc()");
                int secondForeVar = content.Substring(0, posFunction).LastIndexOf("var");
                int firstForeVar = content.Substring(0, secondForeVar).LastIndexOf("var");

                int retAcc = content.IndexOf("return acc;");
                int lastpos = content.IndexOf(";", retAcc + 11);

                string accscript = content.Substring(firstForeVar, lastpos - firstForeVar + 1);

                return JsonHelper.RunJavascript(accscript + "acc();");
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (ThreadInterruptedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Write("KaixinBase.GetAccCode", content, ex, LogSeverity.Error);
                throw;
            }
        }
        #endregion

        #region ExecuteByThread
        public delegate void TryCatchBlock();
        protected void ExecuteTryCatchBlock(TryCatchBlock method, string ErrorMsg)
        {
            try
            {
                method();
            }
            catch (ThreadAbortException)
            {
                SetMessageLn("��ִֹ�У�");
                if (OperationFailed != null)
                    OperationFailed();
            }
            catch (ThreadInterruptedException)
            {
                SetMessageLn("��ִֹ�У�");
                if (OperationFailed != null)
                    OperationFailed();
            }
            catch (Exception ex)
            {
                LogHelper.Write(ErrorMsg + this.Caption, ex);
                SetMessageLn(ErrorMsg + "����" + ex.Message);
                if (OperationFailed != null)
                    OperationFailed();
            }
        }
        #endregion

        #region Properties
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string Module
        {
            get { return _module; }
            set { _module = value; }
        }

        public AccountInfo CurrentAccount
        {
            get { return _account; }
            set { _account = value; }
        }

        public ProxyInfo Proxy
        {
            get { return _proxy; }
            set { _proxy = value; }
        }

        public DelayInfo Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        public HttpHelper HH
        {
            get { return _hh; }
            set { _hh = value; }
        }

        public Collection<FriendInfo> AllMyFriendsList
        {
            get { return this._allMyFriendsList; }
            set { this._allMyFriendsList = value; }
        }

        public TaskInfo Task
        {
            get { return _task; }
            set { _task = value; }
        }

        public OperationInfo Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }

        public StringBuilder ExecutionLog
        {
            get { return _sblog; }
            set { _sblog = value; }
        }

        public string ValidationCode
        {
            get { return _validationCode; }
            set { _validationCode = value; }
        }

        public bool ResetUserInfo
        {
            get { return _resetUserInfo; }
            set { _resetUserInfo = value; }
        }
        #endregion
    }
}
