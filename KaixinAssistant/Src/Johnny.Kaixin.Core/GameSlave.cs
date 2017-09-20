using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class GameSlave : KaixinBase
    {
        private Collection<FriendInfo> _mySlaveList;
        private Collection<FriendInfo> _buyableSlaveList;
        private int _slaveNum;
        private int _slavecash;
        private int _hostuid;

        public delegate void MySlaveFetchedEventHandler(Collection<FriendInfo> slaves);
        public event MySlaveFetchedEventHandler MySlaveFetched;

        public delegate void BuyableSlavesFetchedEventHandler(Collection<FriendInfo> friends);
        public event BuyableSlavesFetchedEventHandler BuyableSlavesFetched;

        public GameSlave()
        {
            _mySlaveList = new Collection<FriendInfo>();
            _buyableSlaveList = new Collection<FriendInfo>();
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //slave
                SetMessageLn("���ڳ�ʼ��[��������]...");

                string content = RequestSlaveHomePage();
                ReadSlaves(content, false);
                SetMessage("[�ҵ�ū��]��Ϣ���سɹ���");
                //buy slaves
                content = RequestBuyableSlaves();
                ReadBuyableSlaves(content, false);
                SetMessage("[�������ū��]��Ϣ���سɹ���");
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
                LogHelper.Write("GameSlave.Initialize", ex, LogSeverity.Error);
                SetMessage(" ��ʼ��[��������]ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion        

        #region GetMySlaves
        public void GetMySlavesByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetMySlaves));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetMySlaves()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_MYKAIXIN;
                SetMessageLn("ˢ��[�ҵ�ū��]...");

                //login
                if (!this.ValidationLogin(true))
                {
                    if (MySlaveFetched != null)
                        MySlaveFetched(_mySlaveList);
                    return;
                }

                //read cars           
                string content = RequestSlaveHomePage();
                ReadSlaves(content, true);

                SetMessageLn("[�ҵ�ū��]��Ϣˢ�³ɹ���");

                //invoke event
                if (MySlaveFetched != null)
                    MySlaveFetched(_mySlaveList);
            });
            base.ExecuteTryCatchBlock(th, "[�ҵ�ū��]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region ReadSlaves
        public void ReadSlaves(string content, bool printMessage)
        {
            int num;
            this._mySlaveList.Clear();

            if (printMessage)
                SetMessageLn("��ȡū���б�");
            for (string info = JsonHelper.GetMid(content, "<div class=\"w265 l c6\">", "\t\t\t </div>", out num); info != null; info = JsonHelper.GetMid(content, "<div class=\"w265 l c6\">", "\t\t\t </div>", out num))
            {
                content = content.Substring(num);

                FriendInfo slave = new FriendInfo();
                slave.Name = JsonHelper.GetMid(info, "class=\"sl2\">", "<");
                slave.Id = JsonHelper.GetInteger(JsonHelper.GetMid(info, "/home/?uid=", "'"));
                slave.Gender = JsonHelper.GetMid(info, "\">��Ҫ�ͷ�", "<") == "��" ? true : false;
                slave.Price = JsonHelper.GetInteger(JsonHelper.GetMid(info, "���ۣ�<strong class=\"dgreen\">&yen;", "<"));
                if ((slave.Name != null))
                {
                    this._mySlaveList.Add(slave);
                    if (printMessage)
                        SetMessageLn(slave.Name + "(" + slave.Id.ToString() + ")--" + slave.Price);
                }
            }
            this._slaveNum = this._mySlaveList.Count;
            if (printMessage)
                SetMessageLn(string.Format("����{0}��ū��", new object[] { this._slaveNum }));
        }
        #endregion

        #region GetBuyableSlaves
        public void GetBuyableSlavesByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetBuyableSlaves));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetBuyableSlaves()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_SLAVE;
                SetMessageLn("ˢ��[�������ū��]...");

                if (!this.ValidationLogin(true))
                {
                    if (BuyableSlavesFetched != null)
                        BuyableSlavesFetched(_buyableSlaveList);
                    return;
                }

                string content = RequestSlaveHomePage();
                content = RequestBuyableSlaves();
                ReadBuyableSlaves(content, true);

                SetMessageLn("[�������ū��]��Ϣˢ�³ɹ���");

                //invoke event
                if (BuyableSlavesFetched != null)
                    BuyableSlavesFetched(_buyableSlaveList);
            });
            base.ExecuteTryCatchBlock(th, "[�������ū��]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region ReadBuyableSlaves
        public void ReadBuyableSlaves(string content, bool printMessage)
        {
            int num;
            this._buyableSlaveList.Clear();

            if (printMessage)
                SetMessageLn("��ȡ[�������ū��]��Ϣ:");
            for (string pos = JsonHelper.GetMid(content, "javascript:parent.gotouser(", ")", out num); pos != null; pos = JsonHelper.GetMid(content, "javascript:parent.gotouser(", ")", out num))
            {
                content = content.Substring(num);
                int id = JsonHelper.GetInteger(pos);
                if (id > 0)
                {
                    FriendInfo friend = new FriendInfo();
                    friend.Id = id;
                    friend.Name = JsonHelper.GetMid(content, "<strong>", "</strong></a>");
                    string price = JsonHelper.GetMid(content, "10em;\">(�۸񣺣�", ")</div>");
                    if ((price != null) && price.Trim() != string.Empty)
                        friend.Price = JsonHelper.GetInteger(price);
                    this._buyableSlaveList.Add(friend);
                    if (printMessage)
                        SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")--�۸񣺣�" + friend.Price);
                }
            }
            if (printMessage)
                SetMessageLn("��ɶ�ȡ��");
        }
        #endregion

        #region DisplayMySlaves
        private void DisplayMySlaves()
        {
            SetMessageLn("�ҵ�ū����Ϣ:");
            int num = 0;
            foreach (FriendInfo slave in this._mySlaveList)
            {
                SetMessageLn(string.Format("ū��#{0}:{1}(���:��{2})", ++num, slave.Name, slave.Price));
            }
        }
        #endregion

        #region RunSlave
        public void RunSlave()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_SLAVE;

                SetMessageLn("��ʼ��������...");

                //slave
                string content = RequestSlaveHomePage();
                ReadSlaves(content, false);

                if (Task.BuySlave)
                    this.BuySlaveFromBuyList();
                if (Task.BuyLowPriceSlave)
                    this.BuyLowPriceSlave();

                if (Task.FawnMaster || Task.PropitiateSlave || Task.AfflictSlave || Task.ReleaseSlave)
                {
                    if (Task.FawnMaster)
                        this.FawnMaster();

                    if (Task.BuySlave || Task.BuyLowPriceSlave)
                    {
                        content = RequestSlaveHomePage();
                        ReadSlaves(content, false);
                        this.DisplayMySlaves();
                    }
                    if (Task.PropitiateSlave)
                        this.PropitiateSlaves();
                    if (Task.AfflictSlave)
                        this.AfflictSlaves();
                    if (Task.ReleaseSlave)
                        this.ReleaseSlaves();
                }

                SetMessageLn("����������ɣ�");

            });
            base.ExecuteTryCatchBlock(th, "�����쳣����������ʧ�ܣ�");
        }
        #endregion

        #region BuySlaveFromBuyList
        private void BuySlaveFromBuyList()
        {
            try
            {
                SetMessageLn("���������е�ū����");
                if (this._slaveNum >= this.Task.MaxSlaves)
                {
                    SetMessageLn(string.Format(" ū����:{0} �ѵ��ﹺ������({1})�����ܹ���", new object[] { this._slaveNum, this.Task.MaxSlaves }));
                }
                else
                {
                    int num = 0;
                    foreach (int uid in Operation.BuyWhiteList)
                    {
                        if (this._slaveNum >= this.Task.MaxSlaves)
                            break;
                        if (IsAlreadyMySlave(uid))
                            continue;
                        SetMessageLn(string.Format("��Ҫ���ū��#{0}:{1}", ++num, this.GetFriendNameById(uid)));
                        if (this.BuyTheSlave(uid))
                            this._slaveNum++;
                        if (this._slavecash < 500)
                            break;
                    }
                }
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
                LogHelper.Write("GameSlave.BuySlaveFromBuyList", ex, LogSeverity.Error);
                SetMessage(" ���������е�ū��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region BuyLowPriceSlave
        private void BuyLowPriceSlave()
        {
            try
            {
                if (this.Task.BuyLowPriceSlave)
                {
                    SetMessageLn("����ͼ�ū����");
                    if (this._slaveNum >= this.Task.MaxSlaves)
                    {
                        SetMessageLn(string.Format(" ū����:{0} �ѵ��ﹺ������({1})�����ܹ���", new object[] { this._slaveNum, this.Task.MaxSlaves }));
                        return;
                    }

                    //get buyable slaves
                    ReadBuyableSlaves(RequestBuyableSlaves(), false);

                    if (_buyableSlaveList.Count <= 0)
                    {
                        SetMessageLn("�㵱ǰ���ֽ��㣬�޷������κ���Ϊū��");
                        return;
                    }

                    int num = 0;
                    foreach (FriendInfo slave in _buyableSlaveList)
                    {
                        if (this._slaveNum >= this.Task.MaxSlaves)
                            break;
                        if (IsAlreadyMySlave(slave.Id))
                            continue;
                        SetMessageLn(string.Format("�����ū��#{0}:{1}(���:��{2})", ++num, slave.Name, slave.Price.ToString()));
                        if (this.BuyTheSlave(slave.Id))
                            this._slaveNum++;
                        if (this._slavecash < slave.Price)
                            break;
                    }
                }
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
                LogHelper.Write("GameSlave.BuyLowPriceSlave", ex, LogSeverity.Error);
                SetMessage(" ����ͼ�ū��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region PropitiateSlaves
        private void PropitiateSlaves()
        {
            try
            {
                SetMessageLn("����ū����");
                int num = 0;
                foreach (FriendInfo slave in this._mySlaveList)
                {
                    SetMessageLn(string.Format("ū��#{0}:{1} ", ++num, slave.Name));
                    //string content = HH.Get(string.Format("http://www.kaixin001.com/slave/comfort1.php?verify={0}&slaveuid={1}&comforttype={2}", this._verifyCode, slave.Id.ToString(), GetComfortType(slave.Id.ToString(), !slave.Gender)));
                    string comfortType = GetComfortType(slave.Id.ToString(), !slave.Gender);
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    string content = HH.Post("http://www.kaixin001.com/slave/comfort1.php", string.Format("verify={0}&slaveuid={1}&comforttype={2}", this._verifyCode, slave.Id.ToString(), comfortType));
                    this.GetFeedback(content);
                }
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
                LogHelper.Write("GameSlave.PropitiateSlaves", ex, LogSeverity.Error);
                SetMessage(" ����ū��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region AfflictSlaves
        private void AfflictSlaves()
        {
            try
            {
                SetMessageLn("��ū����");
                int num = 0;
                string paintype = "";
                foreach (FriendInfo slave in this._mySlaveList)
                {
                    SetMessageLn(string.Format("ū��#{0}:{1} ", new object[] { ++num, slave.Name }));
                    //6 ȥ��úҤ��ú 
                    //8 ȥ��������                 
                    //paintype = (slave.Gender) ? "6" : "8";
                    //18 ȥ�����
                    //17 ȥ��С��ķ
                    paintype = (slave.Gender) ? "18" : "17";
                    //string content = HH.Get(string.Format("http://www.kaixin001.com/slave/pain2.php?verify={0}&slaveuid={1}&paintype={2}", this._verifyCode, slave.Id.ToString(), paintype));
                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                    string content = HH.Post("http://www.kaixin001.com/slave/pain2.php", string.Format("verify={0}&slaveuid={1}&paintype={2}", this._verifyCode, slave.Id.ToString(), paintype));
                    if (!IsSuffering(content))
                    {
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        content = HH.Post("http://www.kaixin001.com/slave/pain1_submit.php", string.Format("verify={0}&slaveuid={1}&paintype={2}", this._verifyCode, slave.Id.ToString(), paintype));
                        if (content.IndexOf("pain1.php") != -1)
                        {
                            //<input type=hidden name=verify value=\"2588258_1028_2588258_1229396025_666b8495bee2045945861962214f4b81\">
                            //<input type=hidden name=slaveuid value=\"1534333\">
                            //<input type=hidden name=paintype value=\"17\">
                            //<input type=hidden name=hour value="3">
                            //<input type=hidden name=diffprice value=\"\">
                            //<input type=hidden name=paintimes value=\"1\">
                            //<input type=hidden name=endtime value=\"\">
                            //<input type=hidden name=flag value=\"right17\">
                            //<input type=hidden name=actionpart1 value=\"\">
                            //<input type=hidden name=actionpart2 value=\"\">
                            string hour = JsonHelper.GetMid(content, "name=hour value=\"", "\">");
                            string diffprice = JsonHelper.GetMid(content, "name=diffprice value=\"", "\">");
                            string paintimes = JsonHelper.GetMid(content, "name=paintimes value=\"", "\">");
                            string endtime = JsonHelper.GetMid(content, "name=endtime value=\"", "\">");
                            string flag = JsonHelper.GetMid(content, "name=flag value=\"", "\">");
                            string actionpart1 = JsonHelper.GetMid(content, "name=actionpart1 value=\"", "\">");
                            string actionpart2 = JsonHelper.GetMid(content, "name=actionpart2 value=\"", "\">");
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            content = HH.Post("http://www.kaixin001.com/slave/pain1.php", string.Format("verify={0}&slaveuid={1}&paintype={2}&hour={3}&diffprice={4}&paintimes={5}&endtime={6}&flag={7}&actionpart1={8}&actionpart2={9}", this._verifyCode, slave.Id.ToString(), paintype, hour, diffprice, paintimes, endtime, flag, actionpart1, actionpart2));
                        }
                    }
                    this.GetFeedback(content);
                }
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
                LogHelper.Write("GameSlave.AfflictSlaves", ex, LogSeverity.Error);
                SetMessage(" ��ū��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region ReleaseSlaves
        private void ReleaseSlaves()
        {
            try
            {
                SetMessageLn("�ͷ�ū����");
                int num = 0;
                bool flag = false;
                foreach (FriendInfo slave in this._mySlaveList)
                {
                    SetMessageLn(string.Format("ū��#{0}:{1}(���:��{2}) ", ++num, slave.Name, slave.Price));
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    string content = HH.Get(string.Format("http://www.kaixin001.com/slave/free_dialog.php?slaveuid={0}&verify={1}", slave.Id.ToString(), this._verifyCode));
                    if (content.IndexOf("$(\"flag2\").style.display") != -1)
                    {
                        string param = "verify=" + this._verifyCode + "&slaveuid=" + slave.Id.ToString();
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        HH.Post("http://www.kaixin001.com/slave/free.php", param);
                        SetMessage("�ͷųɹ���");
                        flag = true;
                    }
                    else
                    {
                        SetMessage("δ�����죬�����ͷţ�");
                    }
                }
                if (flag)
                {
                    string content = RequestSlaveHomePage();
                    ReadSlaves(content, false);
                }
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
                LogHelper.Write("GameSlave.ReleaseSlaves", ex, LogSeverity.Error);
                SetMessage(" �ͷ�ū��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region FawnMaster
        private void FawnMaster()
        {
            try
            {
                SetMessageLn("�ֺ����ˣ�");

                if (_hostuid == -1)
                {
                    SetMessageLn("Ŀǰ����������");
                    return;
                }
                //string content = HH.Get(string.Format("http://www.kaixin001.com/slave/fawn1.php?verify={0}&hostuid={1}&fawntype={2}", this._verifyCode, this._hostuid.ToString(), GetFawnType()));
                string fawnType = GetFawnType();
                HH.DelayedTime = Constants.DELAY_1SECONDS;
                string content = HH.Post("http://www.kaixin001.com/slave/fawn1.php", string.Format("verify={0}&hostuid={1}&fawntype={2}", this._verifyCode, this._hostuid.ToString(), fawnType));
                this.GetFeedback(content);
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
                LogHelper.Write("GameSlave.FawnMaster", ex, LogSeverity.Error);
                SetMessage(" �ֺ�����ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region IsAlreadyMySlave
        private bool IsAlreadyMySlave(int slaveuid)
        {
            foreach (FriendInfo friend in this._mySlaveList)
            {
                if (friend.Id == slaveuid)
                    return true;
            }

            return false;
        }
        #endregion

        #region BuyTheSlave
        private bool BuyTheSlave(int slaveuid)
        {
            if (Operation.BuyBlackList.Contains(slaveuid))
            {
                SetMessage("#" + GetFriendNameById(slaveuid) + "����������������ū�� ");
                return false;
            }
            string slaveNick = Task.NickName;
            if (slaveNick == null)
            {
                slaveNick = string.Empty;
            }
            else
            {
                slaveNick = DataConvert.GetEncodeData(slaveNick);
            }

            //get slave info   
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/slave/buy_dialog.php?slaveuid={0}&verify={1}", slaveuid, this._verifyCode));
            if (!GetBuySlaveFeedback(content))
                return false;

            //current price
            int slaveprice = JsonHelper.GetInteger(JsonHelper.GetMid(content, "���뻨 <strong class=\"dgreen\">&yen;", "</strong> �ļ۸���"));

            //request for buying            
            string param = "verify=" + this._verifyCode + "&slaveuid=" + slaveuid + "&nick=" + slaveNick + "&acc=" + GetAccCode(content);
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            content = HH.Post("http://www.kaixin001.com/slave/buy.php", param);
            if (!GetBuySlaveFeedback(content))
                return false;

            if (this.GetFeedback(content))
            {
                FriendInfo slave = new FriendInfo();
                slave.Id = Convert.ToInt32(slaveuid);
                slave.Name = JsonHelper.GetMid(content, "<span class=\"sl\">", "<");
                if (slave.Name == null)
                {
                    slave.Name = string.Empty;
                }
                slave.Gender = JsonHelper.GetMid(content, "<br />ͬʱ��", "��") == "��" ? true : false;
                slave.Price = JsonHelper.GetInteger(JsonHelper.GetMid(content, "&yen;", "<"));
                this._mySlaveList.Add(slave);
                this._slavecash -= slaveprice;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region GetComfortType
        private string GetComfortType(string userid, bool female)
        {
            string comfortType = "";
            //6 ������Ư�������·�
            //7 ������Ư�������·�
            comfortType = (female) ? "6" : "7";

            string content = "";
            int num = 0;
            try
            {
                HH.DelayedTime = Constants.DELAY_1SECONDS;
                content = HH.Get(string.Format("http://www.kaixin001.com/slave/comfort_dialog.php?slaveuid={0}&verify={1}", userid, this._verifyCode));
                for (string pos = JsonHelper.GetMid(content, "name=\"comforttype\" value=\"", "\">", out num); pos != null; pos = JsonHelper.GetMid(content, "name=\"comforttype\" value=\"", "\">", out num))
                {
                    content = content.Substring(num);
                    int type = JsonHelper.GetInteger(pos);
                    if (type > 0)
                    {
                        return type.ToString();
                    }
                }
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
                LogHelper.Write("GameSlave.GetComfortType", content, ex, LogSeverity.Error);
            }
            return comfortType;
        }
        #endregion

        #region GetFawnType
        private string GetFawnType()
        {
            string fawnType = "1"; //�����밲            
            string content = "";

            int num = 0;
            try
            {
                HH.DelayedTime = Constants.DELAY_1SECONDS;
                content = HH.Get(string.Format("http://www.kaixin001.com/slave/fawn_dialog.php?hostuid={0}&verify={1}", this._hostuid, this._verifyCode));
                for (string pos = JsonHelper.GetMid(content, "<input type=\"radio\" name=\"fawntype\" value=\"", "\">", out num); pos != null; pos = JsonHelper.GetMid(content, "<input type=\"radio\" name=\"fawntype\" value=\"", "\">", out num))
                {
                    content = content.Substring(num);
                    int type = JsonHelper.GetInteger(pos);
                    if (type > 0)
                    {
                        return type.ToString();
                    }
                }
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
                LogHelper.Write("GameSlave.GetComfortType", content, ex, LogSeverity.Error);
            }
            return fawnType;
        }
        #endregion

        #region GetFeedback
        private bool GetFeedback(string content)
        {
            try
            {
                //<script type="text/javascript">
                //    $("error141").style.display = "block";
                //</script>
                bool ret = true;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "\t$(\"", "\")");
                if (strdivid != null)
                {
                    if (strdivid.StartsWith("error"))
                        ret = false;

                    //<div id="error141" style="display:none;">
                    //    <div  style="padding:20px 0 20px 145px;">
                    //        <div class="dealimg"><img src="http://pic.kaixin001.com/logo/73/38/120_6733829_1.jpg" /></div>
                    //        <div class="c"></div>
                    //    </div>
                    //    <div class="tac f14"><strong>������Ѿ�������ū��<span class="sl">������</span>��</strong></div>
                    //    <div class="tac f14"><strong>�����ٰ���һ�Σ�����������</strong></div>
                    //    <div style="padding:50px 166px;">
                    //    <div class="rbs1">
                    //        <input type="button" value="ȷ��" class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';"  style="padding:4px 15px;" onclick="new parent.dialog().reset();" /></div>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, strdivid, "<input type=\"button\"");
                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));
                }
                else
                {
                    //<div class=\"f14 tac\" >\n\t\t\t<strong>ū��Ҳ���˰�������Ҫ��������Ѿ�������һ����</strong>\n\t\t</div>\n\t\t<div class=\"f14 tac\" ><strong>����Ķ�<span class=\"sl\">ͨ��</span>��֮��ǣ�Ҫ������һ�β�ˬ��</strong></div>
                    //������Ѿ����ڹ���״̬ʱ
                    strdivid = JsonHelper.GetFirstLast(content, "<strong>", "</strong>");
                    strmsg = JsonHelper.FiltrateHtmlTags(strdivid);
                }
                SetMessage(" " + strmsg);
                return ret;
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
                LogHelper.Write("GameSlave.GetFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region IsSuffering
        private bool IsSuffering(string content)
        {
            try
            {
                //<script type="text/javascript">
                //    $("error141").style.display = "block";
                //</script>
                bool ret = false;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "\t$(\"", "\")");
                if (strdivid != null)
                {
                    //<div id="error141" style="display:none;">
                    //    <div  style="padding:20px 0 20px 145px;">
                    //        <div class="dealimg"><img src="http://pic.kaixin001.com/logo/73/38/120_6733829_1.jpg" /></div>
                    //        <div class="c"></div>
                    //    </div>
                    //    <div class="tac f14"><strong>������Ѿ�������ū��<span class="sl">������</span>��</strong></div>
                    //    <div class="tac f14"><strong>�����ٰ���һ�Σ�����������</strong></div>
                    //    <div style="padding:50px 166px;">
                    //    <div class="rbs1">
                    //        <input type="button" value="ȷ��" class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';"  style="padding:4px 15px;" onclick="new parent.dialog().reset();" /></div>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, strdivid, "<input type=\"button\"");
                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));
                }
                else
                {
                    //<div class=\"f14 tac\" >\n\t\t\t<strong>ū��Ҳ���˰�������Ҫ��������Ѿ�������һ����</strong>\n\t\t</div>\n\t\t<div class=\"f14 tac\" ><strong>����Ķ�<span class=\"sl\">ͨ��</span>��֮��ǣ�Ҫ������һ�β�ˬ��</strong></div>
                    //������Ѿ����ڹ���״̬ʱ
                    strdivid = JsonHelper.GetFirstLast(content, "<strong>", "</strong>");
                    strmsg = JsonHelper.FiltrateHtmlTags(strdivid);
                }
                if (strmsg.IndexOf("ū��Ҳ���˰�������Ҫ���") != -1 || strmsg.IndexOf("���ʱ����Ͳ�Ҫ����") != -1 || strmsg.IndexOf("��Ǹ") != -1)
                    ret = true;
                return ret;
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
                LogHelper.Write("GameSlave.IsSuffering", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region GetBuySlaveFeedback
        private bool GetBuySlaveFeedback(string content)
        {
            try
            {
                //<script type="text/javascript">
                //    $("error52").style.display = "block";
                //</script>
                bool ret = true;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "\t$(\"", "\")");
                if (strdivid != null)
                {
                    if (strdivid.StartsWith("error"))
                    {
                        ret = false;
                        //<div id="error52" style="display:none;width:445px;">
                        //    <div style="height:390px;">
                        //        <div  style="padding:20px 0 20px 145px;">
                        //            <div class="dealimg"><img src="http://img.kaixin001.com.cn/i/120_0_0.gif" /></div>
                        //            <div class="c"></div>
                        //        </div>
                        //        <div class="f14 tac" style="width:22em;margin:10px auto;">
                        //            <div class=""><strong>���뻨 <strong class="dgreen">&yen;500</strong> �ļ۸��� <span class="sl">��ũ</span> Ϊū��</strong></div>
                        //            <div class="mt5"><strong>����ֻ���ֽ� <strong class="dgreen">&yen;0</strong> �����ǲ�����͸֧���ף�</strong></div>
                        //        </div>
                        //    </div>
                        //    <div style="height:40px;border-top:1px solid #ccc;background:#F2F2F2;">
                        //    <div class="r" style="width:10px;">&nbsp;</div>
                        //        <div class="rbs1 mt5" style="float:right;">
                        //            <input type="button" value="ȡ��" class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';"  style="padding:4px 10px;" onclick="new parent.dialog().reset();" /></div>
                        //    </div>                	
                        //</div>
                        strmsg = JsonHelper.GetMid(content, strdivid, "<input type=\"button\"");
                        int index = strmsg.IndexOf(">");
                        strmsg = strmsg.Substring(index + 1);
                        strmsg = JsonHelper.GetFirstLast(strmsg, "<strong>", "</strong>");
                        strmsg = JsonHelper.FiltrateHtmlTags(strmsg);
                    }
                }
                if (!ret)
                    SetMessage(" " + strmsg);
                return ret;
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
                LogHelper.Write("GameSlave.GetBuySlaveFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region Request

        public string RequestSlaveHomePage()
        {
            string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1028");
            if (content.IndexOf("<title>������ - ������</title>") != -1)
            {
                SetMessageLn("��δ��װ�����������,���԰�װ��...");
                HH.Post("http://www.kaixin001.com/app/install.php", "aid=1028&isinstall=1");
                content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1028");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            this._slavecash = JsonHelper.GetInteger(JsonHelper.GetMid(content, "<p>�֡���<strong class=\"dgreen\">&yen;", "</strong></p>"));
            this._hostuid = JsonHelper.GetInteger(JsonHelper.GetMid(content, "javascript:fawnHost(", ");"));
            return content;
        }

        public string RequestBuyableSlaves()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/slave/selslave_dialog.php?verify=" + this._verifyCode);
        }

        #endregion

        #region Properties
        public Collection<FriendInfo> MySlaveList
        {
            get { return _mySlaveList; }
            set { _mySlaveList = value; }
        }
        public Collection<FriendInfo> BuyableSlaveList
        {
            get { return _buyableSlaveList; }
        }
        #endregion
    }
}
