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
    public partial class DlgStealFruitSelection : Form
    {
        private Collection<FruitInfo> _fruits;
        private Collection<int> _selectedfruits;
        private string _caption;
        private string _selectedtitle;

        public DlgStealFruitSelection()
        {
            InitializeComponent();
        }

        private void DlgStealFruitSelection_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = _caption;
                lstViewFruits.SetSelectedTitle(_selectedtitle);
                _fruits = ConfigCtrl.GetFruits();
                lstViewFruits.Clear();
                lstViewFruits.SelectedItems = _selectedfruits;
                lstViewFruits.AllItems = _fruits;
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
                _selectedfruits = new Collection<int>();
                foreach (int fruitid in lstViewFruits.SelectedItems)
                {
                    _selectedfruits.Add(fruitid);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgStealFruitSelection.btnOk_Click", ex);
            }
        }

        public Collection<FruitInfo> AllFruits
        {
            get { return _fruits; }
            set { _fruits = value; }
        }

        public Collection<int> SelectedFruits
        {
            get { return _selectedfruits; }
            set { _selectedfruits = value; }
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