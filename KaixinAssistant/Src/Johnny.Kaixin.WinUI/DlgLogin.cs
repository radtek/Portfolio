using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Security;

using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class DlgLogin : Form
    {
        private Collection<EncryptFriendInfo> _friends;

        public DlgLogin()
        {
            InitializeComponent();
            _friends = new Collection<EncryptFriendInfo>();
        }

        #region DlgLogin_Load
        private void DlgLogin_Load(object sender, EventArgs e)
        {
            try
            {
                string resname = "Johnny.Kaixin.WinUI.Resources.EncryptFriends.config";
                using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resname)))
                {
                    string configContent = streamReader.ReadToEnd();
                    _friends = ConfigCtrl.GetEncryptFriendFromFile(configContent);
                }

                if (_friends == null || _friends.Count == 0)
                {
                    MessageBox.Show("�޷�ȡ�ú����б�", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

                chkRemember.Checked = Properties.Settings.Default.NeedRemember;
                if (chkRemember.Checked)
                {
                    txtUserName.Text = Properties.Settings.Default.LoginUserName;
                    txtUserId.Text = Properties.Settings.Default.LoginUserID;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgLogin", ex);
            }
        }
        #endregion

        #region btnOk_Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtUserName.Text))
                {
                    txtUserName.Select();
                    MessageBox.Show("�û�������Ϊ�գ�", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (String.IsNullOrEmpty(txtUserId.Text))
                {
                    txtUserId.Select();
                    MessageBox.Show("�û�ID����Ϊ�գ�", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!DataValidation.IsNaturalNumber(txtUserId.Text))
                {
                    txtUserId.Select();
                    MessageBox.Show("�û�IDֻ��Ϊ���֣�", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (EncryptFriendInfo friend in _friends)
                {
                    if (friend.Name == FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserName.Text, "MD5") &&
                        friend.Id == FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserId.Text, "MD5"))
                    {
                        Properties.Settings.Default.NeedRemember = chkRemember.Checked;
                        if (chkRemember.Checked)
                        {
                            Properties.Settings.Default.LoginUserName = txtUserName.Text;
                            Properties.Settings.Default.LoginUserID = txtUserId.Text;
                        }
                        else
                        {
                            Properties.Settings.Default.LoginUserName = "";
                            Properties.Settings.Default.LoginUserID = "";
                        }
                        Properties.Settings.Default.Save();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                }

                MessageBox.Show(string.Format("{0}({1})���Ǳ�������ߵĿ��������ѣ���¼ʧ�ܣ�", txtUserName.Text, txtUserId.Text), Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Select();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgLogin", ex);
            }
        }
        #endregion       
    }
}