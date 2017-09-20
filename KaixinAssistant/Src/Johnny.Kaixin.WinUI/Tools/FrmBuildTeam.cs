using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;

using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;
using WeifenLuo.WinFormsUI.Docking;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmBuildTeam : FrmToolBase
    {
        private ToolBuildTeam _toolbuildteam;

        private AccountInfo _account;
        private NewCarInfo _modelcar;
        private int _maxcarcount;
        private ExchangeCar _exchange;
        private Collection<NewCarInfo> _carsInMarket;

        //public delegate void MessageChangedEventHandler(string caption, string key, string message);
        //public event MessageChangedEventHandler messageChanged;

        public FrmBuildTeam()
        {
            InitializeComponent();
        }

        #region FrmBuildTeam_Load
        private void FrmBuildTeam_Load(object sender, EventArgs e)
        {
            try
            {
                cmbMaxCarCount.SelectedIndex = cmbMaxCarCount.Items.Count - 2;

                //市场上的汽车
                _carsInMarket = ConfigCtrl.GetCarsInMarket();
                if (_carsInMarket == null)
                    return;
                lstViewCarsInMarket.Items.Clear();

                int num = 1;
                foreach (NewCarInfo car in _carsInMarket)
                {
                    string[] subItem = new string[4];
                    subItem[0] = num.ToString();
                    subItem[1] = car.CarName;
                    subItem[2] = car.CarPrice.ToString();
                    subItem[3] = car.CarId.ToString();
                    lstViewCarsInMarket.Items.Add(new ListViewItem(subItem));
                    num++;
                }

                BuildCmbGroup();
                BuildCmbAccount(cmbGroup.Text);
                
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }

        private void BuildCmbGroup()
        {
            string[] groups = ConfigCtrl.GetGroups();
            if (groups != null)
            {
                foreach (string group in groups)
                {
                    cmbGroup.Items.Add(group);
                }
                if (cmbGroup.Items.Count > 0)
                    cmbGroup.SelectedIndex = 0;
            }
        }

        private void BuildCmbAccount(string group)
        {
            //所有的账号
            Collection<AccountInfo> accounts = ConfigCtrl.GetAccounts(group);
            if (accounts != null)
            {
                cmbAccount.Items.Clear();
                foreach (AccountInfo account in accounts)
                {
                    cmbAccount.Items.Add(account);                    
                }
                if (cmbAccount.Items.Count > 0)
                    cmbAccount.SelectedIndex = 0;
            }
        }

        #endregion

        #region FrmBuildTeam_FormClosing
        private void FrmBuildTeam_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_toolbuildteam != null)
                    _toolbuildteam.StopThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }
        #endregion

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildCmbAccount(cmbGroup.Text);
        }
        #endregion

        #region GetCarsInMarket
        private void btnCarsInMarket_Click(object sender, EventArgs e)
        {
            try
            {
                _toolbuildteam = new ToolBuildTeam((AccountInfo)cmbAccount.SelectedItem);
                _toolbuildteam.MessageChanged += new KaixinBase.MessageChangedEventHandler(_toolbuildteam_MessageChanged);
                _toolbuildteam.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolbuildteam_ValidateCodeNeeded);
                _toolbuildteam.AllCarsInMarketFetched += new GamePark.AllCarsInMarketFetchedEventHandler(_toolbuildteam_AllCarsInMarketFetched);

                if (cmbAccount.SelectedIndex >= 0)
                    _account = cmbAccount.Items[cmbAccount.SelectedIndex] as AccountInfo;
                else
                {
                    Collection<AccountInfo> accounts = ConfigCtrl.GetAccounts("");
                    if (accounts == null || accounts.Count == 0)
                    {
                        MessageBox.Show("没有有效的账号，无法刷新！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbAccount.Select();
                        return;
                    }
                    if (accounts[0] == null)
                    {
                        MessageBox.Show("没有有效的账号，无法刷新！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbAccount.Select();
                        return;
                    }

                    _account = accounts[0];
                }

                SetControlStatus(false);

                _toolbuildteam.GetAllCarsInMarketByThread();

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }

        void _toolbuildteam_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_toolbuildteam_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "组建车队";
                if (picCode.ShowDialog() == DialogResult.OK)
                    _toolbuildteam.ValidationCode = picCode.ValidationCode;
                else
                    _toolbuildteam.ValidationCode = null;
            }
        }

        void _toolbuildteam_AllCarsInMarketFetched(Collection<NewCarInfo> carsinmarket)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new GamePark.AllCarsInMarketFetchedEventHandler(_toolbuildteam_AllCarsInMarketFetched), new object[] { carsinmarket });
            }
            else
            {
                if (carsinmarket != null && carsinmarket.Count > 0)
                {
                    lstViewCarsInMarket.Items.Clear();
                    int num = 1;
                    foreach (NewCarInfo car in carsinmarket)
                    {
                        string[] subItem = new string[4];
                        subItem[0] = num.ToString();
                        subItem[1] = car.CarName;
                        subItem[2] = car.CarPrice.ToString();
                        subItem[3] = car.CarId.ToString();
                        lstViewCarsInMarket.Items.Add(new ListViewItem(subItem));
                        num++;
                    }

                    if (!ConfigCtrl.SetCarsInMarket(carsinmarket))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                SetControlStatus(true);
            }
        }

        void _toolbuildteam_MessageChanged(string caption, string key, string message)
        {
            SetMessageByParam(caption, key, message);
        }

        #endregion

        #region Get My Cars
        private void btnCars_Click(object sender, EventArgs e)
        {
            try
            {                
                _toolbuildteam = new ToolBuildTeam((AccountInfo)cmbAccount.SelectedItem);
                _toolbuildteam.MessageChanged += new KaixinBase.MessageChangedEventHandler(_toolbuildteam_MessageChanged);
                _toolbuildteam.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolbuildteam_ValidateCodeNeeded);
                _toolbuildteam.MyCarFetched += new GamePark.MyCarFetchedEventHandler(_toolbuildteam_MyCarFetched);

                if (cmbAccount.Items.Count <= 0 || cmbAccount.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbAccount.Select();
                    return;
                }

                _account = cmbAccount.Items[cmbAccount.SelectedIndex] as AccountInfo;

                if (_account == null)
                    return;

                SetControlStatus(false);
                
                _toolbuildteam.GetMyCarsByThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }

        void _toolbuildteam_MyCarFetched(Collection<CarInfo> cars, int parkcash, int carprice)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GamePark.MyCarFetchedEventHandler(_toolbuildteam_MyCarFetched), new object[] { cars, parkcash, carprice });
                }
                else
                {
                    if (cars != null && cars.Count > 0 && parkcash > 0 && carprice > 0)
                    {
                        txtCash.Text = parkcash.ToString();
                        txtCarPrice.Text = carprice.ToString();
                        txtSum.Text = (parkcash + carprice).ToString();
                        txtAllAsset.Text = txtSum.Text;
                        txtAveragePrice.Text = ((parkcash + carprice) / 6).ToString();
                        lstViewMyCars.Items.Clear();
                        int num = 1;
                        foreach (CarInfo car in SortCarsList(cars))
                        {
                            string[] subItem = new string[4];
                            subItem[0] = num.ToString();
                            subItem[1] = car.CarName;
                            subItem[2] = car.CarPrice.ToString();
                            subItem[3] = car.CarId.ToString();
                            lstViewMyCars.Items.Add(new ListViewItem(subItem));
                            num++;
                        }

                        Collection<NewCarInfo> allbuildablecars = _toolbuildteam.GetBuildableCars(parkcash, carprice, ConfigCtrl.GetCarsInMarket());
                        lstViewValidCars.Items.Clear();
                        num = 1;
                        foreach (NewCarInfo car in allbuildablecars)
                        {
                            string[] subItem = new string[4];
                            subItem[0] = num.ToString();
                            subItem[1] = car.CarName;
                            subItem[2] = car.CarPrice.ToString();
                            subItem[3] = car.CarId.ToString();
                            lstViewValidCars.Items.Add(new ListViewItem(subItem));
                            num++;
                        }
                    }
                    SetControlStatus(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }
        
        #endregion

        #region SortCarsList
        private Collection<CarInfo> SortCarsList(Collection<CarInfo> cars)
        {
            for (int ix = 0; ix < cars.Count; ix++)
            {
                for (int iy = ix + 1; iy < cars.Count; iy++)
                {
                    if (cars[ix].CarPrice > cars[iy].CarPrice)
                    {
                        CarInfo temp = cars[ix];
                        cars[ix] = cars[iy];
                        cars[iy] = temp;
                    }
                }
            }

            return cars;
        }
        #endregion

        #region SetControlStatus
        private void SetControlStatus(bool enabled)
        {
            cmbGroup.Enabled = enabled;
            cmbAccount.Enabled = enabled;
            btnCarsInMarket.Enabled = enabled;
            btnCars.Enabled = enabled;
            btnBuildTeam.Enabled = enabled;
        }
        #endregion

        #region btnBuildTeam_Click
        private void btnBuildTeam_Click(object sender, EventArgs e)
        {
            try
            {
                _toolbuildteam = new ToolBuildTeam((AccountInfo)cmbAccount.SelectedItem);
                _toolbuildteam.MessageChanged += new KaixinBase.MessageChangedEventHandler(_toolbuildteam_MessageChanged);
                _toolbuildteam.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolbuildteam_ValidateCodeNeeded);
                _toolbuildteam.BuildTeamFinished += new GamePark.BuildTeamFinishedEventHandler(_toolbuildteam_BuildTeamFinished);

                if (cmbAccount.Items.Count <= 0 || cmbAccount.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbAccount.Select();
                    return;
                }

                if (lstViewCarsInMarket.SelectedItems.Count == 0)
                {
                    MessageBox.Show("请在市场上的汽车列表里选择你需要组建的车队车型！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lstViewCarsInMarket.Select();
                    return;
                }

                if (cmbMaxCarCount.Items.Count <= 0 || cmbMaxCarCount.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择汽车数量！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMaxCarCount.Select();
                    return;
                }

                _account = cmbAccount.Items[cmbAccount.SelectedIndex] as AccountInfo;

                if (_account == null)
                    return;

                _modelcar = new NewCarInfo();
                ListViewItem item = lstViewCarsInMarket.SelectedItems[0];
                if (item != null)
                {
                    _modelcar.CarId = DataConvert.GetInt32(item.SubItems[3].Text);
                    _modelcar.CarName = item.SubItems[1].Text;
                    _modelcar.CarPrice = DataConvert.GetInt32(item.SubItems[2].Text);
                }

                if (_modelcar.CarPrice < 70000)
                {
                    MessageBox.Show("按规则70000元以下的车型，同一款车只能拥有一辆！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lstViewCarsInMarket.Select();
                    return;
                }
                if (_modelcar.CarPrice < 200000)
                {
                    if (MessageBox.Show("根据游戏规则，不能组建汽车单价低于200000的车队！是否继续？", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        lstViewCarsInMarket.Select();
                        return;
                    }
                }

                _maxcarcount = DataConvert.GetInt32(cmbMaxCarCount.Items[cmbMaxCarCount.SelectedIndex].ToString());
                _exchange = ExchangeCar.Stop;
                if (rdbExpensive.Checked)
                    _exchange = ExchangeCar.Expensive;
                else if (rdbCheap.Checked)
                    _exchange = ExchangeCar.Cheap;

                SetControlStatus(false);
                
                _toolbuildteam._account = _account;
                _toolbuildteam._modelcar = _modelcar;
                _toolbuildteam._maxcarcount = _maxcarcount;
                _toolbuildteam._exchange = _exchange;
                _toolbuildteam._carsInMarket = _carsInMarket;
                _toolbuildteam.BuildTeamByThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }

        void _toolbuildteam_BuildTeamFinished(Collection<CarInfo> cars, int parkcash, int carprice)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GamePark.BuildTeamFinishedEventHandler(_toolbuildteam_BuildTeamFinished), new object[] { cars, parkcash, carprice });
                }
                else
                {
                    if (cars != null)
                    {
                        txtCash.Text = parkcash.ToString();
                        txtCarPrice.Text = carprice.ToString();
                        txtSum.Text = (parkcash + carprice).ToString();
                        txtAllAsset.Text = txtSum.Text;
                        txtAveragePrice.Text = ((parkcash + carprice) / 6).ToString();
                        lstViewMyCars.Items.Clear();
                        int num = 1;
                        foreach (CarInfo car in SortCarsList(cars))
                        {
                            string[] subItem = new string[4];
                            subItem[0] = num.ToString();
                            subItem[1] = car.CarName;
                            subItem[2] = car.CarPrice.ToString();
                            subItem[3] = car.CarId.ToString();
                            lstViewMyCars.Items.Add(new ListViewItem(subItem));
                            num++;
                        }
                    }
                    SetControlStatus(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }

        #endregion

        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_toolbuildteam != null)
                    _toolbuildteam.StopThread();
                SetControlStatus(true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuildTeam", ex);
            }
        }
        #endregion

        

    }
}