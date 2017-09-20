using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;

using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmUpdateData : FrmToolBase
    {        
        private ToolUpdateData _toolupdatedata;
        private Collection<SeedInfo> _shopseeds;
        private GameRich _gRich;
        //public delegate void MessageChangedEventHandler(string caption, string key, string message);
        //public event MessageChangedEventHandler messageChanged;

        #region Ctor
        public FrmUpdateData()
        {
            InitializeComponent();
            _toolupdatedata = new ToolUpdateData();
            _toolupdatedata.MessageChanged += new KaixinBase.MessageChangedEventHandler(_toolupdatedata_MessageChanged);
            _toolupdatedata.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolupdatedata_ValidateCodeNeeded);
            _toolupdatedata.AllCarsInMarketFetched += new ToolUpdateData.AllCarsInMarketFetchedEventHandler(_toolupdatedata_AllCarsInMarketFetched);
            _toolupdatedata.SeedsInShopFetched += new ToolUpdateData.SeedsInShopFetchedEventHandler(_toolupdatedata_SeedsInShopFetched);
            _toolupdatedata.CalvesInShopFetched += new ToolUpdateData.CalvesInShopFetchedEventHandler(_toolupdatedata_CalvesInShopFetched);
            _toolupdatedata.FishFrysFetched += new ToolUpdateData.FishFrysInShopFetchedEventHandler(_toolupdatedata_FishFrysFetched);
            _toolupdatedata.FishTacklesFetched += new ToolUpdateData.FishTacklesInShopFetchedEventHandler(_toolupdatedata_FishTacklesFetched);
            _toolupdatedata.DishesFetched += new ToolUpdateData.DishesInMenuFetchedEventHandler(_toolupdatedata_DishesFetched);
            _toolupdatedata.TransactionDishesFetched += new ToolUpdateData.TransactionDishesFetchedEventHandler(_toolupdatedata_TransactionDishesFetched);
            _gRich = new GameRich();
        }

        void _toolupdatedata_MessageChanged(string caption, string key, string message)
        {
            SetMessageByParam(caption, key, message);
        }

        void _toolupdatedata_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_toolupdatedata_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "更新数据";
                if (picCode.ShowDialog() == DialogResult.OK)
                    //_toolupdatedata.ValidationCode = picCode.ValidationCode;
                    _toolupdatedata.UpdateValidationCode(picCode.ValidationCode);
                else
                    //_toolupdatedata.RetryLogin = false;
                    //_toolupdatedata.UpdateRetryLogin(false);
                    _toolupdatedata.UpdateValidationCode(null);
            }
        }

        void _toolupdatedata_AllCarsInMarketFetched(Collection<NewCarInfo> carsinmarket)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolUpdateData.AllCarsInMarketFetchedEventHandler(_toolupdatedata_AllCarsInMarketFetched), new object[] { carsinmarket });
            }
            else
            {
                if (carsinmarket != null && carsinmarket.Count > 0)
                {
                    BuildLstCars(carsinmarket);

                    if (!ConfigCtrl.SetCarsInMarket(carsinmarket))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                SetControlStatus(true);
            }

        }

        void _toolupdatedata_SeedsInShopFetched(Collection<SeedInfo> seeds)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolUpdateData.SeedsInShopFetchedEventHandler(_toolupdatedata_SeedsInShopFetched), new object[] { seeds });
            }
            else
            {
                if (seeds != null && seeds.Count > 0)
                {
                    if (!ConfigCtrl.SetSeedsInShop(seeds))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BuildGridViewSeeds(AddColumnsToTable(ConfigCtrl.GetSeedsToTable()),"种子列表刷新失败！");
                }
                
                SetControlStatus(true);
            }
        }

        void _toolupdatedata_CalvesInShopFetched(Collection<CalfInfo> calves)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolUpdateData.CalvesInShopFetchedEventHandler(_toolupdatedata_CalvesInShopFetched), new object[] { calves });
            }
            else
            {
                if (calves != null && calves.Count > 0)
                {
                    BuildLstCalves(calves);

                    if (!ConfigCtrl.SetCalvesInShop(calves))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                SetControlStatus(true);
            }
        }


        void _toolupdatedata_FishFrysFetched(Collection<FishFryInfo> fishfrys)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolUpdateData.FishFrysInShopFetchedEventHandler(_toolupdatedata_FishFrysFetched), new object[] { fishfrys });
            }
            else
            {
                if (fishfrys != null && fishfrys.Count > 0)
                {
                    BuildLstFishFrys(fishfrys);

                    if (!ConfigCtrl.SetFishFrysInShop(fishfrys))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                SetControlStatus(true);
            }
        }

        void _toolupdatedata_FishTacklesFetched(Collection<FishTackleInfo> fishtackles)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolUpdateData.FishTacklesInShopFetchedEventHandler(_toolupdatedata_FishTacklesFetched), new object[] { fishtackles });
            }
            else
            {
                if (fishtackles != null && fishtackles.Count > 0)
                {
                    BuildLstFishTackles(fishtackles);

                    if (!ConfigCtrl.SetFishTacklesInShop(fishtackles))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                SetControlStatus(true);
            }
        }

        void _toolupdatedata_DishesFetched(Collection<DishInfo> dishes)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolUpdateData.DishesInMenuFetchedEventHandler(_toolupdatedata_DishesFetched), new object[] { dishes });
            }
            else
            {
                if (dishes != null && dishes.Count > 0)
                {
                    BuildLstDishes(dishes);

                    if (!ConfigCtrl.SetDishesInMenu(dishes))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                SetControlStatus(true);
            }
        }

        void _toolupdatedata_TransactionDishesFetched(Collection<DishInfo> transactiondishes)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToolUpdateData.TransactionDishesFetchedEventHandler(_toolupdatedata_TransactionDishesFetched), new object[] { transactiondishes });
            }
            else
            {
                if (transactiondishes != null && transactiondishes.Count > 0)
                {
                    if (!ConfigCtrl.SetTransactionDishes(transactiondishes, false))
                    {
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BuildDgvTransactionDishes(AddColumnsToTransactionDishes(ConfigCtrl.GetTransactionDishesToTable()), "菜肴交易价格表读取失败！");
                }

                SetControlStatus(true);
            }
        }
        #endregion

        #region FrmUpdateData_Load
        private void FrmUpdateData_Load(object sender, EventArgs e)
        {
            try
            {
                BuildCmbGroup();
                BuildCmbAccount(cmbGroup.Text);
                BuildLstCars(ConfigCtrl.GetCarsInMarket());
                BuildLstMatches(ConfigCtrl.GetMatches());
                BuildLstFruits(ConfigCtrl.GetFruits());
                BuildLstCalves(ConfigCtrl.GetCalvesInShop());
                BuildLstAnimalProducts(ConfigCtrl.GetAnimalProducts());
                BuildLstFishFrys(ConfigCtrl.GetFishFrysInShop());
                BuildLstFishTackles(ConfigCtrl.GetFishTacklesInShop());
                BuildLstFishMatured(ConfigCtrl.GetFishMaturedInMarket());
                BuildLstAssets(ConfigCtrl.GetAssetsInShop());
                BuildLstDishes(ConfigCtrl.GetDishesInMenu());
                BuildGridViewSeeds(AddColumnsToTable(ConfigCtrl.GetSeedsToTable()), "种子列表读取失败！");
                BuildRankGridViewSeeds(AddRankColumnsToTable(ConfigCtrl.GetRankSeedsToTable()), "等级种子列表读取失败！");
                BuildDgvAssetRecommends(AddColumnsToAssetRecommends(ConfigCtrl.GetAssetsToTable()), "推荐买卖率表读取失败！");
                BuildDgvAdvancedPurchase(AddColumnsToAdvancedPurchase(ConfigCtrl.GetAdvancedPurchaseToTable()), "高级购买配置表读取失败！");
                BuildDgvTransactionDishes(AddColumnsToTransactionDishes(ConfigCtrl.GetTransactionDishesToTable()), "菜肴交易价格表读取失败！");
                _shopseeds = ConfigCtrl.GetSeedsInShop();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData", ex);
            }
        }

        private void BuildCmbGroup()
        {
            string[] groups = ConfigCtrl.GetGroups();
            if (groups != null)
            {
                foreach (string group in groups)
                {
                    cmbGroup.Items.Add(group);
                }
                if (cmbGroup.Items.Count > 0)
                    cmbGroup.SelectedIndex = 0;
            }
        }

        private void BuildCmbAccount(string group)
        {
            //所有的账号
            Collection<AccountInfo> accounts = ConfigCtrl.GetAccounts(group);
            if (accounts != null)
            {
                cmbAccount.Items.Clear();
                foreach (AccountInfo account in accounts)
                {
                    cmbAccount.Items.Add(account);
                }
                if (cmbAccount.Items.Count > 0)
                    cmbAccount.SelectedIndex = 0;
            }
        }
        #endregion

        #region AddColumnsToTable
        private DataTable AddColumnsToTable(DataTable dt)
        {
            if (dt != null)
            {
                dt.Columns[0].ColumnName = "种子ID";
                dt.Columns[1].ColumnName = "种子名称";
                dt.Columns[2].ColumnName = "种子价格";
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = true;
            }
            return dt;
        }
        #endregion

        #region AddRankColumnsToTable
        private DataTable AddRankColumnsToTable(DataTable dt)
        {
            if (dt != null)
            {
                dt.Columns[0].ColumnName = "等级";
                dt.Columns[1].ColumnName = "种子ID";
                dt.Columns[2].ColumnName = "种子名称";
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = false;
                dt.Columns[2].ReadOnly = false;   
            }
            return dt;
        }
        #endregion

        #region AddColumnsToAssetRecommends
        private DataTable AddColumnsToAssetRecommends(DataTable dt)
        {
            if (dt != null)
            {
                dt.Columns[0].ColumnName = "资产ID";
                dt.Columns[1].ColumnName = "资产名";
                dt.Columns[2].ColumnName = "基准价";
                dt.Columns[3].ColumnName = "买入率(%)";
                dt.Columns[4].ColumnName = "买入价";
                dt.Columns[5].ColumnName = "卖出率(%)";
                dt.Columns[6].ColumnName = "卖出价";
                dt.Columns[7].ColumnName = "价格浮动区间";
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = false;
                dt.Columns[3].ReadOnly = false;
                dt.Columns[4].ReadOnly = false;
                dt.Columns[5].ReadOnly = false;
                dt.Columns[6].ReadOnly = false;
                dt.Columns[7].ReadOnly = false;
            }
            return dt;
        }
        #endregion

        #region AddColumnsToAdvancedPurchase
        private DataTable AddColumnsToAdvancedPurchase(DataTable dt)
        {
            if (dt != null)
            {
                dt.Columns[0].ColumnName = "总资产(元)";
                dt.Columns[1].ColumnName = "资产单价(元)";
                dt.Columns[2].ColumnName = "最小购买数量";
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = false;
            }
            return dt;
        }
        #endregion

        #region AddColumnsToTable
        private DataTable AddColumnsToTransactionDishes(DataTable dt)
        {
            if (dt != null)
            {
                dt.Columns[0].ColumnName = "菜肴ID";
                dt.Columns[1].ColumnName = "名称";
                dt.Columns[2].ColumnName = "最高价";
                dt.Columns[3].ColumnName = "最低价";
                dt.Columns[4].ColumnName = "出售价";
                dt.Columns[5].ColumnName = "收购价";
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = true;
                dt.Columns[3].ReadOnly = true;
                dt.Columns[4].ReadOnly = false;
                dt.Columns[5].ReadOnly = false;
            }
            return dt;
        }
        #endregion

        #region BuildLstCars
        private void BuildLstCars(Collection<NewCarInfo> carsinmarket)
        {
            if (carsinmarket != null)
            {
                lstCars.Items.Clear();
                foreach (NewCarInfo car in carsinmarket)
                {
                    string[] subItem = new string[3];
                    subItem[0] = car.CarId.ToString();
                    subItem[1] = car.CarName;
                    subItem[2] = car.CarPrice.ToString();
                    lstCars.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstMatches
        private void BuildLstMatches(Collection<MatchInfo> matches)
        {
            if (matches != null)
            {
                lstMatches.Items.Clear();
                foreach (MatchInfo match in matches)
                {
                    string[] subItem = new string[4];
                    subItem[0] = match.MatchId.ToString();
                    subItem[1] = match.Name;
                    subItem[2] = match.ShortName;
                    subItem[3] = match.Distance.ToString() + "km";
                    lstMatches.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstFruits
        private void BuildLstFruits(Collection<FruitInfo> fruits)
        {
            if (fruits != null)
            {
                lstFruits.Items.Clear();
                foreach (FruitInfo fruit in fruits)
                {
                    string[] subItem = new string[3];
                    subItem[0] = fruit.FruitId.ToString();
                    subItem[1] = fruit.Name;
                    subItem[2] = fruit.SellPrice.ToString();
                    lstFruits.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstCalves
        private void BuildLstCalves(Collection<CalfInfo> calves)
        {
            if (calves != null)
            {
                lstCalves.Items.Clear();
                foreach (CalfInfo calf in calves)
                {
                    string[] subItem = new string[3];
                    subItem[0] = calf.AId.ToString();
                    subItem[1] = calf.Name;
                    subItem[2] = calf.Price.ToString();
                    lstCalves.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstAnimalProducts
        private void BuildLstAnimalProducts(Collection<ProductInfo> products)
        {
            if (products != null)
            {
                lstAnimalProducts.Items.Clear();
                foreach (ProductInfo product in products)
                {
                    string[] subItem = new string[4];
                    subItem[0] = product.Aid.ToString();
                    subItem[1] = product.Name;
                    subItem[2] = product.Type.ToString();
                    subItem[3] = product.Price.ToString();
                    lstAnimalProducts.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstFishFrys
        private void BuildLstFishFrys(Collection<FishFryInfo> fishfrys)
        {
            if (fishfrys != null)
            {
                lstFishFrys.Items.Clear();
                foreach (FishFryInfo fishfry in fishfrys)
                {
                    string[] subItem = new string[4];
                    subItem[0] = fishfry.FId.ToString();
                    subItem[1] = fishfry.Name;
                    subItem[2] = fishfry.Price.ToString();
                    subItem[3] = fishfry.Rank.ToString();
                    lstFishFrys.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstFishTackles
        private void BuildLstFishTackles(Collection<FishTackleInfo> fishtackles)
        {
            if (fishtackles != null)
            {
                lstFishTackles.Items.Clear();
                foreach (FishTackleInfo fishtackle in fishtackles)
                {
                    string[] subItem = new string[4];
                    subItem[0] = fishtackle.TId.ToString();
                    subItem[1] = fishtackle.Name;
                    subItem[2] = fishtackle.Price.ToString();
                    subItem[3] = fishtackle.Rank.ToString();
                    lstFishTackles.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstFishTackles
        private void BuildLstFishMatured(Collection<FishMaturedInfo> fishmatureds)
        {
            if (fishmatureds != null)
            {
                lstFishMatured.Items.Clear();
                foreach (FishMaturedInfo fishmatured in fishmatureds)
                {
                    string[] subItem = new string[4];
                    subItem[0] = fishmatured.FId.ToString();
                    subItem[1] = fishmatured.Name;
                    subItem[2] = fishmatured.Price.ToString();
                    subItem[3] = fishmatured.MaxWeight.ToString();
                    lstFishMatured.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildLstAssets
        private void BuildLstAssets(Collection<AssetInfo> assets)
        {
            if (assets != null)
            {
                lstAssets.Items.Clear();
                foreach (AssetInfo asset in assets)
                {
                    string[] subItem = new string[4];
                    subItem[0] = asset.IId.ToString();
                    subItem[1] = asset.Name;
                    subItem[2] = FormatPrice(asset.StandardPrice);
                    subItem[3] = asset.Description.ToString();
                    lstAssets.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion
        
        #region BuildGridViewSeeds
        private void BuildGridViewSeeds(DataTable dt, string message)
        {
            dgvShopSeeds.Columns.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BindingSource dbs = new BindingSource();
            dbs.DataSource = dt;

            this.dgvShopSeeds.DataSource = dbs;
            this.dgvShopSeeds.Columns[0].Width = 70;
            this.dgvShopSeeds.Columns[1].Width = 90;
            this.dgvShopSeeds.Columns[2].Width = 80;
        }
        #endregion

        #region BuildRankGridViewSeeds
        private void BuildRankGridViewSeeds(DataTable dt, string message)
        {
            dgvRankSeeds.Columns.Clear();
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BindingSource dbs = new BindingSource();
            dbs.DataSource = dt;

            this.dgvRankSeeds.DataSource = dbs;
            this.dgvRankSeeds.Columns[0].Width = 55;
            this.dgvRankSeeds.Columns[1].Width = 70;
            this.dgvRankSeeds.Columns[2].Width = 80;
        }
        #endregion

        #region BuildDgvAssetRecommends
        private void BuildDgvAssetRecommends(DataTable dt, string message)
        {
            dgvAssetRecommends.Columns.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BindingSource dbs = new BindingSource();
            dbs.DataSource = dt;

            this.dgvAssetRecommends.DataSource = dbs;
            this.dgvAssetRecommends.Columns[0].Width = 65;
            this.dgvAssetRecommends.Columns[1].Width = 95;
            this.dgvAssetRecommends.Columns[2].Width = 75;
            this.dgvAssetRecommends.Columns[3].Width = 82;
            this.dgvAssetRecommends.Columns[4].Width = 75;
            this.dgvAssetRecommends.Columns[5].Width = 82;
            this.dgvAssetRecommends.Columns[6].Width = 75;
            this.dgvAssetRecommends.Columns[7].Width = 780;
        }
        #endregion

        #region BuildDgvAdvancedPurchase
        private void BuildDgvAdvancedPurchase(DataTable dt, string message)
        {
            dgvAdvancedPurchase.Columns.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BindingSource dbs = new BindingSource();
            dbs.DataSource = dt;

            this.dgvAdvancedPurchase.DataSource = dbs;
            this.dgvAdvancedPurchase.Columns[0].Width = 100;
            this.dgvAdvancedPurchase.Columns[1].Width = 100;
            this.dgvAdvancedPurchase.Columns[2].Width = 100;
        }
        #endregion

        #region BuildLstDishes
        private void BuildLstDishes(Collection<DishInfo> dishes)
        {
            if (dishes != null)
            {
                lstDishes.Items.Clear();
                foreach (DishInfo dish in dishes)
                {
                    string[] subItem = new string[4];
                    subItem[0] = dish.DishId.ToString();
                    subItem[1] = dish.Title;
                    subItem[2] = dish.Price.ToString();
                    subItem[3] = dish.Rank.ToString();
                    lstDishes.Items.Add(new ListViewItem(subItem));
                }
            }
        }
        #endregion

        #region BuildDgvTransactionDishes
        private void BuildDgvTransactionDishes(DataTable dt, string message)
        {
            dgvTransactionDishes.Columns.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(message, MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BindingSource dbs = new BindingSource();
            dbs.DataSource = dt;

            this.dgvTransactionDishes.DataSource = dbs;
            this.dgvTransactionDishes.Columns[0].Width = 55;
            this.dgvTransactionDishes.Columns[1].Width = 75;
            this.dgvTransactionDishes.Columns[2].Width = 60;
            this.dgvTransactionDishes.Columns[3].Width = 60;
            this.dgvTransactionDishes.Columns[4].Width = 60;
            this.dgvTransactionDishes.Columns[5].Width = 60;
        }
        #endregion
        
        #region FrmUpdateData_FormClosing
        private void FrmUpdateData_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_toolupdatedata != null)
                    _toolupdatedata.StopThread();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData", ex);
            }
        }
        #endregion

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroup.Items.Count > 0 && cmbGroup.Text != string.Empty)
            {
                BuildCmbAccount(cmbGroup.Text);
            }
        }
        #endregion

        #region SetControlStatus
        private void SetControlStatus(bool enabled)
        {
            cmbGroup.Enabled = enabled;
            btnRefreshCars.Enabled = enabled;
            btnRefreshSeeds.Enabled = enabled;
            btnRefreshCalves.Enabled = enabled;

            btnReloadFishFrys.Enabled = enabled;
            btnRefreshFishFrys.Enabled = enabled;

            btnReloadFishTackles.Enabled = enabled;
            btnRefreshFishTackles.Enabled = enabled;

            btnReload.Enabled = enabled;

            btnReloadDishes.Enabled = enabled;
            btnRefreshDishes.Enabled = enabled;

            btnReloadTransactionDishes.Enabled = enabled;
            btnRefreshTransactionDishes.Enabled = enabled;
        }
        #endregion

        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_toolupdatedata != null)
                    _toolupdatedata.StopThread();
                SetControlStatus(true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData", ex);
            }
        }
        #endregion

        #region Park
        private void btnRefreshCars_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckAll())
                    _toolupdatedata.RefreshCars();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnRefreshCars_Click", ex);
            }
        }
        
        private void btnReloadMatches_Click(object sender, EventArgs e)
        {
            try
            {
                BuildLstMatches(ConfigCtrl.GetMatches());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadMatches_Click", ex);
            }
        }
        #endregion

        #region btnRefreshCalves_Click
        private void btnRefreshCalves_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckAll())
                    _toolupdatedata.RefreshCalves();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnRefreshCalves_Click", ex);
            }
        }
        #endregion

        #region CheckAll
        private bool CheckAll()
        {
            if (cmbAccount.Items.Count <= 0 || cmbAccount.SelectedIndex < 0)
            {
                MessageBox.Show("请选择帐号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAccount.Select();
                return false;
            }

            AccountInfo account = cmbAccount.Items[cmbAccount.SelectedIndex] as AccountInfo;
            if (account == null)
                return false;

            _toolupdatedata._account = account;

            SetControlStatus(false);
            return true;
        }
        #endregion

        #region Garden
        private void btnRefreshSeeds_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckAll())
                    _toolupdatedata.RefreshSeeds();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData", ex);
            }
        }               

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {                
                BuildGridViewSeeds(AddColumnsToTable(ConfigCtrl.GetSeedsToTable()), "种子列表读取失败！");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData", ex);
            }
        }

        private void btnReloadRankSeeds_Click(object sender, EventArgs e)
        {
            try
            {
                BuildRankGridViewSeeds(AddRankColumnsToTable(ConfigCtrl.GetRankSeedsToTable()), "等级种子列表读取失败！");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData", ex);
            }
        }

        private void btnSaveRankSeeds_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = 0; ix < dgvRankSeeds.Rows.Count; ix++)
                {
                    if (!DataValidation.IsNaturalNumber(dgvRankSeeds.Rows[ix].Cells[1].Value))
                    {
                        MessageBox.Show("种子ID必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvRankSeeds.ClearSelection();
                        dgvRankSeeds.Rows[ix].Cells[1].Selected = true;
                        return;
                    }
                    else
                    {
                        if (!IsValidSeedId(DataConvert.GetInt32(dgvRankSeeds.Rows[ix].Cells[1].Value)))
                        {
                            MessageBox.Show("种子ID无效！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvRankSeeds.ClearSelection();
                            dgvRankSeeds.Rows[ix].Cells[1].Selected = true;
                            return;
                        }
                    }
                }
               
                BindingSource bs = dgvRankSeeds.DataSource as BindingSource;
                if (bs != null)
                {
                    DataTable dt = bs.DataSource as DataTable;
                    if (dt != null)
                    {
                        if (ConfigCtrl.SetRankSeeds(dt))
                            MessageBox.Show("保存成功！");
                        else
                            MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnSaveRankSeeds_Click", ex);
            }
        }

        private void btnReloadFruits_Click(object sender, EventArgs e)
        {
            try
            {
                BuildLstFruits(ConfigCtrl.GetFruits());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadFruits_Click", ex);
            }
        }

        #endregion
        
        #region dgvRankSeeds_CellEndEdit
        private void dgvRankSeeds_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 && DataValidation.IsNaturalNumber(dgvRankSeeds.Rows[e.RowIndex].Cells[1].Value))
                {
                    dgvRankSeeds.Rows[e.RowIndex].Cells[2].Value = GetSeedNameById(DataConvert.GetInt32(dgvRankSeeds.Rows[e.RowIndex].Cells[1].Value));
                }
                if (e.ColumnIndex == 2)
                {
                    dgvRankSeeds.Rows[e.RowIndex].Cells[1].Value = GetSeedIdByName(DataConvert.GetString(dgvRankSeeds.Rows[e.RowIndex].Cells[2].Value));
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.dgvRankSeeds_CellLeave", ex);
            }
        }
        #endregion

        #region Fish
        private void btnReloadFishFrys_Click(object sender, EventArgs e)
        {
            try
            {
                BuildLstFishFrys(ConfigCtrl.GetFishFrysInShop());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadFishFrys_Click", ex);
            }
        }

        private void btnRefreshFishFrys_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckAll())
                    _toolupdatedata.RefreshFishFrys();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnRefreshFishFrys_Click", ex);
            }
        }

        private void btnReloadFishTackles_Click(object sender, EventArgs e)
        {
            try
            {
                BuildLstFishTackles(ConfigCtrl.GetFishTacklesInShop());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadFishTackles_Click", ex);
            }
        }

        private void btnRefreshFishTackles_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckAll())
                    _toolupdatedata.RefreshFishTackles();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnRefreshFishTackles_Click", ex);
            }
        }

        private void btnReloadFishMatured_Click(object sender, EventArgs e)
        {
            try
            {
                BuildLstFishMatured(ConfigCtrl.GetFishMaturedInMarket());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadFishMatured_Click", ex);
            }            
        }
        #endregion

        #region Rich

        private void btnReloadAssets_Click(object sender, EventArgs e)
        {
            try
            {
                BuildLstAssets(ConfigCtrl.GetAssetsInShop());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadAssets_Click", ex);
            }
        }

        private void btnReloadAssetRecommends_Click(object sender, EventArgs e)
        {
            try
            {
                BuildDgvAssetRecommends(AddColumnsToAssetRecommends(ConfigCtrl.GetAssetsToTable()), "推荐买卖率表读取失败！");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadAssetRecommends_Click", ex);
            }
        }

        private void btnSaveAssetRecommends_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = 0; ix < dgvAssetRecommends.Rows.Count; ix++)
                {
                    if (!DataValidation.IsNaturalNumber(dgvAssetRecommends.Rows[ix].Cells[0].Value))
                    {
                        MessageBox.Show("资产ID必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvAssetRecommends.ClearSelection();
                        dgvAssetRecommends.Rows[ix].Cells[0].Selected = true;
                        return;
                    }

                    if (!DataValidation.IsInt64(dgvAssetRecommends.Rows[ix].Cells[2].Value))
                    {
                        MessageBox.Show("基准价必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvAssetRecommends.ClearSelection();
                        dgvAssetRecommends.Rows[ix].Cells[2].Selected = true;
                        return;
                    }

                    if (!DataValidation.IsPercentNumber(dgvAssetRecommends.Rows[ix].Cells[3].Value))
                    {
                        MessageBox.Show("买入率必须为-100~100之间！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvAssetRecommends.ClearSelection();
                        dgvAssetRecommends.Rows[ix].Cells[3].Selected = true;
                        return;
                    }

                    if (!DataValidation.IsPercentNumber(dgvAssetRecommends.Rows[ix].Cells[5].Value))
                    {
                        MessageBox.Show("卖出率必须为-100~100之间！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvAssetRecommends.ClearSelection();
                        dgvAssetRecommends.Rows[ix].Cells[5].Selected = true;
                        return;
                    }

                    decimal buyprice = DataConvert.GetInt64(dgvAssetRecommends.Rows[ix].Cells[2].Value) * (100 - DataConvert.GetDecimal(dgvAssetRecommends.Rows[ix].Cells[3].Value)) / 100;
                    dgvAssetRecommends.Rows[ix].Cells[4].Value = Math.Round(buyprice, 0);

                    decimal sellprice = DataConvert.GetInt64(dgvAssetRecommends.Rows[ix].Cells[2].Value) * (100 + DataConvert.GetDecimal(dgvAssetRecommends.Rows[ix].Cells[5].Value)) / 100;
                    dgvAssetRecommends.Rows[ix].Cells[6].Value = Math.Round(sellprice, 0);
                }

                BindingSource bs = dgvAssetRecommends.DataSource as BindingSource;
                if (bs != null)
                {
                    DataTable dt = bs.DataSource as DataTable;
                    if (dt != null)
                    {
                        if (ConfigCtrl.SetAssetsFromTable(dt))
                            MessageBox.Show("保存成功！");
                        else
                            MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnSaveAssetRecommends_Click", ex);
            }
        }

        private void dgvAssetRecommends_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3 && DataValidation.IsPercentNumber(dgvAssetRecommends.Rows[e.RowIndex].Cells[3].Value))
                {
                    decimal buyprice = DataConvert.GetInt64(dgvAssetRecommends.Rows[e.RowIndex].Cells[2].Value) * (100 - DataConvert.GetDecimal(dgvAssetRecommends.Rows[e.RowIndex].Cells[3].Value)) / 100;
                    dgvAssetRecommends.Rows[e.RowIndex].Cells[4].Value = Math.Round(buyprice, 0);
                }
                if (e.ColumnIndex == 5 && DataValidation.IsPercentNumber(dgvAssetRecommends.Rows[e.RowIndex].Cells[5].Value))
                {
                    decimal sellprice = DataConvert.GetInt64(dgvAssetRecommends.Rows[e.RowIndex].Cells[2].Value) * (100 + DataConvert.GetDecimal(dgvAssetRecommends.Rows[e.RowIndex].Cells[5].Value)) / 100;
                    dgvAssetRecommends.Rows[e.RowIndex].Cells[6].Value = Math.Round(sellprice, 0);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.dgvAssetRecommends_CellEndEdit", ex);
            }
        }

        private void btnReloadAdvancedPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                BuildDgvAdvancedPurchase(AddColumnsToAdvancedPurchase(ConfigCtrl.GetAdvancedPurchaseToTable()), "高级购买配置表读取失败！");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadAdvancedPurchase_Click", ex);
            }
        }

        private void btnSetDefaultAdvancedPurchase_Click(object sender, EventArgs e)
        {
            for (int ix = 0; ix < dgvAdvancedPurchase.Rows.Count; ix++)
            {
                decimal count = DataConvert.GetInt64(dgvAdvancedPurchase.Rows[ix].Cells[0].Value) / DataConvert.GetInt64(dgvAdvancedPurchase.Rows[ix].Cells[1].Value);
                dgvAdvancedPurchase.Rows[ix].Cells[2].Value = Math.Round(count, 0);
            }
        }

        private void btnSaveAdvancedPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ix = 0; ix < dgvAdvancedPurchase.Rows.Count; ix++)
                {
                    if (!DataValidation.IsNaturalNumber(dgvAdvancedPurchase.Rows[ix].Cells[2].Value))
                    {
                        MessageBox.Show("最小购买数量必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvAdvancedPurchase.ClearSelection();
                        dgvAdvancedPurchase.Rows[ix].Cells[3].Selected = true;
                        return;
                    }                   
                }

                BindingSource bs = dgvAdvancedPurchase.DataSource as BindingSource;
                if (bs != null)
                {
                    DataTable dt = bs.DataSource as DataTable;
                    if (dt != null)
                    {
                        if (ConfigCtrl.SetAdvancedPurchaseFromTable(dt))
                            MessageBox.Show("保存成功！");
                        else
                            MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnSaveAdvancedPurchase_Click", ex);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                lblResult.Text = DataConvert.GetString(_gRich.GetMinimumPurchaseCount(ConfigCtrl.GetAdvancedPurchaseMD(), DataConvert.GetInt64(txtCash.Text), DataConvert.GetInt64(txtPrice.Text)));
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnCalculate_Click", ex);
            }
        }
        #endregion

        #region Cafe
        private void btnReloadDishes_Click(object sender, EventArgs e)
        {
            try
            {
                BuildLstDishes(ConfigCtrl.GetDishesInMenu());
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadDishes_Click", ex);
            }
        }

        private void btnRefreshDishes_Click(object sender, EventArgs e)
        {

            try
            {
                if (CheckAll())
                    _toolupdatedata.RefreshDishes();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnRefreshDishes_Click", ex);
            }
        }

        private void btnReloadTransactionDishes_Click(object sender, EventArgs e)
        {
            try
            {
                BuildDgvTransactionDishes(AddColumnsToTransactionDishes(ConfigCtrl.GetTransactionDishesToTable()), "菜肴交易价格表读取失败！");
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadTransactionDishes_Click", ex);
            }
        }

        private void btnRefreshTransactionDishes_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckAll())
                    _toolupdatedata.RefreshTransactionDishes();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnReloadTransactionDishes_Click", ex);
            }
        }

        private void btnSaveTransactionDishes_Click(object sender, EventArgs e)
        {
            try
            {
                //for (int ix = 0; ix < dgvTransactionDishes.Rows.Count; ix++)
                //{
                //    if (!DataValidation.IsNaturalNumber(dgvTransactionDishes.Rows[ix].Cells[4].Value))
                //    {
                //        MessageBox.Show("出售价必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        dgvTransactionDishes.ClearSelection();
                //        dgvTransactionDishes.Rows[ix].Cells[4].Selected = true;
                //        return;
                //    }

                //    if (!DataValidation.IsNaturalNumber(dgvTransactionDishes.Rows[ix].Cells[5].Value))
                //    {
                //        MessageBox.Show("收购价必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        dgvTransactionDishes.ClearSelection();
                //        dgvTransactionDishes.Rows[ix].Cells[5].Selected = true;
                //        return;
                //    }
                //}

                BindingSource bs = dgvTransactionDishes.DataSource as BindingSource;
                if (bs != null)
                {
                    DataTable dt = bs.DataSource as DataTable;
                    if (dt != null)
                    {
                        if (ConfigCtrl.SetTransactionDishesToFile(dt))
                        {
                            MessageBox.Show("保存成功！");
                            return;
                        }
                    }
                }

                MessageBox.Show("保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmUpdateData.btnSaveTransactionDishes_Click", ex);
            }
        }
        #endregion

        #region Private methods
        private bool IsValidSeedId(int seedid)
        {
            foreach (SeedInfo seed in _shopseeds)
            {
                if (seed.SeedId == seedid)
                    return true;
            }
            return false;
        }

        private string GetSeedNameById(int seedid)
        {
            foreach (SeedInfo seed in _shopseeds)
            {
                if (seed.SeedId == seedid)
                    return seed.Name;
            }
            return "";
        }

        private int GetSeedIdByName(string seedname)
        {
            foreach (SeedInfo seed in _shopseeds)
            {
                if (seed.Name == seedname)
                    return seed.SeedId;
            }
            return 0;
        }

        private string FormatPrice(double standardprice)
        {
            if (standardprice < 10000)
                return standardprice + "元";
            else if (standardprice >= 10000 && standardprice < 100000000)
                return (standardprice / 10000).ToString() + "万元";
            else if (standardprice >= 100000000 && standardprice < 1000000000000)
                return (standardprice / 100000000).ToString() + "亿元";
            else
                return standardprice + "元";
        }
        #endregion
    }
}