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
                SetMessageLn("���ڳ�ʼ��[��԰]...");

                string content = RequestHouseHomePage(true);

                //all garden friends
                content = RequestAllGardenFriends();
                ReadAllGardenFriends(content, false);
                SetMessage("[���л�԰�ĺ���]��Ϣ���سɹ���");

                //mature friends
                content = RequestMatureFriends();
                ReadMatureFriends(content, false);
                SetMessage("[��԰���г����ʵ�ĺ���]��Ϣ���سɹ���");
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
                SetMessage(" ��ʼ��[��԰]ʧ�ܣ�����" + ex.Message);
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
                SetMessageLn("ˢ��[���л�԰�ĺ���]...");

                if (!this.ValidationLogin(true))
                {
                    if (AllGardenFriendsFetched != null)
                        AllGardenFriendsFetched(_allGardenFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestAllGardenFriends();
                ReadAllGardenFriends(content, true);
                SetMessageLn("[���л�԰�ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (AllGardenFriendsFetched != null)
                    AllGardenFriendsFetched(_allGardenFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[���л�԰�ĺ���]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region ReadAllGardenFriends
        public void ReadAllGardenFriends(string content, bool printMessage)
        {
            int num;
            this._allGardenFriendsList.Clear();

            if (printMessage)
                SetMessageLn("��ȡ[���л�԰�ĺ���]��Ϣ:");
            //<div class="l" style="width:8em;"><a href="javascript:gotoUser(6194153);" class="sl">ׯ��</a></div>
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
                    SetMessageLn(this._allGardenFriendsList[ix].Name + "(" + this._allGardenFriendsList[ix].Id.ToString() + ")--" + (this._allGardenFriendsList[ix].Gender ? "��" : "Ů"));
                ix++;
                content2 = content2.Substring(num);
            }
            if (printMessage)
                SetMessageLn("��ɶ�ȡ��");
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
                SetMessageLn("ˢ��[��԰���г����ʵ�ĺ���]...");

                if (!this.ValidationLogin(true))
                {
                    if (MatureFriendsFetched != null)
                        MatureFriendsFetched(_myGardenFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestMatureFriends();
                ReadMatureFriends(content, true);
                SetMessageLn("[��԰���г����ʵ�ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (MatureFriendsFetched != null)
                    MatureFriendsFetched(_myGardenFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[��԰���г����ʵ�ĺ���]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region ReadMatureFriends
        public void ReadMatureFriends(string content, bool printMessage)
        {
            try
            {
                if (printMessage)
                    SetMessageLn("��ȡ[��԰���г����ʵ�ĺ���]��Ϣ...");

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
                SetMessage(" ��ȡ[��԰���г����ʵ�ĺ���]��Ϣʧ�ܣ�" + ex.Message);
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
                SetMessageLn("ˢ��[�̵��������б�]...");

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
                    SetMessageLn("[�̵��������б�]��Ϣˢ��ʧ�ܣ�");
                else
                    SetMessageLn("[�̵��������б�]��Ϣˢ�³ɹ���");

                //invoke event
                if (SeedsInShopFetched != null)
                    SeedsInShopFetched(seeds);
            });
            base.ExecuteTryCatchBlock(th, "[�̵��������б�]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region RunGarden
        public void RunGarden()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_GARDEN;

                SetMessageLn("��ʼ��԰...");

                //house
                string contentHome = RequestHouseHomePage(false);

                GardenInfo garden = ReadGarden(this._verifyCode, CurrentAccount.UserId);
                if (garden == null)
                {
                    SetMessageLn("�޷���ȡ�ҵĻ�԰��Ϣ��");
                    return;
                }

                ReadMySeeds(garden);

                if (_myseedsList.Count == 0)
                {
                    SetMessage("û�������ˣ�");
                }

                if (Task.FarmSelf)
                    FarmGarden(garden);

                string content = "";

                //͵�Թ�ʵ
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
                //���ְ��ĵؿ�
                if (Task.HelpOthers || Task.FarmShared)
                    HelpOthersGardens();

                //���͹�ʵ
                if (Task.PresentFruit)
                    PresentFruit();

                //���۹�ʵ
                if (Task.SellFruit)
                    SellFruit(garden);

                SetMessageLn("��԰��ɣ�");

            });
            base.ExecuteTryCatchBlock(th, "�����쳣����԰ʧ�ܣ�");
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
                SetMessageLn("��ȡ��԰��Ϣʧ�ܣ�");
            }
            else
            {
                if (CurrentAccount.UserId == fuid)
                    this._myRank = garden.Rank;

                SetMessageLn(garden.Name + "�Ļ�԰��" + garden.RankTip + " " + garden.CashTip + " ������" + garden.TCharms);

                if (garden.Plots != null)
                {
                    //plot.Shared = 0 ˽�˵ؿ�
                    //plot.Shared = 1 ���ĵؿ�
                    //plot.Shared = 2 ���ʱ��˵Ļ�԰ʱ���Լ��ĵؿ�

                    //int num = 0;
                    //foreach (PlotInfo plot in garden.Plots)
                    //{
                    //    SetMessageLn(string.Format("��{0}���ؿ飺", ++num));
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

                        SetMessageLn(string.Format("��{0}���ؿ飺", plot.FarmNum));
                        if (plot.Status == 1)
                        {
                            if (plot.Shared == 0)
                                SetMessage("˽�˵ؿ�");
                            else
                                SetMessage("���ĵؿ�");
                        }
                        else
                            SetMessage("��δ����");
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
                        //    SetMessage(" *ũ�" + plot.Farm);
                        //}
                        SetMessage(" *ˮ��" + plot.Water.ToString());
                    }
                }
            }

            return garden;
        }
        #endregion

        #region ReadMySeeds
        private void ReadMySeeds(GardenInfo garden)
        {
            SetMessageLn("�ҵ����ӣ�");
            string content = RequestMySeedList(1);
            int totalpage = 0;
            _myseedsList = ConfigCtrl.GetMySeeds(content, ref totalpage);

            if (_myseedsList == null)
            {
                SetMessage("�޷���ȡ�ҵ�������Ϣ��");
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
            //������
            SetMessageLn("��������...");
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/buyseed.php?verify={0}&seedid={1}&num={2}", this._verifyCode, seedid, Task.BuySeedCount));
            return GetBuySeedFeedback(content, GetSeedNameById(seedid), Task.BuySeedCount);
        }
        #endregion

        #region GetBuySeedFeedback
        private bool GetBuySeedFeedback(string content, string name, int count)
        {
            //<data><ret>fail</ret><msg>��������ʧ��&lt;br&gt;����ֽ𲻹������ܹ���</msg><err>1</err></data>
            //<data><ret>fail</ret><msg>�������ӳɹ�</msg><err>1</err></data>
            //<data><ret>succ</ret></data>
            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ʧ�ܣ�");
                //LogHelper.Write(CurrentAccount.UserName, content + " ʧ�ܣ�", LogSeverity.Warn);
                if (content.IndexOf("����ֽ𲻹������ܹ���") > -1)
                {
                    LogHelper.Write(CurrentAccount.UserName, JsonHelper.FiltrateHtmlTags(content).Replace("fail", "").Replace("1", "") + " ʧ�ܣ�", LogSeverity.Warn);
                    _outofmoney = true;
                }
                if (content.IndexOf("<data><ret>fail</ret><msg>�������ӳɹ�</msg><err>1</err></data>") > -1)
                    _outofmoney = true;
                return false;
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                SetMessage("����" + count.ToString() + "��" + name + "������ɣ�");
                return true;
            }
            else
            {
                LogHelper.Write(CurrentAccount.UserName, content + " ʧ�ܣ�", LogSeverity.Warn);
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
                SetMessageLn("��ʼ�ԼҸ���...");

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

                            if ((plot.SeedId == 102 && plot.Crops.IndexOf("�����ҡǮ") > -1) ||
                                plot.Water != 5 ||
                                plot.Vermin == 1 ||
                                plot.Grass == 1 ||
                                plot.CropsStatus == 2 ||
                                plot.Shared == 0 && (plot.CropsStatus == 3 && String.IsNullOrEmpty(plot.Crops) && String.IsNullOrEmpty(plot.Farm) || plot.CropsStatus == -1) ||
                                plot.CropsId == 0 && plot.Shared == 0)
                            {
                                if (plot.Shared == 0)
                                    SetMessageLn(string.Format("=>��{0}���ؿ飺", plot.FarmNum));
                                else
                                    SetMessageLn(string.Format("=>��{0}�����ĵؿ飺", plot.FarmNum));
                            }

                            //ҡǮ��
                            if (plot.SeedId == 102 && plot.Crops.IndexOf("�����ҡǮ") > -1)
                            {
                                //<crops>�����׶Σ�85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;�ٹ�18Сʱ26�ֳ��죨����ҡǮ��&lt;/font&gt;&lt;br&gt;�����ջ�18Сʱ26��&lt;font size='12' color='#666666'&gt;�������ҡǮ&lt;/font&gt;</crops>
                                //<crops>�����׶Σ�85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;�ٹ�18Сʱ14�ֳ��죨����ҡǮ��&lt;/font&gt;&lt;br&gt;�����ջ�18Сʱ14��</crops>
                                //http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid=0&verify=6194153%5F1062%5F6194153%5F1253607483%5F14f6afef57593e63f22fda3adc9a5685
                                SetMessage("����ҡǮ��");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid={0}&verify={1}", "0", this._verifyCode));
                                GetYaoQianFeedback(content);
                            }

                            if (plot.Water != 5)
                            {
                                //��ˮ
                                SetMessage("��Ҫ��ˮ��");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Post("http://www.kaixin001.com/house/garden/water.php", string.Format("fuid=0&farmnum={0}&verify={1}&seedid=0&r=0%2E6590517126023769", plot.FarmNum, this._verifyCode));
                                GetFarmFeedback(content);
                            }

                            if (plot.Vermin == 1)
                            {
                                //׽��
                                SetMessage("��Ҫ׽�棺");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antivermin.php?verify={0}&seedid=0&r=0%2E3779320823960006&fuid=0&farmnum={1}", this._verifyCode, plot.FarmNum));
                                GetFarmFeedback(content);
                            }

                            if (plot.Grass == 1)
                            {
                                //����
                                SetMessage("��Ҫ���ݣ�");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antigrass.php?farmnum={0}&verify={1}&seedid=0&r=0%2E8164945561438799&fuid=0", plot.FarmNum, this._verifyCode));
                                GetFarmFeedback(content);
                            }

                            if (plot.CropsStatus == 2)
                            {
                                //�ջ�
                                SetMessage("�����ջ�");
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

                                        //�˲�
                                        if (plot.SeedId == 21)
                                            garden.PanaxCount--;
                                        //�˲�(���˲�����)
                                        if (plot.SeedId == 25)
                                            garden.PanaxBabyCount--;
                                    }
                                }
                            }

                            if (plot.Shared == 0 &&
                                (plot.CropsStatus == 3 && String.IsNullOrEmpty(plot.Crops) && String.IsNullOrEmpty(plot.Farm) ||
                                plot.CropsStatus == -1))
                            {
                                //���
                                SetMessage("��Ҫ��أ�");
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/plough.php?verify={0}&seedid=0&r=0%2E018698612228035927&fuid=0&farmnum={1}", this._verifyCode, plot.FarmNum));
                                if (GetFarmFeedback(content))
                                    plot.CropsId = 0;
                            }

                            if (plot.CropsId == 0 && plot.Shared == 0)
                            {
                                //����
                                SetMessage("���Բ��֣�");
                                bool issowed = false;
                                if (Task.SowMySeedsFirst && _myseedsList != null && _myseedsList.Count != 0)
                                {
                                    foreach (SeedInfo myseed in _myseedsList)
                                    {
                                        //����������껨
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
                            LogHelper.Write("GameGarden.FarmGarden", "�ԼҸ���ʧ��", ex, LogSeverity.Error);
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
                SetMessage(" �ԼҸ���ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetYaoQianFeedback
        private void GetYaoQianFeedback(string content)
        {
            //<data>
            //  <tip>��ϲ�����Լ��ҵ�ҡǮ��ҡ����һ�ѽ��ӣ���ֵ20000Ԫ���ѳ�������˻�</tip>
            //  <ret>succ</ret>
            //  <swf>http://img.kaixin001.com.cn/i2/house/garden/yaoqianshu.swf</swf>
            //</data>
            //<data><ret>fail</ret><reason>�������ҡǮ�Ĵ���������4��,����������ҡ�ɣ�</reason></data>
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
            SetMessage(" ���Բ���" + seed.Name + "...");
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            string content = HH.Post("http://www.kaixin001.com/house/garden/farmseed.php", string.Format("fuid=0&farmnum={0}&verify={1}&seedid={2}&r=0%2E012194405309855938", plot.FarmNum, this._verifyCode, seed.SeedId));
            if (GetFarmFeedback(content))
            {
                //�˲�
                if (seed.SeedId == 21)
                    garden.PanaxCount++;
                //�˲�(���˲�����)
                if (seed.SeedId == 25)
                    garden.PanaxBabyCount++;
                seed.Num--;
                return true;
            }
            else
            {
                if (content.Contains("���������ͬʱ����"))
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

            //�Ѿ�����������˲Σ��򷵻ض����Ĳ�
            if (rank >= 15 && rank <= 19 && (panaxcount >= 2 || panaxbabycount >= 2))
                rank = 14;
            //�Ѿ���һ���������ɳ�����򷵻زغ컨
            if (rank == 30 && clowningcount >= 1)
                rank = 29;
            //�Ѿ���������������ޣ��򷵻غ�����
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
            //        //����������껨
            //        if (seedunit.SeedId == 39 || seedunit.SeedId == 61 || seedunit.Num < 1)
            //            continue;
            //        //�˲�
            //        if (seedunit.SeedId == 21)
            //        {
            //            //�˲����ͬʱ����2���
            //            if (garden.PanaxCount >= 2)
            //                continue;
            //        }
            //        //�˲�(���˲�����)
            //        else if (seedunit.SeedId == 25)
            //        {
            //            //�˲�(���˲�����)���ͬʱ����2���
            //            if (garden.PanaxBabyCount >= 2)
            //                continue;
            //        }
            //        //����ɳ��
            //        else if (seedunit.SeedId == 104)
            //        {
            //            //����ɳ�����ͬʱ����1���
            //            if (garden.ClowningCount >= 1)
            //                continue;
                    
            //        }
            //        //������
            //        else if (seedunit.SeedId == 114)
            //        {
            //            //���������ͬʱ����2���
            //            if (garden.StramoniumCount >= 2)
            //                continue;
            //        }
            //        //ҡǮ��
            //        else if (seedunit.SeedId == 102)
            //        {
            //            //���������ͬʱ����1���
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
                        SetMessage(" ����Բ��ֵ��������ӣ�" + rankseed.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetOwnFarmSeed", " �޷�ȡ�õȼ�" + garden.Rank + "������", LogSeverity.Warn);
                        SetMessage(" �޷�ȡ�õȼ�" + garden.Rank + "������");
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
                        SetMessage(" ���趨�Ĳ������ӣ�" + seed1.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetOwnFarmSeed", " �޷�ȡ���Զ�������ӣ�" + Task.CustomFarmSelf.ToString(), LogSeverity.Warn);
                        SetMessage(" �޷�ȡ���Զ�������ӣ�" + Task.CustomFarmSelf.ToString());
                        return null;
                        //return FarmStatus.Continue;
                    }
                }
                seed = GetFarmSeedById(seedid);
                if (seed == null)
                {
                    SetMessage(string.Format(" û��{0}������", seedname));
                    if (_outofmoney)
                    {
                        SetMessage(" �ֽ𲻹������ܹ��򣬲���ʧ��");
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
                //���������ͬʱ����2��� ʧ�ܣ�
                //<data><ret>succ</ret></data>
                //<data><ret>fail</ret><reason>��û�и�������</reason></data>
                //<data><tips>���������빲�ֵģ��ղ��ջ�Ĺ�ʵ������1��1��</tips><ret>fail</ret><reason>�㲻�Ǹ����ص����ˣ������ջ�</reason></data>
                //<data><leftnum>16</leftnum><stealnum>0</stealnum><num>16</num><seedname>����</seedname><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/tudou.swf</fruitpic><ret>succ</ret></data>
                //SetMessage(content);
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    if (content.IndexOf("���������ͬʱ����") > -1)
                        _feedlimited = true;

                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "").Replace("���������빲�ֵģ��ղ��ջ�Ĺ�ʵ������1��1��", "") + " ʧ�ܣ�");
                    return false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    if (content.IndexOf("leftnum") > -1)
                    {
                        //�ջ�
                        //StealInfo objSteal = ConfigCtrl.ConvertToStealObject(content);
                        //SetMessage(objSteal.SeedName + "��������" + objSteal.Num + "��ʣ�ࣺ" + objSteal.LeftNum + " ��ɣ�");
                        SetMessage(JsonHelper.GetMid(content, "<seedname>", "</seedname>") + "���ջ�������" + JsonHelper.GetMid(content, "<havestnum>", "</havestnum>") + " ��ɣ�");
                        return true;
                    }
                    else
                    {   //��ˮ//׽��//���//����
                        SetMessage("��ɣ�");
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

            SetMessageLn("��ʼ͵��ʵ��");
            //��͵�������е���
            SetMessageLn("��ʼ͵�������е��ˣ�");
            foreach (int uid in Operation.StealWhiteList)
            {
                try
                {
                    SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");
                    FriendInfo friend = GetHelpGardenFriendById(uid);
                    if (friend == null || friend.GardenHarvest == false)
                    {
                        SetMessage("ûʲô��͵�ģ�����");
                        continue;
                    }
                  
                    if (Operation.StealBlackList.Contains(uid))
                    {
                        SetMessage(base.GetFriendNameById(uid) + "�ں������У�����");
                        continue;
                    }
                    StealTheGarden(uid.ToString());
                    if (this._canstealfruit == false)
                    {
                        SetMessageLn("��������첻����͵�ˣ�ֹͣ͵��ʵ��");
                        return;
                    }
                    if (this._needanswer == true)
                    {
                        SetMessageLn("��Ҫ�ȴ������͵��ֹͣ͵��ʵ��");
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

            //͵ʣ�µ���
            if (Operation.StealAll)
            {
                num = 0;
                SetMessageLn("ȥ�����г����ʵ�Ļ�԰͵��");
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
                            SetMessage(friend.Name + "�ں������У�����");
                            continue;
                        }                        
                        StealTheGarden(friend.Id.ToString());
                        if (this._canstealfruit == false)
                        {
                            SetMessageLn("��������첻����͵�ˣ�ֹͣ͵��ʵ��");
                            return;
                        }
                        if (this._needanswer == true)
                        {
                            SetMessageLn("��Ҫ�ȴ������͵��ֹͣ͵��ʵ��");
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
                    SetMessageLn("�޷���ȡ��԰��Ϣ������");
                }
                else if (garden.HasMonitor)
                {
                    SetMessageLn("�в��ϲ�������");
                }
                else if (garden != null && garden.Plots != null)
                {
                    foreach (PlotInfo plot in garden.Plots)
                    {
                        if (!_canstealfruit)
                            break;
                        if (_needanswer)
                            break;

                        //ҡǮ��
                        if (plot.SeedId == 102 && plot.Crops.IndexOf("�����ҡǮ") > -1)
                        {

                            SetMessageLn(string.Format("=>��{0}���ؿ飺", plot.FarmNum));
                            //<crops>�����׶Σ�85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;�ٹ�18Сʱ26�ֳ��죨����ҡǮ��&lt;/font&gt;&lt;br&gt;�����ջ�18Сʱ26��&lt;font size='12' color='#666666'&gt;�������ҡǮ&lt;/font&gt;</crops>
                            //<crops>�����׶Σ�85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;�ٹ�18Сʱ14�ֳ��죨����ҡǮ��&lt;/font&gt;&lt;br&gt;�����ջ�18Сʱ14��</crops>
                            //http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid=0&verify=6194153%5F1062%5F6194153%5F1253607483%5F14f6afef57593e63f22fda3adc9a5685
                            SetMessage("����ҡǮ��");
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid={0}&verify={1}", fuid, this._verifyCode));
                            GetYaoQianFeedback(content);
                        }

                        //issue:����ж� ��͵��������Ҫ�����
                        if (plot.CropsStatus == 2 && plot.Shared == 0)
                        {
                            try
                            {
                                SetMessageLn(string.Format("=>��{0}���ؿ飺", plot.FarmNum));
                                SetMessage("�г���Ĺ�ʵ������͵�ԣ�");

                                //��͵��
                                if (plot.Crops.IndexOf("��͵��") > -1)
                                {
                                    SetMessage("��͵��");
                                    continue;
                                }
                                //�ٹ�1Сʱ48�ֿ�͵
                                //�ٹ�39�ֿ�͵
                                Regex regular = new Regex(@"�ٹ�[\s\S]+��͵");
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
                                        SetMessage(fruit.Name + "�ڽ�ֹ͵���б��У���͵");
                                        continue;
                                    }
                                    //if (seed.SellPrice < Task.StealPrice)
                                    //{
                                    //    SetMessage(seed.Name + "�ĳ��ۼ۸�" + seed.SellPrice + "С��" + Task.StealPrice + "����͵");
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
                SetMessage(" ͵" + GetFriendNameById(fuid) + "�Ļ�԰ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetStealFeedback
        private bool GetStealFeedback(string content)
        {
            try
            {
                //<data><anti>0</anti><ret>fail</ret><reason>����͵��ʵ�Ĵ��������꣬������͵�κ��˵Ĺ�ʵ</reason></data>
                //<data><ret>fail</ret><reason>���������У�����͵�ԡ�</reason></data>
                //<data><ret>fail</ret><reason>�������飬���������һ������͵</reason></data>
                //<data><ret>fail</ret><reason>��͵��������Ҫ���</reason></data>
                //<data><ret>fail</ret><reason>�������ϵĹ�ʵ�Ѿ���͵���ˣ�</reason></data>
                //<data><ret>fail</ret><reason>���첻����͵��</reason></data>
                //<data><tips>���������빲�ֵģ��ղ��ջ�Ĺ�ʵ������1��1��</tips><ret>fail</ret><reason>�㲻�Ǹ����ص����ˣ������ջ�</reason></data>
                //<data><leftnum>4</leftnum><stealnum>1</stealnum><num>1</num><seedname>���ܲ�</seedname><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/huluobo.swf</fruitpic><ret>succ</ret></data>���ܲ���������1��ʣ�ࣺ4 �ɹ���            
                //<data><anti>1</anti><ret>succ</ret></data>
                //<data><anti>0</anti><leftnum>2</leftnum><stealnum>2</stealnum><num>2</num><seedname>�����Ĳ�</seedname><fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop2/dongchongxiacao.swf</fruitpic><ret>succ</ret></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "").Replace("���������빲�ֵģ��ղ��ջ�Ĺ�ʵ������1��1��", "").Replace("���������빲�ֵģ��ղ��ջ�Ĺ�ʵ������1��1��", "") + " ͵��ʧ�ܣ�");
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    if (content.IndexOf("<anti>1</anti><ret>succ</ret></data>") > -1)
                    {
                        //<data><seedid>63</seedid><anti>1</anti><ret>succ</ret></data>
                        SetMessage("��Ҫ����" + " ͵��ʧ�ܣ�");
                        LogHelper.Write("GetStealFeedback", CurrentAccount.UserName + "��Ҫ���⣬����͵����", LogSeverity.Warn);
                        _needanswer = true;
                    }
                    else if (content.IndexOf("caretips") > -1 || content.IndexOf("caretips2") > -1)
                    {
                        //���ϲ�
                        //<data>
                        //  <caretips>���Ҵ����˰ɣ��´α���͵�ˣ�С�ı�������ȥ�ɳ�����</caretips>
                        //  <caretips2>��͵��ʵ�����ϲ�ץס������ֵ����30</caretips2>
                        //  <ret>succ</ret>
                        //</data>
                        SetMessage("��͵��ʵ�����ϲ�ץס������ֵ����30" + " ͵��ʧ�ܣ�");
                        return false;
                    }
                    else
                    {
                        if (content.IndexOf("leftnum") > -1)
                        {
                            StealInfo objSteal = ConfigCtrl.ConvertToStealObject(content);
                            if (objSteal == null)
                            {
                                SetMessage(content + " ͵��ʧ�ܣ�");
                            }
                            else
                                SetMessage(objSteal.SeedName + "��͵��������" + objSteal.StealNum + "��ʣ�ࣺ" + objSteal.LeftNum + " ͵�Գɹ���");
                        }
                    }
                }
                else
                {
                    SetMessage(content);
                }
                if (content.IndexOf("���첻����͵��") > -1)
                    return false;
                else if (content.IndexOf("���������У�����͵��") > -1)
                    return false;
                else if (content.Contains("���������У����ܲ�ժ"))
                    return false;
                else if (content.IndexOf("����͵��ʵ�Ĵ��������꣬������͵�κ��˵Ĺ�ʵ") > -1)
                    return false;
                else if (content.IndexOf("�����ժ��ʵ�Ĵ��������꣬�����ٲ�ժ�κ��˵Ĺ�ʵ") > -1)
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
                    //���ְ��ĵؿ�
                    SetMessageLn("��ʼ���ְ��ĵؿ飺");
                }
                else
                {
                    SetMessageLn("��ʼȥ���ѵĻ�԰��æ��");
                }

                Collection<int> hasBeenFarmedList = new Collection<int>();

                if (Task.FarmShared)
                {
                    //��ȥ�������еĻ�԰����
                    SetMessageLn("��ʼȥ�������к��ѵĻ�԰���֣�");
                    foreach (int uid in Operation.FarmWhiteList)
                    {
                        try
                        {
                            if (_outofmoney)
                                break;

                            SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");

                            if (Operation.FarmBlackList.Contains(uid))
                            {
                                SetMessage(base.GetFriendNameById(uid) + "�ڲ��ֺ������У�����");
                                continue;
                            }
                            if (this._hasNothingTobeFarmedList.Contains(uid))
                            {
                                SetMessage(base.GetFriendNameById(uid) + "�Ļ�԰��û�пɲ��ֵİ��ĵؿ飬����");
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

                //��������
                if (Task.FarmShared && Operation.FarmAll)
                {
                    //��ȥ�ҵİ��ĵؿ鲥��
                    num = 0;

                    SetMessageLn("��ʼȥ�����еİ��ĵؿ鲥�֣�");
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
                                SetMessage(friend.Name + "�ڲ��ֺ������У�����");
                                continue;
                            }
                            if (this._hasNothingTobeFarmedList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "�Ļ�԰��û�пɲ��ֵİ��ĵؿ飬����");
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
                    //��������
                    num = 0;
                    if (Task.FarmShared && !Task.HelpOthers)
                        SetMessageLn("��ʼȥ�������ѵĻ�԰���֣�");
                    else
                        SetMessageLn("��ʼȥ�������ѵĻ�԰��æ��");
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
                                SetMessage(friend.Name + "�ڲ��ֺ������У�����");
                                continue;
                            }
                            if (this._hasNothingTobeFarmedList.Contains(friend.Id))
                            {
                                if (Task.FarmShared && !Task.HelpOthers)
                                    SetMessage(friend.Name + "�Ļ�԰��û�пɲ��ֵİ��ĵؿ飬����");
                                else
                                    SetMessage(friend.Name + "�Ļ�԰��û��ʲô�ɰ�æ�ģ�����");
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
                    SetMessage("�޷���ȡ��԰��Ϣ������");
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
                                //ҡǮ��
                                if (plot.SeedId == 102 && plot.Crops.IndexOf("�����ҡǮ") > -1)
                                {
                                    SetMessageLn(string.Format("=>��{0}���ؿ飺", plot.FarmNum));
                                    //<crops>�����׶Σ�85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;�ٹ�18Сʱ26�ֳ��죨����ҡǮ��&lt;/font&gt;&lt;br&gt;�����ջ�18Сʱ26��&lt;font size='12' color='#666666'&gt;�������ҡǮ&lt;/font&gt;</crops>
                                    //<crops>�����׶Σ�85%&lt;br&gt;&lt;font size='12' color='#666666'&gt;�ٹ�18Сʱ14�ֳ��죨����ҡǮ��&lt;/font&gt;&lt;br&gt;�����ջ�18Сʱ14��</crops>
                                    //http://www.kaixin001.com/!house/!garden/yaoqianshu.php?r=0%2E1238307012245059&fuid=0&verify=6194153%5F1062%5F6194153%5F1253607483%5F14f6afef57593e63f22fda3adc9a5685
                                    SetMessage("����ҡǮ��");
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
                                        SetMessageLn(string.Format("=>��{0}���ؿ飺", plot.FarmNum));
                                    else
                                        SetMessageLn(string.Format("=>��{0}�����ĵؿ飺", plot.FarmNum));
                                    canfarm = true;
                                }                               

                                if (plot.Water != 5)
                                {
                                    //��ˮ
                                    SetMessage("��Ҫ��ˮ��");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Post("http://www.kaixin001.com/house/garden/water.php", string.Format("fuid={0}&farmnum={1}&verify={2}&seedid=0&r=0%2E6590517126023769", fuid, plot.FarmNum, this._verifyCode));
                                    GetFarmFeedback(content);
                                }

                                if (plot.Vermin == 1)
                                {
                                    //׽��
                                    SetMessage("��Ҫ׽�棺");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antivermin.php?verify={0}&seedid=0&r=0%2E3779320823960006&fuid={1}&farmnum={2}", this._verifyCode, fuid, plot.FarmNum));
                                    GetFarmFeedback(content);
                                }

                                if (plot.Grass == 1)
                                {
                                    //����
                                    SetMessage("��Ҫ���ݣ�");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/antigrass.php?farmnum={0}&verify={1}&seedid=0&r=0%2E8164945561438799&fuid={2}", plot.FarmNum, this._verifyCode, fuid));
                                    GetFarmFeedback(content);
                                }

                                if (plot.CropsStatus == 2 && plot.Fuid.ToString() == CurrentAccount.UserId)
                                {
                                    //�ջ�
                                    SetMessage("�����ջ�");
                                    if (Task.HarvestFruit)
                                    {
                                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                                        content = HH.Post("http://www.kaixin001.com/house/garden/havest.php", string.Format("fuid={0}&farmnum={1}&verify={2}&seedid=0&r=0%2E44418928399682045", fuid, plot.FarmNum, this._verifyCode));
                                        if (GetFarmFeedback(content))
                                        {
                                            plot.CropsStatus = 3;
                                            plot.Crops = null;
                                            plot.Farm = null;
                                            //ͬ���б�
                                            //RemoveSharedFriendById(fuid);
                                        }
                                    }
                                }

                                if (plot.Shared != 0 &&
                                    (plot.CropsStatus == 3 && String.IsNullOrEmpty(plot.Crops) && String.IsNullOrEmpty(plot.Farm) ||
                                    plot.CropsStatus == -1))
                                {
                                    //���
                                    SetMessage("��Ҫ��أ�");
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/plough.php?verify={0}&seedid=0&r=0%2E018698612228035927&fuid={1}&farmnum={2}", this._verifyCode, fuid, plot.FarmNum));
                                    if (GetFarmFeedback(content))
                                        plot.CropsId = 0;
                                }

                                if (plot.CropsId == 0 && plot.Shared != 0 && this._canfarmshared)
                                {
                                    //����
                                    SetMessage("���Բ��֣�");
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
                                                //��Ŀǰ�ļ������ͬʱ����2����ѵĵ�
                                                Regex regular = new Regex(@"��Ŀǰ�ļ������ͬʱ����[\d]+����ѵĵ�");
                                                if (regular.IsMatch(content))
                                                {
                                                    this._canfarmshared = false;
                                                    SetMessageLn("����Ŀǰ�ļ����޷��ٸ��ָ�����ѵĵ��ˣ�ֹͣ���ְ��ĵؿ飡");
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
                                            //��Ŀǰ�ļ������ͬʱ����2����ѵĵ�
                                            Regex regular = new Regex(@"��Ŀǰ�ļ������ͬʱ����[\d]+����ѵĵ�");
                                            if (regular.IsMatch(content))
                                            {
                                                this._canfarmshared = false;
                                                SetMessageLn("����Ŀǰ�ļ����޷��ٸ��ָ�����ѵĵ��ˣ�ֹͣ���ְ��ĵؿ飡");
                                                if (!helpothers && farmshared)
                                                    break;
                                            }
                                            //���Բ��ְ����...���������ͬʱ����1��� ʧ�ܣ�
                                            Regex regular2 = new Regex(@"���������ͬʱ����[\d]���");
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
                SetMessage(" ȥ" + GetFriendNameById(fuid) + "�Ļ�԰��æʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region SowSharedPlot
        private bool SowSharedPlot(string fuid, SeedInfo seed, PlotInfo plot, ref string content)
        {
            SetMessage(" ���Բ���" + seed.Name + "...");
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/farmseed.php?verify={0}&seedid={1}&r=0%2E1429477329365909&fuid={2}&farmnum={3}", this._verifyCode, seed.SeedId, fuid, plot.FarmNum));
            if (GetFarmFeedback(content))
            {
                seed.Num--;
                return true;
            }
            else
            {
                if (content.Contains("���������ͬʱ����"))
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
            //        //��Ч�����磺���������ͬʱ����1���
            //        if (!seedunit.Valid)
            //            continue;
            //        //�˲β������ڰ��ĵؿ�
            //        if (seedunit.SeedId == 21)
            //            continue;
            //        //����ɳ���������ڰ��ĵؿ�
            //        if (seedunit.SeedId == 104)
            //            continue;
            //        //ҡǮ���������ڰ��ĵ���
            //        if (seedunit.SeedId == 102)
            //            continue;
            //        //�����޲������ڰ��ĵ���
            //        if (seedunit.SeedId == 114)
            //            continue;
            //        //��������޲������ڰ��ĵ���
            //        if (seedunit.SeedId == 116)
            //            continue;
            //        //�����ˣ�û��������
            //        if (seedunit.Num < 1)
            //            continue;
            //        //�����
            //        if (seedunit.SeedId == 39)
            //        {
            //            //���Բ��ְ����...������ֻ���������Եİ��ĵ� ʧ�ܣ�
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
                        SetMessage(" ����Բ��ֵ��������ӣ�" + rankseed.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetSharedFarmSeed", " �޷�ȡ�õȼ�" + this._myRank + "������", LogSeverity.Warn);
                        SetMessage(" �޷�ȡ�õȼ�" + this._myRank + "������");
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
                        SetMessage(" ���趨�Ĳ������ӣ�" + seed1.Name);
                    }
                    else
                    {
                        LogHelper.Write("GetSharedFarmSeed", " �޷�ȡ���Զ�������ӣ�" + Task.CustomFarmShared.ToString(), LogSeverity.Warn);
                        SetMessage(" �޷�ȡ���Զ�������ӣ�" + Task.CustomFarmShared.ToString());
                        return null;
                    }
                }
                
                seed = GetFarmSeedById(seedid);
                if (seed == null)
                {
                    SetMessage("û��" + seedname + "������");
                    if (_outofmoney)
                    {
                        SetMessage(" �ֽ𲻹������ܹ���");
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

            //�˲β������ڰ��ĵؿ���򷵻ض����Ĳ�
            if (rank >= 15 && rank <= 19)
                rank = 14;

            //����ɳ���������ڰ��ĵؿ���򷵻زغ컨
            if (rank == 30)
                rank = 29;

            //�����޲������ڰ��ĵؿ���򷵻غ�����
            if (rank >= 32 && rank <= 39)
                rank = 31;

            foreach (RankSeedInfo rankseed in _rankSeedsList)
            {
                //�˲β������ڰ��ĵؿ���
                if (rankseed.SeedId == 21)
                    continue;
                //����ɳ���������ڰ��ĵؿ���
                if (rankseed.SeedId == 104)
                    continue;
                //�����޲������ڰ��ĵؿ���
                if (rankseed.SeedId == 114)
                    continue;

                //�����
                if (rankseed.SeedId == 39)
                {
                    //���Բ��ְ����...������ֻ���������Եİ��ĵ� ʧ�ܣ�
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
                SetMessageLn("��ʼ���͹�ʵ...");
                if (Operation.PresentId == 0)
                {
                    SetMessage("û���趨���͵Ķ����޷�����");
                    return;
                }
                if (!IsAlreadyMyFriend(DataConvert.GetString(Operation.PresentId)))
                {
                    SetMessage(DataConvert.GetString(Operation.PresentId) + "������ĺ��ѣ��޷�����");
                    return;
                }
                    
                string content = RequestMyWarehouse();
                Collection<FruitInfo> fruits = ConfigCtrl.GetMyGardenWarehouse(content);
                if (fruits == null || fruits.Count == 0)
                {
                    SetMessage("�ֿ���û���κι�ʵ");
                    return;
                }

                _incorrentcount = false;

                if (Task.PresentFruitByPrice)
                {
                    //�����ֵ���
                    SetMessageLn("�ֿ�������͵Ĺ�ʵ��");

                    int num = 0;
                    Collection<PresentInfo> presents = new Collection<PresentInfo>();
                    foreach (FruitInfo myfruit in fruits)
                    {
                        //����
                        if (myfruit.FruitId == 63)
                            continue;

                        FruitInfo fruit = GetFruitById(myfruit.FruitId);
                        if (fruit == null)
                        {
                            SetMessage(string.Format("δ֪��ʵ{0}-{1}������ ", myfruit.FruitId, myfruit.Name));
                            LogHelper.Write("GameGarden.PresentFruit" + CurrentAccount.UserName, string.Format("δ֪��ʵ{0}-{1}������ ", myfruit.FruitId, myfruit.Name), LogSeverity.Warn);
                            continue;
                        }
                        PresentInfo present = new PresentInfo();
                        present.SeedId = myfruit.FruitId;
                        present.Name = myfruit.Name;
                        present.SelfNum = myfruit.Num;
                        present.FruitPrice = fruit.SellPrice;
                        presents.Add(present);
                        SetMessageLn(string.Format("#{0}{1}��������{2}�������ۼۣ�{3}���ܼ�ֵ��{4}", ++num, present.Name, present.SelfNum, present.FruitPrice, present.SellSum));
                    }

                    if (presents.Count == 0)
                    {
                        SetMessage(" û�п��͵Ĺ�ʵ");
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

                    SetMessageLn(string.Format("����������͵ļ�ֵ��ߵĹ�ʵ��{0}:{1}*{2}={3}Ԫ", presentMax.Name, presentMax.SelfNum, presentMax.FruitPrice, presentMax.SellSum));
                    if (Task.PresentFruitCheckValue && presentMax.SellSum < Task.PresentFruitValue * 10000)
                    {
                        SetMessage(string.Format(" �ܼ�ֵ{0}С��������ͼ�ֵ{1}������", presentMax.SellSum, Task.PresentFruitValue * 10000));
                        return;
                    }

                    SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentId), presentMax.SelfNum, presentMax.Name, presentMax.FruitPrice, presentMax.SellSum));
                    content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", presentMax.SeedId, Operation.PresentId, presentMax.SelfNum, DataConvert.GetEncodeData("�����ʵ����"), this._verifyCode));
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
                                SetMessage(string.Format(" �ܼ�ֵ{0}С��������ͼ�ֵ{1}������", present.SelfNum * present.FruitPrice, Task.PresentFruitValue * 10000));
                                return;
                            }
                            SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentId), present.SelfNum, present.Name, present.FruitPrice, present.SellSum));
                            content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", presentMax.SeedId, Operation.PresentId, present.SelfNum, DataConvert.GetEncodeData("�����ʵ����"), this._verifyCode));
                            GetPresentFeedback(content);
                            return;
                        }
                    }
                }
                else
                {
                    SetMessageLn(string.Format("��������ָ���Ĺ�ʵ��{0}...", GetFruitNameById(Task.PresentFruitId)));
                    foreach (FruitInfo myfruit in fruits)
                    {
                        //����
                        if (myfruit.FruitId == 63)
                            continue;
                        if (myfruit.FruitId == Task.PresentFruitId)
                        {
                            if (Task.PresentFruitCheckNum && myfruit.Num < Task.PresentFruitNum)
                            {
                                SetMessage(string.Format("����{0}< ��С������{1}������ ", myfruit.Num, Task.PresentFruitNum));
                                return;
                            }
                            FruitInfo fruit = GetFruitById(myfruit.FruitId);
                            if (fruit == null)
                            {
                                SetMessage(string.Format("δ֪��ʵ{0}-{1}������ ", myfruit.FruitId, myfruit.Name));
                                LogHelper.Write("GameGarden.PresentFruit" + CurrentAccount.UserName, string.Format("δ֪��ʵ{0}-{1}������ ", myfruit.FruitId, myfruit.Name), LogSeverity.Warn);
                                return;
                            }
                            SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentId), myfruit.Num, myfruit.Name, fruit.SellPrice, fruit.SellPrice * myfruit.Num));
                            content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", Task.PresentFruitId, Operation.PresentId, myfruit.Num, DataConvert.GetEncodeData("�����ʵ����"), this._verifyCode));
                            if (GetPresentFeedback(content))
                                return;
                            if (content.Contains("����������"))
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
                                        SetMessage(string.Format("��������{0}< ��С������{1}������ ", present.SelfNum, Task.PresentFruitNum));
                                        return;
                                    }
                                    SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentId), present.SelfNum, present.Name, present.FruitPrice, present.SellSum));
                                    content = HH.Post("http://www.kaixin001.com/house/garden/presentfruit.php", string.Format("seedid={0}&touid={1}&num={2}&pmsg={3}&anon=0&verify={4}", Task.PresentFruitId, Operation.PresentId, present.SelfNum, DataConvert.GetEncodeData("�����ʵ����"), this._verifyCode));
                                    GetPresentFeedback(content);
                                    return;
                                }
                            }
                        }
                    }
                    SetMessage("�ֿ���û�и��ֹ�ʵ���޷����͡�");
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
                SetMessage(" ��" + GetFriendNameById(Operation.PresentId) + "���͹�ʵʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetPresentFeedback
        private bool GetPresentFeedback(string content)
        {
            try
            {
                //<data><ret>fail</ret><reason>һ��ֻ�ܸ�ͬһ��������һ�ι�ʵ</reason></data>
                //<data><ret>succ</ret></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ����ʧ�ܣ�");
                    if (content.Contains("��������ȷ"))
                        _incorrentcount = true;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage("���ͳɹ���");
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
                SetMessageLn("��ʼ���۹�ʵ...");
                if (Task.LowCash)
                {
                    if (garden.Cash > Task.LowCashLimit * 10000)
                    {
                        SetMessageLn(string.Format("����{0}Ԫ�ֽ�������ۡ�", garden.Cash));
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
                        SetMessage("�ֿ���û���κι�ʵ");
                        return;
                    }

                    //�����ֵ
                    long soldvalue = 0;
                    long fruitvalue = 0;
                    foreach (FruitInfo myfruit in fruits)
                    {
                        if (Task.SellForbiddennFruitsList.Contains(myfruit.FruitId))
                        {
                            SetMessageLn(string.Format("{0}�ڳ��۵Ľ�ֹ�б��У�����", myfruit.Name));
                            continue;
                        }
                        fruitvalue = 0;
                        if (soldvalue >= Task.MaxSellLimit * 10000)
                        {
                            SetMessageLn(string.Format("�ѳ��۵Ĺ�ʵ�ܼ�ֵ�Ѿ�����{0}��ֹͣ���ۡ�", Task.MaxSellLimit));
                            break;
                        }

                        int seedprice = GetFruitSellPriceById(myfruit.FruitId);
                        if (seedprice <= 0)
                        {
                            SetMessageLn(string.Format("δ֪��ʵ{0}-{1}������", myfruit.Name));
                            continue;
                        }
                        double temp = (Task.MaxSellLimit * 10000 - soldvalue) / seedprice;
                        int sellnum = Math.Min(DataConvert.GetInt32(Math.Ceiling(temp)), myfruit.Num);
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        content = HH.Get(string.Format("http://www.kaixin001.com/house/garden/sellfruit.php?seedid={0}&num={1}&all=0&verify={2}", myfruit.FruitId, sellnum, this._verifyCode));
                        if (GetSellFeedback(content, ref fruitvalue))
                        {
                            soldvalue += fruitvalue;
                            SetMessage(string.Format("�ѳ��۵Ĺ�ʵ�ܼ�ֵ��{0}Ԫ", soldvalue));
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
                SetMessage("���۹�ʵʧ�ܣ�����" + ex.Message);
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
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ����ʧ�ܣ�");                    
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SellInfo objSell = ConfigCtrl.ConvertToSellObject(content);
                    if (objSell == null)
                        SetMessage("���������쳣������ʧ�ܣ�");
                    else
                    {
                        if (objSell.All == 1)
                            SetMessageLn(string.Format("����ȫ��{0}����ʵ�������{1}Ԫ ���۳ɹ���", objSell.Num, objSell.TotalPrice));
                        else
                            SetMessageLn(string.Format("����{0}��{1}�������{2}Ԫ ���۳ɹ���", objSell.Num, objSell.GoodsName, objSell.TotalPrice));
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
            //if (content.IndexOf("<title>������ - ������</title>") != -1)
            if (content.IndexOf("��Ϸ������") == -1)
            {
                SetMessageLn("��δ��װ�������");
                //SetMessageLn("��δ��װ�������,���԰�װ��...");
                //HH.Post("http://www.kaixin001.com/app/install.php", "aid=1062&isinstall=1");
                //content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1062");
                //this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
                ////init city
                //HH.Get("http://www.kaixin001.com/house/inithouse_dialog.php?verify=" + this._verifyCode);
                //HH.Get("http://www.kaixin001.com/house/gethouseconfig.php?verify=" + this._verifyCode + "&roomid=8646&fuid=&r=0.8915753769688308");
                //HH.Post("http://www.kaixin001.com/house/inithouse_dialog.php", "http://www.kaixin001.com/house/inithouse_dialog.php?verify=" + this._verifyCode, "verify=" + this._verifyCode + "&step=2");
                //HH.Post("http://www.kaixin001.com/house/inithouse.php", "http://www.kaixin001.com/house/inithouse_dialog.php", "city=%E4%B8%8A%E6%B5%B7&verify=" + this._verifyCode);
                //content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1062");
                //SetMessage("��Ҫ�����ĳ��У��Ϻ���");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            return content;
        }

        public string RequestAllGardenFriends()
        {
            //��԰�ĺ�����ʵ�����������ӵĺ���
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
