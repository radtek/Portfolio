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
    public class GameRanch : KaixinBase
    {
        private const int MAX_ANIMAL_COUNT = 12;

        private Collection<FriendInfo> _allRanchFriendsList;
        //private Collection<FriendInfo> _stealFriendsList;
        //private Collection<FriendInfo> _productFriendsList;
        private Collection<FriendInfo> _ranchFriendsList;
        private Collection<CalfInfo> _calfsList;
        private Collection<ProductInfo> _productsList;
        //private Collection<int> _hasNothingTobeHelpedList;
        private Collection<FoodInfo> _myfoodList;
        private Collection<RankSeedInfo> _rankSeedsList;
        private bool _canstealproduct;
        private bool _canaddgrass;
        private bool _canaddcarrot;
        private bool _canaddbamboo;
        private int _myRank;
        private bool _outofmoney;
        private bool _feedlimited;
        private bool _canbuyanimals;
        private bool _incorrentcount;

        public delegate void AllRanchFriendsFetchedEventHandler(Collection<FriendInfo> allranchfriends);
        public event AllRanchFriendsFetchedEventHandler AllRanchFriendsFetched;

        public delegate void AgriculturalProductFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event AgriculturalProductFriendsFetchedEventHandler AgriculturalProductFriendsFetched;

        public delegate void CalvesInShopFetchedEventHandler(Collection<CalfInfo> calves);
        public event CalvesInShopFetchedEventHandler CalvesInShopFetched;

        public GameRanch()
        {
            this._canstealproduct = true;
            this._canaddgrass = true;
            this._canaddcarrot = true;
            this._canaddbamboo = true;
            this._myRank = 1;
            this._outofmoney = false;
            this._feedlimited = false;
            this._canbuyanimals = true;
            this._incorrentcount = false;
            this._allRanchFriendsList = new Collection<FriendInfo>();
            //this._stealFriendsList = new Collection<FriendInfo>();
            //this._productFriendsList = new Collection<FriendInfo>();
            this._ranchFriendsList = new Collection<FriendInfo>();
            //this._hasNothingTobeHelpedList = new Collection<int>();
            this._rankSeedsList = new Collection<RankSeedInfo>();
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //ranch
                SetMessageLn("正在初始化[牧场]...");

                string content = RequestHouseHomePage(true);

                //all ranch friends
                content = RequestAllRanchFriends();
                ReadAllRanchFriends(content, false);
                SetMessage("[所有牧场的好友]信息下载成功！");

                //agricultural product friends
                content = RequestAgriculturalProductFriends();
                ReadAgriculturalProductFriends(content, false);
                SetMessage("[牧场中有可偷的农副产品的好友]信息下载成功！");
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
                LogHelper.Write("GameRanch.Initialize", ex, LogSeverity.Error);
                SetMessage(" 初始化[牧场]失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetAllRanchFriends
        public void GetAllRanchFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAllRanchFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void GetAllRanchFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_RANCH;
                SetMessageLn("刷新[所有牧场的好友]...");

                if (!this.ValidationLogin(true))
                {
                    if (AllRanchFriendsFetched != null)
                        AllRanchFriendsFetched(_allRanchFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestAllRanchFriends();
                ReadAllRanchFriends(content, true);
                SetMessageLn("[所有牧场的好友]信息刷新成功！");

                //invoke event
                if (AllRanchFriendsFetched != null)
                    AllRanchFriendsFetched(_allRanchFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[所有牧场的好友]信息刷新失败！");
        }

        #endregion

        #region ReadAllRanchFriends
        public void ReadAllRanchFriends(string content, bool printMessage)
        {
            int num;
            this._allRanchFriendsList.Clear();

            if (printMessage)
                SetMessageLn("读取[所有牧场的好友]信息:");
            //<div class="l" style="width:8em;"><a href="javascript:gotoUser(6194153);" class="sl">庄子</a></div>
            string content2 = content;
            for (string pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num); pos != null; pos = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:8em;\">", "</div>", out num))
            {
                FriendInfo friend = new FriendInfo();
                friend.Id = JsonHelper.GetMidInteger(content, "<a href=\"javascript:gotoUser(", ");\"");
                friend.Name = JsonHelper.GetMid(content, "class=\"sl\">", "</a>");
                this._allRanchFriendsList.Add(friend);
                if (printMessage)
                    SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
                content = content.Substring(num);
            }

            int ix = 0;
            for (string pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num); pos != null; pos = JsonHelper.GetMid(content2, "<input type=hidden id=gender_", ">", out num))
            {
                if (pos.IndexOf("value=\"1\"") > -1)
                {
                    this._allRanchFriendsList[ix].Gender = false;
                }
                else if (pos.IndexOf("value=\"0\"") > -1)
                {
                    this._allRanchFriendsList[ix].Gender = true;
                }
                else
                {
                    break;
                }

                if (printMessage)
                    SetMessageLn(this._allRanchFriendsList[ix].Name + "(" + this._allRanchFriendsList[ix].Id.ToString() + ")--" + (this._allRanchFriendsList[ix].Gender ? "男" : "女"));
                ix++;
                content2 = content2.Substring(num);
            }
            if (printMessage)
                SetMessageLn("完成读取！");
        }
        #endregion

        #region GetAgriculturalProductFriends
        public void GetAgriculturalProductFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAgriculturalProductFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetAgriculturalProductFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_RANCH;
                SetMessageLn("刷新[牧场中有可偷的农副产品的好友]...");

                if (!this.ValidationLogin(true))
                {
                    if (AgriculturalProductFriendsFetched != null)
                        AgriculturalProductFriendsFetched(_ranchFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestAgriculturalProductFriends();
                ReadAgriculturalProductFriends(content, true);
                SetMessageLn("[牧场中有可偷的农副产品的好友]信息刷新成功！");

                //invoke event
                if (AgriculturalProductFriendsFetched != null)
                    AgriculturalProductFriendsFetched(_ranchFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[牧场中有可偷的农副产品的好友]信息刷新失败！");
        }

        #endregion

        #region ReadAgriculturalProductFriends
        public void ReadAgriculturalProductFriends(string content, bool printMessage)
        {
            try
            {
                if (printMessage)
                    SetMessageLn("读取[牧场中有成熟农副产品的好友]信息...");

                //[{"uid":11860509,"real_name":"\u5f90\u5723\u541b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/5\/20_11860509_1.jpg","harvest":1,"food":1},{"uid":1504367,"real_name":"\u4f59\u661f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/50\/43\/20_1504367_20.jpg","harvest":1,"food":1},{"uid":18643363,"real_name":"\u595a\u51e4(\u6021\u6021)","icon20":"http:\/\/pic1.kaixin001.com\/logo\/64\/33\/20_18643363_1.jpg","harvest":1,"food":1},{"uid":32263316,"real_name":"\u5f90\u6daf\u7433","icon20":"http:\/\/pic.kaixin001.com\/logo\/26\/33\/20_32263316_15.jpg","harvest":1,"product":1,"food":1},{"uid":4026057,"real_name":"\u8521\u632f\u534e","icon20":"http:\/\/pic1.kaixin001.com\/logo\/2\/60\/20_4026057_2.jpg","harvest":1},{"uid":4179925,"real_name":"\u51b7\u8840","icon20":"http:\/\/pic1.kaixin001.com\/logo\/17\/99\/20_4179925_1.jpg","harvest":1,"food":1},{"uid":4789786,"real_name":"\u5b5f\u519b\u534e","icon20":"http:\/\/pic.kaixin001.com\/logo\/78\/97\/20_4789786_2.jpg","harvest":1},{"uid":27618660,"real_name":"\u9ad8\u4ebf\u658c","icon20":"http:\/\/pic.kaixin001.com\/logo\/61\/86\/20_27618660_3.jpg","product":1},{"uid":4121752,"real_name":"\u5f90\u4e3d\u82ac","icon20":"http:\/\/pic.kaixin001.com\/logo\/12\/17\/20_4121752_1.jpg","product":1},{"uid":4343401,"real_name":"\u9648\u9e4f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/34\/34\/20_4343401_5.jpg","product":1},{"uid":4570613,"real_name":"\u6f58\u534e","icon20":"http:\/\/pic1.kaixin001.com\/logo\/57\/6\/20_4570613_6.jpg","product":1},{"uid":10151052,"real_name":"\u5b59\u6b63\u82b3","icon20":"http:\/\/pic.kaixin001.com\/logo\/15\/10\/20_10151052_1.jpg","food":1,"water":1},{"uid":10368525,"real_name":"\u502a\u4f1f\u534e","icon20":"http:\/\/pic1.kaixin001.com\/logo\/36\/85\/20_10368525_1.jpg","food":1},{"uid":1560381,"real_name":"\u848b\u745b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/56\/3\/20_1560381_4.jpg","food":1},{"uid":18601260,"real_name":"\u6731\u5e7f\u7530","icon20":"http:\/\/pic.kaixin001.com\/logo\/60\/12\/20_18601260_2.jpg","food":1},{"uid":1922571,"real_name":"\u4f40\u95ef","icon20":"http:\/\/pic1.kaixin001.com\/logo\/92\/25\/20_1922571_5.jpg","food":1},{"uid":1991973,"real_name":"\u5de2\u5a67","icon20":"http:\/\/pic1.kaixin001.com\/logo\/99\/19\/20_1991973_2.jpg","food":1},{"uid":2119333,"real_name":"\u4faf\u9e23","icon20":"http:\/\/pic1.kaixin001.com\/logo\/11\/93\/20_2119333_4.jpg","food":1},{"uid":2511621,"real_name":"\u4e07\u6d69","icon20":"http:\/\/pic1.kaixin001.com\/logo\/51\/16\/20_2511621_5.jpg","food":1},{"uid":26366307,"real_name":"\u6731\u4f1f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/36\/63\/20_26366307_2.jpg","food":1},{"uid":2865629,"real_name":"\u7ae5\u610f\u5fe0(@)","icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/56\/20_2865629_21.jpg","food":1},{"uid":28860603,"real_name":"\u9648\u5609\u59ae","icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/6\/20_28860603_6.jpg","food":1,"fee":1},{"uid":3342217,"real_name":"\u5468\u4e3d","icon20":"http:\/\/pic1.kaixin001.com\/logo\/34\/22\/20_3342217_71.jpg","food":1},{"uid":362564,"real_name":"\u9648\u52bc","icon20":"http:\/\/pic.kaixin001.com\/logo\/36\/25\/20_362564_22.jpg","food":1},{"uid":3653622,"real_name":"\u9ec4\u6587\u7fa4","icon20":"http:\/\/pic.kaixin001.com\/logo\/65\/36\/20_3653622_12.jpg","food":1},{"uid":3754193,"real_name":"\u502a\u519b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/75\/41\/20_3754193_9.jpg","food":1},{"uid":5505715,"real_name":"\u77bf\u535a","icon20":"http:\/\/pic1.kaixin001.com\/logo\/50\/57\/20_5505715_1.jpg","food":1},{"uid":6106453,"real_name":"\u6c5f\u950b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/10\/64\/20_6106453_1.jpg","food":1},{"uid":6265093,"real_name":"\u987e\u73fa\u96ef","icon20":"http:\/\/pic1.kaixin001.com\/logo\/26\/50\/20_6265093_3.jpg","food":1},{"uid":6320371,"real_name":"\u5f20\u79e6\u8273","icon20":"http:\/\/pic1.kaixin001.com\/logo\/32\/3\/20_6320371_5.jpg","food":1},{"uid":6888001,"real_name":"\u66f9\u6e0a","icon20":"http:\/\/pic1.kaixin001.com\/logo\/88\/80\/20_6888001_1.jpg","food":1,"water":1},{"uid":7744681,"real_name":"\u6587\u6653\u6653","icon20":"http:\/\/pic1.kaixin001.com\/logo\/74\/46\/20_7744681_2.jpg","food":1},{"uid":8063649,"real_name":"\u675c\u6ce2","icon20":"http:\/\/pic1.kaixin001.com\/logo\/6\/36\/20_8063649_15.jpg","food":1},{"uid":9637731,"real_name":"\u66f9\u840d","icon20":"http:\/\/pic1.kaixin001.com\/logo\/63\/77\/20_9637731_2.jpg","food":1},{"uid":1283947,"real_name":"\u6797\u8054\u5bf9","icon20":"http:\/\/pic1.kaixin001.com\/logo\/28\/39\/20_1283947_2.jpg","water":1},{"uid":1581208,"real_name":"\u7fc1\u5c11\u534e","icon20":"http:\/\/pic.kaixin001.com\/logo\/58\/12\/20_1581208_4.jpg","water":1},{"uid":1703568,"real_name":"\u5510\u8c6a\u5ddd","icon20":"http:\/\/pic.kaixin001.com\/logo\/70\/35\/20_1703568_6.jpg","water":1},{"uid":1945978,"real_name":"\u9648\u5b66\u8d85","icon20":"http:\/\/pic.kaixin001.com\/logo\/94\/59\/20_1945978_7.jpg","water":1},{"uid":2125264,"real_name":"\u66f9\u548f\u840d","icon20":"http:\/\/pic.kaixin001.com\/logo\/12\/52\/20_2125264_6.jpg","water":1},{"uid":2596914,"real_name":"\u5510\u5ddd\u519b","icon20":"http:\/\/pic.kaixin001.com\/logo\/59\/69\/20_2596914_1.jpg","water":1},{"uid":3223271,"real_name":"\u53f2\u53f2","icon20":"http:\/\/pic1.kaixin001.com\/logo\/22\/32\/20_3223271_2.jpg","water":1},{"uid":3644956,"real_name":"\u6c88\u71d5\u9752","icon20":"http:\/\/pic.kaixin001.com\/logo\/64\/49\/20_3644956_2.jpg","water":1},{"uid":3933628,"real_name":"\u5468\u654f","icon20":"http:\/\/pic.kaixin001.com\/logo\/93\/36\/20_3933628_1.jpg","water":1},{"uid":3986105,"real_name":"\u6c88\u84d3\u6654","icon20":"http:\/\/pic1.kaixin001.com\/logo\/98\/61\/20_3986105_1.jpg","water":1},{"uid":4114760,"real_name":"\u6768\u6770","icon20":"http:\/\/pic.kaixin001.com\/logo\/11\/47\/20_4114760_3.jpg","water":1},{"uid":5010598,"real_name":"\u7f2a\u5357","icon20":"http:\/\/pic.kaixin001.com\/logo\/1\/5\/20_5010598_2.jpg","water":1},{"uid":5969055,"real_name":"\u534e\u654f\u5cf0","icon20":"http:\/\/pic1.kaixin001.com\/logo\/96\/90\/20_5969055_7.jpg","water":1},{"uid":779907,"real_name":"\u9a6c\u749f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/77\/99\/20_779907_1.jpg","water":1},{"uid":9414220,"real_name":"\u738b\u4eae","icon20":"http:\/\/pic.kaixin001.com\/logo\/41\/42\/20_9414220_15.jpg","water":1}]
                JsonTextParser parser = new JsonTextParser();
                JsonArrayCollection arraySharedFriends = parser.Parse(content) as JsonArrayCollection;
                if (arraySharedFriends != null)
                {
                    if (printMessage)
                        SetMessageLn("可以生产的牧场：");
                    foreach (JsonObjectCollection item in arraySharedFriends)
                    {
                        FriendInfo friend = new FriendInfo();
                        friend.Id = JsonHelper.GetIntegerValue(item["uid"]);
                        friend.Name = JsonHelper.GetStringValue(item["real_name"]);
                        friend.RanchHarvest = item["harvest"] != null ? true : false;
                        friend.RanchFood = item["food"] != null ? true : false;
                        friend.RanchProduct = item["product"] != null ? true : false;
                        friend.RanchWater = item["water"] != null ? true : false;
                        this._ranchFriendsList.Add(friend);
                        if (printMessage)
                            SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
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
                LogHelper.Write("GameRanch.ReadAgriculturalProductFriends", content, ex, LogSeverity.Error);
                SetMessage(" 读取[牧场中有成熟农副产品的好友]信息失败！" + ex.Message);
            }
        }
        #endregion

        #region GetAnimalsInShop
        public void GetCalvesInShopByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetCalvesInShop));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetCalvesInShop()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_UPDATEDATA;
                SetMessageLn("刷新[商店中动物幼仔列表]...");

                if (!this.ValidationLogin())
                {
                    if (CalvesInShopFetched != null)
                        CalvesInShopFetched(null);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestCalvesList();
                Collection<CalfInfo> calves = ConfigCtrl.GetOriginalCalves(content);
                calves = SortCalvesListByPrice(calves);
                if (calves == null || calves.Count == 0)
                    SetMessageLn("[商店中动物幼仔列表]信息刷新失败！");
                else
                    SetMessageLn("[商店中动物幼仔列表]信息刷新成功！");

                //invoke event
                if (CalvesInShopFetched != null)
                    CalvesInShopFetched(calves);
            });
            base.ExecuteTryCatchBlock(th, "[商店中动物幼仔列表]信息刷新失败！");
        }

        #endregion

        #region RunRanch
        public void RunRanch()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_RANCH;

                SetMessageLn("开始牧场...");

                //ranch
                string contentHome = RequestHouseHomePage(false);

                //task manager中不会为null，在测试黑白配置时为null
                if (this._calfsList == null)
                {
                    this._calfsList = ConfigCtrl.GetCalvesInShop();
                }

                //RequestRanchConf(this._verifyCode, CurrentAccount.UserId);
                //return;

                RanchInfo ranch = ReadRanch(this._verifyCode, CurrentAccount.UserId, true);
                if (ranch == null)
                {
                    SetMessageLn("无法读取我的牧场信息！");
                    return;
                }

                ReadMyFoods(ranch);

                if (_myfoodList.Count == 0)
                {
                    SetMessage("没有饲料了！");
                }

                string content = "";

                //添水
                if (Task.AddWater)
                    AddWater(CurrentAccount.UserId, ranch);
                //添牧草
                if (Task.AddGrass)
                    AddGrass(CurrentAccount.UserId, ranch);
                //添胡萝卜
                if (Task.AddCarrot)
                    AddCarrot(CurrentAccount.UserId, ranch);
                //添竹子
                if (Task.AddBamboo)
                    AddBamboo(CurrentAccount.UserId, ranch);
                //收获农副产品
                if (Task.HarvestProduct)
                    HarvestProduct(ranch);
                //生产/收获动物
                if (Task.MakeProduct || Task.HarvestAnimal)
                    HarvestAndProductAnimal(ranch);

                //配种
                if (Task.BreedAnimal)
                    BreedAnimal();

                //购买幼仔
                if (Task.BuyCalf)
                {
                    BuyCalf(ranch);
                    //ranch = ReadRanch(this._verifyCode, CurrentAccount.UserId, false);
                }

                if (Task.HelpAddWater || Task.HelpAddGrass || Task.HelpAddCarrot || Task.HelpAddBamboo || Task.HelpMakeProduct)
                {
                    content = RequestAllRanchFriends();
                    ReadAllRanchFriends(content, false);
                }
                
                //偷窃农副产品
                //if (Task.StealProduct && Operation.StealProductAll || Task.HelpMakeProduct && Operation.HelpRanchAll)
                //{
                    
                //}

                content = RequestAgriculturalProductFriends();
                ReadAgriculturalProductFriends(content, false);
                
                if (Task.StealProduct)
                    StealRanchs();

                //帮忙生产
                if (Task.HelpMakeProduct)
                    HelpMakeProduct();

                //去其他牧场帮忙
                if (Task.HelpAddWater || Task.HelpAddGrass || Task.HelpAddCarrot || Task.HelpAddBamboo)
                    HelpOthersRanchs();

                //赠送农副产品
                if (Task.PresentProduct)
                    PresentAnimalProduct();

                //出售果实
                if (Task.SellProduct)
                    SellProduct(ranch);

                SetMessageLn("牧场完成！");

            });
            base.ExecuteTryCatchBlock(th, "发生异常，牧场失败！");
        }
        #endregion

        #region ReadRanch
        private RanchInfo ReadRanch(string verifyCode, string fuid, bool printmessage)
        {
            RanchInfo ranch = null;
            RequestRanch(fuid);
            string strRanch = RequestRanchConf(verifyCode, fuid);
            ranch = ConfigCtrl.GetRanch(strRanch);
            if (ranch == null)
            {
                if (printmessage)
                    SetMessageLn("读取牧场信息失败！");
            }
            else
            {
                if (CurrentAccount.UserId == fuid)
                    this._myRank = ranch.Rank;

                if (printmessage)
                    SetMessageLn(string.Format("{0}的牧场：{1} {2} 魅力：{3} 水：{4} 牧草：{5}", ranch.Name, ranch.RankTip, ranch.CashTip, ranch.TCharms, ranch.Water, ranch.Grass));

                if (ranch.Animals != null)
                {
                    int num = 0;
                    //foreach (AnimalInfo animal in ranch.Animals)
                    //{
                    //    SetMessageLn(string.Format("第{0}个动物：", ++num));
                    //    SetMessage("uid:" + animal.Uid.ToString());
                    //    SetMessage(" animalsid:" + animal.AnimalSid.ToString());
                    //    SetMessage(" aid:" + animal.Aid.ToString());
                    //    SetMessage(" status:" + animal.Status.ToString());
                    //    SetMessage(" btime:" + animal.BTime.ToString());
                    //    SetMessage(" buid:" + animal.BUid.ToString());
                    //    SetMessage(" bnum:" + animal.BNum.ToString());
                    //    SetMessage(" ctime:" + animal.CTime.ToString());
                    //    SetMessage(" gtime:" + animal.GTime.ToString());
                    //    SetMessage(" ftime:" + animal.FTime.ToString());
                    //    SetMessage(" grow:" + animal.Grow.ToString());
                    //    SetMessage(" ptime:" + animal.PTime.ToString());
                    //    SetMessage(" pnum:" + animal.PNum.ToString());
                    //    SetMessage(" ptype:" + animal.Ptype.ToString());
                    //    SetMessage(" daynum:" + animal.DayNum.ToString());
                    //    SetMessage(" fstatus:" + animal.FStatus.ToString());
                    //    SetMessage(" bproduct:" + animal.BProduct.ToString());
                    //    SetMessage(" bstat:" + animal.BStat.ToString());
                    //    SetMessage(" leftptime:" + animal.LeftPTime.ToString());
                    //    SetMessage(" tips:" + animal.Tips.ToString());
                    //    SetMessage(" skey:" + animal.SKey.ToString());
                    //    SetMessage(" pic:" + animal.Pic.ToString());
                    //    SetMessage(" tname:" + animal.TName.ToString());
                    //    SetMessage(" aname:" + animal.AName.ToString());
                    //    SetMessage(" paction:" + animal.PAction.ToString());
                    //}

                    foreach (AnimalInfo animal in ranch.Animals)
                    {
                        if (printmessage)
                            SetMessageLn(string.Format("第{0}个动物：{1}", ++num, animal.AName));

                        if (!String.IsNullOrEmpty(animal.Tips))
                        {
                            if (printmessage)
                                SetMessage(" *" + JsonHelper.FiltrateHtmlTags(animal.Tips));
                        }
                    }
                }
            }

            return ranch;
        }
        #endregion

        #region ReadMyFood
        private void ReadMyFoods(RanchInfo ranch)
        {
            SetMessageLn("我的饲料：");
            string content = RequestMyFoodList(1);
            int totalpage = 0;
            _myfoodList = ConfigCtrl.GetMyFoods(content, ref totalpage);

            if (_myfoodList == null)
            {
                SetMessage("无法读取我的饲料信息！");
                return;
            }

            if (totalpage > 1)
            {
                for (int ix = 2; ix <= totalpage; ix++)
                {
                    content = RequestMyFoodList(ix);
                    int pagenum = 0;
                    Collection<FoodInfo> nextfoods = ConfigCtrl.GetMyFoods(content, ref pagenum);
                    foreach (FoodInfo food in nextfoods)
                    {
                        if (food != null && !String.IsNullOrEmpty(food.Name))
                            _myfoodList.Add(food);
                    }
                }
            }

            int num = 0;
            foreach (FoodInfo food in _myfoodList)
            {
                ++num;
                if (num == _myfoodList.Count)
                    SetMessage(food.Name + "(" + food.Num.ToString() + ")");
                else
                    SetMessage(food.Name + "(" + food.Num.ToString() + "),");
            }
        }
        #endregion

        #region BuyCalf
        private void BuyCalf(RanchInfo ranch)
        {
            //购买幼仔
            SetMessageLn("购买幼仔...");

            if (ranch.Animals.Count >= MAX_ANIMAL_COUNT)
            {
                SetMessage(string.Format("你最多能养{0}只动物", MAX_ANIMAL_COUNT));
                return;
            }

            _canbuyanimals = true;
            int count = ranch.Animals.Count;
            bool isbuyable = true;

            if (Task.BuyCalfByPrice)
            {
                for (int ix = _calfsList.Count - 1; ix >= 0; ix--)
                {
                    try
                    {
                        isbuyable = true;
                        int buytimes = 0;
                        while (count < MAX_ANIMAL_COUNT)
                        {
                            //防止死循环
                            if (buytimes > 20)
                                break;
                            else
                                buytimes++;

                            SetMessageLn(_calfsList[ix].Name + "：");
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            //http://www.kaixin001.com/!house/!ranch//buyanimals.php
                            //num=1&id=1&verify=6209137%5F1062%5F6209137%5F1241620805%5F99a513746e0e8302268413d62b58b409
                            string content = HH.Post("http://www.kaixin001.com/!house/!ranch//buyanimals.php", string.Format("num=1&id={0}&verify={1}", _calfsList[ix].AId, DataConvert.GetEncodeData(this._verifyCode)));
                            if (GetBuyCalfFeedback(content, _calfsList[ix].Name, 1, ref isbuyable))
                                count++;
                            if (!isbuyable)
                                break;
                            if (!_canbuyanimals)
                                break;
                        }
                        if (!_canbuyanimals)
                            break;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GameRanch.BuyCalf", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
            else
            {
                int buytimes = 0;
                while (count < MAX_ANIMAL_COUNT)
                {
                    try
                    {
                        isbuyable = true;
                        //防止死循环
                        if (buytimes > 20)
                            break;
                        else
                            buytimes++;

                        CalfInfo calf = GetCalfById(Task.BuyCalfCustom);
                        SetMessageLn(calf.Name + "：");
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        string content = HH.Post("http://www.kaixin001.com/!house/!ranch//buyanimals.php", string.Format("num=1&id={0}&verify={1}", calf.AId, DataConvert.GetEncodeData(this._verifyCode)));
                        if (GetBuyCalfFeedback(content, calf.Name, 1, ref isbuyable))
                            count++;
                        if (!isbuyable)
                            break;
                        if (!_canbuyanimals)
                            break;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GameRanch.BuyCalf", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
        }
        #endregion

        #region GetBuyCalfFeedback
        private bool GetBuyCalfFeedback(string content, string name, int count, ref bool isbuyable)
        {
            //<data><ret>succ</ret></data>
            //<data><ret>fail</ret><reason>你必须成功收获第一只芦花母鸡后，才能养更多的芦花母鸡</reason></data>
            if (String.IsNullOrEmpty(content))
                return true;

            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 购买失败！");
                //LogHelper.Write(CurrentAccount.UserName, JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 失败！", LogSeverity.Warn);
                if (content.IndexOf("你的现金不够，不能购买") > -1)
                {
                    _canbuyanimals = false;
                }
                else if (content.IndexOf("参数错误") > -1)
                {
                    _canbuyanimals = false;
                }
                else if (content.IndexOf("你必须成功向系统卖出第一只") > -1)
                {
                    isbuyable = false;
                }
                //你目前的级别最多能养6只动物
                Regex regular = new Regex(@"你目前的级别最多能养[\d]+只动物");
                if (regular.IsMatch(content))
                {
                    _canbuyanimals = false;
                    return false;
                }
                Regex regularMax = new Regex(@"每人最多养[\d]+只动物");
                if (regularMax.IsMatch(content))
                {
                    _canbuyanimals = false;
                    return false;
                }
                //你的技能要到45级才饲养该动物
                Regex regularRank = new Regex(@"你的技能要到[\d]+级才饲养该动物");
                if (regularRank.IsMatch(content))
                {
                    isbuyable = false;
                    return false;
                }
                //你的牧场中，只能养2只袋鼠
                Regex regularCount = new Regex(@"你的牧场中，只能养[\d]+");
                if (regularCount.IsMatch(content))
                {
                    isbuyable = false;
                    return false;
                }                
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                SetMessage("购买" + count.ToString() + "个" + name + "完成！");
                return true;
            }
            else
            {
                SetMessage(content);
            }
            return false;
        }
        #endregion

        #region HarvestProduct
        private void HarvestProduct(RanchInfo ranch)
        {
            try
            {
                SetMessageLn("开始收获农副产品...");

                Collection<AnimalProductInfo> animalProducts = ranch.AnimalProducts;
                if (animalProducts == null || animalProducts.Count == 0)
                {
                    SetMessage("没有农副产品可收获");
                    return;
                }
                int num = 0;
                foreach (AnimalProductInfo animalProduct in animalProducts)
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0} {1} 剩余数量：{2} ", ++num, animalProduct.PName, (animalProduct.Num - animalProduct.StealNum)));
                        //http://www.kaixin001.com/!house/!ranch//havest.php
                        //foodnum=1&seedid=0&fuid=0&r=0%2E7162455567158759&verify=6195212%5F1062%5F6195212%5F1241619244%5Fd06fa48b2750f2b126897bb02fa0e3bf&type=0&skey=hen
                        //fuid=0&skey=hen&foodnum=1&seedid=0&type=1&r=0%2E44923274079337716&verify=6208872%5F1062%5F6208872%5F1241849557%5Fb93b666bf7fda774bdedececd2870d72
                        //fuid=0&skey=hen&foodnum=1&seedid=0&type=0&r=0%2E38333278289064765&verify=6208872%5F1062%5F6208872%5F1241849557%5Fb93b666bf7fda774bdedececd2870d72
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        string content = HH.Post("http://www.kaixin001.com/!house/!ranch//havest.php", string.Format("fuid={0}&skey={1}&foodnum=1&seedid=0&type={2}&r=0%2E44923274079337716&verify={3}", 0, animalProduct.SKey, animalProduct.Type, DataConvert.GetEncodeData(this._verifyCode)));
                        GetStealProductFeedback(content, false);
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
                        LogHelper.Write("GameRanch.HarvestProduct", ex, LogSeverity.Error);
                        SetMessage("收获" + animalProduct.PName+ "失败！错误：" + ex.Message);
                        continue;
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
                LogHelper.Write("GameRanch.HarvestProduct", ex, LogSeverity.Error);
                SetMessage("收获失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region HarvestAndProductAnimal
        private void HarvestAndProductAnimal(RanchInfo ranch)
        {
            try
            {
                SetMessageLn("开始生产/收获动物...");

                Collection<AnimalInfo> animals = ranch.Animals;
                Collection<AnimalInfo> newlist = new Collection<AnimalInfo>();
                if (animals == null || animals.Count == 0)
                {
                    return;
                }
                int num = 0;
                foreach (AnimalInfo animal in animals)
                {
                    try
                    {
                        bool harvested = false;
                        if (animal.BProduct == 2 || animal.BStat == 2)
                        {
                            SetMessageLn(string.Format("第{0}个动物 {1}：", ++num, animal.AName));
                        }                       

                        if (animal.BProduct == 2)
                        {
                            SetMessage(" 可以生产：");
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//product.php?fuid={0}&animalsid={1}&r=0%2E20183774875476956&verify={2}", "0", animal.AnimalSid, DataConvert.GetEncodeData(this._verifyCode)));
                            GetMakeProductFeedback(content);
                        }

                        if (animal.BStat == 2)
                        {
                            SetMessage(" 可以收获：");
                            //http://www.kaixin001.com/!house/!ranch//mhavest.php?verify=7998514%5F1062%5F7998514%5F1258905868%5F92f3a7fb3bf711c0e7191e6dc53c8d33&fuid=0&animalsid=2174827016&r=0%2E2255321340635419
                            //<data><ret>succ</ret><mpic>http://img.kaixin001.com.cn//i2/house/ranch/animals2/hedgehogmeat.swf</mpic><cash>0</cash></data>
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//mhavest.php?verify={0}&fuid=0&animalsid={1}&r=0%2E05614474741742015", DataConvert.GetEncodeData(this._verifyCode), animal.AnimalSid));
                            GetHarvestAnimalFeedback(content, ref harvested);
                        }
                        //如果没有被收获，加入到新的list中
                        if (!harvested)
                            newlist.Add(animal);
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
                        LogHelper.Write("GameRanch.HarvestProduct", ex, LogSeverity.Error);
                        SetMessage("收获" + animal.AName + "失败！错误：" + ex.Message);
                        continue;
                    }
                }
                ranch.Animals = newlist;
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
                LogHelper.Write("GameRanch.HarvestProduct", ex, LogSeverity.Error);
                SetMessage("收获失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetMakeProductFeedback
        private void GetMakeProductFeedback(string content)
        {
            try
            {
                //<data><action>product</action><ret>fail</ret><reason>该动物已无生产能力。</reason></data>
                //<data><action>product</action><ret>fail</ret><reason>该动物挨饿中，不能生产</reason></data>
                //<data><action>product</action><ret>succ</ret><skey>hen</skey><ptips>已成功将朱自克的芦花母鸡赶去产蛋&lt;br&gt;产蛋需10分种，10分钟后再来偷</ptips><bproduct>1</bproduct><leftptime>10</leftptime><tips>&lt;font color='#FF0000'&gt;产蛋中&lt;/font&gt;&lt;br&gt;预计产量：10&lt;br&gt;&lt;font color='#666666'&gt;距离可收获还有10分&lt;/font&gt;</tips><pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic><tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;芦花母鸡</tname></data>

                MakeProductInfo objMakeProduct = ConfigCtrl.ConvertToMakeProductObject(content);
                if (objMakeProduct == null)
                {
                    SetMessage("操作发生异常，生产失败！");
                }
                else
                {
                    if (objMakeProduct.Ret == "succ")
                    {
                        SetMessage(string.Format("{0} 生产成功！", JsonHelper.FiltrateHtmlTags(objMakeProduct.PTips)));
                    }
                    else if (objMakeProduct.Ret == "fail")
                        SetMessage(string.Format("{0} 生产失败！", objMakeProduct.Reason));
                    else
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
                LogHelper.Write("GameRanch.GetMakeProductFeedback", content, ex, LogSeverity.Error);
                //return true;
            }
        }
        #endregion

        #region GetHarvestAnimalFeedback
        private bool GetHarvestAnimalFeedback(string content, ref bool harvested)
        {
            //<data><ret>succ</ret><mpic>http://img.kaixin001.com.cn//i2/house/ranch/animals/chichen.swf</mpic></data>
            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                SetMessage(content);
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                harvested = true;
                SetMessage("收获完成！");
                return true;
            }
            else
            {
                SetMessage(content);
            }
            return false;
        }
        #endregion
        
        #region AddWater
        private void AddWater(string uid, RanchInfo ranch)
        {
            try
            {
                SetMessageLn("开始添水...");
                //水量：0格/<font color='#FF0000'>需加水</font> 
                if (!(ranch.WaterTips.IndexOf("需加水") > -1 || ranch.WaterTips.IndexOf("需添加") > -1 || ranch.Water < 50))
                {
                    SetMessage("水量为" + ranch.Water.ToString() + "格，无需添加");
                    return;
                }
                //http://www.kaixin001.com/!house/!ranch//water.php
                //seedid=0&type=0&skey=&fuid=0&r=0%2E21396516868844628&verify=2588258%5F1062%5F2588258%5F1241444968%5F9ddee5e84226f10e772e23fa7b5d3d8a
                HH.DelayedTime = Constants.DELAY_1SECONDS;
                string content = HH.Post("http://www.kaixin001.com/!house/!ranch//water.php", string.Format("seedid=0&type=0&skey=&fuid={0}&r=0%2E21396516868844628&verify={1}", uid, DataConvert.GetEncodeData(this._verifyCode)));
                GetAddWaterFeedback(content);
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
                LogHelper.Write("GameRanch.AddWater", ex, LogSeverity.Error);
                SetMessage("添水失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetAddWaterFeedback
        private void GetAddWaterFeedback(string content)
        {
            try
            {
                //<data><ret>succ</ret><watertips>水量：100格&lt;br&gt;&lt;font color='#666666'&gt;距喝光还有约400小时&lt;/font&gt;</watertips><tips></tips></data>
                //<data><ret>fail</ret><reason>水量多于30格，不需添水</reason></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 添水失败！");
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    WaterInfo objWater = ConfigCtrl.ConvertToWaterObject(content);
                    if (objWater == null)
                    {
                        SetMessage(content);
                        SetMessage("操作发生异常，添水失败！");
                    }
                    else
                    {
                        SetMessage(string.Format("{0} 添水成功！", JsonHelper.FiltrateHtmlTags(objWater.WaterTips)));
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
                LogHelper.Write("GameRanch.GetAddWaterFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region AddGrass
        private void AddGrass(string fuid, RanchInfo ranch)
        {
            try
            {
                SetMessageLn("开始添牧草...");
                //牧草：30棵<font color='#FF0000'>(需加草)</font><br><font color='#666666'>距吃光还有约20小时</font>
                if (!(ranch.GrassTips.IndexOf("需加草") > -1 || ranch.GrassTips.IndexOf("需添加") > -1 || ranch.Grass < 150))
                {
                    SetMessage("还有" + ranch.Grass.ToString() + "颗牧草，无需添加");
                    return;
                }
                if (ranch.GrassTips.IndexOf("不需加") > -1)
                {
                    SetMessage("还没有吃该饲料的动物，不需加");
                    return;
                }
                if (!_canaddgrass || _myfoodList == null || _myfoodList.Count == 0)
                {
                    SetMessage("没有饲料");
                    _canaddgrass = false;
                    return;
                }
                foreach (FoodInfo food in _myfoodList)
                {
                    if (food.SeedId != 63)
                        continue;

                    if (food.Num <= 0)
                    {
                        SetMessage(string.Format("没有{0}({1})了", food.Name, food.SeedId));
                        _canaddgrass = false;
                        continue;
                    }
                    //http://www.kaixin001.com/!house/!ranch//food.php
                    //seedid=0&type=0&skey=&fuid=0&r=0%2E21396516868844628&verify=2588258%5F1062%5F2588258%5F1241444968%5F9ddee5e84226f10e772e23fa7b5d3d8a
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    string content = HH.Post("http://www.kaixin001.com/!house/!ranch//food.php", string.Format("foodnum={0}&seedid={1}&fuid={2}&r=0%2E660982119385153&verify={3}&type=0&skey=", Math.Max(1, Math.Min(food.Num, Task.FoodNum)), food.SeedId, fuid, DataConvert.GetEncodeData(this._verifyCode)));
                    GetAddGrassFeedback(content, food);
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
                LogHelper.Write("GameRanch.AddGrass", ex, LogSeverity.Error);
                SetMessage("添牧草失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetAddGrassFeedback
        private void GetAddGrassFeedback(string content, FoodInfo food)
        {
            try
            {
                //<data><ret>fail</ret><reason>牧草多于100颗，无需添加牧草</reason></data>
                //<data><ret>fail</ret><reason>此次添牧草至少需要1牧草，你的牧草不足，不能添牧草。</reason></data>
                //<data><ret>succ</ret><grasstips>牧草：72棵&lt;font color='#FF0000'&gt;(需加草)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;距吃光还有约288小时&lt;/font&gt;</grasstips><grass>72</grass><animalstips></animalstips></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 添牧草失败！");
                    if (content.IndexOf("你的牧草不足") > -1)
                        _canaddgrass = false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    FeedInfo objFeed = ConfigCtrl.ConvertToFeedObject(content);
                    if (objFeed == null)
                    {
                        SetMessage(content);
                        SetMessage("操作发生异常，添牧草失败！");
                    }
                    else
                    {
                        SetMessage(string.Format("添{0}个牧草 {1} 添牧草成功！", objFeed.Grass, JsonHelper.FiltrateHtmlTags(objFeed.GrassTips)));
                        food.Num = food.Num - objFeed.Grass;
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
                LogHelper.Write("GameRanch.GetAddGrassFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region AddCarrot
        private void AddCarrot(string fuid, RanchInfo ranch)
        {
            try
            {
                SetMessageLn("开始添胡萝卜...");
                //胡萝卜：58棵&lt;font color='#FF0000'&gt;(需添加)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;距吃光还有约232小时&lt;/font&gt;
                Collection<FoodItemInfo> foods = ranch.Foods;
                foreach (FoodItemInfo fooditem in foods)
                {
                    if (fooditem.SeedId == 1)
                    {
                        if (!(fooditem.Tips.IndexOf("需添加") > -1 || fooditem.Grass < 150))
                        {
                            SetMessage("还有" + fooditem.Grass.ToString() + "个胡萝卜，无需添加");
                            return;
                        }
                        if (fooditem.Tips.IndexOf("不需加") > -1)
                        {
                            SetMessage("还没有吃该饲料的动物，不需加");
                            return;
                        }
                        if (!_canaddcarrot || _myfoodList == null || _myfoodList.Count == 0)
                        {
                            SetMessage("没有饲料");
                            _canaddcarrot = false;
                            return;
                        }
                        foreach (FoodInfo food in _myfoodList)
                        {
                            if (food.SeedId != 1)
                                continue;

                            if (food.Num <= 0)
                            {
                                SetMessage(string.Format("没有{0}({1})了", food.Name, food.SeedId));
                                _canaddcarrot = false;
                                continue;
                            }
                            //http://www.kaixin001.com/!house/!ranch//food.php
                            //seedid=0&type=0&skey=&fuid=0&r=0%2E21396516868844628&verify=2588258%5F1062%5F2588258%5F1241444968%5F9ddee5e84226f10e772e23fa7b5d3d8a
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            string content = HH.Post("http://www.kaixin001.com/!house/!ranch//food.php", string.Format("foodnum={0}&seedid={1}&fuid={2}&r=0%2E660982119385153&verify={3}&type=0&skey=", Math.Max(1, Math.Min(food.Num, Task.CarrotNum)), food.SeedId, fuid, DataConvert.GetEncodeData(this._verifyCode)));
                            GetAddCarrotFeedback(content, food);
                        }
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
                LogHelper.Write("GameRanch.AddGrass", ex, LogSeverity.Error);
                SetMessage("添牧草失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetAddCarrotFeedback
        private void GetAddCarrotFeedback(string content, FoodInfo food)
        {
            try
            {
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 添胡萝卜失败！");
                    if (content.IndexOf("你的胡萝卜不足") > -1)
                        _canaddcarrot = false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    FeedInfo objFeed = ConfigCtrl.ConvertToFeedObject(content);
                    if (objFeed == null)
                    {
                        SetMessage(content);
                        SetMessage("操作发生异常，添胡萝卜失败！");
                    }
                    else
                    {
                        SetMessage(string.Format("添{0}个胡萝卜 {1} 添胡萝卜成功！", objFeed.Grass, JsonHelper.FiltrateHtmlTags(objFeed.GrassTips)));
                        food.Num = food.Num - objFeed.Grass;
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
                LogHelper.Write("GameRanch.GetAddGrassFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region AddBamboo
        private void AddBamboo(string fuid, RanchInfo ranch)
        {
            try
            {
                SetMessageLn("开始添竹子...");
                //<tips>竹子：198棵&lt;br&gt;&lt;font color='#666666'&gt;距吃光还有约396小时&lt;/font&gt;</tips>
                Collection<FoodItemInfo> foods = ranch.Foods;
                foreach (FoodItemInfo fooditem in foods)
                {
                    if (fooditem.SeedId == 95)
                    {
                        if (!(fooditem.Tips.IndexOf("需添加") > -1 || fooditem.Grass < 150))
                        {
                            SetMessage("还有" + fooditem.Grass.ToString() + "颗竹子，无需添加");
                            return;
                        }
                        if (fooditem.Tips.IndexOf("不需加") > -1 || fooditem.Tips.IndexOf("没有动物食用该饲料") > -1)
                        {
                            SetMessage("还没有吃该饲料的动物，不需加");
                            return;
                        }
                        if (!_canaddbamboo || _myfoodList == null || _myfoodList.Count == 0)
                        {
                            SetMessage("没有饲料");
                            _canaddbamboo = false;
                            return;
                        }
                        foreach (FoodInfo food in _myfoodList)
                        {
                            if (food.SeedId != 95)
                                continue;

                            if (food.Num <= 0)
                            {
                                SetMessage(string.Format("没有{0}({1})了", food.Name, food.SeedId));
                                _canaddbamboo = false;
                                continue;
                            }
                            
                            //http://www.kaixin001.com/!house/!ranch//food.php
                            //id=0&foodnum=1&seedid=95&fuid=0&r=0%2E8225318137556314&verify=2588258%5F1062%5F2588258%5F1253623471%5F8292808da944c4f9fda51bbd00c276a3&skey=&type=0
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            string content = HH.Post("http://www.kaixin001.com/!house/!ranch//food.php", string.Format("foodnum={0}&seedid={1}&fuid={2}&r=0%2E660982119385153&verify={3}&type=0&skey=", Math.Max(1, Math.Min(food.Num, Task.BambooNum)), food.SeedId, fuid, DataConvert.GetEncodeData(this._verifyCode)));
                            GetAddBambooFeedback(content, food);
                        }
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
                LogHelper.Write("GameRanch.AddBamboo", ex, LogSeverity.Error);
                SetMessage("添竹子失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetAddBambooFeedback
        private void GetAddBambooFeedback(string content, FoodInfo food)
        {
            try
            {
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 添竹子失败！");
                    if (content.IndexOf("你的竹子不足") > -1)
                        _canaddbamboo = false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    FeedInfo objFeed = ConfigCtrl.ConvertToFeedObject(content);
                    if (objFeed == null)
                    {
                        SetMessage(content);
                        SetMessage("操作发生异常，添竹子失败！");
                    }
                    else
                    {
                        SetMessage(string.Format("添{0}颗竹子 {1} 添竹子成功！", objFeed.Grass, JsonHelper.FiltrateHtmlTags(objFeed.GrassTips)));
                        food.Num = food.Num - objFeed.Grass;
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
                LogHelper.Write("GameRanch.GetAddBambooFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region BreedAnimal
        private void BreedAnimal()
        {
            try
            {
                SetMessageLn("开始配种...");

                string content = RequestBreedableList();
                Collection<BreedableInfo> breedableAnimals = ConfigCtrl.GetBreedAnimals(content);
                if (breedableAnimals == null || breedableAnimals.Count == 0)
                {
                    SetMessage("没有可以配种的动物");
                    return;
                }

                int num = 0;
                foreach (BreedableInfo animal in breedableAnimals)
                {
                    try
                    {
                        SetMessageLn(string.Format("可配种的第{0}个动物 {1}：", ++num,GetCalfNameById(animal.Aid)));
                        content = RequestFriendTools(animal.BsKey);
                        Collection<BreedCardInfo> breedcards = ConfigCtrl.GetBreedCards(content);
                        if (breedcards == null || breedcards.Count == 0)
                        {
                            SetMessage("没有可以配种的机会");
                            break;
                        }

                        foreach (BreedCardInfo card in breedcards)
                        {
                            try
                            {
                                SetMessage(JsonHelper.FiltrateHtmlTags(card.TipsText) + "=>");
                                //http://www.kaixin001.com/!house/!ranch//breed.php?fuid=6195212&animalsid=11470716&verify=6194153%5F1062%5F6194153%5F1241698572%5F7b5ae0a171cca7d89c788427337fb7e9
                                HH.DelayedTime = Constants.DELAY_2SECONDS;
                                content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//breed.php?fuid={0}&animalsid={1}&verify={2}", card.Fuid, animal.AnimalSid, DataConvert.GetEncodeData(this._verifyCode)));
                                if (GetBreedAnimalFeedback(content))
                                    break;
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                    catch
                    {
                        continue;
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
                LogHelper.Write("GameRanch.MakeProduct", CurrentAccount.UserName, ex, LogSeverity.Error);
                SetMessage(" 帮忙生产" + CurrentAccount.UserName + "的牧场失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetBreedAnimalFeedback
        private bool GetBreedAnimalFeedback(string content)
        {
            try
            {
                //<data><ret>fail</ret><reason>参数错误，请检查</reason></data>
                //<data><ret>succ</ret><succtips>你的芦花母鸡和朱自克公鸡卡在产蛋期配种成功!&lt;br&gt;24小时内将产下柴鸡蛋（每只30元）</succtips><bproduct>0</bproduct><leftptime>0</leftptime><tips>产蛋期&lt;font color='#FF0000'&gt;(已配种)&lt;/font&gt;&lt;br&gt;距离下次产蛋：6分&lt;br&gt;预计产量：10&lt;br&gt;&lt;font color='#666666'&gt;距不能产蛋还有2天9小时51分&lt;/font&gt;</tips><skey>hen</skey><pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic><tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;芦花母鸡</tname><animalsid>6233300</animalsid></data>

                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 配种失败！");                    
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    BreedAnimalInfo objBreedAnimal = ConfigCtrl.ConvertToBreedAnimalObject(content);
                    if (objBreedAnimal == null)
                        SetMessage("操作发生异常，配种失败！");
                    else
                    {
                        SetMessage(string.Format("{0} 配种成功！", JsonHelper.FiltrateHtmlTags(objBreedAnimal.Succtips)));
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
                LogHelper.Write("GameRanch.GetBreedAnimalFeedback", content, ex, LogSeverity.Error);
                //return true;
            }
            return true;
        }
        #endregion

        #region StealRanchs
        private void StealRanchs()
        {
            int num = 0;

            SetMessageLn("开始偷农副产品：");
            //先偷白名单中的人
            SetMessageLn("开始偷白名单中的人：");
            foreach (int uid in Operation.StealProductWhiteList)
            {
                try
                {
                    SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");
                    FriendInfo friend = GetHelpRanchFriendById(uid);
                    if (friend == null || friend.RanchHarvest == false)
                    {
                        SetMessage("没什么可偷的，跳过");
                        continue;
                    }
                    if (Operation.StealProductBlackList.Contains(uid))
                    {
                        SetMessage(base.GetFriendNameById(uid) + "在黑名单中，跳过");
                        continue;
                    }
                    StealTheRanch(uid.ToString());
                    if (this._canstealproduct == false)
                    {
                        SetMessageLn("由于你今天不能再偷了，停止偷农副产品！");
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
                    LogHelper.Write("GameRanch.StealRanchs", ex, LogSeverity.Error);
                    continue;
                }
            }

            //偷剩下的人
            if (Operation.StealProductAll)
            {
                num = 0;
                SetMessageLn("去其他有成熟农副产品的牧场偷：");
                foreach (FriendInfo friend in this._ranchFriendsList)
                {
                    try
                    {
                        if (!friend.RanchHarvest)
                            continue;
                        if (Operation.StealProductWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                            continue;                        

                        SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                        if (Operation.StealProductBlackList.Contains(friend.Id))
                        {
                            SetMessage(friend.Name + "在黑名单中，跳过");
                            continue;
                        }                        
                        StealTheRanch(friend.Id.ToString());
                        if (this._canstealproduct == false)
                        {
                            SetMessageLn("由于你今天不能再偷了，停止偷农副产品！");
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
                        LogHelper.Write("GameRanch.StealRanchs", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
        }
        #endregion 

        #region StealTheRanch
        private void StealTheRanch(string fuid)
        {
            try
            {
                string content = RequestRanch(fuid);
                string verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");

                RanchInfo ranch = ReadRanch(verifyCode, fuid, true);
                if (ranch == null)
                {
                    SetMessageLn("无法读取牧场信息，跳过");
                }
                else
                {
                    if (!_canstealproduct)
                        return;

                    Collection<AnimalProductInfo> animalProducts = ranch.AnimalProducts;
                    if (animalProducts == null || animalProducts.Count == 0)
                    {
                        SetMessageLn("没有农副产品可偷，跳过");
                        return;
                    }

                    int num = 0;
                    foreach (AnimalProductInfo animalProduct in animalProducts)
                    {
                        try
                        {
                            if (animalProduct.Num > animalProduct.StealNum)
                            {
                                SetMessageLn(string.Format("#{0} {1} 剩余数量：{2} ", ++num, animalProduct.PName, (animalProduct.Num - animalProduct.StealNum)));
                                //http://www.kaixin001.com/!house/!ranch//havest.php
                                //fuid=2479166&skey=hen&foodnum=1&seedid=0&type=0&r=0%2E7316347765736282&verify=6208872%5F1062%5F6208872%5F1241850347%5F27cdf7d19c959f85c71064a4745c992d
                                HH.DelayedTime = Constants.DELAY_1SECONDS;
                                content = HH.Post("http://www.kaixin001.com/!house/!ranch//havest.php", string.Format("fuid={0}&skey={1}&foodnum=1&seedid=0&type={2}&r=0%2E7316347765736282&verify={3}", animalProduct.Uid, animalProduct.SKey, animalProduct.Type, DataConvert.GetEncodeData(verifyCode)));
                                _canstealproduct = GetStealProductFeedback(content, true);
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
                            LogHelper.Write("GameRanch.StealTheRanch", ex, LogSeverity.Error);
                            SetMessage("偷" + animalProduct.PName + "失败！错误：" + ex.Message);
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
                LogHelper.Write("GameRanch.StealTheRanch", GetFriendNameById(fuid), ex, LogSeverity.Error);
                SetMessage(" 偷" + GetFriendNameById(fuid) + "的牧场失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetStealProductFeedback
        private bool GetStealProductFeedback(string content, bool issteal)
        {
            try
            {
                //<data>
                //  <ptype>1</ptype>
                //  <skey>hen</skey>
                //  <action>steal</action>
                //  <num>1</num>
                //  <ppic>http://img.kaixin001.com.cn//i2/house/ranch/animals/egg.swf</ppic>
                //  <ret>succ</ret>
                //</data>

                //<data>
                //  <ptype>1</ptype>
                //  <skey>hen</skey>
                //  <action>steal</action>
                //  <ret>fail</ret>
                //  <reason>已偷过，做人要厚道</reason>
                //</data>
                StealProductInfo objStealProduct = ConfigCtrl.ConvertToStealProductObject(content);
                if (objStealProduct == null)
                {
                    SetMessage(content);
                    if (issteal)
                        SetMessage("操作发生异常，偷窃失败！");
                    else
                        SetMessage("操作发生异常，收获失败！");
                }
                else
                {
                    if (objStealProduct.Ret == "succ")
                    {
                        if (issteal)
                            SetMessage(string.Format("{0}个 偷窃成功！", objStealProduct.Num));
                        else
                            SetMessage(string.Format("{0}个 收获成功！", objStealProduct.Num));
                    }
                    else if (objStealProduct.Ret == "fail")
                    {
                        if (issteal)
                            SetMessage(string.Format("{0} 偷窃失败！", objStealProduct.Reason));
                        else
                            SetMessage(string.Format("{0} 收获失败！", objStealProduct.Reason));
                    }
                    else
                        SetMessage(content);
                }

                if (content.IndexOf("今天不能再偷了") > -1)
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
                LogHelper.Write("GameRanch.GetStealProductFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region HelpMakeProduct
        private void HelpMakeProduct()
        {
            int num = 0;

            SetMessageLn("开始帮忙生产：");
            //先帮忙生产白名单中的人
            SetMessageLn("开始帮忙生产白名单中的人：");
            foreach (int uid in Operation.HelpRanchWhiteList)
            {
                try
                {
                    SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");
                    FriendInfo friend = GetHelpRanchFriendById(uid);
                    if (friend == null || friend.RanchProduct == false)
                    {
                        SetMessage("没什么可帮忙的，跳过");
                        continue;
                    }
                    if (Operation.HelpRanchBlackList.Contains(uid))
                    {
                        SetMessage(base.GetFriendNameById(uid) + "在黑名单中，跳过");
                        continue;
                    }
                    HelpTheMakeProduct(uid.ToString());
                    //if (this._canstealproduct == false)
                    //{
                    //    SetMessageLn("由于你今天不能再偷了，停止偷农副产品！");
                    //    return;
                    //}
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
                    LogHelper.Write("GameRanch.HelpMakeProduct", ex, LogSeverity.Error);
                    continue;
                }
            }

            //剩下的人
            if (Operation.HelpRanchAll)
            {
                num = 0;
                SetMessageLn("去其他可以生产的牧场：");
                foreach (FriendInfo friend in this._ranchFriendsList)
                {
                    try
                    {
                        if (!friend.RanchProduct)
                            continue;
                        if (Operation.HelpRanchWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                            continue;

                        SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                        if (Operation.HelpRanchBlackList.Contains(friend.Id))
                        {
                            SetMessage(friend.Name + "在黑名单中，跳过");
                            continue;
                        }
                        HelpTheMakeProduct(friend.Id.ToString());
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
                        LogHelper.Write("GameRanch.HelpMakeProduct", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
        }
        #endregion
        
        #region HelpTheMakeProduct
        private void HelpTheMakeProduct(string fuid)
        {
            try
            {
                string content = RequestRanch(fuid);
                string verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");

                RanchInfo ranch = ReadRanch(verifyCode, fuid, true);
                if (ranch == null)
                {
                    SetMessageLn("无法读取牧场信息，跳过");
                }
                else
                {
                    try
                    {
                        int num = 0;
                        foreach (AnimalInfo animal in ranch.Animals)
                        {
                            try
                            {
                                if (animal.BProduct == 2)
                                {
                                    SetMessageLn(string.Format("第{0}个动物 {1} {2}：", ++num, animal.AName, animal.PAction));
                                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                                    content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//product.php?fuid={0}&animalsid={1}&r=0%2E20183774875476956&verify={2}", fuid, animal.AnimalSid, DataConvert.GetEncodeData(verifyCode)));
                                    GetMakeProductFeedback(content);
                                }
                            }
                            catch
                            {
                                continue;
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
                        LogHelper.Write("GameRanch.HelpTheMakeProduct", GetFriendNameById(fuid), ex, LogSeverity.Error);
                        return;
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
                LogHelper.Write("GameRanch.HelpTheMakeProduct", GetFriendNameById(fuid), ex, LogSeverity.Error);
                SetMessage(" 帮忙生产" + GetFriendNameById(fuid) + "的牧场失败！错误：" + ex.Message);
            }
        }
        #endregion
        
        #region HelpOthersRanchs
        private void HelpOthersRanchs()
        {
            try
            {
                int num = 0;

                SetMessageLn("开始去好友的牧场帮忙：");

                if (Task.HelpAddWater || Task.HelpAddGrass || Task.HelpAddCarrot || Task.HelpAddBamboo)
                {
                    //先去白名单中的花园播种
                    SetMessageLn("开始去白名单中好友的牧场帮忙：");
                    foreach (int uid in Operation.HelpRanchWhiteList)
                    {
                        try
                        {
                            if (!base.IsAlreadyMyFriend(uid.ToString()))
                            {
                                SetMessageLn(string.Format("#{0} ID:{1}不是你的好友", ++num, uid));
                                LogHelper.Write("HelpOthersRanchs.HelpRanchWhiteList", "(" + uid + ")不是" + CurrentAccount.UserName + "的好友", LogSeverity.Warn);
                                continue;
                            }

                            SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");                            

                            //FriendInfo friend = GetHelpRanchFriendById(uid);
                            //if (friend == null)
                            //{
                            //    SetMessage("没什么可帮忙的，跳过");
                            //    continue;
                            //}
                            if (Operation.HelpRanchBlackList.Contains(uid))
                            {
                                SetMessage(base.GetFriendNameById(uid) + "在帮忙黑名单中，跳过");
                                continue;
                            }
                            HelpTheRanch(uid.ToString(), null, Task.HelpAddWater, Task.HelpAddGrass, Task.HelpAddCarrot, Task.HelpAddBamboo);                            
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
                            LogHelper.Write("GameRanch.HelpOthersRanchs", GetFriendNameById(uid), ex, LogSeverity.Error);
                            continue;
                        }
                    }
                }

                if ((Task.HelpAddWater || Task.HelpAddGrass || Task.HelpAddCarrot || Task.HelpAddBamboo) && Operation.HelpRanchAll)
                {
                    //其他好友
                    num = 0;
                    SetMessageLn("开始去其他好友的牧场帮忙：");
                    foreach (FriendInfo friend in this._ranchFriendsList)
                    {
                        try
                        {
                            if (Operation.HelpRanchWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                                continue;

                            SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                            if (Operation.HelpRanchBlackList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "在帮忙黑名单中，跳过");
                                continue;
                            }
                            HelpTheRanch(friend.Id.ToString(), friend, Task.HelpAddWater, Task.HelpAddGrass, Task.HelpAddCarrot, Task.HelpAddBamboo);                            
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
                            LogHelper.Write("GameRanch.HelpOthersRanchs", friend.Name, ex, LogSeverity.Error);
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
                LogHelper.Write("GameRanch.HelpOthersRanchs", ex, LogSeverity.Error);
            }
        }
        #endregion

        #region LackOfFood
        private bool LackOfFood()
        {
            if (_myfoodList == null || _myfoodList.Count == 0)
                return true;
            foreach (FoodInfo food in _myfoodList)
            {
                if (food != null && food.Num > 0)
                    return false;
            }
            return true;
        }
        #endregion

        #region HelpTheRanch
        private void HelpTheRanch(string fuid, FriendInfo friend, bool helpaddwater, bool helpaddgrass, bool helpaddcarrot, bool helpaddbamboo)
        {
            try
            {
                string content = RequestRanch(fuid);
                string verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");

                RanchInfo ranch = ReadRanch(verifyCode, fuid, true);
                if (ranch == null)
                {
                    SetMessage("无法读取牧场信息，跳过");
                }
                if (friend != null)
                {
                    if (helpaddwater && friend.RanchWater)
                        AddWater(fuid, ranch);
                    if (helpaddgrass && friend.RanchFood)
                        AddGrass(fuid, ranch);
                    if (helpaddcarrot && friend.RanchFood)
                        AddCarrot(fuid, ranch);
                    if (helpaddbamboo && friend.RanchFood)
                        AddBamboo(fuid, ranch);
                }
                else
                {
                    if (helpaddwater)
                        AddWater(fuid, ranch);
                    if (helpaddgrass)
                        AddGrass(fuid, ranch);
                    if (helpaddcarrot)
                        AddCarrot(fuid, ranch);
                    if (helpaddbamboo)
                        AddBamboo(fuid, ranch);
                }


                //_hasNothingTobeHelpedList.Add(DataConvert.GetInt32(fuid));
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
                LogHelper.Write("GameRanch.HelpTheRanch", ex, LogSeverity.Error);
                SetMessage(" 去" + GetFriendNameById(fuid) + "的牧场帮忙失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region PresentAnimalProduct
        private void PresentAnimalProduct()
        {
            try
            {
                SetMessageLn("开始赠送农副产品...");
                if (Operation.PresentProductId == 0)
                {
                    SetMessage("没有设定赠送的对象，无法赠送");
                    return;
                }
                if (!IsAlreadyMyFriend(DataConvert.GetString(Operation.PresentProductId)))
                {
                    SetMessage(DataConvert.GetString(Operation.PresentProductId) + "不是你的好友，无法赠送");
                    return;
                }

                string content = RequestMyRanchWarehouse();
                Collection<ProductInfo> products = ConfigCtrl.GetMyWarehouseProduct(content);
                if (products == null || products.Count == 0)
                {
                    SetMessage("仓库里没有任何农副产品");
                    return;
                }

                int num = 0;
                string productname = "";
                int price = 0;
                int count = 0;
                int type = 0;
                int aid = 0;
                long totalprice = 0;
                long tempprice = 0;

                _incorrentcount = false;
                if (Task.PresentProductByPrice)
                {
                    //计算价值最高
                    SetMessageLn("仓库里可以送的农副产品：");
                    
                    foreach (ProductInfo product in products)
                    {
                        ProductInfo productbase = GetAnimalProductByIdAndType(product.Aid, product.Type);
                        if (productbase == null)
                        {
                            SetMessage(string.Format("未知农副产品{0}-{1}，跳过 ", product.Aid, product.Type));
                            LogHelper.Write("GameRanch.PresentAnimalProduct" + CurrentAccount.UserName, string.Format("未知农副产品{0}-{1}，跳过 ", product.Aid, product.Type), LogSeverity.Warn);
                            return;
                        }

                        tempprice = productbase.Price * product.Num;
                        SetMessageLn(string.Format("#{0}{1}：数量：{2}，单个售价：{3}，总价值：{4}", ++num, product.Name, product.Num, productbase.Price, tempprice));
                        if (tempprice > totalprice)
                        {
                            totalprice = tempprice;
                            productname = product.Name;
                            price = productbase.Price;
                            count = product.Num;
                            type = product.Type;
                            aid = product.Aid;
                        }                       
                    }

                    if (totalprice <= 0)
                        return;

                    SetMessageLn(string.Format("计算出可以送的价值最高的农副产品：{0}:{1}*{2}={3}元", productname, count, price, totalprice));

                    if (Task.PresentProductCheckValue && totalprice < Task.PresentProductValue * 10000)
                    {
                        SetMessage(string.Format(" 总价值{0}小于最低赠送价值{1}，跳过", totalprice, Task.PresentProductValue * 10000));
                        return;
                    }

                    SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentProductId), count, productname, price, totalprice));
                    content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", count, DataConvert.GetEncodeData("送你农副产品啦！"), DataConvert.GetEncodeData(this._verifyCode), type, Operation.PresentProductId, aid));
                    if (GetPresentFeedback(content))
                        return;

                    if (_incorrentcount)
                    {
                        content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//myfruitinfo.php?type={0}&id={1}&verify={2}", type, aid, this._verifyCode));
                        if (!String.IsNullOrEmpty(content) && content.IndexOf("<ret>succ</ret>") > -1)
                        {
                            tempprice = JsonHelper.GetMidInteger(content, "<selfnum>", "</selfnum>") * JsonHelper.GetMidInteger(content, "<price>", "</price>");
                            if (JsonHelper.GetMidInteger(content, "<selfnum>", "</selfnum>") > 0)
                            {
                                SetMessageLn(string.Format("#{0}{1}：可送数量：{2}，单个售价：{3}，总价值：{4}", ++num, JsonHelper.GetMid(content, "<name>", "</name>"), JsonHelper.GetMid(content, "<selfnum>", "</selfnum>"), JsonHelper.GetMid(content, "<price>", "</price>"), tempprice));
                                totalprice = tempprice;
                                price = JsonHelper.GetMidInteger(content, "<price>", "</price>");
                                count = JsonHelper.GetMidInteger(content, "<selfnum>", "</selfnum>");                                

                                if (Task.PresentProductCheckValue && totalprice < Task.PresentProductValue * 10000)
                                {
                                    SetMessage(string.Format("总价值{0}小于最低赠送价值{1}，跳过 ", totalprice, Task.PresentProductValue * 10000));
                                    return;
                                }
                                SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentProductId), count, productname, price, tempprice));
                                content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", count, DataConvert.GetEncodeData("送你农副产品啦！"), DataConvert.GetEncodeData(this._verifyCode), type, Operation.PresentProductId, aid));
                                GetPresentFeedback(content);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    SetMessageLn(string.Format("尝试赠送指定的农副产品：{0}...", GetAnimalProductNameByIdAndType(Task.PresentProductAid, Task.PresentProductType)));
                    foreach (ProductInfo product in products)
                    {
                        if (product.Aid == Task.PresentProductAid && product.Type == Task.PresentProductType)
                        {
                            if (Task.PresentProductCheckNum && product.Num < Task.PresentProductNum)
                            {
                                SetMessage(string.Format("数量{0}< 最小赠送数{1}，跳过 ", product.Num, Task.PresentProductNum));
                                return;
                            }
                            ProductInfo productbase = GetAnimalProductByIdAndType(product.Aid, product.Type);
                            if (productbase == null)
                            {
                                SetMessage(string.Format("未知农副产品{0}-{1}，跳过 ", product.Aid, product.Type));
                                LogHelper.Write("GameRanch.PresentAnimalProduct" + CurrentAccount.UserName, string.Format("未知农副产品{0}-{1}，跳过 ", product.Aid, product.Type), LogSeverity.Warn);
                                return;
                            }
                            SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentProductId), product.Num, product.Name, productbase.Price, productbase.Price * product.Num));
                            content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", product.Num, DataConvert.GetEncodeData("送你农副产品啦！"), DataConvert.GetEncodeData(this._verifyCode), product.Type, Operation.PresentProductId, product.Aid));
                            if (GetPresentFeedback(content))
                                return;

                            if (_incorrentcount)
                            {
                                content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//myfruitinfo.php?type={0}&id={1}&verify={2}", product.Type, product.Aid, this._verifyCode));
                                if (!String.IsNullOrEmpty(content) && content.IndexOf("<ret>succ</ret>") > -1)
                                {
                                    tempprice = JsonHelper.GetMidInteger(content, "<selfnum>", "</selfnum>") * JsonHelper.GetMidInteger(content, "<price>", "</price>");
                                    if (JsonHelper.GetMidInteger(content, "<selfnum>", "</selfnum>") > 0)
                                    {
                                        SetMessageLn(string.Format("#{0}{1}：可送数量：{2}，单个售价：{3}，总价值：{4}", ++num, JsonHelper.GetMid(content, "<name>", "</name>"), JsonHelper.GetMid(content, "<selfnum>", "</selfnum>"), JsonHelper.GetMid(content, "<price>", "</price>"), tempprice));
                                        if (tempprice > totalprice)
                                        {
                                            totalprice = tempprice;
                                            productname = JsonHelper.GetMid(content, "<name>", "</name>");
                                            price = JsonHelper.GetMidInteger(content, "<price>", "</price>");
                                            count = JsonHelper.GetMidInteger(content, "<selfnum>", "</selfnum>");
                                            type = product.Type;
                                            aid = product.Aid;
                                        }

                                        if (Task.PresentProductCheckNum && count < Task.PresentProductNum)
                                        {
                                            SetMessage(string.Format("可送数量{0}< 最小赠送数{1}，跳过 ", count, Task.PresentProductNum));
                                            return;
                                        }
                                        SetMessageLn(string.Format("向{0}赠送{1}个{2}(单价:{3})，总价值：{4}元 ", GetFriendNameById(Operation.PresentProductId), count, productname, price, tempprice));
                                        content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", count, DataConvert.GetEncodeData("送你农副产品啦！"), DataConvert.GetEncodeData(this._verifyCode), product.Type, Operation.PresentProductId, product.Aid));
                                        GetPresentFeedback(content);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    SetMessage("仓库中没有该农副产品，无法赠送。");
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
                LogHelper.Write("GameRanch.PresentAnimalProduct", GetFriendNameById(Operation.PresentProductId), ex, LogSeverity.Error);
                SetMessage(" 向" + GetFriendNameById(Operation.PresentId) + "赠送农副产品失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetPresentFeedback
        private bool GetPresentFeedback(string content)
        {
            try
            {
                //<data><ret>succ</ret><tips>已成功赠送给刘超超1只整鸡&lt;br&gt;刘超超会在系统消息中收到</tips></data>
                //<data><ret>fail</ret><reason>一天只能给同一个好友送一次农副产品</reason></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 赠送失败！");
                    if (content.Contains("数量不正确"))
                        _incorrentcount = true;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("succ", "") + " 赠送成功！");
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
                LogHelper.Write("GameRanch.GetPresentFeedback", content, ex, LogSeverity.Error);
            }
            return false;
        }
        #endregion

        #region SellProduct
        private void SellProduct(RanchInfo ranch)
        {
            try
            {
                SetMessageLn("开始出售农副产品...");
                if (Task.SellProductLowCash)
                {
                    if (ranch.Cash > Task.SellProductLowCashLimit * 10000)
                    {
                        SetMessageLn(string.Format("还有{0}元现金，无须出售。", ranch.Cash));
                        return;
                    }
                }

                if (Task.SellAllProducts)
                {
                    //http://www.kaixin001.com/!house/!ranch//sellfruit.php?type=2&all=1&num=1&id=1&verify=6194153%5F1062%5F6194153%5F1242912584%5F1bc688494cff4b5101818e3a76c3866a
                    string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//sellfruit.php?type=2&all=1&num=1&id=1&verify={0}", DataConvert.GetEncodeData(this._verifyCode)));
                    long productvalue = 0;
                    GetSellProductFeedback(content, ref productvalue);
                }
                else
                {
                    string content = RequestMyRanchWarehouse();
                    Collection<ProductInfo> products = ConfigCtrl.GetMyWarehouseProduct(content);
                    if (products == null || products.Count == 0)
                    {
                        SetMessage("仓库里没有任何农副产品");
                        return;
                    }

                    //计算价值
                    long soldvalue = 0;
                    long productvalue = 0;
                    int num = 0;
                    foreach (ProductInfo product in products)
                    {
                        productvalue = 0;
                        if (soldvalue >= Task.SellProductMaxLimit * 10000)
                        {
                            SetMessageLn(string.Format("已出售的农副产品总价值已经超过{0}万，停止出售。", Task.SellProductMaxLimit));
                            break;
                        }

                        SetMessageLn(string.Format("#{0} ", ++num));
                        if (IsContained(product.Aid, product.Type))
                        {
                            SetMessage(string.Format("{0}在出售的禁止列表中，跳过", product.Name));
                            continue;
                        }

                        int productprice = GetAnimalProductPriceByIdAndType(product.Aid, product.Type);
                        if (productprice <= 0)
                        {
                            SetMessage(string.Format("未知农副产品{0}-{1}，跳过", product.Aid, product.Type));
                            continue;
                        }
                        SetMessage(string.Format("{0} =>", product.Name));
                        double temp = (Task.SellProductMaxLimit * 10000 - soldvalue) / productprice;
                        if (temp < 1)
                            temp = 1;
                        int sellnum = Math.Min(DataConvert.GetInt32(Math.Ceiling(temp)), product.Num);
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//sellfruit.php?verify={0}&id={1}&num={2}&type={3}&all=0&r=0%2E1741087632253766", DataConvert.GetEncodeData(this._verifyCode), product.Aid, sellnum, product.Type));
                        if (GetSellProductFeedback(content, ref productvalue))
                        {
                            soldvalue += productvalue;
                            SetMessage(string.Format("已出售的农副产品总价值：{0}元", soldvalue));
                        }
                        if (sellnum < product.Num)
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
                LogHelper.Write("GameRanch.SellProduct", ex, LogSeverity.Error);
                SetMessage("出售农副产品失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region GetSellProductFeedback
        private bool GetSellProductFeedback(string content, ref long totalprice)
        {
            try
            {
                //<data><ret>fail</ret><reason>数量不对</reason></data>
                //<data><ret>succ</ret><all>0</all><num>1</num><totalprice>70000</totalprice><tips>你卖出小刺猬 &lt;font color='#008000'&gt;&lt;b&gt;1&lt;/b&gt;&lt;/font&gt;只&lt;br /&gt;共获得利润 &lt;font color='#FF6600'&gt;&lt;b&gt;70000&lt;/b&gt;&lt;/font&gt;元</tips></data>
                if (content.Contains("<ret>fail</ret>"))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " 出售失败！");
                }
                else if (content.Contains("<ret>succ</ret>"))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<tips>", "</tips>")) + " 出售成功！");
                    totalprice = DataConvert.GetInt64(JsonHelper.GetMid(content, "<totalprice>", "</totalprice>"));
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
                LogHelper.Write("GameRanch.GetSellProductFeedback", content, ex, LogSeverity.Error);
            }
            return false;
        }
        #endregion
                
        #region GetCalfNameById
        private string GetCalfNameById(int aid)
        {
            foreach (CalfInfo calf in _calfsList)
            {
                if (calf.AId == aid)
                {
                    return calf.Name;
                }
            }
            return aid.ToString();
        }
        #endregion

        #region GetCalfByName
        private CalfInfo GetCalfByName(string name)
        {
            foreach (CalfInfo calf in _calfsList)
            {
                if (calf.Name == name)
                {
                    return calf;
                }
            }
            return null;
        }
        #endregion

        #region GetCalfById
        private CalfInfo GetCalfById(int aid)
        {
            foreach (CalfInfo calf in _calfsList)
            {
                if (calf.AId == aid)
                {
                    return calf;
                }
            }
            return null;
        }
        #endregion

        #region GetHelpRanchFriendById
        private FriendInfo GetHelpRanchFriendById(int uid)
        {
            foreach (FriendInfo friend in _ranchFriendsList)
            {
                if (friend.Id == uid)
                {
                    return friend;
                }
            }
            return null;
        }
        #endregion
        
        #region SortCalvesListByPrice
        private Collection<CalfInfo> SortCalvesListByPrice(Collection<CalfInfo> calves)
        {
            for (int ix = 0; ix < calves.Count; ix++)
            {
                for (int iy = ix + 1; iy < calves.Count; iy++)
                {
                    if (calves[ix].Price > calves[iy].Price)
                    {
                        CalfInfo temp = calves[ix];
                        calves[ix] = calves[iy];
                        calves[iy] = temp;
                    }
                }
            }

            return calves;
        }
        #endregion

        #region GetAnimalProductByIdAndType
        private ProductInfo GetAnimalProductByIdAndType(int aid, int type)
        {
            foreach (ProductInfo product in _productsList)
            {
                if (product.Aid == aid && product.Type == type)
                {
                    return product;
                }
            }
            return null;
        }
        #endregion

        #region GetAnimalProductNameByIdAndType
        private string GetAnimalProductNameByIdAndType(int aid, int type)
        {
            foreach (ProductInfo product in _productsList)
            {
                if (product.Aid == aid && product.Type == type)
                {
                    return product.Name;
                }
            }
            return aid.ToString() + "(" + type.ToString() + ")";
        }
        #endregion

        #region GetAnimalProductPriceByIdAndType
        private int GetAnimalProductPriceByIdAndType(int aid, int type)
        {
            foreach (ProductInfo product in _productsList)
            {
                if (product.Aid == aid && product.Type == type)
                {
                    return product.Price;
                }
            }
            return 0;
        }
        #endregion

        private bool IsContained(int aid, int type)
        {
            foreach (ProductInfo product in Task.SellProductForbiddenList)
            {
                if (product.Aid == aid && product.Type == type)
                    return true;
            }
            return false;
        }

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

        public string RequestAllRanchFriends()
        {
            //牧场的好友其实就是所有买房子的好友
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/house/mystay_dialog.php?verify=" + this._verifyCode);
        }

        public string RequestAgriculturalProductFriends()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            //http://www.kaixin001.com/!house/!ranch/friendlist.php?r=0.8801959441043437
            //[{"uid":6194153,"real_name":"\u5e84\u5b50","icon20":"http:\/\/pic1.kaixin001.com\/logo\/19\/41\/20_6194153_1.jpg","water":1}]
            return HH.Get("http://www.kaixin001.com/!house/!ranch/friendlist.php?r=0.8801959441043437");
        }

        public string RequestRanch(string fuid)
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/!house/ranch/index.php?najax=1&fuid=" + fuid);
        }

        public string RequestRanchConf(string verifyCode, string fuid)
        {
            //http://www.kaixin001.com/!house/!ranch//getconf.php?verify=2588258_1062_2588258_1241441557_8c2bfcb1eac397ec933aa14d58dcc5a6&fuid=0&r=0.9890466774813831
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//getconf.php?verify={0}&fuid={1}&r=0.9890466774813831", verifyCode, fuid));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, CurrentAccount.UserName);
            //}
            return content;
        }

        public string RequestMyFoodList(int page)
        {
            //http://www.kaixin001.com/!house/!ranch//foodlist.php?verify=2588258_1062_2588258_1241444968_9ddee5e84226f10e772e23fa7b5d3d8a&page=1&r=0.04993377858772874
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//foodlist.php?verify={0}&page={1}&r=0.04993377858772874", this._verifyCode, page));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "MySeedList");
            //}
            return content;
        }
        
        public string RequestCalvesList()
        {
            //http://www.kaixin001.com/house/ranch//animalslist.php?verify=2588258_1062_2588258_1241439649_b85973757669bbd41b3357bef3f2a939&r=0.7589110936969519
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/house/ranch//animalslist.php?verify={0}&r=0.7589110936969519", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "AnimalsList");
            //}
            return content;
        }

        public string RequestBreedableList()
        {
            //http://www.kaixin001.com/!house/!ranch//breedable.php?verify=2588258_1062_2588258_1241618324_ce3f9506705b7c86a83547756c1ec017&r=0.6033188928849995
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//breedable.php?verify={0}&r=0.6033188928849995", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "BreedableList");
            //}
            return content;
        }

        public string RequestFriendTools(string bskey)
        {
            //http://www.kaixin001.com/!house/!ranch//getfriendtools.php?bskey=ranch_cock&verify=6209015_1062_6209015_1241699941_c3c2e122363283e77ab51a310791594a&r=0.9986867932602763
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//getfriendtools.php?bskey={0}&verify={1}&r=0.9986867932602763", bskey, this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "RequestFriendTools");
            //}
            return content;
        }

        public string RequestMyRanchWarehouse()
        {
            //http://www.kaixin001.com/!house/!ranch//mygranary.php?verify=2588258_1062_2588258_1242909047_77ddc374da20a50a1942853723f94b69&r=0.6191173549741507
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//mygranary.php?verify={0}", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "RequestMyRanchWarehouse");
            //}
            return content;
        }

        #endregion

        #region Properties
        public Collection<FriendInfo> AllRanchFriendsList
        {
            get { return this._allRanchFriendsList; }
        }

        public Collection<FriendInfo> AgriculturalProductFriendsList
        {
            get { return this._ranchFriendsList; }
        } 

        public Collection<CalfInfo> CalfsList
        {
            get { return _calfsList; }
            set { _calfsList = value; }
        }

        public Collection<ProductInfo> ProductsList
        {
            get { return _productsList; }
            set { _productsList = value; }
        }
        //public Collection<int> HasNothingTobeHelpedList
        //{
        //    get { return _hasNothingTobeHelpedList; }
        //    set { _hasNothingTobeHelpedList = value; }
        //}
        #endregion
    }
}
