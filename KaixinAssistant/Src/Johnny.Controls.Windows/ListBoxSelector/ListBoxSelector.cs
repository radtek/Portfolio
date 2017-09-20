using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;

namespace Johnny.Controls.Windows.ListBoxSelector
{
    public partial class ListBoxSelector : UserControl
    {
        private Collection<object> _allitems;

        public ListBoxSelector()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            lstAllItems.Items.Clear();
            lstSelectedItems.Items.Clear();
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
            }
        }
        #endregion

        public Collection<object> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                foreach (object item in _allitems)
                {
                    lstAllItems.Items.Add(item);
                }
            }
        }
   }
}
