using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;

using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmUpgradeGarage : FrmToolBase
    {
        private Collection<AccountInfo> _accounts;
        private Collection<NewCarInfo> _carsinmarket;
        private ToolUpgradeGarage _toolupgradegarage;

        //public delegate void MessageChangedEventHandler(string caption, string key, string message);
        //public event MessageChangedEventHandler messageChanged;

        #region Ctor
        public FrmUpgradeGarage()
        {
            InitializeComponent();
            _toolupgradegarage = new ToolUpgradeGarage();
            _toolupgradegarage.MessageChanged += new KaixinBase.MessageChangedEventHandler(_toolupgradegarage_MessageChanged);
            _toolupgradegarage.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolupgradegarage_ValidateCodeNeeded);
            _toolupgradegarage.ToolParkFinished += new GamePark.ToolParkFinishedEventHandler(_toolupgradegarage_ToolParkFinished);
        }

        void _toolupgradegarage_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_toolupgradegarage_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "争车位工具";
                if (picCode.ShowDialog() == DialogResult.OK)
                    _toolupgradegarage.ValidationCode = picCode.ValidationCode;
                else
                    _toolupgradegarage.ValidationCode = null;
            }
        }

        void _toolupgradegarage_MessageChanged(string caption, string key, string message)
        {
            SetMessageByParam(caption, key, message);
        }

        void _toolupgradegarage_ToolParkFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new GamePark.ToolParkFinishedEventHandler(_toolupgradegarage_ToolParkFinished), new object[] { });
            }
            else
            {
                SetControlStatus(true);
            }
        }
        #endregion

        #region FrmBuyCards_Load
        private void FrmBuyCards_Load(object sender, EventArgs e)
        {
            try
            {
                //build group combox
                string[] groups = ConfigCtrl.GetGroups();
                if (groups != null)
                {
                    foreach (string group in groups)
                    {
                        cmbGroup.Items.Add(group);
                    }
                }

                if (cmbGroup.Items.Count > 0)
                    cmbGroup.SelectedIndex = 0;

                cmbGroup_SelectedIndexChanged(null, null);

                //市场上的汽车
                _carsinmarket = ConfigCtrl.GetCarsInMarket();
                listBoxSelectorCars.Clear();
                listBoxSelectorCars.AllItems = _carsinmarket;

                chkUpgradeFreeGarage.Checked = true;
                chkBuyNewCars.Checked = true;
                chkBuyNewCars_CheckedChanged(null, null);

                cmbMaxCars.SelectedIndex = 8;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpgradeGarage", ex);
            }
        }
        #endregion

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroup.Items.Count > 0 && cmbGroup.Text != string.Empty)
            {
                _accounts = ConfigCtrl.GetAccounts(cmbGroup.Text);
                listBoxSelectorAccounts.Clear();
                listBoxSelectorAccounts.AllItems = _accounts;
            }
        }
        #endregion

        #region btnRun_Click
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (listBoxSelectorAccounts.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择要执行的账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBoxSelectorAccounts.Select();
                return;
            }

            if (!chkUpgradeFreeGarage.Checked && !chkBuyNewCars.Checked)
            {
                MessageBox.Show("请至少选择一个要执行的操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkUpgradeFreeGarage.Select();
                return;
            }

            if (chkBuyNewCars.Checked && (String.IsNullOrEmpty(txtMaxCars.Text) || !DataValidation.IsNaturalNumber(txtMaxCars.Text)))
            {
                MessageBox.Show("所有账号购买总数上限不能为空且必须是整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaxCars.Select();
                return;
            }

            if (chkBuyNewCars.Checked && rdbCheap.Checked == false && rdbExpensive.Checked == false)
            {
                MessageBox.Show("请选择购买方式！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                rdbCheap.Select();
                return;
            }

            SetControlStatus(false);

            _toolupgradegarage._accounts = listBoxSelectorAccounts.SelectedItems;
            _toolupgradegarage._upgrade = chkUpgradeFreeGarage.Checked;
            _toolupgradegarage._buycars = chkBuyNewCars.Checked;
            _toolupgradegarage._maxcars = cmbMaxCars.SelectedIndex + 1;
            _toolupgradegarage._allmaxcars = DataConvert.GetInt32(txtMaxCars.Text);
            _toolupgradegarage._carsInMarket = _carsinmarket;
            _toolupgradegarage._blackbuylist = listBoxSelectorCars.SelectedItems;
            _toolupgradegarage._cheapest = rdbCheap.Checked;
            _toolupgradegarage.UpgradeGarageByThread();
        }

        
        #endregion

        #region SetControlStatus
        private void SetControlStatus(bool enabled)
        {
            cmbGroup.Enabled = enabled;
            btnRun.Enabled = enabled;
        }
        #endregion

        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_toolupgradegarage != null)
                    _toolupgradegarage.StopThread();
                SetControlStatus(true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpgradeGarage", ex);
            }
        }
        #endregion

        #region FrmBuyCards_FormClosing
        private void FrmBuyCards_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_toolupgradegarage != null)
                    _toolupgradegarage.StopThread();               
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpgradeGarage", ex);
            }
        }
        #endregion

        #region chkBuyNewCars_CheckedChanged
        private void chkBuyNewCars_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblMaxCars.Enabled = chkBuyNewCars.Checked;
                cmbMaxCars.Enabled = chkBuyNewCars.Checked;
                lblAllAccountsMaxCars.Enabled = chkBuyNewCars.Checked;
                txtMaxCars.Enabled = chkBuyNewCars.Checked;
                grpCars.Enabled = chkBuyNewCars.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpgradeGarage", ex);
            }
        }
        #endregion
    }
}