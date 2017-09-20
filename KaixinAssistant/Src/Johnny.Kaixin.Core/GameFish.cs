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
    public class GameFish : KaixinBase
    {
        private Collection<FriendInfo> _allFishFriendsList;
        private Collection<FriendInfo> _harvestFishFriendsList;
        private Collection<FishFryInfo> _fishFrysList; //����
        private Collection<FishTackleInfo> _fishTacklesList; //���
        private Collection<FishMaturedInfo> _fishMaturedList; //�������
        private int _myRank;
        private bool _canbuyfish;
        private bool _canupdatefish;
        private bool _canfish;
        private bool _iscontainfish39;

        public delegate void AllFishFriendsFetchedEventHandler(Collection<FriendInfo> allfishfriends);
        public event AllFishFriendsFetchedEventHandler AllFishFriendsFetched;

        public delegate void FishFrysInShopFetchedEventHandler(Collection<FishFryInfo> fishfrys);
        public event FishFrysInShopFetchedEventHandler FishFrysFetched;

        public delegate void FishTacklesInShopFetchedEventHandler(Collection<FishTackleInfo> fishtackles);
        public event FishTacklesInShopFetchedEventHandler FishTacklesFetched;

        public GameFish()
        {
            this._myRank = 1;
            this._canbuyfish = true;
            this._canupdatefish = true;
            this._canfish = true;
            this._iscontainfish39 = false;
            this._allFishFriendsList = new Collection<FriendInfo>();
            this._harvestFishFriendsList = new Collection<FriendInfo>();
            this._fishFrysList = new Collection<FishFryInfo>();
            this._fishTacklesList = new Collection<FishTackleInfo>();
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //fish
                SetMessageLn("���ڳ�ʼ��[����]...");

                string content = RequestFishHomePage(true);

                //all fish friends
                content = RequestAllFishFriends();
                ReadAllFishFriends(content, false);
                SetMessage("[���е���ĺ���]��Ϣ���سɹ���");
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
                LogHelper.Write("GameFish.Initialize", ex, LogSeverity.Error);
                SetMessage(" ��ʼ��[����]ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetAllFishFriends
        public void GetAllFishFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAllFishFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void GetAllFishFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_FISH;
                SetMessageLn("ˢ��[���е���ĺ���]...");

                if (!this.ValidationLogin(true))
                {
                    if (AllFishFriendsFetched != null)
                        AllFishFriendsFetched(_allFishFriendsList);
                    return;
                }

                string content = RequestFishHomePage(false);
                content = RequestAllFishFriends();
                ReadAllFishFriends(content, true);
                SetMessageLn("[���е���ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (AllFishFriendsFetched != null)
                    AllFishFriendsFetched(_allFishFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[���е���ĺ���]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region ReadAllFishFriends
        public void ReadAllFishFriends(string content, bool printMessage)
        {
            //{"fishing":[],"fishable":[{"uid":10151052,"realname":"\u5b59\u6b63\u82b3","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/15\/10\/20_10151052_1.jpg","extext":"\u53ef\u9493","decor":0},{"uid":1283947,"realname":"\u6797\u8054\u5bf9","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/28\/39\/20_1283947_2.jpg","extext":"\u53ef\u9493","decor":0},{"uid":2511621,"realname":"\u4e07\u6d69","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/51\/16\/20_2511621_5.jpg","extext":"\u53ef\u9493","decor":0},{"uid":26366307,"realname":"\u6731\u4f1f","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/36\/63\/20_26366307_2.jpg","extext":"\u53ef\u9493","decor":0},{"uid":2803054,"realname":"\u8f66\u79be\u5409","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/80\/30\/20_2803054_1.jpg","extext":"\u53ef\u9493","decor":0},{"uid":3092481,"realname":"\u534e\u7ef4\u82ac","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/9\/24\/20_3092481_6.jpg","extext":"\u53ef\u9493","decor":0},{"uid":3125472,"realname":"\u738b\u535a\u667a","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/12\/54\/20_3125472_9.jpg","extext":"\u53ef\u9493","decor":0},{"uid":4026057,"realname":"\u8521\u632f\u534e","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/2\/60\/20_4026057_2.jpg","extext":"\u53ef\u9493","decor":1},{"uid":434362,"realname":"\u59da\u851a\u98de(\uff2aa","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/43\/43\/20_434362_15.jpg","extext":"\u53ef\u9493","decor":1},{"uid":4789786,"realname":"\u5b5f\u519b\u534e","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/78\/97\/20_4789786_2.jpg","extext":"\u53ef\u9493","decor":0},{"uid":5969055,"realname":"\u534e\u654f\u5cf0","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/96\/90\/20_5969055_7.jpg","extext":"\u53ef\u9493","decor":1},{"uid":6194153,"realname":"\u5e84\u5b50","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/19\/41\/20_6194153_1.jpg","extext":"\u53ef\u9493","decor":0},{"uid":6352682,"realname":"\u674e\u4e3a\u6c11","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/35\/26\/20_6352682_4.jpg","extext":"\u53ef\u9493","decor":0},{"uid":7744681,"realname":"\u6587\u6653\u6653","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/74\/46\/20_7744681_2.jpg","extext":"\u53ef\u9493","decor":0},{"uid":8006646,"realname":"\u59da\u7426\u6590","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/0\/66\/20_8006646_1.jpg","extext":"\u53ef\u9493","decor":0},{"uid":405591,"realname":"\u5c45\u7b11\u5929","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/40\/55\/20_405591_1.jpg","extext":"","decor":2},{"uid":2395406,"realname":"\u6731\u8273\u96ef","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/39\/54\/20_2395406_3.jpg","extext":"","decor":1},{"uid":1922571,"realname":"\u4f40\u95ef","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/92\/25\/20_1922571_5.jpg","extext":"","decor":1},{"uid":3788889,"realname":"\u6c88\u51ef\u8f89","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/78\/88\/20_3788889_10.jpg","extext":"","decor":1},{"uid":4538025,"realname":"\u738b\u747e","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/53\/80\/20_4538025_6.jpg","extext":"","decor":1},{"uid":3612627,"realname":"\u5468\u6797","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/61\/26\/20_3612627_4.jpg","extext":"","decor":1},{"uid":5629041,"realname":"\u5f20\u52e4","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/62\/90\/20_5629041_5.jpg","extext":"","decor":1},{"uid":9134133,"realname":"\u5f90\u6c47\u6c5f","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/13\/41\/20_9134133_1.jpg","extext":"","decor":1},{"uid":11860509,"realname":"\u5f90\u5723\u541b","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/5\/20_11860509_1.jpg","extext":"","decor":1},{"uid":4585976,"realname":"\u65b9\u5fd7\u8363","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/58\/59\/20_4585976_3.jpg","extext":"","decor":2},{"uid":18643363,"realname":"\u595a\u51e4(\u6021\u6021)","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/64\/33\/20_18643363_1.jpg","extext":"","decor":1},{"uid":28860603,"realname":"\u9648\u5609\u59ae","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/86\/6\/20_28860603_6.jpg","extext":"","decor":1},{"uid":32263316,"realname":"\u5f90\u6daf\u7433","online":0,"icon20":"http:\/\/pic.kaixin001.com\/logo\/26\/33\/20_32263316_15.jpg","extext":"","decor":1},{"uid":10168533,"realname":"\u864e\u738b","online":1,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/16\/85\/20_10168533_1.jpg","extext":"","decor":1},{"uid":7134351,"realname":"\u5f3a\u601d\u601d","online":1,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/13\/43\/20_7134351_4.jpg","extext":"","decor":1}],"imgdomain":"img.kaixin001.com.cn"}
            //{"fishing":[{"uid":7995015,"realname":"\u5b5f\u83b7","online":0,"icon20":"http:\/\/img.kaixin001.com.cn\/i\/20_1_0.gif","extext":"\u4e0a\u94a9","decor":0},{"uid":6194153,"realname":"\u5e84\u5b50","online":0,"icon20":"http:\/\/pic1.kaixin001.com\/logo\/19\/41\/20_6194153_1.jpg","extext":"\u4e0a\u94a9","decor":0}],"fishable":[],"imgdomain":"img.kaixin001.com.cn"}
            this._allFishFriendsList.Clear();
            this._harvestFishFriendsList.Clear();

            if (printMessage)
                SetMessageLn("��ȡ[���е���ĺ���]��Ϣ:");

            if (content.IndexOf("\"fishing\":[],") > -1 && content.IndexOf(",\"fishable\":[]") > -1)
            {
                if (printMessage)
                    SetMessageLn("û���κε���ĺ��ѣ�");
                return;
            }

            content = content.Replace("\"fishing\":[],", "").Replace(",\"fishable\":[]", "").Replace(",\"imgdomain\":\"img.kaixin001.com.cn\"", "");
            JsonTextParser parser = new JsonTextParser();

            JsonObjectCollection objects = parser.Parse(content) as JsonObjectCollection;
            //���¸˵�����
            JsonArrayCollection arrayFishingFriends = objects["fishing"] as JsonArrayCollection;
            if (arrayFishingFriends != null)
            {
                if (printMessage)
                    SetMessageLn("���¸˵�������");
                foreach (JsonObjectCollection item in arrayFishingFriends)
                {
                    FriendInfo friend = new FriendInfo();
                    friend.Id = JsonHelper.GetIntegerValue(item["uid"]);
                    friend.Name = JsonHelper.GetStringValue(item["realname"]);
                    this._harvestFishFriendsList.Add(friend);
                    if (printMessage)
                        SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
                }
            }
            //�ɵ�������
            JsonArrayCollection arrayFishableFriends = objects["fishable"] as JsonArrayCollection;
            if (arrayFishableFriends != null)
            {
                foreach (JsonObjectCollection item in arrayFishableFriends)
                {
                    FriendInfo friend = new FriendInfo();
                    friend.Id = JsonHelper.GetIntegerValue(item["uid"]);
                    friend.Name = JsonHelper.GetStringValue(item["realname"]);
                    friend.Decor = JsonHelper.GetIntegerValue(item["decor"]) == 0 ? true : false;
                    this._allFishFriendsList.Add(friend);
                    if (printMessage)
                    {
                        SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
                        if (friend.Decor)
                            SetMessage("�ɵ�");
                    }

                }
            }
        }
        #endregion

        #region GetFishFrysInShop
        public void GetFishFrysInShopByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetFishFrysInShop));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetFishFrysInShop()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_UPDATEDATA;
                SetMessageLn("ˢ��[�̵��������б�]...");

                if (!this.ValidationLogin())
                {
                    if (FishFrysFetched != null)
                        FishFrysFetched(null);
                    return;
                }

                string content = RequestFishHomePage(false);
                content = RequestFishFrysList();

                Collection<FishFryInfo> fishfrys = ConfigCtrl.GetOriginalFishFrys(content);
                fishfrys = SortFishFrysListByPrice(fishfrys);
                if (fishfrys == null || fishfrys.Count == 0)
                    SetMessageLn("[�̵��������б�]��Ϣˢ��ʧ�ܣ�");
                else
                    SetMessageLn("[�̵��������б�]��Ϣˢ�³ɹ���");

                //invoke event
                if (FishFrysFetched != null)
                    FishFrysFetched(fishfrys);
            });
            base.ExecuteTryCatchBlock(th, "[�̵��������б�]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region GetMaxWeight
        private Collection<FishFryInfo> GetMaxWeight(Collection<FishFryInfo> fishfrys)
        {
            try
            {
                foreach (FishFryInfo fishfry in fishfrys)
                {

                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    string content = HH.Post("http://www.kaixin001.com/!fish/!fishinfo.php", string.Format("verify={0}&fid={1}&fuid=0&r=0%2E5060226498171687", DataConvert.GetEncodeData(this._verifyCode), fishfry.FId));
                    if (content.Contains("<ret>succ</ret>"))
                        fishfry.MaxWeight = DataConvert.GetDecimal(JsonHelper.GetMid(content, "���������", "��"));
                }

                return fishfrys;
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
                LogHelper.Write("GameFish.GetMaxWeight", ex, LogSeverity.Error);
                SetMessage("�޷���ȡ�������������" + ex.Message);
                return null;
            }
        }
        #endregion

        #region GetFishTackleInShop
        public void GetFishTackleInShopByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetFishTackleInShop));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetFishTackleInShop()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_UPDATEDATA;
                SetMessageLn("ˢ��[�̵�������б�]...");

                if (!this.ValidationLogin())
                {
                    if (FishTacklesFetched != null)
                        FishTacklesFetched(null);
                    return;
                }

                string content = RequestFishHomePage(false);
                content = RequestFishTacklesList();

                Collection<FishTackleInfo> fishtackles = ConfigCtrl.GetOriginalFishTackles(content);
                if (fishtackles == null || fishtackles.Count == 0)
                    SetMessageLn("[�̵�������б�]��Ϣˢ��ʧ�ܣ�");
                else
                    SetMessageLn("[�̵�������б�]��Ϣˢ�³ɹ���");

                //invoke event
                if (FishTacklesFetched != null)
                    FishTacklesFetched(fishtackles);
            });
            base.ExecuteTryCatchBlock(th, "[�̵�������б�]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region RunFish
        public void RunFish()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_FISH;

                SetMessageLn("��ʼ����...");

                //fish
                string contentHome = RequestFishHomePage(false);
                PondInfo pond = ReadPond(this._verifyCode, CurrentAccount.UserId, true);
                if (pond == null)
                {
                    SetMessageLn("�޷���ȡ�ҵ�������Ϣ��");
                    return;
                }

                //�ҵ����
                SetMessageLn("�ҵ���ͣ�");
                string content = RequestMyTacklesList();
                Collection<FishTackleInfo> myTackcles = ConfigCtrl.GetMyTackles(content);

                for (int ix = 0; ix < myTackcles.Count; ix++)
                {
                    if (myTackcles[ix].Status == 1)
                    {
                        SetMessageLn(string.Format("#{0} ���� {1}", ix + 1, myTackcles[ix].Title));
                        if (myTackcles[ix].Status == 1 && myTackcles[ix].BUse == 1)
                            SetMessage("(����ʹ��)");
                    }
                }

                if (Task.Shake)
                    Shake(pond);

                if (Task.TreatFish)
                    TreatFish(pond);

                if (Task.UpdateFishPond)
                    UpdateFishPond();

                if (Task.BangKeJing)
                    BangKeJing(pond);

                if (Task.NetSelfFish)
                {
                    NetSelfFish(pond);
                    pond = ReadPond(this._verifyCode, CurrentAccount.UserId, false);
                    if (pond == null)
                    {
                        SetMessageLn("�޷���ȡ�ҵ�������Ϣ��");
                        return;
                    }
                }

                if (Task.BuyFish)
                    BuyFish(pond);

                if (Task.BuyUpdateTackle)
                    BuyFishTackles(pond);

                if (Task.HarvestFish)
                {
                    content = RequestAllFishFriends();
                    ReadAllFishFriends(content, false);
                    HarvestFish();
                }

                if (Task.Fishing)
                {
                    content = RequestAllFishFriends();
                    ReadAllFishFriends(content, false);
                    FishingPonds(pond);
                }

                if (Task.HelpFish)
                    HelpFish();

                if (Task.PresentFish)
                    PresentFish();

                if (Task.SellFish)
                    SellFish(pond);

                SetMessageLn("������ɣ�");

            });
            base.ExecuteTryCatchBlock(th, "�����쳣������ʧ�ܣ�");
        }
        #endregion

        #region ReadPond
        private PondInfo ReadPond(string verifyCode, string fuid, bool printmessage)
        {
            PondInfo pond = null;
            //RequestFish(fuid);
            string strFish = RequestFishConf(verifyCode, fuid);
            pond = ConfigCtrl.GetPond(strFish);
            if (pond == null)
            {
                if (printmessage)
                    SetMessageLn("��ȡ������Ϣʧ�ܣ�");
            }
            else
            {
                if (CurrentAccount.UserId == fuid)
                    this._myRank = pond.Rank;

                if (printmessage)
                {

                    SetMessageLn(string.Format("{0}��{1} {2} {3}", pond.Title, pond.RankTip, pond.Fish, pond.CashTips));
                    //fishs
                    if (pond.Fishs != null)
                    {
                        int num = 0;
                        foreach (FishInfo fish in pond.Fishs)
                            SetMessageLn(string.Format("��{0}���㣺{1}", ++num, fish.Tips));
                    }

                    //fishers
                    for (int ix = 1; ix < 5; ix++)
                    {
                        SetMessageLn(string.Format("��{0}����λ��", ix));

                        if (pond.Fishers != null)
                        {
                            bool find = false;
                            foreach (FisherInfo fisher in pond.Fishers)
                            {
                                if (fisher.Pos == ix)
                                {
                                    SetMessage(fisher.Name);
                                    find = true;
                                    break;
                                }                             
                            }
                            if (find)
                                continue;
                        }

                        SetMessage("����");
                    }
                }
            }

            return pond;
        }
        #endregion

        #region Shake
        private void Shake(PondInfo pond)
        {
            //<data>
            //  <shake>
            //    <item>
            //      <skey>tools-monster</skey>
            //      <pic>http://img.kaixin001.com.cn/i3/fish/tools/dj_monster.swf</pic>
            //    </item>
            //    <item>
            //      <skey>tools-fisherman</skey>
            //      <pic>http://img.kaixin001.com.cn/i3/fish/tools/dj_fisherman.swf</pic>
            //    </item>
            //    <item>
            //      <skey>tools-food</skey>
            //      <pic>http://img.kaixin001.com.cn/i3/fish/tools/dj_food2.swf</pic>
            //    </item>
            //    <item>
            //      <skey>fish-luopao</skey>
            //      <pic>http://img.kaixin001.com.cn/i3/fish/fish2/dingziluopao.swf</pic>
            //    </item>
            //    <item>
            //      <skey>wishs</skey>
            //      <pic>
            //      </pic>
            //    </item>
            //    <item>
            //      <skey>again</skey>
            //      <pic>
            //      </pic>
            //    </item>
            //  </shake>
            //</data>
            try
            {
                SetMessageLn("��ʼת��...");

                if (!pond.Shakable)
                {
                    SetMessage("�����ѳ������������������");
                    return;
                }

                string content = HH.Post("http://www.kaixin001.com/!fish/!shake.php", string.Format("verify={0}&r=0%2E1539040575735271", DataConvert.GetEncodeData(this._verifyCode)));
                content = HH.Post("http://www.kaixin001.com/!fish/!myshake.php", string.Format("verify={0}&r=0%2E6482421010732651", DataConvert.GetEncodeData(this._verifyCode)));
                GetShakeFeedback(content);
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
                LogHelper.Write("GameFish.Shake", ex, LogSeverity.Error);
                SetMessage("ת��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetShakeFeedback
        private void GetShakeFeedback(string content)
        {
            try
            {
                //<data>
                //  <shake>
                //    <item>
                //      <skey>tools-monster</skey>
                //      <pic>http://img.kaixin001.com.cn/i3/fish/tools/dj_monster.swf</pic>
                //    </item>
                //    <item>
                //      <skey>tools-fisherman</skey>
                //      <pic>http://img.kaixin001.com.cn/i3/fish/tools/dj_fisherman.swf</pic>
                //    </item>
                //    <item>
                //      <skey>tools-food</skey>
                //      <pic>http://img.kaixin001.com.cn/i3/fish/tools/dj_food2.swf</pic>
                //    </item>
                //    <item>
                //      <skey>fish-luopao</skey>
                //      <pic>http://img.kaixin001.com.cn/i3/fish/fish2/dingziluopao.swf</pic>
                //    </item>
                //    <item>
                //      <skey>wishs</skey>
                //      <pic>
                //      </pic>
                //    </item>
                //    <item>
                //      <skey>again</skey>
                //      <pic>
                //      </pic>
                //    </item>
                //  </shake>
                //  <myshake>tools-fisherman</myshake>
                //  <msg>��ϲ��õ��ߺ���������ѷ�����ĵ��߿���</msg>
                //</data>
                Regex regular = new Regex(@"<msg>[\s\S]+</msg>");
                if (regular.IsMatch(content))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(regular.Match(content).ToString()));                    
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
                LogHelper.Write("GameFish.GetShakeFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region TreatFish
        private void TreatFish(PondInfo pond)
        {
            try
            {
                SetMessageLn("��ʼ�β�...");

                if (string.IsNullOrEmpty(pond.SickTips))
                {
                    SetMessage("�����ܽ���������Ҫ�β�");
                    return;
                }

                string content = HH.Post("http://www.kaixin001.com/!fish/!treatfish.php", string.Format("verify={0}&r=0%2E12005394231528044", DataConvert.GetEncodeData(this._verifyCode)));
                GetTreatFishFeedback(content);
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
                LogHelper.Write("GameFish.TreatFish", ex, LogSeverity.Error);
                SetMessage("�β�ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetTreatFishFeedback
        private void GetTreatFishFeedback(string content)
        {
            try
            {
                //<data>
                //  <cashRet>
                //    <cash>120</cash>
                //    <cashtips>sdfsdf</cashtips>
                //  </cashRet>
                //  <msg>�㸶��1000Ԫ���Ʒѣ���ҽ���������Ĳ��κ�����������ɼ�������������</msg>
                //  <ret>succ</ret>
                //  <err>0</err>
                //</data>
                Regex regular = new Regex(@"<msg>[\s\S]+</msg>");
                if (regular.IsMatch(content))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(regular.Match(content).ToString()));
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
                LogHelper.Write("GameFish.GetTreatFishFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region UpdateFishPond
        private void UpdateFishPond()
        {
            try
            {
                SetMessageLn("��ʼ��������...");

                HH.DelayedTime = Constants.DELAY_1SECONDS;
                string content = HH.Post("http://www.kaixin001.com/!fish/!updateinfo.php", string.Format("verify={0}&r=0%2E5434148698113859", DataConvert.GetEncodeData(this._verifyCode)));
                //<data><update>1</update><rtips>&lt;b&gt;��Ŀǰ�ļ�����&lt;font color='#FF6600'&gt;19&lt;/font&gt;��&lt;/b&gt;</rtips><upinfo><item><upnum>10</upnum><msg>��1000Ԫ������10����</msg></item><item><upnum>11</upnum><msg>��2000Ԫ������11����</msg></item><item><upnum>12</upnum><msg>��3000Ԫ������12����</msg></item><item><upnum>13</upnum><msg>��4000Ԫ������13����</msg></item><item><upnum>14</upnum><msg>��5000Ԫ������14����</msg></item><item><upnum>15</upnum><msg>��6000Ԫ������15����</msg></item></upinfo></data>
                if (content.IndexOf("<update>1</update>") < 0)
                {
                    SetMessage("��������Ҫ���ݣ�");
                    return;
                }
                string upnum = JsonHelper.GetMidLast(content, "<upnum>", "</upnum>");
                if (!String.IsNullOrEmpty(upnum))
                {
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    content = HH.Post("http://www.kaixin001.com/!fish/!updatefishpond.php", string.Format("verify={0}&num={1}&r=0%2E9150360440835357", DataConvert.GetEncodeData(this._verifyCode), upnum));
                    GetUpdateFishPondFeedback(content);
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
                LogHelper.Write("GameFish.UpdateFishPond", ex, LogSeverity.Error);
                SetMessage("��������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetUpdateFishPondFeedback
        private void GetUpdateFishPondFeedback(string content)
        {
            try
            {
                //<data>
                //  <cashRet>
                //    <cash>2��</cash>
                //    <cashtips>�ֽ�24033Ԫ</cashtips>
                //  </cashRet>
                //  <ret>succ</ret>
                //  <msg>���ѽ���6000Ԫ���ɹ����������ݡ�&lt;br&gt;������������ڿ�����15���㡣</msg>
                //  <err>0</err>
                //  <accountfish>
                //    <bbuytips>0</bbuytips>
                //    <fish>7/15</fish>
                //    <fishtips>����������7��&lt;br&gt;������15��</fishtips>
                //  </accountfish>
                //</data>
                Regex regular = new Regex(@"<msg>[\s\S]+</msg>");
                if (regular.IsMatch(content))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(regular.Match(content).ToString()));
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
                LogHelper.Write("GameFish.GetUpdateFishPondFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region BangKeJing
        private void BangKeJing(PondInfo pond)
        {
            try
            {
                //ǧ�����
                //<producturl>http://img.kaixin001.com.cn/swf/fish/fish/zhenzhubang_product-1.swf</producturl>
                //<producturl></producturl>

                //http://www.kaixin001.com/!fish/!pearlproduct.php
                //fishid=6856442152&r=0%2E646392990835011&verify=6209767%5F1106%5F6209767%5F1267622827%5Fbc341173c5d3323c5d99c8798a8901df
                //<data><msg>���м�����������ˮ��ǧ�������������顣&lt;br&gt;��������һ���ѷ�����Ĳֿ⡣</msg></data>
                
                SetMessageLn("��ʼ��ǧ�����������...");

                if (!pond.BangKeJing)
                {
                    SetMessage("����Ҫ��������");
                    return;
                }

                string content = HH.Post("http://www.kaixin001.com/!fish/!pearlproduct.php", string.Format("fishid={0}&r=0%2E646392990835011&verify={1}", pond.BangKeJingFishId, DataConvert.GetEncodeData(this._verifyCode)));
                SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
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
                LogHelper.Write("GameFish.BangKeJing", ex, LogSeverity.Error);
                SetMessage("��ǧ�����������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region BuyFish
        private void BuyFish(PondInfo pond)
        {
            //��������
            SetMessageLn("��������...");

            if (pond.BuyableCapacity <= 0)
            {
                SetMessage(string.Format("��������Ѿ��������ˣ�{0}", pond.Fish));
                return;
            }

            if (pond.Fishs.Count >= Task.MaxFishes)
            {
                SetMessage(string.Format("��������Ѿ���{0}�����ˣ�����", pond.Fishs.Count));
                return;
            }

            _iscontainfish39 = false;
            int count = pond.Fishs.Count;

            if (Task.BuyFishByRank)
            {
                Collection<FishFryInfo> rankFishs = SortFishFrysListByRank(this._fishFrysList);
                _canbuyfish = true;
                for (int ix = 0; ix < pond.BuyableCapacity; ix++)
                {
                    try
                    {
                        FishFryInfo fishfry = GetFishFryByRank(rankFishs, pond);
                        if (fishfry != null)
                        {
                            SetMessageLn(string.Format("#{0}����1��{1}��", ix + 1, fishfry.Name));
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            //http://www.kaixin001.com/!fish/!buyfish.php
                            //id=4&verify=2588258%5F1106%5F2588258%5F1258863064%5F0c75cf807573b8a53eab8e77c8810a6a&fuid=0&r=0%2E18200292345136404
                            string content = HH.Post("http://www.kaixin001.com/!fish/!buyfish.php", string.Format("id={0}&verify={1}&fuid=0&r=0%2E18200292345136404", fishfry.FId, DataConvert.GetEncodeData(this._verifyCode)));
                            if (GetBuyFishFeedback(content))
                            {
                                if (fishfry.FId == 39)
                                    _iscontainfish39 = true;
                                count++;
                                if (count >= Task.MaxFishes)
                                {
                                    SetMessageLn(string.Format("��������Ѿ���{0}�����ˣ�����", count));
                                    return;
                                } 
                            }
                            if (!_canbuyfish)
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GameFish.BuyFish(BuyFishByRank)", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
            else
            {
                _canbuyfish = true;
                for (int ix = 0; ix < pond.BuyableCapacity; ix++)
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0}����1��{1}��", ix + 1, GetFishNameById(Task.BuyFishFishId)));
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        //http://www.kaixin001.com/!fish/!buyfish.php
                        //id=4&verify=2588258%5F1106%5F2588258%5F1258863064%5F0c75cf807573b8a53eab8e77c8810a6a&fuid=0&r=0%2E18200292345136404
                        string content = HH.Post("http://www.kaixin001.com/!fish/!buyfish.php", string.Format("id={0}&verify={1}&fuid=0&r=0%2E18200292345136404", Task.BuyFishFishId, DataConvert.GetEncodeData(this._verifyCode)));
                        if (GetBuyFishFeedback(content))
                        {
                            count++;
                            if (count >= Task.MaxFishes)
                            {
                                SetMessageLn(string.Format("��������Ѿ���{0}�����ˣ�����", count));
                                return;
                            }
                        }
                        if (!_canbuyfish)
                            break;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GameFish.BuyFish(BuyFishFishId)", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
        }
        #endregion

        #region GetBuyFishFeedback
        private bool GetBuyFishFeedback(string content)
        {
            //<data>
            //  <fish>
            //    <item>
            //      <bproduct>0</bproduct>
            //      <ismermaid>0</ismermaid>
            //      <ac>
            //      </ac>
            //      <producturl>
            //      </producturl>
            //      <skey>caoyu</skey>
            //      <gswim>0</gswim>
            //      <fishid>4089968926</fishid>
            //      <pic>http://img.kaixin001.com.cn/i3/fish/fish/caoyu1_1.swf</pic>
            //      <icon>http://img.kaixin001.com.cn/i3/fish/fish/caoyu.swf</icon>
            //      <tips>&amp;lt;font color='#333333'&amp;gt;&amp;lt;b&amp;gt;����&amp;lt;/b&amp;gt;&amp;lt;/font&amp;gt;\n&amp;lt;font color='#339933'&amp;gt;��ǰ��0.3��&amp;lt;/font&amp;gt;&amp;lt;font color='#333333'&amp;gt;(���24.5��)&amp;lt;/font&amp;gt;\n&amp;lt;font color='#999999'&amp;gt;�ٹ�2Сʱ��0.6��&amp;lt;/font&amp;gt;</tips>
            //      <owner>0</owner>
            //    </item>
            //  </fish>
            //  <cashRet>
            //    <cash>1490</cash>
            //    <cashtips>�ֽ�1490Ԫ</cashtips>
            //  </cashRet>
            //  <ret>succ</ret>
            //  <msg>��������ɹ����Ѿ����������������&lt;br&gt;�������������ˮ��Ʒ��</msg>
            //  <err>0</err>
            //  <accountfish>
            //    <bbuytips>1</bbuytips>
            //    <fish>1/8</fish>
            //    <fishtips>����������1��&lt;br&gt;������8��</fishtips>
            //  </accountfish>
            //</data>
            //<data><ret>fail</ret><msg>鱼塘只能�?个珍珠蚌</msg><err>1</err></data>
            if (String.IsNullOrEmpty(content))
                return true;

            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + " ����ʧ�ܣ�");
                //LogHelper.Write(CurrentAccount.UserName, JsonHelper.FiltrateHtmlTags(content).Replace("fail", "") + " ʧ�ܣ�", LogSeverity.Warn);
                if (content.IndexOf("����ֽ���") > -1)
                {
                    _canbuyfish = false;
                }                              
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                SetMessage("��ɣ�");
                return true;
            }
            else
            {
                SetMessage(content);
                LogHelper.Write(CurrentAccount.UserName, content, LogSeverity.Warn);
            }
            return false;
        }
        #endregion

        #region BuyFishTackles
        private void BuyFishTackles(PondInfo pond)
        {
            SetMessageLn("����/������ͣ�");

            //�ҵ����
            string content = RequestMyTacklesList();
            Collection<FishTackleInfo> myTackcles = ConfigCtrl.GetMyTackles(content);

            Collection<FishTackleInfo> rankTackles = SortFishTacklesListByRank(this._fishTacklesList);
            _canbuyfish = true;
            _canupdatefish = true;
            int count = GetMyTackleCount(myTackcles);
            for (int ix = 0; ix < myTackcles.Count; ix++)
            {
                try
                {
                    if (myTackcles[ix].Status == 1)
                    {
                        SetMessageLn(string.Format("#{0} {1}...", ix + 1, myTackcles[ix].Title));
                        //
                        FishTackleInfo fishtackle = GetFishTackleByRank(rankTackles, pond.Rank);
                        if (fishtackle != null)
                        {
                            if (fishtackle.TId != myTackcles[ix].TId)
                            {
                                SetMessage(string.Format(" ��������Ϊ{0}��", fishtackle.Name));
                                HH.DelayedTime = Constants.DELAY_5SECONDS;
                                content = HH.Post("http://www.kaixin001.com/!fish/!chgtackle.php", string.Format("verify={0}&oldid={1}&tid={2}&r=0%2E16443684557452798", DataConvert.GetEncodeData(this._verifyCode), myTackcles[ix].TackleId, fishtackle.TId));
                                GetUpdateFishTackleFeedback(content);
                                if (!_canupdatefish)
                                    break;
                            }
                        }                        
                    }
                    else if (myTackcles[ix].Status == 0)
                    {                        
                        FishTackleInfo fishtackle = GetFishTackleByRank(rankTackles, pond.Rank);
                        if (fishtackle != null)
                        {
                            SetMessageLn(string.Format("#{0} ����1��{1}", ix + 1, fishtackle.Name));
                            if (count >= Task.MaxTackles)
                            {
                                SetMessage(string.Format(" �Ѿ���{0}������ˣ�����", count));
                                continue;
                            }
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            //http://www.kaixin001.com/!fish/!buyfish.php
                            //id=4&verify=2588258%5F1106%5F2588258%5F1258863064%5F0c75cf807573b8a53eab8e77c8810a6a&fuid=0&r=0%2E18200292345136404
                            content = HH.Post("http://www.kaixin001.com/!fish/!buyfishtackle.php", string.Format("id={0}&verify={1}&fuid=0&r=0%2E9835843918845057", fishtackle.TId, DataConvert.GetEncodeData(this._verifyCode)));
                            if (GetBuyFishTackleFeedback(content))
                                count++;
                            if (!_canbuyfish)
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Write("GameFish.BuyFishTackles", ex, LogSeverity.Error);
                    continue;
                }
            }
        }
        #endregion

        #region GetUpdateFishTackleFeedback
        private bool GetUpdateFishTackleFeedback(string content)
        {
            //<data><ret>fail</ret><msg>����ֽ��㣬���ܸ���</msg><err>1</err></data>
            //<data><ret>fail</ret><msg>��Ŀǰ�ļ����ܸ����ɸ����</msg><err>1</err></data>
            //<data><cashRet><cash>2330</cash><cashtips>�ֽ�2330Ԫ</cashtips></cashRet><ret>succ</ret><msg>������ͳɹ�</msg><err>0</err></data>
            if (String.IsNullOrEmpty(content))
                return true;

            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                Regex regular = new Regex(@"<msg>[\s\S]+</msg>");
                if (regular.IsMatch(content))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(regular.Match(content).ToString()));
                }
                if (content.IndexOf("����ֽ��㣬���ܸ���") > -1)
                {
                    _canupdatefish = false;
                    LogHelper.Write(CurrentAccount.UserName, "�ֽ𲻹��������������", LogSeverity.Warn);
                }
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                SetMessage(" ��ɣ�");
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

        #region GetBuyFishTackleFeedback
        private bool GetBuyFishTackleFeedback(string content)
        {
            //<data><ret>fail</ret><msg>��Ŀǰ�ļ������ֻ��ӵ��3�����</msg><err>1</err></data>
            if (String.IsNullOrEmpty(content))
                return true;

            if (content.IndexOf("<ret>fail</ret>") > -1)
            {
                Regex regular = new Regex(@"<msg>[\s\S]+</msg>");
                if (regular.IsMatch(content))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(regular.Match(content).ToString()));
                }
                if (content.IndexOf("����ֽ𲻹������ܹ���") > -1)
                {
                    LogHelper.Write(CurrentAccount.UserName, "�ֽ𲻹������ܹ������", LogSeverity.Warn);
                    _canbuyfish = false;
                }
            }
            else if (content.IndexOf("<ret>succ</ret>") > -1)
            {
                SetMessage(" ��ɣ�");
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

        #region HarvestFish
        private void HarvestFish()
        {
            try
            {
                int num = 0;

                SetMessageLn("��ʼȥ����...");

                //��������
                num = 0;
                foreach (FriendInfo friend in this._harvestFishFriendsList)
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                        PondInfo pond = ReadPond(this._verifyCode, friend.Id.ToString(), true);

                        foreach (FisherInfo fisher in pond.Fishers)
                        {
                            if (fisher.FStat == 1)
                            {
                                SetMessageLn(string.Format("#{0} ���� ", fisher.Pos));
                                HH.DelayedTime = Constants.DELAY_4SECONDS;
                                string content = HH.Post("http://www.kaixin001.com/!fish/!finishfish.php", string.Format("r=0%2E3449397818185389&verify={0}&fuid={1}&pos={2}&touid={3}&tackleid={4}", DataConvert.GetEncodeData(this._verifyCode), friend.Id, fisher.Pos, fisher.UId, fisher.TackleId));
                                GetHarvestFishFeedback(content);
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
                        LogHelper.Write("GameFish.HarvestFish", friend.Name, ex, LogSeverity.Error);
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
                LogHelper.Write("GameFish.HarvestFish", ex, LogSeverity.Error);
            }
        }
        #endregion

        #region GetHarvestFishFeedback
        private bool GetHarvestFishFeedback(string content)
        {
            try
            {
                //<data>
                //  <anti>0</anti>
                //  <tackleid>77976489</tackleid>
                //  <pos>3</pos>
                //  <fishid>3868238503</fishid>
                //  <fishid2>3868238503</fishid2>
                //  <err>0</err>
                //  <sicktime>0000-00-00 00:00:00</sicktime>
                //  <recoverytime>0000-00-00 00:00:00</recoverytime>
                //  <sick>0</sick>
                //  <msg>恭喜你！&lt;br&gt;成功钓到1条草鱼，�?4.5斤已放入你的仓库�?</msg>
                //  <wapmsg>1条草鱼，�?4.5�?</wapmsg>
                //  <rankmemo>这是你钓到的最大的草鱼</rankmemo>
                //  <fid>2</fid>
                //  <memo>&lt;b&gt;草鱼&lt;/b&gt;&lt;br&gt;&lt;font color='#FF6600'&gt;&lt;b&gt;24.5�?lt;/b&gt;&lt;/font&gt;</memo>
                //  <pic>http://img.kaixin001.com.cn/i3/fish/fish/caoyu.swf</pic>
                //  <accountfish>
                //    <bbuytips>0</bbuytips>
                //    <fish>7/8</fish>
                //    <fishtips>养鱼数量�?�?lt;br&gt;可容�?�?</fishtips>
                //  </accountfish>
                //  <ret>succ</ret>
                //</data>
                if (String.IsNullOrEmpty(content))
                    return true;
                Regex regular = new Regex(@"<msg>[\s\S]+</msg>");
                if (regular.IsMatch(content))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(regular.Match(content).ToString()));
                }
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    return false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    //SetMessage("�¸˳ɹ���");
                    return true;
                }
                else
                {
                    LogHelper.Write(CurrentAccount.UserName, content, LogSeverity.Warn);
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
                LogHelper.Write("GameFish.GetHarvestFishFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region FishingPonds
        private void FishingPonds(PondInfo pond)
        {
            try
            {
                int num = 0;

                SetMessageLn("��ʼȥ���ѵ���������...");

                bool canfish = false;
                string content = RequestMyTacklesList();
                Collection<FishTackleInfo> myTackcles = ConfigCtrl.GetMyTackles(content);
                foreach (FishTackleInfo fishtackle in myTackcles)
                {
                    if (fishtackle.Status == 1 && fishtackle.BUse == 0)
                    {
                        canfish = true;
                    }
                }

                if (!canfish)
                    return;

                //��ȥ�������е���������
                SetMessageLn("��ʼȥ�������к��ѵ��������㣺");
                foreach (int uid in Operation.FishingWhiteList)
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");

                        FriendInfo friend = GetFishFriendById(uid);
                        if (friend == null)
                        {
                            SetMessage("ûʲô�ɵ��ģ�����");
                            continue;
                        }
                        if (Operation.FishingBlackList.Contains(uid))
                        {
                            SetMessage(base.GetFriendNameById(uid) + "�ڵ���������У�����");
                            continue;
                        }
                        FishingThePond(uid.ToString(), friend);
                        if (!_canfish)
                            break;
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
                        LogHelper.Write("GameFish.FishingPonds", GetFriendNameById(uid), ex, LogSeverity.Error);
                        continue;
                    }
                }

                if (Task.Fishing && Operation.FishingAll)
                {
                    //��������
                    num = 0;
                    SetMessageLn("��ʼȥ�������ѵ��������㣺");
                    foreach (FriendInfo friend in this._allFishFriendsList)
                    {
                        try
                        {
                            if (Operation.FishingWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                                continue;

                            SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                            if (Operation.FishingBlackList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "�ڵ���������У�����");
                                continue;
                            }
                            FishingThePond(friend.Id.ToString(), friend);
                            if (!_canfish)
                                break;
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
                            LogHelper.Write("GameFish.FishingPonds", friend.Name, ex, LogSeverity.Error);
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
                LogHelper.Write("GameFish.FishingPonds", ex, LogSeverity.Error);
            }
        }
        #endregion

        #region FishingThePond
        private void FishingThePond(string fuid, FriendInfo friend)
        {
            try
            {
                PondInfo pond = ReadPond(this._verifyCode, fuid, true);
                if (pond == null)
                {
                    SetMessage("�޷���ȡ������Ϣ��ʧ��");
                    return;
                }

                if (pond.Fishs.Count <= 0)
                {
                    SetMessageLn("��ǰ����û���㣬����");
                    return;
                }

                Collection<int> poses = GetValidPondPos(pond);
                if (poses.Count <= 0)
                {
                    SetMessageLn("û�п������λ������");
                    return;
                }

                string content = RequestMyTacklesList();
                Collection<FishTackleInfo> myTackcles = ConfigCtrl.GetMyTackles(content);

                int busecount = 0;
                int tacklecount = 0;
                foreach(FishTackleInfo fishtackle in myTackcles)
                {
                    if (fishtackle.Status == 0 || fishtackle.Status == 1)
                    {
                        tacklecount++;
                        if (fishtackle.BUse == 1)
                            busecount++;
                    }
                }
                if (busecount == tacklecount)
                {
                    _canfish = false;
                    return;
                }
                int ix = 0;
                for (ix = 0; ix < myTackcles.Count; ix++)
                {
                    if (myTackcles[ix].Status == 1 && myTackcles[ix].BUse != 1)
                    {
                        int pos = poses[0];
                        SetMessageLn(string.Format("=>��{0}����λ ��", pos));
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        content = HH.Post("http://www.kaixin001.com/!fish/!fish.php", string.Format("r=0%2E6440917486324906&verify={0}&fuid={1}&pos={2}&tackleid={3}", DataConvert.GetEncodeData(this._verifyCode), fuid, pos, myTackcles[ix].TackleId));
                        GetFishingThePondFeedback(content);
                        poses.Remove(pos);
                        if (poses.Count == 0)
                            break;
                        if (!_canfish)
                            return;
                    }
                }
                if (ix == tacklecount - 1)
                    _canfish = false;
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
                LogHelper.Write("GameFish.FishingThePond", ex, LogSeverity.Error);
                SetMessage(" ȥ" + GetFriendNameById(fuid) + "����������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetValidPondPos
        private Collection<int> GetValidPondPos(PondInfo pond)
        {
            if (pond.Fishers.Count == 4)
                return new Collection<int>();

            Collection<int> poses = new Collection<int>();
            poses.Add(1);
            poses.Add(2);
            poses.Add(3);
            poses.Add(4);
            foreach (FisherInfo fisher in pond.Fishers)
            { 
                poses.Remove(fisher.Pos);
            }

            return poses;
        }
        #endregion

        #region GetFishingThePondFeedback
        private bool GetFishingThePondFeedback(string content)
        {
            try
            {
                //<data>
                //  <anti>0</anti>
                //  <ret>fail</ret>
                //  <msg>�������������ʹ��</msg>
                //  <err>1</err>
                //  <sicktime>0000-00-00 00:00:00</sicktime>
                //  <recoverytime>0000-00-00 00:00:00</recoverytime>
                //  <sick>0</sick>
                //</data>

                //<data>
                //  <anti>0</anti>
                //  <ret>succ</ret>
                //  <msg>�¸˳ɹ���</msg>
                //  <err>0</err>
                //  <sicktime>0000-00-00 00:00:00</sicktime>
                //  <recoverytime>0000-00-00 00:00:00</recoverytime>
                //  <sick>0</sick>
                //  <fisher>
                //    <uid>6195212</uid>
                //    <pos>2</pos>
                //    <tackleid>77963666</tackleid>
                //    <tackleswf>http://img.kaixin001.com.cn/i3/fish/yugan/c2.swf</tackleswf>
                //    <flogo>
                //      <purl>http://pic.kaixin001.com/logo/19/52/120_6195212_1.jpg</purl>
                //      <lx>0</lx>
                //      <ly>0</ly>
                //      <scale>27.7777777778</scale>
                //      <body>http://img.kaixin001.com.cn/i3/fish/renwu/m1_1.swf</body>
                //      <skey>man1</skey>
                //    </flogo>
                //    <fstat>0</fstat>
                //    <fltime>28800</fltime>
                //    <ttitle>�����Ϲ�</ttitle>
                //    <fishid>0</fishid>
                //    <fishid2>3868237183</fishid2>
                //    <real_name>���Կ�</real_name>
                //    <bfish>0</bfish>
                //    <tools_luck>0</tools_luck>
                //    <bshark>0</bshark>
                //    <bship>
                //    </bship>
                //    <from>fish</from>
                //  </fisher>
                //  <words>
                //    <msg>��ӭ��</msg>
                //    <flag>0</flag>
                //  </words>
                //</data>

                if (String.IsNullOrEmpty(content))
                    return true;

                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    Regex regular = new Regex(@"<msg>[\s\S]+</msg>");
                    if (regular.IsMatch(content))
                    {
                        SetMessage(JsonHelper.FiltrateHtmlTags(regular.Match(content).ToString()));
                    }
                    return false;
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage("�¸˳ɹ���");
                    return true;
                }
                else
                {
                    LogHelper.Write(CurrentAccount.UserName, content, LogSeverity.Warn);
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
                LogHelper.Write("GameFish.GetMakeProductFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion
        
        #region NetSelfFish
        private void NetSelfFish(PondInfo pond)
        {
            try
            {
                SetMessageLn("��ʼ�Լ�����...");

                if (!pond.Netable)
                {
                    SetMessage(pond.NnetFishTips);
                    return;
                }

                int num = 0;
                int num2 = 0;

                if (Task.NetSelfFishCheap)
                {
                    for (int ix = 0; ix < pond.Fishs.Count; ix++)
                    {
                        if (NetTheFish(pond.Fishs[ix], ++num2))
                            num++;
                        if (num >= 2)
                            break;
                    }
                }
                else
                {
                    for (int ix = pond.Fishs.Count - 1; ix >= 0; ix--)
                    {
                        if (NetTheFish(pond.Fishs[ix], ++num2))
                            num++;
                        if (num >= 2)
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
                LogHelper.Write("GameFish.NetSelfFish", ex, LogSeverity.Error);
                SetMessage("�Լ�����ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region NetTheFish
        private bool NetTheFish(FishInfo fish, int num)
        {
            SetMessageLn(string.Format("#{0} {1}", num, fish.Tips));
            if (fish.Tips.Contains("ǧ�����"))
            {
                SetMessage("�� ����");
                return false;
            }
            double weightratio = DataConvert.GetDouble(Math.Round(fish.CurrentWeight / fish.MaxWeight, 3));
            double standardratio = DataConvert.GetDouble(Task.NetSelfFishMature) / 100;
            if (weightratio < standardratio)
            {
                weightratio = Math.Round(weightratio * 100, 0);
                SetMessage(string.Format("��ǰ����{0}/�������{1}={2}% < {3}%������", fish.CurrentWeight, fish.MaxWeight, weightratio, Task.NetSelfFishMature));
                return false;
            }
            else
            {
                string content = HH.Post("http://www.kaixin001.com/!fish/!catchfish.php", string.Format("verify={0}&fishid={1}&fuid=0&r=0%2E4802739913575351", DataConvert.GetEncodeData(this._verifyCode), fish.FishId));
                if (GetNetSelfFishFeedback(content))
                    return true;
                else
                    return false;
            }

        }
        #endregion

        #region GetNetSelfFishFeedback
        private bool GetNetSelfFishFeedback(string content)
        {            
            try
            {
                //<data><ret>succ</ret><err>0</err><stat>1</stat><fishid>6634824523</fishid><rankmemo>这是你钓到的最大的电鳐</rankmemo><msg>成功啦！&lt;br&gt;收获1条电鳐，�?53.5斤，已放入你的仓库里&lt;br&gt;&lt;br&gt;�?小时之内，还可以收鱼一�?/msg><nnetfishtips>�?小时之内，还可以收鱼一�?/nnetfishtips><bnetfish>1</bnetfish><accountfish><bbuytips>0</bbuytips><fish>18/19</fish><fishtips>养鱼数量�?8�?lt;br&gt;可容�?9�?/fishtips></accountfish></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                    //������㲻�ã����7Сʱ33����������
                    Regex regular = new Regex(@"������㲻�ã����[\s\S]+��������");
                    if (regular.IsMatch(content))
                    {
                        return true;
                    }
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                    return true;
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
                LogHelper.Write("GameCafe.GetNetSelfFishFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region HelpFish
        private void HelpFish()
        {
            try
            {
                SetMessageLn("��ʼȥ�Լ���������æ...");
                try
                {
                    SetMessageLn(string.Format("{0}", CurrentAccount.UserName) + "=>");

                    HelpTheFish(CurrentAccount.UserId.ToString());
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
                    LogHelper.Write("GameFish.HelpFish", CurrentAccount.UserName, ex, LogSeverity.Error);                   
                }

                SetMessageLn("��ʼȥ���ѵ�������æ...");

                int num = 0;
                //��ȥ�������е�������æ
                SetMessageLn("��ʼȥ�������к��ѵ�������æ��");
                foreach (int uid in Operation.HelpFishWhiteList)
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");

                        //FriendInfo friend = GetHelpFishFriendById(friends, uid);
                        //if (friend == null)
                        //{
                        //    SetMessage("ûʲô�ɰ�æ�ģ�����");
                        //    continue;
                        //}
                        if (Operation.HelpFishBlackList.Contains(uid))
                        {
                            SetMessage(base.GetFriendNameById(uid) + "�ڰ�æ����������У�����");
                            continue;
                        }
                        HelpTheFish(uid.ToString());
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
                        LogHelper.Write("GameFish.HelpFish", GetFriendNameById(uid), ex, LogSeverity.Error);
                        continue;
                    }
                }

                if (Task.HelpFish && Operation.HelpFishAll)
                {
                    //��������
                    num = 0;
                    SetMessageLn("��ʼȥ�������ѵ�������æ��");

                    string content = RequestHelpFriendList();
                    Collection<FriendInfo> friends = ConfigCtrl.GetHelpFriend(content);
                    if (friends == null || friends.Count == 0)
                    {
                        SetMessage("û����Ҫ��æ�ĺ���");
                        return;
                    }

                    foreach (FriendInfo friend in friends)
                    {
                        try
                        {
                            if (Operation.HelpFishWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                                continue;

                            SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                            if (Operation.HelpFishBlackList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "�ڰ�æ����������У�����");
                                continue;
                            }
                            HelpTheFish(friend.Id.ToString());
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
                            LogHelper.Write("GameFish.HelpFish", friend.Name, ex, LogSeverity.Error);
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
                LogHelper.Write("GameFish.HelpFish", ex, LogSeverity.Error);
            }
        }
        #endregion

        #region HelpTheFish
        private void HelpTheFish(string fuid)
        {
            try
            {
                PondInfo pond = ReadPond(this._verifyCode, fuid, true);

                foreach (FisherInfo fisher in pond.Fishers)
                {
                    if (fisher.FStat == 3)
                    {
                        SetMessageLn(string.Format("#{0} �ɰ�æ ", fisher.Pos));
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        string content = HH.Post("http://www.kaixin001.com/!fish/!helpfish.php", string.Format("r=0%2E9243684019893408&verify={0}&fuid={1}&pos={2}&touid={3}&tackleid={4}", DataConvert.GetEncodeData(this._verifyCode), fuid, fisher.Pos, fisher.UId, fisher.TackleId));
                        GetHelpFishFeedback(content);
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
                LogHelper.Write("GameFish.HelpTheFish", ex, LogSeverity.Error);
                SetMessage(" ȥ" + GetFriendNameById(fuid) + "��������æʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetHelpFishFeedback
        private bool GetHelpFishFeedback(string content)
        {
            try
            {
                //<data>
                //  <anti>0</anti>
                //  <oname>���</oname>
                //  <opic>http://img.kaixin001.com.cn/i/50_0_0.gif</opic>
                //  <hname>�����</hname>
                //  <hpic>http://img.kaixin001.com.cn/i/50_0_0.gif</hpic>
                //  <tackleid>78006554</tackleid>
                //  <pos>1</pos>
                //  <fishid>4107691870</fishid>
                //  <fishid2>4107691870</fishid2>
                //  <bhelpfish>1</bhelpfish>
                //  <err>0</err>
                //  <sicktime>0000-00-00 00:00:00</sicktime>
                //  <recoverytime>0000-00-00 00:00:00</recoverytime>
                //  <sick>0</sick>
                //  <land>1</land>
                //  <msg>��ϲ�㣡�����㽱�������ˣ�&lt;br&gt;�Ժ�Ҫ�����������˰�</msg>
                //  <fmsg>�����ص���1����3.9��Ĳ��㣬&lt;br&gt;������ý������������أ����ǹ黹������أ�������������������������.</fmsg>
                //  <fid>2</fid>
                //  <memo>&lt;b&gt;����&lt;/b&gt;&lt;br&gt;&lt;font color='#FF6600'&gt;&lt;b&gt;3.9��&lt;/b&gt;&lt;/font&gt;</memo>
                //  <pic>http://img.kaixin001.com.cn/i3/fish/fish/caoyu.swf</pic>
                //  <words>
                //    <msg>�����ǻ��׷�</msg>
                //    <pos>1</pos>
                //    <flag>1</flag>
                //  </words>
                //  <accountfish>
                //    <bbuytips>0</bbuytips>
                //    <fish>6/8</fish>
                //    <fishtips>����������6��&lt;br&gt;������8��</fishtips>
                //  </accountfish>
                //  <ret>succ</ret>
                //</data>
                SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));

                if (content.IndexOf("<ret>fail</ret>") > -1)
                    return false;
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                    return true;
                else
                {
                    LogHelper.Write(CurrentAccount.UserName, content, LogSeverity.Warn);
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
                LogHelper.Write("GameFish.GetHelpFishFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region PresentFish
        private void PresentFish()
        {
            try
            {
                SetMessageLn("��ʼ������...");
                if (Operation.PresentFishId == 0)
                {
                    SetMessage("û���趨���͵Ķ����޷�����");
                    return;
                }
                if (!IsAlreadyMyFriend(DataConvert.GetString(Operation.PresentFishId)))
                {
                    SetMessage(DataConvert.GetString(Operation.PresentFishId) + "������ĺ��ѣ��޷�����");
                    return;
                }

                string content = RequestMyWarehouse();
                Collection<FishMaturedInfo> fishes = ConfigCtrl.GetMyWarehouseFish(content);
                if (fishes == null || fishes.Count == 0)
                {
                    SetMessage("�ֿ���û���κ���");
                    return;
                }

                SetMessageLn("�ֿ�������͵��㣺");
                fishes = SortFishMaturedListByValue(fishes);

                int num = 0;
                if (Task.PresentFishCheap)
                {
                    for (int ix = 0; ix < fishes.Count; ix++)
                    {
                        if (PresentTheFish(fishes[ix], ref num))
                            break;
                    }
                }
                else
                {
                    for (int ix = fishes.Count - 1; ix >= 0; ix--)
                    {
                        if (PresentTheFish(fishes[ix], ref num))
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
                LogHelper.Write("GameFish.PresentAnimalProduct", GetFriendNameById(Operation.PresentProductId), ex, LogSeverity.Error);
                SetMessage(" ��" + GetFriendNameById(Operation.PresentId) + "����ũ����Ʒʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion
        
        #region PresentTheFish
        private bool PresentTheFish(FishMaturedInfo fish, ref int num)
        {
            if (fish.TNnum == 0)
                return false;

            SetMessageLn(string.Format("#{0} ", ++num));
            if (fish.Price == 0)
            {
                SetMessage(string.Format(" δ֪��:{0}({1},{2},{3})������", fish, fish.FId, fish.Weight, fish.MaxWeight));
                LogHelper.Write(CurrentAccount.UserName, string.Format(" δ֪��:{0}({1},{2},{3})", fish.Name, fish.FId, fish.Weight, fish.MaxWeight), LogSeverity.Warn);
                return false;
            }

            if (Task.PresentFishForbiddenList.Contains(fish.FId))
            {
                SetMessage(" �ڽ�ֹ�����б��У�����");
                return false;
            }

            decimal currentvalue = fish.MaxWeight * fish.Price;
            SetMessage(string.Format("{0} ��ǰ������{1} * �г��ۣ�{2} = {3}Ԫ", fish.Name, fish.MaxWeight, fish.Price, currentvalue));

            if (Task.PresentFishCheckValue)
            {
                if (currentvalue < DataConvert.GetDecimal(Task.PresentFishValue))
                {
                    SetMessage(string.Format(" < {0}Ԫ������", Task.PresentFishValue));
                    return false;
                }
            }

            //ȷ�������Ƿ���ȷ
            string content = HH.Post("http://www.kaixin001.com/!fish/!granaryinfo.php", string.Format("verify={0}&fid={1}&r=0%2E3951922911219299", DataConvert.GetEncodeData(this._verifyCode), fish.FId));
            Collection<FishMaturedInfo> detailFishes = ConfigCtrl.GetMyWarehouseDetailFish(content);
            FishMaturedInfo detailFish = detailFishes[detailFishes.Count - 1];            
            if (Task.PresentFishCheckValue)
            {
                if (currentvalue < DataConvert.GetDecimal(Task.PresentFishValue))
                {
                    currentvalue = detailFish.Weight * fish.Price;
                    SetMessage(string.Format("{0} ��ǰ������{1} * �г��ۣ�{2} = {3}Ԫ", fish.Name, detailFish.Weight, fish.Price, currentvalue));
                    SetMessage(string.Format(" < {0}Ԫ������", Task.PresentFishValue));
                    return false;
                }
            }

            //HH.DelayedTime = Constants.DELAY_1SECONDS;
            //string content = HH.Post("http://www.kaixin001.com/!fish/!granaryinfo.php", string.Format("verify={0}&fid={1}&r=0%2E3697885484434664", this._verifyCode, fishfry.FId));

            //string name = JsonHelper.GetMid(content, "<name>", "</name>");           
            SetMessage(string.Format(" ������{0}����1��{1}...", base.GetFriendNameById(Operation.PresentFishId), fish.Name));
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            content = HH.Post("http://www.kaixin001.com/!fish/!present.php", string.Format("verify={0}&anony=0&fid={1}&fuid={2}&pmsg={3}&r=0%2E15355860581621528", this._verifyCode, fish.FId, Operation.PresentFishId, DataConvert.GetEncodeData("����������")));
            GetPresentFishFeedback(content);
            return true;
        }
        #endregion

        #region GetPresentFishFeedback
        private void GetPresentFishFeedback(string content)
        {
            try
            {
                //<data><msg>�ѳɹ��͸�ׯ��-johnny1����8.1��ļ���</msg><ret>succ</ret><err>0</err></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + " ����ʧ�ܣ�");
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + " ���ͳɹ���");
                }
                else
                {
                    SetMessage(content);
                    LogHelper.Write("GameFish.GetPresentFishFeedback", content, LogSeverity.Info);
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
                LogHelper.Write("GameFish.GetPresentFishFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region SellFish
        private void SellFish(PondInfo pond)
        {
            try
            {
                SetMessageLn("��ʼ������...");
                if (Task.SellFishLowCash)
                {
                    if (pond.Cash > Task.SellFishLowCashLimit * 10000)
                    {
                        SetMessageLn(string.Format("����{0}Ԫ�ֽ�������ۡ�", pond.Cash));
                        return;
                    }
                }

                if (Task.SellAllFish)
                {
                    string content = HH.Post("http://www.kaixin001.com/!fish/!sellall.php", string.Format("fuid=0&r=0%2E575820348225534&verify={0}", DataConvert.GetEncodeData(this._verifyCode)));
                    long fishvalue = 0;
                    GetSellFishFeedback(content, ref fishvalue);
                }
                else
                {
                    string content = RequestMyWarehouse();
                    Collection<FishMaturedInfo> fishes = ConfigCtrl.GetMyWarehouseFish(content);
                    if (fishes == null || fishes.Count == 0)
                    {
                        SetMessage("�ֿ���û���κ���");
                        return;
                    }

                    //�����ֵ
                    long soldvalue = 0;
                    long fishvalue = 0;
                    StringBuilder sb;
                    fishes = RebuildFishMaturedList(fishes);
                    int num = 0;
                    foreach (FishMaturedInfo fish in fishes)
                    {
                        if (fish.TNnum == 0)
                            continue;
                        
                        fishvalue = 0;
                        if (soldvalue >= Task.SellFishMaxLimit * 10000)
                        {
                            SetMessageLn(string.Format("�ѳ��۵����ܼ�ֵ�Ѿ�����{0}��ֹͣ���ۡ�", Task.SellFishMaxLimit));
                            break;
                        }

                        SetMessageLn(string.Format("#{0} {1} ", ++num, fish.Name));
                        if (Task.SellFishForbiddenList.Contains(fish.FId))
                        {
                            SetMessage("�ڳ��۵Ľ�ֹ�б��У�����");
                            continue;
                        }

                        if (fish.Price <= 0)
                        {
                            SetMessage(string.Format("δ֪��{0}������", fish.FId));
                            continue;
                        }

                        content = HH.Post("http://www.kaixin001.com/!fish/!granaryinfo.php", string.Format("verify={0}&fid={1}&r=0%2E3951922911219299", DataConvert.GetEncodeData(this._verifyCode), fish.FId));
                        Collection<FishMaturedInfo> detailFishes = ConfigCtrl.GetMyWarehouseDetailFish(content);
                        sb = new StringBuilder();
                        foreach (FishMaturedInfo detailfish in detailFishes)
                        {
                            if (Task.SellFishCheckValue)
                            {
                                if (detailfish.Weight * fish.Price >= Task.SellFishValue)
                                {
                                    SetMessage(string.Format("{0} ��ǰ������{1} * �г��ۣ�{2} = {3} >= {4}������", fish.Name, detailfish.Weight, fish.Price, detailfish.Weight * fish.Price, Task.SellFishValue));
                                    continue;
                                }
                            }
                            sb.Append(";");
                            sb.Append(detailfish.Weight * 10);
                            sb.Append("-1");
                        }
                        if (String.IsNullOrEmpty(sb.ToString()))
                            continue;
                        //SetMessageLn("������" + sb.ToString());
                        content = HH.Post("http://www.kaixin001.com/!fish/!sellfish.php", string.Format("id={0}&verify={1}&odata={2}&fuid=0&r=0%2E5109372432343662", fish.FId, DataConvert.GetEncodeData(this._verifyCode), DataConvert.GetEncodeData(sb.ToString())));
                        //;200-1;34-1
                        //id=66&verify=2588258%5F1106%5F2588258%5F1267708811%5Fa0faefb9bbe15b24da6c02058e2c8993&odata=%3B200%2D1%3B34%2D1&fuid=0&r=0%2E5109372432343662
                        if (GetSellFishFeedback(content, ref fishvalue))
                        {
                            soldvalue += fishvalue;
                            SetMessage(string.Format(" �ѳ��۵����ܼ�ֵ��{0}Ԫ", soldvalue));
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
                LogHelper.Write("GameGarden.SellFruit", ex, LogSeverity.Error);
                SetMessage("���۹�ʵʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetSellFishFeedback
        private bool GetSellFishFeedback(string content, ref long totalprice)
        {
            try
            {
                //<data><cashRet><cash>82��</cash><cashtips>�ֽ�23515Ԫ</cashtips></cashRet><ret>succ</ret><err>0</err><msg>����ʯ������11700Ԫ</msg></data>
                //<data><cashRet><cash>2330</cash><cashtips>�ֽ�2330Ԫ</cashtips></cashRet><ret>succ</ret><msg>�������������18885Ԫ</msg><err>0</err></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + " ����ʧ�ܣ�");
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + " ���۳ɹ���");
                    totalprice = DataConvert.GetInt64(JsonHelper.GetMid(content, "����", "Ԫ"));
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
                LogHelper.Write("GameFish.GetSellFishFeedback", content, ex, LogSeverity.Error);
            }
            return false;
        }
        #endregion

        #region GetFishFryByRank
        private FishFryInfo GetFishFryByRank(Collection<FishFryInfo> fishfrys, PondInfo pond)
        {
            for (int ix = fishfrys.Count - 1; ix >= 0; ix--)
            {
                if (fishfrys[ix].Rank <= pond.Rank)
                {
                    if (fishfrys[ix].FId == 39 && (_iscontainfish39 || ContainsFish39(pond)))
                        return fishfrys[ix - 1];
                    else
                        return fishfrys[ix];
                }
            }
            return null;
        }
        #endregion

        #region ContainsFish39
        private bool ContainsFish39(PondInfo pond)
        {
            //����ֻ����1�������
            foreach (FishInfo fish in pond.Fishs)
            {
                if (fish.Tips.Contains("�����"))
                {
                    _iscontainfish39 = true;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region GetFishFriendById
        private FriendInfo GetFishFriendById(int uid)
        {
            foreach (FriendInfo friend in _allFishFriendsList)
            {
                if (friend.Id == uid)
                {
                    return friend;
                }
            }
            return null;
        }
        #endregion

        #region SortFishFrysList
        private Collection<FishFryInfo> SortFishFrysListByPrice(Collection<FishFryInfo> fishfrys)
        {
            for (int ix = 0; ix < fishfrys.Count; ix++)
            {
                for (int iy = ix + 1; iy < fishfrys.Count; iy++)
                {
                    if (fishfrys[ix].Price > fishfrys[iy].Price)
                    {
                        FishFryInfo temp = fishfrys[ix];
                        fishfrys[ix] = fishfrys[iy];
                        fishfrys[iy] = temp;
                    }
                }
            }

            return fishfrys;
        }

        private Collection<FishFryInfo> SortFishFrysListByRank(Collection<FishFryInfo> fishfrys)
        {
            for (int ix = 0; ix < fishfrys.Count; ix++)
            {
                for (int iy = ix + 1; iy < fishfrys.Count; iy++)
                {
                    if (fishfrys[ix].Rank > fishfrys[iy].Rank)
                    {
                        FishFryInfo temp = fishfrys[ix];
                        fishfrys[ix] = fishfrys[iy];
                        fishfrys[iy] = temp;
                    }
                }
            }

            return fishfrys;
        }
        #endregion

        #region SortFishTacklesList
        private Collection<FishTackleInfo> SortFishTacklesListByPrice(Collection<FishTackleInfo> fishtackles)
        {
            for (int ix = 0; ix < fishtackles.Count; ix++)
            {
                for (int iy = ix + 1; iy < fishtackles.Count; iy++)
                {
                    if (fishtackles[ix].Price > fishtackles[iy].Price)
                    {
                        FishTackleInfo temp = fishtackles[ix];
                        fishtackles[ix] = fishtackles[iy];
                        fishtackles[iy] = temp;
                    }
                }
            }

            return fishtackles;
        }

        private Collection<FishTackleInfo> SortFishTacklesListByRank(Collection<FishTackleInfo> fishtackles)
        {
            for (int ix = 0; ix < fishtackles.Count; ix++)
            {
                for (int iy = ix + 1; iy < fishtackles.Count; iy++)
                {
                    if (fishtackles[ix].Rank > fishtackles[iy].Rank)
                    {
                        FishTackleInfo temp = fishtackles[ix];
                        fishtackles[ix] = fishtackles[iy];
                        fishtackles[iy] = temp;
                    }
                }
            }

            return fishtackles;
        }
        #endregion

        #region GetFishTackleByRank
        private FishTackleInfo GetFishTackleByRank(Collection<FishTackleInfo> fishtackles, int rank)
        {
            for (int ix = fishtackles.Count - 1; ix >= 0; ix--)
            {
                if (fishtackles[ix].Rank <= rank)
                {
                    return fishtackles[ix];
                }
            }
            return null;
        }
        #endregion

        #region GetHelpFishFriendById
        private FriendInfo GetHelpFishFriendById(Collection<FriendInfo> helpfriends, int uid)
        {
            foreach (FriendInfo friend in helpfriends)
            {
                if (friend.Id == uid)
                {
                    return friend;
                }
            }
            return null;
        }
        #endregion

        #region GetFishNameById
        private string GetFishNameById(int fishid)
        {
            foreach (FishFryInfo fishfry in this._fishFrysList)
            {
                if (fishfry.FId == fishid)
                {
                    return fishfry.Name;
                }
            }
            return fishid.ToString();
        }
        #endregion

        #region GetFishFryById
        private FishFryInfo GetFishFryById(int fishid)
        {
            foreach (FishFryInfo fishfry in this._fishFrysList)
            {
                if (fishfry.FId == fishid)
                {
                    return fishfry;
                }
            }
            return null;
        }
        #endregion

        #region SortFishMaturedListByValue
        private Collection<FishMaturedInfo> SortFishMaturedListByValue(Collection<FishMaturedInfo> fishmatureds)
        {
            //�����г��۸�
            fishmatureds = RebuildFishMaturedList(fishmatureds);

            for (int ix = 0; ix < fishmatureds.Count; ix++)
            {
                for (int iy = ix + 1; iy < fishmatureds.Count; iy++)
                {
                    if (fishmatureds[ix].Price * fishmatureds[ix].Weight > fishmatureds[iy].Price * fishmatureds[iy].Weight)
                    {
                        FishMaturedInfo temp = fishmatureds[ix];
                        fishmatureds[ix] = fishmatureds[iy];
                        fishmatureds[iy] = temp;
                    }
                }
            }

            return fishmatureds;
        }
        #endregion

        #region RebuildFishMaturedList
        private Collection<FishMaturedInfo> RebuildFishMaturedList(Collection<FishMaturedInfo> fishmatureds)
        {
            FishMaturedInfo temp;
            foreach (FishMaturedInfo fishmatured in fishmatureds)
            {
                temp = GetFishMaturedById(fishmatured.FId);
                if (temp == null)
                {
                    fishmatured.Price = 0;
                }
                else
                {
                    fishmatured.Price = temp.Price;
                    fishmatured.Name = temp.Name;
                    if (fishmatured.Weight == 0)
                        fishmatured.Weight = temp.Weight;
                }
            }
            return fishmatureds;
        }
        #endregion

        #region GetFishMaturedById
        private FishMaturedInfo GetFishMaturedById(int fishid)
        {
            foreach (FishMaturedInfo fishmatured in this._fishMaturedList)
            {
                if (fishmatured.FId == fishid)
                {
                    return fishmatured;
                }
            }
            return null;
        }
        #endregion

        #region GetFishPriceById
        private decimal GetFishPriceById(int fishid)
        {
            foreach (FishMaturedInfo fishmatured in this._fishMaturedList)
            {
                if (fishmatured.FId == fishid)
                {
                    return fishmatured.Price;
                }
            }
            return 0;
        }
        #endregion

        #region GetMyTackleCount
        private int GetMyTackleCount(Collection<FishTackleInfo> tackles)
        {
            int count = 0;
            foreach (FishTackleInfo tackle in tackles)
            {
                if (tackle.Status == 1)
                    count++;
            }
            return count;
        }
        #endregion

        #region Request

        public string RequestFishHomePage(bool IsInitial)
        {
            string content = HH.Get("http://www.kaixin001.com/!fish/index.php");
            if (content.IndexOf("<title>������ - ������</title>") != -1)
            {
                SetMessageLn("��δ��װ�������,���԰�װ��...");
                HH.Post("http://www.kaixin001.com/app/install.php", "aid=1106&isinstall=1");
                content = HH.Get("http://www.kaixin001.com/!fish/index.php");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            return content;
        }

        public string RequestAllFishFriends()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Post("http://www.kaixin001.com/!fish/!getfriendstat.php", string.Format("verify={0}&fuid=0&help=0&r=0%2E8526007486507297", this._verifyCode));
        }

        public string RequestFishConf(string verifyCode, string fuid)
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Post("http://www.kaixin001.com/!fish/!getconf.php",string.Format("verify={0}&fuid={1}&r=0%2E04362671356648207", verifyCode, fuid));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, CurrentAccount.UserName + "FishConf");
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
        
        public string RequestFishFrysList()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Post("http://www.kaixin001.com/!fish/!fishlist.php", string.Format("verify={0}&r=0%2E03616209840402007", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "FishFrysList");
            //}
            return content;
        }

        public string RequestFishTacklesList()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Post("http://www.kaixin001.com/!fish/!fishtacklelist.php", string.Format("verify={0}&r=0%2E03616209840402007", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "RequestFishTackleList");
            //}
            return content;
        }

        public string RequestMyTacklesList()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Post("http://www.kaixin001.com/!fish/!mytackle.php", string.Format("verify={0}&r=0%2E5276619186624885", DataConvert.GetEncodeData(this._verifyCode)));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "MyTackles");
            //}
            return content;
        }

        public string RequestHelpFriendList()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Post("http://www.kaixin001.com/!fish/!gethelpfriend.php", string.Format("verify={0}&r=0%2E5658171777613461", DataConvert.GetEncodeData(this._verifyCode)));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, "HelpFriends");
            //}
            return content;
        }

        public string RequestMyWarehouse()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Post("http://www.kaixin001.com/!fish/!granary.php", string.Format("verify={0}&r=0%2E31028140103444457", this._verifyCode));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, CurrentAccount.UserName + "RequestMyWarehouseFish");
            //}
            return content;
        }

        #endregion

        #region Properties
        public Collection<FriendInfo> AllFishFriendsList
        {
            get { return this._allFishFriendsList; }
        }

        public Collection<FishFryInfo> FishFrysList
        {
            get { return _fishFrysList; }
            set { _fishFrysList = value; }
        }

        public Collection<FishTackleInfo> FishTacklesList
        {
            get { return _fishTacklesList; }
            set { _fishTacklesList = value; }
        }

        public Collection<FishMaturedInfo> FishMaturedList
        {
            get { return _fishMaturedList; }
            set { _fishMaturedList = value; }
        }
        #endregion
    }
}
