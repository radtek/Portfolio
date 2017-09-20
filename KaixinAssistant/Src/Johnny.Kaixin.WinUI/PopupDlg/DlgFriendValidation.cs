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
    public partial class DlgFriendValidation : Form
    {
        private KaixinBase _kaixinbase;
        private bool _expanded = false;

        #region Ctor
        public DlgFriendValidation()
        {
            InitializeComponent();
            _kaixinbase = new KaixinBase();
            _kaixinbase.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_kaixinbase_ValidateCodeNeeded);
            _kaixinbase.LoginFailed += new KaixinBase.LoginFailedEventHandler(_kaixinbase_LoginFailed);
            _kaixinbase.AllMyFriendsFetched += new KaixinBase.AllMyFriendsFetchedEventHandler(_kaixinbase_AllMyFriendsFetched);
        }

        void _kaixinbase_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_kaixinbase_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    if (!_expanded)
                        SetControls(true);
                    else
                        MessageBox.Show(this, "密码或验证码输入错误，请重试！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (image.Length > 0)
                    {
                        DlgPicValidation dlgPicVal = new DlgPicValidation();
                        dlgPicVal.Email = txtEmail.Text;
                        dlgPicVal.Password = txtPassword.Text;
                        dlgPicVal.ValidationImage = image;
                        dlgPicVal.IsRemember = chkRemember.Checked;
                        dlgPicVal.Location = this.Location;
                        this.Visible = false;
                        if (dlgPicVal.ShowDialog(this) == DialogResult.OK)
                        {
                            this.Location = dlgPicVal.Location;
                            this.Visible = true;
                            txtEmail.Text = dlgPicVal.Email;
                            txtPassword.Text = dlgPicVal.Password;
                            txtValidationCode.Text = dlgPicVal.ValidationCode;
                            chkRemember.Checked = dlgPicVal.IsRemember;
                            if (image.Length > 0)
                            {
                                MemoryStream stream = new MemoryStream();
                                stream.Write(image, 0, image.Length);
                                imgValidationCode.Image = Image.FromStream(stream);
                            }
                            _kaixinbase.ValidationCode = dlgPicVal.ValidationCode;
                        }
                        else
                            _kaixinbase.ValidationCode = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFriendValidation._kaixinbase_ValidateCodeNeeded", ex);
            }
        }

        private void _kaixinbase_LoginFailed()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.LoginFailedEventHandler(_kaixinbase_LoginFailed), new object[] { });
                }
                else
                {                    
                    SetControlStatus(true);
                    MessageBox.Show(this, "无法登录开心网，请检查：\r\n1.Email或密码是否正确？\r\n2.网络代理设置是否正确？          ", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Select();
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFriendValidation._kaixinbase_AllMyFriendsFetched", ex);
            }
        }

        private void _kaixinbase_AllMyFriendsFetched(Collection<FriendInfo> allmyfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.AllMyFriendsFetchedEventHandler(_kaixinbase_AllMyFriendsFetched), new object[] { allmyfriends });
                }
                else
                {
                    foreach (FriendInfo friend in allmyfriends)
                    {
                        if (FormsAuthentication.HashPasswordForStoringInConfigFile(friend.Name, "MD5") == "451E21794B66319D9110DC7746C3A974" &&
                            friend.Id.ToString().StartsWith("2") && friend.Id.ToString().EndsWith("8") && friend.Id.ToString().Contains("5"))
                        {
                            Properties.Settings.Default.NeedRemember = chkRemember.Checked;
                            if (chkRemember.Checked)
                            {
                                Properties.Settings.Default.Email = txtEmail.Text;
                                Properties.Settings.Default.Password = txtPassword.Text;
                            }
                            else
                            {
                                Properties.Settings.Default.Email = "";
                                Properties.Settings.Default.Password = "";
                            }
                            Properties.Settings.Default.Save();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                    }
                    if (allmyfriends != null && allmyfriends.Count > 0)
                    {
                        SetControlStatus(true);
                        MessageBox.Show(this, string.Format("{0}({1})不是本外挂作者的开心网好友，启动失败！", _kaixinbase.CurrentAccount.UserName, _kaixinbase.CurrentAccount.UserId), Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmail.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFriendValidation._kaixinbase_AllMyFriendsFetched", ex);
            }
        }
        #endregion

        #region DlgFriendValidation_Load
        private void DlgFriendValidation_Load(object sender, EventArgs e)
        {
            try
            {
                SetControls(false);
                chkRemember.Checked = Properties.Settings.Default.NeedRemember;
                if (chkRemember.Checked)
                {
                    txtEmail.Text = Properties.Settings.Default.Email;
                    txtPassword.Text = Properties.Settings.Default.Password;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFriendValidation.DlgFriendValidation_Load", ex);
            }
        }
        #endregion

        #region btnOk_Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtEmail.Text))
                {
                    txtEmail.Select();
                    MessageBox.Show("请输入Email！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!DataValidation.IsEmail(txtEmail.Text))
                {
                    txtEmail.Select();
                    MessageBox.Show("Email格式不正确！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (String.IsNullOrEmpty(txtPassword.Text))
                {
                    txtPassword.Select();
                    MessageBox.Show("请输入密码！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //MessageBox.Show(this, string.Format("{0}({1})不是本外挂作者的开心网好友，启动失败！", "阿桂", 12345678), Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                //backdoor
                //if (FormsAuthentication.HashPasswordForStoringInConfigFile(txtEmail.Text, "MD5") == "E90D7C18DB67A45E54903E39CE2A9ED7" &&
                //    FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "MD5") == "273F00B5B77540677F83BBCD50317934")
                //{
                //    Properties.Settings.Default.NeedRemember = chkRemember.Checked;
                //    if (chkRemember.Checked)
                //    {
                //        Properties.Settings.Default.Email = txtEmail.Text;
                //        Properties.Settings.Default.Password = txtPassword.Text;
                //    }
                //    else
                //    {
                //        Properties.Settings.Default.Email = "";
                //        Properties.Settings.Default.Password = "";
                //    }
                //    Properties.Settings.Default.Save();
                //    this.DialogResult = DialogResult.OK;
                //    this.Close();
                //    return;
                //}

                if (!NetworkHelper.IsConnected())
                {
                    MessageBox.Show("没有可用的网络连接，请确保你的电脑已经连接到Internet。", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                

                SetControlStatus(false);
                _kaixinbase.Caption = "登录验证";
                _kaixinbase.Key = "登录验证";
                _kaixinbase.CurrentAccount = new AccountInfo(txtEmail.Text, txtPassword.Text, "", "", true);
                _kaixinbase.Proxy = ConfigCtrl.GetProxy();
                _kaixinbase.Delay = ConfigCtrl.GetDelay();
                _kaixinbase.Initial();
                _kaixinbase.ResetUserInfo = true; //reset username and user id.
                _kaixinbase.GetAllMyFriendsByThread();
                
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFriendValidation.btnOk_Click", ex);
            }
        }
        #endregion
  
        #region SetControlStatus
        private void SetControlStatus(bool enabled)
        {
            txtEmail.Enabled = enabled;
            txtPassword.Enabled = enabled;
            txtValidationCode.Enabled = enabled;
            chkRemember.Enabled = enabled;
            btnOk.Enabled = enabled;
            btnConnection.Enabled = enabled;
        }
        #endregion

        #region btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_kaixinbase != null)
                    _kaixinbase.StopThread();
                SetControlStatus(true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFriendValidation.btnCancel_Click", ex);
            }
        }
        #endregion

        #region btnConnection_Click
        private void btnConnection_Click(object sender, EventArgs e)
        {
            try
            {
                DlgProxySetting frmProxy = new DlgProxySetting();
                frmProxy.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFriendValidation.btnConnection_Click", ex);
            }
        }
        #endregion

        private void SetControls(bool isImage)
        {
            if (isImage)
            {
                lblValidationCode.Visible = true;
                txtValidationCode.Visible = true;
                imgValidationCode.Visible = true;
                chkRemember.Top += 40;
                grpAccount.Height += 40;
                lblWarning.Top += 40;
                btnOk.Top += 40;
                btnConnection.Top += 40;
                btnCancel.Top += 40;
                this.Height += 40;
                _expanded = true;
            }
            else
            {
                lblValidationCode.Visible = false;
                txtValidationCode.Visible = false;
                imgValidationCode.Visible = false;
                chkRemember.Top -= 40;
                grpAccount.Height -= 40;
                lblWarning.Top -= 40;
                btnOk.Top -= 40;
                btnConnection.Top -= 40;
                btnCancel.Top -= 40;
                this.Height -= 40;
            }
        }

    }
}