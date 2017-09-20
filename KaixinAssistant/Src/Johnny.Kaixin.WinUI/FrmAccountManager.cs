using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

using WeifenLuo.WinFormsUI.Docking;

using Johnny.Kaixin.Controls.AccountTree;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmAccountManager : DockContent
    {
        public FrmAccountManager()
        {
            InitializeComponent();
        }

        private void FrmAccountManager_Load(object sender, EventArgs e)
        {
            try
            {                
                accountTree.OpenGroupConfigEvent += new AccountTree.OpenGroupConfigEventHandler(accountTree_OpenGroupConfigEvent);
                accountTree.OpenAccountEvent += new Johnny.Kaixin.Controls.AccountTree.AccountTree.OpenAccountEventHandler(accountTree_OpenUserEvent);
                accountTree.OpenOperationConfigEvent += new AccountTree.OpenOperationConfigEventHandler(accountTree_OpenOperationConfigEvent);
                accountTree.OpenInBrowserEvent += new Johnny.Kaixin.Controls.AccountTree.AccountTree.OpenInBrowserEventHandler(accountTree_OpenInBrowserEvent);
                accountTree.AccountNodeSelectedEvent += new Johnny.Kaixin.Controls.AccountTree.AccountTree.AccountNodeSelectedEventHandler(accountTree_AccountNodeSelectedEvent);
                accountTree.GroupNodeSelectedEvent += new AccountTree.GroupNodeSelectedEventHandler(accountTree_GroupNodeSelectedEvent);
                accountTree.RootNodeSelectedEvent += new Johnny.Kaixin.Controls.AccountTree.AccountTree.RootNodeSelectedEventHandler(accountTree_RootNodeSelectedEvent);
                accountTree.InitialNodes();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }

        void accountTree_OpenGroupConfigEvent(object sender, GroupEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.OpenGroupConfigFile(e.Group);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }

        private void accountTree_AccountNodeSelectedEvent(object sender, Johnny.Kaixin.Controls.AccountTree.AccountEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.AccountNodeSelected(e.Account);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }


        void accountTree_OpenOperationConfigEvent(object sender, AccountEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.OpenOperationConfigFile(e.Group, e.Account);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }

        void accountTree_GroupNodeSelectedEvent(object sender, GroupEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.AccountGroupNodeSelected(e.Accounts, e.Group);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }
        private void accountTree_RootNodeSelectedEvent(object sender, Johnny.Kaixin.Controls.AccountTree.RootNodeEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.AccountRootNodeSelected(e.Groups);

            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }

        void accountTree_OpenInBrowserEvent(object sender, Johnny.Kaixin.Controls.AccountTree.AccountEventArgs e)
        {
            try
            {
                //get the top form
                MainForm mainform = this.TopLevelControl as MainForm;
                if (mainform != null)
                    mainform.ShowWebBrowser(e.Account);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }

        void accountTree_OpenUserEvent(object sender, Johnny.Kaixin.Controls.AccountTree.AccountEventArgs e)
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
                Program.ShowMessageBox("FrmAccountManager", ex);
            }
        }
        
    }
}