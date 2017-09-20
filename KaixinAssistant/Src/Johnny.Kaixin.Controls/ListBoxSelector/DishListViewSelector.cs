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
    public partial class DishListViewSelector : UserControl
    {
        private Collection<DishInfo> _allitems;
        private Collection<int> _selecteditems;

        public DishListViewSelector()
        {
            InitializeComponent();
            _allitems = new Collection<DishInfo>();
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
                ErrorHandler.ShowMessageBox("DishListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("DishListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("DishListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("DishListViewSelector", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<DishInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (DishInfo dish in _allitems)
                    {
                        string[] subItem = new string[3];
                        subItem[0] = dish.DishId.ToString();
                        subItem[1] = dish.Title;
                        subItem[2] = dish.Price.ToString();
                        if (_selecteditems.Contains(dish.DishId))
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
                Collection<int> dishes = new Collection<int>();
                foreach (ListViewItem item in lstSelectedItems.Items)
                {
                    if (item != null)
                    {
                        dishes.Add(DataConvert.GetInt32(item.SubItems[0].Text));
                    }
                }
                return dishes;
            }
            set 
            {
                _selecteditems = value;
            }
        }
        #endregion

        public void SetSelectedTitle(string title)
        {
            lblForbiddenFood.Text = title;
        } 
    }
}
