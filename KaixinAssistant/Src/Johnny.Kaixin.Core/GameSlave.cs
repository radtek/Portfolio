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
                SetMessageLn("正在初始化[朋友买卖]...");

                string content = RequestSlaveHomePage();
                ReadSlaves(content, false);
                SetMessage("[我的奴隶]信息下载成功！");
                //buy slaves
                content = RequestBuyableSlaves();
                ReadBuyableSlaves(content, false);
                SetMessage("[我能买的奴隶]信息下载成功！");
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
                SetMessage(" 初始化[朋友买卖]失败！错误：" + ex.Message);
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
                SetMessageLn("刷新[我的奴隶]...");

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

                SetMessageLn("[我的奴隶]信息刷新成功！");

                //invoke event
                if (MySlaveFetched != null)
                    MySlaveFetched(_mySlaveList);
            });
            base.ExecuteTryCatchBlock(th, "[我的奴隶]信息刷新失败！");
        }
        #endregion

        #region ReadSlaves
        public void ReadSlaves(string content, bool printMessage)
        {
            int num;
            this._mySlaveList.Clear();

            if (printMessage)
                SetMessageLn("读取奴隶列表：");
            for (string info = JsonHelper.GetMid(content, "<div class=\"w265 l c6\">", "\t\t\t </div>", out num); info != null; info = JsonHelper.GetMid(content, "<div class=\"w265 l c6\">", "\t\t\t </div>", out num))
            {
                content = content.Substring(num);

                FriendInfo slave = new FriendInfo();
                slave.Name = JsonHelper.GetMid(info, "class=\"sl2\">", "<");
                slave.Id = JsonHelper.GetInteger(JsonHelper.GetMid(info, "/home/?uid=", "'"));
                slave.Gender = JsonHelper.GetMid(info, "\">我要释放", "<") == "他" ? true : false;
                slave.Price = JsonHelper.GetInteger(JsonHelper.GetMid(info, "身　价：<strong class=\"dgreen\">&yen;", "<"));
                if ((slave.Name != null))
                {
                    this._mySlaveList.Add(slave);
                    if (printMessage)
                        SetMessageLn(slave.Name + "(" + slave.Id.ToString() + ")--" + slave.Price);
                }
            }
            this._slaveNum = this._mySlaveList.Count;
            if (printMessage)
                SetMessageLn(string.Format("您有{0}个奴隶", new object[] { this._slaveNum }));
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
                SetMessageLn("刷新[我能买的奴隶]...");

                if (!this.ValidationLogin(true))
                {
                    if (BuyableSlavesFetched != null)
                        BuyableSlavesFetched(_buyableSlaveList);
                    return;
                }

                string content = RequestSlaveHomePage();
                content = RequestBuyableSlaves();
                ReadBuyableSlaves(content, true);

                SetMessageLn("[我能买的奴隶]信息刷新成功！");

                //invoke event
                if (BuyableSlavesFetched != null)
                    BuyableSlavesFetched(_buyableSlaveList);
            });
            base.ExecuteTryCatchBlock(th, "[我能买的奴隶]信息刷新失败！");
        }
        #endregion

        #region ReadBuyableSlaves
        public void ReadBuyableSlaves(string content, bool printMessage)
        {
            int num;
            this._buyableSlaveList.Clear();

            if (printMessage)
                SetMessageLn("读取[我能买的奴隶]信息:");
            for (string pos = JsonHelper.GetMid(content, "javascript:parent.gotouser(", ")", out num); pos != null; pos = JsonHelper.GetMid(content, "javascript:parent.gotouser(", ")", out num))
            {
                content = content.Substring(num);
                int id = JsonHelper.GetInteger(pos);
                if (id > 0)
                {
                    FriendInfo friend = new FriendInfo();
                    friend.Id = id;
                    friend.Name = JsonHelper.GetMid(content, "<strong>", "</strong></a>");
                    string price = JsonHelper.GetMid(content, "10em;\">(价格：￥", ")</div>");
                    if ((price != null) && price.Trim() != string.Empty)
                        friend.Price = JsonHelper.GetInteger(price);
                    this._buyableSlaveList.Add(friend);
                    if (printMessage)
                        SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")--价格：￥" + friend.Price);
                }
            }
            if (printMessage)
                SetMessageLn("完成读取！");
        }
        #endregion

        #region DisplayMySlaves
        private void DisplayMySlaves()
        {
            SetMessageLn("我的奴隶信息:");
            int num = 0;
            foreach (FriendInfo slave in this._mySlaveList)
            {
                SetMessageLn(string.Format("奴隶#{0}:{1}(身价:￥{2})", ++num, slave.Name, slave.Price));
            }
        }
        #endregion

        #region RunSlave
        public void RunSlave()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_SLAVE;

                SetMessageLn("开始朋友买卖...");

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

                SetMessageLn("朋友买卖完成！");

            });
            base.ExecuteTryCatchBlock(th, "发生异常，朋友买卖失败！");
        }
        #endregion

        #region BuySlaveFromBuyList
        private void BuySlaveFromBuyList()
        {
            try
            {
                SetMessageLn("购买名单中的奴隶：");
                if (this._slaveNum >= this.Task.MaxSlaves)
                {
                    SetMessageLn(string.Format(" 奴隶数:{0} 已到达购买上限({1})，不能购买！", new object[] { this._slaveNum, this.Task.MaxSlaves }));
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
                        SetMessageLn(string.Format("需要买的奴隶#{0}:{1}", ++num, this.GetFriendNameById(uid)));
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
                SetMessage(" 购买名单中的奴隶失败！错误：" + ex.Message);
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
                    SetMessageLn("购买低价奴隶：");
                    if (this._slaveNum >= this.Task.MaxSlaves)
                    {
                        SetMessageLn(string.Format(" 奴隶数:{0} 已到达购买上限({1})，不能购买！", new object[] { this._slaveNum, this.Task.MaxSlaves }));
                        return;
                    }

                    //get buyable slaves
                    ReadBuyableSlaves(RequestBuyableSlaves(), false);

                    if (_buyableSlaveList.Count <= 0)
                    {
                        SetMessageLn("你当前的现金不足，无法购买任何人为奴。");
                        return;
                    }

                    int num = 0;
                    foreach (FriendInfo slave in _buyableSlaveList)
                    {
                        if (this._slaveNum >= this.Task.MaxSlaves)
                            break;
                        if (IsAlreadyMySlave(slave.Id))
                            continue;
                        SetMessageLn(string.Format("可买的奴隶#{0}:{1}(身价:￥{2})", ++num, slave.Name, slave.Price.ToString()));
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
                SetMessage(" 购买低价奴隶失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region PropitiateSlaves
        private void PropitiateSlaves()
        {
            try
            {
                SetMessageLn("安抚奴隶：");
                int num = 0;
                foreach (FriendInfo slave in this._mySlaveList)
                {
                    SetMessageLn(string.Format("奴隶#{0}:{1} ", ++num, slave.Name));
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
                SetMessage(" 安抚奴隶失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region AfflictSlaves
        private void AfflictSlaves()
        {
            try
            {
                SetMessageLn("整奴隶：");
                int num = 0;
                string paintype = "";
                foreach (FriendInfo slave in this._mySlaveList)
                {
                    SetMessageLn(string.Format("奴隶#{0}:{1} ", new object[] { ++num, slave.Name }));
                    //6 去黑煤窑挖煤 
                    //8 去歌厅卖唱                 
                    //paintype = (slave.Gender) ? "6" : "8";
                    //18 去挑大粪
                    //17 去当小保姆
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
                SetMessage(" 整奴隶失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region ReleaseSlaves
        private void ReleaseSlaves()
        {
            try
            {
                SetMessageLn("释放奴隶：");
                int num = 0;
                bool flag = false;
                foreach (FriendInfo slave in this._mySlaveList)
                {
                    SetMessageLn(string.Format("奴隶#{0}:{1}(身价:￥{2}) ", ++num, slave.Name, slave.Price));
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    string content = HH.Get(string.Format("http://www.kaixin001.com/slave/free_dialog.php?slaveuid={0}&verify={1}", slave.Id.ToString(), this._verifyCode));
                    if (content.IndexOf("$(\"flag2\").style.display") != -1)
                    {
                        string param = "verify=" + this._verifyCode + "&slaveuid=" + slave.Id.ToString();
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        HH.Post("http://www.kaixin001.com/slave/free.php", param);
                        SetMessage("释放成功！");
                        flag = true;
                    }
                    else
                    {
                        SetMessage("未满两天，不能释放！");
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
                SetMessage(" 释放奴隶失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region FawnMaster
        private void FawnMaster()
        {
            try
            {
                SetMessageLn("讨好主人：");

                if (_hostuid == -1)
                {
                    SetMessageLn("目前我是自由身！");
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
                SetMessage(" 讨好主人失败！错误：" + ex.Message);
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
                SetMessage("#" + GetFriendNameById(slaveuid) + "，不购买黑名单里的奴隶 ");
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
            int slaveprice = JsonHelper.GetInteger(JsonHelper.GetMid(content, "你想花 <strong class=\"dgreen\">&yen;", "</strong> 的价格购买"));

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
                slave.Gender = JsonHelper.GetMid(content, "<br />同时，", "的") == "他" ? true : false;
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
            //6 给她穿漂亮的新衣服
            //7 给他穿漂亮的新衣服
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
            string fawnType = "1"; //给他请安            
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
                    //    <div class="tac f14"><strong>你今天已经安抚过奴隶<span class="sl">蒋大名</span>了</strong></div>
                    //    <div class="tac f14"><strong>还想再安抚一次？明天再来吧</strong></div>
                    //    <div style="padding:50px 166px;">
                    //    <div class="rbs1">
                    //        <input type="button" value="确定" class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';"  style="padding:4px 15px;" onclick="new parent.dialog().reset();" /></div>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, strdivid, "<input type=\"button\"");
                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));
                }
                else
                {
                    //<div class=\"f14 tac\" >\n\t\t\t<strong>奴隶也是人啊，做人要厚道！你已经整过他一次了</strong>\n\t\t</div>\n\t\t<div class=\"f14 tac\" ><strong>你真的对<span class=\"sl\">通用</span>恨之入骨，要再整他一次才爽？</strong></div>
                    //整完后，已经不在工作状态时
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
                    //    <div class="tac f14"><strong>你今天已经安抚过奴隶<span class="sl">蒋大名</span>了</strong></div>
                    //    <div class="tac f14"><strong>还想再安抚一次？明天再来吧</strong></div>
                    //    <div style="padding:50px 166px;">
                    //    <div class="rbs1">
                    //        <input type="button" value="确定" class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';"  style="padding:4px 15px;" onclick="new parent.dialog().reset();" /></div>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, strdivid, "<input type=\"button\"");
                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));
                }
                else
                {
                    //<div class=\"f14 tac\" >\n\t\t\t<strong>奴隶也是人啊，做人要厚道！你已经整过他一次了</strong>\n\t\t</div>\n\t\t<div class=\"f14 tac\" ><strong>你真的对<span class=\"sl\">通用</span>恨之入骨，要再整他一次才爽？</strong></div>
                    //整完后，已经不在工作状态时
                    strdivid = JsonHelper.GetFirstLast(content, "<strong>", "</strong>");
                    strmsg = JsonHelper.FiltrateHtmlTags(strdivid);
                }
                if (strmsg.IndexOf("奴隶也是人啊，做人要厚道") != -1 || strmsg.IndexOf("这段时间你就不要再整") != -1 || strmsg.IndexOf("抱歉") != -1)
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
                        //            <div class=""><strong>你想花 <strong class="dgreen">&yen;500</strong> 的价格购买 <span class="sl">忻农</span> 为奴？</strong></div>
                        //            <div class="mt5"><strong>可你只有现金 <strong class="dgreen">&yen;0</strong> ，我们不接受透支交易！</strong></div>
                        //        </div>
                        //    </div>
                        //    <div style="height:40px;border-top:1px solid #ccc;background:#F2F2F2;">
                        //    <div class="r" style="width:10px;">&nbsp;</div>
                        //        <div class="rbs1 mt5" style="float:right;">
                        //            <input type="button" value="取消" class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';"  style="padding:4px 10px;" onclick="new parent.dialog().reset();" /></div>
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
            if (content.IndexOf("<title>添加组件 - 开心网</title>") != -1)
            {
                SetMessageLn("还未安装朋友买卖组件,尝试安装中...");
                HH.Post("http://www.kaixin001.com/app/install.php", "aid=1028&isinstall=1");
                content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1028");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            this._slavecash = JsonHelper.GetInteger(JsonHelper.GetMid(content, "<p>现　金：<strong class=\"dgreen\">&yen;", "</strong></p>"));
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
