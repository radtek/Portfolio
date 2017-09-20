using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.Controls.TaskTree
{
    public partial class DlgAddTask : Form
    {
        private TaskInfo _task;

        public DlgAddTask()
        {
            InitializeComponent();
        }

        #region DlgAddTask_Load
        private void DlgAddTask_Load(object sender, EventArgs e)
        {
            try
            {
                if (_task != null && _task.TaskId != string.Empty)
                {
                    txtTaskName.Text = _task.TaskName;
                    this.Text = "修改任务名";
                }
                else
                {
                    _task = new TaskInfo();
                    this.Text = "添加新任务";
                }
            }
            catch (Exception ex)
            {                
                ErrorHandler.ShowMessageBox(TaskConstants.EXCEPTION_MODULE, ex);
            }
        }
        #endregion

        #region btnOk_Click
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaskName.Text == string.Empty)
                {
                    txtTaskName.Select();
                    MessageBox.Show("任务名称不能为空！", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                _task.TaskName = txtTaskName.Text;

                string ret = ConfigCtrl.EditTask(_task);
                if (ret != Constants.STATUS_SUCCESS)
                {
                    MessageBox.Show(ret, Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TaskConstants.EXCEPTION_MODULE, ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Properties

        public TaskInfo Task
        {
            get { return _task; }
            set { _task = value; }
        }

        #endregion
        
    }
}