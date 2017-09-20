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
    public partial class DlgPresentFoodSelection : Form
    {
        private Collection<DishInfo> _dishes;
        private Collection<int> _selecteddishes;
        private string _caption;
        private string _selectedtitle;

        public DlgPresentFoodSelection()
        {
            InitializeComponent();
        }

        private void DlgStealFruitSelection_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = _caption;
                lstViewDishes.SetSelectedTitle(_selectedtitle);
                //≤ÀÎ»
                _dishes = ConfigCtrl.GetDishesInMenu();
                lstViewDishes.Clear();
                lstViewDishes.SelectedItems = _selecteddishes;
                lstViewDishes.AllItems = _dishes;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgStealFruitSelection.DlgStealFruitSelection_Load", ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _selecteddishes = new Collection<int>();
                foreach (int dishid in lstViewDishes.SelectedItems)
                {
                    _selecteddishes.Add(dishid);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgStealFruitSelection.btnOk_Click", ex);
            }
        }

        public Collection<DishInfo> AllDishes
        {
            get { return _dishes; }
            set { _dishes = value; }
        }

        public Collection<int> SelectedDishes
        {
            get { return _selecteddishes; }
            set { _selecteddishes = value; }
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