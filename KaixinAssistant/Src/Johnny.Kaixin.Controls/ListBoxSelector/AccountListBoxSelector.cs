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
    public partial class AccountListBoxSelector : UserControl
    {
        private Collection<AccountInfo> _allitems;

        public AccountListBoxSelector()
        {
            InitializeComponent();
            _allitems = new Collection<AccountInfo>();
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
                ErrorHandler.ShowMessageBox("AccountListBoxSelector", ex);
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
                ErrorHandler.ShowMessageBox("AccountListBoxSelector", ex);
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
                ErrorHandler.ShowMessageBox("AccountListBoxSelector", ex);
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
                ErrorHandler.ShowMessageBox("AccountListBoxSelector", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<AccountInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (AccountInfo account in _allitems)
                    {
                        lstAllItems.Items.Add(account);
                    }
                }
            }
        }

        public Collection<AccountInfo> SelectedItems
        {
            get
            {
                Collection<AccountInfo> accounts = new Collection<AccountInfo>();
                foreach (object item in lstSelectedItems.Items)
                {
                    AccountInfo account = item as AccountInfo;
                    if (account != null)
                    {
                        accounts.Add(account);
                    }
                }
                return accounts;
            }
        }
        #endregion
    }
}
