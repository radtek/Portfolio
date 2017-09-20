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

                SetMessageLn("开始导出所有好友列表...");

                //start
                SetMessage("\r\n" + "============================== 开始 ==============================");

                int num = 0;
                foreach (AccountInfo account in _accounts)
                {
                    try
                    {
                        num++;
                        SetMessageLn("------ 共" + _accounts.Count + "个帐户，第" + num + "个帐户：" + account.UserName + "(" + account.Email + ") ------");
                        if (!this.ValidationLogin(account))
                        {
                            continue;
                        }

                        SetMessageLn("取得[我在开心网上的所有好友]...");
                        string content = RequestAllMyFriends();
                        ReadAllMyFriends(content, false);
                        SetMessageLn("[我在开心网上的所有好友]信息取得成功！");
                        this.LogOut(true);

                        SetMessageLn("保存所有好友信息至文件...");
                        string result = ConfigCtrl.SaveContactToFile(_path, account, base.AllMyFriendsList);
                        if (result == Constants.STATUS_SUCCESS)
                            SetMessageLn("成功！");
                        else
                            SetMessageLn("失败：" + result);
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
                        SetMessageLn("发生异常，此账户操作失败！错误：" + ex.Message);
                        continue;
                    }                    
                }

                SetMessage("\r\n" + "============================== 完成 ==============================");
                if (AllFetchFinished != null)
                    AllFetchFinished();
            });
            base.ExecuteTryCatchBlock(th, "发生异常，导出所有好友列表失败！");
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

                SetMessageLn("开始发送添加好友请求...");

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
                    SetMessageLn(string.Format("向#{0} {1}({2})发送请求：", num, friend.Name, friend.Id));
                    if (this.SendFriendRequest(friend.Id, _requestcontent))
                        SetMessage("成功！");
                    else
                        SetMessage("失败！");
                }

                SetMessageLn("发送添加好友请求完成！");
                if (SendRequestFinished != null)
                    SendRequestFinished();
            });
            base.ExecuteTryCatchBlock(th, "发生异常，发送添加好友请求失败！");
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
