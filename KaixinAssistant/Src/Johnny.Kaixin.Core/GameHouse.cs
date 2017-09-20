using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;

using System.Net.Json;
using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class GameHouse : KaixinBase
    {        
        private Collection<FriendInfo> _sameVillageFriendsList;
        private Collection<FriendInfo> _allHouseFriendsList;
        private Collection<FriendInfo> _freeFriendsList;
        private StayStatus _staystatus;
        private string _otherhousemaster;
        private Collection<int> _houseFullList;
        private Collection<int> _robbedFriendsList;

        //多套房子
        private Collection<HouseInfo> _myHouses;

        public delegate void SameVillageFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event SameVillageFriendsFetchedEventHandler SameVillageFriendsFetched;

        public delegate void AllHouseFriendsFetchedEventHandler(Collection<FriendInfo> allhousefriends);
        public event AllHouseFriendsFetchedEventHandler AllHouseFriendsFetched;

        public delegate void FreeFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event FreeFriendsFetchedEventHandler FreeFriendsFetched;

        public GameHouse()
        {
            this._sameVillageFriendsList = new Collection<FriendInfo>();
            this._allHouseFriendsList = new Collection<FriendInfo>();
            this._freeFriendsList = new Collection<FriendInfo>();
            this._staystatus = StayStatus.UnKnown;
            this._myHouses = new Collection<HouseInfo>();
            this._robbedFriendsList = new Collection<int>();
        }        

        #region Initialize
        public void Initialize()
        {
            try
            {
                //house
                SetMessageLn("正在初始化[买房子]...");

                string content = RequestHouseHomePage(true);
                //all house friends
                string content2 = RequestAllHouseFriends();
                ReadAllHouseFriends(content2, false);
                SetMessage("[所有买房子的好友]信息下载成功！");
                //same village friends
                ReadSameVillageFriends(content, false);
                SetMessage("[住在同小区里的好友]信息下载成功！");
                //free friends
                content = RequestFreeFriends();
                ReadFreeFriends(content, false);
                SetMessage("[露宿街头的好友]信息下载成功！");
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
                LogHelper.Write("GameHouse.Initialize", ex, LogSeverity.Error);
                SetMessage(" 初始化[买房子]失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetSameVillageFriends
        public void GetSameVillageFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetSameVillageFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetSameVillageFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_HOUSE;
                SetMessageLn("刷新[住在同小区里的好友]...");

                if (!this.ValidationLogin(true))
                {
                    if (SameVillageFriendsFetched != null)
                        SameVillageFriendsFetched(_sameVillageFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);

                string content2 = RequestAllHouseFriends();
                ReadAllHouseFriends(content2, false);

                ReadSameVillageFriends(content, true);
                SetMessageLn("[住在同小区里的好友]信息刷新成功！");

                //invoke event
                if (SameVillageFriendsFetched != null)
                    SameVillageFriendsFetched(_sameVillageFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[住在同小区里的好友]信息刷新失败！");
        }

        #endregion

        #region ReadSameVillageFriends
        public void ReadSameVillageFriends(string content, bool printMessage)
        {
            int num;
            this._sameVillageFriendsList.Clear();

            if (printMessage)
                SetMessageLn("读取[住在同小区里的好友]信息:");
            content = JsonHelper.GetMid(content, "<div class=\"mt30 ml10\"><b class=\"f13\">住在本小区里的好友：</b>", "</div>");
            for (string pos = JsonHelper.GetMid(content, "<a href='/~house/index.php?", "</a>", out num); pos != null; pos = JsonHelper.GetMid(content, "<a href='/~house/index.php?", "</a>", out num))
            {
                FriendInfo friend = new FriendInfo();
                friend.Id = JsonHelper.GetMidInteger(content, "_lgmode=pri&fuid=", "' class='sl'>");
                friend.Name = JsonHelper.GetMid(content, "' class='sl'>", "</a>");
                friend.Gender = GetGenderById(friend.Id);
                this._sameVillageFriendsList.Add(friend);
                if (printMessage)
                    SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
                content = content.Substring(num);
            }
            if (printMessage)
                SetMessageLn("完成读取！");
        }
        #endregion

        #region GetAllHouseFriends
        public void GetAllHouseFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAllHouseFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void GetAllHouseFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_HOUSE;
                SetMessageLn("刷新[所有买房子的好友]...");

                if (!this.ValidationLogin(true))
                {
                    if (AllHouseFriendsFetched != null)
                        AllHouseFriendsFetched(_allHouseFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestAllHouseFriends();
                ReadAllHouseFriends(content, true);
                SetMessageLn("[所有买房子的好友]信息刷新成功！");

                //invoke event
                if (AllHouseFriendsFetched != null)
                    AllHouseFriendsFetched(_allHouseFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[所有买房子的好友]信息刷新失败！");
        }            

        #endregion

        #region ReadAllHouseFriends
        public void ReadAllHouseFriends(string content, bool printMessage)
        {
            int num;
            this._allHouseFriendsList.Clear();

            if (printMessage)
                SetMessageLn("读取[所有买房子的好友]信息:");
            //<div class="l" style="width:8em;"><a href="javascript:gotoUser(6194153);" class="sl">庄子</a></div>
            string content2 = content;
            for (string pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num); pos != null; pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num))
            {
                FriendInfo friend = new FriendInfo();
                friend.Id = JsonHelper.GetMidInteger(content, "<a href=\"javascript:gotoUser(", ");\"");
                friend.Name = JsonHelper.GetMid(content, "class=\"sl\">", "</a>");
                this._allHouseFriendsList.Add(friend);
                if (printMessage)
                    SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
                content = content.Substring(num);
            }
            
            int ix = 0;
            for (string pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num); pos != null; pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num))
            {
                if (pos.IndexOf("value=\"1\"") > -1)
                {
                    this._allHouseFriendsList[ix].Gender = false;
                }
                else if (pos.IndexOf("value=\"0\"") > -1)
                {
                    this._allHouseFriendsList[ix].Gender = true;
                }
                else
                {
                    break;
                }

                if (printMessage)
                    SetMessageLn(this._allHouseFriendsList[ix].Name + "(" + this._allHouseFriendsList[ix].Id.ToString() + ")--" + (this._allHouseFriendsList[ix].Gender ? "男" : "女"));
                ix++;
                content2 = content2.Substring(num);
            }
            if (printMessage)
                SetMessageLn("完成读取！");
        }
        #endregion

        #region GetFreeFriends
        public void GetFreeFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetFreeFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetFreeFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_HOUSE;
                SetMessageLn("刷新[露宿街头的好友]...");

                if (!this.ValidationLogin(true))
                {
                    if (FreeFriendsFetched != null)
                        FreeFriendsFetched(_freeFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestFreeFriends();
                ReadFreeFriends(content, true);
                SetMessageLn("[露宿街头的好友]信息刷新成功！");

                //invoke event
                if (FreeFriendsFetched != null)
                    FreeFriendsFetched(_freeFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[露宿街头的好友]信息刷新失败！");
        }

        #endregion

        #region ReadFreeFriends
        public void ReadFreeFriends(string content, bool printMessage)
        {
            int num;
            this._freeFriendsList.Clear();

            if (printMessage)
                SetMessageLn("读取[露宿街头的好友]信息:");
            //<input type=hidden id=gender_4 value="0">
            //<div id=udiv_4 style="border-bottom:1px dashed #ccc;padding:3px 5px;">
            //    <div class="l" style="width:8em;"><a href="javascript:gotoUser(10753474);" class="sl">武大郎</a></div>
            //    <div class="l c9" style="width:13em;"></div>
            //    <div class="c"></div>
            //</div>
            //<input type=hidden id=gender_5 value="1">
            //<div id=udiv_5 style="border-bottom:1px dashed #ccc;padding:3px 5px;">
            //    <div class="l" style="width:8em;"><a href="javascript:gotoUser(10753642);" class="sl">王思懿</a></div>
            //    <div class="l c9" style="width:13em;"></div>
            //    <div class="c"></div>
            //</div>
            string content2 = content;
            for (string pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num); pos != null; pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num))
            {
                FriendInfo friend = new FriendInfo();
                friend.Id = JsonHelper.GetMidInteger(content, "<a href=\"javascript:gotoUser(", ");\"");
                friend.Name = JsonHelper.GetMid(content, "class=\"sl\">", "</a>");
                if (friend.Id != 0 && friend.Name != string.Empty)
                {
                    this._freeFriendsList.Add(friend);                    
                }
                content = content.Substring(num);
            }
            int ix = 0;
            for (string pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num); pos != null; pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num))
            {
                if (pos.IndexOf("value=\"1\"") > -1)
                {
                    this._freeFriendsList[ix].Gender = false;
                }
                else if (pos.IndexOf("value=\"0\"") > -1)
                {
                    this._freeFriendsList[ix].Gender = true;
                }
                else
                {
                    break;
                }
                if (printMessage)
                    SetMessageLn(this._freeFriendsList[ix].Name + "(" + this._freeFriendsList[ix].Id.ToString() + ")--" + (this._freeFriendsList[ix].Gender ? "男" : "女"));
                ix++;
                content2 = content2.Substring(num);
            }
            if (printMessage)
                SetMessageLn("完成读取！");
        }
        #endregion

        #region RunHouse
        public void RunHouse()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_HOUSE;

                SetMessageLn("开始买房子...");
                                
                //house
                string contentHome = RequestHouseHomePage(false);

                //same village friends
                ReadSameVillageFriends(contentHome, false);
                //all house friends
                string content = RequestAllHouseFriends();
                ReadAllHouseFriends(content, false);
                //free friends
                content = RequestFreeFriends();
                ReadFreeFriends(content, false);

                if (Task.DoJob)
                    DoJob();
                if (Task.StayHouse)
                    StayHouse(contentHome);
                if (Task.RobFriends || Task.RobFreeFriends)
                {
                    _robbedFriendsList.Clear();
                    SetMessageLn("抢人：");
                    SetMessageLn("#1");
                    RobFriends(this._myHouses[0], true);
                    for (int ix = 1; ix < this._myHouses.Count; ix++)
                    {
                        SetMessageLn("#" + (ix + 1).ToString());
                        RobFriends(this._myHouses[ix], false);
                    }
                }

                SetMessageLn("买房子完成！");
            });
            base.ExecuteTryCatchBlock(th, "发生异常，买房子失败！");
        }
        #endregion

        #region GetPreviouHouse
        private HouseInfo GetPreviouHouse(string content)
        {
            HouseInfo house = new HouseInfo();
            if (String.IsNullOrEmpty(content))
                return house;
            //<a href="/!house/index_room.php?roomid=9719" class="sl noline" style="margin-left:0;">进入我的房间</a></li>
            //<a href="/!house/index_room.php?fuid=6195212&roomid=15553" class="sl noline" style="margin-left:0;"> 进入他的房间</a></li>
            string houseinfo = JsonHelper.GetMid(content, "<a href=\"/!house/index_room.php?", " class=\"sl noline\" style=\"margin-left:0;\">");
            if (houseinfo != null)
            {
                house.RoomId = JsonHelper.GetMid(houseinfo, "roomid=", "\"");
                house.Fuid = CurrentAccount.UserId;
            }
            return house;
        }
        #endregion

        #region ReadHouse
        private HouseInfo ReadHouse(string content, HouseInfo house, bool printMessage)
        {
            //housename
            string strName = JsonHelper.GetMid(content, "<div class=\"f14 tac\" style=\"margin:0;*margin:10px 0 0 0;padding:10px 0 0 0;*padding:0;\">", "</div>");
            if (!String.IsNullOrEmpty(strName))
                house.HouseName = strName;
            else
                house.HouseName = "";
    
            if (printMessage)
                SetMessage(house.HouseName + "：");

            if (JsonHelper.GetMid(content, "<span  class=\"c_sl \">", "</span>").IndexOf("自己家") > -1)
                house.IsMasterInHouse = true;
            else
                house.IsMasterInHouse = false;

            int num = 0;            
            int lodgercount = 0;
            int roomcount = 0;
            for (int ix = 0; ix < 6; ix++)
            {
                string client = JsonHelper.GetMid(content, "<div style=\"margin-top:-10px;\" class=\"tac\">", "<div class='butt_box", out num);
                if (client != null)
                {
                    lodgercount++;
                    roomcount++;
                    if (printMessage)
                        SetMessage(string.Format(" {0}:{1}", lodgercount, JsonHelper.FiltrateHtmlTags(client)));
                }
                else
                {
                    string empty = JsonHelper.GetMid(content, "<img src=\"http://img.kaixin001.com.cn/i2/house/icon_w.jpg\"", "<div class=\"butt_box1\"><a href=\"javascript:stayhouse(", out num);
                    if (empty != null)
                        roomcount++;
                    else
                        break;
                }

                content = content.Substring(num);
            }

            house.LodgerCount = lodgercount;
            house.RoomCount = roomcount;

            if (house.LodgerCount == 0 && printMessage)
                SetMessage("没人居住！");

            return house;
        }
        #endregion

        #region DoJob
        private void DoJob()
        {
            try
            {
                SetMessageLn("打工：");

                string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1062&url=jobslist.php");
                //<form action="" method="get" >
                //<div style="width:52em;margin:50px auto;">
                //    <div class="l"><img align="absmiddle" src="http://img.kaixin001.com.cn/i2/house/lady.gif" alt="工作小姐" /></div>
                //    <div class="f14 l " style="margin:20px 20px;width:26em;">你正在做餐厅洗碗工，今天不能再干其它工作了！
                //        <br />要注意身体，别累坏了。
                //    </div>
                //    <div class="l"></div>
                //</div>
                //</form>
                string feedback = JsonHelper.GetMid(content, "<div class=\"f14 l \" style=\"margin:20px 20px;width:26em;\">", "</div>");
                if (feedback != null)
                {
                    SetMessage(" " + JsonHelper.FiltrateHtmlTags(feedback));
                    return;
                }

                if (content.IndexOf("目前你可以做的工作有") > -1)
                {
                    string jobType = GetJobType(content);
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    content = HH.Get(string.Format("http://www.kaixin001.com/house/dojob.php?verify={0}&petid={1}", this._verifyCode, jobType));
                    //<script language="JavaScript"  type="text/javascript">document.domain="kaixin001.com";
                    //new parent.dialog().reset();parent.dojob_ret(49,0);
                    //</script>
                    string jobstatus = JsonHelper.GetMid(content, "parent.dojob_ret(", ");").Split(',')[1];
                    content = HH.Get(string.Format("http://www.kaixin001.com/house/dojobret_dialog.php?verify={0}&petid={1}&flag={2}", this._verifyCode, jobType, jobstatus));
                    feedback = JsonHelper.GetMid(content, "<div id=\"flag" + jobstatus + "\" style=\"display:none;\">", "</strong></div>");
                    SetMessage(" " + JsonHelper.FiltrateHtmlTags(feedback));
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
                LogHelper.Write("GameHouse.DoJob", CurrentAccount.UserName, ex, LogSeverity.Error);
                SetMessage(" 打工失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetJobType
        private string GetJobType(string content)
        {
            string jobType = "9"; //做餐厅洗碗工
            int num = 0;
            try
            {
                for (string pos = JsonHelper.GetMid(content, "<div class=\"l\"><input name=\"petid\" type=\"radio\" value=\"", "\" />", out num); pos != null; pos = JsonHelper.GetMid(content, "<div class=\"l\"><input name=\"petid\" type=\"radio\" value=\"", "\" />", out num))
                {
                    content = content.Substring(num);
                    jobType = pos;
                }
                if (jobType != string.Empty)
                    return jobType;
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
                LogHelper.Write("GameHouse.GetJobType", content, ex, LogSeverity.Error);
            }
            return "9";
        }
        #endregion

        #region StayHouse
        private void StayHouse(string content)
        {
            try
            {
                SetMessageLn("住进房子：");

                /*------------------------------------住自己家------------------------------------------------------*/
                //我已经住在自己的房间里
                if (this._staystatus == StayStatus.StayInOwn || this._staystatus == StayStatus.WithinOneHour || this._staystatus == StayStatus.UnKnown)
                {
                    SetMessage("我已经住在自己的房间里！");
                    return;
                }
                ////我露宿街头
                //else if (this._staystatus == StayStatus.InTheStreet)
                //{
                //    //自己的房子里没有人或者只有一个人
                //    if (this._myHouses[0].Status == HouseStatus.NoLodger || this._myHouses[0].Status == HouseStatus.CanStay)
                //    {
                //        if (StayTheHouse("我", CurrentAccount.UserId, this._myHouses[0]) == true)
                //            return;
                //    }
                //    else if (this._myHouses[0].Status == HouseStatus.Full)
                //    {
                //        if (Task.DriveFriends)
                //        {
                //            DriveAwayFriends(content);
                //            if (StayTheHouse("我", CurrentAccount.UserId, this._myHouses[0]) == true)
                //                return;
                //        }
                //    }
                //}
                //// 我住在其他人的房子里
                //else if (this._staystatus == StayStatus.StayInOthers)
                //{
                //    if (this._otherhousemaster != null)
                //    {
                //        HH.DelayedTime = Constants.DELAY_1SECONDS;
                //        string othercontent = HH.Get("http://www.kaixin001.com/~house/index.php?_lgmode=pri&fuid=" + this._otherhousemaster);
                //        if (othercontent.IndexOf("她目前住在她自己家") > -1 || othercontent.IndexOf("他目前住在他自己家") > -1)
                //        {
                //            SetMessage("我住在别人家里而且他(她)也住在家里，暂不搬离！");
                //            return;
                //        }
                //        else
                //        {
                //            //自己的房子里没有人或者只有一个人
                //            if (this._myHouses[0].Status == HouseStatus.NoLodger || this._myHouses[0].Status == HouseStatus.CanStay)
                //            {
                //                if (StayTheHouse("我", CurrentAccount.UserId, this._myHouses[0]) == true)
                //                    return;
                //            }
                //            else if (this._myHouses[0].Status == HouseStatus.Full)
                //            {
                //                if (Task.DriveFriends)
                //                {
                //                    DriveAwayFriends(content);
                //                    if (StayTheHouse("我", CurrentAccount.UserId, this._myHouses[0]) == true)
                //                        return;
                //                }
                //            }
                //        }
                //    }
                //}
                //我露宿街头
                else if (this._staystatus == StayStatus.InTheStreet || this._staystatus == StayStatus.StayInOthers)
                {
                    //自己的房子里没有人或者只有一个人
                    if (this._myHouses[0].Status == HouseStatus.NoLodger || this._myHouses[0].Status == HouseStatus.CanStay)
                    {
                        if (StayTheHouse("我", CurrentAccount.UserId, this._myHouses[0]) == true)
                            return;
                    }
                    else if (this._myHouses[0].Status == HouseStatus.Full)
                    {
                        if (Task.DriveFriends)
                        {
                            DriveAwayFriends(content);
                            if (StayTheHouse("我", CurrentAccount.UserId, this._myHouses[0]) == true)
                                return;
                        }
                    }
                }
               
                if (this._staystatus == StayStatus.WithinOneHour)
                    return;

                //1.自己房间满了，没有设定驱赶; 2.同一房间不能连续住两次
                StayOthersHouse();
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
                LogHelper.Write("GameHouse.StayHouse", content, ex, LogSeverity.Error);
                SetMessage(" 住进房子失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region StayTheHouse
        private bool StayTheHouse(string master, string uid, HouseInfo room)
        {
            try
            {
                //SetMessageLn("尝试住进" + master + "的房子：");

                bool ret = false;
                if (room != null && uid != string.Empty && room.Fuid != string.Empty && room.RoomId != string.Empty)
                {
                    string feedback = "";
                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                    string content = HH.Post("http://www.kaixin001.com/house/stayhouse.php", string.Format("verify={0}&roomid={1}&tuid={2}&fuid={3}&status=0", this._verifyCode, room.RoomId, uid, room.Fuid));
                    //<script language=javascript>new parent.dialog().reset();parent.stayhouse_newret(10752309,10752309,0,'0','0');</script>
                    //<script language="JavaScript"  type="text/javascript">document.domain="kaixin001.com";
                    //new parent.dialog().reset();parent.stayhouse_ret(6195212,6209068,5,0);
                    //</script>
                    //function stayhouse_newret(fuid,tuid,flag,cash,staycash)
                    //{
                    //    url = '/house/stayhouseret_dialog.php?verify=2588258_1062_2588258_1233119730_593462a6f2bbfedcabc156dacef4be1f&tuid='+tuid+'&flag='+flag+'&fuid='+fuid+'&cash='+cash+'&staycash='+staycash;
                    //    openWindow(url, 460, 500, '入住房子');
                    //}
                    feedback = JsonHelper.GetMid(content, "parent.stayhouse_newret(", ");");
                    if (feedback != null)
                    {
                        string[] feedparam = feedback.Split(',');
                        if (feedparam.Length == 5)
                        {
                            content = HH.Get(string.Format("http://www.kaixin001.com/house/stayhouseret_dialog.php?verify={0}&tuid={1}&flag={2}&fuid={3}cash={4}&staycash={5}", this._verifyCode, feedparam[1], feedparam[2], feedparam[0], feedparam[3], feedparam[4]));
                            ret = GetStayHouseFeedback(content, uid);
                        }
                    }
                    //<script language=javascript>new parent.dialog().reset();parent.stayhouse_ret(10752309,10752309,7,0);</script>
                    //function stayhouse_ret(fuid,tuid,flag,cash)
                    //{
                    //    url = '/house/stayhouseret_dialog.php?verify=10752309_1062_10752309_1233216189_dbbb110718a7618a0214e04fcf6c3ad8&tuid='+tuid+'&flag='+flag+'&fuid='+fuid+'&cash='+cash;
                    //    openWindow(url, 460, 500, '入住房子');
                    //}
                    feedback = JsonHelper.GetMid(content, "parent.stayhouse_ret(", ");");
                    if (feedback != null)
                    {
                        string[] feedparam = feedback.Split(',');
                        if (feedparam.Length == 4)
                        {
                            content = HH.Get(string.Format("http://www.kaixin001.com/house/stayhouseret_dialog.php?verify={0}&tuid={1}&flag={2}&fuid={3}&cash={4}", this._verifyCode, feedparam[1], feedparam[2], feedparam[0], feedparam[3]));
                            ret = GetStayHouseFeedback(content, uid);
                        }
                    }
                }
                else
                {
                    SetMessage(master + "的房间已住满了人！");
                }

                if (ret)
                {
                    if (master == "我")
                    {
                        this._staystatus = StayStatus.StayInOwn;
                        room.LodgerCount++;
                    }
                    else
                        this._staystatus = StayStatus.StayInOthers;
                }
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
                LogHelper.Write("GameHouse.StayTheHouse", master, ex, LogSeverity.Error);
                SetMessage(" 住进" + GetFriendNameById(uid) + "的房子失败！错误：" + ex.Message);
                return false;
            }
        }
        #endregion

        #region GetStayHouseFeedback
        private bool GetStayHouseFeedback(string content, string uid)
        {
            try
            {
                //if ($("flag1_1"))
                //{
                //    $("flag1_1").style.display = "block";
                //}
                string strmsg = "";
                bool ret = false;
                string strdivid = JsonHelper.GetMid(content, "if ($(\"", "\"))");
                if (strdivid != null)
                {
                    //<div id="flag1_1" style="display:none;">
                    //    <div class="h100">&nbsp;</div><div class="h50">&nbsp;</div>
                    //    <div class="f14 tac">
                    //        <div class="l" style="padding-left:80px;"><img src="http://img.kaixin001.com.cn/i2/notice.gif" alt="警告" /></div>
                    //        <div class="l" style="padding-left:10px;width:20em;"><strong><span class="sl">宋江</span>不是你的好友，你不能抢！</strong></div>
                    //        <div class="c"></div>
                    //    </div>
                    //    <div style="padding:30px 167px;">

                    //        <div class="rbs1"><input type="button" id="btn_sc" value="确定"  class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';" onclick="new parent.dialog().reset();"  style="padding:5px 20px;" /></div>
                    //    </div>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, "<div id=\"" + strdivid + "\" style=\"display:none;\">", "<div style=\"padding:30px 167px;\">");
                    if (strmsg != null)
                    {
                        SetMessage(" " + JsonHelper.FiltrateHtmlTags(strmsg));
                        if (strmsg.IndexOf("你搬进") > -1 || strmsg.IndexOf("你已经在") > -1)
                        {
                            ret = true;
                            SetMessage("住入成功！");
                        }
                        if (strmsg.IndexOf("你上次入住还没满1小时，不能换房居住") > -1 || strmsg.IndexOf("你在好友的房子里还没住满1小时，不能换房住") > -1)
                        {
                            this._staystatus = StayStatus.WithinOneHour;
                        }
                        if (strmsg.IndexOf("房子已经住满了，你不能入住") > -1 || strmsg.IndexOf("的房间已住满了人") > -1)
                            this._houseFullList.Add(DataConvert.GetInt32(uid));
                    }
                    else
                    {
                        //<div id="flag0_2" style="display:none;">
                        //    <div class="f14 tac" style="margin-top:100px;"><strong>你搬进<span class="sl">武小浪</span>的房子里住了！<br />
                        //    </strong>
                        //</div>
                        strmsg = JsonHelper.GetMid(content, "<div id=\"" + strdivid + "\" style=\"display:none;\">", "</div>");
                        if (strmsg != null)
                        {
                            SetMessage(" " + JsonHelper.FiltrateHtmlTags(strmsg));
                            if (strmsg.IndexOf("你搬进") > -1)
                            {
                                ret = true;
                                SetMessage(" 系统随机给你一笔住房津贴，数额为：" + JsonHelper.GetMid(content, "var staycash = \"", "\";") + "。 ");
                                SetMessage("住入成功！");
                            }
                            if (strmsg.IndexOf("你上次入住还没满1小时，不能换房居住") > -1 || strmsg.IndexOf("你在好友的房子里还没住满1小时，不能换房住") > -1)
                            {
                                this._staystatus = StayStatus.WithinOneHour; ;
                            }
                            if (strmsg.IndexOf("房子已经住满了，你不能入住") > -1 || strmsg.IndexOf("的房间已住满了人") > -1)
                                this._houseFullList.Add(DataConvert.GetInt32(uid));
                        }
                    }
                }

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
                LogHelper.Write("GameHouse.GetStayHouseFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region StayOthersHouse
        private void StayOthersHouse()
        {
            try
            {
                int num = 0;
                string content = "";
                string master = "";

                SetMessageLn("尝试住到白名单好友的房子里：");
                foreach (int uid in Operation.StayWhiteList)
                {
                    if (!this._houseFullList.Contains(uid))
                    {
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1062&url=index.php&fuid=" + uid);
                        master = JsonHelper.GetMid(content, "class=\"sl2\">", "</a>");
                        SetMessage(string.Format("=>好友#{0}:{1} ", new object[] { ++num, master }));
                        HouseInfo house = GetPreviouHouse(content);
                        house = ReadHouse(content, house, false);
                        //只有别人住在主人的房子里
                        if (house.IsOnlyOtherLodgersStayIn)
                            continue;
                        if (StayTheHouse(master, uid.ToString(), house))
                            return;
                        if (this._staystatus == StayStatus.WithinOneHour)
                            break;
                    }
                }
                if (this._staystatus == StayStatus.WithinOneHour)
                {
                    SetMessage("住入失败！");
                    return;
                }
                SetMessageLn("尝试住到其他好友的房子里：");
                foreach (FriendInfo friend in this._allHouseFriendsList)
                {
                    if (!Operation.StayWhiteList.Contains(friend.Id) && !this._houseFullList.Contains(friend.Id) && !Operation.StayBlackList.Contains(friend.Id))
                    {
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        content = HH.Get("http://www.kaixin001.com/!house/index.php?fuid=" + friend.Id);
                        master = JsonHelper.GetMid(content, "class=\"sl2\">", "</a>");
                        SetMessage(string.Format("=>好友#{0}:{1} ", new object[] { ++num, master }));
                        HouseInfo house = GetPreviouHouse(content);
                        house = ReadHouse(content, house, false);
                        //只有别人住在主人的房子里
                        if (house.IsOnlyOtherLodgersStayIn)
                            continue;
                        if (StayTheHouse(master, friend.Id.ToString(), house))
                            return;
                        if (this._staystatus == StayStatus.WithinOneHour)
                            break;
                    }
                }
                SetMessage("住入失败！");
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
                LogHelper.Write("GameHouse.StayOthersHouse", ex, LogSeverity.Error);
                SetMessage("住入其他好友房间失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region RobFriends
        private void RobFriends(HouseInfo house, bool IsFirstRoom)
        {
            try
            {
                SetMessage(house.HouseName + "：");

                if (house.Status == HouseStatus.Full)
                {
                    SetMessage("已住满，无法抢人！");
                    return;
                }
                //if (this._staystatus != StayStatus.StayInOwn && IsFirstRoom)
                //{
                //    SetMessage("我目前不住在自己家里，暂不抢人！");
                //    return;
                //}

                int num = 0;
                if (Task.RobFriends)
                {
                    SetMessageLn("抢白名单中的好友：");
                    foreach (int uid in Operation.RobWhiteList)
                    {
                        if (!Operation.RobBlackList.Contains(uid))
                        {
                            if (_robbedFriendsList.Contains(uid))
                                continue;

                            SetMessage(string.Format("=>好友#{0}:{1} ", new object[] { ++num, GetFriendNameById(uid) }));
                            if (!DifferentGender(uid) && IsFirstRoom)
                            {
                                SetMessage("性别相同，跳过 ");
                                continue;
                            }
                                                        
                            RobTheFriend(uid, house);                            

                            if (house.Status == HouseStatus.Full)
                                return;
                        }
                        else
                        {
                            SetMessage(string.Format("=>好友#{0}:{1} ", new object[] { ++num, GetFriendNameById(uid) }));
                            SetMessage("在黑名单中，跳过 ");
                        }
                    }
                }

                if (Task.RobFreeFriends)
                {
                    SetMessageLn("抢露宿街头的好友：");
                    foreach (FriendInfo friend in this._freeFriendsList)
                    {
                        if (!Operation.RobBlackList.Contains(friend.Id))
                        {
                            if (_robbedFriendsList.Contains(friend.Id))
                                continue;

                            SetMessage(string.Format("=>好友#{0}:{1} ", new object[] { ++num, friend.Name }));
                            if (!DifferentGender(friend.Id) && IsFirstRoom)
                            {
                                SetMessage("性别相同，跳过 ");
                                continue;
                            }

                            RobTheFriend(friend.Id, house);

                            if (house.Status == HouseStatus.Full)
                                return;
                        }
                        else
                        {
                            SetMessage(string.Format("=>好友#{0}:{1} ", new object[] { ++num, friend.Name }));
                            SetMessage("在黑名单中，跳过 ");
                        }
                    }
                    if (this._freeFriendsList.Count == 0)
                        SetMessage("没有露宿街头的好友！");
                }
                SetMessage("抢人失败！");
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
                LogHelper.Write("GameHouse.RobFriends", ex, LogSeverity.Error);
                SetMessage("抢人失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region RobTheFriend
        private bool RobTheFriend(int fuid, HouseInfo house)
        {
            try
            {
                bool ret = false;

                string feedback = "";
                HH.DelayedTime = Constants.DELAY_2SECONDS;
                string content = HH.Post("http://www.kaixin001.com/house/stayhouse.php", string.Format("verify={0}&roomid={1}&tuid={2}&fuid={3}&status=1", this._verifyCode, house.RoomId, house.Fuid, fuid));
                //<script language=javascript>new parent.dialog().reset();parent.stayhouse2_newret(10755333,10754113,0,'0','0');</script>"
                //function stayhouse2_newret(fuid,tuid,flag,cash,staycash)
                //{
                //    url = '/house/stayhouseret_dialog.php?verify=10752309_1062_10752309_1233234231_6ea15aac2840c10fae1b94b210a004d0&tuid='+tuid+'&flag='+flag+'&fuid='+fuid+'&cash='+cash+'&staycash='+staycash;
                //    openWindow(url, 460, 500, '抢人');
                //}
                feedback = JsonHelper.GetMid(content, "parent.stayhouse2_newret(", ");");
                if (feedback != null)
                {
                    string[] feedparam = feedback.Split(',');
                    if (feedparam.Length == 5)
                    {
                        content = HH.Get(string.Format("http://www.kaixin001.com/house/stayhouseret_dialog.php?verify={0}&tuid={1}&flag={2}&fuid={3}cash={4}&staycash={5}", this._verifyCode, feedparam[1], feedparam[2], feedparam[0], feedparam[3], feedparam[4]));
                        ret = GetRobFriendsFeedback(content, house);
                    }
                }
                else
                {
                    //<script language=javascript>new parent.dialog().reset();parent.stayhouse2_ret(10754420,10752309,5,0);</script>
                    //function stayhouse2_ret(fuid,tuid,flag,cash)
                    //{
                    //    url = '/house/stayhouseret_dialog.php?verify=10752657_1062_10752657_1233279955_19e8767fa5e10fb7a88940408db7422c&tuid='+tuid+'&flag='+flag+'&fuid='+fuid+'&cash='+cash;
                    //    openWindow(url, 460, 500, '抢人');
                    //}
                    feedback = JsonHelper.GetMid(content, "parent.stayhouse2_ret(", ");");
                    if (feedback != null)
                    {
                        string[] feedparam = feedback.Split(',');
                        if (feedparam.Length == 4)
                        {
                            content = HH.Get(string.Format("http://www.kaixin001.com/house/stayhouseret_dialog.php?verify={0}&tuid={1}&flag={2}&fuid={3}cash={4}", this._verifyCode, feedparam[1], feedparam[2], feedparam[0], feedparam[3]));
                            ret = GetRobFriendsFeedback(content, house);
                        }
                    }
                }

                _robbedFriendsList.Add(fuid);
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
                LogHelper.Write("GameHouse.RobTheFriend", GetFriendNameById(fuid), ex, LogSeverity.Error);
                SetMessage("抢" + GetFriendNameById(fuid) + "失败！错误：" + ex.Message);
                return false;
            }
        }
        #endregion

        #region GetRobFriendsFeedback
        private bool GetRobFriendsFeedback(string content, HouseInfo house)
        {
            try
            {
                //if ($("flag2_0"))
                //{
                //    $("flag2_0").style.display = "block";
                //}
                string strmsg = "";
                bool ret = false;
                string strdivid = JsonHelper.GetMid(content, "if ($(\"", "\"))");
                if (strdivid != null)
                {
                    //<div id="flag2_0" style="display:none;">
                    //    <div class="h100">&nbsp;</div><div class="h50">&nbsp;</div>
                    //    <div class="f14 tac">
                    //        <div class="l" style="padding-left:80px;"><img src="http://img.kaixin001.com.cn/i2/notice.gif" alt="警告" /></div>
                    //        <div class="l" style="padding-left:10px;width:20em;"><strong><span class="sl">庄子</span>已经在你的房子里住了！</strong></div>
                    //        <div class="c"></div>
                    //    </div>
                    //    <div style="padding:30px 167px;">

                    //        <div class="rbs1"><input type="button" id="btn_sc" value="确定"  class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';" onclick="new parent.dialog().reset();"  style="padding:5px 20px;" /></div>
                    //    </div>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, "<div id=\"" + strdivid + "\" style=\"display:none;\">", "<div style=\"padding:30px 167px;\">");
                    if (strmsg != null)
                    {
                        SetMessage(" " + JsonHelper.FiltrateHtmlTags(strmsg));
                        if (strmsg.IndexOf("已经在你的房子里住了") > -1 || strmsg.IndexOf("记得要好好招待你的客人哦") > -1)
                        {
                            ret = true;
                            SetMessage("抢人成功！");
                        }
                        if (strmsg.IndexOf("我的房间已住满了人") > -1 || strmsg.IndexOf("你的这套房子住满人了") > -1)
                        {
                            house.LodgerCount = house.RoomCount;
                        }
                    }
                }

                if (ret)
                    house.LodgerCount++;

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
                LogHelper.Write("GameHouse.GetRobFriendsFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region DriveAwayFriends
        private bool DriveAwayFriends(string content)
        {
            try
            {
                SetMessageLn("驱赶客人：");

                bool ret = false;
                string feedback = "";

                //<div class='butt_box'><a href='javascript:drive_away(10754113,8646)' class='butt'>驱赶</a><a href='javascript:dopet(10754113,8646)' class='butt ml10'>招待</a></div></li>
                string strLodger = JsonHelper.GetMid(content, "<a href='javascript:drive_away(", ")' class='butt'>驱赶");
                if (strLodger != null)
                {
                    string[] driveinfo = strLodger.Split(',');
                    if (driveinfo.Length == 2)
                    {
                        SetMessage(GetFriendNameById(driveinfo[0]) + " ");
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        feedback = HH.Post("http://www.kaixin001.com/house/driveaway.php", string.Format("verify={0}&roomid={1}&tuid={2}", this._verifyCode, driveinfo[1], driveinfo[0]));
                        //<script language=javascript>new parent.dialog().reset();parent.driveaway_ret(10753258,2);</script>
                        //function driveaway_ret(tuid,flag)
                        //{	
                        //    url = '/house/driveawayret_dialog.php?verify=10753642_1062_10753642_1233633837_468822c0cacfc789364fda7c9d2ced4f&tuid='+tuid+'&flag='+flag; 
                        //    openWindow(url, 460, 500, '驱赶客人');
                        //}
                        if (feedback != null)
                        {
                            string[] feedparam = feedback.Split(',');
                            if (feedparam.Length == 2)
                            {
                                feedback = HH.Get(string.Format("http://www.kaixin001.com/house/driveawayret_dialog.php?verify={0}&tuid={1}&flag={2}", this._verifyCode, feedparam[0], feedparam[1]));
                                ret = GetDriveAwayFriendsFeedback(feedback);
                            }
                        }
                    }
                }

                if (ret)
                    this._myHouses[0].LodgerCount--;

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
                LogHelper.Write("GameHouse.DriveAwayFriends", content, ex, LogSeverity.Error);
                SetMessage("驱赶客人失败！错误：" + ex.Message);
                return false;
            }
        }
        #endregion

        #region GetDriveAwayFriendsFeedback
        private bool GetDriveAwayFriendsFeedback(string content)
        {
            try
            {
                //if ($("flag0"))
                //{
                //    $("flag0").style.display = "block";
                //}
                string strmsg = "";
                bool ret = false;
                string strdivid = JsonHelper.GetMid(content, "if ($(\"", "\"))");
                if (strdivid != null)
                {
                    //<div id="flag0" style="display:none;">
                    //    <div class="f14 tac" style="margin-top:150px;"><strong>你已经把客人<span class=sl>西门望</span>赶走了！</strong></div>
                    //    <div style="padding:30px 167px;">

                    //        <div class="rbs1"><input type="button" id="btn_sc" value="确定"  class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';" onclick="javascript:parent.window.location.reload();"  style="padding:5px 20px;" /></div>
                    //    </div>
                    //</div>
                    //<div id="flag2" style="display:none;">
                    //    <div class="h100">&nbsp;</div><div class="h50">&nbsp;</div>
                    //    <div class="f14 tac">
                    //        <div class="l" style="padding-left:80px;"><img src="http://img.kaixin001.com.cn/i2/notice.gif" alt="警告" /></div>
                    //        <div class="l" style="padding-left:10px;width:20em;"><strong><span class="sl">西门望</span>还没住满1小时，你不能赶他走！</strong></div>
                    //        <div class="c"></div>
                    //    </div>

                    //    <div style="padding:30px 167px;">

                    //        <div class="rbs1"><input type="button" id="btn_sc" value="确定"  class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';" onclick="new parent.dialog().reset();"  style="padding:5px 20px;" /></div>
                    //    </div>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, "<div id=\"" + strdivid + "\" style=\"display:none;\">", "<div style=\"padding:30px 167px;\">");
                    if (strmsg != null)
                    {
                        SetMessage(" " + JsonHelper.FiltrateHtmlTags(strmsg));
                        if (strmsg.IndexOf("赶走了！") > -1 || strmsg.IndexOf("已经离开你的房间了！") > -1)
                        {
                            ret = true;
                            SetMessage("赶人成功！");
                        }
                    }
                }

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
                LogHelper.Write("GameHouse.GetDriveAwayFriendsFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region DifferentGender
        private bool DifferentGender(int uid)
        {
            //还未加入买房子游戏
            for (int ix = 0; ix < this._freeFriendsList.Count; ix++)
            {
                if (this._freeFriendsList[ix].Id == uid)
                {
                    if (this._freeFriendsList[ix].Gender == false && base.CurrentAccount.Gender == false)
                        return false;
                    if (this._freeFriendsList[ix].Gender == true && base.CurrentAccount.Gender == true)
                        return false;
                    if ((this._freeFriendsList[ix].Gender && base.CurrentAccount.Gender) == false)
                        return true;
                }
            }

            //还未加入买房子游戏， 无法读取性别
            for (int ix = 0; ix < this._allHouseFriendsList.Count; ix++)
            {
                if (this._allHouseFriendsList[ix].Id == uid)
                {
                    if (this._allHouseFriendsList[ix].Gender == false && base.CurrentAccount.Gender == false)
                        return false;
                    if (this._allHouseFriendsList[ix].Gender == true && base.CurrentAccount.Gender == true)
                        return false;
                    if ((this._allHouseFriendsList[ix].Gender && base.CurrentAccount.Gender) == false)
                        return true;
                }
            }
            return false;
        }
        #endregion

        #region GetGenderById
        private bool GetGenderById(int uid)
        {
            for (int ix = 0; ix < this._allHouseFriendsList.Count; ix++)
            {
                if (this._allHouseFriendsList[ix].Id == uid)
                {
                    return this._allHouseFriendsList[ix].Gender;
                }
            }
            return false;
        }
        #endregion

        #region Request

        public string RequestHouseHomePage(bool IsInitial)
        {
            string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1062");
            //if (content.IndexOf("<title>添加组件 - 开心网</title>") != -1)
            if (content.IndexOf("游戏：买房子") == -1)
            {
                SetMessageLn("还未安装买房子组件");
                //SetMessageLn("还未安装买房子组件,尝试安装中...");
                //HH.Post("http://www.kaixin001.com/app/install.php", "aid=1062&isinstall=1");
                //content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1062");
                //this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
                ////init city
                //HH.Get("http://www.kaixin001.com/house/inithouse_dialog.php?verify=" + this._verifyCode);
                //HH.Get("http://www.kaixin001.com/house/gethouseconfig.php?verify=" + this._verifyCode + "&roomid=8646&fuid=&r=0.8915753769688308");
                //HH.Post("http://www.kaixin001.com/house/inithouse_dialog.php", "http://www.kaixin001.com/house/inithouse_dialog.php?verify=" + this._verifyCode, "verify=" + this._verifyCode + "&step=2");
                //HH.Post("http://www.kaixin001.com/house/inithouse.php", "http://www.kaixin001.com/house/inithouse_dialog.php", "city=%E4%B8%8A%E6%B5%B7&verify=" + this._verifyCode);
                //content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1062");
                //SetMessage("你要闯荡的城市：上海！");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            if (content.IndexOf("我目前露宿街头") > -1)
                this._staystatus = StayStatus.InTheStreet;
            else if (content.IndexOf("我目前住在自己家") > -1)
                this._staystatus = StayStatus.StayInOwn;
            else if (content.IndexOf("我目前住在<a href=/~house/index.php?_lgmode=pri&fuid=") > -1)
            {
                this._staystatus = StayStatus.StayInOthers;
                this._otherhousemaster = JsonHelper.GetMid(content, "我目前住在<a href=/~house/index.php?_lgmode=pri&fuid=", " class='sl noline'>");
            }
            if (!IsInitial)
            {
                SetMessageLn("我的状态：" + JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<span  class=\"c_sl \">", "</span>")));                
               //<div class="tar">
               //             <a href="/!house/index.php?roomid=31647"><img src="http://img.kaixin001.com.cn/i2/house/1h-2.gif"/></a>
               //             <a href="/!house/index.php?roomid=9717"><img src="http://img.kaixin001.com.cn/i2/house/2h-1.gif"/></a>
               //             <a href="/!house/index.php?roomid=15552"><img src="http://img.kaixin001.com.cn/i2/house/3h-1.gif"/></a>
 
               //         </div>
                string myhouses = JsonHelper.GetMid(content, "<div class=\"tar\">", "</div>");
                if (!String.IsNullOrEmpty(myhouses) && myhouses.Trim() != string.Empty)
                {
                    int num = 0;
                    int count = 0;
                    for (string pos = JsonHelper.GetMid(myhouses, "<a href=\"/!house/index.php?roomid=", "\"><img src=\"http://img.kaixin001.com.cn/i2/house/", out num); pos != null; pos = JsonHelper.GetMid(myhouses, "<a href=\"/!house/index.php?roomid=", "\"><img src=\"http://img.kaixin001.com.cn/i2/house/", out num))
                    {
                        myhouses = myhouses.Substring(num);
                        if (!String.IsNullOrEmpty(pos))
                        {
                            string content2 = HH.Get("http://www.kaixin001.com/~house/index.php?roomid=" + pos);
                            HouseInfo house = GetPreviouHouse(content2);
                            SetMessageLn(string.Format("#{0}", ++count));
                            house = ReadHouse(content2, house, true);
                            this._myHouses.Add(house);
                        }
                    }
                }
                else
                {
                    HouseInfo house = GetPreviouHouse(content);
                    SetMessageLn("#1");
                    house = ReadHouse(content, house, true);
                    this._myHouses.Add(house);
                }
            }
            return content;
        }

        public string RequestAllHouseFriends()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/house/mystay_dialog.php?verify=" + this._verifyCode);
        }

        public string RequestFreeFriends()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/house/friendfree_dialog.php?verify=" + this._verifyCode);
        }
        #endregion

        #region Properties
        public Collection<FriendInfo> SameVillageFriendsList
        {
            get { return this._sameVillageFriendsList; }
        }
        public Collection<FriendInfo> AllHouseFriendsList
        {
            get { return this._allHouseFriendsList; }
        }
        public Collection<FriendInfo> FreeFriendsList
        {
            get { return this._freeFriendsList; }
        }
        public Collection<int> HouseFullList
        {
            get { return _houseFullList; }
            set { _houseFullList = value; }
        }
        #endregion
    }
}
