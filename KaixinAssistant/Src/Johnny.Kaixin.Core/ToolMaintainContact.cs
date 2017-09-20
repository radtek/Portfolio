using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class ToolMaintainContact : KaixinBase
    {
        //output
        public string _path;
        public Collection<AccountInfo> _accounts;

        //sendrequest
        public AccountInfo _account;
        public string _requestcontent;
        public Collection<FriendInfo> _friends;

        //Event
        public delegate void AllFetchFinishedEventHandler();
        public event AllFetchFinishedEventHandler AllFetchFinished;

        public delegate void SendRequestFinishedEventHandler();
        public event SendRequestFinishedEventHandler SendRequestFinished;

        public ToolMaintainContact()
        {
            base.Caption = Constants.TOOL_MAINTAINCONTACT;
            base.Key = Constants.TOOL_MAINTAINCONTACT;
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();
        }

        #region SaveFriendsToFileByThread
        public void SaveFriendsToFileByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(SaveFriendsToFile));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void SaveFriendsToFile()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_MAINTAINCONTACT;

                SetMessageLn("��ʼ�������к����б�...");

                //start
                SetMessage("\r\n" + "============================== ��ʼ ==============================");

                int num = 0;
                foreach (AccountInfo account in _accounts)
                {
                    try
                    {
                        num++;
                        SetMessageLn("------ ��" + _accounts.Count + "���ʻ�����" + num + "���ʻ���" + account.UserName + "(" + account.Email + ") ------");
                        if (!this.ValidationLogin(account))
                        {
                            continue;
                        }

                        SetMessageLn("ȡ��[���ڿ������ϵ����к���]...");
                        string content = RequestAllMyFriends();
                        ReadAllMyFriends(content, false);
                        SetMessageLn("[���ڿ������ϵ����к���]��Ϣȡ�óɹ���");
                        this.LogOut(true);

                        SetMessageLn("�������к�����Ϣ���ļ�...");
                        string result = ConfigCtrl.SaveContactToFile(_path, account, base.AllMyFriendsList);
                        if (result == Constants.STATUS_SUCCESS)
                            SetMessageLn("�ɹ���");
                        else
                            SetMessageLn("ʧ�ܣ�" + result);
                    }
                    catch (ThreadAbortException)
                    {
                        LogHelper.Write("ToolMaintainContact.SaveFriendsToFile", account.UserName, LogSeverity.Info);
                    }
                    catch (ThreadInterruptedException)
                    {
                        LogHelper.Write("ToolMaintainContact.SaveFriendsToFile", account.UserName, LogSeverity.Info);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("ToolMaintainContact.SaveFriendsToFile", account.UserName, ex, LogSeverity.Error);
                        SetMessageLn("�����쳣�����˻�����ʧ�ܣ�����" + ex.Message);
                        continue;
                    }                    
                }

                SetMessage("\r\n" + "============================== ��� ==============================");
                if (AllFetchFinished != null)
                    AllFetchFinished();
            });
            base.ExecuteTryCatchBlock(th, "�����쳣���������к����б�ʧ�ܣ�");
        }
        #endregion

        #region SendRequestByThread
        public void SendRequestByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(SendRequest));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void SendRequest()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_MAINTAINCONTACT;

                SetMessageLn("��ʼ������Ӻ�������...");

                if (!this.ValidationLogin(_account))
                {
                    if (SendRequestFinished != null)
                        SendRequestFinished();
                    return;
                }

                int num = 0;
                foreach (FriendInfo friend in _friends)
                {
                    num++;
                    SetMessageLn(string.Format("��#{0} {1}({2})��������", num, friend.Name, friend.Id));
                    if (this.SendFriendRequest(friend.Id, _requestcontent))
                        SetMessage("�ɹ���");
                    else
                        SetMessage("ʧ�ܣ�");
                }

                SetMessageLn("������Ӻ���������ɣ�");
                if (SendRequestFinished != null)
                    SendRequestFinished();
            });
            base.ExecuteTryCatchBlock(th, "�����쳣��������Ӻ�������ʧ�ܣ�");
        }
        #endregion

        #region SendFriendRequest
        private bool SendFriendRequest(int uid, string requestcontent)
        {
            string param = string.Format("from=&touid={0}&content={1}", uid, DataConvert.GetEncodeData(requestcontent));
            string content = HH.Post("http://www.kaixin001.com/friend/addverify.php", param);

            if (content.IndexOf("var type = \"4\"") != -1)
                return true;
            else
                return false;
        }
        #endregion
    }
}
