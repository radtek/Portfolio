using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;

using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmSendMessage : FrmToolBase
    {        
        private AccountInfo _account;        
        private ToolSendMessages _toolsendmsg;

        //public delegate void MessageChangedEventHandler(string caption, string key, string message);
        //public event MessageChangedEventHandler messageChanged;
        
        public FrmSendMessage()
        {
            InitializeComponent();
        }

        #region FrmSendMessage_Load
        private void FrmSendMessage_Load(object sender, EventArgs e)
        {
            try
            {
                BuildCmbGroup();
                BuildCmbAccount(cmbGroup.Text);
                
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
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
            Collection<AccountInfo> accounts = ConfigCtrl.GetAccounts(group);
            if (accounts != null)
            {
                cmbSender.Items.Clear();
                foreach (AccountInfo account in accounts)
                {
                    cmbSender.Items.Add(account);
                }                
            }

            lstAllAccounts.Items.Clear();
            lstSelectedAccounts.Items.Clear();
        }
        #endregion

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSender.Text = "";
            BuildCmbAccount(cmbGroup.Text);
        }
        #endregion

        #region FrmSendMessage_FormClosing
        private void FrmSendMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_toolsendmsg != null)
                    _toolsendmsg.StopThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }
        #endregion

        #region cmbSender_SelectedIndexChanged
        private void cmbSender_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSender.Items.Count <= 0 || cmbSender.SelectedIndex < 0)
                    return;

                _account = cmbSender.Items[cmbSender.SelectedIndex] as AccountInfo;
                if (_account == null)
                    return;

                SetControlStatus(false);

                _toolsendmsg = new ToolSendMessages(_account);
                _toolsendmsg.MessageChanged += new KaixinBase.MessageChangedEventHandler(toolsendmsg_MessageChanged);
                _toolsendmsg.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolsendmsg_ValidateCodeNeeded);
                _toolsendmsg.AllMyFriendsFetched += new KaixinBase.AllMyFriendsFetchedEventHandler(toolsendmsg_AllMyFriendsFetched);
                _toolsendmsg.SendMessageFinished += new ToolSendMessages.SendMessageFinishedEventHandler(_toolsendmsg_SendMessageFinished);
                _toolsendmsg.GetAllMyFriends();

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }

        void _toolsendmsg_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_toolsendmsg_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "群发消息";
                if (picCode.ShowDialog() == DialogResult.OK)
                    _toolsendmsg.ValidationCode = picCode.ValidationCode;
                else
                    _toolsendmsg.ValidationCode = null;
            }
        }
        
        void toolsendmsg_AllMyFriendsFetched(Collection<FriendInfo> allmyfriends)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.AllMyFriendsFetchedEventHandler(toolsendmsg_AllMyFriendsFetched), new object[] { allmyfriends });
            }
            else
            {
                lstAllAccounts.Items.Clear();
                lstSelectedAccounts.Items.Clear();
                foreach (FriendInfo friend in allmyfriends)
                {
                    lstAllAccounts.Items.Add(friend);
                }
                SetControlStatus(true);
            }            
        }

        void toolsendmsg_MessageChanged(string caption, string key, string message)
        {
            SetMessageByParam(caption, key, message);
        }        

        #endregion

        #region btnSend_Click
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSender.Items.Count <= 0 || cmbSender.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择发送者", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSender.Select();
                    return;
                }
                if (lstSelectedAccounts.Items.Count <= 0)
                {
                    MessageBox.Show("请选择接收者", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lstSelectedAccounts.Select();
                    return;
                }
                if (txtMessage.Text == string.Empty)
                {
                    MessageBox.Show("请输入要发送的内容", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMessage.Select();
                    return;
                }

                Collection<FriendInfo> receivers = new Collection<FriendInfo>();
                foreach (object item in lstSelectedAccounts.Items)
                {
                    FriendInfo friend = item as FriendInfo;
                    if (friend != null)
                    {
                        receivers.Add(friend);
                    }
                }

                SetControlStatus(false);
                
                _toolsendmsg.SendMessage(_account, receivers, JsonHelper.CreateHtml(txtMessage.Text), rdbMulti.Checked);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }        

        void _toolsendmsg_SendMessageFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolSendMessages.SendMessageFinishedEventHandler(_toolsendmsg_SendMessageFinished), new object[] { });
            }
            else
            {
                SetControlStatus(true);
            }
        }
        
        #endregion

        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_toolsendmsg != null)
                    _toolsendmsg.StopThread();

                SetControlStatus(true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }
        #endregion

        #region list Select Event
        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllAccounts.SelectedItems)
                {
                    lstSelectedAccounts.Items.Add(item);
                }
                for (int ix = lstAllAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstAllAccounts.Items.Remove(lstAllAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllAccounts.Items)
                {
                    lstSelectedAccounts.Items.Add(item);
                }
                for (int ix = lstAllAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstAllAccounts.Items.Remove(lstAllAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }

        private void btnUnselectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstSelectedAccounts.SelectedItems)
                {
                    lstAllAccounts.Items.Add(item);
                }
                for (int ix = lstSelectedAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstSelectedAccounts.Items.Remove(lstSelectedAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstSelectedAccounts.Items)
                {
                    lstAllAccounts.Items.Add(item);
                }
                for (int ix = lstSelectedAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstSelectedAccounts.Items.Remove(lstSelectedAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmSendMessage", ex);
            }
        }
        #endregion

        #region SetControlStatus
        private void SetControlStatus(bool enabled)
        {
            cmbGroup.Enabled = enabled;
            cmbSender.Enabled = enabled;
            btnSelectOne.Enabled = enabled;
            btnSelectAll.Enabled = enabled;
            btnUnselectOne.Enabled = enabled;
            btnUnselectAll.Enabled = enabled;
            btnSend.Enabled = enabled;
        }
        #endregion        

    }
}