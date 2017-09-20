using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class DlgSellProductSelection : Form
    {
        private Collection<ProductInfo> _products;
        private Collection<ProductInfo> _selectedproducts;
        private string _caption;
        private string _selectedtitle;

        public DlgSellProductSelection()
        {
            InitializeComponent();
        }

        private void DlgSellProductSelection_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = _caption;
                lstViewProducts.SetSelectedTitle(_selectedtitle);
                //农副产品
                //_products = ConfigCtrl.get();
                lstViewProducts.Clear();
                lstViewProducts.SelectedItems = _selectedproducts;
                lstViewProducts.AllItems = _products;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSellProductSelection.DlgSellProductSelection_Load", ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedproducts = new Collection<ProductInfo>();
                foreach (ProductInfo product in lstViewProducts.SelectedItems)
                {
                    _selectedproducts.Add(product);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSellProductSelection.btnOk_Click", ex);
            }
        }

        public Collection<ProductInfo> AllProducts
        {
            get { return _products; }
            set { _products = value; }
        }

        public Collection<ProductInfo> SelectedProducts
        {
            get { return _selectedproducts; }
            set { _selectedproducts = value; }
        }

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        public string SelectedTitle
        {
            get { return _selectedtitle; }
            set { _selectedtitle = value; }
        }   
    }
}