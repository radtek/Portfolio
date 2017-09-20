using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;

using WeifenLuo.WinFormsUI.Docking;
using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmAddFriends : FrmToolBase
    {
        private ToolAddFriends _tooladdfriends;

        //public delegate void MessageChangedEventHandler(string caption, string key, string message);
        //public event MessageChangedEventHandler messageChanged;

        public FrmAddFriends()
        {
            InitializeComponent();
            _tooladdfriends = new ToolAddFriends();
            _tooladdfriends.MessageChanged += new KaixinBase.MessageChangedEventHandler(_tooladdfriends_MessageChanged);
            _tooladdfriends.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_tooladdfriends_ValidateCodeNeeded);
            _tooladdfriends.AddFriendsFinished += new ToolAddFriends.AddFriendsFinishedEventHandler(_tooladdfriends_AddFriendsFinished);
        }

        void _tooladdfriends_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_tooladdfriends_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "互加好友";
                if (picCode.ShowDialog() == DialogResult.OK)
                    _tooladdfriends.ValidationCode = picCode.ValidationCode;
                else
                    _tooladdfriends.ValidationCode = null;
            }
        }

        #region FrmAddFriends_Load
        private void FrmAddFriends_Load(object sender, EventArgs e)
        {
            try
            {
                BuildCmbGroup();
                
                rdbAuto.Checked = true;
                chkExecuteSendRequest.Checked = true;
                chkExecuteConfirmRequest.Checked = true;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
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

        private void BuildListAccount(string group)
        {
            Collection<AccountInfo> accounts = ConfigCtrl.GetAccounts(group);

            if (accounts != null)
            {
                lstAllNewAccounts.Items.Clear();
                lstAllOldAccounts.Items.Clear();

                foreach (AccountInfo user in accounts)
                {
                    lstAllNewAccounts.Items.Add(user);
                    lstAllOldAccounts.Items.Add(user);
                }
            }

        }

        #endregion

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildListAccount(cmbGroup.Text);
        }
        #endregion

        #region FrmAddFriends_FormClosed
        private void FrmAddFriends_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _tooladdfriends.StopThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }
        #endregion

        #region btnRun_Click
        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstNewAccounts.Items.Count <= 0)
                {
                    MessageBox.Show("请至少选择一个账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lstNewAccounts.Select();
                    return;
                }
                if (!rdbAuto.Checked && lstOldAccounts.Items.Count<=0)
                {
                    MessageBox.Show("请至少选择一个旧账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lstOldAccounts.Select();
                    return;
                }

                if (!chkExecuteSendRequest.Checked && !chkExecuteConfirmRequest.Checked)
                {
                    MessageBox.Show("请至少选择一个操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkExecuteSendRequest.Select();
                    return;
                }

                //accounts
                Collection<AccountInfo> newaccounts = new Collection<AccountInfo>();
                foreach (object item in lstNewAccounts.Items)
                {
                    AccountInfo user = item as AccountInfo;
                    if (user != null)
                    {
                        newaccounts.Add(user);
                    }
                }

                Collection<AccountInfo> oldaccounts = new Collection<AccountInfo>();
                foreach (object item in lstOldAccounts.Items)
                {
                    AccountInfo user = item as AccountInfo;
                    if (user != null)
                    {
                        oldaccounts.Add(user);
                    }
                }

                SetControlStatus(false);

                AddFriendsInfo addfriends = new AddFriendsInfo();
                addfriends.AddMode = rdbAuto.Checked;
                addfriends.DeleteAllMessages = chkDeleteAllMessage.Checked;
                addfriends.ExecuteSendRequest = chkExecuteSendRequest.Checked;
                addfriends.ExecuteConfirmRequest = chkExecuteConfirmRequest.Checked;
                addfriends.NewAccountsList = newaccounts;
                addfriends.OldAccountsList = oldaccounts;
                addfriends.Accounts = newaccounts;

                _tooladdfriends.AddFriends(addfriends);


            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }

        void _tooladdfriends_AddFriendsFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolAddFriends.AddFriendsFinishedEventHandler(_tooladdfriends_AddFriendsFinished), new object[] { });
            }
            else
            {
                SetControlStatus(true);
            }
        }

        void _tooladdfriends_MessageChanged(string caption, string key, string message)
        {
            SetMessageByParam(caption, key, message);
        }        
        
        #endregion

        #region list Select Event
        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllNewAccounts.SelectedItems)
                {
                    lstNewAccounts.Items.Add(item);
                }
                for (int ix = lstAllNewAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstAllNewAccounts.Items.Remove(lstAllNewAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllNewAccounts.Items)
                {
                    lstNewAccounts.Items.Add(item);
                }
                for (int ix = lstAllNewAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstAllNewAccounts.Items.Remove(lstAllNewAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }

        private void btnUnselectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstNewAccounts.SelectedItems)
                {
                    lstAllNewAccounts.Items.Add(item);
                }
                for (int ix = lstNewAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstNewAccounts.Items.Remove(lstNewAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstNewAccounts.Items)
                {
                    lstAllNewAccounts.Items.Add(item);
                }
                for (int ix = lstNewAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstNewAccounts.Items.Remove(lstNewAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }
        #endregion

        #region old list
        private void btnSelectOneOld_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllOldAccounts.SelectedItems)
                {
                    lstOldAccounts.Items.Add(item);
                }
                for (int ix = lstAllOldAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstAllOldAccounts.Items.Remove(lstAllOldAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }

        private void btnSelectAllOld_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllOldAccounts.Items)
                {
                    lstOldAccounts.Items.Add(item);
                }
                for (int ix = lstAllOldAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstAllOldAccounts.Items.Remove(lstAllOldAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }

        private void btnUnselectOneOld_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstOldAccounts.SelectedItems)
                {
                    lstAllOldAccounts.Items.Add(item);
                }
                for (int ix = lstOldAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstOldAccounts.Items.Remove(lstOldAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }

        private void btnUnselectAllOld_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstOldAccounts.Items)
                {
                    lstAllOldAccounts.Items.Add(item);
                }
                for (int ix = lstOldAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstOldAccounts.Items.Remove(lstOldAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }
        #endregion

        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                _tooladdfriends.StopThread();

                SetControlStatus(true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }
        #endregion        

        #region rdbAuto_CheckedChanged
        private void rdbAuto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.MinimumSize = new Size(790, 300);
                tableLayoutPanelParent.RowCount = 1;
                grpOldAccounts.Visible = false;
                grpNewAccounts.Text = "选择账号";
                lblNewAccounts.Text = "*需要互加为好友的帐号";
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }

        }

        private void rdbManual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.MinimumSize = new Size(790, 590);
                tableLayoutPanelParent.RowCount = 2;
                grpOldAccounts.Visible = true;
                grpNewAccounts.Text = "选择新帐号";
                lblNewAccounts.Text = "*新账号";
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAddFriends", ex);
            }
        }
        #endregion

        #region SetControlStatus
        private void SetControlStatus(bool enabled)
        {
            cmbGroup.Enabled = enabled;
            btnRun.Enabled = enabled;
        }
        #endregion
        
    }
}