using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class ToolAddFriends : KaixinBase
    {
        private AddFriendsInfo _addFriends;

        public delegate void AddFriendsFinishedEventHandler();
        public event AddFriendsFinishedEventHandler AddFriendsFinished;

        public ToolAddFriends()
        {
            base.Caption = Constants.TOOL_ADDFRIENDS;
            base.Key = Constants.TOOL_ADDFRIENDS;            
        }

        public void AddFriends(AddFriendsInfo addfriends)
        {
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();

            this._addFriends = addfriends;

            _threadMain = new Thread(new System.Threading.ThreadStart(RunAddFriend));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        #region 互加好友
        private void RunAddFriend()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_ADDFRIENDS;

                SetMessageLn("开始互加好友...");

                if (_addFriends.DeleteAllMessages)
                {
                    SetMessageLn("清空所有账号的系统消息");
                    int num = 0;
                    foreach (AccountInfo account in _addFriends.Accounts)
                    {
                        num++;
                        SetMessageLn("正在清空#" + num + ":" + account.UserName + "(" + account.Email + ")" + "的所有系统消息...");
                        if (!this.ValidationLogin(account, false))
                        {
                            //SetMessageLn(account.UserName + "(" + account.Email + ")" + "登录失败！");
                            continue;
                        }
                        HH.Post("http://www.kaixin001.com/msg/clear_sys_msg.php", "");
                        SetMessageLn(account.UserName + "(" + account.Email + ")" + "的所有系统消息已被删除！");
                    }
                }

                if (!_addFriends.ExecuteSendRequest && !_addFriends.ExecuteConfirmRequest)
                    return;

                if (_addFriends.AddMode)
                {
                    int num;
                    if (_addFriends.ExecuteSendRequest)
                    {
                        SetMessageLn("发送添加好友请求：");
                        num = 0;
                        for (int ix = 0; ix < _addFriends.Accounts.Count; ix++)
                        {
                            num++;
                            SetMessageLn("账号#" + num + ":" + _addFriends.Accounts[ix].UserName + "(" + _addFriends.Accounts[ix].Email + ")" + "开始发送请求...");
                            if (!this.ValidationLogin(_addFriends.Accounts[ix], true))
                            {
                                continue;
                            }

                            string content = RequestAllMyFriends();
                            ReadAllMyFriends(content, false);

                            int newcount = 0;
                            for (int iy = ix + 1; iy < _addFriends.Accounts.Count; iy++)
                            {
                                if (!IsAlreadyMyFriend(_addFriends.Accounts[iy].UserId))
                                {
                                    newcount++;
                                    SetMessageLn(string.Format("添加好友请求#{0} {1}({2}) :", newcount.ToString(), _addFriends.Accounts[iy].UserName, _addFriends.Accounts[iy].UserId));
                                    if (this.SendFriendRequest(_addFriends.Accounts[iy].UserId))
                                        SetMessage("成功！");
                                    else
                                        SetMessage("失败！");
                                }
                            }
                        }
                    }

                    if (_addFriends.ExecuteConfirmRequest)
                    {
                        SetMessageLn("确认好友请求：");
                        num = 0;
                        foreach (AccountInfo account in _addFriends.Accounts)
                        {
                            num++;
                            SetMessageLn("账号#" + num + ":" + account.UserName + "(" + account.Email + ")" + "开始确认请求...");
                            if (!this.ValidationLogin(account, true))
                                continue;
                            this.ConfirmFriend();
                        }
                    }
                }
                else
                {
                    int num;
                    if (_addFriends.ExecuteSendRequest)
                    {
                        SetMessageLn("发送添加好友请求：");
                        num = 0;
                        foreach (AccountInfo account in _addFriends.NewAccountsList)
                        {
                            num++;
                            SetMessageLn("新账号#" + num + ":" + account.UserName + "(" + account.Email + ")" + "开始发送请求...");
                            if (!this.ValidationLogin(account, true))
                            {
                                continue;
                            }

                            this.AddAllList(account, _addFriends.NewAccountsList, true);
                            this.AddAllList(account, _addFriends.OldAccountsList, false);
                        }
                    }

                    if (_addFriends.ExecuteConfirmRequest)
                    {
                        SetMessageLn("确认好友请求：");
                        num = 0;
                        foreach (AccountInfo account in _addFriends.NewAccountsList)
                        {
                            num++;
                            SetMessageLn("新账号#" + num + ":" + account.UserName + "(" + account.Email + ")" + "开始确认请求...");
                            if (!this.ValidationLogin(account, true))
                                continue;
                            this.ConfirmFriend();
                        }
                        num = 0;
                        foreach (AccountInfo account in _addFriends.OldAccountsList)
                        {
                            num++;
                            SetMessageLn("旧账号#" + num + ":" + account.UserName + "(" + account.Email + ")" + "开始确认请求...");
                            if (!this.ValidationLogin(account, true))
                                continue;
                            this.ConfirmFriend();
                        }
                    }
                }

                SetMessageLn("互加好友完成！");
                if (AddFriendsFinished != null)
                    AddFriendsFinished();
            });
            base.ExecuteTryCatchBlock(th, "发生异常，互加好友失败！");
        }

        private void AddAllList(AccountInfo currentuser, Collection<AccountInfo> accountsList, bool isnew)
        {
            int num = 0;
            foreach (AccountInfo account in accountsList)
            {
                if (currentuser.UserId == account.UserId)
                    continue;

                num++;
                if (isnew)
                    SetMessageLn(string.Format("添加好友请求新账号#{0} {1}({2}) :", num, account.UserName, account.UserId));
                else
                    SetMessageLn(string.Format("添加好友请求旧账号#{0} {1}({2}) :", num, account.UserName, account.UserId));
                if (this.SendFriendRequest(account.UserId))
                    SetMessage("成功！");
                else
                    SetMessage("失败！");
            }
        }

        private bool SendFriendRequest(string userid)
        {
            string content = HH.Get("http://www.kaixin001.com/home/?uid=" + userid);
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            content = HH.Get("http://www.kaixin001.com/friend/new.php?touid=" + userid, "http://www.kaixin001.com/home/?uid=" + userid);
            string param = string.Format("from=&touid={0}&content=hi&rcode=&code=&usercode=&email=&bidirection=", userid);
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            content = HH.Post("http://www.kaixin001.com/friend/addverify.php", "http://www.kaixin001.com/friend/new.php?touid=" + userid, param);

            if (content.IndexOf("var type = \"4\"") != -1)
                return true;
            else
            {
                LogHelper.Write("ToolAddFriends.SendFriendRequest()", content, LogSeverity.Warn);
                return false;
            }
        }

        private void ConfirmFriend()
        {
            int page = 0;
            do
            {
                //防止死循环
                if (page > 20)
                    return;

                HH.DelayedTime = Constants.DELAY_1SECONDS;
                string content = HH.Get("http://www.kaixin001.com/msg/sys.php?type=1&start=" + (page * 10).ToString());

                if (string.IsNullOrEmpty(content))
                {
                    SetMessageLn("没有加好友请求消息");
                    return;
                }
                if (content.IndexOf("消息中心") > -1)
                {
                    SetMessageLn("没有加好友请求消息");
                    return;
                }
                string temp = JsonHelper.GetMid(content, "javascript:agreefriend(", ");\" class=\"sl\">同意");
                if (string.IsNullOrEmpty(temp))
                {
                    SetMessageLn("没有加好友请求消息");
                    return;
                }

                page++;

                SetMessageLn("第" + page.ToString() + "页");

                int num;
                for (string info = JsonHelper.GetMid(content, "javascript:agreefriend(", ");\" class=\"sl\">同意", out num); info != null; info = JsonHelper.GetMid(content, "javascript:agreefriend(", ");\" class=\"sl\">同意", out num))
                {
                    content = content.Substring(num);
                    string[] arrayAgree = new string[2];
                    arrayAgree = info.Split(',');
                    if (arrayAgree != null)
                    {
                        AgreeFriend(arrayAgree[0], arrayAgree[1]);
                    }
                }
            }
            while (true);
        }
       

        private void AgreeFriend(string fuid, string smid)
        {
            try
            {
                HH.DelayedTime = Constants.DELAY_2SECONDS;
                string content = HH.Get(string.Format("http://www.kaixin001.com/friend/editfriend_dialog.php?smid={0}&fuid={1}", smid, fuid));
                string param = string.Format("fuid={0}&byname=&memo=&groups=&allgroups=%E7%8E%B0%E5%9C%A8%E5%90%8C%E4%BA%8B%2C%E4%BB%A5%E5%89%8D%E5%90%8C%E4%BA%8B%2C%E5%A4%A7%E5%AD%A6%E5%90%8C%E5%AD%A6%2C%E9%AB%98%E4%B8%AD%E5%90%8C%E5%AD%A6%2C%E5%AE%B6%E4%BA%BA%E4%BA%B2%E6%88%9A%2C%E6%8C%9A%E4%BA%A4%E5%A5%BD%E5%8F%8B%2C%E6%99%AE%E9%80%9A%E6%9C%8B%E5%8F%8B%2C%E6%9C%8B%E5%8F%8B%E7%9A%84%E6%9C%8B%E5%8F%8B%2C%E5%85%B6%E4%BB%96&start=0&sysmsgtype=1&from=sysmsg&smid={1}", fuid, smid);
                HH.DelayedTime = Constants.DELAY_2SECONDS;
                HH.Post("http://www.kaixin001.com/friend/editfriend.php", param);
                SetMessageLn(string.Format("已同意{0}的好友请求!", this.GetFriendNameById(fuid)));
            }
            catch (OverflowException)
            {
            }
            catch (FormatException)
            {
            }
        }


        #endregion
    }
}
