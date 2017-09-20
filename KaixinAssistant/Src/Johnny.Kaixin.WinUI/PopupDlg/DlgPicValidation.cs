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
    public partial class DlgPicValidation : Form
    {
        private string _email = "";
        private string _password = "";
        private string _validationCode = "";
        private bool _isRemember = false;

        public DlgPicValidation()
        {
            InitializeComponent();
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

        public string ValidationCode
        {
            get { return _validationCode; }
            set { _validationCode = value; }
        }

        public bool IsRemember
        {
            get { return _isRemember; }
            set
            {
                _isRemember = value;
                chkRemember.Checked = _isRemember;
            }
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
        
        private void DlgPicValidation_Load(object sender, EventArgs e)
        {
            try
            {
                txtValidationCode.Select();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgPicdValidation.DlgPicValidation_Load", ex);
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
                IsRemember = chkRemember.Checked;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgPicdValidation.btnOK_Click", ex);
            }
        }
        #endregion
  
        private void btnConnection_Click(object sender, EventArgs e)
        {
            try
            {
                DlgProxySetting frmProxy = new DlgProxySetting();
                frmProxy.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgPicdValidation.btnConnection_Click", ex);
            }
        }

    }
}