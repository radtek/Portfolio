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
    public partial class DlgImageCode : Form
    {
        private string _email = "";
        private string _password = "";
        private string _validationCode = "";
        private string _username = "";
        private string _userid = "";
        private string _gender = "";

        public DlgImageCode()
        {
            InitializeComponent();
        }

        public string WindowsCaption
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                    this.Text = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                txtEmail.Text = _email;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                txtPassword.Text = _password;
            }
        }

        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                txtUserName.Text = _username;
            }
        }

        public string UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
                txtUserId.Text = _userid;
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                txtGender.Text = _gender;
            }
        }

        public string ValidationCode
        {
            get { return _validationCode; }
            set { _validationCode = value; }
        } 

        public byte[] ValidationImage
        {
            set
            {
                if (value.Length > 0)
                {
                    MemoryStream stream = new MemoryStream();
                    stream.Write(value, 0, value.Length);
                    imgValidationCode.Image = Image.FromStream(stream);
                }
            }
        }

        private void DlgImageCode_Load(object sender, EventArgs e)
        {
            try
            {
                txtValidationCode.Select();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("AccountTree.DlgPicdValidation.DlgImageCode_Load", ex);
            }
        }

        #region btnOk_Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Email = txtEmail.Text;
                Password = txtPassword.Text;
                ValidationCode = txtValidationCode.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("AccountTree.DlgImageCode.btnOK_Click", ex);
            }
        }
        #endregion
        
    }
}