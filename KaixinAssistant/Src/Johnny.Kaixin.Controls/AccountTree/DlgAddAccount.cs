using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.Controls.AccountTree
{
    public partial class DlgAddAccount : Form
    {
        private string _groupname;
        private AccountInfo _account;
        private string _oldemail;
        private HttpHelper _hh;
        private byte[] _image;
        private bool _expanded = false;

        public DlgAddAccount()
        {
            InitializeComponent();
        }

        #region DlgAddAccount_Load
        private void DlgAddAccount_Load(object sender, EventArgs e)
        {
            try
            {
                SetControls(false);
                if (_oldemail != null && _oldemail != string.Empty)
                {
                    this.Icon = IconCtrl.GetIconFromResx(TreeConstants.ICON_KEYS);
                    txtEmail.Text = _account.Email;
                    txtPassword.Text = _account.Password;
                    txtUserName.Text = _account.UserName;
                    txtUserId.Text = _account.UserId;
                    txtGender.Text = _account.Gender ? "男" : "女";
                    this.Text = "编辑账号";
                }
                else
                {
                    _account = new AccountInfo();
                    this.Text = "添加账号";
                }

                _hh = new HttpHelper();
                ProxyInfo proxy = ConfigCtrl.GetProxy();

                if (proxy != null && proxy.Enable == true)
                    _hh.SetProxy(proxy.Server, proxy.Port, proxy.UserName, proxy.Password);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }
        #endregion

        #region btnOk_Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text == string.Empty)
                {
                    txtEmail.Select();
                    MessageBox.Show("邮件地址不能为空！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtPassword.Text == string.Empty)
                {
                    txtPassword.Select();
                    MessageBox.Show("密码不能为空！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                _account.Email = txtEmail.Text;
                _account.Password = txtPassword.Text;
                _account.UserName = txtUserName.Text;
                _account.UserId = txtUserId.Text;

                if (ValidationLogin())
                {
                    string ret = ConfigCtrl.EditAccount(_groupname, _account, _oldemail);
                    if (ret != Constants.STATUS_SUCCESS)
                    {
                        MessageBox.Show(ret, Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //MessageBox.Show("登录失败，请检查Email和密码是否正确！或者代理服务器设置是否正确！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.txtEmail.Select();
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region ValidationLogin
        private bool ValidationLogin()
        {
            if (!this.Login(""))
            {
                MessageBox.Show(this, "密码或验证码输入错误，请重试！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);

                bool retryLogin = true;
                while (retryLogin)
                {
                    DlgImageCode dlgPic = new DlgImageCode();                    
                    dlgPic.Email = txtEmail.Text;
                    dlgPic.Password = txtPassword.Text;
                    dlgPic.UserName = txtUserName.Text;
                    dlgPic.UserId = txtUserId.Text;
                    dlgPic.Gender = txtGender.Text;
                    dlgPic.ValidationImage = _image;
                    dlgPic.Location = this.Location;
                    this.Visible = false;
                    if (_oldemail != null && _oldemail != string.Empty)
                    {
                        dlgPic.Icon = IconCtrl.GetIconFromResx(TreeConstants.ICON_KEYS);
                        dlgPic.WindowsCaption = "编辑账号";
                    }
                    else
                        dlgPic.WindowsCaption = "添加账号";
                    if (dlgPic.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!_expanded)
                            SetControls(true);
                        this.Location = dlgPic.Location;
                        this.Visible = true;
                        txtEmail.Text = dlgPic.Email;
                        txtPassword.Text = dlgPic.Password;
                        txtValidationCode.Text = dlgPic.ValidationCode;
                        if (_image != null & _image.Length > 0)
                        {
                            MemoryStream stream = new MemoryStream();
                            stream.Write(_image, 0, _image.Length);
                            imgValidationCode.Image = Image.FromStream(stream);
                        }

                        if (!this.Login(dlgPic.ValidationCode))
                        {
                            MessageBox.Show(this, "密码或验证码输入错误，请重试！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        else
                            break;
                    }
                    else
                        retryLogin = false;

                }
                if (!retryLogin)
                    return false;
            }
            return true;
        }
        #endregion

        #region Login
        private bool Login(string validationcode)
        {
            try
            {
                string content = RequestLogin(txtEmail.Text, txtPassword.Text, validationcode);
                if (content.IndexOf("security.kaixin001.com/js/sso.js") != -1) {
                    content = _hh.Get("http://www.kaixin001.com/home/index.php");
                    if (content.IndexOf("<title>我的首页 - 开心网</title>") != -1)
                    {
                        _account.UserId = JsonHelper.GetMid(content, "我的开心网ID:", "\"></a>");
                        _account.UserName = JsonHelper.GetMid(content, _account.UserId + "\" class=\"sl2\">", "</a></p>");
                        content = _hh.Get("http://www.kaixin001.com/set/account.php");
                        _account.Gender = (content.IndexOf("<option value=\"1\" selected>男</option>") != -1) ? true : false;
                        //_hh.Get("http://www.kaixin001.com/login/logout.php");
                        _hh.Post("http://www.kaixin001.com/interface/s.php", "type=ss2&key=hd_nav_logout_click");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (content.IndexOf("<title>登录 - 开心网</title>") != -1)
                {
                    _image = this._hh.GetImage("http://www.kaixin001.com/interface/regcreatepng.php?randnum=0.03706184340980051_1253091176687&norect=1", "http://www.kaixin001.com/login/login.php");
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
                return false;
            }
        }
        #endregion

        #region RequestLogin
        private string RequestLogin(string email, string password, string validationcode)
        {
            if (String.IsNullOrEmpty(validationcode))
                this._hh.Get("http://www.kaixin001.com/");
            this._hh.DelayedTime = Constants.DELAY_2SECONDS;

            //string loginUrl = "http://www.kaixin001.com/login/login.php";
            string loginUrl = "https://security.kaixin001.com/login/login_auth.php";
            string param = "";
            if (string.IsNullOrEmpty(validationcode))
                //param = "url=%2Fhome%2F&email=" + DataConvert.GetEncodeData(email) + "&password=" + DataConvert.GetEncodeData(password);
                param = "rcode=&url=http%3A%2F%2Fwww.kaixin001.com%2F%3F647383871%3D342757378&email=" + DataConvert.GetEncodeData(email) + "&password=" + DataConvert.GetEncodeData(password) +"&code=";
            else
                param = "rcode=0.03706184340980051_1253091176687&url=%2Fhome%2F&rpkey=&diarykey=&invisible_mode=0&email=" + DataConvert.GetEncodeData(email) + "&password=" + DataConvert.GetEncodeData(password) + "&code=" + DataConvert.GetEncodeData(validationcode);
            return this._hh.Post(loginUrl, "http://www.kaixin001.com/", param);
        }
        #endregion

        #region Properties

        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }

        public string OldEmail
        {
            get { return _oldemail; }
            set { _oldemail = value; }
        }

        public AccountInfo Account
        {
            get { return _account; }
            set { _account = value; }
        }

        #endregion

        private void SetControls(bool isImage)
        {
            int offset = 60;
            if (isImage)
            {
                lblValidationCode.Visible = true;
                txtValidationCode.Visible = true;
                imgValidationCode.Visible = true;
                grpAccount.Height += offset;
                lblUserName.Top += offset;
                txtUserName.Top += offset;
                lblUserId.Top += offset;
                txtUserId.Top += offset;
                lblGender.Top += offset;
                txtGender.Top += offset;
                btnOk.Top += offset;
                btnCancel.Top += offset;
                this.Height += offset;
                _expanded = true;
            }
            else
            {
                lblValidationCode.Visible = false;
                txtValidationCode.Visible = false;
                imgValidationCode.Visible = false;
                grpAccount.Height -= offset;
                lblUserName.Top -= offset;
                txtUserName.Top -= offset;
                lblUserId.Top -= offset;
                txtUserId.Top -= offset;
                lblGender.Top -= offset;
                txtGender.Top -= offset;
                btnOk.Top -= offset;
                btnCancel.Top -= offset;
                this.Height -= offset;
            }
        }
        
    }
}