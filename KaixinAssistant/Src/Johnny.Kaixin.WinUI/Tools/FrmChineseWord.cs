using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Johnny.Kaixin.Helper;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmChineseWord : FrmToolBase
    {
        public FrmChineseWord()
        {
            InitializeComponent();
        }        

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                txtPinyin.Text = Hz2PyHelp.Convert(txtChinese.Text);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmChineseWord", ex);
            }
        }
    }
}