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
    public class GameCafe : KaixinBase
    {
        private const int MAX_ANIMAL_COUNT = 12;

        private Collection<FriendInfo> _allCafeFriendsList;  //���п��Ĳ����ĺ���
        private Collection<FriendInfo> _hirableFriendsList;  //�ɹ�Ӷ�ĵĺ���    
        private Collection<DishInfo> _dishesList; //�����еĲ���
        private Collection<DishInfo> _transactiondDishesList; //���Ƚ��׼۸��        
        private bool _purchaseblocked;
        private bool _todaypurchasedlimit;
        private bool _tomatoexchangable;
        private bool _medlarexchangable;
        private bool _crabexchangable;
        private bool _pineappleexchangable;
        private string _flashid;

        public delegate void AllCafeFriendsFetchedEventHandler(Collection<FriendInfo> allcafefriends);
        public event AllCafeFriendsFetchedEventHandler AllCafeFriendsFetched;

        public delegate void HirableFriendsFetchedEventHandler(Collection<FriendInfo> hirablefriends);
        public event HirableFriendsFetchedEventHandler HirableFriendsFetched;

        public delegate void DishesInMenuFetchedEventHandler(Collection<DishInfo> dishes);
        public event DishesInMenuFetchedEventHandler DishesFetched;

        public delegate void TransactionDishesFetchedEventHandler(Collection<DishInfo> transactiondishes);
        public event TransactionDishesFetchedEventHandler TransactionDishesFetched;

        public GameCafe()
        {
            this._purchaseblocked = false;
            this._todaypurchasedlimit = false;
            this._tomatoexchangable = true;
            this._medlarexchangable = true;
            this._crabexchangable = true;
            this._pineappleexchangable = true;
            this._allCafeFriendsList = new Collection<FriendInfo>();
            this._hirableFriendsList = new Collection<FriendInfo>();
            this._dishesList = new Collection<DishInfo>();
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //cafe
                SetMessageLn("���ڳ�ʼ��[���Ĳ���]...");

                string content = RequestCafeHomePage(true);
                CafeInfo cafe = ReadCafe(this._verifyCode, CurrentAccount.UserId, false);
                //all cafe friends
                content = RequestAllCafeFriends();
                ReadAllCafeFriends(content, false);
                SetMessage("[���п��Ĳ����ĺ���]��Ϣ���سɹ���");

                ReadHirableFriends(cafe, false);
                SetMessage("[�ɹ�Ӷ�ĺ���]��Ϣ���سɹ���");

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
                LogHelper.Write("GameCafe.Initialize", ex, LogSeverity.Error);
                SetMessage(" ��ʼ��[���Ĳ���]ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetAllCafeFriends
        public void GetAllCafeFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAllCafeFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }

        private void GetAllCafeFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_CAFE;
                SetMessageLn("ˢ��[���п��Ĳ����ĺ���]...");

                if (!this.ValidationLogin(true))
                {
                    if (AllCafeFriendsFetched != null)
                        AllCafeFriendsFetched(_allCafeFriendsList);
                    return;
                }

                string content = RequestCafeHomePage(false);
                content = RequestAllCafeFriends();
                ReadAllCafeFriends(content, true);
                SetMessageLn("[���п��Ĳ����ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (AllCafeFriendsFetched != null)
                    AllCafeFriendsFetched(_allCafeFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[���п��Ĳ����ĺ���]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region ReadAllCafeFriends
        public void ReadAllCafeFriends(string content, bool printMessage)
        {
            this._allCafeFriendsList.Clear();
            this._allCafeFriendsList = ConfigCtrl.GetCafeFriends(content);
            if (printMessage)
            {
                foreach (FriendInfo friend in this._allCafeFriendsList)
                {
                    SetMessageLn(friend.Name + "(" + friend.Id.ToString() + ")");
                }
            }
            if (printMessage)
                SetMessageLn("��ȡ[���п��Ĳ����ĺ���]��Ϣ:");
            
            if (printMessage)
                SetMessageLn("��ɶ�ȡ��");
        }
        #endregion

        #region GetHirableFriends
        public void GetHirableFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetHirableFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetHirableFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_CAFE;
                SetMessageLn("ˢ��[���Թ�Ӷ�ĺ����б�]...");

                if (!this.ValidationLogin())
                {
                    if (HirableFriendsFetched != null)
                        HirableFriendsFetched(null);
                    return;
                }

                string content = RequestCafeHomePage(false);
                CafeInfo cafe = ReadCafe(this._verifyCode, CurrentAccount.UserId, false);
                ReadHirableFriends(cafe, true);

                if (this._hirableFriendsList == null || this._hirableFriendsList.Count == 0)
                    SetMessageLn("[���Թ�Ӷ�ĺ����б�]��Ϣˢ��ʧ�ܣ�");
                else
                    SetMessageLn("[���Թ�Ӷ�ĺ����б�]��Ϣˢ�³ɹ���");

                //invoke event
                if (HirableFriendsFetched != null)
                    HirableFriendsFetched(this._hirableFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[���Թ�Ӷ�ĺ����б�]��Ϣˢ��ʧ�ܣ�");
        }

        #region ReadHirableFriends
        public void ReadHirableFriends(CafeInfo cafe, bool printMessage)
        {
            if (printMessage)
                SetMessage("��ȡ���Թ�Ӷ�ĺ�����Ϣ...");
            this._hirableFriendsList.Clear();
            string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_notemployees.php?verify={0}&cafeid={1}&start=0&text=&rand=0.7530548870563507", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId));
            this._hirableFriendsList = ConfigCtrl.GetEmptyEmployees(content);
            if (this._hirableFriendsList == null || this._hirableFriendsList.Count == 0)
                return;
            int totalcount = JsonHelper.GetMidInteger(content, "<data><total>", "</total>");
            int offset = JsonHelper.GetMidInteger(content, "</start><num>", "</num>");
            int page = offset;
            do
            {
                HH.DelayedTime = Constants.DELAY_1SECONDS;
                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_notemployees.php?verify={0}&cafeid={1}&start={2}&text=&rand=0.7530548870563507", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, page));
                Collection<FriendInfo> employees = new Collection<FriendInfo>();
                employees = ConfigCtrl.GetEmptyEmployees(content);
                if (employees == null || employees.Count == 0)
                    break;
                else
                {
                    foreach (FriendInfo item in employees)
                    {
                        FriendInfo employee = new FriendInfo();
                        employee.Clone(item);
                        this._hirableFriendsList.Add(employee);
                    }
                    page += offset;
                }
            }
            while (true);
            this._hirableFriendsList = SortEmployeeByPower(this._hirableFriendsList);
            if (printMessage)
            {
                foreach (FriendInfo friend in this._hirableFriendsList)
                {
                    SetMessageLn(friend.Name + "(" + friend.Power.ToString() + ")");
                }
            }

            if (printMessage)
                SetMessageLn("��ɶ�ȡ��");
        }
        #endregion


        #endregion
        
        #region GetDishInMenu
        public void GetDishInMenuByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetDishInMenu));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetDishInMenu()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_UPDATEDATA;
                SetMessageLn("ˢ��[�����в����б�]...");

                if (!this.ValidationLogin())
                {
                    if (DishesFetched != null)
                        DishesFetched(null);
                    return;
                }

                string content = RequestCafeHomePage(false);
                Collection<DishInfo> dishes = new Collection<DishInfo>();
                int page = 0;
                do
                {
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dishlist.php?verify={0}&page={1}&r=0.11418269015848637", DataConvert.GetEncodeData(this._verifyCode), page));
                    Collection<DishInfo> dishes2 = new Collection<DishInfo>();
                    dishes2 = ConfigCtrl.GetOriginalDishes(content);
                    if (dishes2 == null || dishes2.Count == 0)
                        break;
                    else
                    {
                        foreach (DishInfo item in dishes2)
                        {
                            DishInfo dish = new DishInfo();
                            dish.Clone(item);
                            dishes.Add(dish);
                        }
                        page += 1;
                    }
                }
                while (true);

                if (dishes == null || dishes.Count == 0)
                    SetMessageLn("[�����в����б�]��Ϣˢ��ʧ�ܣ�");
                else
                    SetMessageLn("[�����в����б�]��Ϣˢ�³ɹ���");

                //invoke event
                if (DishesFetched != null)
                    DishesFetched(dishes);
            });
            base.ExecuteTryCatchBlock(th, "[�����в����б�]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region GetTransactionDishesInMarket
        public void GetTransactionDishesInMarketByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetTransactionDishesInMarket));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetTransactionDishesInMarket()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_UPDATEDATA;
                SetMessageLn("ˢ��[���Ƚ��׼۸��]...");

                if (!this.ValidationLogin())
                {
                    if (TransactionDishesFetched != null)
                        TransactionDishesFetched(null);
                    return;
                }

                string content = RequestCafeHomePage(false);
                CafeInfo cafe = ReadCafe(this._verifyCode, CurrentAccount.UserId, false);
                if (cafe == null)
                {
                    return;
                }
                Collection<DishInfo> dishes = ConfigCtrl.GetDishesInMenu();
                int num = 0;
                foreach (DishInfo dish in dishes)
                {
                    SetMessageLn(string.Format("#{0} {1} ", ++num, dish.Title));
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    //http://www.kaixin001.com/cafe/api_tradeinfo.php?verify=2588258_1136_2588258_1266372458_f84ac5d57a67249f7b4068ead5dc8dfc&cafeid=185596&uid=2588258&dishid=22&type=21&r=0.3238336769863963
                    //http://www.kaixin001.com/cafe/api_tradeinfo.php?verify=2588258_1136_2588258_1267272898_fd382ccf4b137ed2ae8f6e20f3d12811&cafeid=185596&uid=2588258&dishid=18&type=21&r=0.35850675450637937
                    content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_tradeinfo.php?verify={0}&cafeid={1}&uid={2}&dishid={3}&type=21&r=0.3238336769863963", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, CurrentAccount.UserId, dish.DishId));
                    dish.MaxPrice = DataConvert.GetDecimal(JsonHelper.GetMid(content, "<maxprice>", "</maxprice>"));
                    dish.MinPrice = DataConvert.GetDecimal(JsonHelper.GetMid(content, "<minprice>", "</minprice>"));
                    SetMessage(string.Format("��߼ۣ�{0}����ͼۣ�{1}", dish.MaxPrice, dish.MinPrice));                    
                }

                //invoke event
                if (TransactionDishesFetched != null)
                    TransactionDishesFetched(dishes);
            });
            base.ExecuteTryCatchBlock(th, "[���Ƚ��׼۸��]��Ϣˢ��ʧ�ܣ�");
        }

        #endregion

        #region RunCafe
        public void RunCafe()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_CAFE;

                SetMessageLn("��ʼ���Ĳ���...");

                //cafe
                string contentHome = RequestCafeHomePage(false);

                contentHome = RequestAllCafeFriends();
                ReadAllCafeFriends(contentHome, false);

                CafeInfo cafe = ReadCafe(this._verifyCode, CurrentAccount.UserId, true);
                if (cafe == null)
                {
                    SetMessageLn("�޷���ȡ�ҵĲ�����Ϣ��");
                    return;
                }

                //��æ����
                if (Task.HelpFriend)
                    HelpFriend();

                //SetMessageLn("afe.Grade=" + cafe.Grade);
                //if (cafe.Grade <= 20)
                //{
                //    Majia(cafe);
                //    cafe = ReadCafe(this._verifyCode, CurrentAccount.UserId, false);
                //    if (cafe == null)
                //    {
                //        SetMessageLn("�޷���ȡ�ҵĲ�����Ϣ��");
                //        return;
                //    }
                //}

                //����
                if (Task.BoxClean || Task.Cook)
                    BoxCleanCook(CurrentAccount.UserId, cafe);

                //��Ա
                if (Task.Hire)
                    Hire(cafe);

                //����ʳ��
                if (Task.PresentFood)
                    PresentFood(CurrentAccount.UserId, cafe);

                //�չ�ʳ��
                if (Task.PurchaseFood)
                    PurchaseFood(cafe);

                //����ʳ��
                if (Task.SellFood)
                    SellFood(CurrentAccount.UserId, cafe);

                SetMessageLn("���Ĳ�����ɣ�");

            });
            base.ExecuteTryCatchBlock(th, "�����쳣�����Ĳ���ʧ�ܣ�");
        }
        #endregion

        #region ReadCafe
        private CafeInfo ReadCafe(string verifyCode, string uid, bool printmessage)
        {
            CafeInfo cafe = null;
            string strCafe = RequestCafeConf(verifyCode, uid);
            cafe = ConfigCtrl.GetCafe(strCafe);
            try
            { 
                if (cafe.Chef == true)
                {
                    string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_autochef.php?verify={0}&act=0&cafeid={1}&viewuid={2}&rand=0.7089885603636503", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, uid));
                    //<?xml version="1.0" encoding="UTF-8" ?>
                    //<data><orderid>2321286397</orderid><version>normal</version><leavetime>0</leavetime><uid>2588258</uid><status>0</status><cheftype>0</cheftype><mana>772</mana><mtime>1274878792</mtime><ctime>1269664834</ctime><restoresec>3542</restoresec></data><!-- module end -->
                    cafe.ChefMana = JsonHelper.GetMidInteger(content, "<mana>", "</mana>");
                }
            }
            catch(Exception ex)
            {
                LogHelper.Write("GameCafe.ReadCafe->Get chef info", ex, LogSeverity.Error);                
                cafe.Chef = false;
                cafe.ChefMana = 0;
            }

            if (cafe == null)
            {
                if (printmessage)
                    SetMessageLn("��ȡ������Ϣʧ�ܣ�");
            }
            else
            {
                if (!printmessage)
                    return cafe;

                SetMessageLn(string.Format("{0}���ֽ�{1} ��ң�{2}", cafe.CafeName, cafe.Cash, cafe.GoldNum));
                if (cafe.Chef)
                    SetMessageLn(string.Format("������{0} �ȼ���{1}({2}) ����ֵ��{3} ���������{4}�㷨��", cafe.Name, cafe.Grade, cafe.GradeLabel, cafe.Evalue, cafe.ChefMana));
                else
                    SetMessageLn(string.Format("������{0} �ȼ���{1}({2}) ����ֵ��{3}", cafe.Name, cafe.Grade, cafe.GradeLabel, cafe.Evalue));

                if (cafe.Cookings != null)
                {
                    int num = 0;
                    foreach (CookingInfo cooking in cafe.Cookings)
                    {
                        if (cooking.FoodNum != 0)
                        {
                            SetMessageLn(string.Format("��{0}��¯�ӣ�", ++num));
                            SetMessage(string.Format(" {0}", cooking.Name));
                        }
                        //else
                        //{
                        //    SetMessageLn(string.Format("��{0}��¯�ӣ���", ++num));
                        //}
                        //SetMessage(" Stage:" + cooking.Stage.ToString());
                        //if (cooking.Stage != -1)
                        //{
                        //    SetMessage("DishId:" + cooking.DishId.ToString());
                        //    SetMessage(" Name:" + cooking.Name.ToString());
                        //    SetMessage(" FoodNum:" + cooking.FoodNum.ToString());                            
                        //    SetMessage(" Step:" + cooking.Step.ToString());
                        //    SetMessage(" Resver:" + cooking.Resver.ToString());
                        //}
                    }
                }

                if (cafe.Employees != null)
                {
                    int num = 0;
                    foreach (FriendInfo employee in cafe.Employees)
                    {
                        SetMessageLn(string.Format("��{0}����Ա��{1}", ++num, employee.Name));
                    }
                }

                if (cafe.DinnerTables != null)
                {
                    int num = 0;
                    foreach (DinnerTableInfo dinnertable in cafe.DinnerTables)
                    {
                        SetMessageLn(string.Format("��{0}����̨��", ++num));
                        SetMessage(string.Format(" {0} ʣ�ࣺ{1}", dinnertable.Name, dinnertable.Num));
                        //SetMessage("OrderId:" + dish.OrderId.ToString());
                        //if (!string.IsNullOrEmpty(dish.Name))
                        //{
                        //    SetMessage(" Name:" + dish.Name.ToString());
                        //    SetMessage(" FoodNum:" + dish.FoodNum.ToString());
                        //    SetMessage(" DishId:" + dish.DishId.ToString());
                        //    SetMessage(" Num:" + dish.Num.ToString());
                        //    SetMessage(" Resver:" + dish.Resver.ToString());
                        //}
                    }
                }
            }

            return cafe;
        }
        #endregion

        #region Majia
        private void Majia(CafeInfo cafe)
        {
            try
            {
                SetMessageLn("��ʼ���...");
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>succ</ret><err>0</err><x>3</x><y>10</y><direct>1</direct><ui>decor.stove0</ui><directional>0</directional><tkey>stove</tkey><account><cash>82161</cash></account><addcash>-600</addcash><orderid>851729733</orderid><msg>购买物品成功</msg></data>

                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_buygoods.php?verify={0}&cafeid={1}&goodsid=256&x=3&y=10&direct=1&r=0.5628602504730225", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId));
                SetMessageLn(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                //SetMessageLn(content);

                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_buygoods.php?verify={0}&cafeid={1}&goodsid=256&x=5&y=10&direct=1&r=0.5628602504730225", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId));
                SetMessageLn(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                //SetMessageLn(content);

                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_buygoods.php?verify={0}&cafeid={1}&goodsid=256&x=7&y=10&direct=1&r=0.5628602504730225", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId));
                SetMessageLn(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                //SetMessageLn(content);

                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_buygoods.php?verify={0}&cafeid={1}&goodsid=256&x=2&y=9&direct=1&r=0.5628602504730225", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId));
                SetMessageLn(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                //SetMessageLn(content);
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
                LogHelper.Write("GameCafe.Majia", ex, LogSeverity.Error);
                SetMessage("���ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region HelpFriend
        private void HelpFriend()
        {
            try
            {
                SetMessageLn("��ʼ��æ����...");

                bool bhelped = false;
                string content = "";
                int num = 0;
                foreach (FriendInfo friend in _allCafeFriendsList)
                {
                    if (friend.Help)
                    {
                        SetMessageLn(string.Format("#{0} {1}", ++num, friend.Name));                        
                        //http://www.kaixin001.com/cafe/api_tastedish.php?verify=2588258_1136_2588258_1267109077_ce91b071ad268b0257a57e9e8ebefd62&uid=3243790
                        //<?xml version="1.0" encoding="UTF-8" ?>
                        //<data><avatar><item><type>1</type><ui>avatar.face1</ui><avatarid>1</avatarid><val></val></item><item><type>2</type><ui>avatar.eye1</ui><avatarid>10</avatarid><val></val></item><item><type>3</type><ui>avatar.nose1</ui><avatarid>11</avatarid><val></val></item><item><type>4</type><ui>avatar.mouth1</ui><avatarid>12</avatarid><val></val></item><item><type>5</type><ui>avatar.brow1</ui><avatarid>13</avatarid><val></val></item><item><type>6</type><ui>avatar.hair0</ui><avatarid>14</avatarid><val></val></item><item><type>7</type><ui>avatar.</ui><avatarid>20</avatarid><val>fcf5e0</val></item><item><type>8</type><ui></ui><avatarid>0</avatarid><val></val></item><item><type>9</type><ui></ui><avatarid>0</avatarid><val></val></item><item><type>10</type><ui>avatar.</ui><avatarid>38</avatarid><val>cc0000</val></item></avatar><cloth><item><type>1</type><ui>avatar.coat25</ui><clothid>6</clothid></item><item><type>2</type><ui>avatar.trousers1</ui><clothid>3</clothid></item><item><type>3</type><ui>avatar.shoe42</ui><clothid>4</clothid></item></cloth><ret>succ</ret><err>0</err><ui>cook.gbjd</ui><name>��������</name><logo>http://img.kaixin001.com.cn/i/50_0_0.gif</logo><title>������ѧ��һ���²ˣ��������ҳ���ζ��������ô����</title><fee>��С�Ѹ����һԣ�Ǯ��ϵͳ֧����</fee></data>

                        //http://www.kaixin001.com/cafe/api_dotastedish.php?verify=2588258_1136_2588258_1267109077_ce91b071ad268b0257a57e9e8ebefd62&uid=3243790&pay=1
                        //<?xml version="1.0" encoding="UTF-8" ?>
                        //<data><ret>succ</ret><err>0</err><account><cash>10146</cash></account><addcash>1</addcash><addmycash>30</addmycash><addmyevalue>10</addmyevalue><avatar><item><type>1</type><ui>avatar.face1</ui><avatarid>1</avatarid><val></val></item><item><type>2</type><ui>avatar.eye1</ui><avatarid>10</avatarid><val></val></item><item><type>3</type><ui>avatar.nose1</ui><avatarid>11</avatarid><val></val></item><item><type>4</type><ui>avatar.mouth1</ui><avatarid>12</avatarid><val></val></item><item><type>5</type><ui>avatar.brow1</ui><avatarid>13</avatarid><val></val></item><item><type>6</type><ui>avatar.hair0</ui><avatarid>14</avatarid><val></val></item><item><type>7</type><ui>avatar.</ui><avatarid>20</avatarid><val>fcf5e0</val></item><item><type>8</type><ui></ui><avatarid>0</avatarid><val></val></item><item><type>9</type><ui></ui><avatarid>0</avatarid><val></val></item><item><type>10</type><ui>avatar.</ui><avatarid>38</avatarid><val>cc0000</val></item></avatar><cloth><item><type>1</type><ui>avatar.coat25</ui><clothid>6</clothid></item><item><type>2</type><ui>avatar.trousers1</ui><clothid>3</clothid></item><item><type>3</type><ui>avatar.shoe42</ui><clothid>4</clothid></item></cloth><msg>лл���ҵĲ���Ʒ���²ˣ�&lt;br&gt;�����͸�����Ϊ������&lt;br&gt;����ֵ��+30&lt;br&gt;�ֽ�+10</msg></data>

                        content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_tastedish.php?verify={0}&uid={1}", DataConvert.GetEncodeData(this._verifyCode), friend.Id));
                        SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<title>", "</title>")));
                        content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dotastedish.php?verify={0}&uid={1}&pay=1", DataConvert.GetEncodeData(this._verifyCode), friend.Id));
                        SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                        
                        //http://www.kaixin001.com/cafe/api_userevent.php?verify=2588258_1136_2588258_1267109077_ce91b071ad268b0257a57e9e8ebefd62&uid=6209137&r=0.7955835554748774
                        //<?xml version="1.0" encoding="UTF-8" ?>
                        //<data><ret>succ</ret><err>0</err><evtid>26</evtid><ekey>evt26</ekey><title>�����ҼҲ���ˮ�ܻ��ˣ��ò���ˮ������������һ�°ɣ�</title><cash>2000</cash><status>1</status><goodnews>{_OPUSER_}����{_CAFEUSER_}�Ҳ�����æ����ˮ�ܣ�{_CAFEUSER_}�ܿ�����������ˮ�ˣ�|������{_CAFEUSER_}�Ҳ�����æ����ˮ�ܣ�{_CAFEUSER_}�ܿ�����������ˮ�ˣ�</goodnews><badnews>{_OPUSER_}����{_CAFEUSER_}�Ҳ�����ˮ���ҿ��ˣ�����{_CAFETA_}�Ĳ�����ˮ���ˡ�|������{_CAFEUSER_}�Ҳ�����ˮ���ҿ��ˣ�����{_CAFETA_}�Ĳ�����ˮ���ˡ�</badnews><type>2</type><pic>http://img.kaixin001.com.cn/i3/cafe/random/2daomei.png</pic><logo>http://img.kaixin001.com.cn/i/50_0_0.gif</logo></data>

                        //http://www.kaixin001.com/cafe/api_doevent.php?verify=2588258_1136_2588258_1267109077_ce91b071ad268b0257a57e9e8ebefd62&uid=6209137&ret=1
                        //<?xml version="1.0" encoding="UTF-8" ?>
                        //<data><ret>succ</ret><err>0</err><msg>���������һԼҲ�����æ����ˮ�ܣ����һԺܿ�����������ˮ�ˣ�&lt;br&gt;�����͸�����Ϊ������</msg><addmycash>32</addmycash><addmyevalue>13</addmyevalue></data>

                        //content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_userevent.php?verify={0}&uid={1}&r=0.7955835554748774", DataConvert.GetEncodeData(this._verifyCode), friend.Id));
                        //SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<title>", "</title>")));
                        //content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_doevent.php?verify={0}&uid={1}&ret=1", DataConvert.GetEncodeData(this._verifyCode), friend.Id));
                        //SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + " �ֽ�+" + JsonHelper.GetMid(content, "<addmycash>", "</addmycash>") + ";����ֵ+" + JsonHelper.GetMid(content, "<addmyevalue>", "</addmyevalue>"));
                        bhelped = true;
                    }
                }

                if (bhelped == false)
                    SetMessage("û����Ҫ��æ�ĺ���");

                if (bhelped == true)
                {
                    CafeInfo cafe = ReadCafe(this._verifyCode, CurrentAccount.UserId, false);
                    if (cafe == null)
                    {
                        SetMessageLn("�޷���ȡ�ҵĲ�����Ϣ��");
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
                LogHelper.Write("GameCafe.HelpFriend", ex, LogSeverity.Error);
                SetMessage("��æ����ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        private bool IsChefAvailable(CafeInfo cafe)
        {
            //�������
            if (cafe.Chef && cafe.ChefMana > 0 && !string.IsNullOrEmpty(this._flashid))
                return true;
            else
                return false;
        }

        #region BoxCleanCook
        private void BoxCleanCook(string uid, CafeInfo cafe)
        {
            try
            {
                SetMessageLn("��ʼ���...");

                _tomatoexchangable = true;
                _medlarexchangable = true;
                _crabexchangable = true;
                _pineappleexchangable = true;

                int num = 0;
                foreach (CookingInfo cooking in cafe.Cookings)
                {
                    if (!(cooking.Stage == 2 || cooking.Stage == -1 || cooking.Stage == -98 || cooking.Stage == 0 || cooking.Stage == -2 || cooking.Stage == -3))
                        continue;

                    SetMessageLn(string.Format("��{0}��¯�ӣ�{1}", ++num, cooking.Name));

                    if (cooking.Stage == 2)
                    {
                        SetMessage(" ����װ�̣�");

                        if (Task.BoxClean)
                        {
                            if (IsChefAvailable(cafe))
                            {
                                HH.DelayedTime = Constants.DELAY_1SECONDS;
                                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dish2counter.php?verify={0}&cafeid={1}&uid={2}&orderid={3}&auto=1&rand=0.48554128454998136", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, uid, cooking.OrderId));
                                if (GetCleanFeedback(content))
                                {
                                    content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_stoveclean.php?verify={0}&cafeid={1}&orderid={2}&rand=0.9596593934111297", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId));
                                    if (GetCleanFeedback(content))
                                    {
                                        cooking.Stage = -98;
                                        cafe.ChefMana--;
                                    }
                                }
                            }
                            else
                            {
                                HH.DelayedTime = Constants.DELAY_4SECONDS;
                                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dish2counter.php?verify={0}&cafeid={1}&orderid={2}&rand=0.06428298819810152", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId));
                                if (GetCleanFeedback(content))
                                {
                                    cooking.Stage = -1;
                                }
                            }
                        }
                    }
                    //<stage>-3</stage> ������</font> ������ź���Źǿ츯���ˣ����������˲�̨
                    //<stage>-2</stage> <dirttips><font color='#336699'>ׯ��-johnny</font> ������ź���Źǿ츯���ˣ�����1320Ԫ�չ���ʳ��</dirttips> 
                    if (cooking.Stage == -1 || cooking.Stage == -2 || cooking.Stage == -3)
                    {
                        SetMessage(" ��Ҫ��ࣺ");

                        if (Task.BoxClean)
                        {
                            if (IsChefAvailable(cafe))
                            {
                                HH.DelayedTime = Constants.DELAY_1SECONDS;
                                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_stoveclean.php?verify={0}&cafeid={1}&orderid={2}&rand=0.9596593934111297", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId));
                                if (GetCleanFeedback(content))
                                {
                                    cooking.Stage = -98;
                                    cafe.ChefMana--;
                                }
                            }
                            else
                            {
                                HH.DelayedTime = Constants.DELAY_4SECONDS;
                                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_stoveclean.php?verify={0}&cafeid={1}&orderid={2}&rand=0.9596593934111297", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId));
                                if (GetCleanFeedback(content))
                                    cooking.Stage = -98;
                            }
                        }
                    }

                    if (cooking.Stage == -98 || cooking.Stage == 0)
                    {
                        SetMessage(" ���Գ��ˣ�");
                        if (!Task.Cook)
                            continue;

                        if (Task.CookTomatoFirst && _tomatoexchangable)
                        {
                            SetMessage(" ���ȣ����ѳ��� �һ���");
                            //http://www.kaixin001.com/cafe/api_dishmaterials.php?verify=6209093_1136_6209093_1266493649_87f00011aa9971038dfd4df615ec809d&dishid=1
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dishmaterials.php?verify={0}&dishid=1", DataConvert.GetEncodeData(this._verifyCode)));
                            if (CookTheDish(cafe, cooking, 1))
                                continue;
                        }

                        if (Task.CookMedlarFirst && _medlarexchangable)
                        {
                            SetMessage(" ���ȣ���������� �һ���");
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dishmaterials.php?verify={0}&dishid=60", DataConvert.GetEncodeData(this._verifyCode)));
                            if (CookTheDish(cafe, cooking, 60))
                                continue;
                        }

                        if (Task.CookCrabFirst && _crabexchangable)
                        {
                            SetMessage(" ���ȣ�������բз �һ���");
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dishmaterials.php?verify={0}&dishid=61", DataConvert.GetEncodeData(this._verifyCode)));
                            if (CookTheDish(cafe, cooking, 61))
                                continue;
                        }

                        if (Task.CookPineappleFirst && _pineappleexchangable && cafe.Grade >= 19)
                        {
                            SetMessage(" ���ȣ����ܹ����� �һ���");
                            //http://www.kaixin001.com/cafe/api_dishmaterials.php?verify=6209093_1136_6209093_1266493649_87f00011aa9971038dfd4df615ec809d&dishid=1
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dishmaterials.php?verify={0}&dishid=15", DataConvert.GetEncodeData(this._verifyCode)));
                            if (CookTheDish(cafe, cooking, 15))
                                continue;
                        }

                        if (IsChefAvailable(cafe))
                            SetMessage(string.Format(" ���ȣ�{0} ", GetDishNameById(Task.CookDishId)));
                        else
                            SetMessage(string.Format(" ���ȣ�{0} ����{1}->", GetDishNameById(Task.CookDishId), cooking.Step));

                        if (Task.CookLowCash)
                        {
                            if (cafe.Cash < Task.CookLowCashLimit)
                            {
                                SetMessage(string.Format("�ֽ�{0}�ѵ���{1}��ֹͣ���ˡ�", cafe.Cash, Task.CookLowCashLimit));
                                continue;
                            }
                        }

                        CookTheDish(cafe, cooking, Task.CookDishId);
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
                LogHelper.Write("GameCafe.BoxCleanCook", ex, LogSeverity.Error);
                SetMessage("���ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region CookTheDish
        private bool CookTheDish(CafeInfo cafe, CookingInfo cooking, int dishid)
        {
            int currentstep = 0;
            int totalsteps = 0;
            string content = "";

            if (IsChefAvailable(cafe))
            {
                HH.DelayedTime = Constants.DELAY_2SECONDS;
                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_cooking.php?verify={0}&auto=1&cafeid={1}&orderid={2}&cafepp=2092500&dishid={3}&flashid={4}&rand=0.6033724816516042", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId, dishid, this._flashid));
                if (GetCookFeedback(content, ref currentstep, ref totalsteps))
                {
                    cooking.Stage = 1;
                    cafe.ChefMana--;
                    return true;
                }                
            }
            else
            {
                HH.DelayedTime = Constants.DELAY_2SECONDS;
                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_cooking.php?verify={0}&cafeid={1}&orderid={2}&dishid={3}&rand=0.36006107507273555", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId, dishid));
                if (GetCookFeedback(content, ref currentstep, ref totalsteps))
                {
                    for (int ix = currentstep; ix < totalsteps; ix++)
                    {
                        SetMessage(string.Format("����{0}->", ix + 1));
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_cooking.php?verify={0}&cafeid={1}&orderid={2}&dishid={3}&rand=0.36006107507273555", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId, dishid));
                        int currentstep2 = 0;
                        int totalsteps2 = 0;
                        if (!GetCookFeedback(content, ref currentstep2, ref totalsteps2))
                        {
                            cooking.Stage = 0;
                            return true;
                        }
                    }
                    cooking.Stage = 1;
                    return true;
                }                
            }

            if (content.Contains("��Ŀǰ�ļ�������������") || content.Contains("�һ�ʧ��"))
            {
                if (dishid == 1)
                    this._tomatoexchangable = false;
                if (dishid == 60)
                    this._medlarexchangable = false;
                if (dishid == 61)
                    this._crabexchangable = false;
                if (dishid == 15)
                    this._pineappleexchangable = false;
            }

            return false;
        }
        #endregion

        #region GetCleanFeedback
        private bool GetCleanFeedback(string content)
        {
            try
            {
                //���
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><orderid>3487435</orderid><ret>succ</ret><err>0</err><account><cash>9607</cash><evalue>2408</evalue><maxevalue>0</maxevalue></account><addcash>-15</addcash><addevalue>1</addevalue></data>
                //װ��
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><orderid>3487434</orderid><foodnum>205</foodnum><account><evalue>2460</evalue><maxevalue>0</maxevalue></account><addevalue>48</addevalue><ret>succ</ret><err>0</err><torderid>3487440</torderid></data>
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><orderid>135628719</orderid><ret>fail</ret><err>1</err><msg>û�п����̨</msg></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + "ʧ�ܣ�");
                    if (content.IndexOf("û�п����̨") > -1)
                    {
                        LogHelper.Write(CurrentAccount.UserName, "û�п����̨", LogSeverity.Warn);
                    }
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage(" �ɹ���");
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
                LogHelper.Write("GameCafe.GetCookFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region GetCookFeedback
        private bool GetCookFeedback(string content, ref int currentstep, ref int totalsteps)
        {
            try
            {
                //<data>
                // <ret>fail</ret> 
                // <err>1</err> 
                // <msg>�һ�ʧ��!<br>��������ֿ��е�ԭ���ϲ���<br>��԰����ֲ��<br>������������</msg> 
                // <dish>
                // <dishui>cook.xhsjd.eat100</dishui> 
                // <badui>cook.xhsjd.b</badui> 
                // <matureui>cook.xhsjd.m</matureui> 
                // <resver>0</resver> 
                // <bdiscard>0</bdiscard> 
                // </dish>
                // <from>change</from> 
                // <orderid>139864968</orderid> 
                // </data>

                //���
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>succ</ret><err>0</err><dish><dishui>cook.gbjd.eat100</dishui><badui>cook.gbjd.b</badui><matureui>cook.gbjd.m</matureui><resver>0</resver><bdiscard>0</bdiscard><dishid>4</dishid><name>宫保鸡丁</name><foodnum>100</foodnum><ui>cook.gbjd.eat100</ui><foodui>100;80;20</foodui><income>1</income><stage>0</stage><step>0</step><totalstep>3</totalstep><stepname>空锅</stepname><stepui>cafe.pot</stepui><tips><picRes>cook.gbjd.t1</picRes><tips>切鸡�</tips></tips></dish><account><cash>9770</cash></account><addcash>-35</addcash><orderid>3487435</orderid></data>
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><orderid>98102659</orderid><warning>1</warning><ret>fail</ret><err>1</err><msg>����ʧ�ܣ�������ˢ��ҳ�����ԣ�</msg></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")) + "ʧ�ܣ�");
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessage(" �ɹ���");
                    currentstep = DataConvert.GetInt32(JsonHelper.GetMid(content, "<step>", "</step>"));
                    totalsteps = DataConvert.GetInt32(JsonHelper.GetMid(content, "<totalstep>", "</totalstep>"));
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
                LogHelper.Write("GameCafe.GetCookFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region Hire
        private void Hire(CafeInfo cafe)
        {
            try
            {
                SetMessageLn("��ʼ��Ա...");

                if (cafe.Employees.Count >= Task.MaxEmployees)
                {
                    SetMessage(string.Format("����{0}λԱ��������", cafe.Employees.Count));
                    return;
                }

                int count = cafe.Employees.Count;

                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_myemployees.php?verify={0}&cafeid={1}&uid={2}&start=0&rand=0.7985394787974656", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, CurrentAccount.UserId));
                string content2 = "";
                int num;                
                bool bneeds = false;
                bool bhired = false;

                if (JsonHelper.GetMid(content, "<role>emptyemp", "</role>") == null)
                    content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_myemployees.php?verify={0}&cafeid={1}&uid={2}&start=6&rand=0.7985394787974656", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, CurrentAccount.UserId));

                for (string pos = JsonHelper.GetMid(content, "<role>emptyemp", "</role>", out num); pos != null; pos = JsonHelper.GetMid(content, "<role>emptyemp", "</role>", out num))
                {
                    bneeds = true;
                    bhired = false;
                    //��ȥ��Ӷ�������еĺ���
                    SetMessageLn("�ȹ�Ӷ�������еĺ��ѣ�");
                    int num2 = 0;
                    foreach (int uid in Operation.HireWhiteList)
                    {
                        try
                        {
                            SetMessageLn(string.Format("#{0}{1}", ++num2, base.GetFriendNameById(uid)) + "=>");

                            if (Operation.HireBlackList.Contains(uid))
                            {
                                SetMessage(base.GetFriendNameById(uid) + "�ڹ�Ӷ�������У�����");
                                continue;
                            }

                            content2 = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_employ.php?verify={0}&cafeid={1}&touid={2}&rand=0.6188818383961916", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, uid));
                            if (GetHireFeedback(content2))
                            {
                                count++;
                                Operation.HireWhiteList.Remove(uid);
                                bhired = true;
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
                            LogHelper.Write("GameCafe.Hire(HireWhiteList)", GetFriendNameById(uid), ex, LogSeverity.Error);
                            continue;
                        }
                    }

                    if (count >= Task.MaxEmployees)
                    {
                        SetMessageLn(string.Format("����{0}λԱ����ֹͣ��Ӷ", count));
                        return;
                    }

                    if (Operation.HireAll && !bhired)
                    {
                        if (this._hirableFriendsList == null || this._hirableFriendsList.Count == 0)
                            ReadHirableFriends(cafe, false);

                        //��������
                        num2 = 0;
                        SetMessageLn("��ʼȥ��Ӷ�������ѣ�");
                        foreach (FriendInfo friend in this._hirableFriendsList)
                        {
                            try
                            {
                                if (Operation.HireWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                                    continue;

                                SetMessageLn(string.Format("#{0}{1}", ++num2, friend.Name + "=>"));
                                if (Operation.HireBlackList.Contains(friend.Id))
                                {
                                    SetMessage(friend.Name + "�ڹ�Ӷ�������У�����");
                                    continue;
                                }

                                content2 = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_employ.php?verify={0}&cafeid={1}&touid={2}&rand=0.6188818383961916", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, friend.Id));
                                if (GetHireFeedback(content2))
                                {
                                    count++;
                                    this._hirableFriendsList.Remove(friend);
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
                                LogHelper.Write("GameCafe.Hire(HireAll)", friend.Name, ex, LogSeverity.Error);
                                continue;
                            }
                        }
                    }
                    if (count >= Task.MaxEmployees)
                    {
                        SetMessageLn(string.Format("����{0}λԱ����ֹͣ��Ӷ", count));
                        return;
                    }
                    content = content.Substring(num);
                }
                if (bneeds == false)
                    SetMessage("����Ҫ��Ա");
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
                LogHelper.Write("GameCafe.Hire", ex, LogSeverity.Error);
                SetMessage("��Աʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetHireFeedback
        private bool GetHireFeedback(string content)
        {
            try
            {
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>succ</ret><err>0</err><touid>6209015</touid><toreal_name>������</toreal_name><toicon>http://img.kaixin001.com.cn/i/50_1_0.gif</toicon><togender>1</togender><msg>��Ӷ�ɹ���<br/>�����ྫ�����棬������ʢ����Ϊ�㹤��4��3Сʱ��</msg><avatar><item><type>1</type><ui>avatar.face3</ui><val></val></item><item><type>2</type><ui>avatar.eye1</ui><val></val></item><item><type>3</type><ui>avatar.nose1</ui><val></val></item><item><type>4</type><ui>avatar.mouth1</ui><val></val></item><item><type>5</type><ui>avatar.brow1</ui><val></val></item><item><type>6</type><ui>avatar.hair15</ui><val></val></item><item><type>7</type><ui>avatar.</ui><val>fff1ef</val></item><item><type>8</type><ui></ui><val></val></item><item><type>9</type><ui></ui><val></val></item><item><type>10</type><ui>avatar.</ui><val>33cc00</val></item></avatar><cloth><item><type>1</type><ui>avatar.coat41</ui><val></val></item><item><type>2</type><ui>avatar.trousers44</ui><val></val></item><item><type>3</type><ui>avatar.shoe42</ui><val></val></item></cloth></data>
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>fail</ret><err>1</err><msg>��Ӷ����ʧ�ܣ�������Ѿ���Ӷ����</msg><avatar><item><type>1</type><ui>avatar.face1</ui><val></val></item><item><type>2</type><ui>avatar.eye1</ui><val></val></item><item><type>3</type><ui>avatar.nose1</ui><val></val></item><item><type>4</type><ui>avatar.mouth1</ui><val></val></item><item><type>5</type><ui>avatar.brow1</ui><val></val></item><item><type>6</type><ui>avatar.hair4</ui><val></val></item><item><type>7</type><ui>avatar.</ui><val>f7cfaa</val></item><item><type>8</type><ui></ui><val></val></item><item><type>9</type><ui></ui><val></val></item><item><type>10</type><ui>avatar.</ui><val>000000</val></item></avatar><cloth><item><type>1</type><ui>avatar.coat25</ui><val></val></item><item><type>2</type><ui>avatar.trousers25</ui><val></val></item><item><type>3</type><ui>avatar.shoe42</ui><val></val></item></cloth></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessageLn(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                }
                else if (content.IndexOf("<ret>succ</ret>") > -1)
                {
                    SetMessageLn(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
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
                LogHelper.Write("GameCafe.GetHireFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region PresentFood
        private void PresentFood(string uid, CafeInfo cafe)
        {
            try
            {
                SetMessageLn("��ʼ����ʳ��...");

                if (Operation.PresentFoodId == 0)
                {
                    SetMessage("û���趨���͵Ķ����޷�����");
                    return;
                }
                if (!IsAlreadyMyFriend(DataConvert.GetString(Operation.PresentFoodId)))
                {
                    SetMessage(DataConvert.GetString(Operation.PresentFoodId) + "������ĺ��ѣ��޷�����");
                    return;
                }

                if (Task.PresentLowCash)
                {
                    if (cafe.Cash < Task.PresentLowCashLimit)
                    {
                        SetMessage(string.Format("�ֽ�{0}�ѵ���{1}��ֹͣ���͡�", cafe.Cash, Task.PresentLowCashLimit));
                        return;
                    }
                }

                if (Task.PresentFoodLowCount)
                {
                    int foodcount = GetFoodCount(cafe.DinnerTables);
                    if (foodcount < Task.PresentFoodLowCountLimit)
                    {
                        SetMessage(string.Format("��̨�ϵ�ʳ��������{0}�ѵ���{1}��ֹͣ���͡�", foodcount, Task.PresentFoodLowCountLimit));
                        return;
                    }
                }

                if (Task.PresentFoodByCount)
                {
                    cafe.DinnerTables = SortDinnerTableByNum(cafe.DinnerTables);
                    foreach (DinnerTableInfo dinnertable in cafe.DinnerTables)
                    {
                        if (Task.PresentForbiddenFoodList.Contains(dinnertable.DishId))
                        {
                            SetMessageLn(string.Format("{0}�ڽ�ֹ�����б��У����� ", dinnertable.Name));
                            continue;
                        }

                        if (dinnertable.Num > 0)
                        {
                            long presentcount = dinnertable.Num * Task.PresentFoodRatio / 100;
                            if (presentcount > 0)
                            {
                                SetMessageLn(string.Format("������{0}����{1}*{2}%={3}��{4}...", base.GetFriendNameById(Operation.PresentFoodId),dinnertable.Num, Task.PresentFoodRatio, presentcount, dinnertable.Name));
                                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dishact.php?verify={0}&cafeid={1}&orderid={2}&touid={3}&num={4}&msg={5}&anony=0&r=0.5074032391421497", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, dinnertable.OrderId, Operation.PresentFoodId, presentcount, DataConvert.GetEncodeData(Task.PresentFoodMessage)));
                                if (GetPresentFeedback(content))
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (DinnerTableInfo dinnertable in cafe.DinnerTables)
                    {
                        if (dinnertable.Num > 0 && dinnertable.DishId == Task.PresentFoodDishId)
                        {
                            long presentcount = dinnertable.Num * Task.PresentFoodRatio / 100;
                            if (presentcount > 0)
                            {
                                SetMessageLn(string.Format("������{0}����{1}*{2}%={3}��{4}...", base.GetFriendNameById(Operation.PresentFoodId), dinnertable.Num, Task.PresentFoodRatio, presentcount, dinnertable.Name));
                                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_dishact.php?verify={0}&cafeid={1}&orderid={2}&touid={3}&num={4}&msg={5}&anony=0&r=0.5074032391421497", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, dinnertable.OrderId, Operation.PresentFoodId, presentcount, DataConvert.GetEncodeData(Task.PresentFoodMessage)));
                                if (GetPresentFeedback(content))
                                    break;
                            }
                            break;
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
                LogHelper.Write("GameCafe.PresentFood", ex, LogSeverity.Error);
                SetMessage("����ʳ��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetPresentFeedback
        private bool GetPresentFeedback(string content)
        {
            try
            {
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>succ</ret><err>0</err><msg>赠送成�?&lt;br&gt;已放入庄�?johnny的仓库中。庄�?johnny将在系统消息中收到赠送消息�?/msg><sendnum>10000</sendnum><orderid>65996961</orderid></data>
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>fail</ret><err>1</err><msg>����ʧ�ܣ�&lt;br&gt;������Ѿ���ׯ��-johnny�͹������ˣ����������Ͱɣ�</msg><orderid>65996961</orderid></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                    //����ʧ�ܣ�������Ѿ������Կ��͹������ˣ����������Ͱɣ�
                    Regex regular = new Regex(@"������Ѿ���[\s\S]+�͹������ˣ����������Ͱɣ�");
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
                LogHelper.Write("GameCafe.GetHireFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region PurchaseFood
        private void PurchaseFood(CafeInfo cafe)
        {
            try
            {
                SetMessageLn("��ʼ�չ�ʳ��...");

                if (cafe.Grade < 30)
                {
                    SetMessage("�ȼ�δ��30�����޷��չ���");
                    return;
                }
                _purchaseblocked = false;

                //��ȥ�չ��������еĺ���
                SetMessageLn("���չ��������к��ѵ�ʳ�");
                int num = 0;
                foreach (int uid in Operation.PurchaseWhiteList)
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0}{1}", ++num, base.GetFriendNameById(uid)) + "=>");

                        if (Operation.PurchaseBlackList.Contains(uid))
                        {
                            SetMessage(base.GetFriendNameById(uid) + "���չ��������У�����");
                            continue;
                        }

                        PurchaseTheCafe(uid.ToString());
                        if (_purchaseblocked)
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
                        LogHelper.Write("GameCafe.PurchaseFood(PurchaseWhiteList)", GetFriendNameById(uid), ex, LogSeverity.Error);
                        continue;
                    }
                }

                if (Operation.PurchaseAll)
                {
                    //��������
                    num = 0;
                    SetMessageLn("��ʼȥ�չ��������ѵ�ʳ�");
                    foreach (FriendInfo friend in this._allCafeFriendsList)
                    {
                        try
                        {
                            if (friend.Food == false)
                                continue;

                            if (Operation.PurchaseWhiteList.Contains(friend.Id) || friend.Id.ToString() == CurrentAccount.UserId)
                                continue;

                            SetMessageLn(string.Format("#{0}{1}", ++num, friend.Name + "=>"));
                            if (Operation.PurchaseBlackList.Contains(friend.Id))
                            {
                                SetMessage(friend.Name + "���չ��������У�����");
                                continue;
                            }
                            PurchaseTheCafe(friend.Id.ToString());
                            if (_purchaseblocked)
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
                            LogHelper.Write("GameCafe.PurchaseFood(PurchaseAll)", friend.Name, ex, LogSeverity.Error);
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
                LogHelper.Write("GameCafe.PurchaseFood", ex, LogSeverity.Error);
                SetMessage("�չ�ʳ��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region PurchaseTheCafe
        private void PurchaseTheCafe(string uid)
        {
            _todaypurchasedlimit = false;
            CafeInfo cafe = ReadCafe(this._verifyCode, uid, true);
            if (cafe == null)
            {
                SetMessageLn(string.Format("�޷���ȡ{0}�Ĳ�����Ϣ��", base.GetFriendNameById(uid)));
                return;
            }

            if (cafe.Grade < 20)
            {
                SetMessage(string.Format("{0}�ĵȼ�δ��20�����޷��չ�����������ʳ�", cafe.Name));
                return;
            }

            string content = "";
            int num = 0;
            DishInfo transactionDish;
            string title = "";
            decimal currentprice = 0;
            int purchasedcount = 0;
            long leftbadtime = 0;
            long leftbuytime = 0;
            foreach (CookingInfo cooking in cafe.Cookings)
            {
                if (cooking.Stage != 2)
                    continue;

                SetMessageLn(string.Format("#{0} {1} ", ++num, cooking.Name));

                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_checkfood.php?verify={0}&cafeid={1}&uid={2}&orderid={3}&rand=0.7839874033816159", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, uid, cooking.OrderId));
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>succ</ret><err>0</err><stage>2</stage><leftbadtime>9705</leftbadtime><leftbuytime>-16215</leftbuytime><orderid>65768265</orderid></data>
                leftbadtime = DataConvert.GetInt64(JsonHelper.GetMid(content, "<leftbadtime>", "</leftbadtime>"));
                leftbuytime = DataConvert.GetInt64(JsonHelper.GetMid(content, "<leftbuytime>", "</leftbuytime>"));
                SetMessage(CalculateTime(leftbadtime, leftbuytime));
                if (leftbuytime > 0)
                    continue;

                content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_stovedish.php?verify={0}&cafeid={1}&uid={2}&orderid={3}&r=0.5512974858283997", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, uid, cooking.OrderId));
                //<data>
                //<ret>succ</ret>
                //<title>
                //<b>������<font color='#FF6600'>1320Ԫ</font>�۸��չ�<font color='#FF6600'>550��</font>��ź���Ź���<br>����:<font color='#FF6600'>2.4Ԫ</font></b>
                //</title>
                //<name>��ź���Ź�</name>
                //<ui>cook.dpg</ui>
                //<foodnum>550</foodnum>
                //</data>
                if (content.Contains("<ret>succ</ret>"))
                {
                    if (Task.PurchaseFoodByRefPrice)
                    {
                        transactionDish = GetTransactionDishById(cooking.DishId);
                        if (transactionDish == null)
                        {
                            SetMessage("�ڽ��ױ���û���ҵ��ò��ȣ�����");
                            continue;
                        }
                        title = JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<title>", "</title>"));
                        SetMessage(title);
                        currentprice = DataConvert.GetDecimal(JsonHelper.GetMid(title, "����:", "Ԫ"));
                        if (currentprice <= transactionDish.PurchasePrice)
                        {
                            SetMessage(string.Format(" ��ǰ�۸�{0}<=�չ��۸�{1}�������չ�...", currentprice, transactionDish.PurchasePrice));
                            content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_buydish.php?verify={0}&cafeid={1}&orderid={2}&uid={3}&r=0.2824217448942363", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId, uid));
                            if (GetPurchaseFeedback(content))
                            {
                                purchasedcount++;
                                if (purchasedcount >= 3)
                                    break;
                            }
                            if (_todaypurchasedlimit)
                                break;
                            if (_purchaseblocked)
                                break;
                        }
                        else
                        {
                            SetMessage(string.Format(" ��ǰ�۸�{0}>�չ��۸�{1}������", currentprice, transactionDish.PurchasePrice));
                        }
                    }
                    else
                    {
                        SetMessage("���Ե�ǰ�۸񣬳����չ�...");
                        content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_buydish.php?verify={0}&cafeid={1}&orderid={2}&uid={3}&r=0.2824217448942363", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, cooking.OrderId, uid));
                        if (GetPurchaseFeedback(content))
                        {
                            purchasedcount++;
                            if (purchasedcount >= 3)
                                break;
                        }
                        if (_todaypurchasedlimit)
                            break;
                        if (_purchaseblocked)
                            break;
                    }
                }
                else if (content.Contains("<ret>fail</ret>"))
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
                }
            }
        }
        #endregion

        #region GetPurchaseFeedback
        private bool GetPurchaseFeedback(string content)
        {
            try
            {
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>succ</ret><err>0</err><msg>收购成功�?lt;br&gt;成功购进�?&lt;font color='#FF0000'&gt;550&lt;/font&gt;�?lt;font color='#0000FF'&gt;莲藕炖排�?lt;/font&gt;�?lt;br&gt;食物已放入你的仓库�?lt;br&gt;餐厅现金金额�?lt;font color='#FF00'&gt;1295045&lt;/font&gt;元�?/msg><dirttips>&lt;font color='#336699'&gt;庄荣-johnny&lt;/font&gt; 看到快腐坏了，给�?320元收购了食物</dirttips></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    string printcontent = JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>"));
                    if (String.IsNullOrEmpty(printcontent))
                        LogHelper.Write("GameCafe.GetPurchaseFeedback", content, LogSeverity.Warn);
                    SetMessage(printcontent);

                    //���Կ˲�����ʳ������Ѿ����չ���3�Σ������������ɣ�
                    if (printcontent.Contains("�����Ѿ����չ���3�Σ�������������"))
                        _todaypurchasedlimit = true;

                    //Regex regular2 = new Regex(@"ʱ�����Ѿ��չ���[\s\S]+�Σ������´��չ�");
                    //if (regular2.IsMatch(printcontent))
                    //{
                    //    _purchaseblocked = true;
                    //}
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
                LogHelper.Write("GameCafe.GetPurchaseFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region SellFood
        private void SellFood(string uid, CafeInfo cafe)
        {
            try
            {
                SetMessageLn("��ʼ����ʳ��...");

                string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_mygranary.php?verify={0}&page=0&type=dish&cafeid={1}&r=0.25149386934936047", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId));

                if (content.IndexOf("<goods>") <= -1)
                {
                    SetMessage("�ֿ���û���κ�ʳ��������");
                    return;
                }

                content = JsonHelper.GetMid(content, "<goods>", "</goods>");
                int index = 0;
                int num;
                int id = 0;
                int count = 0;
                int type = 0;
                string name = "";
                decimal currentprice = 0;
                string content2 = "";
                DishInfo dish;
                for (string pos = JsonHelper.GetMid(content, "<item>", "</item>", out num); pos != null; pos = JsonHelper.GetMid(content, "<item>", "</item>", out num))
                {
                    content = content.Substring(num);
                    id = JsonHelper.GetMidInteger(pos, "<id>", "</id>");
                    count = JsonHelper.GetMidInteger(pos, "<num>", "</num>");
                    name = JsonHelper.GetMid(pos, "<name>", "</name>");
                    type = JsonHelper.GetMidInteger(pos, "<type>", "</type>");

                    SetMessageLn(string.Format("#{0} {1} ������{2} ", ++index, name, count));
                    if (Task.SellFoodByRefPrice)
                    {
                        dish = GetTransactionDishById(id);
                        if (dish == null)
                        {
                            SetMessage("�ڽ��ױ���û���ҵ��ò��ȣ�����");
                            continue;
                        }
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        content2 = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_tradeinfo.php?verify={0}&cafeid={1}&uid={2}&dishid={3}&type={4}&r=0.3238336769863963", DataConvert.GetEncodeData(this._verifyCode), cafe.CafeId, uid, id, type));
                        currentprice = DataConvert.GetDecimal(JsonHelper.GetMidLast(content2, "<item><price>", "</price><dtime>"));
                        if (currentprice >= dish.SellPrice)
                        {
                            SetMessage(string.Format("��ǰ�۸�{0}>=���ۼ۸�{1}�����Գ���...", currentprice, dish.SellPrice));
                            content2 = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_salegoods.php?verify={0}&id={1}&type={2}&num={3}&r=0.6677296073175967", DataConvert.GetEncodeData(this._verifyCode), id, type, count));
                            GetSellFeedback(content2);
                        }
                        else
                        {
                            SetMessage(string.Format("��ǰ�۸�{0}<���ۼ۸�{1}������", currentprice, dish.SellPrice));                            
                        }
                    }
                    else
                    {
                        SetMessage("���Ե�ǰ�۸񣬳��Գ���...");
                        content2 = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_salegoods.php?verify={0}&id={1}&type={2}&num={3}&r=0.6677296073175967", DataConvert.GetEncodeData(this._verifyCode), id, type, count));
                        GetSellFeedback(content2);
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
                LogHelper.Write("GameCafe.PresentFood", ex, LogSeverity.Error);
                SetMessage("����ʳ��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetSellFeedback
        private bool GetSellFeedback(string content)
        {
            try
            {
                //<?xml version="1.0" encoding="UTF-8" ?>
                //<data><ret>succ</ret><err>0</err><account><cash>92055</cash></account><tips></tips><bnext>0</bnext><goods><item><ui>cook.hsqz</ui><id>5</id><type>21</type><tkey>dish</tkey><num>13</num><name>红烧茄子</name><saletitle>&lt;b&gt;你想�?lt;font color='#FF6600'&gt;2�?lt;/font&gt;的价格出售“红烧茄子”吗�?lt;/b&gt;</saletitle><nsend>赠送失败！&lt;br&gt;这是好友赠送的礼品，不能转赠�?/nsend><ckey>recv</ckey><otx>10</otx><oty>-20</oty></item><item><ui>cook.jjrs</ui><id>6</id><type>21</type><tkey>dish</tkey><num>1</num><name>京酱肉丝</name><saletitle>&lt;b&gt;你想�?lt;font color='#FF6600'&gt;2�?lt;/font&gt;的价格出售“京酱肉丝”吗�?lt;/b&gt;</saletitle><nsend>赠送失败！&lt;br&gt;这是好友赠送的礼品，不能转赠�?/nsend><ckey>recv</ckey><otx>10</otx><oty>-20</oty></item><item><ui>cook.mrzs</ui><id>22</id><type>21</type><tkey>dish</tkey><num>54</num><name>美容猪手</name><saletitle>&lt;b&gt;你想�?lt;font color='#FF6600'&gt;2�?lt;/font&gt;的价格出售“美容猪手”吗�?lt;/b&gt;</saletitle><nsend>赠送失败！&lt;br&gt;这是好友赠送的礼品，不能转赠�?/nsend><ckey>recv</ckey><otx>10</otx><oty>-20</oty></item></goods><gtype><item>goods</item><item>dish</item><item>clothing</item></gtype><type>dish</type><addcash>5</addcash><msg>出售成功!&lt;br&gt;共获�?lt;font color='#FF6600'&gt;5�?lt;/font&gt;现金</msg><id></id></data>
                if (content.IndexOf("<ret>fail</ret>") > -1)
                {
                    SetMessage(JsonHelper.FiltrateHtmlTags(JsonHelper.GetMid(content, "<msg>", "</msg>")));
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
                LogHelper.Write("GameCafe.GetHireFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion
        
        #region SortEmployeeByPower
        private Collection<FriendInfo> SortEmployeeByPower(Collection<FriendInfo> employees)
        {
            for (int ix = 0; ix < employees.Count; ix++)
            {
                for (int iy = ix + 1; iy < employees.Count; iy++)
                {
                    if (employees[ix].Power < employees[iy].Power)
                    {
                        FriendInfo temp = employees[ix];
                        employees[ix] = employees[iy];
                        employees[iy] = temp;
                    }
                }
            }

            return employees;
        }
        #endregion

        #region SortDinnerTableByNum
        private Collection<DinnerTableInfo> SortDinnerTableByNum(Collection<DinnerTableInfo> dinnertables)
        {
            for (int ix = 0; ix < dinnertables.Count; ix++)
            {
                for (int iy = ix + 1; iy < dinnertables.Count; iy++)
                {
                    if (dinnertables[ix].Num < dinnertables[iy].Num)
                    {
                        DinnerTableInfo temp = dinnertables[ix];
                        dinnertables[ix] = dinnertables[iy];
                        dinnertables[iy] = temp;
                    }
                }
            }

            return dinnertables;
        }
        #endregion

        #region GetFoodCount
        private int GetFoodCount(Collection<DinnerTableInfo> dinnertables)
        {
            int count = 0;
            foreach (DinnerTableInfo dinnertable in dinnertables)
            {
                if (dinnertable.Num > 0)
                    count++;
            }
            return count;
        }
        #endregion

        #region GetDishNameById
        private string GetDishNameById(int dishid)
        {
            foreach (DishInfo dish in this._dishesList)
            {
                if (dish.DishId == dishid)
                {
                    return dish.Title;
                }
            }
            return dishid.ToString();
        }
        #endregion

        #region GetDishNameById
        private DishInfo GetTransactionDishById(int dishid)
        {
            foreach (DishInfo dish in this._transactiondDishesList)
            {
                if (dish.DishId == dishid)
                {
                    return dish;
                }
            }
            return null;
        }
        #endregion

        #region CalculateTime
        private string CalculateTime(long leftbadtime, long leftbuytime)
        {
            if (leftbadtime == 0 && leftbuytime == 0)
                return "";

            string strBad = "";
            string strBuy = "";
            if (leftbadtime > 0)
                strBad = "���븯��" + leftbadtime / 3600 + "Сʱ" + (leftbadtime % 3600) / 60 + "����";
            if (leftbuytime > 0)
                strBuy = "����չ�" + leftbuytime / 3600 + "Сʱ" + (leftbuytime % 3600) / 60 + "����";
            return strBad + ";" + strBuy;
        }
        #endregion

        #region Request

        public string RequestCafeHomePage(bool IsInitial)
        {
            string content = HH.Get("http://www.kaixin001.com/!cafe/index.php");
            if (content.IndexOf("<title>������ - ������</title>") != -1)
            {
                SetMessageLn("��δ��װ����������,���԰�װ��...");
                HH.Post("http://www.kaixin001.com/app/install.php", "aid=1136&isinstall=1");
                content = HH.Get("http://www.kaixin001.com/!cafe/index.php");
            }
            this._verifyCode = JsonHelper.GetMid(content, "'FlashVars', 'verify=", "&cgidomain=");
            this._flashid = JsonHelper.GetMid(content, "flashid=", "&shareitem");
            return content;
        }

        public string RequestAllCafeFriends()
        {            
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get("http://www.kaixin001.com/cafe/api_friendlist.php?verify=" + this._verifyCode);
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, CurrentAccount.UserName);
            //}
            return content;
        }

        public string RequestCafeConf(string verifyCode, string uid)
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get(string.Format("http://www.kaixin001.com/cafe/api_getconf.php?verify={0}&rand=0%2E4566531740128994&uid={1}", verifyCode, uid));
            //if (!String.IsNullOrEmpty(content))
            //{
            //    ConfigCtrl.SaveXmlStringToFile(content, uid + "RequestGetConf");
            //}
            return content;
        }

        #endregion

        #region Properties

        public Collection<FriendInfo> AllCafeFriendsList
        {
            get { return this._allCafeFriendsList; }
        }

        public Collection<FriendInfo> HirableFriendsList
        {
            get { return this._hirableFriendsList; }
        }
        
        public Collection<DishInfo> DishesList
        {
            get { return _dishesList; }
            set { _dishesList = value; }
        }

        public Collection<DishInfo> TransactiondDishesList
        {
            get { return _transactiondDishesList; }
            set { _transactiondDishesList = value; }
        }

        #endregion
    }
}
