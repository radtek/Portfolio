using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;

using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class DlgSelectionBase : Form
    {
        private KaixinBase _kaixinBase;

        private Collection<FriendInfo> _gameuser;
        private Collection<FriendInfo> _selecteduser;
        private Collection<FriendInfo> _allmyfriend;

        private string _radioAllCaption;
        private string _radioGameCaption;
        private string _columnCategory;

        protected virtual void FormLoad() { }
        protected virtual void RefreshAllFriends() { }
        protected virtual void RefreshGameFriends() { }        
        protected virtual string[] BuildListView(FriendInfo user) { return new string[2];}
        protected readonly string VALIDATION_CAPTION = "配置黑白名单-选择好友";

        public DlgSelectionBase()
        {
            InitializeComponent();
            
            _kaixinBase = new KaixinBase();
        }

        #region Form_Load
        private void DlgSelectionBase_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "选择好友 By Johnny";
                if (!String.IsNullOrEmpty(_radioAllCaption))
                    rdbAllFriends.Text = _radioAllCaption;
                if (!String.IsNullOrEmpty(_radioGameCaption))
                    rdbGameFriends.Text = _radioGameCaption;
                lstViewFriend.Columns[2].Text = _columnCategory;
                rdbGameFriends.Checked = true;
                BuildListView(_gameuser);
                FormLoad();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSelectionBase", ex);
            }
        }
        #endregion

        #region Form Closing
        private void DlgSelectionBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _kaixinBase.StopThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSelectionBase", ex);
            }
        }
        #endregion

        #region btnOK_Click
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                _selecteduser = new Collection<FriendInfo>();
                foreach (ListViewItem item in lstViewFriend.SelectedItems)
                {
                    FriendInfo user = new FriendInfo();
                    user.Name = item.SubItems[0].Text;
                    user.Id = Convert.ToInt32(item.SubItems[1].Text);                    
                    _selecteduser.Add(user);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSelectionBase", ex);
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                EnableControls(false);

                if (rdbGameFriends.Checked)
                {                    
                    RefreshGameFriends();
                }
                else
                {
                    RefreshAllFriends();
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSelectionBase", ex);
            }
        }
        #endregion

        #region EnableControls
        protected void EnableControls(bool enable)
        {
            if (enable)
            {
                btnOK.Enabled = true;
                btnRefresh.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
                btnRefresh.Enabled = false;
                btnCancel.Enabled = false;
            }
        }
        #endregion

        #region rdbAllFriends_CheckedChanged
        private void rdbAllFriends_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BuildListView(_allmyfriend);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSelectionBase", ex);
            }
        }
        #endregion

        #region rdbGameFriends_CheckedChanged
        private void rdbGameFriends_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BuildListView(_gameuser);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSelectionBase", ex);
            }
        }
        #endregion

        #region BuildListView
        protected void BuildListView(Collection<FriendInfo> friends)
        {
            if (friends == null)
                return;

            lstViewFriend.Items.Clear();
            foreach (FriendInfo user in friends)
            {
                //override                
                //string[] subItem = new string[3];
                //subItem[0] = user.Name;
                //subItem[1] = user.Id.ToString();
                //subItem[2] = user.Status;
                //lstViewFriend.Items.Add(new ListViewItem(subItem));
                lstViewFriend.Items.Add(new ListViewItem(BuildListView(user)));
            }
        }
        #endregion

        #region Properties
        protected KaixinBase KaixinBase
        {
            set { _kaixinBase = value; }
        }

        public Collection<FriendInfo> GameUser
        {
            get { return _gameuser; }
            set { _gameuser = value; }
        }

        public Collection<FriendInfo> SelectedUser
        {
            get { return _selecteduser; }
            set { _selecteduser = value; }
        }

        public Collection<FriendInfo> AllMyFriend
        {
            get { return _allmyfriend; }
            set { _allmyfriend = value; }
        }

        protected virtual string RadioAllCaption
        {
            get { return _radioAllCaption; }
            set { _radioAllCaption = value; }
        }

        protected virtual string RadioGameCaption
        {
            get { return _radioGameCaption; }
            set { _radioGameCaption = value; }
        }

        protected virtual string ColumnCategory
        {
            get { return _columnCategory; }
            set { _columnCategory = value; }
        }   
        #endregion

    }
}