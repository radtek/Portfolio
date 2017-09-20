using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Johnny.Kaixin.WinUI
{
    public partial class DlgPicCode : Form
    {
        private string _validationCode = "";

        public DlgPicCode()
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

        public string ValidationCode
        {
            set { _validationCode = value; }
            get { return _validationCode; }
        }

        public byte[] ValidationImage
        {
            set
            {
                if (value.Length > 0)
                {
                    MemoryStream stream = new MemoryStream();
                    stream.Write(value, 0, value.Length);
                    imgValidateCode.Image = Image.FromStream(stream);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationCode = txtValidateCode.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgPicCode.btnOK_Click", ex);
            }
        }
    }
}