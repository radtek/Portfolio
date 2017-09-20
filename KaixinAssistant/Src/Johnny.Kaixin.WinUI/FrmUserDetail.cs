using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;
using System.Collections;

using Johnny.Kaixin.Helper;
using System.Net.Json;
using System.Runtime.InteropServices;

using Johnny.Kaixin.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmUserDetail : FrmBaseCloseMenu
    {
        #region Varibles
        private string _group;
        private AccountInfo _myaccount;

        public Collection<FriendInfo> _allmyfriend; //我所有的好友
        public Collection<FriendInfo> _allparkeruser;   //所有争车位的好友
        public Collection<FriendInfo> _emptygarageuser;   //目前有空车位的好友
        public Collection<FriendInfo> _bitableuser;  //所有可咬的好友
        public Collection<FriendInfo> _recoverableuser; //所有我可以休息的好友
        public Collection<FriendInfo> _buyableuser;    //所有我能买的奴隶
        private Collection<FriendInfo> _myslaves;  //我的所有的奴隶
        private Collection<CarInfo> _mycars; //我的所有的汽车
       
        //house
        public Collection<FriendInfo> _allhousefriends; //所有买房子的好友
        public Collection<FriendInfo> _samevillagefriends; //住在同小区里的好友
        public Collection<FriendInfo> _freefriends; //住在同小区里的好友
        //garden
        public Collection<FriendInfo> _allgardenfriends; //所有花园的好友
        public Collection<FriendInfo> _maturefriends; //花园中有成熟果实的好友
        //ranch
        public Collection<FriendInfo> _allranchfriends; //所有牧场的好友
        public Collection<FriendInfo> _agriculturalproductfriends; //牧场中有成熟农副产品的好友

        //fish
        public Collection<FriendInfo> _allfishfriends; //所有钓鱼的好友

        //cafe
        public Collection<FriendInfo> _allcafefriends; //所有钓鱼的好友
        public Collection<FriendInfo> _hirablefriends; //所有钓鱼的好友

        private WhiteBlackCore _wbCore;
        #endregion

        #region ctor
        public FrmUserDetail()
        {
            InitializeComponent();            
        }
        #endregion

        #region Form_Load
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {                
                txtEmail.Text = _myaccount.Email;
                txtUserName.Text = _myaccount.UserName;
                txtUserId.Text = _myaccount.UserId;
                txtGroup.Text = _group;
                txtLog.Clear();
                this._allmyfriend = new Collection<FriendInfo>();
                this._allparkeruser = new Collection<FriendInfo>();
                this._emptygarageuser = new Collection<FriendInfo>();
                this._bitableuser = new Collection<FriendInfo>();
                this._recoverableuser = new Collection<FriendInfo>();
                this._buyableuser = new Collection<FriendInfo>();
                this._myslaves = new Collection<FriendInfo>();
                this._mycars = new Collection<CarInfo>();

                this._allhousefriends = new Collection<FriendInfo>();
                this._samevillagefriends = new Collection<FriendInfo>();
                this._freefriends = new Collection<FriendInfo>();
                this._allgardenfriends = new Collection<FriendInfo>();
                this._maturefriends = new Collection<FriendInfo>();

                this._allranchfriends = new Collection<FriendInfo>();
                this._agriculturalproductfriends = new Collection<FriendInfo>();

                this._allfishfriends = new Collection<FriendInfo>();

                this._allcafefriends = new Collection<FriendInfo>();
                this._hirablefriends = new Collection<FriendInfo>();

                this.Cursor = Cursors.WaitCursor;
                SetControlStatus(Constants.STATUS_FORMLOAD);
                SetControlStatus(Constants.STATUS_CONNECTING);
                
                _wbCore = new WhiteBlackCore(_group, _myaccount);
                _wbCore.MessageChanged += new KaixinBase.MessageChangedEventHandler(_wbCore_MessageChanged);
                _wbCore.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_wbCore_ValidateCodeNeeded);
                _wbCore.OperationFailed += new KaixinBase.OperationFailedEventHandler(_wbCore_OperationFailed);
                _wbCore.InitializationFailed += new WhiteBlackCore.InitializationFailedEventHandler(_wbCore_InitializationFailed);
                _wbCore.InitializationFinished += new WhiteBlackCore.InitializationFinishedEventHandler(_wbCore_InitializationFinished);
                _wbCore.AllMyFriendsFetched += new KaixinBase.AllMyFriendsFetchedEventHandler(_wbCore_AllMyFriendsFetched);
                _wbCore.Park.MyCarFetched += new GamePark.MyCarFetchedEventHandler(Park_MyCarFetched);
                _wbCore.Slave.MySlaveFetched += new GameSlave.MySlaveFetchedEventHandler(Slave_MySlaveFetched);
                _wbCore.OperationFinished += new WhiteBlackCore.OperationFinishedEventHandler(_wbCore_OperationFinished);
                _wbCore.InitializeByThread();

            }
            catch (Exception ex)
            {
                SetControlStatus(Constants.STATUS_LOGINFAILED);
                Program.ShowMessageBox("FrmUserDetail", ex);
                this.Cursor = Cursors.Arrow;
            }
        }

        void _wbCore_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_wbCore_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "配置黑白名单";
                if (picCode.ShowDialog() == DialogResult.OK)
                    _wbCore.ValidationCode = picCode.ValidationCode;
                else
                    _wbCore.ValidationCode = null;
            }
        }

        void _wbCore_InitializationFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new WhiteBlackCore.InitializationFinishedEventHandler(_wbCore_InitializationFinished), new object[] { });
            }
            else
            {
                SetControlStatus(Constants.STATUS_AFTERLOGIN);

                _allmyfriend = _wbCore.AllMyFriendsList;
                _allparkeruser = _wbCore.Park.ParkFriendsList;
                _emptygarageuser = _wbCore.Park.ParkEmptyGarageFriendsList;
                _mycars = _wbCore.Park.MyCarList;
                _bitableuser = _wbCore.Bite.BitableFriendsList;
                _recoverableuser = _wbCore.Bite.RestableFriendsList;
                _buyableuser = _wbCore.Slave.BuyableSlaveList;
                _myslaves = _wbCore.Slave.MySlaveList;

                _allhousefriends = _wbCore.House.AllHouseFriendsList;
                _samevillagefriends = _wbCore.House.SameVillageFriendsList;
                _freefriends = _wbCore.House.FreeFriendsList;
                _allgardenfriends = _wbCore.Garden.AllGardenFriendsList;
                _maturefriends = _wbCore.Garden.MatureFriendsList;

                _allranchfriends = _wbCore.Ranch.AllRanchFriendsList;
                _agriculturalproductfriends = _wbCore.Ranch.AgriculturalProductFriendsList;

                _allfishfriends = _wbCore.Fish.AllFishFriendsList;
                _allcafefriends = _wbCore.Cafe.AllCafeFriendsList;
                _hirablefriends = _wbCore.Cafe.HirableFriendsList;

                lstCars.Items.Clear();
                foreach (CarInfo car in _mycars)
                {
                    lstCars.Items.Add(car.CarName);
                }
                lstSlaves.Items.Clear();
                foreach (FriendInfo slave in _myslaves)
                {
                    lstSlaves.Items.Add(slave.Name + "(" + slave.Id.ToString() + ")--身价:" + slave.Price);
                }

                lstAllMyFriends.Items.Clear();
                cmbProtectId.Items.Clear();
                cmbProtectId.Items.Add("--请选择--");
                cmbProtectId.SelectedIndex = 0;
                foreach (FriendInfo friend in _allmyfriend)
                {
                    lstAllMyFriends.Items.Add(friend.Name + "(" + friend.Id.ToString() + ")");
                    cmbProtectId.Items.Add(friend.Name + "(" + friend.Id.ToString() + ")");
                    if (friend.Id == _wbCore.Operation.ProtectId)
                        cmbProtectId.SelectedItem = cmbProtectId.Items[cmbProtectId.Items.Count - 1];
                }                
                //park
                BuildListBox(lstParkWhite, _wbCore.Operation.ParkWhiteList);
                BuildListBox(lstParkBlack, _wbCore.Operation.ParkBlackList);
                BuildListBox(lstPostList, _wbCore.Operation.PostList);
                chkPostOnlyWhite.Checked = !_wbCore.Operation.PostAll;
                //bite
                BuildListBox(lstBiteWhite, _wbCore.Operation.BiteWhiteList);
                BuildListBox(lstBiteBlack, _wbCore.Operation.BiteBlackList);
                BuildListBox(lstRecoverWhite, _wbCore.Operation.RecoverWhiteList);
                BuildListBox(lstRecoverBlack, _wbCore.Operation.RecoverBlackList);
                chbBiteOnlyWhite.Checked = !_wbCore.Operation.BiteAll;
                //slave
                BuildListBox(lstBuyWhite, _wbCore.Operation.BuyWhiteList);
                BuildListBox(lstBuyBlack, _wbCore.Operation.BuyBlackList);
                //house
                BuildListBox(lstStayWhite, _wbCore.Operation.StayWhiteList);
                BuildListBox(lstStayBlack, _wbCore.Operation.StayBlackList);
                BuildListBox(lstRobWhite, _wbCore.Operation.RobWhiteList);
                BuildListBox(lstRobBlack, _wbCore.Operation.RobBlackList);                
                //garden
                BuildListBox(lstStealWhite, _wbCore.Operation.StealWhiteList);
                BuildListBox(lstStealBlack, _wbCore.Operation.StealBlackList);
                BuildListBox(lstFarmWhite, _wbCore.Operation.FarmWhiteList);
                BuildListBox(lstFarmBlack, _wbCore.Operation.FarmBlackList);
                chkStealOnlyWhite.Checked = !_wbCore.Operation.StealAll;
                chkFarmOnlyWhite.Checked = !_wbCore.Operation.FarmAll;

                cmbPresentId.Items.Clear();
                cmbPresentId.Items.Add("--请选择--");
                cmbPresentId.SelectedIndex = 0;
                foreach (FriendInfo friend in _allgardenfriends)
                {
                    cmbPresentId.Items.Add(friend.Name + "(" + friend.Id.ToString() + ")");
                    if (friend.Id == _wbCore.Operation.PresentId)
                        cmbPresentId.SelectedItem = cmbPresentId.Items[cmbPresentId.Items.Count - 1];
                }

                //ranch
                BuildListBox(lstStealProductWhite, _wbCore.Operation.StealProductWhiteList);
                BuildListBox(lstStealProductBlack, _wbCore.Operation.StealProductBlackList);
                BuildListBox(lstHelpRanchWhite, _wbCore.Operation.HelpRanchWhiteList);
                BuildListBox(lstHelpRanchBlack, _wbCore.Operation.HelpRanchBlackList);
                chkStealProductOnlyWhite.Checked = !_wbCore.Operation.StealProductAll;
                chkHelpRanchOnlyWhite.Checked = !_wbCore.Operation.HelpRanchAll;

                cmbPresentProductId.Items.Clear();
                cmbPresentProductId.Items.Add("--请选择--");
                cmbPresentProductId.SelectedIndex = 0;
                foreach (FriendInfo friend in _allgardenfriends)
                {
                    cmbPresentProductId.Items.Add(friend.Name + "(" + friend.Id.ToString() + ")");
                    if (friend.Id == _wbCore.Operation.PresentProductId)
                        cmbPresentProductId.SelectedItem = cmbPresentProductId.Items[cmbPresentProductId.Items.Count - 1];
                }

                //fish
                BuildListBox(lstFishingWhite, _wbCore.Operation.FishingWhiteList);
                BuildListBox(lstFishingBlack, _wbCore.Operation.FishingBlackList);
                BuildListBox(lstHelpFishWhite, _wbCore.Operation.HelpFishWhiteList);
                BuildListBox(lstHelpFishBlack, _wbCore.Operation.HelpFishBlackList);
                chkFishingOnlyWhite.Checked = !_wbCore.Operation.FishingAll;
                chkHelpFishOnlyWhite.Checked = !_wbCore.Operation.HelpFishAll;

                cmbPresentFishId.Items.Clear();
                cmbPresentFishId.Items.Add("--请选择--");
                cmbPresentFishId.SelectedIndex = 0;
                foreach (FriendInfo friend in _allfishfriends)
                {
                    cmbPresentFishId.Items.Add(friend.Name + "(" + friend.Id.ToString() + ")");
                    if (friend.Id == _wbCore.Operation.PresentFishId)
                        cmbPresentFishId.SelectedItem = cmbPresentFishId.Items[cmbPresentFishId.Items.Count - 1];
                }

                //cafe
                BuildListBox(lstHireWhite, _wbCore.Operation.HireWhiteList);
                BuildListBox(lstHireBlack, _wbCore.Operation.HireBlackList);
                chkHireOnlyWhite.Checked = !_wbCore.Operation.HireAll;
                BuildListBox(lstPurchaseWhite, _wbCore.Operation.PurchaseWhiteList);
                BuildListBox(lstPurchaseBlack, _wbCore.Operation.PurchaseBlackList);
                chkPurchaseOnlyWhite.Checked = !_wbCore.Operation.PurchaseAll;

                cmbPresentFoodId.Items.Clear();
                cmbPresentFoodId.Items.Add("--请选择--");
                cmbPresentFoodId.SelectedIndex = 0;
                foreach (FriendInfo friend in _allcafefriends)
                {
                    cmbPresentFoodId.Items.Add(friend.Name + "(" + friend.Id.ToString() + ")");
                    if (friend.Id == _wbCore.Operation.PresentFoodId)
                        cmbPresentFoodId.SelectedItem = cmbPresentFoodId.Items[cmbPresentFoodId.Items.Count - 1];
                }

                this.Cursor = Cursors.Arrow;
            }
        }

        void _wbCore_InitializationFailed()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new WhiteBlackCore.InitializationFailedEventHandler(_wbCore_InitializationFailed), new object[] { });
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                SetControlStatus(Constants.STATUS_LOGINFAILED);

            }
        }

        void _wbCore_OperationFailed()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.OperationFailedEventHandler(_wbCore_OperationFailed), new object[] { });
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                SetControlStatus(Constants.STATUS_TASKSTOPPED); 
            }           
        }

        void _wbCore_MessageChanged(string caption, string key, string message)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new WhiteBlackCore.MessageChangedEventHandler(_wbCore_MessageChanged), new object[] { caption, key, message });
                }
                else
                {
                    txtLog.AppendText(message);
                    txtLog.ScrollToCaret();
                }
            }
            catch
            {
                //无法访问已释放的对象
                //MessageBox.Show(ex.Message);
            }
        }


        void _wbCore_OperationFinished()
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new WhiteBlackCore.OperationFinishedEventHandler(_wbCore_OperationFinished), new object[] { });
            }
            else
            {
                //btnTaskStart.Enabled = true;
            }
        }

        private void btnInitial_Click(object sender, EventArgs e)
        {
            try
            {
                Main_Load(sender, e);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion        

        #region Form_Closing
        private void Main_FormClosing(object sender, FormClosingEventArgs e)        
        {
            try
            {
                _wbCore.StopThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion

        #region BuildListBox
        private void BuildListBox(ListBox listbox, Collection<int> selectedlist)
        {
            listbox.Items.Clear();
            foreach (int id in selectedlist)
            {
                foreach (FriendInfo friend in _allmyfriend)
                {
                    if (friend.Id.Equals(id))
                    {
                        listbox.Items.Add(friend.Name + "(" + friend.Id.ToString() + ")");
                        break;
                    }
                }
            }
        }
        #endregion

        #region 我的开心网

        #region GetAllMyFriends
        private void _wbCore_AllMyFriendsFetched(Collection<FriendInfo> allmyfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.AllMyFriendsFetchedEventHandler(_wbCore_AllMyFriendsFetched), new object[] { allmyfriends });
                }
                else
                {
                    if (allmyfriends != null && allmyfriends.Count > 0)
                    {
                        lstAllMyFriends.Items.Clear();
                        foreach (FriendInfo user in allmyfriends)
                        {
                            lstAllMyFriends.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                        }
                        _allmyfriend = allmyfriends;
                    }
                    SetControlStatus(Constants.STATUS_NORMAL);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAllMyFriends_Click(object sender, EventArgs e)
        {
            try
            {
                SetControlStatus(Constants.STATUS_PROCESSING);
                _wbCore.GetAllMyFriendsByThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion

        #region GetCars
        void Park_MyCarFetched(Collection<CarInfo> cars, int parkcash, int carprice)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GamePark.MyCarFetchedEventHandler(Park_MyCarFetched), new object[] { cars, parkcash, carprice });
                }
                else
                {
                    if (cars != null && cars.Count > 0 && parkcash > 0 && carprice > 0)
                    {
                        lstCars.Items.Clear();
                        foreach (CarInfo car in cars)
                        {
                            lstCars.Items.Add(car.CarName);
                        }
                        _mycars = cars;
                    }
                    SetControlStatus(Constants.STATUS_NORMAL);
                }

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        private void btnCars_Click(object sender, EventArgs e)
        {
            try
            {
                SetControlStatus(Constants.STATUS_PROCESSING);
                _wbCore.Park.GetMyCarsByThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion

        #region Get Slaves
        void Slave_MySlaveFetched(Collection<FriendInfo> slaves)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameSlave.MySlaveFetchedEventHandler(Slave_MySlaveFetched), new object[] { slaves });
                }
                else
                {
                    if (slaves != null && slaves.Count > 0)
                    {
                        lstSlaves.Items.Clear();
                        foreach (FriendInfo slave in slaves)
                        {
                            lstSlaves.Items.Add(slave.Name + "(" + slave.Id.ToString() + ")--身价:" + slave.Price);
                        }
                        _myslaves = slaves;
                    }
                    SetControlStatus(Constants.STATUS_NORMAL);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnSlaves_Click(object sender, EventArgs e)
        {
            try
            {
                SetControlStatus(Constants.STATUS_PROCESSING);
                _wbCore.Slave.GetMySlavesByThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion

        #endregion        

        #region 争车位

        #region Set Parker White & Black list

        private void btnAddParkWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgParkerSelection dlgparkeruser = new DlgParkerSelection();
                //dlgparkeruser.GamePark = _wbCore.Park;
                dlgparkeruser.GamePark.Clone(_wbCore, true);
                dlgparkeruser.AllMyFriend = _allparkeruser;
                dlgparkeruser.GameUser = _emptygarageuser;
                if (dlgparkeruser.ShowDialog() == DialogResult.OK)
                {
                    bool isExisted;

                    foreach (FriendInfo user in dlgparkeruser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstParkWhite.Items.Count; ix++)
                        {
                            if (lstParkWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstParkWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveParkWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstParkWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstParkWhite.Items.Remove(lstParkWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddParkBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgParkerSelection dlgparkeruser = new DlgParkerSelection();
                //dlgparkeruser.GamePark = _wbCore.Park;
                dlgparkeruser.GamePark.Clone(_wbCore, true);
                dlgparkeruser.AllMyFriend = _allparkeruser;
                dlgparkeruser.GameUser = _emptygarageuser;
                if (dlgparkeruser.ShowDialog() == DialogResult.OK)
                {
                    bool isExisted;
                    foreach (FriendInfo user in dlgparkeruser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstParkBlack.Items.Count; ix++)
                        {
                            if (lstParkBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstParkBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveParkBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstParkBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstParkBlack.Items.Remove(lstParkBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddPostList_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DlgParkerSelection dlgparkeruser = new DlgParkerSelection();
                //dlgparkeruser.GamePark = _wbCore.Park;
                dlgparkeruser.GamePark.Clone(_wbCore, true);
                dlgparkeruser.AllMyFriend = _allparkeruser;
                dlgparkeruser.GameUser = _emptygarageuser;
                if (dlgparkeruser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgparkeruser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstPostList.Items.Count; ix++)
                        {
                            if (lstPostList.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstPostList.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemovePostList_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstPostList.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstPostList.Items.Remove(lstPostList.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        #endregion

        #endregion

        #region 咬人
        
        #region Set Biter White & Black list

        private void btnAddBiteWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgBiterSelection dlgbiteruser = new DlgBiterSelection();
                //dlgbiteruser.GameBite = _wbCore.Bite;
                dlgbiteruser.GameBite.Clone(_wbCore, true);
                dlgbiteruser.GameUser = _bitableuser;
                dlgbiteruser.AllMyFriend = _allmyfriend;
                if (dlgbiteruser.ShowDialog() == DialogResult.OK)
                {
                    bool isExisted;
                    foreach (FriendInfo user in dlgbiteruser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstBiteWhite.Items.Count; ix++)
                        {
                            if (lstBiteWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstBiteWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveBiteWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstBiteWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstBiteWhite.Items.Remove(lstBiteWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddBiteBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgBiterSelection dlgbiteruser = new DlgBiterSelection();
                //dlgbiteruser.GameBite = _wbCore.Bite;
                dlgbiteruser.GameBite.Clone(_wbCore, true);
                dlgbiteruser.GameUser = _bitableuser;
                dlgbiteruser.AllMyFriend = _allmyfriend;
                if (dlgbiteruser.ShowDialog() == DialogResult.OK)
                {
                    bool isExisted;
                    foreach (FriendInfo user in dlgbiteruser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstBiteBlack.Items.Count; ix++)
                        {
                            if (lstBiteBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstBiteBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveBiteBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstBiteBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstBiteBlack.Items.Remove(lstBiteBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddRecoverWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgRecoverSelection dlgrecoveruser = new DlgRecoverSelection();
                //dlgrecoveruser.GameBite = _wbCore.Bite;
                dlgrecoveruser.GameBite.Clone(_wbCore, true);
                dlgrecoveruser.GameUser = _recoverableuser;
                dlgrecoveruser.AllMyFriend = _allmyfriend;
                if (dlgrecoveruser.ShowDialog() == DialogResult.OK)
                {
                    bool isExisted;
                    foreach (FriendInfo user in dlgrecoveruser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstRecoverWhite.Items.Count; ix++)
                        {
                            if (lstRecoverWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstRecoverWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveRecoverWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstRecoverWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstRecoverWhite.Items.Remove(lstRecoverWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddRecoverBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgRecoverSelection dlgrecoveruser = new DlgRecoverSelection();
                //dlgrecoveruser.GameBite = _wbCore.Bite;
                dlgrecoveruser.GameBite.Clone(_wbCore, true);
                dlgrecoveruser.GameUser = _recoverableuser;
                dlgrecoveruser.AllMyFriend = _allmyfriend;
                if (dlgrecoveruser.ShowDialog() == DialogResult.OK)
                {
                    bool isExisted;
                    foreach (FriendInfo user in dlgrecoveruser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstRecoverBlack.Items.Count; ix++)
                        {
                            if (lstRecoverBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstRecoverBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveRecoverBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstRecoverBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstRecoverBlack.Items.Remove(lstRecoverBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion               

        #endregion

        #region 朋友买卖
        
        #region Set Slave White & Black list
        private void btnAddBuyWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgSlaveSelection dlgslaveuser = new DlgSlaveSelection();
                //dlgslaveuser.GameSlave = _wbCore.Slave;
                dlgslaveuser.GameSlave.Clone(_wbCore, true);
                dlgslaveuser.GameUser = _buyableuser;
                dlgslaveuser.AllMyFriend = _allmyfriend;
                if (dlgslaveuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgslaveuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstBuyWhite.Items.Count; ix++)
                        {
                            if (lstBuyWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstBuyWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveBuyWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstBuyWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstBuyWhite.Items.Remove(lstBuyWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddBuyBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgSlaveSelection dlgslaveuser = new DlgSlaveSelection();
                //dlgslaveuser.GameSlave = _wbCore.Slave;
                dlgslaveuser.GameSlave.Clone(_wbCore, true);
                dlgslaveuser.GameUser = _buyableuser;
                dlgslaveuser.AllMyFriend = _allmyfriend;
                if (dlgslaveuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgslaveuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstBuyBlack.Items.Count; ix++)
                        {
                            if (lstBuyBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstBuyBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveBuyBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstBuyBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstBuyBlack.Items.Remove(lstBuyBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion

        #endregion        

        #region 买房子
                
        #region Set House White & Black list
        private void btnAddStayWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgHouseStaySelection dlghousestayuser = new DlgHouseStaySelection();
                //dlghousestayuser.GameHouse = _wbCore.House;
                dlghousestayuser.GameHouse.Clone(_wbCore, true);
                dlghousestayuser.AllMyFriend = _allhousefriends;
                dlghousestayuser.GameUser = _samevillagefriends;
                if (dlghousestayuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlghousestayuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstStayWhite.Items.Count; ix++)
                        {
                            if (lstStayWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstStayWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        private void btnRemoveStayWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstStayWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstStayWhite.Items.Remove(lstStayWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddStayBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgHouseStaySelection dlghousestayuser = new DlgHouseStaySelection();
                //dlghousestayuser.GameHouse = _wbCore.House;
                dlghousestayuser.GameHouse.Clone(_wbCore, true);
                dlghousestayuser.AllMyFriend = _allhousefriends;
                dlghousestayuser.GameUser = _samevillagefriends;
                if (dlghousestayuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlghousestayuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstStayBlack.Items.Count; ix++)
                        {
                            if (lstStayBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstStayBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveStayBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstStayBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstStayBlack.Items.Remove(lstStayBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddRobWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgHouseRobSelection dlghouserobuser = new DlgHouseRobSelection();
                //dlghouserobuser.GameHouse = _wbCore.House;
                dlghouserobuser.GameHouse.Clone(_wbCore, true);
                dlghouserobuser.AllMyFriend = _allhousefriends;
                dlghouserobuser.GameUser = _freefriends;
                if (dlghouserobuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlghouserobuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstRobWhite.Items.Count; ix++)
                        {
                            if (lstRobWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstRobWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveRobWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstRobWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstRobWhite.Items.Remove(lstRobWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddRobBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgHouseRobSelection dlghouserobuser = new DlgHouseRobSelection();
                //dlghouserobuser.GameHouse = _wbCore.House;
                dlghouserobuser.GameHouse.Clone(_wbCore, true);
                dlghouserobuser.AllMyFriend = _allhousefriends;
                dlghouserobuser.GameUser = _freefriends;
                if (dlghouserobuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlghouserobuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstRobBlack.Items.Count; ix++)
                        {
                            if (lstRobBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstRobBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveRobBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstRobBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstRobBlack.Items.Remove(lstRobBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        #endregion

        #endregion

        #region 花园
        
        #region Set Garden White & Black list
        private void btnAddStealWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgGardenSelection dlggardenuser = new DlgGardenSelection();
                //dlggardenuser.GameGarden = _wbCore.Garden;
                dlggardenuser.GameGarden.Clone(_wbCore, true);
                dlggardenuser.AllMyFriend = _allgardenfriends;
                dlggardenuser.GameUser = _maturefriends;
                if (dlggardenuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlggardenuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstStealWhite.Items.Count; ix++)
                        {
                            if (lstStealWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstStealWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveStealWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstStealWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstStealWhite.Items.Remove(lstStealWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddStealBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgGardenSelection dlggardenuser = new DlgGardenSelection();
                //dlggardenuser.GameGarden = _wbCore.Garden;
                dlggardenuser.GameGarden.Clone(_wbCore, true);
                dlggardenuser.AllMyFriend = _allgardenfriends;
                dlggardenuser.GameUser = _maturefriends;
                if (dlggardenuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlggardenuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstStealBlack.Items.Count; ix++)
                        {
                            if (lstStealBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstStealBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveStealBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstStealBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstStealBlack.Items.Remove(lstStealBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddFarmWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgGardenSelection dlggardenuser = new DlgGardenSelection();
                //dlggardenuser.GameGarden = _wbCore.Garden;
                dlggardenuser.GameGarden.Clone(_wbCore, true);
                dlggardenuser.AllMyFriend = _allgardenfriends;
                dlggardenuser.GameUser = _maturefriends;
                if (dlggardenuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlggardenuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstFarmWhite.Items.Count; ix++)
                        {
                            if (lstFarmWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstFarmWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveFarmWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstFarmWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstFarmWhite.Items.Remove(lstFarmWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnAddFarmBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgGardenSelection dlggardenuser = new DlgGardenSelection();
                //dlggardenuser.GameGarden = _wbCore.Garden;
                dlggardenuser.GameGarden.Clone(_wbCore, true);
                dlggardenuser.AllMyFriend = _allgardenfriends;
                dlggardenuser.GameUser = _maturefriends;
                if (dlggardenuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlggardenuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstFarmBlack.Items.Count; ix++)
                        {
                            if (lstFarmBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstFarmBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }

        private void btnRemoveFarmBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstFarmBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstFarmBlack.Items.Remove(lstFarmBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion

        #endregion

        #region 牧场

        #region Set Ranch White & Black list
        private void btnAddStealProductWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgRanchSelection dlgranchuser = new DlgRanchSelection();
                //dlgranchuser.GameRanch = _wbCore.Ranch;
                dlgranchuser.GameRanch.Clone(_wbCore, true);
                dlgranchuser.AllMyFriend = _allranchfriends;
                dlgranchuser.GameUser = _agriculturalproductfriends;
                if (dlgranchuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgranchuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstStealProductWhite.Items.Count; ix++)
                        {
                            if (lstStealProductWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstStealProductWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddStealProductWhite_Click", ex);
            }
        }

        private void btnRemoveStealProductWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstStealProductWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstStealProductWhite.Items.Remove(lstStealProductWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveStealProductWhite_Click", ex);
            }
        }

        private void btnAddStealProductBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgRanchSelection dlgranchuser = new DlgRanchSelection();
                //dlgranchuser.GameRanch = _wbCore.Ranch;
                dlgranchuser.GameRanch.Clone(_wbCore, true);
                dlgranchuser.AllMyFriend = _allranchfriends;
                dlgranchuser.GameUser = _agriculturalproductfriends;
                if (dlgranchuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgranchuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstStealProductBlack.Items.Count; ix++)
                        {
                            if (lstStealProductBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstStealProductBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddStealProductBlack_Click", ex);
            }
        }

        private void btnRemoveStealProductBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstStealProductBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstStealProductBlack.Items.Remove(lstStealProductBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveStealProductWhite_Click", ex);
            }
        }

        private void btnAddHelpRanchWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgRanchSelection dlgranchuser = new DlgRanchSelection();
                //dlgranchuser.GameRanch = _wbCore.Ranch;
                dlgranchuser.GameRanch.Clone(_wbCore, true);
                dlgranchuser.AllMyFriend = _allranchfriends;
                dlgranchuser.GameUser = _agriculturalproductfriends;
                if (dlgranchuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgranchuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstHelpRanchWhite.Items.Count; ix++)
                        {
                            if (lstHelpRanchWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstHelpRanchWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddHelpRanchWhite_Click", ex);
            }
        }

        private void btnRemoveHelpRanchWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstHelpRanchWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstHelpRanchWhite.Items.Remove(lstHelpRanchWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveHelpRanchWhite_Click", ex);
            }
        }

        private void btnAddHelpRanchBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgRanchSelection dlgranchuser = new DlgRanchSelection();
                //dlgranchuser.GameRanch = _wbCore.Ranch;
                dlgranchuser.GameRanch.Clone(_wbCore, true);
                dlgranchuser.AllMyFriend = _allranchfriends;
                dlgranchuser.GameUser = _agriculturalproductfriends;
                if (dlgranchuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgranchuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstHelpRanchBlack.Items.Count; ix++)
                        {
                            if (lstHelpRanchBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstHelpRanchBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddHelpRanchBlack_Click", ex);
            }
        }

        private void btnRemoveHelpRanchBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstHelpRanchBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstHelpRanchBlack.Items.Remove(lstHelpRanchBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveHelpRanchBlack_Click", ex);
            }
        }
        #endregion

        #endregion

        #region 钓鱼

        #region Set Ranch White & Black list
        private void btnAddFishingWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgFishSelection dlgfishuser = new DlgFishSelection();
                dlgfishuser.GameFish.Clone(_wbCore, true);
                dlgfishuser.AllMyFriend = _allmyfriend;
                dlgfishuser.GameUser = _allfishfriends;
                if (dlgfishuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgfishuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstFishingWhite.Items.Count; ix++)
                        {
                            if (lstFishingWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstFishingWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddFishingWhite_Click", ex);
            }
        }

        private void btnRemoveFishingWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstFishingWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstFishingWhite.Items.Remove(lstFishingWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveFishingWhite_Click", ex);
            }
        }

        private void btnAddFishingBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgFishSelection dlgfishuser = new DlgFishSelection();
                dlgfishuser.GameFish.Clone(_wbCore, true);
                dlgfishuser.AllMyFriend = _allmyfriend;
                dlgfishuser.GameUser = _allfishfriends;
                if (dlgfishuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgfishuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstFishingBlack.Items.Count; ix++)
                        {
                            if (lstFishingBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstFishingBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddFishingBlack_Click", ex);
            }
        }

        private void btnRemoveFishingBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstFishingBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstFishingBlack.Items.Remove(lstFishingBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveFishingBlack_Click", ex);
            }
        }

        private void btnAddHelpFishWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgFishSelection dlgfishuser = new DlgFishSelection();
                dlgfishuser.GameFish.Clone(_wbCore, true);
                dlgfishuser.AllMyFriend = _allmyfriend;
                dlgfishuser.GameUser = _allfishfriends;
                if (dlgfishuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgfishuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstHelpFishWhite.Items.Count; ix++)
                        {
                            if (lstHelpFishWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstHelpFishWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddHelpFishWhite_Click", ex);
            }
        }

        private void btnRemoveHelpFishWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstHelpFishWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstHelpFishWhite.Items.Remove(lstHelpFishWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveHelpFishWhite_Click", ex);
            }
        }

        private void btnAddHelpFishBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgFishSelection dlgfishuser = new DlgFishSelection();
                dlgfishuser.GameFish.Clone(_wbCore, true);
                dlgfishuser.AllMyFriend = _allmyfriend;
                dlgfishuser.GameUser = _allfishfriends;
                if (dlgfishuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgfishuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstHelpFishBlack.Items.Count; ix++)
                        {
                            if (lstHelpFishBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstHelpFishBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddHelpFishBlack_Click", ex);
            }
        }

        private void btnRemoveHelpFishBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstHelpFishBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstHelpFishBlack.Items.Remove(lstHelpFishBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveHelpFishBlack_Click", ex);
            }
        }
        #endregion

        #endregion

        #region SetControlStatus
        private void SetControlStatus(string status)
        {
            switch (status)
            { 
                case Constants.STATUS_FORMLOAD:
                    btnInitial.Enabled = false;
                    btnSaveConfig.Enabled =false;
                    tabKaixin.Enabled = false;                    
                    lstAllMyFriends.Items.Clear();
                    lstCars.Items.Clear();
                    lstSlaves.Items.Clear();
                    lstParkWhite.Items.Clear();
                    lstParkBlack.Items.Clear();
                    lstPostList.Items.Clear();
                    lstBiteWhite.Items.Clear();
                    lstBiteBlack.Items.Clear();
                    lstRecoverWhite.Items.Clear();
                    lstRecoverBlack.Items.Clear();
                    lstBuyWhite.Items.Clear();
                    lstBuyBlack.Items.Clear();                             
                    tabKaixin.SelectedIndex = 0;
                    break;
                case Constants.STATUS_CONNECTING:
                    btnInitial.Enabled = false;
                    btnSaveConfig.Enabled =false;
                    tabKaixin.Enabled = false;
                    break;
                case Constants.STATUS_LOGINFAILED:
                    btnInitial.Enabled = true;
                    btnSaveConfig.Enabled =false;
                    tabKaixin.Enabled = false;
                    break;
                case Constants.STATUS_AFTERLOGIN:
                    btnInitial.Enabled = true;
                    btnSaveConfig.Enabled = true;
                    tabKaixin.Enabled = true;
                    break;
                case Constants.STATUS_PROCESSING:
                    btnInitial.Enabled = false;
                    btnSaveConfig.Enabled = false;
                    tabKaixin.Enabled = true;
                    btnAllMyFriends.Enabled = false;
                    btnCars.Enabled = false;
                    btnSlaves.Enabled = false;
                    break;
                case Constants.STATUS_NORMAL:
                    btnInitial.Enabled = true;
                    btnSaveConfig.Enabled = true;
                    tabKaixin.Enabled = true;
                    btnAllMyFriends.Enabled = true;
                    btnCars.Enabled = true;
                    btnSlaves.Enabled = true;
                    break;
                case Constants.STATUS_TASKSTARTED:
                    btnInitial.Enabled = false;
                    break;
                case Constants.STATUS_TASKSTOPPED:
                    btnInitial.Enabled = true;
                    btnAllMyFriends.Enabled = true;
                    btnCars.Enabled = true;
                    btnSlaves.Enabled = true;
                    break;
                default:
                    break;
            }            
        }        
        #endregion                           

        #region Save Configuration
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                OperationInfo operation = new OperationInfo();
                operation.Email = _myaccount.Email;
                operation.Password = _myaccount.Password;
                               
                //park
                operation.ParkWhiteList = GetList(lstParkWhite.Items);
                operation.ParkBlackList = GetList(lstParkBlack.Items);
                operation.PostList = GetList(lstPostList.Items);
                operation.PostAll = !chkPostOnlyWhite.Checked;                
                //bite
                operation.BiteWhiteList = GetList(lstBiteWhite.Items);
                operation.BiteAll = !chbBiteOnlyWhite.Checked;
                operation.BiteBlackList = GetList(lstBiteBlack.Items);
                operation.RecoverWhiteList = GetList(lstRecoverWhite.Items);
                operation.RecoverBlackList = GetList(lstRecoverBlack.Items);
                if (cmbProtectId.Text == "--请选择--")
                    operation.ProtectId = 0;
                else
                    operation.ProtectId = DataConvert.GetInt32(JsonHelper.GetMidLast(cmbProtectId.Text, "(", ")"));
                //slave
                operation.BuyWhiteList = GetList(lstBuyWhite.Items);
                operation.BuyBlackList = GetList(lstBuyBlack.Items);
                //house
                operation.StayWhiteList = GetList(lstStayWhite.Items);
                operation.StayBlackList = GetList(lstStayBlack.Items);
                operation.RobWhiteList = GetList(lstRobWhite.Items);
                operation.RobBlackList = GetList(lstRobBlack.Items);
                //garden
                operation.StealWhiteList = GetList(lstStealWhite.Items);
                operation.StealBlackList = GetList(lstStealBlack.Items);
                operation.StealAll = !chkStealOnlyWhite.Checked;
                operation.FarmWhiteList = GetList(lstFarmWhite.Items);
                operation.FarmBlackList = GetList(lstFarmBlack.Items);
                operation.FarmAll = !chkFarmOnlyWhite.Checked;
                if (cmbPresentId.Text == "--请选择--")
                    operation.PresentId = 0;
                else
                    operation.PresentId = DataConvert.GetInt32(JsonHelper.GetMidLast(cmbPresentId.Text, "(", ")"));
                //ranch
                operation.StealProductWhiteList = GetList(lstStealProductWhite.Items);
                operation.StealProductBlackList = GetList(lstStealProductBlack.Items);
                operation.StealProductAll = !chkStealProductOnlyWhite.Checked;
                operation.HelpRanchWhiteList = GetList(lstHelpRanchWhite.Items);
                operation.HelpRanchBlackList = GetList(lstHelpRanchBlack.Items);
                operation.HelpRanchAll = !chkHelpRanchOnlyWhite.Checked;
                if (cmbPresentProductId.Text == "--请选择--")
                    operation.PresentProductId = 0;
                else
                    operation.PresentProductId = DataConvert.GetInt32(JsonHelper.GetMidLast(cmbPresentProductId.Text, "(", ")"));
                //fish
                operation.FishingWhiteList = GetList(lstFishingWhite.Items);
                operation.FishingBlackList = GetList(lstFishingBlack.Items);
                operation.FishingAll = !chkFishingOnlyWhite.Checked;
                operation.HelpFishWhiteList = GetList(lstHelpFishWhite.Items);
                operation.HelpFishBlackList = GetList(lstHelpFishBlack.Items);
                operation.HelpFishAll = !chkHelpFishOnlyWhite.Checked;
                if (cmbPresentFishId.Text == "--请选择--")
                    operation.PresentFishId = 0;
                else
                    operation.PresentFishId = DataConvert.GetInt32(JsonHelper.GetMidLast(cmbPresentFishId.Text, "(", ")"));

                //cafe
                operation.HireWhiteList = GetList(lstHireWhite.Items);
                operation.HireBlackList = GetList(lstHireBlack.Items);
                operation.HireAll= !chkHireOnlyWhite.Checked;
                operation.PurchaseWhiteList = GetList(lstPurchaseWhite.Items);
                operation.PurchaseBlackList = GetList(lstPurchaseBlack.Items);
                operation.PurchaseAll = !chkPurchaseOnlyWhite.Checked;
                if (cmbPresentFoodId.Text == "--请选择--")
                    operation.PresentFoodId = 0;
                else
                    operation.PresentFoodId = DataConvert.GetInt32(JsonHelper.GetMidLast(cmbPresentFoodId.Text, "(", ")"));

                if(ConfigCtrl.SetOperation(_group, operation))
                    MessageBox.Show("配置信息保存成功！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("配置信息保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }              
        }

        private Collection<int> GetList(ListBox.ObjectCollection items)
        {
            Collection<int> ids = new Collection<int>();
            foreach (object user in items)
            {
                ids.Add(DataConvert.GetInt32(JsonHelper.GetMidLast(user.ToString(), "(", ")")));
            }
            return ids;
        }
        #endregion
        
        #region btnStopOperation_Click
        private void btnStopOperation_Click(object sender, EventArgs e)
        {
            try
            {
                _wbCore.StopThread();

                SetControlStatus(Constants.STATUS_TASKSTOPPED);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail", ex);
            }
        }
        #endregion

        #region Properties
        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
        public AccountInfo Account
        {
            get { return _myaccount; }
            set { _myaccount = value; }
        }
        #endregion        

        #region 开心餐厅
        private void btnAddHireWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgCafeSelection dlgcafeuser = new DlgCafeSelection();
                dlgcafeuser.GameCafe.Clone(_wbCore, true);
                dlgcafeuser.AllMyFriend = _allcafefriends;
                dlgcafeuser.GameUser = _hirablefriends;
                if (dlgcafeuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgcafeuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstHireWhite.Items.Count; ix++)
                        {
                            if (lstHireWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstHireWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddHireWhite_Click", ex);
            }
        }

        private void btnRemoveHireWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstHireWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstHireWhite.Items.Remove(lstHireWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveHireWhite_Click", ex);
            }
        }

        private void btnAddHireBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgCafeSelection dlgcafeuser = new DlgCafeSelection();
                dlgcafeuser.GameCafe.Clone(_wbCore, true);
                dlgcafeuser.AllMyFriend = _allcafefriends;
                dlgcafeuser.GameUser = _hirablefriends;
                if (dlgcafeuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgcafeuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstHireBlack.Items.Count; ix++)
                        {
                            if (lstHireBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstHireBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddHireBlack_Click", ex);
            }
        }

        private void btnRemoveHireBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstHireBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstHireBlack.Items.Remove(lstHireBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemoveHireBlack_Click", ex);
            }
        }

        private void btnAddPurchaseWhite_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgCafeSelection dlgcafeuser = new DlgCafeSelection();
                dlgcafeuser.GameCafe.Clone(_wbCore, true);
                dlgcafeuser.AllMyFriend = _allcafefriends;
                dlgcafeuser.GameUser = _hirablefriends;
                if (dlgcafeuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgcafeuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstPurchaseWhite.Items.Count; ix++)
                        {
                            if (lstPurchaseWhite.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstPurchaseWhite.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddPurchaseWhite_Click", ex);
            }
        }

        private void btnRemovePurchaseWhite_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstPurchaseWhite.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstPurchaseWhite.Items.Remove(lstPurchaseWhite.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemovePurchaseWhite_Click", ex);
            }
        }

        private void btnAddPurchaseBlack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DlgCafeSelection dlgcafeuser = new DlgCafeSelection();
                dlgcafeuser.GameCafe.Clone(_wbCore, true);
                dlgcafeuser.AllMyFriend = _allcafefriends;
                dlgcafeuser.GameUser = _hirablefriends;
                if (dlgcafeuser.ShowDialog() == DialogResult.OK)
                {

                    bool isExisted;
                    foreach (FriendInfo user in dlgcafeuser.SelectedUser)
                    {
                        isExisted = false;
                        for (int ix = 0; ix < lstPurchaseBlack.Items.Count; ix++)
                        {
                            if (lstPurchaseBlack.Items[ix].ToString().IndexOf(user.Id.ToString()) > 0)
                            {
                                isExisted = true;
                                break;
                            }
                        }
                        if (isExisted)
                            continue;
                        lstPurchaseBlack.Items.Add(user.Name + "(" + user.Id.ToString() + ")");
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnAddPurchaseBlack_Click", ex);
            }
        }

        private void btnRemovePurchaseBlack_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = lstPurchaseBlack.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstPurchaseBlack.Items.Remove(lstPurchaseBlack.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUserDetail.btnRemovePurchaseBlack_Click", ex);
            }
        }
        #endregion

    }
}