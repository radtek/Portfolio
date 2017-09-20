using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmMaintainContact : FrmToolBase
    {
        private Collection<AccountInfo> _accounts;
        private ToolMaintainContact _toolmaintaincontact;

        //public delegate void MessageChangedEventHandler(string caption, string key, string message);
        //public event MessageChangedEventHandler messageChanged;

        public FrmMaintainContact()
        {
            InitializeComponent();
            _toolmaintaincontact = new ToolMaintainContact();
            _toolmaintaincontact.MessageChanged += new KaixinBase.MessageChangedEventHandler(_toolmaintaincontact_MessageChanged);
            _toolmaintaincontact.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolmaintaincontact_ValidateCodeNeeded);
            _toolmaintaincontact.AllFetchFinished += new ToolMaintainContact.AllFetchFinishedEventHandler(_toolmaintaincontact_AllFetchFinished);
            _toolmaintaincontact.SendRequestFinished += new ToolMaintainContact.SendRequestFinishedEventHandler(_toolmaintaincontact_SendRequestFinished);
        }

        void _toolmaintaincontact_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_toolmaintaincontact_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "维护联系人";
                if (picCode.ShowDialog() == DialogResult.OK)
                    _toolmaintaincontact.ValidationCode = picCode.ValidationCode;
                else
                    _toolmaintaincontact.ValidationCode = null;
            }
        }        

        void _toolmaintaincontact_MessageChanged(string caption, string key, string message)
        {
            SetMessageByParam(caption, key, message);
        }

        void _toolmaintaincontact_AllFetchFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolMaintainContact.AllFetchFinishedEventHandler(_toolmaintaincontact_AllFetchFinished), new object[] { });
            }
            else
            {
                SetControlStatus(true, true);
            }
        }

        void _toolmaintaincontact_SendRequestFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolMaintainContact.SendRequestFinishedEventHandler(_toolmaintaincontact_SendRequestFinished), new object[] { });
            }
            else
            {
                SetControlStatus(true, false);
            }
        }

        #region FrmMaintainContact_Load
        private void FrmMaintainContact_Load(object sender, EventArgs e)
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
                        cmbGroupAdd.Items.Add(group);
                    }
                }

                if (cmbGroup.Items.Count > 0)
                    cmbGroup.SelectedIndex = 0;
                if (cmbGroupAdd.Items.Count > 0)
                    cmbGroupAdd.SelectedIndex = 0;

                cmbGroup_SelectedIndexChanged(null, null);

                txtRequestContent.Text = "Hi， 我是XXX，这是我的小号。";
                
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region FrmMaintainContact_FormClosing
        private void FrmMaintainContact_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_toolmaintaincontact != null)
                    _toolmaintaincontact.StopThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroup.Items.Count > 0 && cmbGroup.Text != string.Empty)
                {
                    _accounts = ConfigCtrl.GetAccounts(cmbGroup.Text);
                    listBoxSelectorAccounts.Clear();
                    listBoxSelectorAccounts.AllItems = _accounts;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region btnRun_Click
        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxSelectorAccounts.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("请选择要执行的账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBoxSelectorAccounts.Select();
                    return;
                }

                if (String.IsNullOrEmpty(txtOutputPath.Text))
                {
                    MessageBox.Show("导出目录不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOutputPath.Select();
                    return;
                }

                if (!Directory.Exists(txtOutputPath.Text))
                {
                    MessageBox.Show("导出目录不存在！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOutputPath.Select();
                    return;
                }

                SetControlStatus(false, true);

                _toolmaintaincontact._accounts = listBoxSelectorAccounts.SelectedItems;
                _toolmaintaincontact._path = txtOutputPath.Text;
                _toolmaintaincontact.SaveFriendsToFileByThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }

        #endregion

        #region SetControlStatus
        private void SetControlStatus(bool enabled, bool outputop)
        {
            if (outputop)
            {
                cmbGroup.Enabled = enabled;
                btnSaveToFile.Enabled = enabled;
                btnSendRequest.Enabled = enabled;
                btnStop2.Enabled = enabled;
            }
            else
            {
                btnSaveToFile.Enabled = enabled;
                btnStop.Enabled = enabled;
                btnSendRequest.Enabled = enabled;
            }
        }
        #endregion

        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_toolmaintaincontact != null)
                    _toolmaintaincontact.StopThread();
                SetControlStatus(true, true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region btnSelectFolder_Click
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowNewFolderButton = true;
                dialog.SelectedPath = Directory.GetCurrentDirectory();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = dialog.SelectedPath;
                }                
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region BuildCmbAccountAdd
        private void BuildCmbAccountAdd(string group)
        {
            //所有的账号
            Collection<AccountInfo> accounts = ConfigCtrl.GetAccounts(group);
            if (accounts != null)
            {
                cmbAccountAdd.Items.Clear();
                foreach (AccountInfo account in accounts)
                {
                    cmbAccountAdd.Items.Add(account);
                }
                if (cmbAccountAdd.Items.Count > 0)
                    cmbAccountAdd.SelectedIndex = 0;
            }
        }
        #endregion

        #region cmbGroupAdd_SelectedIndexChanged
        private void cmbGroupAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BuildCmbAccountAdd(cmbGroupAdd.Text);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region btnImport_Click
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = Application.ExecutablePath;
                dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Collection<FriendInfo> friends = ConfigCtrl.GetContactsFromFile(dialog.FileName);
                    if (friends == null || friends.Count == 0)
                    {
                        MessageBox.Show("读取联系人信息失败！", Constants.MSG_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    listBoxSelectorFriends.Clear();
                    listBoxSelectorFriends.AllItems = friends;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region btnSendRequest_Click
        private void btnSendRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccountAdd.Items.Count <= 0 || cmbAccountAdd.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbAccountAdd.Select();
                    return;
                }

                if (listBoxSelectorFriends.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("请选择要发送的好友！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBoxSelectorFriends.Select();
                    return;
                }

                if (String.IsNullOrEmpty(txtRequestContent.Text))
                {
                    MessageBox.Show("请输入请求内容！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRequestContent.Select();
                    return;
                }

                SetControlStatus(false, false);

                AccountInfo account = cmbAccountAdd.Items[cmbAccountAdd.SelectedIndex] as AccountInfo;

                if (account == null)
                    return;

                _toolmaintaincontact._account = account;
                _toolmaintaincontact._friends = listBoxSelectorFriends.SelectedItems;
                _toolmaintaincontact._requestcontent = txtRequestContent.Text;
                _toolmaintaincontact.SendRequestByThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

        #region btnStop2_Click
        private void btnStop2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_toolmaintaincontact != null)
                    _toolmaintaincontact.StopThread();
                SetControlStatus(true, false);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmMaintainContact", ex);
            }
        }
        #endregion

    }
}