using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data;

using System.Net.Json;
using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class GameGarden : KaixinBase
    {
        private const int MAX_SOWSHAREDPLOTS_COUNT = 5;
        private Collection<FriendInfo> _allGardenFriendsList;
        //private Collection<FriendInfo> _matureFriendsList;
        //private Collection<FriendInfo> _mySharedFriendsList;
        private Collection<FriendInfo> _myGardenFriendsList;
        private Collection<SeedInfo> _seedsList;
        private Collection<RankSeedInfo> _rankSeedsList;
        private Collection<FruitInfo> _fruitsList;
        private Collection<int> _hasNothingTobeFarmedList;
        private Collection<SeedInfo> _myseedsList;        
        private bool _canstealfruit;
        private bool _needanswer;
        private bool _canfarmshared;
        private int _myRank;
        private bool _outofmoney;
        private bool _feedlimited;
        private bool _incorrentcount;

        public delegate void AllGardenFriendsFetchedEventHandler(Collection<FriendInfo> allgardenfriends);
        public event AllGardenFriendsFetchedEventHandler AllGardenFriendsFetched;

        public delegate void MatureFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event MatureFriendsFetchedEventHandler MatureFriendsFetched;

        public delegate void SeedsInShopFetchedEventHandler(Collection<SeedInfo> seeds);
        public event SeedsInShopFetchedEventHandler SeedsInShopFetched;

        public GameGarden()
        {
            this._canstealfruit = true;
            this._needanswer = false;
            this._canfarmshared = true;
            this._myRank = 1;
            this._outofmoney = false;
            this._feedlimited = false;
            this._incorrentcount = false;
            this._allGardenFriendsList = new Collection<FriendInfo>();
            //this._matureFriendsList = new Collection<FriendInfo>();
            //this._mySharedFriendsList = new Collection<FriendInfo>();
            this._myGardenFriendsList = new Collection<FriendInfo>();
            this._hasNothingTobeFarmedList = new Collection<int>();
            this._rankSeedsList = new Collection<RankSeedInfo>();
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //house
                SetMessageLn("正在初始化[花园]...");

                string content = RequestHouseHomePage(true);

                //all garden friends
                content = RequestAllGardenFriends();
                ReadAllGardenFriends(content, false);
                SetMessage("[所有花园的好友]信息下载成功！");

                //mature friends
                content = RequestMatureFriends();
                ReadMatureFriends(content, false);
                SetMessage("[花园中有成熟果实的好友]信息下载成功！");
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
                LogHelper.Write("GameGarden.Initialize", ex, LogSeverity.Error);
                SetMessage(" 初始化[花园]失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetAllGardenFriends
        public void GetAllGardenFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAllGardenFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void GetAllGardenFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_GARDEN;
                SetMessageLn("刷新[所有花园的好友]...");

                if (!this.ValidationLogin(true))
                {
                    if (AllGardenFriendsFetched != null)
                        AllGardenFriendsFetched(_allGardenFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestAllGardenFriends();
                ReadAllGardenFriends(content, true);
                SetMessageLn("[所有花园的好友]信息刷新成功！");

                //invoke event
                if (AllGardenFriendsFetched != null)
                    AllGardenFriendsFetched(_allGardenFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[所有花园的好友]信息刷新失败！");
        }

        #endregion

        #region ReadAllGardenFriends
        public void ReadAllGardenFriends(string content, bool printMessage)
        {
            int num;
            this._allGardenFriendsList.Clear();

            if (printMessage)
                SetMessageLn("读取[所有花园的好友]信息:");
            //<div class="l" style="width:8em;"><a href="javascript:gotoUser(6194153);" class="sl">庄子</a></div>
            string content2 = content;
            for (string pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num); pos != null; pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num))
            {
                FriendInfo friend = new FriendInfo();
                friend.Id = JsonHelper.GetMidInteger(content, "<a href=\"javascript:gotoUser(", ");\"");
                friend.Name = JsonHelper.GetMid(content, "class=\"sl\">", "</a>");
                this._allGardenFriendsList.Add(friend);
                if (printMessage)
                    SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
                content = content.Substring(num);
            }

            int ix = 0;
            for (string pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num); pos != null; pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num))
            {
                if (pos.IndexOf("value=\"1\"") > -1)
                {
                    this._allGardenFriendsList[ix].Gender = false;
                }
                else if (pos.IndexOf("value=\"0\"") > -1)
                {
                    this._allGardenFriendsList[ix].Gender = true;
                }
                else
                {
                    break;
                }

                if (printMessage)
                    SetMessageLn(this._allGardenFriendsList[ix].Name + "(" + this._allGardenFriendsList[ix].Id.ToString() + ")--" + (this._allGardenFriendsList[ix].Gender ? "男" : "女"));
                ix++;
                content2 = content2.Substring(num);
            }
            if (printMessage)
                SetMessageLn("完成读取！");
        }
        #endregion

        #region GetMatureFriends
        public void GetMatureFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetMatureFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetMatureFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_GARDEN;
                SetMessageLn("刷新[花园中有成熟果实的好友]...");

                if (!this.ValidationLogin(true))
                {
                    if (MatureFriendsFetched != null)
                        MatureFriendsFetched(_myGardenFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestMatureFriends();
                ReadMatureFriends(content, true);
                SetMessageLn("[花园中有成熟果实的好友]信息刷新成功！");

                //invoke event
                if (MatureFriendsFetched != null)
                    MatureFriendsFetched(_myGardenFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[花园中有成熟果实的好友]信息刷新失败！");
        }

        #endregion

        #region ReadMatureFriends
        public void ReadMatureFriends(string content, bool printMessage)
        {
            try
            {
                if (printMessage)
                    SetMessageLn("读取[花园中有成熟果实的好友]信息...");

                //this._mySharedFriendsList.Clear();
                //this._matureFriendsList.Clear();
                this._myGardenFriendsList.Clear();
               
                //[{"uid":"10752908","real_name":"\u5173\u4ec1","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","share":1},{"uid":"10752309","real_name":"\u5b8b\u6c5f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/75\/23\/20_10752309_1.jpg","share":1},{"uid":10752657,"real_name":"\u6b66\u5c0f\u6d6a","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","harvest":1},{"uid":10755959,"real_name":"\u5218\u6210\u540d","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_1_0.gif","harvest":1}]
                //[{"uid":"5629041","real_name":"\u5f20\u52e4","icon20":"http:\/\/pic1.kaixin001.com\/logo\/62\/90\/20_5629041_5.jpg","share":1},{"uid":13285985,"real_name":"\u66f9\u519b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/28\/59\/20_13285985_23.jpg","harvest":1},{"uid":2176837,"real_name":"\u9676\u51b6\uff08\u82b1\u843d\u65e0\u98ce\uff09","icon20":"http:\/\/pic1.kaixin001.com\/logo\/17\/68\/20_2176837_2.jpg","harvest":1},{"uid":2287096,"real_name":"\u5510\u6167","icon20":"http:\/\/pic.kaixin001.com\/logo\/28\/70\/20_2287096_1.jpg","harvest":1,"antiharvest":1},{"uid":2395406,"real_name":"\u6731\u8273\u96ef","icon20":"http:\/\/pic.kaixin001.com\/logo\/39\/54\/20_2395406_3.jpg","harvest":1,"fee":1},{"uid":27353139,"real_name":"\u9648\u6b63\u4e1c","icon20":"http:\/\/pic1.kaixin001.com\/logo\/35\/31\/20_27353139_1.jpg","harvest":1},{"uid":2803054,"real_name":"\u8f66\u79be\u5409","icon20":"http:\/\/pic.kaixin001.com\/logo\/80\/30\/20_2803054_1.jpg","harvest":1},{"uid":3125472,"real_name":"\u738b\u535a\u667a","icon20":"http:\/\/pic.kaixin001.com\/logo\/12\/54\/20_3125472_9.jpg","harvest":1},{"uid":3172993,"real_name":"\u5f90\u632f\u4e9a","icon20":"http:\/\/pic1.kaixin001.com\/logo\/17\/29\/20_3172993_2.jpg","harvest":1},{"uid":330818,"real_name":"\u8881\u4f73\u534e","icon20":"http:\/\/pic.kaixin001.com\/logo\/33\/8\/20_330818_22.jpg","harvest":1},{"uid":3352378,"real_name":"\u5f90\u9e4f\u52c7","icon20":"http:\/\/pic.kaixin001.com\/logo\/35\/23\/20_3352378_2.jpg","harvest":1},{"uid":35926680,"real_name":"\u65bd\u6625\u534e","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","harvest":1},{"uid":3612627,"real_name":"\u5468\u6797","icon20":"http:\/\/pic1.kaixin001.com\/logo\/61\/26\/20_3612627_4.jpg","harvest":1},{"uid":4026057,"real_name":"\u8521\u632f\u534e","icon20":"http:\/\/pic1.kaixin001.com\/logo\/2\/60\/20_4026057_2.jpg","harvest":1},{"uid":4343401,"real_name":"\u9648\u9e4f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/34\/34\/20_4343401_5.jpg","harvest":1},{"uid":5350880,"real_name":"\u5434\u6653\u6e05","icon20":"http:\/\/pic.kaixin001.com\/logo\/35\/8\/20_5350880_1.jpg","harvest":1},{"uid":6265093,"real_name":"\u987e\u73fa\u96ef","icon20":"http:\/\/pic1.kaixin001.com\/logo\/26\/50\/20_6265093_3.jpg","harvest":1},{"uid":7969758,"real_name":"\u9ad8\u5927\u519b","icon20":"http:\/\/pic.kaixin001.com\/logo\/96\/97\/20_7969758_8.jpg","harvest":1},{"uid":8288802,"real_name":"\u738b\u5ca9","icon20":"http:\/\/pic.kaixin001.com\/logo\/28\/88\/20_8288802_8.jpg","harvest":1},{"uid":9637731,"real_name":"\u66f9\u840d","icon20":"http:\/\/pic1.kaixin001.com\/logo\/63\/77\/20_9637731_2.jpg","harvest":1}]
                //[{"uid":"6752990","real_name":"\u5434\u5b50\u725b","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","share":1},{"uid":"6752812","real_name":"\u6c88\u81f4\u51b0","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","share":1},{"uid":"6904295","real_name":"\u5218\u7684\u8bdd","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","share":1},{"uid":"6903449","real_name":"\u9676\u5b9d","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_1_0.gif","share":1},{"uid":6194153,"real_name":"\u5e84\u5b50","icon20":"http:\/\/pic1.kaixin001.com\/logo\/19\/41\/20_6194153_1.jpg","harvest":1},{"uid":6733320,"real_name":"\u9676\u9187","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","harvest":1,"grass":1},{"uid":6209710,"real_name":"\u9648\u5fd7","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","grass":1},{"uid":6985380,"real_name":"\u9648\u89c2\u897f","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","grass":1},{"uid":7995480,"real_name":"\u9648\u6c5f\u94f8","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","grass":1}]
                JsonTextParser parser = new JsonTextParser();
                JsonArrayCollection arraySharedFriends = parser.Parse(content) as JsonArrayCollection;
                if (arraySharedFriends != null)
                {                   
                    foreach (JsonObjectCollection item in arraySharedFriends)
                    {
                        FriendInfo friend = new FriendInfo();
                        friend.Id = JsonHelper.GetIntegerValue(item["uid"]);
                        friend.Name = JsonHelper.GetStringValue(item["real_name"]);
                        friend.GardenShare = item["share"] != null ? true : false;
                        friend.GardenHarvest = item["harvest"] != null ? true : false;
                        friend.GardenFee = item["fee"] != null ? true : false;
                        friend.GardenGrass = item["grass"] != null ? true : false;
                        friend.GardenVermin = item["vermin"] != null ? true : false;
                        this._myGardenFriendsList.Add(friend);
                        //if (friend.GardenShare)
                        //    this._mySharedFriendsList.Add(friend);
                        //if (friend.GardenHarvest)
                        //    this._matureFriendsList.Add(friend);
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
                LogHelper.Write("GameGarden.ReadMatureFriends", content, ex, LogSeverity.Error);
                SetMessage(" 读取[花园中有成熟果实的好友]信息失败！" + ex.Message);
            }
        }
        #endregion

        #region GetSeedsInShop
        public void GetSeedsInShopByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetSeedsInShop));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetSeedsInShop()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_UPDATEDATA;
                SetMessageLn("刷新[商店中种子列表]...");

                if (!this.ValidationLogin())
                {
                    if (SeedsInShopFetched != null)
                        SeedsInShopFetched(null);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestSeedsList();
                Collection<SeedInfo> seeds = ConfigCtrl.GetOriginalSeedsList(content);

                if (seeds == null || seeds.Count == 0)
                    SetMessageLn("[商店中种子列表]信息刷新失败！");
                else
                    SetMessageLn("[商店中种子列表]信息刷新成功！");

                //invoke event
                if (SeedsInShopFetched != null)
                    SeedsInShopFetched(seeds);
            });
            base.ExecuteTryCatchBlock(th, "[商店中种子列表]信息刷新失败！");
        }

        #endregion

        #region RunGarden
        public void RunGarden()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_GARDEN;

                SetMessageLn("开始花园...");

                //house
                string contentHome = RequestHouseHomePage(false);

                GardenInfo garden = ReadGarden(this._verifyCode, CurrentAccount.UserId);
                if (garden == null)
                {
                    SetMessageLn("无法读取我的花园信息！");
                    return;
                }

                ReadMySeeds(garden);

                if (_myseedsList.Count == 0)
                {
                    SetMessage("没有种子了！");
                }

                if (Task.FarmSelf)
                    FarmGarden(garden);

                string content = "";

                //偷窃果实
                if (Task.StealFruit && Operation.StealAll || Task.FarmShared && Operation.FarmAll)
                {
                    content = RequestMatureFriends();
                    ReadMatureFriends(content, false);
                }
                if (Task.StealFruit)
                    StealGardens();

                if (Task.HelpOthers || Task.FarmShared && Operation.FarmAll)
                {
                    content = RequestAllGardenFriends();
                    ReadAllGardenFriends(content, false);
                }
                //播种爱心地块
                if (Task.HelpOthers || Task.FarmShared)
                    HelpOthersGardens();

                //赠送果实
                if (Task.PresentFruit)
                    PresentFruit();

                //出售果实
                if (Task.SellFruit)
                    SellFruit(garden);

                SetMessageLn("花园完成！");

            });
            base.ExecuteTryCatchBlock(th, "发生异常，花园失败！");
        }
        #endregion

        #region ReadGarden
        private GardenInfo ReadGarden(string verifyCode, string fuid)
        {
            GardenInfo garden = null;
            RequestGarden(fuid);
            string strGarden = RequestGardenConf(verifyCode, fuid);           
            garden = ConfigCtrl.GetGarden(strGarden);
            if (garden == null)
            {
                SetMessageLn("读取花园信息失败！");
            }
            else
            {
                if (CurrentAccount.UserId == fuid)
                    this._myRank = garden.Rank;

                SetMessageLn(garden.Name + "的花园：" + garden.RankTip + " " + garden.CashTip + " 魅力：" + garden.TCharms);

                if (garden.Plots != null)
                {
                    //plot.Shared = 0 私人地块
                    //plot.Shared = 1 爱心地块
                    //plot.Shared = 2 访问别人的花园时，自己的地块

                    //int num = 0;
                    //foreach (PlotInfo plot in garden.Plots)
                    //{
                    //    SetMessageLn(string.Format("第{0}个地块：", ++num));
                    //    SetMessage("water:" + plot.Water.ToString());
                    //    SetMessage(" farmnum:" + plot.FarmNum.ToString());
                    //    SetMessage(" vermin:" + plot.Vermin.ToString());
                    //    SetMessage(" cropsid:" + plot.CropsId.ToString());
                    //    SetMessage(" fuid:" + plot.Fuid.ToString());
                    //    SetMessage(" status:" + plot.Status.ToString());
                    //    SetMessage(" grass:" + plot.Grass.ToString());
                    //    SetMessage(" shared:" + plot.Shared.ToString());
                    //    if (plot.Pic != null)
                    //    {
                    //        SetMessage(" pic:" + plot.Pic.ToString());
                    //        SetMessage(" fruitpic:" + plot.FruitPic.ToString());
                    //        SetMessage(" picwidth:" + plot.PicWidth.ToString());
                    //        SetMessage(" picheight:" + plot.PicHeight.ToString());
                    //        SetMessage(" cropsstatus:" + plot.CropsStatus.ToString());
                    //        SetMessage(" grow:" + plot.Grow.ToString());
                    //        SetMessage(" totalgrow:" + plot.TotalGrow.ToString());
                    //        SetMessage(" fruitnum:" + plot.FruitNum.ToString());
                    //        SetMessage(" seedid:" + plot.SeedId.ToString());
                    //    }
                    //    if (plot.Crops != null)
                    //        SetMessage(" crops:" + plot.Crops.ToString());
                    //    if (plot.Farm != null)
                    //        SetMessage(" farm:" + plot.Farm.ToString());
                    //}

                    foreach (PlotInfo plot in garden.Plots)
                    {
                        if (plot.Status != 1)
                            continue;

                        SetMessageLn(string.Format("第{0}个地块：", plot.FarmNum));
                        if (plot.Status == 1)
                        {
                            if (plot.Shared == 0)
                                SetMessage("私人地块");
                            else
                                SetMessage("爱心地块");
                        }
                        else
                            SetMessage("尚未开发");
                        if (plot.SeedId != 0)
                        {
                            SetMessage(" *" + GetFruitNameById(plot.SeedId));
                        }
                        if (!String.IsNullOrEmpty(plot.Crops))
                        {
                            SetMessage(" *" + plot.Crops);
                        }
                        //if (!String.IsNullOrEmpty(plot.Farm))
                        //{
                        //    SetMessage(" *农田：" + plot.Farm);
                        //}
                        SetMessage(" *水：" + plot.Water.ToString());
                    }
                }
            }

            return garden;
        }
        #endregion

        #region ReadMySeeds
        private void ReadMySeeds(GardenInfo garden)
        {
            SetMessageLn("我的种子：");
            string content = RequestMySeedList(1);
            int totalpage = 0;
            _myseedsList = ConfigCtrl.GetMySeeds(content, ref totalpage);

            if (_myseedsList == null)
            {
                SetMessage("无法读取我的种子信息！");
                return;
            }

            if (totalpage > 1)
            {
                for (int ix = 2; ix <= totalpage; ix++)
                {
                    content = RequestMySeedList(ix);
                    int pagenum = 0;
                    Collection<SeedInfo> nextseeds = ConfigCtrl.GetMySeeds(content, ref pagenum);
                    foreach (SeedInfo seed in nextseeds)
                    {
                        if (seed != null && !String.IsNullOrEmpty(seed.Name))
                            _myseedsList.Add(seed);
                    }
                }
            }

            int num = 0;
            foreach (SeedInfo seed in _myseedsList)
            {
                ++num;
                if (num == _myseedsList.Count)
                    SetMessage(seed.Name + "(" + seed.Num.ToString() + ")");
                else
                    SetMessage(seed.Name + "(" + seed.Num.ToString() + "),");
            }
        }
        #endregion

        #region BuySeed
        private bool BuySeed(int seedid)
        {
            //买种子
            SetMessageLn("购买种子...");
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/buyseed.php?verify={0}&seedid={1}&num={2}", this._verifyCode, seedid, Task.BuySeedCount));
            return GetBuySeedFeedback(content, GetSeedNameById(seedid), Task.BuySeedCount);
        }
        #endregion

        #region GetBuySeedFeedback
        private bool GetBuySeedFeedback(string content, string name, int count)
        {
            //<data><ret>fail</ret><msg>购买种子失败&lt;br&gt;你的现金不够，不能购买</msg><err>1</err></data>
            //<data><ret>fail</ret><msg>购买种子成功</msg><err>1</err></data>
            //<data><ret>succ</ret></data>
            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 失败！");
                //LogHelper.Write(CurrentAccount.UserName, content + " 失败！", LogSeverity.Warn);
                if (content.IndexOf("你的现金不够，不能购买") > -1)
                {
                    LogHelper.Write(CurrentAccount.UserName, JsonHelper.FiltrateHtmlTags(content).Replace("fail", "").Replace("1", "") + " 失败！", LogSeverity.Warn);
                    _outofmoney = true;
                }
                if (content.IndexOf("<data><ret>fail</ret><msg>购买种子成功</msg><err>1</err></data>") > -1)
                    _outofmoney = true;
                return false;
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                SetMessage("购买" + count.ToString() + "个" + name + "种子完成！");
                return true;
            }
            else
            {
                LogHelper.Write(CurrentAccount.UserName, content + " 失败！", LogSeverity.Warn);
                SetMessage(content);
            }
            return false;
        }
        #endregion        

        #region FarmGarden
        private void FarmGarden(GardenInfo garden)
        {
            try
            {
                SetMessageLn("开始自家耕作...");

                if (garden == null)
                    return;

                string content = "";

                if (garden.Plots != null)
                {
                    _outofmoney = false;
                    _feedlimited = false;

                    foreach (PlotInfo plot in garden.Plots)
                    {
                        try
                        {
                            if (plot.Status != 1)
                                continue;

                            if ((plot.SeedId == 102 && plot.Crops.IndexOf("点击可摇钱") > -1) ||
                                plot.Water != 5 ||
                                plot.Vermin == 1 ||
                                plot.Grass == 1 ||
                                plot.CropsStatus == 2 ||
                                plot.Shared == 0 && (plot.CropsStatus == 3 && String.IsNullOrEmpty(plot.Crops) && String.IsNullOrEmpty(plot.Farm) || plot.CropsStatus == -1) ||
                                plot.CropsId == 0 && plot.Shared == 0)
                            {
                                if (plot.Shared == 0)
                                    SetMessageLn(string.Format("=>第{0}个地块：", plot.FarmNum));
                                else
                                    SetMessageLn(string.Format("=>第{0}个爱心地块：", plot.FarmNum));
                            }

                            //摇钱树
                            if (plot.SeedId == 102 && plot.Crops.IndexOf("点击可摇钱") > -1)
                            {
                                //<crops>生长阶段：85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;再过18小时26分成熟（不可摇钱）&lt;/font&gt;&lt;br&gt;距离收获：18小时26分&lt;font size='12' color='#666666'&gt;，点击可摇钱&lt;/font&gt;</crops>
                                //<crops>生长阶段：85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;再过18小时14分成熟（不可摇钱）&lt;/font&gt;&lt;br&gt;距离收获：18小时14分</crops>
                                //http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid=0&verify=6194153%5F1062%5F6194153%5F1253607483%5F14f6afef57593e63f22fda3adc9a5685
                                SetMessage("可以摇钱：");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid={0}&verify={1}", "0", this._verifyCode));
                                GetYaoQianFeedback(content);
                            }

                            if (plot.Water != 5)
                            {
                                //浇水
                                SetMessage("需要浇水：");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Post("http://www.kaixin001.com/house/garden/water.php", string.Format("fuid=0&farmnum={0}&verify={1}&seedid=0&r=0%2E6590517126023769", plot.FarmNum, this._verifyCode));
                                GetFarmFeedback(content);
                            }

                            if (plot.Vermin == 1)
                            {
                                //捉虫
                                SetMessage("需要捉虫：");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antivermin.php?verify={0}&seedid=0&r=0%2E3779320823960006&fuid=0&farmnum={1}", this._verifyCode, plot.FarmNum));
                                GetFarmFeedback(content);
                            }

                            if (plot.Grass == 1)
                            {
                                //除草
                                SetMessage("需要除草：");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antigrass.php?farmnum={0}&verify={1}&seedid=0&r=0%2E8164945561438799&fuid=0", plot.FarmNum, this._verifyCode));
                                GetFarmFeedback(content);
                            }

                            if (plot.CropsStatus == 2)
                            {
                                //收获
                                SetMessage("可以收获：");
                                if (Task.HarvestFruit)
                                {
                                    HH.DelayedTime = Constants.DELAY_3SECONDS;                                    
                                    //content = HH.Post("http://www.kaixin001.com/house/garden/havest.php", string.Format("fuid=0&farmnum={0}&verify={1}&seedid=0&r=0%2E44418928399682045", plot.FarmNum, this._verifyCode));
                                    //http://www.kaixin001.com/!house/!garden/havest.php?r=0%2E39583466947078705&fuid=0&seedid=0&farmnum=14&verify=6194153%5F1062%5F6194153%5F1247533515%5F6f718b35e6908e970099b0ab9a9237a3
                                    content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden/havest.php?r=0%2E39583466947078705&fuid=0&seedid=0&farmnum={0}&verify={1}", plot.FarmNum, this._verifyCode));
                                    if (GetFarmFeedback(content))
                                    {
                                        plot.CropsStatus = 3;
                                        plot.Crops = null;
                                        plot.Farm = null;

                                        //人参
                                        if (plot.SeedId == 21)
                                            garden.PanaxCount--;
                                        //人参(有人参娃娃)
                                        if (plot.SeedId == 25)
                                            garden.PanaxBabyCount--;
                                    }
                                }
                            }

                            if (plot.Shared == 0 &&
                                (plot.CropsStatus == 3 && String.IsNullOrEmpty(plot.Crops) && String.IsNullOrEmpty(plot.Farm) ||
                                plot.CropsStatus == -1))
                            {
                                //犁地
                                SetMessage("需要犁地：");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/plough.php?verify={0}&seedid=0&r=0%2E018698612228035927&fuid=0&farmnum={1}", this._verifyCode, plot.FarmNum));
                                if (GetFarmFeedback(content))
                                    plot.CropsId = 0;
                            }

                            if (plot.CropsId == 0 && plot.Shared == 0)
                            {
                                //播种
                                SetMessage("可以播种：");
                                bool issowed = false;
                                if (Task.SowMySeedsFirst && _myseedsList != null && _myseedsList.Count != 0)
                                {
                                    foreach (SeedInfo myseed in _myseedsList)
                                    {
                                        //爱情果，友谊花
                                        if (myseed.SeedId == 39 || myseed.SeedId == 61 || myseed.Num < 1 || myseed.Valid == false)
                                            continue;

                                        if (SowPlot(garden, myseed, plot))
                                        {
                                            issowed = true;
                                            break;
                                        }
                                    }
                                }
                                if (issowed)
                                    continue;
                                SeedInfo seed = GetOwnFarmSeed(garden);
                                if (seed != null)
                                {
                                    SowPlot(garden, seed, plot);
                                }
                                if (_outofmoney)
                                    return;
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
                            LogHelper.Write("GameGarden.FarmGarden", "自家耕作失败", ex, LogSeverity.Error);
                            SetMessage(ex.Message);
                            continue;
                        }
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
                LogHelper.Write("GameGarden.FarmGarden", ex, LogSeverity.Error);
                SetMessage(" 自家耕作失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetYaoQianFeedback
        private void GetYaoQianFeedback(string content)
        {
            //<data>
            //  <tip>恭喜你在自己家的摇钱树摇到了一堆金子，价值20000元，已充入你的账户</tip>
            //  <ret>succ</ret>
            //  <swf>http://img.kaixin001.com.cn/i2/house/garden/yaoqianshu.swf</swf>
            //</data>
            //<data><ret>fail</ret><reason>今天好友摇钱的次数超过了4次,请明天再来摇吧！</reason></data>
            if (content.IndexOf("<ret>succ</ret>") > -1)
                SetMessage(JsonHelper.GetMid(content, "<tip>", "</tip>"));
            else if (content.IndexOf("<ret>fail</ret>") > -1)
                SetMessage(JsonHelper.GetMid(content, "<reason>", "</reason>"));
            else
            {
                SetMessage(content);
                LogHelper.Write("GameGarden.GetYaoQianFeedback", content, LogSeverity.Info);
            }
        }
        #endregion

        #region SowPlot
        private bool SowPlot(GardenInfo garden, SeedInfo seed, PlotInfo plot)
        {
            SetMessage(" 尝试播种" + seed.Name + "...");
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            string content = HH.Post("http://www.kaixin001.com/house/garden/farmseed.php", string.Format("fuid=0&farmnum={0}&verify={1}&seedid={2}&r=0%2E012194405309855938", plot.FarmNum, this._verifyCode, seed.SeedId));
            if (GetFarmFeedback(content))
            {
                //人参
                if (seed.SeedId == 21)
                    garden.PanaxCount++;
                //人参(有人参娃娃)
                if (seed.SeedId == 25)
                    garden.PanaxBabyCount++;
                seed.Num--;
                return true;
            }
            else
            {
                if (content.Contains("该种子最多同时可种"))
                    seed.Valid = false;
                return false;
            }

        }
        #endregion

        #region GetOwnSeedByRank
        private RankSeedInfo GetOwnSeedByRank(int rank, int panaxcount, int panaxbabycount, int clowningcount, int stramoniumcount)
        {

            if (rank > 55)
                rank = 55;

            //已经有两块地是人参，则返回冬虫夏草
            if (rank >= 15 && rank <= 19 && (panaxcount >= 2 || panaxbabycount >= 2))
                rank = 14;
            //已经有一块地是曼珠沙华，则返回藏红花
            if (rank == 30 && clowningcount >= 1)
                rank = 29;
            //已经有两块地是曼陀罗，则返回何首乌
            if (rank >= 32 && rank <= 39 && stramoniumcount >= 2)
                rank = 31;

            foreach (RankSeedInfo rankseed in _rankSeedsList)
            {
                if (rankseed.Rank == rank)
                    return rankseed;
            }
            return null;
        }
        #endregion

        #region GetOwnFarmSeed
        private SeedInfo GetOwnFarmSeed(GardenInfo garden)
        {
            int seedid = 0;
            string seedname = "";
            SeedInfo seed = null;
            //if (Task.SowMySeedsFirst && _myseedsList != null && _myseedsList.Count != 0)
            //{
            //    foreach (SeedInfo seedunit in _myseedsList)
            //    {
            //        //爱情果，友谊花
            //        if (seedunit.SeedId == 39 || seedunit.SeedId == 61 || seedunit.Num < 1)
            //            continue;
            //        //人参
            //        if (seedunit.SeedId == 21)
            //        {
            //            //人参最多同时可种2块地
            //            if (garden.PanaxCount >= 2)
            //                continue;
            //        }
            //        //人参(有人参娃娃)
            //        else if (seedunit.SeedId == 25)
            //        {
            //            //人参(有人参娃娃)最多同时可种2块地
            //            if (garden.PanaxBabyCount >= 2)
            //                continue;
            //        }
            //        //曼珠沙华
            //        else if (seedunit.SeedId == 104)
            //        {
            //            //曼珠沙华最多同时可种1块地
            //            if (garden.ClowningCount >= 1)
            //                continue;
                    
            //        }
            //        //曼陀罗
            //        else if (seedunit.SeedId == 114)
            //        {
            //            //该种子最多同时可种2块地
            //            if (garden.StramoniumCount >= 2)
            //                continue;
            //        }
            //        //摇钱树
            //        else if (seedunit.SeedId == 102)
            //        {
            //            //该种子最多同时可种1块地
            //            if (garden.YaoqianCount >= 1)
            //                continue;
            //        }
                        
            //        else
            //        {
            //            seed = seedunit;
            //            break;
            //        }
            //    }
            //}

            if (seed == null)
            {
                if (Task.ExpensiveFarmSelf)
                {
                    RankSeedInfo rankseed = GetOwnSeedByRank(garden.Rank, garden.PanaxCount, garden.PanaxBabyCount, garden.ClowningCount, garden.StramoniumCount);
                    if (rankseed != null)
                    {
                        seedid = rankseed.SeedId;
                        seedname = rankseed.Name;
                        SetMessage(" 你可以播种的最贵的种子：" + rankseed.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetOwnFarmSeed", " 无法取得等级" + garden.Rank + "的种子", LogSeverity.Warn);
                        SetMessage(" 无法取得等级" + garden.Rank + "的种子");
                        return null;
                        //return FarmStatus.Continue;
                    }
                }
                else
                {
                    SeedInfo seed1 = GetSeedById(Task.CustomFarmSelf);
                    if (seed1 != null)
                    {
                        seedid = seed1.SeedId;
                        seedname = seed1.Name;
                        SetMessage(" 你设定的播种种子：" + seed1.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetOwnFarmSeed", " 无法取得自定义的种子：" + Task.CustomFarmSelf.ToString(), LogSeverity.Warn);
                        SetMessage(" 无法取得自定义的种子：" + Task.CustomFarmSelf.ToString());
                        return null;
                        //return FarmStatus.Continue;
                    }
                }
                seed = GetFarmSeedById(seedid);
                if (seed == null)
                {
                    SetMessage(string.Format(" 没有{0}的种子", seedname));
                    if (_outofmoney)
                    {
                        SetMessage(" 现金不够，不能购买，播种失败");
                        //return FarmStatus.OutOfMoney;
                    }
                    if (Task.BuySeed)
                    {
                        if (BuySeed(seedid))
                        {
                            ReadMySeeds(garden);
                            seed = GetFarmSeedById(seedid);
                        }
                        //else
                        //    return FarmStatus.Continue;
                    }
                }
            }

            return seed;
        }
        #endregion

        #region GetFarmSeedById
        private SeedInfo GetFarmSeedById(int seedid)
        {
            if (_myseedsList == null || _myseedsList.Count == 0)
                return null;

            for (int ix = 0; ix <= _myseedsList.Count - 1; ix++)
            {
                if (_myseedsList[ix].Num >= 1 && _myseedsList[ix].SeedId == seedid)
                    return _myseedsList[ix];
            }

            return null;
        }
        #endregion

        #region GetFarmFeedback
        private bool GetFarmFeedback(string content)
        {
            try
            {
                //该种子最多同时可种2块地 失败！
                //<data><ret>succ</ret></data>
                //<data><ret>fail</ret><reason>已没有该种子了</reason></data>
                //<data><tips>这块地是你与共种的，刚才收获的果实你们两1人1半</tips><ret>fail</ret><reason>你不是该土地的主人，不能收获</reason></data>
                //<data><leftnum>16</leftnum><stealnum>0</stealnum><num>16</num><seedname>土豆</seedname><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/tudou.swf</fruitpic><ret>succ</ret></data>
                //SetMessage(content);
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    if (content.IndexOf("该种子最多同时可种") > -1)
                        _feedlimited = true;

                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "").Replace("这块地是你与共种的，刚才收获的果实你们两1人1半", "") + " 失败！");
                    return false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    if (content.IndexOf("leftnum") > -1)
                    {
                        //收获
                        //StealInfo objSteal = ConfigCtrl.ConvertToStealObject(content);
                        //SetMessage(objSteal.SeedName + "，数量：" + objSteal.Num + "，剩余：" + objSteal.LeftNum + " 完成！");
                        SetMessage(JsonHelper.GetMid(content, "<seedname>", "</seedname>") + "，收获数量：" + JsonHelper.GetMid(content, "<havestnum>", "</havestnum>") + " 完成！");
                        return true;
                    }
                    else
                    {   //浇水//捉虫//犁地//播种
                        SetMessage("完成！");
                        return true;
                    }
                }
                else
                {
                    SetMessage(content);
                }
                return false;
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
                LogHelper.Write("GameGarden.GetFarmFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion        

        #region StealGardens
        private void StealGardens()
        {
            int num = 0;

            SetMessageLn("开始偷果实：");
            //先偷白名单中的人
            SetMessageLn("开始偷白名单中的人：");
            foreach (int uid in Operation.StealWhiteList)
            {
                try
                {
                    SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");
                    FriendInfo friend = GetHelpGardenFriendById(uid);
                    if (friend == null || friend.GardenHarvest == false)
                    {
                        SetMessage("没什么可偷的，跳过");
                        continue;
                    }
                  
                    if (Operation.StealBlackList.Contains(uid))
                    {
                        SetMessage(base.GetFriendNameById(uid) + "在黑名单中，跳过");
                        continue;
                    }
                    StealTheGarden(uid.ToString());
                    if (this._canstealfruit == false)
                    {
                        SetMessageLn("由于你今天不能再偷了，停止偷果实！");
                        return;
                    }
                    if (this._needanswer == true)
                    {
                        SetMessageLn("需要先答题才能偷，停止偷果实！");
                        return;
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
                    LogHelper.Write("GameGarden.StealGardens", ex, LogSeverity.Error);
                    continue;
                }
            }

            //偷剩下的人
            if (Operation.StealAll)
            {
                num = 0;
                SetMessageLn("去其他有成熟果实的花园偷：");
                foreach (FriendInfo friend in this._myGardenFriendsList)
                {
                    try
                    {
                        if (!friend.GardenHarvest)
                            continue;

                        if (Operation.StealWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                            continue;
                        
                        SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                        if (Operation.StealBlackList.Contains(friend.Id))
                        {
                            SetMessage(friend.Name + "在黑名单中，跳过");
                            continue;
                        }                        
                        StealTheGarden(friend.Id.ToString());
                        if (this._canstealfruit == false)
                        {
                            SetMessageLn("由于你今天不能再偷了，停止偷果实！");
                            return;
                        }
                        if (this._needanswer == true)
                        {
                            SetMessageLn("需要先答题才能偷，停止偷果实！");
                            return;
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
                        LogHelper.Write("GameGarden.StealGardens", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
        }
        #endregion 

        #region StealTheGarden
        private void StealTheGarden(string fuid)
        {
            try
            {
                string content = RequestGarden(fuid);
                string verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");

                GardenInfo garden = ReadGarden(verifyCode, fuid);
                if (garden == null)
                {
                    SetMessageLn("无法读取花园信息，跳过");
                }
                else if (garden.HasMonitor)
                {
                    SetMessageLn("有菜老伯，跳过");
                }
                else if (garden != null && garden.Plots != null)
                {
                    foreach (PlotInfo plot in garden.Plots)
                    {
                        if (!_canstealfruit)
                            break;
                        if (_needanswer)
                            break;

                        //摇钱树
                        if (plot.SeedId == 102 && plot.Crops.IndexOf("点击可摇钱") > -1)
                        {

                            SetMessageLn(string.Format("=>第{0}个地块：", plot.FarmNum));
                            //<crops>生长阶段：85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;再过18小时26分成熟（不可摇钱）&lt;/font&gt;&lt;br&gt;距离收获：18小时26分&lt;font size='12' color='#666666'&gt;，点击可摇钱&lt;/font&gt;</crops>
                            //<crops>生长阶段：85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;再过18小时14分成熟（不可摇钱）&lt;/font&gt;&lt;br&gt;距离收获：18小时14分</crops>
                            //http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid=0&verify=6194153%5F1062%5F6194153%5F1253607483%5F14f6afef57593e63f22fda3adc9a5685
                            SetMessage("可以摇钱：");
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid={0}&verify={1}", fuid, this._verifyCode));
                            GetYaoQianFeedback(content);
                        }

                        //issue:如何判断 已偷过，做人要厚道？
                        if (plot.CropsStatus == 2 && plot.Shared == 0)
                        {
                            try
                            {
                                SetMessageLn(string.Format("=>第{0}个地块：", plot.FarmNum));
                                SetMessage("有成熟的果实，尝试偷窃：");

                                //已偷过
                                if (plot.Crops.IndexOf("已偷过") > -1)
                                {
                                    SetMessage("已偷过");
                                    continue;
                                }
                                //再过1小时48分可偷
                                //再过39分可偷
                                Regex regular = new Regex(@"再过[\s\S]+可偷");
                                if (regular.IsMatch(plot.Crops))
                                {
                                    SetMessage(regular.Match(plot.Crops).ToString());
                                    continue;
                                }

                                FruitInfo fruit = GetFruitById(plot.SeedId);
                                if (fruit != null)
                                {
                                    if (Task.StealForbiddenFruitsList.Contains(fruit.FruitId))
                                    {
                                        SetMessage(fruit.Name + "在禁止偷窃列表中，不偷");
                                        continue;
                                    }
                                    //if (seed.SellPrice < Task.StealPrice)
                                    //{
                                    //    SetMessage(seed.Name + "的出售价格：" + seed.SellPrice + "小于" + Task.StealPrice + "，不偷");
                                    //    continue;
                                    //}
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    //http://www.kaixin001.com/!house/!garden/havest.php?r=0%2E6745863440446556&fuid=6209015&seedid=0&farmnum=2&verify=6208965%5F1062%5F6208965%5F1247493125%5F932cabd78128ff57424958e42fc034c4
                                    content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden/havest.php?r=0%2E6745863440446556&fuid={0}&seedid=0&farmnum={1}&verify={2}", fuid, plot.FarmNum, verifyCode));
                                    _canstealfruit = GetStealFeedback(content);
                                }
                                else
                                {
                                    if (Task.StealUnknowFruit)
                                    {
                                        SetMessage("StealTheGarden:" + GetFriendNameById(fuid) + "'s plot.SeedId:" + plot.SeedId);
                                        LogHelper.Write("GameGarden.StealTheGarden", GetFriendNameById(fuid) + "'s plot.SeedId:" + plot.SeedId, LogSeverity.Warn);

                                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                                        content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/havest.php?verify={0}&seedid=0&r=0%2E5035183746367693&fuid={1}&farmnum={2}", verifyCode, fuid, plot.FarmNum));
                                        LogHelper.Write("GameGarden.StealTheGarden", content, LogSeverity.Warn);
                                        _canstealfruit = GetStealFeedback(content);
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
                                LogHelper.Write("GameGarden.StealTheGarden", GetFriendNameById(fuid), ex, LogSeverity.Error);
                                continue;
                            }
                        }
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
                LogHelper.Write("GameGarden.StealTheGarden", GetFriendNameById(fuid), ex, LogSeverity.Error);
                SetMessage(" 偷" + GetFriendNameById(fuid) + "的花园失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetStealFeedback
        private bool GetStealFeedback(string content)
        {
            try
            {
                //<data><anti>0</anti><ret>fail</ret><reason>今天偷果实的次数已用完，不能再偷任何人的果实</reason></data>
                //<data><ret>fail</ret><reason>正在麻醉中，不能偷窃。</reason></data>
                //<data><ret>fail</ret><reason>手下留情，该作物最后一个不能偷</reason></data>
                //<data><ret>fail</ret><reason>已偷过，做人要厚道</reason></data>
                //<data><ret>fail</ret><reason>该土地上的果实已经被偷光了！</reason></data>
                //<data><ret>fail</ret><reason>今天不能再偷了</reason></data>
                //<data><tips>这块地是你与共种的，刚才收获的果实你们两1人1半</tips><ret>fail</ret><reason>你不是该土地的主人，不能收获</reason></data>
                //<data><leftnum>4</leftnum><stealnum>1</stealnum><num>1</num><seedname>胡萝卜</seedname><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/huluobo.swf</fruitpic><ret>succ</ret></data>胡萝卜，数量：1，剩余：4 成功！            
                //<data><anti>1</anti><ret>succ</ret></data>
                //<data><anti>0</anti><leftnum>2</leftnum><stealnum>2</stealnum><num>2</num><seedname>冬虫夏草</seedname><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop2/dongchongxiacao.swf</fruitpic><ret>succ</ret></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "").Replace("这块地是你与共种的，刚才收获的果实你们两1人1半", "").Replace("这块地是你与共种的，刚才收获的果实你们俩1人1半", "") + " 偷窃失败！");
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    if (content.IndexOf("<anti>1</anti><ret>succ</ret></data>") > -1)
                    {
                        //<data><seedid>63</seedid><anti>1</anti><ret>succ</ret></data>
                        SetMessage("需要答题" + " 偷窃失败！");
                        LogHelper.Write("GetStealFeedback", CurrentAccount.UserName + "需要答题，才能偷东西", LogSeverity.Warn);
                        _needanswer = true;
                    }
                    else if (content.IndexOf("caretips") > -1 || content.IndexOf("caretips2") > -1)
                    {
                        //菜老伯
                        //<data>
                        //  <caretips>被我逮到了吧，下次别再偷了，小心被我送你去派出所！</caretips>
                        //  <caretips2>你偷果实被菜老伯抓住，魅力值减少30</caretips2>
                        //  <ret>succ</ret>
                        //</data>
                        SetMessage("你偷果实被菜老伯抓住，魅力值减少30" + " 偷窃失败！");
                        return false;
                    }
                    else
                    {
                        if (content.IndexOf("leftnum") > -1)
                        {
                            StealInfo objSteal = ConfigCtrl.ConvertToStealObject(content);
                            if (objSteal == null)
                            {
                                SetMessage(content + " 偷窃失败！");
                            }
                            else
                                SetMessage(objSteal.SeedName + "，偷窃数量：" + objSteal.StealNum + "，剩余：" + objSteal.LeftNum + " 偷窃成功！");
                        }
                    }
                }
                else
                {
                    SetMessage(content);
                }
                if (content.IndexOf("今天不能再偷了") > -1)
                    return false;
                else if (content.IndexOf("正在麻醉中，不能偷窃") > -1)
                    return false;
                else if (content.Contains("正在麻醉中，不能采摘"))
                    return false;
                else if (content.IndexOf("今天偷果实的次数已用完，不能再偷任何人的果实") > -1)
                    return false;
                else if (content.IndexOf("今天采摘果实的次数已用完，不能再采摘任何人的果实") > -1)
                    return false;
                else
                    return true;
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
                LogHelper.Write("GameGarden.GetStealFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region HelpOthersGardens
        private void HelpOthersGardens()
        {
            try
            {
                int num = 0;

                if (Task.FarmShared)
                {
                    //播种爱心地块
                    SetMessageLn("开始播种爱心地块：");
                }
                else
                {
                    SetMessageLn("开始去好友的花园帮忙：");
                }

                Collection<int> hasBeenFarmedList = new Collection<int>();

                if (Task.FarmShared)
                {
                    //先去白名单中的花园播种
                    SetMessageLn("开始去白名单中好友的花园播种：");
                    foreach (int uid in Operation.FarmWhiteList)
                    {
                        try
                        {
                            if (_outofmoney)
                                break;

                            SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");

                            if (Operation.FarmBlackList.Contains(uid))
                            {
                                SetMessage(base.GetFriendNameById(uid) + "在播种黑名单中，跳过");
                                continue;
                            }
                            if (this._hasNothingTobeFarmedList.Contains(uid))
                            {
                                SetMessage(base.GetFriendNameById(uid) + "的花园里没有可播种的爱心地块，跳过");
                                continue;
                            }
                            HelpTheGarden(uid.ToString(), Task.HelpOthers, true);
                            hasBeenFarmedList.Add(uid);
                            if (this._canfarmshared == false)
                            {
                                if (Task.FarmShared && !Task.HelpOthers)
                                    return;
                                else
                                    break;
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
                            LogHelper.Write("GameGarden.HelpOthersGardens", GetFriendNameById(uid), ex, LogSeverity.Error);
                            continue;
                        }
                    }
                }

                //其他好友
                if (Task.FarmShared && Operation.FarmAll)
                {
                    //再去我的爱心地块播种
                    num = 0;

                    SetMessageLn("开始去我已有的爱心地块播种：");
                    foreach (FriendInfo friend in this._myGardenFriendsList)
                    {
                        try
                        {
                            if (_outofmoney)
                                break;
                            if (!friend.GardenShare)
                                continue;
                            if (hasBeenFarmedList.Contains(friend.Id))
                                continue;

                            SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));

                            if (Operation.FarmBlackList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "在播种黑名单中，跳过");
                                continue;
                            }
                            if (this._hasNothingTobeFarmedList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "的花园里没有可播种的爱心地块，跳过");
                                continue;
                            }
                            HelpTheGarden(friend.Id.ToString(), Task.HelpOthers, true);
                            hasBeenFarmedList.Add(friend.Id);
                            if (this._canfarmshared == false)
                            {
                                if (Task.FarmShared && !Task.HelpOthers)
                                    return;
                                else
                                    break;
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
                            LogHelper.Write("GameGarden.HelpOthersGardens", friend.Name, ex, LogSeverity.Error);
                            continue;
                        }
                    }
                }

                if (Task.FarmShared && Operation.FarmAll || Task.HelpOthers)
                {
                    //其他好友
                    num = 0;
                    if (Task.FarmShared && !Task.HelpOthers)
                        SetMessageLn("开始去其他好友的花园播种：");
                    else
                        SetMessageLn("开始去其他好友的花园帮忙：");
                    foreach (FriendInfo friend in this._allGardenFriendsList)
                    {
                        try
                        {
                            if (Task.FarmShared && !Task.HelpOthers && _outofmoney)
                                break;

                            if (hasBeenFarmedList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                                continue;

                            SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                            if ((!Task.HelpOthers || !IsNeedHelp(friend.Id)) && Operation.FarmBlackList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "在播种黑名单中，跳过");
                                continue;
                            }
                            if (this._hasNothingTobeFarmedList.Contains(friend.Id))
                            {
                                if (Task.FarmShared && !Task.HelpOthers)
                                    SetMessage(friend.Name + "的花园里没有可播种的爱心地块，跳过");
                                else
                                    SetMessage(friend.Name + "的花园里没有什么可帮忙的，跳过");
                                continue;
                            }
                            HelpTheGarden(friend.Id.ToString(), Task.HelpOthers, this._canfarmshared && Task.FarmShared && Operation.FarmAll && !Operation.FarmBlackList.Contains(friend.Id));
                            if (this._canfarmshared == false)
                            {
                                if (Task.FarmShared && !Task.HelpOthers)
                                    return;
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
                            LogHelper.Write("GameGarden.HelpOthersGardens", friend.Name, ex, LogSeverity.Error);
                            continue;
                        }
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
                LogHelper.Write("GameGarden.HelpOthersGardens", ex, LogSeverity.Error);
            }
        }
        #endregion

        #region HelpTheGarden
        private void HelpTheGarden(string fuid, bool helpothers, bool farmshared)
        {
            try
            {
                string content = RequestGarden(fuid);
                string verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");

                GardenInfo garden = ReadGarden(verifyCode, fuid);
                if (garden == null)
                {
                    SetMessage("无法读取花园信息，跳过");
                }
                else if (garden != null && garden.Plots != null)
                {
                    bool canfarm = false;
                    _outofmoney = false;
                    foreach (PlotInfo plot in garden.Plots)
                    {
                        if (plot.Status == 1)
                        {
                            try
                            {
                                //摇钱树
                                if (plot.SeedId == 102 && plot.Crops.IndexOf("点击可摇钱") > -1)
                                {
                                    SetMessageLn(string.Format("=>第{0}个地块：", plot.FarmNum));
                                    //<crops>生长阶段：85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;再过18小时26分成熟（不可摇钱）&lt;/font&gt;&lt;br&gt;距离收获：18小时26分&lt;font size='12' color='#666666'&gt;，点击可摇钱&lt;/font&gt;</crops>
                                    //<crops>生长阶段：85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;再过18小时14分成熟（不可摇钱）&lt;/font&gt;&lt;br&gt;距离收获：18小时14分</crops>
                                    //http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid=0&verify=6194153%5F1062%5F6194153%5F1253607483%5F14f6afef57593e63f22fda3adc9a5685
                                    SetMessage("可以摇钱：");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid={0}&verify={1}", fuid, this._verifyCode));
                                    GetYaoQianFeedback(content);                                    
                                }

                                if (plot.Shared == 0 && !helpothers)
                                    continue;
                                if ((plot.Shared == 1 || plot.Shared == 2) && !farmshared)
                                    continue;

                                if (plot.Water != 5 ||
                                    plot.Vermin == 1 ||
                                    plot.Grass == 1 ||
                                    (plot.CropsStatus == 2 && plot.Fuid.ToString() == CurrentAccount.UserId) ||
                                    (plot.Shared != 0 &&
                                        (plot.CropsStatus == 3 && String.IsNullOrEmpty(plot.Crops) && String.IsNullOrEmpty(plot.Farm) || plot.CropsStatus == -1)) ||
                                    plot.CropsId == 0 && plot.Shared != 0)
                                {
                                    if (plot.Shared == 0)
                                        SetMessageLn(string.Format("=>第{0}个地块：", plot.FarmNum));
                                    else
                                        SetMessageLn(string.Format("=>第{0}个爱心地块：", plot.FarmNum));
                                    canfarm = true;
                                }                               

                                if (plot.Water != 5)
                                {
                                    //浇水
                                    SetMessage("需要浇水：");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Post("http://www.kaixin001.com/house/garden/water.php", string.Format("fuid={0}&farmnum={1}&verify={2}&seedid=0&r=0%2E6590517126023769", fuid, plot.FarmNum, this._verifyCode));
                                    GetFarmFeedback(content);
                                }

                                if (plot.Vermin == 1)
                                {
                                    //捉虫
                                    SetMessage("需要捉虫：");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antivermin.php?verify={0}&seedid=0&r=0%2E3779320823960006&fuid={1}&farmnum={2}", this._verifyCode, fuid, plot.FarmNum));
                                    GetFarmFeedback(content);
                                }

                                if (plot.Grass == 1)
                                {
                                    //除草
                                    SetMessage("需要除草：");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antigrass.php?farmnum={0}&verify={1}&seedid=0&r=0%2E8164945561438799&fuid={2}", plot.FarmNum, this._verifyCode, fuid));
                                    GetFarmFeedback(content);
                                }

                                if (plot.CropsStatus == 2 && plot.Fuid.ToString() == CurrentAccount.UserId)
                                {
                                    //收获
                                    SetMessage("可以收获：");
                                    if (Task.HarvestFruit)
                                    {
                                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                                        content = HH.Post("http://www.kaixin001.com/house/garden/havest.php", string.Format("fuid={0}&farmnum={1}&verify={2}&seedid=0&r=0%2E44418928399682045", fuid, plot.FarmNum, this._verifyCode));
                                        if (GetFarmFeedback(content))
                                        {
                                            plot.CropsStatus = 3;
                                            plot.Crops = null;
                                            plot.Farm = null;
                                            //同步列表
                                            //RemoveSharedFriendById(fuid);
                                        }
                                    }
                                }

                                if (plot.Shared != 0 &&
                                    (plot.CropsStatus == 3 && String.IsNullOrEmpty(plot.Crops) && String.IsNullOrEmpty(plot.Farm) ||
                                    plot.CropsStatus == -1))
                                {
                                    //犁地
                                    SetMessage("需要犁地：");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/plough.php?verify={0}&seedid=0&r=0%2E018698612228035927&fuid={1}&farmnum={2}", this._verifyCode, fuid, plot.FarmNum));
                                    if (GetFarmFeedback(content))
                                        plot.CropsId = 0;
                                }

                                if (plot.CropsId == 0 && plot.Shared != 0 && this._canfarmshared)
                                {
                                    //播种
                                    SetMessage("可以播种：");
                                    bool issowed = false;
                                    if (Task.SowMySeedsFirst && _myseedsList != null && _myseedsList.Count != 0)
                                    {
                                        foreach (SeedInfo myseed in _myseedsList)
                                        {
                                            if (myseed.SeedId == 21 || myseed.SeedId == 102 || myseed.Num < 1 || myseed.Valid == false)
                                                continue;

                                            if (SowSharedPlot(fuid, myseed, plot, ref content))
                                            {
                                                issowed = true;
                                                break;
                                            }
                                            else
                                            {
                                                //你目前的级别，最多同时可种2块好友的地
                                                Regex regular = new Regex(@"你目前的级别，最多同时可种[\d]+块好友的地");
                                                if (regular.IsMatch(content))
                                                {
                                                    this._canfarmshared = false;
                                                    SetMessageLn("以你目前的级别，无法再耕种更多好友的地了，停止播种爱心地块！");
                                                    if (!helpothers && farmshared)
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    if (issowed)
                                        continue;
                                    if (this._canfarmshared == false && !helpothers && farmshared)
                                        break;

                                    SeedInfo seed = GetSharedFarmSeed(garden, fuid);
                                    if (seed != null)
                                    {
                                        if (SowSharedPlot(fuid, seed, plot, ref content))
                                        {
                                        }
                                        else
                                        {
                                            //你目前的级别，最多同时可种2块好友的地
                                            Regex regular = new Regex(@"你目前的级别，最多同时可种[\d]+块好友的地");
                                            if (regular.IsMatch(content))
                                            {
                                                this._canfarmshared = false;
                                                SetMessageLn("以你目前的级别，无法再耕种更多好友的地了，停止播种爱心地块！");
                                                if (!helpothers && farmshared)
                                                    break;
                                            }
                                            //尝试播种爱情果...该种子最多同时可种1块地 失败！
                                            Regex regular2 = new Regex(@"该种子最多同时可种[\d]块地");
                                            if (regular2.IsMatch(content))
                                            {
                                                seed.Valid = false;
                                            }

                                        }
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
                                LogHelper.Write("GameGarden.HelpTheGarden", ex, LogSeverity.Error);
                                SetMessage(ex.Message);
                            }
                        }
                    }
                    if (!canfarm)
                        this._hasNothingTobeFarmedList.Add(DataConvert.GetInt32(fuid));
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
                LogHelper.Write("GameGarden.HelpTheGarden", ex, LogSeverity.Error);
                SetMessage(" 去" + GetFriendNameById(fuid) + "的花园帮忙失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region SowSharedPlot
        private bool SowSharedPlot(string fuid, SeedInfo seed, PlotInfo plot, ref string content)
        {
            SetMessage(" 尝试播种" + seed.Name + "...");
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/farmseed.php?verify={0}&seedid={1}&r=0%2E1429477329365909&fuid={2}&farmnum={3}", this._verifyCode, seed.SeedId, fuid, plot.FarmNum));
            if (GetFarmFeedback(content))
            {
                seed.Num--;
                return true;
            }
            else
            {
                if (content.Contains("该种子最多同时可种"))
                    seed.Valid = false;
                return false;
            }
        }
        #endregion

        #region GetSharedFarmSeed
        private SeedInfo GetSharedFarmSeed(GardenInfo garden, string fuid)
        {
            int seedid = 0;
            string seedname = "";
            SeedInfo seed = null;
            //if (Task.SowMySeedsFirst && _myseedsList != null && _myseedsList.Count != 0)
            //{
            //    foreach (SeedInfo seedunit in _myseedsList)
            //    {
            //        //无效，比如：该种子最多同时可种1块地
            //        if (!seedunit.Valid)
            //            continue;
            //        //人参不能种在爱心地块
            //        if (seedunit.SeedId == 21)
            //            continue;
            //        //曼珠沙华不能种在爱心地块
            //        if (seedunit.SeedId == 104)
            //            continue;
            //        //摇钱树不能种在爱心地里
            //        if (seedunit.SeedId == 102)
            //            continue;
            //        //曼陀罗不能种在爱心地里
            //        if (seedunit.SeedId == 114)
            //            continue;
            //        //翡翠曼陀罗不能种在爱心地里
            //        if (seedunit.SeedId == 116)
            //            continue;
            //        //种完了，没有种子了
            //        if (seedunit.Num < 1)
            //            continue;
            //        //爱情果
            //        if (seedunit.SeedId == 39)
            //        {
            //            //尝试播种爱情果...该种子只能种在异性的爱心地 失败！
            //            FriendInfo friend = GetFriendById(fuid);
            //            if (friend != null)
            //            {
            //                if (!(CurrentAccount.Gender ^ friend.Gender))
            //                    continue;
            //            }
            //        }
            //        else
            //        {
            //            seed = seedunit;
            //            break;
            //        }
            //    }
            //}
            if (seed == null)
            {
                if (Task.ExpensiveFarmShared)
                {
                    RankSeedInfo rankseed = GetSharedSeedByRank(this._myRank, fuid);
                    if (rankseed != null)
                    {
                        seedid = rankseed.SeedId;
                        seedname = rankseed.Name;
                        SetMessage(" 你可以播种的最贵的种子：" + rankseed.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetSharedFarmSeed", " 无法取得等级" + this._myRank + "的种子", LogSeverity.Warn);
                        SetMessage(" 无法取得等级" + this._myRank + "的种子");
                        return null;
                    }
                }
                else
                {
                    SeedInfo seed1 = GetSeedById(Task.CustomFarmShared);
                    if (seed1 != null)
                    {
                        seedid = seed1.SeedId;
                        seedname = seed1.Name;
                        SetMessage(" 你设定的播种种子：" + seed1.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetSharedFarmSeed", " 无法取得自定义的种子：" + Task.CustomFarmShared.ToString(), LogSeverity.Warn);
                        SetMessage(" 无法取得自定义的种子：" + Task.CustomFarmShared.ToString());
                        return null;
                    }
                }
                
                seed = GetFarmSeedById(seedid);
                if (seed == null)
                {
                    SetMessage("没有" + seedname + "的种子");
                    if (_outofmoney)
                    {
                        SetMessage(" 现金不够，不能购买");
                        return null;
                    }
                    if (Task.BuySeed)
                    {
                        if (BuySeed(seedid))
                        {
                            ReadMySeeds(garden);
                            seed = GetFarmSeedById(seedid);
                        }
                    }
                }
            }

            return seed;
        }
        #endregion

        #region GetSharedSeedByRank
        private RankSeedInfo GetSharedSeedByRank(int rank, string fuid)
        {
            if (rank > 40)
                rank = 40;

            //人参不能种在爱心地块里，则返回冬虫夏草
            if (rank >= 15 && rank <= 19)
                rank = 14;

            //曼珠沙华不能种在爱心地块里，则返回藏红花
            if (rank == 30)
                rank = 29;

            //曼陀罗不能种在爱心地块里，则返回何首乌
            if (rank >= 32 && rank <= 39)
                rank = 31;

            foreach (RankSeedInfo rankseed in _rankSeedsList)
            {
                //人参不能种在爱心地块里
                if (rankseed.SeedId == 21)
                    continue;
                //曼珠沙华不能种在爱心地块里
                if (rankseed.SeedId == 104)
                    continue;
                //曼陀罗不能种在爱心地块里
                if (rankseed.SeedId == 114)
                    continue;

                //爱情果
                if (rankseed.SeedId == 39)
                {
                    //尝试播种爱情果...该种子只能种在异性的爱心地 失败！
                    FriendInfo friend = GetFriendById(fuid);
                    if (friend != null)
                    {
                        if (!(CurrentAccount.Gender ^ friend.Gender))
                            continue;
                    }
                }
                if (rankseed.Rank == rank)
                    return rankseed;
            }
            return null;
        }
        #endregion
        
        #region PresentFruit
        private void PresentFruit()
        {
            try
            {
                SetMessageLn("开始赠送果实...");
                if (Operation.PresentId == 0)
                {
                    SetMessage("没有设定赠送的对象，无法赠送");
                    return;
                }
                if (!IsAlreadyMyFriend(DataConvert.GetString(Operation.PresentId)))
                {
                    SetMessage(DataConvert.GetString(Operation.PresentId) + "不是你的好友，无法赠送");
                    return;
                }
                    
                string content = RequestMyWarehouse();
                Collection<FruitInfo> fruits = ConfigCtrl.GetMyGardenWarehouse(content);
                if (fruits == null || fruits.Count == 0)
                {
                    SetMessage("仓库里没有任何果实");
                    return;
                }

                _incorrentcount = false;

                if (Task.PresentFruitByPrice)
                {
                    //计算价值最高
                    SetMessageLn("仓库里可以送的果实：");

                    int num = 0;
                    Collection<PresentInfo> presents = new Collection<PresentInfo>();
                    foreach (FruitInfo myfruit in fruits)
                    {
                        //牧草
                        if (myfruit.FruitId == 63)
                            continue;

                        FruitInfo fruit = GetFruitById(myfruit.FruitId);
                        if (fruit == null)
                        {
                            SetMessage(string.Format("未知果实{0}-{1}，跳过 ", myfruit.FruitId, myfruit.Name));
                            LogHelper.Write("GameGarden.PresentFruit" + CurrentAccount.UserName, string.Format("未知果实{0}-{1}，跳过 ", myfruit.FruitId, myfruit.Name), LogSeverity.Warn);
                            continue;
                        }
                        PresentInfo present = new PresentInfo();
                        present.SeedId = myfruit.FruitId;
                        present.Name = myfruit.Name;
                        present.SelfNum = myfruit.Num;
                        present.FruitPrice = fruit.SellPrice;
                        presents.Add(present);
                        SetMessageLn(string.Format("#{0}{1}：数量：{2}，单个售价：{3}，总价值：{4}", ++num, present.Name, present.SelfNum, present.FruitPrice, present.SellSum));
                    }

                    if (presents.Count == 0)
                    {
                        SetMessage(" 没有可送的果实");
                        return;
                    }

                    for (int ix = 0; ix < presents.Count; ix++)
                    {
                        for (int iy = ix + 1; iy < presents.Count; iy++)
                        {
                            if (presents[ix].SellSum > presents[iy].SellSum)
                            {
                                PresentInfo temp = presents[ix];
                                presents[ix] = presents[iy];
                                presents[iy] = temp;
                            }
                        }
                    }

                    PresentInfo presentMax = presents[presents.Count - 1];

                    SetMessageLn(string.Format("计算出可以送的价值最高的果实：{0}:{1}*{2}={3}元", presentMax.Name, presentMax.SelfNum, presentMax.FruitPrice, presentMax.SellSum));
                    if (Task.PresentFruitCheckValue && presentMax.SellSum < Task.PresentFruitValue * 10000)
                    {
                        SetMessage(string.Format(" 总价值{0}小于最低赠送价值{1}，跳过", presentMax.SellSum, Task.PresentFruitValue * 10000));
                        return;
                    }

                    SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentId), presentMax.SelfNum, presentMax.Name, presentMax.FruitPrice, presentMax.SellSum));
                    content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", presentMax.SeedId, Operation.PresentId, presentMax.SelfNum, DataConvert.GetEncodeData("送你果实啦！"), this._verifyCode));
                    if (GetPresentFeedback(content))
                        return;

                    if (_incorrentcount)
                    {
                        //http://www.kaixin001.com/house/garden/myfruitinfo.php?seedid=30&verify=6209015_1062_6209015_1238501495_1be9db343ea4e4094275e8fc3a17480b&r=0.5750930430367589
                        content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/myfruitinfo.php?seedid={0}&verify={1}&r=0.4879359360784292", presentMax.SeedId, this._verifyCode));
                        PresentInfo present = ConfigCtrl.ConvertToPresentObject(content);
                        if (present != null && present.SelfNum > 0)
                        {
                            if (Task.PresentFruitCheckValue && present.SelfNum * present.FruitPrice < Task.PresentFruitValue * 10000)
                            {
                                SetMessage(string.Format(" 总价值{0}小于最低赠送价值{1}，跳过", present.SelfNum * present.FruitPrice, Task.PresentFruitValue * 10000));
                                return;
                            }
                            SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentId), present.SelfNum, present.Name, present.FruitPrice, present.SellSum));
                            content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", presentMax.SeedId, Operation.PresentId, present.SelfNum, DataConvert.GetEncodeData("送你果实啦！"), this._verifyCode));
                            GetPresentFeedback(content);
                            return;
                        }
                    }
                }
                else
                {
                    SetMessageLn(string.Format("尝试赠送指定的果实：{0}...", GetFruitNameById(Task.PresentFruitId)));
                    foreach (FruitInfo myfruit in fruits)
                    {
                        //牧草
                        if (myfruit.FruitId == 63)
                            continue;
                        if (myfruit.FruitId == Task.PresentFruitId)
                        {
                            if (Task.PresentFruitCheckNum && myfruit.Num < Task.PresentFruitNum)
                            {
                                SetMessage(string.Format("数量{0}< 最小赠送数{1}，跳过 ", myfruit.Num, Task.PresentFruitNum));
                                return;
                            }
                            FruitInfo fruit = GetFruitById(myfruit.FruitId);
                            if (fruit == null)
                            {
                                SetMessage(string.Format("未知果实{0}-{1}，跳过 ", myfruit.FruitId, myfruit.Name));
                                LogHelper.Write("GameGarden.PresentFruit" + CurrentAccount.UserName, string.Format("未知果实{0}-{1}，跳过 ", myfruit.FruitId, myfruit.Name), LogSeverity.Warn);
                                return;
                            }
                            SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentId), myfruit.Num, myfruit.Name, fruit.SellPrice, fruit.SellPrice * myfruit.Num));
                            content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", Task.PresentFruitId, Operation.PresentId, myfruit.Num, DataConvert.GetEncodeData("送你果实啦！"), this._verifyCode));
                            if (GetPresentFeedback(content))
                                return;
                            if (content.Contains("请明天再送"))
                                return;

                            if (_incorrentcount)
                            {
                                //http://www.kaixin001.com/house/garden/myfruitinfo.php?seedid=30&verify=6209015_1062_6209015_1238501495_1be9db343ea4e4094275e8fc3a17480b&r=0.5750930430367589
                                content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/myfruitinfo.php?seedid={0}&verify={1}&r=0.4879359360784292", myfruit.FruitId, this._verifyCode));
                                PresentInfo present = ConfigCtrl.ConvertToPresentObject(content);
                                if (present != null && present.SelfNum > 0)
                                {
                                    if (Task.PresentFruitCheckNum && present.SelfNum < Task.PresentFruitNum)
                                    {
                                        SetMessage(string.Format("可送数量{0}< 最小赠送数{1}，跳过 ", present.SelfNum, Task.PresentFruitNum));
                                        return;
                                    }
                                    SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentId), present.SelfNum, present.Name, present.FruitPrice, present.SellSum));
                                    content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", Task.PresentFruitId, Operation.PresentId, present.SelfNum, DataConvert.GetEncodeData("送你果实啦！"), this._verifyCode));
                                    GetPresentFeedback(content);
                                    return;
                                }
                            }
                        }
                    }
                    SetMessage("仓库中没有该种果实，无法赠送。");
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
                LogHelper.Write("GameGarden.PresentFruit", GetFriendNameById(Operation.PresentId), ex, LogSeverity.Error);
                SetMessage(" 向" + GetFriendNameById(Operation.PresentId) + "赠送果实失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetPresentFeedback
        private bool GetPresentFeedback(string content)
        {
            try
            {
                //<data><ret>fail</ret><reason>一天只能给同一个好友送一次果实</reason></data>
                //<data><ret>succ</ret></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 赠送失败！");
                    if (content.Contains("数量不正确"))
                        _incorrentcount = true;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage("赠送成功！");
                    return true;
                }
                else
                {
                    SetMessage(content);
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
                LogHelper.Write("GameGarden.GetPresentFeedback", content, ex, LogSeverity.Error);
            }
            return false;
        }
        #endregion

        #region SellFruit
        private void SellFruit(GardenInfo garden)
        {
            try
            {
                SetMessageLn("开始出售果实...");
                if (Task.LowCash)
                {
                    if (garden.Cash > Task.LowCashLimit * 10000)
                    {
                        SetMessageLn(string.Format("还有{0}元现金，无须出售。", garden.Cash));
                        return;
                    }
                }

                if (Task.SellAllFruit)
                {
                    //http://www.kaixin001.com/house/garden/sellfruit.php?seedid=7&num=3&all=0&verify=8125598%5F1062%5F8125598%5F1239430307%5Fc9ffe05915837c4411a43fc9a3fdd3a6
                    //http://www.kaixin001.com/house/garden/sellfruit.php?seedid=4&num=3&all=1&verify=8125598%5F1062%5F8125598%5F1239430307%5Fc9ffe05915837c4411a43fc9a3fdd3a6
                    string content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/sellfruit.php?seedid=4&num=3&all=1&verify={0}", this._verifyCode));
                    long fruitvalue = 0;
                    GetSellFeedback(content, ref fruitvalue);
                }
                else
                {
                    string content = RequestMyWarehouse();
                    Collection<FruitInfo> fruits = ConfigCtrl.GetMyGardenWarehouse(content);
                    if (fruits == null || fruits.Count == 0)
                    {
                        SetMessage("仓库里没有任何果实");
                        return;
                    }

                    //计算价值
                    long soldvalue = 0;
                    long fruitvalue = 0;
                    foreach (FruitInfo myfruit in fruits)
                    {
                        if (Task.SellForbiddennFruitsList.Contains(myfruit.FruitId))
                        {
                            SetMessageLn(string.Format("{0}在出售的禁止列表中，跳过", myfruit.Name));
                            continue;
                        }
                        fruitvalue = 0;
                        if (soldvalue >= Task.MaxSellLimit * 10000)
                        {
                            SetMessageLn(string.Format("已出售的果实总价值已经超过{0}万，停止出售。", Task.MaxSellLimit));
                            break;
                        }

                        int seedprice = GetFruitSellPriceById(myfruit.FruitId);
                        if (seedprice <= 0)
                        {
                            SetMessageLn(string.Format("未知果实{0}-{1}，跳过", myfruit.Name));
                            continue;
                        }
                        double temp = (Task.MaxSellLimit * 10000 - soldvalue) / seedprice;
                        int sellnum = Math.Min(DataConvert.GetInt32(Math.Ceiling(temp)), myfruit.Num);
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/sellfruit.php?seedid={0}&num={1}&all=0&verify={2}", myfruit.FruitId, sellnum, this._verifyCode));
                        if (GetSellFeedback(content, ref fruitvalue))
                        {
                            soldvalue += fruitvalue;
                            SetMessage(string.Format("已出售的果实总价值：{0}元", soldvalue));
                        }
                        if (sellnum < myfruit.Num)
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
                LogHelper.Write("GameGarden.SellFruit", ex, LogSeverity.Error);
                SetMessage("出售果实失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetSellFeedback
        private bool GetSellFeedback(string content, ref long totalprice)
        {
            try
            {
                //<data><ret>succ</ret><goodsname></goodsname><totalprice>0</totalprice><num>0</num><pic></pic><all>1</all></data>
                //<data><ret>succ</ret><goodsname></goodsname><totalprice>60760</totalprice><num>611</num><pic></pic><all>1</all></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 出售失败！");                    
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SellInfo objSell = ConfigCtrl.ConvertToSellObject(content);
                    if (objSell == null)
                        SetMessage("操作发生异常，出售失败！");
                    else
                    {
                        if (objSell.All == 1)
                            SetMessageLn(string.Format("出售全部{0}个果实，共获得{1}元 出售成功！", objSell.Num, objSell.TotalPrice));
                        else
                            SetMessageLn(string.Format("出售{0}个{1}，共获得{2}元 出售成功！", objSell.Num, objSell.GoodsName, objSell.TotalPrice));
                        totalprice = objSell.TotalPrice;
                        return true;
                    }
                }
                else
                {
                    SetMessage(content);
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
                LogHelper.Write("GameGarden.GetSellFeedback", content, ex, LogSeverity.Error);
            }
            return false;
        }
        #endregion

        #region GetSeedNameById
        private string GetSeedNameById(int seedid)
        {
            foreach (SeedInfo seed in _seedsList)
            {
                if (seed.SeedId == seedid)
                {
                    return seed.Name;
                }
            }
            return seedid.ToString();
        }
        #endregion

        #region GetSeedById
        private SeedInfo GetSeedById(int seedid)
        {
            foreach (SeedInfo seed in _seedsList)
            {
                if (seed.SeedId == seedid)
                {
                    return seed;
                }
            }
            return null;
        }
        #endregion

        #region GetFruitById
        private FruitInfo GetFruitById(int fruitid)
        {
            foreach (FruitInfo fruit in _fruitsList)
            {
                if (fruit.FruitId == fruitid)
                {
                    return fruit;
                }
            }
            return null;
        }
        #endregion

        #region GetFruitSellPriceById
        private int GetFruitSellPriceById(int fruitid)
        {
            foreach (FruitInfo fruit in _fruitsList)
            {
                if (fruit.FruitId == fruitid)
                {
                    return fruit.SellPrice;
                }
            }
            return 0;
        }
        #endregion

        #region GetFruitNameById
        private string GetFruitNameById(int fruitid)
        {
            foreach (FruitInfo fruit in _fruitsList)
            {
                if (fruit.FruitId == fruitid)
                {
                    return fruit.Name;
                }
            }
            return fruitid.ToString();
        }
        #endregion

        #region GetHelpGardenFriendById
        private FriendInfo GetHelpGardenFriendById(int uid)
        {
            foreach (FriendInfo friend in _myGardenFriendsList)
            {
                if (friend.Id == uid)
                {
                    return friend;
                }
            }
            return null;
        }
        #endregion

        #region IsNeedHelp
        private bool IsNeedHelp(int uid)
        {
            foreach (FriendInfo friend in _myGardenFriendsList)
            {
                if (friend.Id == uid)
                    return true;
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
            return content;
        }

        public string RequestAllGardenFriends()
        {
            //花园的好友其实就是所有买房子的好友
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/house/mystay_dialog.php?verify=" + this._verifyCode);
        }

        public string RequestMatureFriends()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            //return HH.Post("http://www.kaixin001.com/house/garden/getfriendmature.php", "verify=" + this._verifyCode + "&_=");
            //return HH.Get(string.Format("http://www.kaixin001.com/house/garden/getfriendmature.php?verify={0}&r=0.8085891040973365", this._verifyCode));
            
            //http://www.kaixin001.com/!house/!garden/friendlist.php?r=0.3831629320047796
            //[{"uid":"10752908","real_name":"\u5173\u4ec1","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","share":1},{"uid":"10752309","real_name":"\u5b8b\u6c5f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/75\/23\/20_10752309_1.jpg","share":1},{"uid":10752657,"real_name":"\u6b66\u5c0f\u6d6a","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_0_0.gif","harvest":1},{"uid":10755959,"real_name":"\u5218\u6210\u540d","icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_1_0.gif","harvest":1}]
            return HH.Get("http://www.kaixin001.com/!house/!garden/friendlist.php?r=0.3831629320047796");
        }

        public string RequestGarden(string fuid)
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            //if (String.IsNullOrEmpty(fuid))
            //    return HH.Get("http://www.kaixin001.com/~house/garden/index.php");
            //else
            //    return HH.Get("http://www.kaixin001.com/~house/garden/index.php?fuid=" + fuid);     
            return HH.Get("http://www.kaixin001.com/app/app.php?aid=1062&url=garden/index.php&fuid=" + fuid);
        }

        public string RequestGardenConf(string verifyCode, string fuid)
        {
            //http://www.kaixin001.com/!house/!garden//getconf.php?verify=6209202_1062_6209202_1244600990_d8144605574ecee55dc293075a7f63ea&fuid=0&r=0.7394348406232893
            //http://www.kaixin001.com/house/garden/getconf.php?verify=6208965_1062_6208965_1236171981_248903adf57123277579cc56493f84c1&fuid=0&r=0.38592613535001874
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden//getconf.php?verify={0}&fuid={1}&r=0.7394348406232893", verifyCode, fuid));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, CurrentAccount.UserName);
            //}
            return content;
        }

        public string RequestMySeedList(int page)
        {
            //http://www.kaixin001.com/house/garden/getconf.php?verify=6208965_1062_6208965_1236171981_248903adf57123277579cc56493f84c1&fuid=0&r=0.38592613535001874
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/myseedlist.php?verify={0}&page={1}&r=0.3626677463762462", this._verifyCode, page));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "MySeedList");
            //}
            return content;
        }
        
        public string RequestSeedsList()
        {
            //http://www.kaixin001.com/house/garden/seedlist.php?verify=6208965_1062_6208965_1236173017_3097204521b882fac39c705582240625&r=0.8337999805808067
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/seedlist.php?verify={0}&r=0.8337999805808067", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "SeedsList");
            //}
            return content;
        }

        public string RequestMyWarehouse()
        {
            //http://www.kaixin001.com/house/garden/mygranary.php?verify=6209015_1062_6209015_1238500762_e5968394e2c2453ffa549a978f80945b&r=0.22738536354154348
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/mygranary.php?verify={0}&r=0.22738536354154348", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "RequestMyWarehouse");
            //}
            return content;
        }

        #endregion

        #region Properties
        public Collection<FriendInfo> AllGardenFriendsList
        {
            get { return this._allGardenFriendsList; }
        }

        public Collection<FriendInfo> MatureFriendsList
        {
            get { return this._myGardenFriendsList; }
        } 

        public Collection<SeedInfo> SeedsList
        {
            get { return _seedsList; }
            set { _seedsList = value; }
        }

        public Collection<RankSeedInfo> RankSeedsList
        {
            get { return _rankSeedsList; }
            set { _rankSeedsList = value; }
        }

        public Collection<FruitInfo> FruitsList
        {
            get { return _fruitsList; }
            set { _fruitsList = value; }
        }

        public Collection<int> HasNothingTobeFarmedList
        {
            get { return _hasNothingTobeFarmedList; }
            set { _hasNothingTobeFarmedList = value; }
        }
        #endregion
    }
}
