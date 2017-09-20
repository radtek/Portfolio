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
                SetMessageLn("正在初始化[咬人]...");

                RequestBiteHomePage();
                //bitable
                string content = RequestBitableFriends();
                ReadBitableFriends(content, false);
                SetMessage("[我可以去咬的人]信息下载成功！");
                //restable
                content = RequestRestableFriends();
                ReadRestableFriends(content, false);
                SetMessage("[我能休息的房间]信息下载成功！");
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
                SetMessage(" 初始化[咬人]失败！错误：" + ex.Message);
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
                SetMessageLn("刷新[我可以去咬的人]...");

                if (!this.ValidationLogin(true))
                {
                    if (BitableFriendsFetched != null)
                        BitableFriendsFetched(_bitableFriendsList);
                    return;
                }

                string content = RequestBiteHomePage();
                content = RequestBitableFriends();
                ReadBitableFriends(content, true);
                SetMessageLn("[我可以去咬的人]信息刷新成功！");

                //invoke event
                if (BitableFriendsFetched != null)
                    BitableFriendsFetched(_bitableFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[我可以去咬的人]信息刷新失败！");
        }
        #endregion

        #region ReadBiteFriends
        public void ReadBitableFriends(string content, bool printMessage)
        {
            int num;
            this._bitableFriendsList.Clear();

            if (printMessage)
                SetMessageLn("读取[我可以去咬的人]信息:");
            for (string pos = JsonHelper.GetMid(content, "javascript:gotoUser(", ")", out num); pos != null; pos = JsonHelper.GetMid(content, "javascript:gotoUser(", ")", out num))
            {
                content = content.Substring(num);
                string status = JsonHelper.GetMid(content, "\"width:13em;\">(", ")");
                if ((status != null) && (status.IndexOf("今天咬过") == -1))
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
                SetMessageLn("完成读取！");
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
                SetMessageLn("刷新[我能休息的房间]...");

                if (!this.ValidationLogin(true))
                {
                    if (RestableFriendsFetched != null)
                        RestableFriendsFetched(_restableFriendsList);
                    return;
                }

                string content = RequestBiteHomePage();
                content = RequestRestableFriends();
                ReadRestableFriends(content, true);
                SetMessageLn("[我能休息的房间]信息刷新成功！");

                //invoke event
                if (RestableFriendsFetched != null)
                    RestableFriendsFetched(_restableFriendsList);

            });
            base.ExecuteTryCatchBlock(th, "[我能休息的房间]信息刷新失败！");
        }

        #endregion

        #region ReadRestableFriends
        public void ReadRestableFriends(string content, bool printMessage)
        {
            int num;
            this._restableFriendsList.Clear();

            if (printMessage)
                SetMessageLn("读取[我能休息的房间]信息:");
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
                        friend.Status = "在线";
                    else
                        friend.Status = "离线";
                    this._restableFriendsList.Add(friend);
                    if (printMessage)
                        SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")--" + friend.Status);
                }
            }
            if (printMessage)
                SetMessageLn("完成读取！");
        }
        #endregion

        #region RunBite
        public void RunBite()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_BITING;
                SetMessageLn("开始咬人...");
                RunBiteWithReturn();
                SetMessageLn("咬人完成！");
            });
            base.ExecuteTryCatchBlock(th, "发生异常，咬人失败！");
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
            SetMessageLn("正在检查我的状态...");
            BiteStatus status = this.GetBiteStatus(content, ref printmsg);
            SetMessage(printmsg);

            if (status == BiteStatus.IsRecovering)
            {
                if (Task.BiteOthers)
                    SetMessageLn("你的体力已几乎耗尽，需要休息。待体力恢复后，才能继续咬人。");
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

                    //先咬白名单中的人
                    SetMessageLn("开始咬白名单里的人：");
                    foreach (int uid in Operation.BiteWhiteList)
                    {
                        if (!(this.BiteTheFriend(uid) == BiteStatus.Healthy))
                            return;
                    }

                    //咬剩下的人
                    if (Operation.BiteAll)
                    {
                        SetMessageLn("还有体力，继续咬剩下的人：");
                        foreach (FriendInfo friend in this._bitableFriendsList)
                        {
                            if (Operation.BiteWhiteList.Contains(friend.Id))
                                continue;
                            if (!(this.BiteTheFriend(friend.Id) == BiteStatus.Healthy))
                                return;
                        }
                    }
                    //刷新状态
                    content = RequestBiteHomePage();
                    status = this.GetBiteStatus(content, ref printmsg);
                    SetMessageLn(string.Format("咬完所有人后: {0}", printmsg));
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
            SetMessageLn(string.Format("咬 {0}:", this.GetFriendNameById(id)));
            if (Operation.BiteBlackList.Contains(id))
            {
                SetMessage(" 在不被咬名单中，不咬 ");
                return BiteStatus.InBlackList;
            }

            string content = RequestBiteStyle(id.ToString());
            string param = string.Format("verify={0}&touid={1}&style={2}&position={3}&acc={4}", new object[] { this._verifyCode, id.ToString(), GetBiteStyle(content), GetBitePosition(content), this._biteAcc });

            for (int ix = 0; ix < 2; ix++)
            {
                HH.DelayedTime = Constants.DELAY_4SECONDS;
                content = HH.Post("http://www.kaixin001.com/bite/bite.php", param);
                string ret = this.GetBiteFeedBack(content);
                if (ret.StartsWith("你的体力已几乎耗尽，需要休息") || ret.Contains("你的体力已耗尽，需要休息"))
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
            //var g_be_rest_agree = parseInt("1");  有人在我家休息，而且我已经同意
            //var g_be_rest_agree = parseInt("0");  有人在我家休息，但我还没同意
            //var g_be_rest_agree = parseInt("");  没有人在我家休息

            SetMessageLn("正在检查是否有人在我家休息...");

            try
            {
                string strStatus = JsonHelper.GetMid(content, "var g_be_rest_agree = parseInt(\"", "\")");
                if (strStatus == null || strStatus == "")
                {
                    SetMessage("没有人在我家休息");
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
                    SetMessage(string.Format("{0}正在我家休息", username));
                    if ((Operation.AllowRestList.Count <= 0) || Operation.AllowRestList.Contains(userid))
                    {
                        param = "verify=" + this._verifyCode + "&touid=" + userid + "&agree=1";
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        HH.Post("http://www.kaixin001.com/bite/agree.php", param);
                        SetMessage(string.Format(" 允许{0}在我家休息", username));
                    }
                    else
                    {
                        param = "verify=" + this._verifyCode + "&touid=" + userid + "&agree=0";
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        HH.Post("http://www.kaixin001.com/bite/agree.php", param);
                        SetMessage(string.Format(" 拒绝{0}在我家休息", username));
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
                SetMessageLn("同意别人在我家休息失败！错误：" + ex.Message);
            }
        }

        #endregion

        #region StartRecover
        private bool StartRecover()
        {
            if (Task.AutoRecover)
            {
                SetMessageLn("正在找房间休息...");

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
                    SetMessageLn("未找到房间休息！");
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
                    SetMessage("找房间休息失败！错误：" + ex.Message);
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
                if ((content.StartsWith("你现在正在他家休息！") || content.StartsWith("你已经在")) || (content.IndexOf("开始恢复体力") != -1))
                {
                    SetMessageLn("你已经在" + GetFriendNameById(uid) + "的房间休息，开始恢复体力。");
                    //if (Task.SendRemindMessage)
                    //{
                    //    RequestMessageSending(uid.ToString(), Task.MessageContent);
                    //    SetMessageLn("已经给" + GetFriendNameById(uid) + "发送消息提醒对方！");
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
                //<li><span>体力：</span><em>16</em><br></li>
                //<li><span>状态：</span><i class="cb1" style="width:17em;">正常</i><br></li>
                //<li><span>状态：</span><i class="cb1" style="width:17em;">体力已耗尽 <a href="javascript:restoreHelp();" class="sl">恢复体力</a></i><br></li>
                //<li><span>状态：</span><i class="cb1" style="width:17em;">正在<a  class="sl2" style="color:#8690A5;" href="/~bite/index.php?touid=6194153">庄子</a>的房间休息(38秒，对方已同意)</i><br></li>
                string health = JsonHelper.GetMid(content, "体力：</span><em>", "</em>");
                result = JsonHelper.GetMid(content, "<i class=\"cb1\" style=\"width:17em;\">", "</i>");
                result = string.Format("{0} (体力{1})", JsonHelper.FiltrateHtmlTags(result), health);

                if (result.StartsWith("正在"))
                    return BiteStatus.IsRecovering;
                if (result.StartsWith("正常"))
                    return BiteStatus.Healthy;
                if (!result.StartsWith("正常"))
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
                    SetMessageLn("失败！");
                    return string.Empty;
                }

                JsonTextParser parser = new JsonTextParser();
                JsonObjectCollection objects = parser.Parse(content) as JsonObjectCollection;
                string ret = JsonHelper.FiltrateHtmlTags(JsonHelper.GetStringValue(objects["ret"]) + JsonHelper.GetStringValue(objects["prompt"]));
                if (ret.IndexOf("对方体力已经处于耗尽状态，你不能再咬了") > -1 || ret.IndexOf("根据你的级别，1天只能咬同1个人2次") > -1)
                    ret += " 失败！";
                else
                    ret += " 成功！";
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
                    SetMessageLn("保护" + GetFriendNameById(Operation.ProtectId) + "：");
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
                    SetMessageLn("保护好友失败！错误：" + ex.Message);
                }
            }
        }
        #endregion

        #region Request
        public string RequestBiteHomePage()
        {
            string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1048");
            if (content.IndexOf("<title>添加组件 - 开心网</title>") != -1)
            {
                SetMessageLn("还未安装咬人组件,尝试安装中...");
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
