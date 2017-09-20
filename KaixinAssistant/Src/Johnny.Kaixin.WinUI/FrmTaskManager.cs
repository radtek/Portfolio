using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

using Johnny.Kaixin.Controls.TaskTree;

using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmTaskManager : DockContent
    {
        public FrmTaskManager()
        {
            InitializeComponent();
        }

        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            taskTree.RightToLeftLayout = RightToLeftLayout;
        }

        private void FrmTaskManager_Load(object sender, EventArgs e)
        {
            try
            {                
                taskTree.OpenTaskEditorEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.OpenTaskEditorEventHandler(taskTree_OpenTaskEditorEvent);
                taskTree.OpenTaskConfigFileEvent += new TaskTree.OpenTaskConfigFileEventHandler(taskTree_OpenTaskConfigFileEvent);
                taskTree.StartTaskEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.StartTaskEventHandler(taskTree_StartTaskEvent);
                taskTree.StopTaskEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.StopTaskEventHandler(taskTree_StopTaskEvent);
                taskTree.TaskNodeSelectedEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.TaskNodeSelectedEventHandler(taskTree_TaskNodeSelectedEvent);
                taskTree.RootNodeSelectedEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.RootNodeSelectedEventHandler(taskTree_RootNodeSelectedEvent);
                taskTree.OperationNodeSelectedEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.OperationNodeSelectedEventHandler(taskTree_OperationNodeSelectedEvent);
                taskTree.TaskNameChangedEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.TaskNameChangedEventHandler(taskTree_TaskNameChangedEvent);
                taskTree.OpenAccountEvent += new Johnny.Kaixin.Controls.TaskTree.TaskTree.OpenAccountEventHandler(taskTree_OpenUserEvent);
                taskTree.InitialNodes();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", "读取任务配置文件失败！\r\n请尝试删除以下目录中的所有文件，重新配置任务！\r\n" + Path.Combine(Application.StartupPath, "Tasks"), ex);
            }
        }

        void taskTree_OpenTaskConfigFileEvent(object sender, TaskEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.OpenTaskConfigFile(e.Task);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }
        
        public void RefreshTaskNode(string taskid, string taskname)
        {
            taskTree.RefreshTaskNode(taskid, taskname);
        }

        private void taskTree_OperationNodeSelectedEvent(object sender, Johnny.Kaixin.Controls.TaskTree.OperationEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.OperationNodeSelected(e.Operation);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

        private void taskTree_RootNodeSelectedEvent(object sender, Johnny.Kaixin.Controls.TaskTree.RootNodeEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.RootNodeSelected(e.Tasks);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

        private void taskTree_TaskNodeSelectedEvent(object sender, Johnny.Kaixin.Controls.TaskTree.TaskEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.TaskNodeSelected(e.Task, e.Operations);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

        private void taskTree_StopTaskEvent(object sender, Johnny.Kaixin.Controls.TaskTree.TaskEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.StopTask(e.Task.TaskId, e.Task.TaskName);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

        private void taskTree_StartTaskEvent(object sender, Johnny.Kaixin.Controls.TaskTree.TaskEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.StartTask(e.Task.TaskId, e.Task.TaskName);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

        private void taskTree_OpenTaskEditorEvent(object sender, Johnny.Kaixin.Controls.TaskTree.TaskEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.ShowTaskEditor(e.Task);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

        private void taskTree_TaskNameChangedEvent(object sender, Johnny.Kaixin.Controls.TaskTree.TaskEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.ChangeTaskEditTabName(e.Task);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

        private void taskTree_OpenUserEvent(object sender, Johnny.Kaixin.Controls.TaskTree.AccountEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.ShowUserDetail(e.Group, e.Account);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskManager", ex);
            }
        }

    }
}