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
    public partial class DlgPresentFishSelection : Form
    {
        private Collection<FishMaturedInfo> _fishes;
        private Collection<int> _selectedfishes;
        private string _caption;
        private string _selectedtitle;

        public DlgPresentFishSelection()
        {
            InitializeComponent();
        }

        private void DlgPresentFishSelection_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = _caption;
                lstViewFishes.SetSelectedTitle(_selectedtitle);
                //гу
                _fishes = ConfigCtrl.GetFishMaturedInMarket();
                lstViewFishes.Clear();
                lstViewFishes.SelectedItems = _selectedfishes;
                lstViewFishes.AllItems = _fishes;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgPresentFishSelection.DlgPresentFishSelection", ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedfishes = new Collection<int>();
                foreach (int fishid in lstViewFishes.SelectedItems)
                {
                    _selectedfishes.Add(fishid);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgPresentFishSelection.btnOk_Click", ex);
            }
        }

        public Collection<FishMaturedInfo> AllFishes
        {
            get { return _fishes; }
            set { _fishes = value; }
        }

        public Collection<int> SelectedFishes
        {
            get { return _selectedfishes; }
            set { _selectedfishes = value; }
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