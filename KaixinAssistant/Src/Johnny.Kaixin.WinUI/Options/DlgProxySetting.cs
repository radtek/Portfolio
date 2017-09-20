using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class DlgProxySetting : Form
    {
        public DlgProxySetting()
        {
            InitializeComponent();
        }

        private void DlgProxySetting_Load(object sender, EventArgs e)
        {
            try
            {
                //load config info
                ProxyInfo proxy = ConfigCtrl.GetProxy();
                if (proxy != null)
                {
                    chkProxy.Checked = proxy.Enable;
                    txtSever.Text = proxy.Server;
                    txtPort.Text = proxy.Port.ToString();
                    txtUserName.Text = proxy.UserName;
                    txtPassword.Text = proxy.Password;
                }
                chkProxy_CheckedChanged(null, null);
                txtSever.Select();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgProxySetting", ex);
                this.Close();
            }
        }

        private void chkProxy_CheckedChanged(object sender, EventArgs e)
        {
            txtSever.Enabled = chkProxy.Checked;
            txtPort.Enabled = chkProxy.Checked;
            txtUserName.Enabled = chkProxy.Checked;
            txtPassword.Enabled = chkProxy.Checked;
            lblServer.Enabled = chkProxy.Checked;
            lblPort.Enabled = chkProxy.Checked;
            lblUserName.Enabled = chkProxy.Checked;
            lblPassword.Enabled = chkProxy.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtPort.Text != string.Empty) && !DataValidation.IsNaturalNumber(txtPort.Text))
                {
                    MessageBox.Show("端口必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPort.Select();
                    return;
                }

                ProxyInfo proxy = new ProxyInfo();
                proxy.Enable = chkProxy.Checked;
                proxy.Server = txtSever.Text;
                if (txtPort.Text != string.Empty)
                    proxy.Port = DataConvert.GetInt32(txtPort.Text);
                else
                    proxy.Port = null;
                proxy.UserName = txtUserName.Text;
                proxy.Password = txtPassword.Text;

                if (!ConfigCtrl.SetProxy(proxy))
                {
                    MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgProxySetting", ex);
            }
        }        
    }
}