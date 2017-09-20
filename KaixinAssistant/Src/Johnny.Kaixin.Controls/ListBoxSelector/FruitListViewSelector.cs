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
    public partial class FruitListViewSelector : UserControl
    {
        private Collection<FruitInfo> _allitems;
        private Collection<int> _selecteditems;

        public FruitListViewSelector()
        {
            InitializeComponent();            
            _allitems = new Collection<FruitInfo>();
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
                ErrorHandler.ShowMessageBox("FruitListViewSelector.btnSelectOne_Click", ex);
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
                ErrorHandler.ShowMessageBox("FruitListViewSelector.btnSelectAll_Click", ex);
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
                ErrorHandler.ShowMessageBox("FruitListViewSelector.btnUnselectOne_Click", ex);
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
                ErrorHandler.ShowMessageBox("FruitListViewSelector.btnUnselectAll_Click", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<FruitInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (FruitInfo fruit in _allitems)
                    {
                        string[] subItem = new string[3];
                        subItem[0] = fruit.FruitId.ToString();
                        subItem[1] = fruit.Name;
                        subItem[2] = fruit.SellPrice.ToString();
                        if (_selecteditems.Contains(fruit.FruitId))
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
                Collection<int> fruits = new Collection<int>();
                foreach (ListViewItem item in lstSelectedItems.Items)
                {
                    if (item != null)
                    {
                        fruits.Add(DataConvert.GetInt32(item.SubItems[0].Text));
                    }
                }
                return fruits;
            }
            set 
            {
                _selecteditems = value;
            }
        }
        #endregion

        public void SetSelectedTitle(string title)
        {
            lblStealFruits.Text = title;
        } 
    }
}
