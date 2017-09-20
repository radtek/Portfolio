using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;
using System.Net.Json;

namespace Johnny.Kaixin.Core
{
    public class GameBite : KaixinBase
    {
        private string _biteAcc;

        private Collection<FriendInfo> _bitableFriendsList;
        private Collection<FriendInfo> _restableFriendsList;

        public delegate void BitableFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event BitableFriendsFetchedEventHandler BitableFriendsFetched;

        public delegate void RestableFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event RestableFriendsFetchedEventHandler RestableFriendsFetched;

        public GameBite()
        {
            _bitableFriendsList = new Collection<FriendInfo>();
            _restableFriendsList = new Collection<FriendInfo>();
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //biting
                SetMessageLn("���ڳ�ʼ��[ҧ��]...");

                RequestBiteHomePage();
                //bitable
                string content = RequestBitableFriends();
                ReadBitableFriends(content, false);
                SetMessage("[�ҿ���ȥҧ����]��Ϣ���سɹ���");
                //restable
                content = RequestRestableFriends();
                ReadRestableFriends(content, false);
                SetMessage("[������Ϣ�ķ���]��Ϣ���سɹ���");
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
                LogHelper.Write("GameBite.Initialize", ex, LogSeverity.Error);
                SetMessage(" ��ʼ��[ҧ��]ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetBitableFriends
        public void GetBitableFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetBitableFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetBitableFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_BITING;
                SetMessageLn("ˢ��[�ҿ���ȥҧ����]...");

                if (!this.ValidationLogin(true))
                {
                    if (BitableFriendsFetched != null)
                        BitableFriendsFetched(_bitableFriendsList);
                    return;
                }

                string content = RequestBiteHomePage();
                content = RequestBitableFriends();
                ReadBitableFriends(content, true);
                SetMessageLn("[�ҿ���ȥҧ����]��Ϣˢ�³ɹ���");

                //invoke event
                if (BitableFriendsFetched != null)
                    BitableFriendsFetched(_bitableFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[�ҿ���ȥҧ����]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region ReadBiteFriends
        public void ReadBitableFriends(string content, bool printMessage)
        {
            int num;
            this._bitableFriendsList.Clear();

            if (printMessage)
                SetMessageLn("��ȡ[�ҿ���ȥҧ����]��Ϣ:");
            for (string pos = JsonHelper.GetMid(content, "javascript:gotoUser(", ")", out num); pos != null; pos = JsonHelper.GetMid(content, "javascript:gotoUser(", ")", out num))
            {
                content = content.Substring(num);
                string status = JsonHelper.GetMid(content, "\"width:13em;\">(", ")");
                if ((status != null) && (status.IndexOf("����ҧ��") == -1))
                {
                    int uid = JsonHelper.GetInteger(pos);
                    if (uid > 0)
                    {
                        string name = JsonHelper.GetMid(content, "\">", "</a>");
                        FriendInfo friend = new FriendInfo();
                        friend.Id = uid;
                        friend.Name = name;
                        friend.Status = status;
                        this._bitableFriendsList.Add(friend);
                        if (printMessage)
                            SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")--" + friend.Status);
                    }
                }
            }
            if (printMessage)
                SetMessageLn("��ɶ�ȡ��");
        }
        #endregion

        #region GetRestableFriends
        public void GetRestableFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetRestableFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void GetRestableFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_BITING;
                SetMessageLn("ˢ��[������Ϣ�ķ���]...");

                if (!this.ValidationLogin(true))
                {
                    if (RestableFriendsFetched != null)
                        RestableFriendsFetched(_restableFriendsList);
                    return;
                }

                string content = RequestBiteHomePage();
                content = RequestRestableFriends();
                ReadRestableFriends(content, true);
                SetMessageLn("[������Ϣ�ķ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (RestableFriendsFetched != null)
                    RestableFriendsFetched(_restableFriendsList);

            });
            base.ExecuteTryCatchBlock(th, "[������Ϣ�ķ���]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region ReadRestableFriends
        public void ReadRestableFriends(string content, bool printMessage)
        {
            int num;
            this._restableFriendsList.Clear();

            if (printMessage)
                SetMessageLn("��ȡ[������Ϣ�ķ���]��Ϣ:");
            for (string pos = JsonHelper.GetMid(content, "javascript:gotoUser(", ")", out num); pos != null; pos = JsonHelper.GetMid(content, "javascript:gotoUser(", ")", out num))
            {
                content = content.Substring(num);
                int uid = JsonHelper.GetInteger(pos);
                if (uid > 0)
                {
                    string name = JsonHelper.GetMid(content, "\">", "</a>");
                    FriendInfo friend = new FriendInfo();
                    friend.Id = uid;
                    friend.Name = name;
                    string status = JsonHelper.GetMid(content, "\"width:1em;\">", "</div>");
                    if ((status != null) && status.Trim() != string.Empty)
                        friend.Status = "����";
                    else
                        friend.Status = "����";
                    this._restableFriendsList.Add(friend);
                    if (printMessage)
                        SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")--" + friend.Status);
                }
            }
            if (printMessage)
                SetMessageLn("��ɶ�ȡ��");
        }
        #endregion

        #region RunBite
        public void RunBite()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_BITING;
                SetMessageLn("��ʼҧ��...");
                RunBiteWithReturn();
                SetMessageLn("ҧ����ɣ�");
            });
            base.ExecuteTryCatchBlock(th, "�����쳣��ҧ��ʧ�ܣ�");
        }

        private void RunBiteWithReturn()
        {
            string content = RequestBiteHomePage();

            //Approve Recover
            if (Task.ApproveRecovery)
            {
                this.ApproveRecover(content);
            }

            if (!Task.BiteOthers && !Task.AutoRecover && !Task.ProtectFriend)
                return;

            string printmsg = "";
            SetMessageLn("���ڼ���ҵ�״̬...");
            BiteStatus status = this.GetBiteStatus(content, ref printmsg);
            SetMessage(printmsg);

            if (status == BiteStatus.IsRecovering)
            {
                if (Task.BiteOthers)
                    SetMessageLn("��������Ѽ����ľ�����Ҫ��Ϣ���������ָ��󣬲��ܼ���ҧ�ˡ�");
                return;
            }

            //Get bite Acc code
            RequestBiteAcc();
            
            if (status == BiteStatus.NeedRecovery)
            {
                this.StartRecover();
            }
            else
            {
                if (Task.BiteOthers)
                {
                    //Get bitable friends                        
                    content = RequestBitableFriends();
                    ReadBitableFriends(content, false);

                    //��ҧ�������е���
                    SetMessageLn("��ʼҧ����������ˣ�");
                    foreach (int uid in Operation.BiteWhiteList)
                    {
                        if (!(this.BiteTheFriend(uid) == BiteStatus.Healthy))
                            return;
                    }

                    //ҧʣ�µ���
                    if (Operation.BiteAll)
                    {
                        SetMessageLn("��������������ҧʣ�µ��ˣ�");
                        foreach (FriendInfo friend in this._bitableFriendsList)
                        {
                            if (Operation.BiteWhiteList.Contains(friend.Id))
                                continue;
                            if (!(this.BiteTheFriend(friend.Id) == BiteStatus.Healthy))
                                return;
                        }
                    }
                    //ˢ��״̬
                    content = RequestBiteHomePage();
                    status = this.GetBiteStatus(content, ref printmsg);
                    SetMessageLn(string.Format("ҧ�������˺�: {0}", printmsg));
                }
            }

            if (status == BiteStatus.Healthy && Task.ProtectFriend == true)
            {
                ProtectFriend();
            }
        }
        #endregion

        #region BiteTheFriend
        private BiteStatus BiteTheFriend(int id)
        {
            if (id <= 0)
            {
                return BiteStatus.Unknown;
            }
            SetMessageLn(string.Format("ҧ {0}:", this.GetFriendNameById(id)));
            if (Operation.BiteBlackList.Contains(id))
            {
                SetMessage(" �ڲ���ҧ�����У���ҧ ");
                return BiteStatus.InBlackList;
            }

            string content = RequestBiteStyle(id.ToString());
            string param = string.Format("verify={0}&touid={1}&style={2}&position={3}&acc={4}", new object[] { this._verifyCode, id.ToString(), GetBiteStyle(content), GetBitePosition(content), this._biteAcc });

            for (int ix = 0; ix < 2; ix++)
            {
                HH.DelayedTime = Constants.DELAY_4SECONDS;
                content = HH.Post("http://www.kaixin001.com/bite/bite.php", param);
                string ret = this.GetBiteFeedBack(content);
                if (ret.StartsWith("��������Ѽ����ľ�����Ҫ��Ϣ") || ret.Contains("��������Ѻľ�����Ҫ��Ϣ"))
                {
                    if (!this.StartRecover())
                        return BiteStatus.NoRoom;
                    else
                        return BiteStatus.IsRecovering;
                }
            }
            return BiteStatus.Healthy;
        }
        #endregion

        #region GetBiteStyle
        private string GetBiteStyle(string content)
        {
            string biteStyle = "a";
            int num = 0;
            try
            {
                for (string pos = JsonHelper.GetMid(content, "<input type=radio name=style value=\"", "\"", out num); pos != null; pos = JsonHelper.GetMid(content, "<input type=radio name=style value=\"", "\"", out num))
                {
                    content = content.Substring(num);
                    if (pos != biteStyle)
                        biteStyle = pos;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Write("GameBite.GetBiteStyle", content, ex, LogSeverity.Error);
            }
            return biteStyle;
        }
        #endregion

        #region GetBitePosition
        private string GetBitePosition(string content)
        {
            string bitePosition = "a";
            int num = 0;
            try
            {
                for (string pos = JsonHelper.GetMid(content, "<input type=radio name=position value=\"", "\"", out num); pos != null; pos = JsonHelper.GetMid(content, "<input type=radio name=position value=\"", "\"", out num))
                {
                    content = content.Substring(num);
                    if (pos != bitePosition)
                        bitePosition = pos;
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
                LogHelper.Write("GameBite.GetBitePosition", content, ex, LogSeverity.Error);
            }
            return bitePosition;
        }
        #endregion

        #region ApproveRecover
        private void ApproveRecover(string content)
        {
            //var g_be_rest_agree = parseInt("1");  �������Ҽ���Ϣ���������Ѿ�ͬ��
            //var g_be_rest_agree = parseInt("0");  �������Ҽ���Ϣ�����һ�ûͬ��
            //var g_be_rest_agree = parseInt("");  û�������Ҽ���Ϣ

            SetMessageLn("���ڼ���Ƿ��������Ҽ���Ϣ...");

            try
            {
                string strStatus = JsonHelper.GetMid(content, "var g_be_rest_agree = parseInt(\"", "\")");
                if (strStatus == null || strStatus == "")
                {
                    SetMessage("û�������Ҽ���Ϣ");
                }
                else if (strStatus == "1")
                {
                    string message = JsonHelper.GetMid(content, "<div class=\"c hf_yr\" id=rest2div", "<div class=\"c hfb\">");
                    message = JsonHelper.GetMid(message, "<ul class=\"l\">", "</li>");
                    SetMessageLn(JsonHelper.FiltrateHtmlTags(message));
                }
                else
                {
                    string user = JsonHelper.GetMid(content, "javascript:agree(", ")");
                    int userid = JsonHelper.GetInteger(user);

                    string param;
                    string username = JsonHelper.GetMid(content, "\"sl\"><strong>", "</strong>");
                    SetMessage(string.Format("{0}�����Ҽ���Ϣ", username));
                    if ((Operation.AllowRestList.Count <= 0) || Operation.AllowRestList.Contains(userid))
                    {
                        param = "verify=" + this._verifyCode + "&touid=" + userid + "&agree=1";
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        HH.Post("http://www.kaixin001.com/bite/agree.php", param);
                        SetMessage(string.Format(" ����{0}���Ҽ���Ϣ", username));
                    }
                    else
                    {
                        param = "verify=" + this._verifyCode + "&touid=" + userid + "&agree=0";
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        HH.Post("http://www.kaixin001.com/bite/agree.php", param);
                        SetMessage(string.Format(" �ܾ�{0}���Ҽ���Ϣ", username));
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
                LogHelper.Write("GameBite.ApproveRecover", content, ex, LogSeverity.Error);
                SetMessageLn("ͬ��������Ҽ���Ϣʧ�ܣ�����" + ex.Message);
            }
        }

        #endregion

        #region StartRecover
        private bool StartRecover()
        {
            if (Task.AutoRecover)
            {
                SetMessageLn("�����ҷ�����Ϣ...");

                try
                {
                    //Get restable friends
                    string content = RequestRestableFriends();
                    ReadRestableFriends(content, false);

                    foreach (int uid in Operation.RecoverWhiteList)
                    {
                        if (RecoverInParticularRoom(uid))
                            return true;
                        else
                            continue;
                    }
                    foreach (FriendInfo friend in this._restableFriendsList)
                    {
                        if (RecoverInParticularRoom(friend.Id))
                            return true;
                        else
                            continue;
                    }
                    SetMessageLn("δ�ҵ�������Ϣ��");
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
                    LogHelper.Write("GameBite.StartRecover", ex, LogSeverity.Error);
                    SetMessage("�ҷ�����Ϣʧ�ܣ�����" + ex.Message);
                }
            }
            return false;
        }
        #endregion

        #region RecoverInParticularRoom
        private bool RecoverInParticularRoom(int uid)
        {
            if (!Operation.RecoverBlackList.Contains(uid))
            {
                string param = string.Format("verify={0}&touid={1}&acc={2}", this._verifyCode, uid, _biteAcc);
                HH.DelayedTime = Constants.DELAY_4SECONDS;
                string content = HH.Post("http://www.kaixin001.com/bite/rest.php", param);
                content = this.GetBiteFeedBack(content);
                if ((content.StartsWith("����������������Ϣ��") || content.StartsWith("���Ѿ���")) || (content.IndexOf("��ʼ�ָ�����") != -1))
                {
                    SetMessageLn("���Ѿ���" + GetFriendNameById(uid) + "�ķ�����Ϣ����ʼ�ָ�������");
                    //if (Task.SendRemindMessage)
                    //{
                    //    RequestMessageSending(uid.ToString(), Task.MessageContent);
                    //    SetMessageLn("�Ѿ���" + GetFriendNameById(uid) + "������Ϣ���ѶԷ���");
                    //}
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region GetBiteStatus
        private BiteStatus GetBiteStatus(string content, ref string result)
        {
            try
            {
                //<li><span>������</span><em>16</em><br></li>
                //<li><span>״̬��</span><i class="cb1" style="width:17em;">����</i><br></li>
                //<li><span>״̬��</span><i class="cb1" style="width:17em;">�����Ѻľ� <a href="javascript:restoreHelp();" class="sl">�ָ�����</a></i><br></li>
                //<li><span>״̬��</span><i class="cb1" style="width:17em;">����<a  class="sl2" style="color:#8690A5;" href="/~bite/index.php?touid=6194153">ׯ��</a>�ķ�����Ϣ(38�룬�Է���ͬ��)</i><br></li>
                string health = JsonHelper.GetMid(content, "������</span><em>", "</em>");
                result = JsonHelper.GetMid(content, "<i class=\"cb1\" style=\"width:17em;\">", "</i>");
                result = string.Format("{0} (����{1})", JsonHelper.FiltrateHtmlTags(result), health);

                if (result.StartsWith("����"))
                    return BiteStatus.IsRecovering;
                if (result.StartsWith("����"))
                    return BiteStatus.Healthy;
                if (!result.StartsWith("����"))
                    return BiteStatus.NeedRecovery;
                return BiteStatus.Unknown;
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
                LogHelper.Write("GameBite.GetBiteStatus", content, ex, LogSeverity.Error);
                return BiteStatus.Unknown;
            }
        }
        #endregion

        #region GetBiteFeedBack
        private string GetBiteFeedBack(string content)
        {
            try
            {
                if (content.Length < 4)
                {
                    SetMessageLn("ʧ�ܣ�");
                    return string.Empty;
                }

                JsonTextParser parser = new JsonTextParser();
                JsonObjectCollection objects = parser.Parse(content) as JsonObjectCollection;
                string ret = JsonHelper.FiltrateHtmlTags(JsonHelper.GetStringValue(objects["ret"]) + JsonHelper.GetStringValue(objects["prompt"]));
                if (ret.IndexOf("�Է������Ѿ����ںľ�״̬���㲻����ҧ��") > -1 || ret.IndexOf("������ļ���1��ֻ��ҧͬ1����2��") > -1)
                    ret += " ʧ�ܣ�";
                else
                    ret += " �ɹ���";
                SetMessage(ret);
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
                LogHelper.Write("GameBite.GetBiteFeedBack", content, ex, LogSeverity.Error);
                return "";
            }
        }
        #endregion

        #region ProtectFriend
        private void ProtectFriend()
        {
            if (Operation.ProtectId != 0)
            {
                try
                {
                    SetMessageLn("����" + GetFriendNameById(Operation.ProtectId) + "��");
                    string param = string.Format("verify={0}&touid={1}&_=", this._verifyCode, Operation.ProtectId);
                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                    string content = HH.Post("http://www.kaixin001.com/bite/shield.php", param);
                    //{"ret":"","prompt":"<div style='margin:20px 0 60px 60px;'><strong class='f14'>\u4f60\u5df2\u7ecf\u6210\u4e3a\u6731\u536b\u9752\u7684\u4fdd\u62a4\u5929\u4f7f\uff01<br>\u5982\u679c\u522b\u4eba\u6765\u54ac\u6731\u536b\u9752\uff0c\u4f60\u5c06\u66ff\u5979\u62b5\u6321\u4e00\u53e3\u3002<\/strong><\/div>"}
                    JsonTextParser parser = new JsonTextParser();
                    JsonObjectCollection objects = parser.Parse(content) as JsonObjectCollection;
                    string ret = JsonHelper.FiltrateHtmlTags(JsonHelper.GetStringValue(objects["ret"]) + JsonHelper.GetStringValue(objects["prompt"]));
                    SetMessage(ret);
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
                    LogHelper.Write("GameBite.ProtectFriend", ex, LogSeverity.Error);
                    SetMessageLn("��������ʧ�ܣ�����" + ex.Message);
                }
            }
        }
        #endregion

        #region Request
        public string RequestBiteHomePage()
        {
            string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1048");
            if (content.IndexOf("<title>������ - ������</title>") != -1)
            {
                SetMessageLn("��δ��װҧ�����,���԰�װ��...");
                HH.Post("http://www.kaixin001.com/app/install.php", "aid=1048&isinstall=1");
                content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1048");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            return content;
        }
        private void RequestBiteAcc()
        {
            string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1048&url=index.php&touid=6747992");
            this._biteAcc = GetAccCode(content);
        }

        private string RequestBiteStyle(string touid)
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get(string.Format("http://www.kaixin001.com/bite/style.php?verify={0}&touid={1}", this._verifyCode, touid));
        }

        public string RequestBitableFriends()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/bite/bitable.php?verify=" + this._verifyCode);
        }

        public string RequestRestableFriends()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/bite/restable.php?verify=" + this._verifyCode);
        }

        #endregion

        #region Properties
        public Collection<FriendInfo> BitableFriendsList
        {
            get { return _bitableFriendsList; }
        }

        public Collection<FriendInfo> RestableFriendsList
        {
            get { return _restableFriendsList; }
        }
        #endregion
    }
}
