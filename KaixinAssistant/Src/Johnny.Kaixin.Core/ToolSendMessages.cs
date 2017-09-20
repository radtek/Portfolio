using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.Core
{
    public class ToolSendMessages : KaixinBase
    {
        //SendMessage
        public delegate void SendMessageFinishedEventHandler();
        public event SendMessageFinishedEventHandler SendMessageFinished;

        public ToolSendMessages(AccountInfo account)
        {
            base.Caption = Constants.TOOL_SENDMESSAGE;
            base.Key = Constants.TOOL_SENDMESSAGE;
            base.CurrentAccount = account;
        }

        public void GetAllMyFriends()
        {
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();
            base.GetAllMyFriendsByThread();
        }

        public void SendMessage(AccountInfo account, Collection<FriendInfo> friends, string msg, bool bMulti)
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(delegate
            {
                TryCatchBlock th = new TryCatchBlock(delegate
                {
                    _module = Constants.MSG_SENDMESSAGE;

                    SetMessageLn("开始群发消息...");
                    if (!this.ValidationLogin(account, true))
                    {
                        if (SendMessageFinished != null)
                            SendMessageFinished();
                        return;
                    }

                    if (!bMulti)
                    {
                        foreach (FriendInfo friend in friends)
                        {
                            RequestMessageSending(friend.Id.ToString(), msg);
                            SetMessageLn(string.Format("已经给{0}({1})发送消息！", friend.Name, friend.Id));
                        }
                        SetMessageLn("全部消息发送完毕！");
                    }
                    else
                    {
                        StringBuilder sbuids = new StringBuilder();
                        StringBuilder sbnames = new StringBuilder();
                        string[,] sendList = new string[(friends.Count - 1) / 30 + 1, 2];
                        for (int ix = 0; ix < friends.Count; ix++)
                        {
                            sbuids.Append(friends[ix].Id.ToString());
                            if ((ix + 1) % 30 != 0 && ix != friends.Count - 1)
                                sbuids.Append(",");
                            sbnames.Append(friends[ix].Name);
                            if ((ix + 1) % 30 != 0 && ix != friends.Count - 1)
                                sbnames.Append(",");

                            if ((ix + 1) % 30 == 0)
                            {
                                sendList[ix / 30, 0] = sbuids.ToString();
                                sendList[ix / 30, 1] = sbnames.ToString();
                                sbuids = new StringBuilder();
                                sbnames = new StringBuilder();
                            }
                        }

                        if (friends.Count % 30 != 0)
                        {
                            sendList[sendList.GetLength(0) - 1, 0] = sbuids.ToString();
                            sendList[sendList.GetLength(0) - 1, 1] = sbnames.ToString();
                        }

                        for (int ix = 0; ix < sendList.GetLength(0); ix++)
                        {
                            RequestMessageSending(sendList[ix, 0], msg);
                            SetMessageLn("给" + sendList[ix, 1] + "群发消息成功！");
                        }
                    }

                    if (SendMessageFinished != null)
                        SendMessageFinished();
                    return;
                });
                base.ExecuteTryCatchBlock(th, "发生异常，群发消息失败！");
            }));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
    }
}
