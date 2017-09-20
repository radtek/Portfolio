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
    public partial class DlgBuyAssetsSelection : Form
    {
        private Collection<AssetInfo> _assets;
        private Collection<int> _selectedassets;
        private string _caption;
        private string _selectedtitle;

        public DlgBuyAssetsSelection()
        {
            InitializeComponent();
        }

        private void DlgBuyAssetsSelection_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = _caption;
                lstViewAssets.SetSelectedTitle(_selectedtitle);
                //×Ê²ú
                _assets = ConfigCtrl.GetAssetsInShop();
                lstViewAssets.Clear();
                lstViewAssets.SelectedItems = _selectedassets;
                lstViewAssets.AllItems = _assets;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgBuyAssetsSelection.DlgBuyAssetsSelection_Load", ex);
            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedassets = new Collection<int>();
                foreach (int assetid in lstViewAssets.SelectedItems)
                {
                    _selectedassets.Add(assetid);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgBuyAssetsSelection.btnOk_Click", ex);
            }
        }

        public Collection<AssetInfo> AllAssets
        {
            get { return _assets; }
            set { _assets = value; }
        }

        public Collection<int> SelectedAssets
        {
            get { return _selectedassets; }
            set { _selectedassets = value; }
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