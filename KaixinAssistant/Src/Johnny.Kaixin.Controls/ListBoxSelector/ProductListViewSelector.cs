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
    public partial class ProductListViewSelector : UserControl
    {
        private Collection<ProductInfo> _allitems;
        private Collection<ProductInfo> _selecteditems;

        public ProductListViewSelector()
        {
            InitializeComponent();
            _allitems = new Collection<ProductInfo>();
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
                ErrorHandler.ShowMessageBox("ProductListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("ProductListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("ProductListViewSelector", ex);
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
                ErrorHandler.ShowMessageBox("ProductListViewSelector", ex);
            }
        }
        #endregion

        #region Properties
        public Collection<ProductInfo> AllItems
        {
            get { return _allitems; }
            set
            {
                _allitems = value;
                if (_allitems != null)
                {
                    foreach (ProductInfo product in _allitems)
                    {
                        string[] subItem = new string[4];
                        subItem[0] = product.Aid.ToString();
                        subItem[1] = product.Name;
                        subItem[2] = product.Type.ToString();
                        subItem[3] = product.Price.ToString();
                        if (IsContained(product.Aid, product.Type))
                            lstSelectedItems.Items.Add(new ListViewItem(subItem));
                        else
                            lstAllItems.Items.Add(new ListViewItem(subItem));
                    }
                }
            }
        }

        public Collection<ProductInfo> SelectedItems
        {
            get
            {
                Collection<ProductInfo> products = new Collection<ProductInfo>();
                foreach (ListViewItem item in lstSelectedItems.Items)
                {
                    if (item != null)
                    {
                        ProductInfo product = new ProductInfo();
                        product.Aid = DataConvert.GetInt32(item.SubItems[0].Text);
                        product.Name = DataConvert.GetString(item.SubItems[1].Text);
                        product.Type = DataConvert.GetInt32(item.SubItems[2].Text);
                        product.Price = DataConvert.GetInt32(item.SubItems[3].Text);
                        products.Add(product);
                    }
                }
                return products;
            }
            set 
            {
                _selecteditems = value;
            }
        }
        #endregion

        public void SetSelectedTitle(string title)
        {
            lblForbiddenProducts.Text = title;
        }

        private bool IsContained(int aid, int type)
        {
            foreach (ProductInfo product in _selecteditems)
            {
                if (product.Aid == aid && product.Type == type)
                    return true;
            }
            return false;
        }
    }
}
