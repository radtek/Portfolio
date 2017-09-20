using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.Controls.AccountTree
{
    public class AccountTree : TreeView
    {
        #region Variable
  
        private System.ComponentModel.IContainer components;
        private bool DoubleClicked = false;
        private ImageList imageListIcon;
        private ContextMenuStrip contextmenustrip = new ContextMenuStrip();

        #endregion

        #region OnOpenGroupConfig
        public delegate void OpenGroupConfigEventHandler(object sender, GroupEventArgs e);
        public event OpenGroupConfigEventHandler OpenGroupConfigEvent;
        protected virtual void OnOpenGroupConfig(GroupEventArgs e)
        {
            if (OpenGroupConfigEvent != null)
                OpenGroupConfigEvent(this, e);
        }
        #endregion

        #region OnOpenAccount
        public delegate void OpenAccountEventHandler(object sender, AccountEventArgs e);
        public event OpenAccountEventHandler OpenAccountEvent;
        protected virtual void OnOpenAccount(AccountEventArgs e)
        {
            if (OpenAccountEvent != null)
                OpenAccountEvent(this, e);
        }
        #endregion

        #region OnOpenOperationConfig
        public delegate void OpenOperationConfigEventHandler(object sender, AccountEventArgs e);
        public event OpenOperationConfigEventHandler OpenOperationConfigEvent;
        protected virtual void OnOpenOperationConfig(AccountEventArgs e)
        {
            if (OpenOperationConfigEvent != null)
                OpenOperationConfigEvent(this, e);
        }
        #endregion

        #region OnOpenInBrowser
        public delegate void OpenInBrowserEventHandler(object sender, AccountEventArgs e);
        public event OpenInBrowserEventHandler OpenInBrowserEvent;
        protected virtual void OnOpenInBrowser(AccountEventArgs e)
        {
            if (OpenInBrowserEvent != null)
                OpenInBrowserEvent(this, e);
        }
        #endregion

        #region OnAccountNodeSelected
        public delegate void AccountNodeSelectedEventHandler(object sender, AccountEventArgs e);
        public event AccountNodeSelectedEventHandler AccountNodeSelectedEvent;
        protected virtual void OnAccountNodeSelected(AccountEventArgs e)
        {
            if (AccountNodeSelectedEvent != null)
                AccountNodeSelectedEvent(this, e);
        }
        #endregion

        #region OnGroupNodeSelected
        public delegate void GroupNodeSelectedEventHandler(object sender, GroupEventArgs e);
        public event GroupNodeSelectedEventHandler GroupNodeSelectedEvent;
        protected virtual void OnGroupNodeSelected(GroupEventArgs e)
        {
            if (GroupNodeSelectedEvent != null)
                GroupNodeSelectedEvent(this, e);
        }
        #endregion

        #region OnRootNodeSelected
        public delegate void RootNodeSelectedEventHandler(object sender, RootNodeEventArgs e);
        public event RootNodeSelectedEventHandler RootNodeSelectedEvent;
        protected virtual void OnRootNodeSelected(RootNodeEventArgs e)
        {
            if (RootNodeSelectedEvent != null)
                RootNodeSelectedEvent(this, e);
        }
        #endregion

        #region Ctor
        public AccountTree()
        {
            this.ShowNodeToolTips = true;
            this.ContextMenuStrip = contextmenustrip;
            this.BeforeExpand += new TreeViewCancelEventHandler(OnBeforeExpand);
            this.MouseDown += new MouseEventHandler(OnMouseDown);
            this.DoubleClick += new EventHandler(OnDoubleClick);
            this.AfterSelect += new TreeViewEventHandler(OnAfterSelect);
            imageListIcon = new ImageList();
        }        
        #endregion

        #region InitializeComponent
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageListIcon = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageListIcon
            // 
            this.imageListIcon.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListIcon.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.ResumeLayout(false);
        }
        #endregion

        #region Initlize AccountTree
        public void InitialNodes()
        {
            //set images
            imageListIcon.ColorDepth = ColorDepth.Depth32Bit;//不然图片会失真，周围会有黑线
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_USERS));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_USER));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_ADDUSER));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_KEYS));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_DELETE));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_WEBBROWSER));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_OPERATIONCONFIG));
            imageListIcon.Images.Add(ImageCtrl.GetIconFromResx(TreeConstants.IMAGE_REFRESH));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_ROOT));
            imageListIcon.Images.Add(IconCtrl.GetIconFromResx(TreeConstants.ICON_XML));

            this.ImageList = imageListIcon;  

            //set nodes
            base.Nodes.Clear();

            //build Root node
            BaseNode Root = new BaseNode("所有帐号");
            base.Nodes.Add(Root);

            //build Groups node
            string[] groups = ConfigCtrl.GetGroups();
            foreach (string group in groups)
            {
                GroupNode gn = new GroupNode(group);
                Root.Nodes.Add(gn);

                //build Accounts node
                Collection<AccountInfo> accounts = ConfigCtrl.GetAccounts(group);
                if (accounts != null)
                {
                    gn.Text = gn.Text + "(" + accounts.Count + ")";
                    foreach (AccountInfo account in accounts)
                    {
                        AccountNode sn = new AccountNode(account);
                        gn.Nodes.Add(sn);
                    }
                }
            }
            
            if (Root.Nodes.Count > 0)
            {
                Root.Expand();
            }
        }        

        #endregion        
  
        #region Override Tree Event

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    contextmenustrip.Items.Clear();

                    //onfocus when right click on the node
                    BaseNode bn = (BaseNode)this.GetNodeAt(e.X, e.Y);
                    if (bn == null)
                        return;
                    else
                        this.SelectedNode = bn;

                    switch (bn.NodeType)
                    {
                        //BaseNode
                        case NodeType.Base:
                            contextmenustrip.Items.Add("添加新组");
                            contextmenustrip.Items.Add("刷新");                            
                            contextmenustrip.Items[0].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Users)];
                            contextmenustrip.Items[0].Click += new EventHandler(OnAddGroupClick);
                            contextmenustrip.Items[1].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Refresh)];
                            contextmenustrip.Items[1].Click += new EventHandler(OnRefreshGroupsClick);
                            break;
                        //GroupNode
                        case NodeType.Group:
                            contextmenustrip.Items.Add("添加帐号");
                            contextmenustrip.Items.Add("刷新");
                            contextmenustrip.Items.Add("打开组配置文件");
                            contextmenustrip.Items.Add(new ToolStripSeparator());
                            contextmenustrip.Items.Add("删除");
                            contextmenustrip.Items[0].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.AddUser)];
                            contextmenustrip.Items[0].Click += new EventHandler(OnAddAccountClick);
                            contextmenustrip.Items[1].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Refresh)];
                            contextmenustrip.Items[1].Click += new EventHandler(OnRefreshAccountsClick);
                            contextmenustrip.Items[2].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Xml)];
                            contextmenustrip.Items[2].Click += new EventHandler(OnOpenGroupConfigClick);
                            contextmenustrip.Items[4].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Delete)];
                            contextmenustrip.Items[4].Click += new EventHandler(OnDeleteGroupClick);
                            break;
                        //UserNode
                        case NodeType.Account:
                            contextmenustrip.Items.Add("配置黑白名单");
                            contextmenustrip.Items.Add("打开配置文件");
                            contextmenustrip.Items.Add(new ToolStripSeparator());
                            contextmenustrip.Items.Add("在浏览器中打开");
                            contextmenustrip.Items.Add("删除");
                            contextmenustrip.Items.Add(new ToolStripSeparator());
                            contextmenustrip.Items.Add("登录信息");

                            contextmenustrip.Items[0].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.OperationConfig)];
                            contextmenustrip.Items[0].Click += new EventHandler(OnOpenAccountClick);
                            contextmenustrip.Items[1].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Xml)];
                            contextmenustrip.Items[1].Click += new EventHandler(OnOpenOperationConfigClick);                            
                            contextmenustrip.Items[3].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.WebBrowser)];
                            contextmenustrip.Items[3].Click += new EventHandler(OnOpenInBrowserClick);
                            contextmenustrip.Items[4].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Delete)];
                            contextmenustrip.Items[4].Click += new EventHandler(OnDeleteClick);
                            contextmenustrip.Items[6].Image = imageListIcon.Images[DataConvert.GetInt32(IconType.Keys)];
                            contextmenustrip.Items[6].Click += new EventHandler(OnEditAccountClick);
                            break;
                        default:
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            try
            {
                OnOpenAccountClick(sender, e);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }

        private void OnBeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            try
            {
                if (DoubleClicked)
                {
                    e.Cancel = true;
                    DoubleClicked = false;
                    e.Node.Collapse();
                }
                else
                {
                    if (e.Node.ImageIndex == 1)
                    {
                        //ShowConnection((DataNode)e.Node);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }
        
        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.SelectedNode is GroupNode)
                {
                    GroupNode bn = this.SelectedNode as GroupNode;
                    if (bn != null)
                    {
                        GroupEventArgs ge = new GroupEventArgs();
                        ge.Group = bn.GroupName;
                        ge.Accounts = new Collection<AccountInfo>();
                        foreach (AccountNode accountnode in bn.Nodes)
                        {
                            ge.Accounts.Add(accountnode.Account);
                        }
                        OnGroupNodeSelected(ge);
                    }
                }
                else if (this.SelectedNode is AccountNode)
                {
                    AccountNode an = this.SelectedNode as AccountNode;
                    AccountEventArgs ae = new AccountEventArgs();
                    if (an != null)
                    {
                        ae.Account = an.Account;
                    }
                    else
                        ae.Account = null;
                    OnAccountNodeSelected(ae);
                }                
                else if (this.SelectedNode is BaseNode)
                {
                    BaseNode bn = this.SelectedNode as BaseNode;
                    if (bn != null)
                    {
                        RootNodeEventArgs re = new RootNodeEventArgs();                        
                        re.Groups = new string[bn.Nodes.Count];
                        for (int ix = 0; ix < bn.Nodes.Count; ix++)
                        {
                            GroupNode gn = bn.Nodes[ix] as GroupNode;
                            if (gn != null)
                                re.Groups[ix] = gn.GroupName;
                        }
                        OnRootNodeSelected(re);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }
        #endregion

        #region Custom Tree Event

        #region Root Node
        private void OnAddGroupClick(object sender, EventArgs e)
        {
            try
            {
                DlgAddGroup adg = new DlgAddGroup();
                if (adg.ShowDialog() == DialogResult.OK)
                {
                    BaseNode bn = (BaseNode)this.SelectedNode;
                    GroupNode gn = new GroupNode(adg.GroupName);
                    bn.Nodes.Add(gn);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }

        private void OnRefreshGroupsClick(object sender, EventArgs e)
        {
            try
            {
                InitialNodes();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }
        #endregion

        #region Group Node
        private void OnAddAccountClick(object sender, EventArgs e)
        {
            try
            {
                GroupNode gn = this.SelectedNode as GroupNode;
                if (gn != null)
                {
                    DlgAddAccount adu = new DlgAddAccount();
                    adu.GroupName = gn.GroupName;
                    if (adu.ShowDialog() == DialogResult.OK)
                    {
                        AccountNode un = new AccountNode(adu.Account);
                        gn.Nodes.Add(un);
                        gn.Text = gn.GroupName + "(" + gn.Nodes.Count + ")";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }

        private void OnRefreshAccountsClick(object sender, EventArgs e)
        {
            try
            {
                InitialNodes();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }


        private void OnOpenGroupConfigClick(object sender, EventArgs e)
        {
            try
            {
                GroupNode gn = this.SelectedNode as GroupNode;
                if (gn != null)
                {
                    GroupEventArgs ge = new GroupEventArgs();
                    ge.Group = gn.GroupName;
                    OnOpenGroupConfig(ge);
                }                
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }

        private void OnDeleteGroupClick(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认要删除么？此组下的所有账号都会被删除。", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    GroupNode gn = this.SelectedNode as GroupNode;
                    if (gn != null)
                    {
                        string ret = ConfigCtrl.DeleteGroup(gn.GroupName);
                        if (ret != Constants.STATUS_SUCCESS)
                        {
                            MessageBox.Show(ret, Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        BaseNode bn = (BaseNode)gn.Parent;
                        bn.Nodes.Remove(gn);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }
        #endregion

        #region Account Node
        private void OnOpenAccountClick(object sender, EventArgs e)
        {
            try
            {                
                AccountNode un = this.SelectedNode as AccountNode;               
                if (un != null)
                {
                    AccountEventArgs te = new AccountEventArgs();
                    GroupNode gn = un.Parent as GroupNode;
                    te.Group = gn.GroupName;
                    te.Account = un.Account;
                    OnOpenAccount(te);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }

        private void OnOpenOperationConfigClick(object sender, EventArgs e)
        {
            try
            {               
                AccountNode an = this.SelectedNode as AccountNode;
                if (an != null)
                {
                    AccountEventArgs te = new AccountEventArgs();
                    GroupNode gn = an.Parent as GroupNode;
                    te.Group = gn.GroupName;
                    te.Account = an.Account;
                    OnOpenOperationConfig(te);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }        

        private void OnOpenInBrowserClick(object sender, EventArgs e)
        {
            try
            {                
                AccountNode un = this.SelectedNode as AccountNode;
                if (un != null)
                {
                    AccountEventArgs te = new AccountEventArgs();
                    te.Account = un.Account;
                    OnOpenInBrowser(te);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }
        
        private void OnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认要删除么？", Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    AccountNode un = this.SelectedNode as AccountNode;
                    AccountInfo account = null;
                    if (un != null)
                        account = un.Account;
                    if (account != null)
                    {
                        GroupNode gn = (GroupNode)un.Parent;
                        string ret = ConfigCtrl.DeleteAccount(gn.GroupName, account);
                        if (ret != Constants.STATUS_SUCCESS)
                        {
                            MessageBox.Show(ret, Constants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        gn.Nodes.Remove(un);
                        gn.Text = gn.GroupName + "(" + gn.Nodes.Count + ")";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }

        private void OnEditAccountClick(object sender, EventArgs e)
        {
            try
            {
                AccountNode un = this.SelectedNode as AccountNode;
                if (un != null)
                {                    
                    GroupNode gn = un.Parent as GroupNode;
                    if (gn != null)
                    {
                        DlgAddAccount adu = new DlgAddAccount();
                        adu.GroupName = gn.GroupName;
                        adu.OldEmail = un.Account.Email;
                        adu.Account = un.Account;
                        if (adu.ShowDialog() == DialogResult.OK)
                        {
                            un.Text = adu.Account.UserName;
                            un.Account = adu.Account;
                            OnAfterSelect(null, null);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox(TreeConstants.EXCEPTION_MODULE, ex);
            }
        }
        #endregion

        #endregion
    }
}
