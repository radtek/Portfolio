using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmMessageBox : Form
    {
        private ShrinkArea shrinkAreaADetail;

        public FrmMessageBox()
        {
            InitializeComponent();
            shrinkAreaADetail = new ShrinkArea(this, 100, 250);
            shrinkAreaADetail.Expanded = false;
        }

        //public string Message
        //{
        //    set
        //    {
        //        if (!String.IsNullOrEmpty(value))
        //        {
        //            lblMessage.Text = value;
        //        }
        //    }
        //}

        //public string FullMessage
        //{
        //    set
        //    {
        //        if (!String.IsNullOrEmpty(value))
        //        {
        //            txtFullMessage.Text = value;
        //        }
        //    }
        //}

        public Exception Exception
        {
            set 
            {
                if (value != null)
                {
                    lblMessage.Text = value.Message;

                    string strMsg = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "Message: {0}\r\nSource: {1}\r\nTargetSite: {2}\r\nStack Trace: {3}\r\n", value.Message, value.Source, value.TargetSite, value.StackTrace);

                    txtFullMessage.Text = strMsg;
                }
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                shrinkAreaADetail.Toggle();

                // Update button text depending on state of shrink area.

                if (shrinkAreaADetail.Expanded)
                {
                    this.btnDetail.Text = "隐藏详细信息";
                }
                else
                {
                    this.btnDetail.Text = "显示详细信息";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}