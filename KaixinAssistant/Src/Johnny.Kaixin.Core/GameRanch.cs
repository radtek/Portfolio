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
                SetMessageLn("���ڳ�ʼ��[����]...");

                string content = RequestHouseHomePage(true);

                //all ranch friends
                content = RequestAllRanchFriends();
                ReadAllRanchFriends(content, false);
                SetMessage("[���������ĺ���]��Ϣ���سɹ���");

                //agricultural product friends
                content = RequestAgriculturalProductFriends();
                ReadAgriculturalProductFriends(content, false);
                SetMessage("[�������п�͵��ũ����Ʒ�ĺ���]��Ϣ���سɹ���");
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
                SetMessage(" ��ʼ��[����]ʧ�ܣ�����" + ex.Message);
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
                SetMessageLn("ˢ��[���������ĺ���]...");

                if (!this.ValidationLogin(true))
                {
                    if (AllRanchFriendsFetched != null)
                        AllRanchFriendsFetched(_allRanchFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestAllRanchFriends();
                ReadAllRanchFriends(content, true);
                SetMessageLn("[���������ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (AllRanchFriendsFetched != null)
                    AllRanchFriendsFetched(_allRanchFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[���������ĺ���]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region ReadAllRanchFriends
        public void ReadAllRanchFriends(string content, bool printMessage)
        {
            int num;
            this._allRanchFriendsList.Clear();

            if (printMessage)
                SetMessageLn("��ȡ[���������ĺ���]��Ϣ:");
            //<div class="l" style="width:8em;"><a href="javascript:gotoUser(6194153);" class="sl">ׯ��</a></div>
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
                    SetMessageLn(this._allRanchFriendsList[ix].Name + "(" + this._allRanchFriendsList[ix].Id.ToString() + ")--" + (this._allRanchFriendsList[ix].Gender ? "��" : "Ů"));
                ix++;
                content2 = content2.Substring(num);
            }
            if (printMessage)
                SetMessageLn("��ɶ�ȡ��");
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
                SetMessageLn("ˢ��[�������п�͵��ũ����Ʒ�ĺ���]...");

                if (!this.ValidationLogin(true))
                {
                    if (AgriculturalProductFriendsFetched != null)
                        AgriculturalProductFriendsFetched(_ranchFriendsList);
                    return;
                }

                string content = RequestHouseHomePage(false);
                content = RequestAgriculturalProductFriends();
                ReadAgriculturalProductFriends(content, true);
                SetMessageLn("[�������п�͵��ũ����Ʒ�ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (AgriculturalProductFriendsFetched != null)
                    AgriculturalProductFriendsFetched(_ranchFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[�������п�͵��ũ����Ʒ�ĺ���]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region ReadAgriculturalProductFriends
        public void ReadAgriculturalProductFriends(string content, bool printMessage)
        {
            try
            {
                if (printMessage)
                    SetMessageLn("��ȡ[�������г���ũ����Ʒ�ĺ���]��Ϣ...");

                //[{"uid":11860509,"real_name":"\u5f90\u5723\u541b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/5\/20_11860509_1.jpg","harvest":1,"food":1},{"uid":1504367,"real_name":"\u4f59\u661f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/50\/43\/20_1504367_20.jpg","harvest":1,"food":1},{"uid":18643363,"real_name":"\u595a\u51e4(\u6021\u6021)","icon20":"http:\/\/pic1.kaixin001.com\/logo\/64\/33\/20_18643363_1.jpg","harvest":1,"food":1},{"uid":32263316,"real_name":"\u5f90\u6daf\u7433","icon20":"http:\/\/pic.kaixin001.com\/logo\/26\/33\/20_32263316_15.jpg","harvest":1,"product":1,"food":1},{"uid":4026057,"real_name":"\u8521\u632f\u534e","icon20":"http:\/\/pic1.kaixin001.com\/logo\/2\/60\/20_4026057_2.jpg","harvest":1},{"uid":4179925,"real_name":"\u51b7\u8840","icon20":"http:\/\/pic1.kaixin001.com\/logo\/17\/99\/20_4179925_1.jpg","harvest":1,"food":1},{"uid":4789786,"real_name":"\u5b5f\u519b\u534e","icon20":"http:\/\/pic.kaixin001.com\/logo\/78\/97\/20_4789786_2.jpg","harvest":1},{"uid":27618660,"real_name":"\u9ad8\u4ebf\u658c","icon20":"http:\/\/pic.kaixin001.com\/logo\/61\/86\/20_27618660_3.jpg","product":1},{"uid":4121752,"real_name":"\u5f90\u4e3d\u82ac","icon20":"http:\/\/pic.kaixin001.com\/logo\/12\/17\/20_4121752_1.jpg","product":1},{"uid":4343401,"real_name":"\u9648\u9e4f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/34\/34\/20_4343401_5.jpg","product":1},{"uid":4570613,"real_name":"\u6f58\u534e","icon20":"http:\/\/pic1.kaixin001.com\/logo\/57\/6\/20_4570613_6.jpg","product":1},{"uid":10151052,"real_name":"\u5b59\u6b63\u82b3","icon20":"http:\/\/pic.kaixin001.com\/logo\/15\/10\/20_10151052_1.jpg","food":1,"water":1},{"uid":10368525,"real_name":"\u502a\u4f1f\u534e","icon20":"http:\/\/pic1.kaixin001.com\/logo\/36\/85\/20_10368525_1.jpg","food":1},{"uid":1560381,"real_name":"\u848b\u745b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/56\/3\/20_1560381_4.jpg","food":1},{"uid":18601260,"real_name":"\u6731\u5e7f\u7530","icon20":"http:\/\/pic.kaixin001.com\/logo\/60\/12\/20_18601260_2.jpg","food":1},{"uid":1922571,"real_name":"\u4f40\u95ef","icon20":"http:\/\/pic1.kaixin001.com\/logo\/92\/25\/20_1922571_5.jpg","food":1},{"uid":1991973,"real_name":"\u5de2\u5a67","icon20":"http:\/\/pic1.kaixin001.com\/logo\/99\/19\/20_1991973_2.jpg","food":1},{"uid":2119333,"real_name":"\u4faf\u9e23","icon20":"http:\/\/pic1.kaixin001.com\/logo\/11\/93\/20_2119333_4.jpg","food":1},{"uid":2511621,"real_name":"\u4e07\u6d69","icon20":"http:\/\/pic1.kaixin001.com\/logo\/51\/16\/20_2511621_5.jpg","food":1},{"uid":26366307,"real_name":"\u6731\u4f1f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/36\/63\/20_26366307_2.jpg","food":1},{"uid":2865629,"real_name":"\u7ae5\u610f\u5fe0(@)","icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/56\/20_2865629_21.jpg","food":1},{"uid":28860603,"real_name":"\u9648\u5609\u59ae","icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/6\/20_28860603_6.jpg","food":1,"fee":1},{"uid":3342217,"real_name":"\u5468\u4e3d","icon20":"http:\/\/pic1.kaixin001.com\/logo\/34\/22\/20_3342217_71.jpg","food":1},{"uid":362564,"real_name":"\u9648\u52bc","icon20":"http:\/\/pic.kaixin001.com\/logo\/36\/25\/20_362564_22.jpg","food":1},{"uid":3653622,"real_name":"\u9ec4\u6587\u7fa4","icon20":"http:\/\/pic.kaixin001.com\/logo\/65\/36\/20_3653622_12.jpg","food":1},{"uid":3754193,"real_name":"\u502a\u519b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/75\/41\/20_3754193_9.jpg","food":1},{"uid":5505715,"real_name":"\u77bf\u535a","icon20":"http:\/\/pic1.kaixin001.com\/logo\/50\/57\/20_5505715_1.jpg","food":1},{"uid":6106453,"real_name":"\u6c5f\u950b","icon20":"http:\/\/pic1.kaixin001.com\/logo\/10\/64\/20_6106453_1.jpg","food":1},{"uid":6265093,"real_name":"\u987e\u73fa\u96ef","icon20":"http:\/\/pic1.kaixin001.com\/logo\/26\/50\/20_6265093_3.jpg","food":1},{"uid":6320371,"real_name":"\u5f20\u79e6\u8273","icon20":"http:\/\/pic1.kaixin001.com\/logo\/32\/3\/20_6320371_5.jpg","food":1},{"uid":6888001,"real_name":"\u66f9\u6e0a","icon20":"http:\/\/pic1.kaixin001.com\/logo\/88\/80\/20_6888001_1.jpg","food":1,"water":1},{"uid":7744681,"real_name":"\u6587\u6653\u6653","icon20":"http:\/\/pic1.kaixin001.com\/logo\/74\/46\/20_7744681_2.jpg","food":1},{"uid":8063649,"real_name":"\u675c\u6ce2","icon20":"http:\/\/pic1.kaixin001.com\/logo\/6\/36\/20_8063649_15.jpg","food":1},{"uid":9637731,"real_name":"\u66f9\u840d","icon20":"http:\/\/pic1.kaixin001.com\/logo\/63\/77\/20_9637731_2.jpg","food":1},{"uid":1283947,"real_name":"\u6797\u8054\u5bf9","icon20":"http:\/\/pic1.kaixin001.com\/logo\/28\/39\/20_1283947_2.jpg","water":1},{"uid":1581208,"real_name":"\u7fc1\u5c11\u534e","icon20":"http:\/\/pic.kaixin001.com\/logo\/58\/12\/20_1581208_4.jpg","water":1},{"uid":1703568,"real_name":"\u5510\u8c6a\u5ddd","icon20":"http:\/\/pic.kaixin001.com\/logo\/70\/35\/20_1703568_6.jpg","water":1},{"uid":1945978,"real_name":"\u9648\u5b66\u8d85","icon20":"http:\/\/pic.kaixin001.com\/logo\/94\/59\/20_1945978_7.jpg","water":1},{"uid":2125264,"real_name":"\u66f9\u548f\u840d","icon20":"http:\/\/pic.kaixin001.com\/logo\/12\/52\/20_2125264_6.jpg","water":1},{"uid":2596914,"real_name":"\u5510\u5ddd\u519b","icon20":"http:\/\/pic.kaixin001.com\/logo\/59\/69\/20_2596914_1.jpg","water":1},{"uid":3223271,"real_name":"\u53f2\u53f2","icon20":"http:\/\/pic1.kaixin001.com\/logo\/22\/32\/20_3223271_2.jpg","water":1},{"uid":3644956,"real_name":"\u6c88\u71d5\u9752","icon20":"http:\/\/pic.kaixin001.com\/logo\/64\/49\/20_3644956_2.jpg","water":1},{"uid":3933628,"real_name":"\u5468\u654f","icon20":"http:\/\/pic.kaixin001.com\/logo\/93\/36\/20_3933628_1.jpg","water":1},{"uid":3986105,"real_name":"\u6c88\u84d3\u6654","icon20":"http:\/\/pic1.kaixin001.com\/logo\/98\/61\/20_3986105_1.jpg","water":1},{"uid":4114760,"real_name":"\u6768\u6770","icon20":"http:\/\/pic.kaixin001.com\/logo\/11\/47\/20_4114760_3.jpg","water":1},{"uid":5010598,"real_name":"\u7f2a\u5357","icon20":"http:\/\/pic.kaixin001.com\/logo\/1\/5\/20_5010598_2.jpg","water":1},{"uid":5969055,"real_name":"\u534e\u654f\u5cf0","icon20":"http:\/\/pic1.kaixin001.com\/logo\/96\/90\/20_5969055_7.jpg","water":1},{"uid":779907,"real_name":"\u9a6c\u749f","icon20":"http:\/\/pic1.kaixin001.com\/logo\/77\/99\/20_779907_1.jpg","water":1},{"uid":9414220,"real_name":"\u738b\u4eae","icon20":"http:\/\/pic.kaixin001.com\/logo\/41\/42\/20_9414220_15.jpg","water":1}]
                JsonTextParser parser = new JsonTextParser();
                JsonArrayCollection arraySharedFriends = parser.Parse(content) as JsonArrayCollection;
                if (arraySharedFriends != null)
                {
                    if (printMessage)
                        SetMessageLn("����������������");
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
                SetMessage(" ��ȡ[�������г���ũ����Ʒ�ĺ���]��Ϣʧ�ܣ�" + ex.Message);
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
                SetMessageLn("ˢ��[�̵��ж��������б�]...");

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
                    SetMessageLn("[�̵��ж��������б�]��Ϣˢ��ʧ�ܣ�");
                else
                    SetMessageLn("[�̵��ж��������б�]��Ϣˢ�³ɹ���");

                //invoke event
                if (CalvesInShopFetched != null)
                    CalvesInShopFetched(calves);
            });
            base.ExecuteTryCatchBlock(th, "[�̵��ж��������б�]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region RunRanch
        public void RunRanch()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_RANCH;

                SetMessageLn("��ʼ����...");

                //ranch
                string contentHome = RequestHouseHomePage(false);

                //task manager�в���Ϊnull���ڲ��Ժڰ�����ʱΪnull
                if (this._calfsList == null)
                {
                    this._calfsList = ConfigCtrl.GetCalvesInShop();
                }

                //RequestRanchConf(this._verifyCode, CurrentAccount.UserId);
                //return;

                RanchInfo ranch = ReadRanch(this._verifyCode, CurrentAccount.UserId, true);
                if (ranch == null)
                {
                    SetMessageLn("�޷���ȡ�ҵ�������Ϣ��");
                    return;
                }

                ReadMyFoods(ranch);

                if (_myfoodList.Count == 0)
                {
                    SetMessage("û�������ˣ�");
                }

                string content = "";

                //��ˮ
                if (Task.AddWater)
                    AddWater(CurrentAccount.UserId, ranch);
                //������
                if (Task.AddGrass)
                    AddGrass(CurrentAccount.UserId, ranch);
                //����ܲ�
                if (Task.AddCarrot)
                    AddCarrot(CurrentAccount.UserId, ranch);
                //������
                if (Task.AddBamboo)
                    AddBamboo(CurrentAccount.UserId, ranch);
                //�ջ�ũ����Ʒ
                if (Task.HarvestProduct)
                    HarvestProduct(ranch);
                //����/�ջ���
                if (Task.MakeProduct || Task.HarvestAnimal)
                    HarvestAndProductAnimal(ranch);

                //����
                if (Task.BreedAnimal)
                    BreedAnimal();

                //��������
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
                
                //͵��ũ����Ʒ
                //if (Task.StealProduct && Operation.StealProductAll || Task.HelpMakeProduct && Operation.HelpRanchAll)
                //{
                    
                //}

                content = RequestAgriculturalProductFriends();
                ReadAgriculturalProductFriends(content, false);
                
                if (Task.StealProduct)
                    StealRanchs();

                //��æ����
                if (Task.HelpMakeProduct)
                    HelpMakeProduct();

                //ȥ����������æ
                if (Task.HelpAddWater || Task.HelpAddGrass || Task.HelpAddCarrot || Task.HelpAddBamboo)
                    HelpOthersRanchs();

                //����ũ����Ʒ
                if (Task.PresentProduct)
                    PresentAnimalProduct();

                //���۹�ʵ
                if (Task.SellProduct)
                    SellProduct(ranch);

                SetMessageLn("������ɣ�");

            });
            base.ExecuteTryCatchBlock(th, "�����쳣������ʧ�ܣ�");
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
                    SetMessageLn("��ȡ������Ϣʧ�ܣ�");
            }
            else
            {
                if (CurrentAccount.UserId == fuid)
                    this._myRank = ranch.Rank;

                if (printmessage)
                    SetMessageLn(string.Format("{0}��������{1} {2} ������{3} ˮ��{4} ���ݣ�{5}", ranch.Name, ranch.RankTip, ranch.CashTip, ranch.TCharms, ranch.Water, ranch.Grass));

                if (ranch.Animals != null)
                {
                    int num = 0;
                    //foreach (AnimalInfo animal in ranch.Animals)
                    //{
                    //    SetMessageLn(string.Format("��{0}�����", ++num));
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
                            SetMessageLn(string.Format("��{0}�����{1}", ++num, animal.AName));

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
            SetMessageLn("�ҵ����ϣ�");
            string content = RequestMyFoodList(1);
            int totalpage = 0;
            _myfoodList = ConfigCtrl.GetMyFoods(content, ref totalpage);

            if (_myfoodList == null)
            {
                SetMessage("�޷���ȡ�ҵ�������Ϣ��");
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
            //��������
            SetMessageLn("��������...");

            if (ranch.Animals.Count >= MAX_ANIMAL_COUNT)
            {
                SetMessage(string.Format("���������{0}ֻ����", MAX_ANIMAL_COUNT));
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
                            //��ֹ��ѭ��
                            if (buytimes > 20)
                                break;
                            else
                                buytimes++;

                            SetMessageLn(_calfsList[ix].Name + "��");
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
                        //��ֹ��ѭ��
                        if (buytimes > 20)
                            break;
                        else
                            buytimes++;

                        CalfInfo calf = GetCalfById(Task.BuyCalfCustom);
                        SetMessageLn(calf.Name + "��");
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
            //<data><ret>fail</ret><reason>�����ɹ��ջ��һֻ«��ĸ���󣬲����������«��ĸ��</reason></data>
            if (String.IsNullOrEmpty(content))
                return true;

            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ����ʧ�ܣ�");
                //LogHelper.Write(CurrentAccount.UserName, JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ʧ�ܣ�", LogSeverity.Warn);
                if (content.IndexOf("����ֽ𲻹������ܹ���") > -1)
                {
                    _canbuyanimals = false;
                }
                else if (content.IndexOf("��������") > -1)
                {
                    _canbuyanimals = false;
                }
                else if (content.IndexOf("�����ɹ���ϵͳ������һֻ") > -1)
                {
                    isbuyable = false;
                }
                //��Ŀǰ�ļ����������6ֻ����
                Regex regular = new Regex(@"��Ŀǰ�ļ����������[\d]+ֻ����");
                if (regular.IsMatch(content))
                {
                    _canbuyanimals = false;
                    return false;
                }
                Regex regularMax = new Regex(@"ÿ�������[\d]+ֻ����");
                if (regularMax.IsMatch(content))
                {
                    _canbuyanimals = false;
                    return false;
                }
                //��ļ���Ҫ��45���������ö���
                Regex regularRank = new Regex(@"��ļ���Ҫ��[\d]+���������ö���");
                if (regularRank.IsMatch(content))
                {
                    isbuyable = false;
                    return false;
                }
                //��������У�ֻ����2ֻ����
                Regex regularCount = new Regex(@"��������У�ֻ����[\d]+");
                if (regularCount.IsMatch(content))
                {
                    isbuyable = false;
                    return false;
                }                
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                SetMessage("����" + count.ToString() + "��" + name + "��ɣ�");
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
                SetMessageLn("��ʼ�ջ�ũ����Ʒ...");

                Collection<AnimalProductInfo> animalProducts = ranch.AnimalProducts;
                if (animalProducts == null || animalProducts.Count == 0)
                {
                    SetMessage("û��ũ����Ʒ���ջ�");
                    return;
                }
                int num = 0;
                foreach (AnimalProductInfo animalProduct in animalProducts)
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0} {1} ʣ��������{2} ", ++num, animalProduct.PName, (animalProduct.Num - animalProduct.StealNum)));
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
                        SetMessage("�ջ�" + animalProduct.PName+ "ʧ�ܣ�����" + ex.Message);
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
                SetMessage("�ջ�ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region HarvestAndProductAnimal
        private void HarvestAndProductAnimal(RanchInfo ranch)
        {
            try
            {
                SetMessageLn("��ʼ����/�ջ���...");

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
                            SetMessageLn(string.Format("��{0}������ {1}��", ++num, animal.AName));
                        }                       

                        if (animal.BProduct == 2)
                        {
                            SetMessage(" ����������");
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//product.php?fuid={0}&animalsid={1}&r=0%2E20183774875476956&verify={2}", "0", animal.AnimalSid, DataConvert.GetEncodeData(this._verifyCode)));
                            GetMakeProductFeedback(content);
                        }

                        if (animal.BStat == 2)
                        {
                            SetMessage(" �����ջ�");
                            //http://www.kaixin001.com/!house/!ranch//mhavest.php?verify=7998514%5F1062%5F7998514%5F1258905868%5F92f3a7fb3bf711c0e7191e6dc53c8d33&fuid=0&animalsid=2174827016&r=0%2E2255321340635419
                            //<data><ret>succ</ret><mpic>http://img.kaixin001.com.cn//i2/house/ranch/animals2/hedgehogmeat.swf</mpic><cash>0</cash></data>
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!ranch//mhavest.php?verify={0}&fuid=0&animalsid={1}&r=0%2E05614474741742015", DataConvert.GetEncodeData(this._verifyCode), animal.AnimalSid));
                            GetHarvestAnimalFeedback(content, ref harvested);
                        }
                        //���û�б��ջ񣬼��뵽�µ�list��
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
                        SetMessage("�ջ�" + animal.AName + "ʧ�ܣ�����" + ex.Message);
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
                SetMessage("�ջ�ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetMakeProductFeedback
        private void GetMakeProductFeedback(string content)
        {
            try
            {
                //<data><action>product</action><ret>fail</ret><reason>�ö�����������������</reason></data>
                //<data><action>product</action><ret>fail</ret><reason>�ö��ﰤ���У���������</reason></data>
                //<data><action>product</action><ret>succ</ret><skey>hen</skey><ptips>�ѳɹ������Կ˵�«��ĸ����ȥ����&lt;br&gt;������10���֣�10���Ӻ�����͵</ptips><bproduct>1</bproduct><leftptime>10</leftptime><tips>&lt;font color='#FF0000'&gt;������&lt;/font&gt;&lt;br&gt;Ԥ�Ʋ�����10&lt;br&gt;&lt;font color='#666666'&gt;������ջ���10��&lt;/font&gt;</tips><pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic><tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;«��ĸ��</tname></data>

                MakeProductInfo objMakeProduct = ConfigCtrl.ConvertToMakeProductObject(content);
                if (objMakeProduct == null)
                {
                    SetMessage("���������쳣������ʧ�ܣ�");
                }
                else
                {
                    if (objMakeProduct.Ret == "succ")
                    {
                        SetMessage(string.Format("{0} �����ɹ���", JsonHelper.FiltrateHtmlTags(objMakeProduct.PTips)));
                    }
                    else if (objMakeProduct.Ret == "fail")
                        SetMessage(string.Format("{0} ����ʧ�ܣ�", objMakeProduct.Reason));
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
                SetMessage("�ջ���ɣ�");
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
                SetMessageLn("��ʼ��ˮ...");
                //ˮ����0��/<font color='#FF0000'>���ˮ</font> 
                if (!(ranch.WaterTips.IndexOf("���ˮ") > -1 || ranch.WaterTips.IndexOf("�����") > -1 || ranch.Water < 50))
                {
                    SetMessage("ˮ��Ϊ" + ranch.Water.ToString() + "���������");
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
                SetMessage("��ˮʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetAddWaterFeedback
        private void GetAddWaterFeedback(string content)
        {
            try
            {
                //<data><ret>succ</ret><watertips>ˮ����100��&lt;br&gt;&lt;font color='#666666'&gt;��ȹ⻹��Լ400Сʱ&lt;/font&gt;</watertips><tips></tips></data>
                //<data><ret>fail</ret><reason>ˮ������30�񣬲�����ˮ</reason></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ��ˮʧ�ܣ�");
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    WaterInfo objWater = ConfigCtrl.ConvertToWaterObject(content);
                    if (objWater == null)
                    {
                        SetMessage(content);
                        SetMessage("���������쳣����ˮʧ�ܣ�");
                    }
                    else
                    {
                        SetMessage(string.Format("{0} ��ˮ�ɹ���", JsonHelper.FiltrateHtmlTags(objWater.WaterTips)));
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
                SetMessageLn("��ʼ������...");
                //���ݣ�30��<font color='#FF0000'>(��Ӳ�)</font><br><font color='#666666'>��Թ⻹��Լ20Сʱ</font>
                if (!(ranch.GrassTips.IndexOf("��Ӳ�") > -1 || ranch.GrassTips.IndexOf("�����") > -1 || ranch.Grass < 150))
                {
                    SetMessage("����" + ranch.Grass.ToString() + "�����ݣ��������");
                    return;
                }
                if (ranch.GrassTips.IndexOf("�����") > -1)
                {
                    SetMessage("��û�гԸ����ϵĶ�������");
                    return;
                }
                if (!_canaddgrass || _myfoodList == null || _myfoodList.Count == 0)
                {
                    SetMessage("û������");
                    _canaddgrass = false;
                    return;
                }
                foreach (FoodInfo food in _myfoodList)
                {
                    if (food.SeedId != 63)
                        continue;

                    if (food.Num <= 0)
                    {
                        SetMessage(string.Format("û��{0}({1})��", food.Name, food.SeedId));
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
                SetMessage("������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetAddGrassFeedback
        private void GetAddGrassFeedback(string content, FoodInfo food)
        {
            try
            {
                //<data><ret>fail</ret><reason>���ݶ���100�ţ������������</reason></data>
                //<data><ret>fail</ret><reason>�˴�������������Ҫ1���ݣ�������ݲ��㣬���������ݡ�</reason></data>
                //<data><ret>succ</ret><grasstips>���ݣ�72��&lt;font color='#FF0000'&gt;(��Ӳ�)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;��Թ⻹��Լ288Сʱ&lt;/font&gt;</grasstips><grass>72</grass><animalstips></animalstips></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ������ʧ�ܣ�");
                    if (content.IndexOf("������ݲ���") > -1)
                        _canaddgrass = false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    FeedInfo objFeed = ConfigCtrl.ConvertToFeedObject(content);
                    if (objFeed == null)
                    {
                        SetMessage(content);
                        SetMessage("���������쳣��������ʧ�ܣ�");
                    }
                    else
                    {
                        SetMessage(string.Format("��{0}������ {1} �����ݳɹ���", objFeed.Grass, JsonHelper.FiltrateHtmlTags(objFeed.GrassTips)));
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
                SetMessageLn("��ʼ����ܲ�...");
                //���ܲ���58��&lt;font color='#FF0000'&gt;(�����)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;��Թ⻹��Լ232Сʱ&lt;/font&gt;
                Collection<FoodItemInfo> foods = ranch.Foods;
                foreach (FoodItemInfo fooditem in foods)
                {
                    if (fooditem.SeedId == 1)
                    {
                        if (!(fooditem.Tips.IndexOf("�����") > -1 || fooditem.Grass < 150))
                        {
                            SetMessage("����" + fooditem.Grass.ToString() + "�����ܲ����������");
                            return;
                        }
                        if (fooditem.Tips.IndexOf("�����") > -1)
                        {
                            SetMessage("��û�гԸ����ϵĶ�������");
                            return;
                        }
                        if (!_canaddcarrot || _myfoodList == null || _myfoodList.Count == 0)
                        {
                            SetMessage("û������");
                            _canaddcarrot = false;
                            return;
                        }
                        foreach (FoodInfo food in _myfoodList)
                        {
                            if (food.SeedId != 1)
                                continue;

                            if (food.Num <= 0)
                            {
                                SetMessage(string.Format("û��{0}({1})��", food.Name, food.SeedId));
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
                SetMessage("������ʧ�ܣ�����" + ex.Message);
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
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ����ܲ�ʧ�ܣ�");
                    if (content.IndexOf("��ĺ��ܲ�����") > -1)
                        _canaddcarrot = false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    FeedInfo objFeed = ConfigCtrl.ConvertToFeedObject(content);
                    if (objFeed == null)
                    {
                        SetMessage(content);
                        SetMessage("���������쳣������ܲ�ʧ�ܣ�");
                    }
                    else
                    {
                        SetMessage(string.Format("��{0}�����ܲ� {1} ����ܲ��ɹ���", objFeed.Grass, JsonHelper.FiltrateHtmlTags(objFeed.GrassTips)));
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
                SetMessageLn("��ʼ������...");
                //<tips>���ӣ�198��&lt;br&gt;&lt;font color='#666666'&gt;��Թ⻹��Լ396Сʱ&lt;/font&gt;</tips>
                Collection<FoodItemInfo> foods = ranch.Foods;
                foreach (FoodItemInfo fooditem in foods)
                {
                    if (fooditem.SeedId == 95)
                    {
                        if (!(fooditem.Tips.IndexOf("�����") > -1 || fooditem.Grass < 150))
                        {
                            SetMessage("����" + fooditem.Grass.ToString() + "�����ӣ��������");
                            return;
                        }
                        if (fooditem.Tips.IndexOf("�����") > -1 || fooditem.Tips.IndexOf("û�ж���ʳ�ø�����") > -1)
                        {
                            SetMessage("��û�гԸ����ϵĶ�������");
                            return;
                        }
                        if (!_canaddbamboo || _myfoodList == null || _myfoodList.Count == 0)
                        {
                            SetMessage("û������");
                            _canaddbamboo = false;
                            return;
                        }
                        foreach (FoodInfo food in _myfoodList)
                        {
                            if (food.SeedId != 95)
                                continue;

                            if (food.Num <= 0)
                            {
                                SetMessage(string.Format("û��{0}({1})��", food.Name, food.SeedId));
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
                SetMessage("������ʧ�ܣ�����" + ex.Message);
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
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ������ʧ�ܣ�");
                    if (content.IndexOf("������Ӳ���") > -1)
                        _canaddbamboo = false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    FeedInfo objFeed = ConfigCtrl.ConvertToFeedObject(content);
                    if (objFeed == null)
                    {
                        SetMessage(content);
                        SetMessage("���������쳣��������ʧ�ܣ�");
                    }
                    else
                    {
                        SetMessage(string.Format("��{0}������ {1} �����ӳɹ���", objFeed.Grass, JsonHelper.FiltrateHtmlTags(objFeed.GrassTips)));
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
                SetMessageLn("��ʼ����...");

                string content = RequestBreedableList();
                Collection<BreedableInfo> breedableAnimals = ConfigCtrl.GetBreedAnimals(content);
                if (breedableAnimals == null || breedableAnimals.Count == 0)
                {
                    SetMessage("û�п������ֵĶ���");
                    return;
                }

                int num = 0;
                foreach (BreedableInfo animal in breedableAnimals)
                {
                    try
                    {
                        SetMessageLn(string.Format("�����ֵĵ�{0}������ {1}��", ++num,GetCalfNameById(animal.Aid)));
                        content = RequestFriendTools(animal.BsKey);
                        Collection<BreedCardInfo> breedcards = ConfigCtrl.GetBreedCards(content);
                        if (breedcards == null || breedcards.Count == 0)
                        {
                            SetMessage("û�п������ֵĻ���");
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
                SetMessage(" ��æ����" + CurrentAccount.UserName + "������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetBreedAnimalFeedback
        private bool GetBreedAnimalFeedback(string content)
        {
            try
            {
                //<data><ret>fail</ret><reason>������������</reason></data>
                //<data><ret>succ</ret><succtips>���«��ĸ�������Կ˹������ڲ��������ֳɹ�!&lt;br&gt;24Сʱ�ڽ����²񼦵���ÿֻ30Ԫ��</succtips><bproduct>0</bproduct><leftptime>0</leftptime><tips>������&lt;font color='#FF0000'&gt;(������)&lt;/font&gt;&lt;br&gt;�����´β�����6��&lt;br&gt;Ԥ�Ʋ�����10&lt;br&gt;&lt;font color='#666666'&gt;�಻�ܲ�������2��9Сʱ51��&lt;/font&gt;</tips><skey>hen</skey><pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic><tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;«��ĸ��</tname><animalsid>6233300</animalsid></data>

                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ����ʧ�ܣ�");                    
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    BreedAnimalInfo objBreedAnimal = ConfigCtrl.ConvertToBreedAnimalObject(content);
                    if (objBreedAnimal == null)
                        SetMessage("���������쳣������ʧ�ܣ�");
                    else
                    {
                        SetMessage(string.Format("{0} ���ֳɹ���", JsonHelper.FiltrateHtmlTags(objBreedAnimal.Succtips)));
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

            SetMessageLn("��ʼ͵ũ����Ʒ��");
            //��͵�������е���
            SetMessageLn("��ʼ͵�������е��ˣ�");
            foreach (int uid in Operation.StealProductWhiteList)
            {
                try
                {
                    SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");
                    FriendInfo friend = GetHelpRanchFriendById(uid);
                    if (friend == null || friend.RanchHarvest == false)
                    {
                        SetMessage("ûʲô��͵�ģ�����");
                        continue;
                    }
                    if (Operation.StealProductBlackList.Contains(uid))
                    {
                        SetMessage(base.GetFriendNameById(uid) + "�ں������У�����");
                        continue;
                    }
                    StealTheRanch(uid.ToString());
                    if (this._canstealproduct == false)
                    {
                        SetMessageLn("��������첻����͵�ˣ�ֹͣ͵ũ����Ʒ��");
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

            //͵ʣ�µ���
            if (Operation.StealProductAll)
            {
                num = 0;
                SetMessageLn("ȥ�����г���ũ����Ʒ������͵��");
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
                            SetMessage(friend.Name + "�ں������У�����");
                            continue;
                        }                        
                        StealTheRanch(friend.Id.ToString());
                        if (this._canstealproduct == false)
                        {
                            SetMessageLn("��������첻����͵�ˣ�ֹͣ͵ũ����Ʒ��");
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
                    SetMessageLn("�޷���ȡ������Ϣ������");
                }
                else
                {
                    if (!_canstealproduct)
                        return;

                    Collection<AnimalProductInfo> animalProducts = ranch.AnimalProducts;
                    if (animalProducts == null || animalProducts.Count == 0)
                    {
                        SetMessageLn("û��ũ����Ʒ��͵������");
                        return;
                    }

                    int num = 0;
                    foreach (AnimalProductInfo animalProduct in animalProducts)
                    {
                        try
                        {
                            if (animalProduct.Num > animalProduct.StealNum)
                            {
                                SetMessageLn(string.Format("#{0} {1} ʣ��������{2} ", ++num, animalProduct.PName, (animalProduct.Num - animalProduct.StealNum)));
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
                            SetMessage("͵" + animalProduct.PName + "ʧ�ܣ�����" + ex.Message);
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
                SetMessage(" ͵" + GetFriendNameById(fuid) + "������ʧ�ܣ�����" + ex.Message);
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
                //  <reason>��͵��������Ҫ���</reason>
                //</data>
                StealProductInfo objStealProduct = ConfigCtrl.ConvertToStealProductObject(content);
                if (objStealProduct == null)
                {
                    SetMessage(content);
                    if (issteal)
                        SetMessage("���������쳣��͵��ʧ�ܣ�");
                    else
                        SetMessage("���������쳣���ջ�ʧ�ܣ�");
                }
                else
                {
                    if (objStealProduct.Ret == "succ")
                    {
                        if (issteal)
                            SetMessage(string.Format("{0}�� ͵�Գɹ���", objStealProduct.Num));
                        else
                            SetMessage(string.Format("{0}�� �ջ�ɹ���", objStealProduct.Num));
                    }
                    else if (objStealProduct.Ret == "fail")
                    {
                        if (issteal)
                            SetMessage(string.Format("{0} ͵��ʧ�ܣ�", objStealProduct.Reason));
                        else
                            SetMessage(string.Format("{0} �ջ�ʧ�ܣ�", objStealProduct.Reason));
                    }
                    else
                        SetMessage(content);
                }

                if (content.IndexOf("���첻����͵��") > -1)
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

            SetMessageLn("��ʼ��æ������");
            //�Ȱ�æ�����������е���
            SetMessageLn("��ʼ��æ�����������е��ˣ�");
            foreach (int uid in Operation.HelpRanchWhiteList)
            {
                try
                {
                    SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");
                    FriendInfo friend = GetHelpRanchFriendById(uid);
                    if (friend == null || friend.RanchProduct == false)
                    {
                        SetMessage("ûʲô�ɰ�æ�ģ�����");
                        continue;
                    }
                    if (Operation.HelpRanchBlackList.Contains(uid))
                    {
                        SetMessage(base.GetFriendNameById(uid) + "�ں������У�����");
                        continue;
                    }
                    HelpTheMakeProduct(uid.ToString());
                    //if (this._canstealproduct == false)
                    //{
                    //    SetMessageLn("��������첻����͵�ˣ�ֹͣ͵ũ����Ʒ��");
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

            //ʣ�µ���
            if (Operation.HelpRanchAll)
            {
                num = 0;
                SetMessageLn("ȥ��������������������");
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
                            SetMessage(friend.Name + "�ں������У�����");
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
                    SetMessageLn("�޷���ȡ������Ϣ������");
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
                                    SetMessageLn(string.Format("��{0}������ {1} {2}��", ++num, animal.AName, animal.PAction));
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
                SetMessage(" ��æ����" + GetFriendNameById(fuid) + "������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion
        
        #region HelpOthersRanchs
        private void HelpOthersRanchs()
        {
            try
            {
                int num = 0;

                SetMessageLn("��ʼȥ���ѵ�������æ��");

                if (Task.HelpAddWater || Task.HelpAddGrass || Task.HelpAddCarrot || Task.HelpAddBamboo)
                {
                    //��ȥ�������еĻ�԰����
                    SetMessageLn("��ʼȥ�������к��ѵ�������æ��");
                    foreach (int uid in Operation.HelpRanchWhiteList)
                    {
                        try
                        {
                            if (!base.IsAlreadyMyFriend(uid.ToString()))
                            {
                                SetMessageLn(string.Format("#{0} ID:{1}������ĺ���", ++num, uid));
                                LogHelper.Write("HelpOthersRanchs.HelpRanchWhiteList", "(" + uid + ")����" + CurrentAccount.UserName + "�ĺ���", LogSeverity.Warn);
                                continue;
                            }

                            SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");                            

                            //FriendInfo friend = GetHelpRanchFriendById(uid);
                            //if (friend == null)
                            //{
                            //    SetMessage("ûʲô�ɰ�æ�ģ�����");
                            //    continue;
                            //}
                            if (Operation.HelpRanchBlackList.Contains(uid))
                            {
                                SetMessage(base.GetFriendNameById(uid) + "�ڰ�æ�������У�����");
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
                    //��������
                    num = 0;
                    SetMessageLn("��ʼȥ�������ѵ�������æ��");
                    foreach (FriendInfo friend in this._ranchFriendsList)
                    {
                        try
                        {
                            if (Operation.HelpRanchWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                                continue;

                            SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                            if (Operation.HelpRanchBlackList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "�ڰ�æ�������У�����");
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
                    SetMessage("�޷���ȡ������Ϣ������");
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
                SetMessage(" ȥ" + GetFriendNameById(fuid) + "��������æʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region PresentAnimalProduct
        private void PresentAnimalProduct()
        {
            try
            {
                SetMessageLn("��ʼ����ũ����Ʒ...");
                if (Operation.PresentProductId == 0)
                {
                    SetMessage("û���趨���͵Ķ����޷�����");
                    return;
                }
                if (!IsAlreadyMyFriend(DataConvert.GetString(Operation.PresentProductId)))
                {
                    SetMessage(DataConvert.GetString(Operation.PresentProductId) + "������ĺ��ѣ��޷�����");
                    return;
                }

                string content = RequestMyRanchWarehouse();
                Collection<ProductInfo> products = ConfigCtrl.GetMyWarehouseProduct(content);
                if (products == null || products.Count == 0)
                {
                    SetMessage("�ֿ���û���κ�ũ����Ʒ");
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
                    //�����ֵ���
                    SetMessageLn("�ֿ�������͵�ũ����Ʒ��");
                    
                    foreach (ProductInfo product in products)
                    {
                        ProductInfo productbase = GetAnimalProductByIdAndType(product.Aid, product.Type);
                        if (productbase == null)
                        {
                            SetMessage(string.Format("δ֪ũ����Ʒ{0}-{1}������ ", product.Aid, product.Type));
                            LogHelper.Write("GameRanch.PresentAnimalProduct" + CurrentAccount.UserName, string.Format("δ֪ũ����Ʒ{0}-{1}������ ", product.Aid, product.Type), LogSeverity.Warn);
                            return;
                        }

                        tempprice = productbase.Price * product.Num;
                        SetMessageLn(string.Format("#{0}{1}��������{2}�������ۼۣ�{3}���ܼ�ֵ��{4}", ++num, product.Name, product.Num, productbase.Price, tempprice));
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

                    SetMessageLn(string.Format("����������͵ļ�ֵ��ߵ�ũ����Ʒ��{0}:{1}*{2}={3}Ԫ", productname, count, price, totalprice));

                    if (Task.PresentProductCheckValue && totalprice < Task.PresentProductValue * 10000)
                    {
                        SetMessage(string.Format(" �ܼ�ֵ{0}С��������ͼ�ֵ{1}������", totalprice, Task.PresentProductValue * 10000));
                        return;
                    }

                    SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentProductId), count, productname, price, totalprice));
                    content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", count, DataConvert.GetEncodeData("����ũ����Ʒ����"), DataConvert.GetEncodeData(this._verifyCode), type, Operation.PresentProductId, aid));
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
                                SetMessageLn(string.Format("#{0}{1}������������{2}�������ۼۣ�{3}���ܼ�ֵ��{4}", ++num, JsonHelper.GetMid(content, "<name>", "</name>"), JsonHelper.GetMid(content, "<selfnum>", "</selfnum>"), JsonHelper.GetMid(content, "<price>", "</price>"), tempprice));
                                totalprice = tempprice;
                                price = JsonHelper.GetMidInteger(content, "<price>", "</price>");
                                count = JsonHelper.GetMidInteger(content, "<selfnum>", "</selfnum>");                                

                                if (Task.PresentProductCheckValue && totalprice < Task.PresentProductValue * 10000)
                                {
                                    SetMessage(string.Format("�ܼ�ֵ{0}С��������ͼ�ֵ{1}������ ", totalprice, Task.PresentProductValue * 10000));
                                    return;
                                }
                                SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentProductId), count, productname, price, tempprice));
                                content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", count, DataConvert.GetEncodeData("����ũ����Ʒ����"), DataConvert.GetEncodeData(this._verifyCode), type, Operation.PresentProductId, aid));
                                GetPresentFeedback(content);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    SetMessageLn(string.Format("��������ָ����ũ����Ʒ��{0}...", GetAnimalProductNameByIdAndType(Task.PresentProductAid, Task.PresentProductType)));
                    foreach (ProductInfo product in products)
                    {
                        if (product.Aid == Task.PresentProductAid && product.Type == Task.PresentProductType)
                        {
                            if (Task.PresentProductCheckNum && product.Num < Task.PresentProductNum)
                            {
                                SetMessage(string.Format("����{0}< ��С������{1}������ ", product.Num, Task.PresentProductNum));
                                return;
                            }
                            ProductInfo productbase = GetAnimalProductByIdAndType(product.Aid, product.Type);
                            if (productbase == null)
                            {
                                SetMessage(string.Format("δ֪ũ����Ʒ{0}-{1}������ ", product.Aid, product.Type));
                                LogHelper.Write("GameRanch.PresentAnimalProduct" + CurrentAccount.UserName, string.Format("δ֪ũ����Ʒ{0}-{1}������ ", product.Aid, product.Type), LogSeverity.Warn);
                                return;
                            }
                            SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentProductId), product.Num, product.Name, productbase.Price, productbase.Price * product.Num));
                            content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", product.Num, DataConvert.GetEncodeData("����ũ����Ʒ����"), DataConvert.GetEncodeData(this._verifyCode), product.Type, Operation.PresentProductId, product.Aid));
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
                                        SetMessageLn(string.Format("#{0}{1}������������{2}�������ۼۣ�{3}���ܼ�ֵ��{4}", ++num, JsonHelper.GetMid(content, "<name>", "</name>"), JsonHelper.GetMid(content, "<selfnum>", "</selfnum>"), JsonHelper.GetMid(content, "<price>", "</price>"), tempprice));
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
                                            SetMessage(string.Format("��������{0}< ��С������{1}������ ", count, Task.PresentProductNum));
                                            return;
                                        }
                                        SetMessageLn(string.Format("��{0}����{1}��{2}(����:{3})���ܼ�ֵ��{4}Ԫ ", GetFriendNameById(Operation.PresentProductId), count, productname, price, tempprice));
                                        content = HH.Post("http://www.kaixin001.com/!house/!ranch//presentfruit.php", string.Format("num={0}&pmsg={1}&verify={2}&type={3}&anon=0&touid={4}&id={5}", count, DataConvert.GetEncodeData("����ũ����Ʒ����"), DataConvert.GetEncodeData(this._verifyCode), product.Type, Operation.PresentProductId, product.Aid));
                                        GetPresentFeedback(content);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    SetMessage("�ֿ���û�и�ũ����Ʒ���޷����͡�");
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
                SetMessage(" ��" + GetFriendNameById(Operation.PresentId) + "����ũ����Ʒʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetPresentFeedback
        private bool GetPresentFeedback(string content)
        {
            try
            {
                //<data><ret>succ</ret><tips>�ѳɹ����͸�������1ֻ����&lt;br&gt;����������ϵͳ��Ϣ���յ�</tips></data>
                //<data><ret>fail</ret><reason>һ��ֻ�ܸ�ͬһ��������һ��ũ����Ʒ</reason></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ����ʧ�ܣ�");
                    if (content.Contains("��������ȷ"))
                        _incorrentcount = true;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("succ", "") + " ���ͳɹ���");
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
                SetMessageLn("��ʼ����ũ����Ʒ...");
                if (Task.SellProductLowCash)
                {
                    if (ranch.Cash > Task.SellProductLowCashLimit * 10000)
                    {
                        SetMessageLn(string.Format("����{0}Ԫ�ֽ�������ۡ�", ranch.Cash));
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
                        SetMessage("�ֿ���û���κ�ũ����Ʒ");
                        return;
                    }

                    //�����ֵ
                    long soldvalue = 0;
                    long productvalue = 0;
                    int num = 0;
                    foreach (ProductInfo product in products)
                    {
                        productvalue = 0;
                        if (soldvalue >= Task.SellProductMaxLimit * 10000)
                        {
                            SetMessageLn(string.Format("�ѳ��۵�ũ����Ʒ�ܼ�ֵ�Ѿ�����{0}��ֹͣ���ۡ�", Task.SellProductMaxLimit));
                            break;
                        }

                        SetMessageLn(string.Format("#{0} ", ++num));
                        if (IsContained(product.Aid, product.Type))
                        {
                            SetMessage(string.Format("{0}�ڳ��۵Ľ�ֹ�б��У�����", product.Name));
                            continue;
                        }

                        int productprice = GetAnimalProductPriceByIdAndType(product.Aid, product.Type);
                        if (productprice <= 0)
                        {
                            SetMessage(string.Format("δ֪ũ����Ʒ{0}-{1}������", product.Aid, product.Type));
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
                            SetMessage(string.Format("�ѳ��۵�ũ����Ʒ�ܼ�ֵ��{0}Ԫ", soldvalue));
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
                SetMessage("����ũ����Ʒʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetSellProductFeedback
        private bool GetSellProductFeedback(string content, ref long totalprice)
        {
            try
            {
                //<data><ret>fail</ret><reason>��������</reason></data>
                //<data><ret>succ</ret><all>0</all><num>1</num><totalprice>70000</totalprice><tips>������С��� &lt;font color='#008000'&gt;&lt;b&gt;1&lt;/b&gt;&lt;/font&gt;ֻ&lt;br /&gt;��������� &lt;font color='#FF6600'&gt;&lt;b&gt;70000&lt;/b&gt;&lt;/font&gt;Ԫ</tips></data>
                if (content.Contains("<ret>fail</ret>"))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ����ʧ�ܣ�");
                }
                else if (content.Contains("<ret>succ</ret>"))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<tips>", "</tips>")) + " ���۳ɹ���");
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

        public string RequestAllRanchFriends()
        {
            //�����ĺ�����ʵ�����������ӵĺ���
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
