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
    public class GamePark : KaixinBase
    {
        private string _parkAcc;
        private Collection<FriendInfo> _parkFriendsList;
        private Collection<FriendInfo> _parkEmptyGarageFriendsList;
        private Collection<CarInfo> _carList;
        private Collection<SeatInfo> _seatList;
        private Collection<int> _validParkList;
        private int _parkcash;
        private int _carprice;
        private Collection<NewCarInfo> _allCarsInMarketList;
        private Collection<MatchInfo> _matchList;
        //private Collection<NewCarInfo> _buyNewCarList;
        private MatchStatus _matchstatus;

        private bool _outofmoney;
        private int _carcount;

        public delegate void ParkFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event ParkFriendsFetchedEventHandler ParkFriendsFetched;

        public delegate void ParkEmptyGarageFriendsFetchedEventHandler(Collection<FriendInfo> friends);
        public event ParkEmptyGarageFriendsFetchedEventHandler ParkEmptyGarageFriendsFetched;

        public delegate void MyCarFetchedEventHandler(Collection<CarInfo> cars, int parkcash, int carprice);
        public event MyCarFetchedEventHandler MyCarFetched;

        public delegate void AllCarsInMarketFetchedEventHandler(Collection<NewCarInfo> carsinmarket);
        public event AllCarsInMarketFetchedEventHandler AllCarsInMarketFetched;

        public delegate void BuildTeamFinishedEventHandler(Collection<CarInfo> cars, int parkcash, int carprice);
        public event BuildTeamFinishedEventHandler BuildTeamFinished;

        public delegate void BuyCardsFinishedEventHandler();
        public event BuyCardsFinishedEventHandler BuyCardsFinished;

        public delegate void ToolParkFinishedEventHandler();
        public event ToolParkFinishedEventHandler ToolParkFinished;

        public GamePark()
        {
            this._parkFriendsList = new Collection<FriendInfo>();
            this._parkEmptyGarageFriendsList = new Collection<FriendInfo>();
            this._carList = new Collection<CarInfo>();
            this._seatList = new Collection<SeatInfo>();
            this._validParkList = new Collection<int>();
            this._parkcash = -1;
            this._allCarsInMarketList = new Collection<NewCarInfo>();
            this._matchList = new Collection<MatchInfo>();
            //this._buyNewCarList = new Collection<NewCarInfo>();
            this._matchstatus = MatchStatus.UnKnown;
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //parking
                SetMessageLn("���ڳ�ʼ��[����λ]...");

                string content = RequestParkHomePage();
                string content2 = RequestParkFriends(false);
                ReadParkFriends(content2, false, false);
                SetMessage("[��������λ�ĺ���]��Ϣ���سɹ���");
                content2 = RequestParkFriends(true);
                ReadParkFriends(content2, true, false);
                SetMessage("[Ŀǰ�пճ�λ�ĺ���]��Ϣ���سɹ���");
                ReadCars(content, false);
                SetMessage("[�ҵ�����]��Ϣ���سɹ���");
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
                LogHelper.Write("GamePark.Initialize", ex, LogSeverity.Error);
                SetMessage(" ��ʼ��[����λ]ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetParkFriends
        public void GetParkFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetParkFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetParkFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_PARKING;
                SetMessageLn("ˢ��[�ҵ�����λ�ĺ���]��Ϣ...");

                //login
                if (!this.ValidationLogin(true))
                {
                    if (ParkFriendsFetched != null)
                        ParkFriendsFetched(_parkFriendsList);
                    return;
                }

                //read friend            
                string content = RequestParkHomePage();
                content = RequestParkFriends(false);
                this.ReadParkFriends(content, false, true);
                SetMessageLn("[�ҵ�����λ�ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (ParkFriendsFetched != null)
                    ParkFriendsFetched(_parkFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[�ҵ�����λ�ĺ���]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region GetParkEmptyGarageFriends
        public void GetParkEmptyGarageFriendsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetParkEmptyGarageFriends));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetParkEmptyGarageFriends()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_PARKING;
                SetMessageLn("ˢ��[Ŀǰ�пճ�λ�ĺ���]��Ϣ...");

                //login
                if (!this.ValidationLogin(true))
                {
                    if (ParkEmptyGarageFriendsFetched != null)
                        ParkEmptyGarageFriendsFetched(_parkFriendsList);
                    return;
                }

                //read friend            
                string content = RequestParkHomePage();
                content = RequestParkFriends(true);
                this.ReadParkFriends(content, true, true);
                SetMessageLn("[Ŀǰ�пճ�λ�ĺ���]��Ϣˢ�³ɹ���");

                //invoke event
                if (ParkEmptyGarageFriendsFetched != null)
                    ParkEmptyGarageFriendsFetched(_parkFriendsList);
            });
            base.ExecuteTryCatchBlock(th, "[Ŀǰ�пճ�λ�ĺ���]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region ReadParkFriends
        private void ReadParkFriends(string content, bool empty, bool printMessage)
        {
            if (printMessage)
                SetMessageLn("��ȡ����λ������Ϣ...");
            //string strFriends = JsonHelper.GetMid(content, "v_frienddata = ", "\n");
            if (content != null)
            {
                if (empty)
                    this._parkEmptyGarageFriendsList.Clear();
                else
                    this._parkFriendsList.Clear();
                JsonTextParser parser = new JsonTextParser();

                //�ҵ���������λ�ĺ���
                JsonArrayCollection friendslist = parser.Parse(content) as JsonArrayCollection;
                foreach (JsonObjectCollection item in friendslist)
                {
                    FriendInfo friend = new FriendInfo();
                    friend.Id = JsonHelper.GetIntegerValue(item["uid"]);
                    friend.Full = JsonHelper.GetStringValue(item["full"]) != "0";
                    friend.Name = JsonHelper.GetStringValue(item["real_name"]);
                    friend.Online = JsonHelper.GetIntegerValue(item["online"]) == 1;
                    friend.IsNeighbor = JsonHelper.GetIntegerValue(item["neighbor"]) == 1;

                    if (printMessage)
                    {
                        if (empty)
                            SetMessageLn(string.Format(" #{0}: {1}({2}) {3}", new object[] { _parkEmptyGarageFriendsList.Count + 1, friend.Name, friend.Id, friend.Full ? "��λ��" : "" }));
                        else
                            SetMessageLn(string.Format(" #{0}: {1}({2}) {3}", new object[] { _parkFriendsList.Count + 1, friend.Name, friend.Id, friend.Full ? "��λ��" : "" }));
                    }
                    if (empty)
                        this._parkEmptyGarageFriendsList.Add(friend);
                    else
                        this._parkFriendsList.Add(friend);
                }
            }
        }
        #endregion

        #region GetCars
        public void GetMyCarsByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetMyCars));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetMyCars()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_MYKAIXIN;

                SetMessageLn("ˢ��[�ҵ�����]...");
                //login
                if (!this.ValidationLogin())
                {
                    if (MyCarFetched != null)
                        MyCarFetched(_carList, _parkcash, _carprice);
                    return;
                }

                //read cars           
                string content = RequestParkHomePage();
                this.ReadCars(content, true);
                SetMessageLn("[�ҵ�����]��Ϣˢ�³ɹ���");

                //invoke event
                if (MyCarFetched != null)
                    MyCarFetched(_carList, _parkcash, _carprice);
            });
            base.ExecuteTryCatchBlock(th, "[�ҵ�����]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region ReadCars
        private void ReadCars(string content, bool printMessage)
        {
            if (printMessage)
                SetMessageLn("��ȡ������Ϣ:");
            string strMyCars = JsonHelper.GetMid(content, "v_userdata = ", "\n");
            if (strMyCars != null)
            {
                this._seatList.Clear();
                this._carList.Clear();

                JsonTextParser parser = new JsonTextParser();
                JsonObjectCollection objects = parser.Parse(strMyCars) as JsonObjectCollection;

                //�ҵ���Ϣ
                JsonObjectCollection myself = objects["user"] as JsonObjectCollection;
                int uid = JsonHelper.GetIntegerValue(myself["uid"]);
                string realname = JsonHelper.GetStringValue(myself["real_name"]);
                this._parkcash = JsonHelper.GetIntegerValue(myself["cash"]);

                //�ҵĳ�λ
                JsonArrayCollection seatlist = objects["parking"] as JsonArrayCollection;
                foreach (JsonObjectCollection item in seatlist)
                {
                    SeatInfo seat = new SeatInfo();
                    seat.ParkId = JsonHelper.GetIntegerValue(item["parkid"]);
                    seat.CarId = JsonHelper.GetIntegerValue(item["carid"]);
                    seat.CarName = JsonHelper.GetStringValue(item["car_name"]);
                    seat.CarOwnerId = JsonHelper.GetIntegerValue(item["car_uid"]);
                    seat.CarOwnerName = JsonHelper.GetStringValue(item["car_real_name"]);
                    seat.CarProfit = JsonHelper.GetIntegerValue(item["car_profit"]);
                    seat.CarPrice = JsonHelper.GetIntegerValue(item["car_price"]);
                    this._seatList.Add(seat);
                }

                //�ҵ�����
                _carprice = 0;
                JsonArrayCollection carlist = objects["car"] as JsonArrayCollection;
                foreach (JsonObjectCollection item in carlist)
                {
                    CarInfo car = new CarInfo();
                    car.CarId = JsonHelper.GetIntegerValue(item["carid"]);
                    car.CarName = JsonHelper.GetStringValue(item["car_name"]);
                    car.CarPrice = JsonHelper.GetIntegerValue(item["price"]);
                    car.Profit = JsonHelper.GetIntegerValue(item["park_profit"]);
                    car.ParkingMinutes = JsonHelper.GetIntegerValue(item["park_profit"]) / JsonHelper.GetIntegerValue(item["park_moneyminute"]);
                    car.ParkUserId = JsonHelper.GetIntegerValue(item["park_uid"]);
                    car.ParkUserName = JsonHelper.GetStringValue(item["park_real_name"]);
                    car.CarColor = GetCarColor(JsonHelper.GetStringValue(item["car_logo_big"]));
                    string profitPerMin = JsonHelper.GetStringValue(item["park_moneyminute"]);
                    car.IsPosted = null == profitPerMin;
                    _carprice += JsonHelper.GetIntegerValue(item["price"]);
                    this._carList.Add(car);
                }
                if (printMessage)
                    SetMessageLn(string.Format("{0}({1}) �ֽ�:{2} ����:{3} �ܼ�:{4}", new object[] { realname, uid, _parkcash, _carprice, _parkcash + _carprice }));
            }
        }
        #endregion

        #region DisplayMyCars
        private void DisplayMyCars()
        {
            SetMessageLn("�ҵ�����ͣ����Ϣ:");
            for (int i = 0; i < this._carList.Count; i++)
            {
                if (this._carList[i].IsPosted)
                {
                    SetMessageLn(string.Format(" #{0}:{1}, Ŀǰû�г�λ", i + 1, this._carList[i].CarName));
                }
                else
                {
                    SetMessageLn(string.Format(" #{0}:{1}, Ŀǰͣ�� {2} ��˽�ҳ�λ ����{3}Ԫ", i + 1, this._carList[i].CarName, this._carList[i].ParkUserName, this._carList[i].Profit));
                }
            }
        }
        #endregion

        #region RunPark
        public void RunPark()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_PARKING;
                SetMessageLn("��ʼ����λ...");

                //for (int ix = 0; ix < 20; ix++)
                //{
                //    HH.DelayedTime = Constants.DELAY_1SECONDS;
                //    HH.Get("http://www.kaixin001.com/interface/regcreatepng.php?randnum=0.02962231445649477_1238760471986");
                //}

                //return;
                string content = RequestParkHomePage();
                this._parkAcc = GetAccCode(content);
                this.ReadCars(content, false);
                content = RequestParkFriends(false);
                this.ReadParkFriends(content, false, false);
                this.GetValidParkList();

                //if (Task.BuyNewCars)
                //{
                //    this.BuyNewCars();

                //    if (Task.ParkMyCars || Task.PostOthersCars || Task.UpgradeGarage)
                //    {
                //        content = RequestParkHomePage();
                //        this._parkAcc = GetAccCode(content);
                //        this.ReadCars(content, false);
                //        content = RequestParkFriends(false);
                //        this.ReadParkFriends(content, false, false);
                //        this.GetValidParkList();
                //    }
                //}

                if (Task.ParkMyCars)
                {
                    this.DisplayMyCars();
                    this.ParkCars();
                }
                if (Task.PostOthersCars)
                    this.PostCars();
                //if (Task.UpgradeGarage)
                //    this.UpgradeGarage();
                if (Task.JoinMatch || Task.OriginateMatch || Task.StartCar || Task.CheerUp)
                {
                    content = RequestCompetition();
                    if (Task.JoinMatch)
                    {
                        if (this.JoinMatch(content) == true)
                            content = RequestCompetition();
                    }
                    if (Task.OriginateMatch)
                    {
                        if (this.OriginateMatch(content) == true)
                            content = RequestCompetition();
                    }
                    if (Task.StartCar)
                        this.StartMyCar(content);
                    if (Task.CheerUp)
                        this.CheerUpTeam(content);
                }                

                SetMessageLn("����λ��ɣ�");
            });
            base.ExecuteTryCatchBlock(th, "�����쳣������λʧ�ܣ�");            
        }
        #endregion

        #region ParkCars
        private void ParkCars()
        {
            try
            {
                SetMessageLn("��ʼͣ��:");

                int num = 0;

                foreach (CarInfo car in this._carList)
                {
                    if (((car.ParkingMinutes < 15) && (car.Profit <= 300)) && !car.IsPosted)
                    {
                        SetMessageLn(string.Format("#{0}:{1}(����:{2}Ԫ) [����](����15����)", new object[] { ++num, car.CarName, car.Profit }));
                        continue;
                    }

                    Collection<int> parkableGarage = GetParkableList();

                    if (parkableGarage.Count <= 0)
                    {
                        LogHelper.Write(CurrentAccount.UserName, "��λ�����ˣ��޷�ͣ����", LogSeverity.Warn);
                        SetMessage("��λ�����ˣ��޷�ͣ����");
                        return;
                    }
                    SetMessageLn(string.Format(" #{0}:{1}(����:{2}Ԫ)", new object[] { ++num, car.CarName, car.Profit }));
                    if (this.ParkTheCar(car.CarId, car.ParkUserId, parkableGarage))
                    {
                        //ԭ���ĳ�λ�ճ�����
                        FriendInfo friend = this.GetParkFriendById(car.ParkUserId);
                        if (friend != null)
                        {
                            friend.Full = false;
                        }
                        this._parkcash += car.Profit;
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
                LogHelper.Write("GamePark.ParkCars", ex, LogSeverity.Error);
                SetMessage(" ͣ��ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region ParkTheCar
        private bool ParkTheCar(int carId, int currentPuid, Collection<int> garageList)
        {
            try
            {
                foreach (int puid in garageList)
                {
                    //ͬһ��λ�ò���ͣ
                    if (currentPuid == puid)
                    {
                        continue;
                    }
                    FriendInfo friend = GetParkFriendById(puid);
                    if (friend == null)
                        return false;

                    SetMessage(string.Format("=>{0}({1})", friend.Name, puid));
                    string param = string.Format("verify={0}&puid={1}&_=", this._verifyCode, puid);
                    string text = string.Empty;
                    HH.DelayedTime = Constants.DELAY_2SECONDS;
                    if (friend.IsNeighbor)
                    {
                        text = HH.Post("http://www.kaixin001.com/parking/neighbor.php", "http://www.kaixin001.com/app/app.php?aid=1040", param);
                    }
                    else
                    {
                        text = HH.Post("http://www.kaixin001.com/parking/user.php", "http://www.kaixin001.com/app/app.php?aid=1040", param);
                    }
                    JsonTextParser parser = new JsonTextParser();
                    JsonObjectCollection objects;
                    try
                    {
                        objects = parser.Parse(text) as JsonObjectCollection;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GamePark.ParkTheCar", "text��" + text, ex, LogSeverity.Error);
                        throw;
                    }
                    JsonArrayCollection arrays = objects["parking"] as JsonArrayCollection;
                    JsonObjectCollection owner = objects["user"] as JsonObjectCollection;
                    string stringValue = JsonHelper.GetStringValue(owner["real_name"]);
                    foreach (JsonObjectCollection garage in arrays)
                    {
                        int carid = JsonHelper.GetIntegerValue(garage["carid"]);
                        int parkId = JsonHelper.GetIntegerValue(garage["parkid"]);
                        if (this.CanParkHere(carid, parkId))
                        {
                            //20090216
                            //HH.DelayedTime = Constants.DELAY_1SECONDS;
                            //HH.Get("http://www.kaixin001.com/parking/selcar.php?verify=" + this._verifyCode);
                            string postData = string.Format("verify={0}&park_uid={1}&parkid={2}&carid={3}&neighbor={4}&acc={5}&first_fee_parking=0&_=", new object[] { this._verifyCode, puid, parkId, carId, friend.IsNeighbor ? "1" : "0", _parkAcc });
                            HH.DelayedTime = Constants.DELAY_4SECONDS;
                            string retcontent = HH.Post("http://www.kaixin001.com/parking/park.php", "http://www.kaixin001.com/app/app.php?aid=1040", postData);
                            //20090216
                            //param = string.Format("verify={0}&puid={1}&_=", this._verifyCode, puid);
                            //if (friend.IsNeighbor)
                            //{
                            //    HH.Post("http://www.kaixin001.com/parking/neighbor.php", "http://www.kaixin001.com/app/app.php?aid=1040", param);
                            //}
                            //else
                            //{
                            //    HH.Post("http://www.kaixin001.com/parking/user.php", "http://www.kaixin001.com/app/app.php?aid=1040", param);
                            //}

                            JsonTextParser retparser = new JsonTextParser();
                            JsonObjectCollection retobjects;
                            try
                            {
                                retobjects = retparser.Parse(retcontent) as JsonObjectCollection;
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Write("GamePark.ParkTheCar", "retcontent��" + retcontent, ex, LogSeverity.Error);
                                throw;
                            }
                            SetMessage(JsonHelper.GetStringValue(retobjects["error"]));
                            if (JsonHelper.GetIntegerValue(retobjects["errno"]) == 0)
                            {
                                return true;
                            }
                        }
                    }

                    //���û��ĳ�λ����
                    friend.Full = true;

                }
                SetMessage(" δ�ҵ���λ��");
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
                LogHelper.Write("GamePark.ParkTheCar", GetFriendNameById(currentPuid), ex, LogSeverity.Error);
                SetMessage(" ��" + GetFriendNameById(currentPuid) + "��ͣ��ʧ�ܣ�����" + ex.Message);
                return false;
            }
        }
        #endregion

        #region PostCars
        private void PostCars()
        {
            try
            {
                SetMessageLn("��ʼ����:");
                int num = 0;
                string content = "";
                foreach (SeatInfo seat in this._seatList)
                {
                    if (seat.CarId <= 0)
                    {
                        SetMessageLn(string.Format(" �ҵĳ�λ#{0}: ����", ++num));
                    }
                    else
                    {
                        SetMessageLn(string.Format(" �ҵĳ�λ#{0}: {1} �� {2} (����{3}Ԫ) ", new object[] { ++num, seat.CarOwnerName, seat.CarName, seat.CarProfit }));
                        if (Operation.PostAll || (!Operation.PostAll && Operation.PostList.Contains(seat.CarOwnerId)))
                        {
                            HH.DelayedTime = Constants.DELAY_4SECONDS;
                            content = HH.Post("http://www.kaixin001.com/parking/post.php", "http://www.kaixin001.com/app/app.php?aid=1040", string.Format("verify={0}&parkid={1}&acc={2}&_=", this._verifyCode, seat.ParkId, _parkAcc));
                            JsonTextParser parser = new JsonTextParser();
                            JsonObjectCollection jsonobj = parser.Parse(content) as JsonObjectCollection;
                            int errno = JsonHelper.GetInteger(JsonHelper.GetStringValue(jsonobj["errno"]));
                            string error = JsonHelper.GetStringValue(jsonobj["error"]);
                            if (errno == 0)
                                SetMessage(error);
                        }
                    }
                }
                SetMessageLn("������ɣ�");
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
                LogHelper.Write("GamePark.PostCars", ex, LogSeverity.Error);
                SetMessage(" ����ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region GetAllCarsInMarket
        public void GetAllCarsInMarketByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(GetAllCarsInMarket));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void GetAllCarsInMarket()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_UPDATEDATA;
                SetMessageLn("ˢ��[�����ϵ�����]...");

                if (!this.ValidationLogin())
                {
                    if (AllCarsInMarketFetched != null)
                        AllCarsInMarketFetched(_allCarsInMarketList);
                    return;
                }

                ReadAllCarsInMarket(true);
                SetMessageLn("[�����ϵ�����]��Ϣˢ�³ɹ���");

                //invoke event
                if (AllCarsInMarketFetched != null)
                    AllCarsInMarketFetched(_allCarsInMarketList);

            });
            base.ExecuteTryCatchBlock(th, "[�����ϵ�����]��Ϣˢ��ʧ�ܣ�");
        }
        #endregion

        #region ReadAllCarsInMarket
        private void ReadAllCarsInMarket(bool printMessage)
        {
            if (printMessage)
                SetMessageLn("��ȡ�����ϵ�������Ϣ...");

            this._allCarsInMarketList.Clear();

            int page = 0;
            do
            {
                int num;
                HH.DelayedTime = Constants.DELAY_1SECONDS;
                string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1040&url=market.php&start=" + page);
                content = JsonHelper.GetMid(content, "<table class=\"cheshi\">", "<br />");
                if (content != null)
                {
                    if (printMessage)
                        SetMessageLn("��" + (page / 8 + 1).ToString() + "ҳ");

                    for (string info = JsonHelper.GetMid(content, "<td width=\"25%\">", "</div></div></td>", out num); info != null; info = JsonHelper.GetMid(content, "<td width=\"25%\">", "</div></div></td>", out num))
                    {
                        content = content.Substring(num);
                        NewCarInfo car = new NewCarInfo();
                        car.CarId = JsonHelper.GetMidInteger(info, "purchase(", ")");
                        if (car.CarId == -1)
                            car.CarId = JsonHelper.GetMidInteger(info, "purchase_vip(", ",");
                        car.CarName = JsonHelper.GetMid(info, "align=\"absmiddle\" /> ", "</div>");
                        car.CarPrice = JsonHelper.GetMidInteger(info, "�۸�", "Ԫ");
                        if (car.CarId != 0 && car.CarName != string.Empty && car.CarPrice != -1)
                        {
                            this._allCarsInMarketList.Add(car);
                            if (printMessage)
                                SetMessageLn(car.ToString());
                        }
                    }
                    page += 8;
                }
                else
                {
                    return;
                }
            }
            while (true);
        }
        #endregion
                        
        #region GetValidParkList
        private void GetValidParkList()
        {
            this._validParkList.Clear();

            if (_parkFriendsList.Count <= 0)
                return;

            Collection<int> leftList = new Collection<int>();

            foreach (FriendInfo friend in _parkFriendsList)
            {
                if (Operation.ParkWhiteList.Contains(friend.Id) && !Operation.ParkBlackList.Contains(friend.Id))
                    _validParkList.Add(friend.Id);
                else
                    leftList.Add(friend.Id);
            }

            foreach (int userid in leftList)
            {
                if (!Operation.ParkBlackList.Contains(userid))
                    _validParkList.Add(userid);
            }
        }
        #endregion

        #region GetParkableList
        private Collection<int> GetParkableList()
        {
            Collection<int> parkablelist = new Collection<int>();
            foreach (int uid in this._validParkList)
            {
                if (uid > 0)
                {
                    FriendInfo friend = this.GetParkFriendById(uid);
                    if ((friend != null) && !friend.Full)
                    {
                        parkablelist.Add(friend.Id);
                    }
                }
            }
            return parkablelist;
        }
        #endregion

        #region JoinMatch
        private bool JoinMatch(string content)
        {
            try
            {
                SetMessageLn("�μӱ���:");

                if (this._matchstatus == MatchStatus.WithoutTeam)
                    SetMessage("�㻹û���齨�Լ��ĳ��ӣ�");
                if (this._matchstatus == MatchStatus.OriginateMatch)
                    SetMessage("���Լ������˱������޷��μ�����������");
                if (this._matchstatus == MatchStatus.InMatch)
                    SetMessage("���Ѿ��ڱ������ˣ�");

                if (this._matchstatus != MatchStatus.NotInMatch)
                    return false;

                // cheerup for friends
                content = JsonHelper.GetMid(content, "<div id=\"orimatchlist\" style=\"display:none;\" class=\"mb10\">", "<script type=\"text/javascript\">");
                int num = 0;
                int index = 0;
                //<div class="l" style="width:80px;"><u><a class=sl href="javascript:matchJoin(6209015,3178099);">��������</a></u></div>
                for (string info = JsonHelper.GetMid(content, "<a class=sl href=\"javascript:matchJoin(", ");\">��", out num); info != null; info = JsonHelper.GetMid(content, "<a class=sl href=\"javascript:matchJoin(", ");\">��", out num))
                {
                    SetMessageLn(string.Format("#{0}: ", ++index));
                    string matchinfo = JsonHelper.GetMid(content, "<div class=\"l\" style=\"width:350px;\">", "<div class=\"l\" style=\"width:80px;\">");
                    SetMessage(JsonHelper.FiltrateHtmlTags(matchinfo));
                    content = content.Substring(num);
                    string[] match = info.Split(',');
                    if (match.Length == 2)
                    {
                        string query = "";
                        //string freeback = HH.GetRedirect(string.Format("http://www.kaixin001.com/parking/matchjoin.php?verify={0}&matchuid={1}&matchid={2}", this._verifyCode, match[0], match[1]), ref query);
                        HH.DelayedTime = Constants.DELAY_2SECONDS;
                        string freeback = HH.Post("http://www.kaixin001.com/parking/matchjoin.php?", ref query, string.Format("verify={0}&matchuid={1}&matchid={2}", this._verifyCode, match[0], match[1]));

                        if (query != null && query.Contains("action="))
                        {
                            freeback = HH.Get(string.Format("http://www.kaixin001.com/parking/matchjoin.php{0}", query));
                        }
                        GetJoinMatchFeedback(freeback);
                        if (freeback.IndexOf("��μ��˱���") > -1)
                        {
                            break;
                        }
                    }
                }
                if (index == 0)
                {
                    SetMessage("û���κη���ı�����");
                    return false;
                }

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
                LogHelper.Write("GamePark.JoinMatch", content, ex, LogSeverity.Error);
                SetMessage(" �μӱ���ʧ�ܣ�����" + ex.Message);
                return false;
            }
        }
        #endregion

        #region GetJoinMatchFeedback
        private void GetJoinMatchFeedback(string content)
        {
            try
            {
                //<div id=action6 style="display:block">
                //<div class="f14 tac" style="padding:150px 0 0 0;">
                //    <div class="l" style="padding-left:60px;"><img src="http://img.kaixin001.com.cn/i2/notice.gif" alt="����" /></div>
                //    <div class="l" style="padding-left:10px;width:20em;"><strong>��ĳ���Ŀǰ������������������˲��ܲμ����������</strong></div>
                //    <div class="c"></div>
                //</div>
                if (content == null || content == string.Empty)
                    return;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "style=\"display:block\">", "</strong></div>");
                if (strdivid != null)
                {
                    strmsg = JsonHelper.GetMid(content, "style=\"display:block\">", "</strong></div>");
                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));

                    SetMessage(" " + strmsg);
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
                LogHelper.Write("GamePark.GetJoinMatchFeedback", content, ex, LogSeverity.Error);                
            }
        }
        #endregion

        #region OriginateMatch
        private bool OriginateMatch(string content)
        {
            try
            {
                SetMessageLn("�������:");

                if (this._matchstatus == MatchStatus.WithoutTeam)
                    SetMessage("�㻹û���齨�Լ��ĳ��ӣ�");
                if (this._matchstatus == MatchStatus.OriginateMatch)
                    SetMessage("���Ѿ������˱�����");
                if (this._matchstatus == MatchStatus.InMatch)
                    SetMessage("���Ѿ��ڱ������ˣ�");
                if (this._matchstatus != MatchStatus.NotInMatch)
                    return false;

                //<a href="javascript:matchOrigin(4482521);" class="sl">�������</a>
                string teamid = JsonHelper.GetMid(content, "<a href=\"javascript:matchOrigin(", ");\" class=\"sl\">�������</a>");
                if (teamid == null)
                {
                    SetMessage("�㻹û���齨���ӻ��Ѿ��ڱ������ˣ�");
                    return false;
                }

                SetMessage(string.Format("���Է���{0}����{1}...", Task.OriginateTeamNum, GetMatchShortNameById(Task.OriginateMatchId)));
                HH.DelayedTime = Constants.DELAY_2SECONDS;
                HH.Post("http://www.kaixin001.com/parking/matchorigin.php?", string.Format("verify={0}&teamid={1}&lineid={2}&maxteamnum={3}", this._verifyCode, teamid, Task.OriginateMatchId, Task.OriginateTeamNum));
                SetMessage("�ɹ���");

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
                LogHelper.Write("GamePark.OriginateMatch", content, ex, LogSeverity.Error);
                SetMessage(" �������ʧ�ܣ�����" + ex.Message);
                return false;
            }
        }
        #endregion

        #region StartMyCar
        private void StartMyCar(string content)
        {
            try
            {
                SetMessageLn("��������:");

                if (this._matchstatus == MatchStatus.WithoutTeam)
                    SetMessage("�㻹û���齨�Լ��ĳ��ӣ�");
                if (this._matchstatus == MatchStatus.OriginateMatch)
                    SetMessage("���Ѿ������˱���������û���˲μӣ�");
                if (this._matchstatus == MatchStatus.NotInMatch)
                    SetMessage("�㻹δ�μ��κα�����");

                if (this._matchstatus != MatchStatus.InMatch)
                    return;

                //my match
                //<a href="javascript:matchDetail(6194153,3160921);" class="sl">�鿴��������</a>
                string MyDetails = JsonHelper.GetMid(content, "<a href=\"javascript:matchDetail(", ");\" class=\"sl\">�鿴��������</a>");
                if (MyDetails == null)
                {
                    SetMessage("��û�вμ��κα�����");
                    return;
                }

                //2588258,464509
                string[] myMatch = MyDetails.Split(',');
                if (myMatch.Length == 2)
                {
                    HH.DelayedTime = Constants.DELAY_1SECONDS;
                    content = HH.Get("http://www.kaixin001.com/~parking/match.php?matchuid=" + myMatch[0] + "&matchid=" + myMatch[1]);
                    //<a href="javascript:teamBegin(6195212,4464953);" class="qd">��������</a>
                    string teamInfo = JsonHelper.GetMid(content, "<a href=\"javascript:teamBegin(", ");\" class=\"qd\">��������");
                    if (teamInfo == null)
                    {
                        SetMessage("���������Ѿ��������ˣ�");
                        return;
                    }

                    if (CanStartCar(content) == false)
                        return;
                    SetMessageLn("��ĳ��ӽ��컹û����������������...");
                    string[] team = teamInfo.Split(',');
                    if (team != null && team.Length == 2)
                    {
                        string query = "";
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        string freeback = HH.Get(string.Format("http://www.kaixin001.com/parking/teambegin.php?verify={0}&teamuid={1}&teamid={2}", this._verifyCode, team[0], team[1]), ref query);
                        if (query != null && query.Contains("action="))
                        {
                            freeback = HH.Get(string.Format("http://www.kaixin001.com/parking/teambegin.php{0}", query));
                        }
                        string strdivid = JsonHelper.GetMid(freeback, "style=\"display:block\">", "<div class=\"c\">");
                        if (strdivid != null)
                        {
                            string strmsg = JsonHelper.GetMid(freeback, "style=\"display:block\">", "<div class=\"c\">");
                            int index = strmsg.IndexOf(">");
                            strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));

                            SetMessage(" " + strmsg);
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
                LogHelper.Write("GamePark.StartMyCar", content, ex, LogSeverity.Error);
                SetMessage(" ��������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region CanStartCar
        private bool CanStartCar(string content)
        {
            try
            {
                if (System.DateTime.Now.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Task.StartCarTime.Hour, Task.StartCarTime.Minute, 0)) < 0)
                {
                    //<ul>
                    // <h4>�ҵ�������</h4>
                    // <li><span style="width:170px;"><a href="javascript:team('2112870','ͯҰ');">ͯҰ</a>�������� (+5km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">10:46</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('5723148','������');">������</a>�������� (+5km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">10:15</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('2588258','ׯ��-johnny');">ׯ��-joh</a>�������� (+1km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">01:50</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('11485935','������');">������</a>�������� (+3km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">00:12</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('2089107','�����');">�����</a>�������� (+2km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">01��29��</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('11485935','������');">������</a>�������� (+1km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">01��29��</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('8785314','����');">����</a>�������� (+5km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">01��29��</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('2617660','֣����');">֣����</a>�������� (+1km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">01��29��</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('2346870','������');">������</a>�������� (+5km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">01��29��</em><br></li>
                    // <li><span style="width:170px;"><a href="javascript:team('2588258','ׯ��-johnny');">ׯ��-joh</a>�������� (+1km/h)</span><em style="font-family:Arial;font-size:10px;width:50px;">01��29��</em><br></li>
                    //</ul>
                    string strCheerList = JsonHelper.GetMid(content, "�ҵ�������", "</ul>");
                    int num = 0;
                    int index = 0;
                    int count = 0;
                    for (string info = JsonHelper.GetMid(strCheerList, "<li><span style=\"width:170px;\">", "<br></li>", out num); info != null; info = JsonHelper.GetMid(strCheerList, "<li><span style=\"width:170px;\">", "<br></li>", out num))
                    {
                        SetMessageLn(string.Format("#{0}: ", ++index));
                        string printinfo = JsonHelper.FiltrateHtmlTags(info);
                        SetMessage(printinfo);
                        strCheerList = strCheerList.Substring(num);
                        if (printinfo.IndexOf(":") > -1)
                        {
                            count++;
                        }
                    }
                    if (count < 10)
                    {
                        SetMessageLn("������Ҽ��͵Ĵ���������10�Σ��ݲ�������");
                        return false;
                    }
                }
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
                LogHelper.Write("GamePark.CanStartCar", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region CheerUpTeam
        private void CheerUpTeam(string content)
        {
            try
            {
                SetMessageLn("����������:");

                // cheerup for friends
                content = JsonHelper.GetMid(content, "<div id=\"matchinglist\" style=\"display:none;\">", "<script type=\"text/javascript\">");
                int num = 0;
                int index = 0;
                for (string info = JsonHelper.GetMid(content, "title=\"�ҿ���ȥ����\" ><a class=\"sl\" href=\"javascript:matchDetail(", ");\"><img src=\"http://img1.kaixin001.com.cn/i2/park/oilgun.gif", out num); info != null; info = JsonHelper.GetMid(content, "title=\"�ҿ���ȥ����\" ><a class=\"sl\" href=\"javascript:matchDetail(", ");\"><img src=\"http://img1.kaixin001.com.cn/i2/park/oilgun.gif", out num))
                {
                    try
                    {
                        SetMessageLn(string.Format("#{0}: ", ++index));
                        content = content.Substring(num);
                        string[] match = info.Split(',');
                        if (match.Length == 2)
                        {
                            HH.DelayedTime = Constants.DELAY_1SECONDS;
                            string content2 = HH.Get("http://www.kaixin001.com/!parking/match.php?matchuid=" + match[0] + "&matchid=" + match[1]);
                            GetCompetition(content2);

                            //first team                    
                            CheerUpTheFriend(content2, match[0], match[1]);

                            int num2 = content2.IndexOf("<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ico_vs.gif\" style=\"margin:80px 4px 0\" class=\"l\"/>");
                            content2 = content2.Substring(num2);
                            CheerUpTheFriend(content2, match[0], match[1]);

                            //���˱���
                            num2 = content2.IndexOf("<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ico_vs.gif\" style=\"margin:80px 4px 0\" class=\"l\"/>", 20);
                            if (num2 > -1)
                            {
                                content2 = content2.Substring(num2);
                                CheerUpTheFriend(content2, match[0], match[1]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GamePark.CheerUpTeam", ex, LogSeverity.Error);
                        continue;
                    }
                }

                SetMessageLn("������������ɣ�");
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
                LogHelper.Write("GamePark.CheerUpTeam", content, ex, LogSeverity.Error);
                SetMessageLn("����������ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region CheerUpTheFriend
        private void CheerUpTheFriend(string content, string matchuid, string matchid)
        {
            try
            {
                string strTeam = JsonHelper.GetMid(content, "<div class=\"c3 mt5 ml15\" ><b>", "��</b></div>");

                string teamInfo = JsonHelper.GetMid(content, "<a href=\"javascript:oil(", " , 1);\" class=\"jy\">����");
                if (teamInfo != null && strTeam != null)
                {
                    string[] team = teamInfo.Split(',');
                    if (team != null && team.Length == 2)
                    {
                        if (!base.IsAlreadyMyFriend(team[0]))
                            return;
                        SetMessageLn(string.Format("��{0}����...", strTeam));
                        //SetMessageLn(string.Format("http://www.kaixin001.com/parking/teamoil.php?verify={0}&teamuid={1}&teamid={2}&type=1&matchuid={3}&matchid={4}", this._verifyCode, team[0], team[1], matchuid, matchid));
                        string query = "";
                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        string freeback = HH.Get(string.Format("http://www.kaixin001.com/parking/teamoil.php?verify={0}&teamuid={1}&teamid={2}&type=1&matchuid={3}&matchid={4}", this._verifyCode, team[0], team[1], matchuid, matchid), ref query);
                        if (query != null && query.Contains("action="))
                        {
                            freeback = HH.Get(string.Format("http://www.kaixin001.com/parking/teamoil.php{0}", query));
                        }
                        GetCheerUpFeedback(freeback);
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
                LogHelper.Write("GamePark.CheerUpTheFriend", content, ex, LogSeverity.Error);
                SetMessageLn("��" + GetFriendNameById(matchuid) + "����ʧ�ܣ�����" + ex.Message);
            }
        }
        private void GetCompetition(string content)
        {
            //2����
            //<div class="l"><b><img src="http://img.kaixin001.com.cn/i2/park/match/ico_v.gif" width="17" height="19" align="absmiddle" /> �ԻԵı���Z4���� <img src="http://img.kaixin001.com.cn/i2/park/match/ico_vs.gif" align="absmiddle" /> �򿭻Եı���730����</b><span class="c9">( ȫ�����������ܳ�3506km)</span></div>
            //3����
            //<div class="l"><b><img src="http://img.kaixin001.com.cn/i2/park/match/ico_v.gif" width="17" height="19" align="absmiddle" /> ���ʵ�S-MAX���� <img src="http://img.kaixin001.com.cn/i2/park/match/ico_vs.gif" align="absmiddle" /> �ν���S-MAX���� <img src="http://img.kaixin001.com.cn/i2/park/match/ico_vs.gif" align="absmiddle" /> ��С�˵�S-MAX����</b><span class="c9">( ȫ�����������ܳ�3506km)</span></div>
            string team1 = JsonHelper.GetMid(content, "<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ico_v.gif\" width=\"17\" height=\"19\" align=\"absmiddle\" />", "<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ico_vs.gif\" align=\"absmiddle\" />");
            string team2 = JsonHelper.GetMid(content, "<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ico_vs.gif\" align=\"absmiddle\" />", "<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ico_vs.gif\" align=\"absmiddle\" />");
            string team3 = JsonHelper.GetMidLast(content, "<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ico_vs.gif\" align=\"absmiddle\" />", "</span></div>");

            if (team2 != null)
                SetMessage(JsonHelper.FiltrateHtmlTags(String.Concat(team1, " VS ", team2, "VS", team3)));
            else
                SetMessage(JsonHelper.FiltrateHtmlTags(String.Concat(team1, " VS ", team3)));
        }

        private void GetCheerUpFeedback(string content)
        {
            try
            {
                //<div id=action9 style="display:block">
                //<div class="f14 tac" style="padding-top:150px;">
                //    <div class="l" style="padding-left:60px;"><img src="http://img.kaixin001.com.cn/i2/notice.gif" alt="����" /></div>
                //    <div class="l" style="padding-left:10px;width:20em;"><strong>
                //        <div style="text-indent:2em;">�����Ǳ����ĵ�0�죬�����ܸ����Ӽ��ͻ򵷹�</div>
                //        <div style="text-indent:2em;">�����쿪ʼ��ÿ���00:00��������ʻ��������ڼ䣬�㶼���Ը��ó��Ӽ��ͻ򵷹�</div>
                //    </strong></div>
                //    <div class="c"></div>
                //</div>
                if (content == null || content == string.Empty)
                    return;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "style=\"display:block\">", "</strong></div>");
                if (strdivid != null)
                {
                    strmsg = JsonHelper.GetMid(content, "style=\"display:block\">", "</strong></div>");
                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));

                    SetMessage(" " + strmsg);
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
                LogHelper.Write("GamePark.GetCheerUpFeedback", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region GetParkFriendById
        private FriendInfo GetParkFriendById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            foreach (FriendInfo info in this._parkFriendsList)
            {
                if (info.Id == id)
                {
                    return info;
                }
            }
            return null;
        }
        #endregion

        #region GetCarColor
        private CarColor GetCarColor(string imageurl)
        {
            if (imageurl == null || imageurl == string.Empty)
                return CarColor.Red;
            //int start = imageurl.LastIndexOf("-");
            //int end = imageurl.LastIndexOf(".");
            string image = JsonHelper.GetMidLast(imageurl, "-", ".");
            if (image == null)
                return CarColor.Red;

            switch (image)
            {
                case "16777215":
                    return CarColor.White;
                case "13421772":
                    return CarColor.Silver;
                case "0":
                    return CarColor.Black;
                case "16711680":
                    return CarColor.Red;
                case "255":
                    return CarColor.Blue;
                case "16776960":
                    return CarColor.Yellow;
                default:
                    return CarColor.Red;
            }

        }
        #endregion

        #region CanParkHere
        private bool CanParkHere(int carId, int parkId)
        {
            //(parkId >> 0x10) & 0xff == 0   �շѳ�λ
            return ((carId == 0 || carId == -1) && ((parkId >> 0x10) & 0xff) == 0);
        }
        #endregion

        #region GetMatchShortNameById
        private string GetMatchShortNameById(int matchid)
        {
            foreach (MatchInfo match in this._matchList)
            {
                if (match.MatchId == matchid)
                {
                    return match.ShortName;
                }
            }
            return matchid.ToString();
        }
        #endregion

        #region Tool of Build Team

        #region BuildTeam
        public void BuildTeam(AccountInfo account, NewCarInfo modelcar, int carcount, ExchangeCar exchange, Collection<NewCarInfo> carsInMarket)
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {             
                _module = Constants.MSG_BUILDTEAM;
                _outofmoney = false;
                //if (modelcar.CarPrice < 200000)
                //{
                //    SetMessageLn("������Ϸ���򣬲����齨�������۵���200000�ĳ��ӣ�");
                //    OnBuildTeamFinished();
                //    return;
                //}

                _allCarsInMarketList = carsInMarket;

                SetMessageLn("��ʼ�齨����...");

                if (!this.ValidationLogin(account))
                {
                    if (BuildTeamFinished != null)
                        BuildTeamFinished(null, 0, 0);
                    return;
                }

                string content = RequestParkHomePage();
                this.ReadCars(content, false);

                Collection<NewCarInfo> buildableCars = FindNeedsBuildCars(modelcar);
                if (buildableCars.Count <= 0)
                {
                    SetMessageLn("���Ѿ�ӵ��6���ÿ�͵������������ٻ�����");
                }
                else
                {
                    int ix = 0;
                    int index = 0;
                    foreach (NewCarInfo newcar in buildableCars)
                    {
                        if (this._outofmoney)
                            break;

                        if (carcount > _carList.Count)
                        {
                            SetMessageLn(string.Format("��Ҫ�齨�ĳ���#{0}({1}):{2}({3},{4}) ", ++index, (carcount > _carList.Count) ? "����" : "����", newcar.CarName, GetCarColor(newcar.CarColor), newcar.CarPrice));
                            BuyTheCar(newcar);
                        }
                        else
                        {
                            if (exchange == ExchangeCar.Stop)
                            {
                                SetMessageLn("�ﵽ�������������ֹͣ������");
                                break;
                            }

                            SetMessageLn(string.Format("��Ҫ�齨�ĳ���#{0}({1}):{2}({3},{4}) ", ++index, (carcount > _carList.Count) ? "����" : "����", newcar.CarName, GetCarColor(newcar.CarColor), newcar.CarPrice));

                            _carList = SortCarsList();
                            if (exchange == ExchangeCar.Expensive)
                            {
                                for (ix = _carList.Count - 1; ix >= 0; ix--)
                                {
                                    if (this._outofmoney || UpgradeTheCar(_carList[ix], newcar))
                                        break;
                                }
                            }
                            else
                            {
                                for (ix = 0; ix < _carList.Count; ix++)
                                {
                                    if (this._outofmoney || UpgradeTheCar(_carList[ix], newcar))
                                        break;
                                }
                            }
                        }
                    }
                }

                content = RequestParkHomePage();
                this.ReadCars(content, false);

                SetMessageLn("�齨������ɣ�");
                if (BuildTeamFinished != null)
                    BuildTeamFinished(_carList, _parkcash, _carprice);
            });
            base.ExecuteTryCatchBlock(th, "�����쳣���齨����ʧ�ܣ�");
        }
        #endregion

        #region FindNeedsBuildCars
        private Collection<NewCarInfo> FindNeedsBuildCars(NewCarInfo newcar)
        {
            Collection<NewCarInfo> newcars = new Collection<NewCarInfo>();
            for (int ix = 0; ix < 6; ix++)
            {
                NewCarInfo car = new NewCarInfo();
                car.CarId = newcar.CarId;
                car.CarName = newcar.CarName;
                car.CarPrice = newcar.CarPrice;
                switch (ix)
                {
                    case 0:
                        car.CarColor = CarColor.White;
                        break;
                    case 1:
                        car.CarColor = CarColor.Silver;
                        break;
                    case 2:
                        car.CarColor = CarColor.Black;
                        break;
                    case 3:
                        car.CarColor = CarColor.Red;
                        break;
                    case 4:
                        car.CarColor = CarColor.Blue;
                        break;
                    case 5:
                        car.CarColor = CarColor.Yellow;
                        break;
                }

                if (!IsExistInMyCarList(car))
                    newcars.Add(car);
            }

            return newcars;
        }
        #endregion

        #region IsExistInMyCarList
        private bool IsExistInMyCarList(NewCarInfo newcar)
        {
            foreach (CarInfo car in _carList)
            {
                if (newcar.CarName == car.CarName && newcar.CarColor == car.CarColor)
                    return true;
            }
            return false;
        }
        #endregion

        #region BuyTheCar
        private bool BuyTheCar(NewCarInfo newcar)
        {
            try
            {
                string query = "";
                HH.DelayedTime = Constants.DELAY_1SECONDS;                
                //HH.Post("http://www.kaixin001.com/!parking/purchase.php", ref query, string.Format("verify={0}&carid={1}&color={2}", this._verifyCode, newcar.CarId, DataConvert.GetInt32(newcar.CarColor)));
                //string content = HH.Get(string.Format("http://www.kaixin001.com/parking/purchase.php{0}", query));
                string content = HH.Post("http://www.kaixin001.com/!parking/purchase.php", ref query, string.Format("verify={0}&carid={1}&color={2}", this._verifyCode, newcar.CarId, DataConvert.GetInt32(newcar.CarColor)));
                //<script language="JavaScript">
                //window.location="/!parking/!purchase.php?action=2&carid=28&color=16776960";
                //</script>
                string navigationurl = JsonHelper.GetMid(content, "window.location=\"/", "\";");
                content = HH.Get(string.Format("http://www.kaixin001.com/{0}", navigationurl));
                if (PrintBuyFeedback(content))
                {
                    _parkcash -= newcar.CarPrice;
                    _carprice += newcar.CarPrice;
                    CarInfo oldcar = new CarInfo();
                    oldcar.CarId = newcar.CarId;
                    oldcar.CarName = newcar.CarName;
                    oldcar.CarPrice = newcar.CarPrice;
                    oldcar.CarColor = newcar.CarColor;
                    _carList.Add(oldcar);
                    return true;
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
                LogHelper.Write("GamePark.BuyTheCar", ex, LogSeverity.Error);
                SetMessage("�����³�ʧ�ܣ�����" + ex.Message);
                return false;
            }
        }

        private bool PrintBuyFeedback(string content)
        {
            try
            {
                /*-----------------Success----------------------------------*/
                //<div id=action1 style="display:none">
                //<div class="f14 tac" style="margin-top:150px;"><strong>�㹺����һ������</strong></div>
                //<div style="padding:30px 166px;">

                //    <div class="rbs1"><input type="button" id="btn_sc" value="ȷ��"  class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';" onclick="new parent.dialog().reset();"  style="padding:5px 20px;" /></div>
                //</div>
                //</div>
                /*-----------------Failed----------------------------------*/
                //<div id=action1 style="display:block">
                //<div class="h100">&nbsp;</div><div class="h50">&nbsp;</div>
                //<div class="f14 tac">
                //    <div class="l" style="padding-left:80px;"><img src="http://img.kaixin001.com.cn/i2/notice.gif" alt="����" /></div>
                //    <div class="l" style="padding-left:10px;width:20em;"><strong>���ֽ��㣬�޷�������</strong></div>
                //    <div class="c"></div>
                //</div>
                //<div style="padding:30px 166px;">

                //    <div class="rbs1"><input type="button" id="btn_sc" value="ȷ��"  class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';" onclick="new parent.dialog().reset();"  style="padding:5px 20px;" /></div>
                //</div>
                //</div>
                bool ret = false;
                string strmsg = JsonHelper.GetMid(content, "style=\"display:block\">", "</strong></div>");
                if (strmsg != null)
                {
                    if (strmsg.IndexOf("�㹺����һ��") < 0)
                        ret = false;
                    else
                        ret = true;

                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));

                    SetMessage(" " + strmsg);
                    if (strmsg.IndexOf("���ֽ���") != -1)
                        this._outofmoney = true;
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
                LogHelper.Write("GamePark.PrintBuyFeedback", content, ex, LogSeverity.Error);
                return false;
            }
        }
        #endregion

        #region GetCarColor
        private string GetCarColor(CarColor color)
        {
            switch (color)
            {
                case CarColor.Black:
                    return "��ɫ";
                case CarColor.Blue:
                    return "��ɫ";
                case CarColor.Red:
                    return "��ɫ";
                case CarColor.Silver:
                    return "��ɫ";
                case CarColor.White:
                    return "��ɫ";
                case CarColor.Yellow:
                    return "��ɫ";
                default:
                    return "δ֪ɫ";
            }
        }
        #endregion

        #region SortCarsList
        private Collection<CarInfo> SortCarsList()
        {
            for (int ix = 0; ix < _carList.Count; ix++)
            {
                for (int iy = ix + 1; iy < _carList.Count; iy++)
                {
                    if (_carList[ix].CarPrice > _carList[iy].CarPrice)
                    {
                        CarInfo temp = _carList[ix];
                        _carList[ix] = _carList[iy];
                        _carList[iy] = temp;
                    }
                }
            }

            return _carList;
        }
        #endregion

        #region UpgradeTheCar
        private bool UpgradeTheCar(CarInfo oldcar, NewCarInfo newcar)
        {
            try
            {
                if (oldcar.CarName == newcar.CarName)
                    return false;

                SetMessageLn(string.Format("=>������������:{0}({1})", oldcar.CarName, oldcar.CarPrice));
                string param = string.Format("verify={0}&carid={1}&color={2}&oldcarid={3}&interval={4}", new object[] { this._verifyCode, newcar.CarId, DataConvert.GetInt32(newcar.CarColor), oldcar.CarId, oldcar.CarPrice - newcar.CarPrice });
                string query = "";
                HH.DelayedTime = Constants.DELAY_2SECONDS;
                HH.Post("http://www.kaixin001.com/parking/updatecar.php", ref query, param);
                string content = HH.Get(string.Format("http://www.kaixin001.com/parking/updatecar.php{0}", query));
                if (GetBuildTeamFeedback(content))
                {
                    _parkcash -= newcar.CarPrice - oldcar.CarPrice;
                    _carprice += newcar.CarPrice - oldcar.CarPrice;
                    oldcar.CarId = newcar.CarId;
                    oldcar.CarName = newcar.CarName;
                    oldcar.CarPrice = newcar.CarPrice;
                    return true;
                }
                if (query.Contains("action=3"))
                    this._outofmoney = true;
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
                LogHelper.Write("GamePark.UpgradeTheCar", ex, LogSeverity.Error);
                SetMessage("��������ʧ�ܣ�����" + ex.Message);
                return false;
            }
        }
        #endregion

        #region GetBuildTeamFeedback
        private bool GetBuildTeamFeedback(string content)
        {
            try
            {
                /*-----------------Success----------------------------------*/
                //<div id=action2 style="display:block">
                //<div class="f14 tac" style="margin-top:150px;"><strong>�㻻����һ������С���ӣ��һ����ֽ�96000Ԫ</strong></div>
                //<div style="padding:30px 166px;">

                //    <div class="rbs1"><input type="button" id="btn_sc" value="ȷ��"  class="rb1-12" onmouseover="this.className='rb2-12';" onmouseout="this.className='rb1-12';" onclick="new parent.dialog().reset();"  style="padding:5px 20px;" /></div>
                //</div>
                //</div>

                /*-----------------Fail----------------------------------*/
                //<div id=action3 style="display:block">

                //<div class="h100">&nbsp;</div><div class="h50">&nbsp;</div>
                //<div class="f14 tac">
                //    <div class="l" style="padding-left:80px;"><img src="http://img.kaixin001.com.cn/i2/notice.gif" alt="����" /></div>
                //    <div class="l" style="padding-left:10px;width:20em;"><strong>���ֽ��㣬�޷����򲼼ӵ�����</strong></div>
                //    <div class="c"></div>
                //</div>
                bool ret = false;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "style=\"display:block\">", "</strong></div>");
                if (strdivid != null)
                {
                    if (strdivid.IndexOf("�㻻����һ��") < 0)
                        ret = false;
                    else
                        ret = true;

                    strmsg = JsonHelper.GetMid(content, "style=\"display:block\">", "</strong></div>");
                    int index = strmsg.IndexOf(">");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg.Substring(index + 1));

                    SetMessage(" " + strmsg);
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
                LogHelper.Write("GamePark.GetBuildTeamFeedback", content, ex, LogSeverity.Error);               
                return false;
            }
        }
        #endregion

        #region GetBuildableCars
        public Collection<NewCarInfo> GetBuildableCars(int parkcash, int allcarprice, Collection<NewCarInfo> carsInMarket)
        {
            this._module = Constants.MSG_BUILDTEAM;

            Collection<NewCarInfo> validcarlist = new Collection<NewCarInfo>();

            int summoney = parkcash + allcarprice;
            int averageprice = summoney / 6;

            foreach (NewCarInfo newcar in carsInMarket)
            {
                if (newcar.CarPrice < 200000)
                    continue;

                if (newcar.CarPrice <= averageprice)
                {
                    validcarlist.Add(newcar);
                    //SetMessageLn(newcar.ToString());
                }
                else
                    break;
            }
            if (validcarlist.Count <= 0)
                SetMessageLn("����ʲ���������κγ��ӣ�");

            return validcarlist;
        }
        #endregion

        #endregion

        #region Tool of BuyCards
        protected void BuyCards(Collection<AccountInfo> accounts, CardInfo card, int count)
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_BUYCARDS;

                SetMessageLn("��ʼ�������...");

                //start
                SetMessage("\r\n" + "============================== ��ʼ ==============================");

                int num = 0;
                foreach (AccountInfo account in accounts)
                {
                    try
                    {
                        num++;
                        SetMessageLn("------ ��" + accounts.Count + "���ʻ�����" + num + "���ʻ���" + account.UserName + "(" + account.Email + ") ------");

                        if (!this.ValidationLogin(account))
                        {
                            continue;
                        }

                        string content = RequestParkHomePage();
                        this.ReadCars(content, false);

                        for (int ix = 0; ix < count; ix++)
                        {
                            //this._parkcash
                            if (this._parkcash < card.Price)
                            {
                                SetMessage("��û���㹻���ֽ�");
                                break;
                            }
                            else
                            {
                                BuyTheCard(card);
                                this._parkcash -= card.Price;
                            }
                        }

                        this.LogOut(true);
                    }
                    catch (ThreadAbortException)
                    {
                        LogHelper.Write("GamePark.BuyCards", account.UserName, LogSeverity.Info);
                    }
                    catch (ThreadInterruptedException)
                    {
                        LogHelper.Write("GamePark.BuyCards", account.UserName, LogSeverity.Info);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GamePark.BuyCards", account.UserName, ex, LogSeverity.Error);
                        SetMessageLn("�����쳣�����˻�����ʧ�ܣ�����" + ex.Message);
                    }
                }

                SetMessage("\r\n" + "============================== ��� ==============================");                
                if (BuyCardsFinished != null)
                    BuyCardsFinished();
            });
            base.ExecuteTryCatchBlock(th, "�����쳣���������ʧ�ܣ�");
        }

        private bool BuyTheCard(CardInfo card)
        {
            string param = string.Format("verify={0}&cardid={1}", this._verifyCode, card.CardId);
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            HH.Post("http://www.kaixin001.com/parking/buycard.php", param);
            SetMessageLn("��ɹ�������1��" + card.CardName + "��");
            return true;
        }
        #endregion

        #region Tool of UpgradeGarage

        protected void UpgradeGarage(Collection<AccountInfo> accounts, bool upgrade, bool buycars, int maxcars, int allmaxcars, Collection<NewCarInfo> carsInMarket, Collection<int> blackbuylist, bool cheapest)
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_TOOLPARK;
                _outofmoney = false;
                _carcount = 0;

                if (!upgrade && !buycars)
                    return;

                //start
                SetMessage("\r\n" + "============================== ��ʼ ==============================");

                int num = 0;
                foreach (AccountInfo account in accounts)
                {
                    try
                    {
                        num++;
                        SetMessageLn("------ ��" + accounts.Count + "���ʻ�����" + num + "���ʻ���" + account.UserName + "(" + account.Email + ") ------");
                        if (!this.ValidationLogin(account))
                        {
                            continue;
                        }

                        string content = RequestParkHomePage();
                        this.ReadCars(content, true);

                        if (upgrade)
                            UpgradeTheGarage();

                        if (buycars)
                        {
                            if (_carcount >= allmaxcars)
                            {
                                SetMessageLn("���������˺Ź����������ޣ�" + allmaxcars.ToString() + "��ֹͣ����");
                                if (!upgrade)
                                    break;                                
                            }
                            else
                            {
                                BuyNewCars(maxcars, allmaxcars, carsInMarket, blackbuylist, cheapest);
                            }
                        }

                        this.LogOut(true);
                    }
                    catch (ThreadAbortException)
                    {
                        LogHelper.Write("GamePark.UpgradeGarage", account.UserName, LogSeverity.Info);
                    }
                    catch (ThreadInterruptedException)
                    {
                        LogHelper.Write("GamePark.UpgradeGarage", account.UserName, LogSeverity.Info);
                    }
                    catch(Exception ex)
                    {
                        LogHelper.Write("GamePark.UpgradeGarage", account.UserName, ex, LogSeverity.Error);
                        SetMessageLn("�����쳣�����˻�����ʧ�ܣ�����" + ex.Message);
                        continue;
                    }
                }

                SetMessage("\r\n" + "============================== ��� ==============================");

                SetMessageLn("����λ������ɣ�");
                if (ToolParkFinished != null)
                    ToolParkFinished();
               
            });
            base.ExecuteTryCatchBlock(th, "�����쳣������λ����ʧ�ܣ�");
        }

        #region UpgradeTheGarage
        private void UpgradeTheGarage()
        {
            try
            {
                SetMessageLn("��ʼ������ѳ�λ:");
                //����Ƿ�����ѳ�λ
                int num = 0;
                foreach (SeatInfo seat in _seatList)
                {
                    if (IsFreeSeat(seat.ParkId))
                        break;
                    else
                        num++;
                }
                if (num >= 4)
                {
                    SetMessageLn("��ĳ�����û����ѳ�λ��Ҫ������");
                    return;
                }

                //�ֽ��Ƿ��㹻
                if (_parkcash > 30000)
                {
                    num = 0;
                    foreach (SeatInfo seat in _seatList)
                    {
                        bool isFree = false;
                        string msg = BuildSeatInfo(seat, ref isFree);
                        SetMessageLn(string.Format(msg, ++num));
                        if (isFree)
                        {
                            if (BuyUseCard(15))
                            {
                                _parkcash -= 30000;
                            }
                        }
                    }
                }
                else
                {
                    SetMessageLn("���ֽ��㣬�޷�����λ�������");
                }
                SetMessageLn("������ѳ�λ��ɣ�");
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
                LogHelper.Write("GamePark.UpgradeTheGarage", ex, LogSeverity.Error);
                SetMessageLn("������ѳ�λʧ�ܣ�����" + ex.Message);
            }
        }

        private string BuildSeatInfo(SeatInfo seat, ref bool isFree)
        {
            string message;
            if (IsFreeSeat(seat.ParkId))
            {
                message = " �ҵĳ�λ#{0}: ��ѳ�λ ";
                isFree = true;
            }
            else
            {
                message = " �ҵĳ�λ#{0}: �շѳ�λ ";
                isFree = false;
            }

            if (seat.CarId <= 0)
            {
                message += "����";
            }
            else
            {
                message += string.Format("{0} �� {1} (����{2}Ԫ) ", new object[] { seat.CarOwnerName, seat.CarName, seat.CarProfit });
            }
            return message;
        }

        private bool IsFreeSeat(int parkid)
        {
            //1000002  �շѳ�λ
            //2000003  �շѳ�λ
            //3010000  ��ѳ�λ
            //4000005  �շѳ�λ
            return (((parkid >> 0x10) & 0xff) == 1);
        }

        private bool BuyUseCard(int cardId)
        {
            string param = string.Format("verify={0}&cardid={1}", this._verifyCode, cardId);
            HH.DelayedTime = Constants.DELAY_2SECONDS;
            HH.Post("http://www.kaixin001.com/parking/buycard.php", param);
            SetMessage("=>��ɹ�������1�ų�λ�������");
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            HH.Post("http://www.kaixin001.com/parking/card_resetpark.php", param);
            SetMessage("=>��ѳ�λ����Ϊ�շѳ�λ�ɹ������� 30000 Ԫ��");
            return true;
        }
        #endregion

        #region BuyNewCars
        private void BuyNewCars(int maxcars, int allmaxcars, Collection<NewCarInfo> carsInMarket, Collection<int> blackbuylist, bool cheapest)
        {
            try
            {
                if (cheapest)
                    SetMessageLn("��ʼ������˵��³�...");
                else
                    SetMessageLn("��ʼ�������³�...");

                if (this._carList.Count >= maxcars)
                {
                    SetMessageLn(string.Format("������������Ѿ��ﵽ����{0}", maxcars));
                    return;
                }

                int num = 0;
                if (cheapest)
                {                    
                    foreach (NewCarInfo car in carsInMarket)
                    {
                        if (num + this._carList.Count >= maxcars)
                        {
                            SetMessageLn(string.Format("������������Ѿ��ﵽ����{0}", maxcars));
                            break;
                        }

                        if (_carcount >= allmaxcars)
                            break;

                        if (!CanBuyTheCar(car, blackbuylist))
                            continue;

                        SetMessageLn(car.ToString());

                        if (this._parkcash < car.CarPrice)
                        {
                            SetMessageLn("����ֽ��㣬���ܹ����³���");
                            break;
                        }
                        if (BuyTheCar(car))
                        {
                            _carcount++;
                            num++;
                        }
                    }
                }
                else
                {
                    for (int ix = carsInMarket.Count - 1; ix >= 0; ix--)
                    {
                        if (num + this._carList.Count >= maxcars)
                        {
                            SetMessageLn(string.Format("������������Ѿ��ﵽ����{0}", maxcars));
                            break;
                        }

                        if (_carcount >= allmaxcars)
                            break;

                        if (!CanBuyTheCar(carsInMarket[ix], blackbuylist))
                            continue;

                        SetMessageLn("=>");
                        SetMessage(carsInMarket[ix].ToString());

                        if (this._parkcash < 16000)
                        {
                            SetMessageLn("����ֽ��㣬���ܹ����³���");
                            break;
                        }

                        if (this._parkcash < carsInMarket[ix].CarPrice)
                        {
                            SetMessage(" ->�ֽ��㣬����");
                            continue;
                        }
                        if (BuyTheCar(carsInMarket[ix]))
                        {
                            _carcount++;
                            num++;
                        }
                    }
                }
                SetMessageLn("�����³���ɣ�");
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
                LogHelper.Write("GamePark.BuyNewCars", ex, LogSeverity.Error);
                SetMessageLn("��ͼ��³�ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region  CanBuyTheCar
        private bool CanBuyTheCar(NewCarInfo newcar, Collection<int> blackbuylist)
        {
            if (!newcar.IsValid)
                return false;

            if (blackbuylist.Contains(newcar.CarId))
                return false;

            foreach (CarInfo car in _carList)
            {
                if (newcar.CarName == car.CarName)
                    return false;
            }

            return true;
        }
        #endregion
        
        #endregion

        #region Request
        private string RequestParkHomePage()
        {
            string content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1040");

            if (content.IndexOf("<title>������ - ������</title>") != -1)
            {
                SetMessageLn("��δ��װ����λ���,���԰�װ��...");
                HH.Post("http://www.kaixin001.com/app/install.php", "aid=1040&isinstall=1");
                content = HH.Get("http://www.kaixin001.com/app/app.php?aid=1040");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            return content;
        }
        private string RequestCompetition()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            string content = HH.Get("http://www.kaixin001.com/!parking/myteam.php");
            string imageID = JsonHelper.GetMid(content, "<img src=\"http://img1.kaixin001.com.cn/i2/park/match/ts_", ".gif");
            if (imageID == "3")
                this._matchstatus = MatchStatus.NotInMatch;
            else if (imageID == "2")
                this._matchstatus = MatchStatus.OriginateMatch;
            else if (imageID == "1")
                this._matchstatus = MatchStatus.InMatch;
            else if (content.IndexOf("�㻹û���齨��ĳ���") > -1)
                this._matchstatus = MatchStatus.WithoutTeam;

            return content;
        }

        private string RequestParkFriends(bool free)
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            if (free)
                return HH.Get("http://www.kaixin001.com/parking/getfriendsdata.php?verify=" + this._verifyCode + "&freechk=1");
            else
                return HH.Get("http://www.kaixin001.com/parking/getfriendsdata.php?verify=" + this._verifyCode + "&freechk=0");

            //string content = HH.Get(string.Format("http://www.kaixin001.com/!house/!garden//getconf.php?verify={0}&fuid={1}&r=0.7394348406232893", verifyCode, fuid));
            ////if (!String.IsNullOrEmpty(content))
            ////{
            ////    ConfigCtrl.SaveXmlStringToFile(content, CurrentAccount.UserName);
            ////}
            //return content;
        }
        #endregion

        #region Properties
        public Collection<FriendInfo> ParkFriendsList
        {
            get { return _parkFriendsList; }
        }

        public Collection<FriendInfo> ParkEmptyGarageFriendsList
        {
            get { return _parkEmptyGarageFriendsList; }
        }

        public Collection<CarInfo> MyCarList
        {
            get { return _carList; }
        }

        public Collection<MatchInfo> MatchesList
        {
            get { return _matchList; }
            set { _matchList = value; }
        }
        #endregion
    }
}
