using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.Controls.ListBoxSelector
{
    public partial class FishMaturedListViewSelector : UserControl
    {
        private Collection<FishMaturedInfo> _allitems;
        private Collection<int> _selecteditems;

        public FishMaturedListViewSelector()
        {
            InitializeComponent();
            _allitems = new Collection<FishMaturedInfo>();
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
                foreach (ListViewItem item in lstAllItems.SelectedItems)
                {
                    lstAllItems.Items.Remove(item);
                    lstSelectedItems.Items.Add(item);
                }
            }
            catch(Exception ex)
            {
                ErrorHandler.ShowMessageBox("FishMaturedListViewSelector", ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lstAllItems.Items)
                {
                    lstAllItems.Items.Remove(item);
                    lstSelectedItems.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("FishMaturedListViewSelector", ex);
            }
        }

        private void btnUnselectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lstSelectedItems.SelectedItems)
                {
                    lstSelectedItems.Items.Remove(item);
                    lstAllItems.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("FishMaturedListViewSelector", ex);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lstSelectedItems.Items)
                {
                    lstSelectedItems.Items.Remove(item);
                    lstAllItems.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowMessageBox("FishMaturedListViewSelector", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<FishMaturedInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (FishMaturedInfo fish in _allitems)
                    {
                        string[] subItem = new string[4];
                        subItem[0] = fish.FId.ToString();
                        subItem[1] = fish.Name;
                        subItem[2] = fish.Price.ToString();
                        subItem[3] = fish.MaxWeight.ToString();
                        if (_selecteditems.Contains(fish.FId))
                            lstSelectedItems.Items.Add(new ListViewItem(subItem));
                        else
                            lstAllItems.Items.Add(new ListViewItem(subItem));
                    }
                }
            }
        }

        public Collection<int> SelectedItems
        {
            get
            {
                Collection<int> fishes = new Collection<int>();
                foreach (ListViewItem item in lstSelectedItems.Items)
                {
                    if (item != null)
                    {
                        fishes.Add(DataConvert.GetInt32(item.SubItems[0].Text));
                    }
                }
                return fishes;
            }
            set 
            {
                _selecteditems = value;
            }
        }
        #endregion

        public void SetSelectedTitle(string title)
        {
            lblForbiddenFishes.Text = title;
        } 
    }
}
