using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.Controls.ListBoxSelector
{
    public partial class FriendListBoxSelector : UserControl
    {
        private Collection<FriendInfo> _allitems;

        public FriendListBoxSelector()
        {
            InitializeComponent();
            _allitems = new Collection<FriendInfo>();
        }

        public void Clear()
        {
            lstAllItems.Items.Clear();
            lstSelectedItems.Items.Clear();
        }

        public new void Select()
        {
            lstAllItems.Select();
        }

        #region list Select Event
        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllItems.SelectedItems)
                {
                    lstSelectedItems.Items.Add(item);
                }
                for (int ix = lstAllItems.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstAllItems.Items.Remove(lstAllItems.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("FriendListBoxSelector", ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllItems.Items)
                {
                    lstSelectedItems.Items.Add(item);
                }
                for (int ix = lstAllItems.Items.Count - 1; ix >= 0; ix--)
                    lstAllItems.Items.Remove(lstAllItems.Items[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("FriendListBoxSelector", ex);
            }
        }

        private void btnUnselectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstSelectedItems.SelectedItems)
                {
                    lstAllItems.Items.Add(item);
                }
                for (int ix = lstSelectedItems.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstSelectedItems.Items.Remove(lstSelectedItems.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("FriendListBoxSelector", ex);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstSelectedItems.Items)
                {
                    lstAllItems.Items.Add(item);
                }
                for (int ix = lstSelectedItems.Items.Count - 1; ix >= 0; ix--)
                    lstSelectedItems.Items.Remove(lstSelectedItems.Items[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("FriendListBoxSelector", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<FriendInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (FriendInfo friend in _allitems)
                    {
                        lstAllItems.Items.Add(friend);
                    }
                }
            }
        }

        public Collection<FriendInfo> SelectedItems
        {
            get
            {
                Collection<FriendInfo> friends = new Collection<FriendInfo>();
                foreach (object item in lstSelectedItems.Items)
                {
                    FriendInfo friend = item as FriendInfo;
                    if (friend != null)
                    {
                        friends.Add(friend);
                    }
                }
                return friends;
            }
        }
        #endregion
    }
}
