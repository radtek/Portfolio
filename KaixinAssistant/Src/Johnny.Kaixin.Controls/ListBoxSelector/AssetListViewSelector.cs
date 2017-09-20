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
    public partial class AssetListViewSelector : UserControl
    {
        private Collection<AssetInfo> _allitems;
        private Collection<int> _selecteditems;

        public AssetListViewSelector()
        {
            InitializeComponent();
            _allitems = new Collection<AssetInfo>();
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
                ErrorHandler.ShowMessageBox("AssetListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("AssetListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("AssetListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("AssetListViewSelector", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<AssetInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (AssetInfo asset in _allitems)
                    {
                        string[] subItem = new string[3];
                        subItem[0] = asset.IId.ToString();
                        subItem[1] = asset.Name;
                        subItem[2] = asset.StandardPrice.ToString();
                        if (_selecteditems.Contains(asset.IId))
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
                Collection<int> assets = new Collection<int>();
                foreach (ListViewItem item in lstSelectedItems.Items)
                {
                    if (item != null)
                    {
                        assets.Add(DataConvert.GetInt32(item.SubItems[0].Text));
                    }
                }
                return assets;
            }
            set 
            {
                _selecteditems = value;
            }
        }
        #endregion

        public void SetSelectedTitle(string title)
        {
            lblBuyAssets.Text = title;
        } 
    }
}
