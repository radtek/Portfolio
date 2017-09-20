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
    public partial class CarListViewSelector : UserControl
    {
        private Collection<NewCarInfo> _allitems;

        public CarListViewSelector()
        {
            InitializeComponent();
            _allitems = new Collection<NewCarInfo>();
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
                ErrorHandler.ShowMessageBox("CarListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("CarListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("CarListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("CarListViewSelector", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<NewCarInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (NewCarInfo car in _allitems)
                    {
                        string[] subItem = new string[3];
                        subItem[0] = car.CarId.ToString();
                        subItem[1] = car.CarName;
                        subItem[2] = car.CarPrice.ToString();
                        if (car.CarId == 5 && car.CarName == "Ã°ÅÆ¾¯³µ")
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
                Collection<int> cars = new Collection<int>();
                foreach (ListViewItem item in lstSelectedItems.Items)
                {
                    if (item != null)
                    {                        
                        cars.Add(DataConvert.GetInt32(item.SubItems[0].Text));
                    }
                }
                return cars;
            }
        }
        #endregion

    }
}
