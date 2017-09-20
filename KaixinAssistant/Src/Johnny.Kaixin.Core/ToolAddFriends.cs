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

        #region ���Ӻ���
        private void RunAddFriend()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_ADDFRIENDS;

                SetMessageLn("��ʼ���Ӻ���...");

                if (_addFriends.DeleteAllMessages)
                {
                    SetMessageLn("��������˺ŵ�ϵͳ��Ϣ");
                    int num = 0;
                    foreach (AccountInfo account in _addFriends.Accounts)
                    {
                        num++;
                        SetMessageLn("�������#" + num + ":" + account.UserName + "(" + account.Email + ")" + "������ϵͳ��Ϣ...");
                        if (!this.ValidationLogin(account, false))
                        {
                            //SetMessageLn(account.UserName + "(" + account.Email + ")" + "��¼ʧ�ܣ�");
                            continue;
                        }
                        HH.Post("http://www.kaixin001.com/msg/clear_sys_msg.php", "");
                        SetMessageLn(account.UserName + "(" + account.Email + ")" + "������ϵͳ��Ϣ�ѱ�ɾ����");
                    }
                }

                if (!_addFriends.ExecuteSendRequest && !_addFriends.ExecuteConfirmRequest)
                    return;

                if (_addFriends.AddMode)
                {
                    int num;
                    if (_addFriends.ExecuteSendRequest)
                    {
                        SetMessageLn("������Ӻ�������");
                        num = 0;
                        for (int ix = 0; ix < _addFriends.Accounts.Count; ix++)
                        {
                            num++;
                            SetMessageLn("�˺�#" + num + ":" + _addFriends.Accounts[ix].UserName + "(" + _addFriends.Accounts[ix].Email + ")" + "��ʼ��������...");
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
                                    SetMessageLn(string.Format("��Ӻ�������#{0} {1}({2}) :", newcount.ToString(), _addFriends.Accounts[iy].UserName, _addFriends.Accounts[iy].UserId));
                                    if (this.SendFriendRequest(_addFriends.Accounts[iy].UserId))
                                        SetMessage("�ɹ���");
                                    else
                                        SetMessage("ʧ�ܣ�");
                                }
                            }
                        }
                    }

                    if (_addFriends.ExecuteConfirmRequest)
                    {
                        SetMessageLn("ȷ�Ϻ�������");
                        num = 0;
                        foreach (AccountInfo account in _addFriends.Accounts)
                        {
                            num++;
                            SetMessageLn("�˺�#" + num + ":" + account.UserName + "(" + account.Email + ")" + "��ʼȷ������...");
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
                        SetMessageLn("������Ӻ�������");
                        num = 0;
                        foreach (AccountInfo account in _addFriends.NewAccountsList)
                        {
                            num++;
                            SetMessageLn("���˺�#" + num + ":" + account.UserName + "(" + account.Email + ")" + "��ʼ��������...");
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
                        SetMessageLn("ȷ�Ϻ�������");
                        num = 0;
                        foreach (AccountInfo account in _addFriends.NewAccountsList)
                        {
                            num++;
                            SetMessageLn("���˺�#" + num + ":" + account.UserName + "(" + account.Email + ")" + "��ʼȷ������...");
                            if (!this.ValidationLogin(account, true))
                                continue;
                            this.ConfirmFriend();
                        }
                        num = 0;
                        foreach (AccountInfo account in _addFriends.OldAccountsList)
                        {
                            num++;
                            SetMessageLn("���˺�#" + num + ":" + account.UserName + "(" + account.Email + ")" + "��ʼȷ������...");
                            if (!this.ValidationLogin(account, true))
                                continue;
                            this.ConfirmFriend();
                        }
                    }
                }

                SetMessageLn("���Ӻ�����ɣ�");
                if (AddFriendsFinished != null)
                    AddFriendsFinished();
            });
            base.ExecuteTryCatchBlock(th, "�����쳣�����Ӻ���ʧ�ܣ�");
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
                    SetMessageLn(string.Format("��Ӻ����������˺�#{0} {1}({2}) :", num, account.UserName, account.UserId));
                else
                    SetMessageLn(string.Format("��Ӻ���������˺�#{0} {1}({2}) :", num, account.UserName, account.UserId));
                if (this.SendFriendRequest(account.UserId))
                    SetMessage("�ɹ���");
                else
                    SetMessage("ʧ�ܣ�");
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
                //��ֹ��ѭ��
                if (page > 20)
                    return;

                HH.DelayedTime = Constants.DELAY_1SECONDS;
                string content = HH.Get("http://www.kaixin001.com/msg/sys.php?type=1&start=" + (page * 10).ToString());

                if (string.IsNullOrEmpty(content))
                {
                    SetMessageLn("û�мӺ���������Ϣ");
                    return;
                }
                if (content.IndexOf("��Ϣ����") > -1)
                {
                    SetMessageLn("û�мӺ���������Ϣ");
                    return;
                }
                string temp = JsonHelper.GetMid(content, "javascript:agreefriend(", ");\" class=\"sl\">ͬ��");
                if (string.IsNullOrEmpty(temp))
                {
                    SetMessageLn("û�мӺ���������Ϣ");
                    return;
                }

                page++;

                SetMessageLn("��" + page.ToString() + "ҳ");

                int num;
                for (string info = JsonHelper.GetMid(content, "javascript:agreefriend(", ");\" class=\"sl\">ͬ��", out num); info != null; info = JsonHelper.GetMid(content, "javascript:agreefriend(", ");\" class=\"sl\">ͬ��", out num))
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
                SetMessageLn(string.Format("��ͬ��{0}�ĺ�������!", this.GetFriendNameById(fuid)));
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
