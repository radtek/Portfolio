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
    public partial class DlgAddGroup : Form
    {
        private string _groupname;

        public DlgAddGroup()
        {
            InitializeComponent();
        }

        #region DlgAddGroup_Load
        private void DlgAddGroup_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(_groupname))
                {
                    txtGroupName.Text = _groupname;
                    this.Text = "修改组名";
                }
                else
                {
                    this.Text = "添加新组";
                }
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
                if (txtGroupName.Text == string.Empty)
                {
                    txtGroupName.Select();
                    MessageBox.Show("组名称不能为空！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!DataValidation.IsWindowsFileName(txtGroupName.Text))
                {
                    txtGroupName.Select();
                    MessageBox.Show("组名称不能包含下列任何字符之一：\r\n\\/:*?\"<>|", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                _groupname = txtGroupName.Text;

                string ret = ConfigCtrl.AddGroup(_groupname);
                if (ret != Constants.STATUS_SUCCESS)
                {
                    MessageBox.Show(ret, Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGroupName.Select();
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();

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

        #region Properties
        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }
        #endregion

    }
}