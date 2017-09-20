namespace Johnny.Kaixin.WinUI
{
    partial class FrmTaskEditor
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaskEditor));
            this.lblSelectedAccounts = new System.Windows.Forms.Label();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.txtRoundTime = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUnselectOne = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectOne = new System.Windows.Forms.Button();
            this.lstSelectedAccounts = new System.Windows.Forms.ListBox();
            this.lstAllAccounts = new System.Windows.Forms.ListBox();
            this.grpAccounts = new System.Windows.Forms.GroupBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.grpTasks = new System.Windows.Forms.GroupBox();
            this.chkTaskCafe = new System.Windows.Forms.CheckBox();
            this.chkTaskRich = new System.Windows.Forms.CheckBox();
            this.chkTaskFish = new System.Windows.Forms.CheckBox();
            this.chkTaskRanch = new System.Windows.Forms.CheckBox();
            this.chkWriteLogToFile = new System.Windows.Forms.CheckBox();
            this.chkTaskGarden = new System.Windows.Forms.CheckBox();
            this.chkTaskHouse = new System.Windows.Forms.CheckBox();
            this.lblReceiverEmail = new System.Windows.Forms.Label();
            this.txtReceiverEmail = new System.Windows.Forms.TextBox();
            this.chkSendLog = new System.Windows.Forms.CheckBox();
            this.chkTaskSlave = new System.Windows.Forms.CheckBox();
            this.chkTaskParking = new System.Windows.Forms.CheckBox();
            this.chkTaskBiting = new System.Windows.Forms.CheckBox();
            this.lblLoopTime = new System.Windows.Forms.Label();
            this.grpLoopTime = new System.Windows.Forms.GroupBox();
            this.grpRunMode = new System.Windows.Forms.GroupBox();
            this.rdbMultiLoop = new System.Windows.Forms.RadioButton();
            this.rdbTiming = new System.Windows.Forms.RadioButton();
            this.rdbSingleLoop = new System.Windows.Forms.RadioButton();
            this.grpValidation = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbPopupValidationWindow = new System.Windows.Forms.RadioButton();
            this.rdbSkip = new System.Windows.Forms.RadioButton();
            this.grpLoop = new System.Windows.Forms.GroupBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.timeForbiddenEnd = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.chkForbidden = new System.Windows.Forms.CheckBox();
            this.timeForbiddenStart = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.lblStart = new System.Windows.Forms.Label();
            this.groupTiming = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.timeSelector10 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timeSelector9 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector8 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector7 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector6 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector5 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector4 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector3 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector2 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.timeSelector1 = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBreedAnimal = new System.Windows.Forms.CheckBox();
            this.chkHarvestProduct = new System.Windows.Forms.CheckBox();
            this.chkHelpMakeProduct = new System.Windows.Forms.CheckBox();
            this.chkMakeProduct = new System.Windows.Forms.CheckBox();
            this.chkStealProduct = new System.Windows.Forms.CheckBox();
            this.grpPark = new System.Windows.Forms.GroupBox();
            this.lblWarningPostCars = new System.Windows.Forms.Label();
            this.lblWarningParkCars = new System.Windows.Forms.Label();
            this.panelStartCar = new System.Windows.Forms.Panel();
            this.lblStartCarTime = new System.Windows.Forms.Label();
            this.timeStartCarTime = new Johnny.Controls.Windows.DateTimer.TimeSelector();
            this.panelOriginateMatch = new System.Windows.Forms.Panel();
            this.lblMatch = new System.Windows.Forms.Label();
            this.cmbOriginateTeamNum = new System.Windows.Forms.ComboBox();
            this.cmbOriginateMatchId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkJoinMatch = new System.Windows.Forms.CheckBox();
            this.chkStartCar = new System.Windows.Forms.CheckBox();
            this.chkOriginateMatch = new System.Windows.Forms.CheckBox();
            this.chkCheerUp = new System.Windows.Forms.CheckBox();
            this.chkParkMyCars = new System.Windows.Forms.CheckBox();
            this.chkPostOthersCars = new System.Windows.Forms.CheckBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.cmbBuySeedCount = new System.Windows.Forms.ComboBox();
            this.chkBuySeed = new System.Windows.Forms.CheckBox();
            this.chkStealFruit = new System.Windows.Forms.CheckBox();
            this.chkFarmSelf = new System.Windows.Forms.CheckBox();
            this.chkHarvestFruit = new System.Windows.Forms.CheckBox();
            this.grpGarden = new System.Windows.Forms.GroupBox();
            this.lblWarningPresentFruit = new System.Windows.Forms.Label();
            this.lblWarningFarmShared = new System.Windows.Forms.Label();
            this.lblWarningStealFruit = new System.Windows.Forms.Label();
            this.panelPresentFruit = new System.Windows.Forms.Panel();
            this.lblPresentFruitCheckNum = new System.Windows.Forms.Label();
            this.lblPresentFruitCheckValue = new System.Windows.Forms.Label();
            this.txtPresentFruitNum = new System.Windows.Forms.TextBox();
            this.txtPresentFruitValue = new System.Windows.Forms.TextBox();
            this.chkPresentFruitCheckValue = new System.Windows.Forms.CheckBox();
            this.chkPresentFruitCheckNum = new System.Windows.Forms.CheckBox();
            this.rdbPresentFruitByPrice = new System.Windows.Forms.RadioButton();
            this.cmbPresentFruitId = new System.Windows.Forms.ComboBox();
            this.rdbPresentFruitCustom = new System.Windows.Forms.RadioButton();
            this.panelSellFruit = new System.Windows.Forms.Panel();
            this.panelSellForbiddenFruit = new System.Windows.Forms.Panel();
            this.btnForbiddenFruitList = new System.Windows.Forms.Button();
            this.lblMaxSellLimit1 = new System.Windows.Forms.Label();
            this.txtMaxSellLimit = new System.Windows.Forms.TextBox();
            this.lblMaxSellLimit2 = new System.Windows.Forms.Label();
            this.lblLowCash = new System.Windows.Forms.Label();
            this.txtLowCashLimit = new System.Windows.Forms.TextBox();
            this.chkLowCash = new System.Windows.Forms.CheckBox();
            this.rdbSellAllFruit = new System.Windows.Forms.RadioButton();
            this.rdbFobiddenFruit = new System.Windows.Forms.RadioButton();
            this.chkStealUnknowFruit = new System.Windows.Forms.CheckBox();
            this.btnSetFruitList = new System.Windows.Forms.Button();
            this.chkSowMySeedsFirst = new System.Windows.Forms.CheckBox();
            this.chkSellFruit = new System.Windows.Forms.CheckBox();
            this.chkPresentFruit = new System.Windows.Forms.CheckBox();
            this.panelFarmSelf = new System.Windows.Forms.Panel();
            this.rdbExpensiveFarmSelf = new System.Windows.Forms.RadioButton();
            this.cmbCustomFarmSelf = new System.Windows.Forms.ComboBox();
            this.rdbCustomFarmSelf = new System.Windows.Forms.RadioButton();
            this.lblFarmSelf = new System.Windows.Forms.Label();
            this.panelFarmShared = new System.Windows.Forms.Panel();
            this.cmbCustomFarmShared = new System.Windows.Forms.ComboBox();
            this.rdbCustomFarmShared = new System.Windows.Forms.RadioButton();
            this.rdbExpensiveFarmShared = new System.Windows.Forms.RadioButton();
            this.lblFarmShared = new System.Windows.Forms.Label();
            this.chkHelpOthers = new System.Windows.Forms.CheckBox();
            this.lblSeedsCount = new System.Windows.Forms.Label();
            this.chkFarmShared = new System.Windows.Forms.CheckBox();
            this.grpRanch = new System.Windows.Forms.GroupBox();
            this.lblWarningHelpRanch = new System.Windows.Forms.Label();
            this.lblWarningStealProduct = new System.Windows.Forms.Label();
            this.lblWarningPresentProduct = new System.Windows.Forms.Label();
            this.panelSellProduct = new System.Windows.Forms.Panel();
            this.panelSellProductForbidden = new System.Windows.Forms.Panel();
            this.btnSellProductForbiddenList = new System.Windows.Forms.Button();
            this.lblSellProductMaxLimit = new System.Windows.Forms.Label();
            this.txtSellProductMaxLimit = new System.Windows.Forms.TextBox();
            this.lblSellProductMaxLimit2 = new System.Windows.Forms.Label();
            this.lblSellProductLowCash = new System.Windows.Forms.Label();
            this.txtSellProductLowCashLimit = new System.Windows.Forms.TextBox();
            this.chkSellProductLowCash = new System.Windows.Forms.CheckBox();
            this.rdbSellAllProducts = new System.Windows.Forms.RadioButton();
            this.rdbSellForbiddenProduct = new System.Windows.Forms.RadioButton();
            this.panelPresentProduct = new System.Windows.Forms.Panel();
            this.lblPresentProductCheckValue = new System.Windows.Forms.Label();
            this.txtPresentProductValue = new System.Windows.Forms.TextBox();
            this.chkPresentProductCheckValue = new System.Windows.Forms.CheckBox();
            this.lblPresentProductCheckNum = new System.Windows.Forms.Label();
            this.txtPresentProductNum = new System.Windows.Forms.TextBox();
            this.chkPresentProductCheckNum = new System.Windows.Forms.CheckBox();
            this.rdbPresentProductByPrice = new System.Windows.Forms.RadioButton();
            this.cmbAnimalProducts = new System.Windows.Forms.ComboBox();
            this.rdbPresentProductCustom = new System.Windows.Forms.RadioButton();
            this.panelBuyCalf = new System.Windows.Forms.Panel();
            this.rdbBuyCalfByPrice = new System.Windows.Forms.RadioButton();
            this.cmbBuyCalfCustom = new System.Windows.Forms.ComboBox();
            this.rdbBuyCalfCustom = new System.Windows.Forms.RadioButton();
            this.txtBambooNum = new System.Windows.Forms.TextBox();
            this.lblBambooNum = new System.Windows.Forms.Label();
            this.chkHelpAddBamboo = new System.Windows.Forms.CheckBox();
            this.chkAddBamboo = new System.Windows.Forms.CheckBox();
            this.lblCarrotNum = new System.Windows.Forms.Label();
            this.txtCarrotNum = new System.Windows.Forms.TextBox();
            this.chkHelpAddCarrot = new System.Windows.Forms.CheckBox();
            this.chkAddCarrot = new System.Windows.Forms.CheckBox();
            this.chkSellProduct = new System.Windows.Forms.CheckBox();
            this.chkPresentProduct = new System.Windows.Forms.CheckBox();
            this.chkHarvestAnimal = new System.Windows.Forms.CheckBox();
            this.lblFoodNum = new System.Windows.Forms.Label();
            this.txtFoodNum = new System.Windows.Forms.TextBox();
            this.chkBuyCalf = new System.Windows.Forms.CheckBox();
            this.chkHelpAddGrass = new System.Windows.Forms.CheckBox();
            this.chkAddGrass = new System.Windows.Forms.CheckBox();
            this.chkHelpAddWater = new System.Windows.Forms.CheckBox();
            this.chkAddWater = new System.Windows.Forms.CheckBox();
            this.tabGames = new System.Windows.Forms.TabControl();
            this.tabPagePark = new System.Windows.Forms.TabPage();
            this.tabPageBite = new System.Windows.Forms.TabPage();
            this.grpBite = new System.Windows.Forms.GroupBox();
            this.lblWarningProtect = new System.Windows.Forms.Label();
            this.lblWarningRecover = new System.Windows.Forms.Label();
            this.lblWarningBite = new System.Windows.Forms.Label();
            this.chkProtectFriend = new System.Windows.Forms.CheckBox();
            this.chkAutoRecover = new System.Windows.Forms.CheckBox();
            this.chkApproveRecovery = new System.Windows.Forms.CheckBox();
            this.chkBiteOthers = new System.Windows.Forms.CheckBox();
            this.tabPageSlave = new System.Windows.Forms.TabPage();
            this.grpSlave = new System.Windows.Forms.GroupBox();
            this.lblWarningBuySlave = new System.Windows.Forms.Label();
            this.chkFawnMaster = new System.Windows.Forms.CheckBox();
            this.chkBuySlave = new System.Windows.Forms.CheckBox();
            this.chkBuyLowPriceSlave = new System.Windows.Forms.CheckBox();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.chkPropitiateSlave = new System.Windows.Forms.CheckBox();
            this.lblNickName = new System.Windows.Forms.Label();
            this.chkAfflictSlave = new System.Windows.Forms.CheckBox();
            this.lblMaxSlaves = new System.Windows.Forms.Label();
            this.chkReleaseSlave = new System.Windows.Forms.CheckBox();
            this.cmbMaxSlaves = new System.Windows.Forms.ComboBox();
            this.tabPageHouse = new System.Windows.Forms.TabPage();
            this.grpHouse = new System.Windows.Forms.GroupBox();
            this.lblWarningRobFriends = new System.Windows.Forms.Label();
            this.lblWarningStayHouse = new System.Windows.Forms.Label();
            this.chkDriveFriends = new System.Windows.Forms.CheckBox();
            this.chkRobFreeFriends = new System.Windows.Forms.CheckBox();
            this.chkRobFriends = new System.Windows.Forms.CheckBox();
            this.chkStayHouse = new System.Windows.Forms.CheckBox();
            this.chkDoJob = new System.Windows.Forms.CheckBox();
            this.tabPageGarden = new System.Windows.Forms.TabPage();
            this.tabPageRanch = new System.Windows.Forms.TabPage();
            this.tabPageFish = new System.Windows.Forms.TabPage();
            this.grpFish = new System.Windows.Forms.GroupBox();
            this.lblWarningFishing = new System.Windows.Forms.Label();
            this.lblWarningHelpFish = new System.Windows.Forms.Label();
            this.lblWarningPresentFish = new System.Windows.Forms.Label();
            this.panelBuyUpdateTackle = new System.Windows.Forms.Panel();
            this.lblMaxTackles = new System.Windows.Forms.Label();
            this.cmbMaxTackles = new System.Windows.Forms.ComboBox();
            this.panelSellFish = new System.Windows.Forms.Panel();
            this.panelSellFishForbidden = new System.Windows.Forms.Panel();
            this.lblSellFishMaxLimit2 = new System.Windows.Forms.Label();
            this.lblSellFishCheckValue = new System.Windows.Forms.Label();
            this.txtSellFishMaxLimit = new System.Windows.Forms.TextBox();
            this.btnSellFishForbiddenList = new System.Windows.Forms.Button();
            this.lblSellFishMaxLimit = new System.Windows.Forms.Label();
            this.chkSellFishCheckValue = new System.Windows.Forms.CheckBox();
            this.txtSellFishValue = new System.Windows.Forms.TextBox();
            this.lblSellFishLowCash = new System.Windows.Forms.Label();
            this.txtSellFishLowCashLimit = new System.Windows.Forms.TextBox();
            this.chkSellFishLowCash = new System.Windows.Forms.CheckBox();
            this.rdbSellAllFish = new System.Windows.Forms.RadioButton();
            this.rdbForbiddenFish = new System.Windows.Forms.RadioButton();
            this.chkBangKeJing = new System.Windows.Forms.CheckBox();
            this.panelPresentFish = new System.Windows.Forms.Panel();
            this.rdbPresentFishCheap = new System.Windows.Forms.RadioButton();
            this.rdbPresentFishExpensive = new System.Windows.Forms.RadioButton();
            this.lblPresentFishCheap = new System.Windows.Forms.Label();
            this.chkPresentFishCheckValue = new System.Windows.Forms.CheckBox();
            this.btnPresentFishForbiddenList = new System.Windows.Forms.Button();
            this.lblPresentFishValue = new System.Windows.Forms.Label();
            this.txtPresentFishValue = new System.Windows.Forms.TextBox();
            this.panelBuyFish = new System.Windows.Forms.Panel();
            this.cmbMaxFishes = new System.Windows.Forms.ComboBox();
            this.lblMaxFishes = new System.Windows.Forms.Label();
            this.rdbBuyFishByRank = new System.Windows.Forms.RadioButton();
            this.cmbBuyFishFishId = new System.Windows.Forms.ComboBox();
            this.rdbBuyFishCustom = new System.Windows.Forms.RadioButton();
            this.panelNetSelfFish = new System.Windows.Forms.Panel();
            this.lblNetSelfFishMaturePercentage = new System.Windows.Forms.Label();
            this.lblNetSelfFishCheap = new System.Windows.Forms.Label();
            this.lblNetSelfFishMature = new System.Windows.Forms.Label();
            this.rdbNetSelfFishExpensive = new System.Windows.Forms.RadioButton();
            this.txtNetSelfFishMature = new System.Windows.Forms.TextBox();
            this.rdbNetSelfFishCheap = new System.Windows.Forms.RadioButton();
            this.chkNetSelfFish = new System.Windows.Forms.CheckBox();
            this.chkUpdateFishPond = new System.Windows.Forms.CheckBox();
            this.chkBuyUpdateTackle = new System.Windows.Forms.CheckBox();
            this.chkFishing = new System.Windows.Forms.CheckBox();
            this.chkSellFish = new System.Windows.Forms.CheckBox();
            this.chkPresentFish = new System.Windows.Forms.CheckBox();
            this.chkTreatFish = new System.Windows.Forms.CheckBox();
            this.chkHelpFish = new System.Windows.Forms.CheckBox();
            this.chkHarvestFish = new System.Windows.Forms.CheckBox();
            this.chkBuyFish = new System.Windows.Forms.CheckBox();
            this.chkShake = new System.Windows.Forms.CheckBox();
            this.tabPageRich = new System.Windows.Forms.TabPage();
            this.grpRich = new System.Windows.Forms.GroupBox();
            this.lblRich = new System.Windows.Forms.Label();
            this.panelBuyAsset = new System.Windows.Forms.Panel();
            this.lblIfMinimum = new System.Windows.Forms.Label();
            this.lblGiveUpMinimum = new System.Windows.Forms.Label();
            this.txtGiveUpMinimum = new System.Windows.Forms.TextBox();
            this.chkGiveUpIfMinimum = new System.Windows.Forms.CheckBox();
            this.lblAdvancedPurchase = new System.Windows.Forms.Label();
            this.chkAdvancedPurchase = new System.Windows.Forms.CheckBox();
            this.lblGiveUpAssetCount = new System.Windows.Forms.Label();
            this.txtGiveUpAssetCount = new System.Windows.Forms.TextBox();
            this.chkGiveUpIfMyAsset = new System.Windows.Forms.CheckBox();
            this.btnSetBuyAssetsList = new System.Windows.Forms.Button();
            this.lblGiveUpRatio = new System.Windows.Forms.Label();
            this.txtGiveUpRatio = new System.Windows.Forms.TextBox();
            this.chkGiveUpIfRatio = new System.Windows.Forms.CheckBox();
            this.panelBuyAssetCheap = new System.Windows.Forms.Panel();
            this.rdbBuyAssetExpensive = new System.Windows.Forms.RadioButton();
            this.rdbBuyAssetCheap = new System.Windows.Forms.RadioButton();
            this.lblBuyAssetCheap = new System.Windows.Forms.Label();
            this.chkBuyAsset = new System.Windows.Forms.CheckBox();
            this.chkSellAsset = new System.Windows.Forms.CheckBox();
            this.tabPageCafe = new System.Windows.Forms.TabPage();
            this.grpCafe = new System.Windows.Forms.GroupBox();
            this.lblWarningPurchaseFood = new System.Windows.Forms.Label();
            this.lblWarningPresentFood = new System.Windows.Forms.Label();
            this.lblWarningHire = new System.Windows.Forms.Label();
            this.chkHelpFriend = new System.Windows.Forms.CheckBox();
            this.panelHire = new System.Windows.Forms.Panel();
            this.lblMaxEmployees = new System.Windows.Forms.Label();
            this.cmbMaxEmployees = new System.Windows.Forms.ComboBox();
            this.panelPurchaseFood = new System.Windows.Forms.Panel();
            this.rdbPurchaseFoodByRefPrice = new System.Windows.Forms.RadioButton();
            this.rdbPurchaseFoodIgnorePrice = new System.Windows.Forms.RadioButton();
            this.chkPurchaseFood = new System.Windows.Forms.CheckBox();
            this.panelSellFood = new System.Windows.Forms.Panel();
            this.rdbSellFoodByRefPrice = new System.Windows.Forms.RadioButton();
            this.rdbSellFoodIgnorePrice = new System.Windows.Forms.RadioButton();
            this.chkSellFood = new System.Windows.Forms.CheckBox();
            this.chkBoxClean = new System.Windows.Forms.CheckBox();
            this.panelPresentFood = new System.Windows.Forms.Panel();
            this.btnPresentForbiddenFoodList = new System.Windows.Forms.Button();
            this.txtPresentFoodMessage = new System.Windows.Forms.TextBox();
            this.lblPresentFoodMessage = new System.Windows.Forms.Label();
            this.lblPresentFoodLowCount = new System.Windows.Forms.Label();
            this.txtPresentFoodLowCountLimit = new System.Windows.Forms.TextBox();
            this.chkPresentFoodLowCount = new System.Windows.Forms.CheckBox();
            this.cmbPresentFoodDishId = new System.Windows.Forms.ComboBox();
            this.rdbPresentFoodDishId = new System.Windows.Forms.RadioButton();
            this.rdbPresentFoodByCount = new System.Windows.Forms.RadioButton();
            this.chkPresentLowCash = new System.Windows.Forms.CheckBox();
            this.txtPresentLowCashLimit = new System.Windows.Forms.TextBox();
            this.lblPresentFoodRatioPercentage = new System.Windows.Forms.Label();
            this.lblPresentFoodRatio = new System.Windows.Forms.Label();
            this.txtPresentFoodRatio = new System.Windows.Forms.TextBox();
            this.lblPresentLowCash = new System.Windows.Forms.Label();
            this.panelCook = new System.Windows.Forms.Panel();
            this.chkCookMedlarFirst = new System.Windows.Forms.CheckBox();
            this.chkCookCrabFirst = new System.Windows.Forms.CheckBox();
            this.chkCookPineappleFirst = new System.Windows.Forms.CheckBox();
            this.chkCookTomatoFirst = new System.Windows.Forms.CheckBox();
            this.cmbCookDishId = new System.Windows.Forms.ComboBox();
            this.lblCookDishId = new System.Windows.Forms.Label();
            this.chkCookLowCash = new System.Windows.Forms.CheckBox();
            this.lblCookLowCash = new System.Windows.Forms.Label();
            this.txtCookLowCashLimit = new System.Windows.Forms.TextBox();
            this.chkPresentFood = new System.Windows.Forms.CheckBox();
            this.chkHire = new System.Windows.Forms.CheckBox();
            this.chkCook = new System.Windows.Forms.CheckBox();
            this.lblWarningNotes = new System.Windows.Forms.Label();
            this.grpAccounts.SuspendLayout();
            this.grpTasks.SuspendLayout();
            this.grpLoopTime.SuspendLayout();
            this.grpRunMode.SuspendLayout();
            this.grpValidation.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpLoop.SuspendLayout();
            this.groupTiming.SuspendLayout();
            this.grpPark.SuspendLayout();
            this.panelStartCar.SuspendLayout();
            this.panelOriginateMatch.SuspendLayout();
            this.grpGarden.SuspendLayout();
            this.panelPresentFruit.SuspendLayout();
            this.panelSellFruit.SuspendLayout();
            this.panelSellForbiddenFruit.SuspendLayout();
            this.panelFarmSelf.SuspendLayout();
            this.panelFarmShared.SuspendLayout();
            this.grpRanch.SuspendLayout();
            this.panelSellProduct.SuspendLayout();
            this.panelSellProductForbidden.SuspendLayout();
            this.panelPresentProduct.SuspendLayout();
            this.panelBuyCalf.SuspendLayout();
            this.tabGames.SuspendLayout();
            this.tabPagePark.SuspendLayout();
            this.tabPageBite.SuspendLayout();
            this.grpBite.SuspendLayout();
            this.tabPageSlave.SuspendLayout();
            this.grpSlave.SuspendLayout();
            this.tabPageHouse.SuspendLayout();
            this.grpHouse.SuspendLayout();
            this.tabPageGarden.SuspendLayout();
            this.tabPageRanch.SuspendLayout();
            this.tabPageFish.SuspendLayout();
            this.grpFish.SuspendLayout();
            this.panelBuyUpdateTackle.SuspendLayout();
            this.panelSellFish.SuspendLayout();
            this.panelSellFishForbidden.SuspendLayout();
            this.panelPresentFish.SuspendLayout();
            this.panelBuyFish.SuspendLayout();
            this.panelNetSelfFish.SuspendLayout();
            this.tabPageRich.SuspendLayout();
            this.grpRich.SuspendLayout();
            this.panelBuyAsset.SuspendLayout();
            this.panelBuyAssetCheap.SuspendLayout();
            this.tabPageCafe.SuspendLayout();
            this.grpCafe.SuspendLayout();
            this.panelHire.SuspendLayout();
            this.panelPurchaseFood.SuspendLayout();
            this.panelSellFood.SuspendLayout();
            this.panelPresentFood.SuspendLayout();
            this.panelCook.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSelectedAccounts
            // 
            this.lblSelectedAccounts.AutoSize = true;
            this.lblSelectedAccounts.ForeColor = System.Drawing.Color.Red;
            this.lblSelectedAccounts.Location = new System.Drawing.Point(249, 23);
            this.lblSelectedAccounts.Name = "lblSelectedAccounts";
            this.lblSelectedAccounts.Size = new System.Drawing.Size(95, 12);
            this.lblSelectedAccounts.TabIndex = 17;
            this.lblSelectedAccounts.Text = "*需要执行的账号";
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(196, 219);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(40, 23);
            this.btnUnselectAll.TabIndex = 16;
            this.btnUnselectAll.Text = "<<";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(142, 23);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(29, 12);
            this.lblMinutes.TabIndex = 2;
            this.lblMinutes.Text = "分钟";
            // 
            // txtRoundTime
            // 
            this.txtRoundTime.Location = new System.Drawing.Point(76, 18);
            this.txtRoundTime.Name = "txtRoundTime";
            this.txtRoundTime.Size = new System.Drawing.Size(60, 21);
            this.txtRoundTime.TabIndex = 1;
            this.txtRoundTime.Text = "60";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(742, 299);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 42);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUnselectOne
            // 
            this.btnUnselectOne.Location = new System.Drawing.Point(196, 165);
            this.btnUnselectOne.Name = "btnUnselectOne";
            this.btnUnselectOne.Size = new System.Drawing.Size(40, 23);
            this.btnUnselectOne.TabIndex = 15;
            this.btnUnselectOne.Text = "<";
            this.btnUnselectOne.UseVisualStyleBackColor = true;
            this.btnUnselectOne.Click += new System.EventHandler(this.btnUnselectOne_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(196, 111);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(40, 23);
            this.btnSelectAll.TabIndex = 14;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectOne
            // 
            this.btnSelectOne.Location = new System.Drawing.Point(196, 59);
            this.btnSelectOne.Name = "btnSelectOne";
            this.btnSelectOne.Size = new System.Drawing.Size(40, 23);
            this.btnSelectOne.TabIndex = 13;
            this.btnSelectOne.Text = ">";
            this.btnSelectOne.UseVisualStyleBackColor = true;
            this.btnSelectOne.Click += new System.EventHandler(this.btnSelectOne_Click);
            // 
            // lstSelectedAccounts
            // 
            this.lstSelectedAccounts.FormattingEnabled = true;
            this.lstSelectedAccounts.ItemHeight = 12;
            this.lstSelectedAccounts.Location = new System.Drawing.Point(251, 48);
            this.lstSelectedAccounts.Name = "lstSelectedAccounts";
            this.lstSelectedAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelectedAccounts.Size = new System.Drawing.Size(182, 208);
            this.lstSelectedAccounts.TabIndex = 12;
            // 
            // lstAllAccounts
            // 
            this.lstAllAccounts.FormattingEnabled = true;
            this.lstAllAccounts.ItemHeight = 12;
            this.lstAllAccounts.Location = new System.Drawing.Point(8, 48);
            this.lstAllAccounts.Name = "lstAllAccounts";
            this.lstAllAccounts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAllAccounts.Size = new System.Drawing.Size(174, 208);
            this.lstAllAccounts.TabIndex = 8;
            // 
            // grpAccounts
            // 
            this.grpAccounts.Controls.Add(this.lblGroup);
            this.grpAccounts.Controls.Add(this.cmbGroup);
            this.grpAccounts.Controls.Add(this.btnSelectAll);
            this.grpAccounts.Controls.Add(this.btnUnselectAll);
            this.grpAccounts.Controls.Add(this.lstSelectedAccounts);
            this.grpAccounts.Controls.Add(this.btnUnselectOne);
            this.grpAccounts.Controls.Add(this.lstAllAccounts);
            this.grpAccounts.Controls.Add(this.btnSelectOne);
            this.grpAccounts.Controls.Add(this.lblSelectedAccounts);
            this.grpAccounts.Location = new System.Drawing.Point(8, 6);
            this.grpAccounts.Name = "grpAccounts";
            this.grpAccounts.Size = new System.Drawing.Size(442, 267);
            this.grpAccounts.TabIndex = 25;
            this.grpAccounts.TabStop = false;
            this.grpAccounts.Text = "所要执行的帐号";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(6, 23);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(29, 12);
            this.lblGroup.TabIndex = 40;
            this.lblGroup.Text = "组：";
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(40, 20);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(142, 20);
            this.cmbGroup.TabIndex = 18;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // grpTasks
            // 
            this.grpTasks.Controls.Add(this.chkTaskCafe);
            this.grpTasks.Controls.Add(this.chkTaskRich);
            this.grpTasks.Controls.Add(this.chkTaskFish);
            this.grpTasks.Controls.Add(this.chkTaskRanch);
            this.grpTasks.Controls.Add(this.chkWriteLogToFile);
            this.grpTasks.Controls.Add(this.chkTaskGarden);
            this.grpTasks.Controls.Add(this.chkTaskHouse);
            this.grpTasks.Controls.Add(this.lblReceiverEmail);
            this.grpTasks.Controls.Add(this.txtReceiverEmail);
            this.grpTasks.Controls.Add(this.chkSendLog);
            this.grpTasks.Controls.Add(this.chkTaskSlave);
            this.grpTasks.Controls.Add(this.chkTaskParking);
            this.grpTasks.Controls.Add(this.chkTaskBiting);
            this.grpTasks.Location = new System.Drawing.Point(638, 380);
            this.grpTasks.Name = "grpTasks";
            this.grpTasks.Size = new System.Drawing.Size(198, 168);
            this.grpTasks.TabIndex = 23;
            this.grpTasks.TabStop = false;
            this.grpTasks.Text = "任务";
            // 
            // chkTaskCafe
            // 
            this.chkTaskCafe.AutoSize = true;
            this.chkTaskCafe.Location = new System.Drawing.Point(11, 82);
            this.chkTaskCafe.Name = "chkTaskCafe";
            this.chkTaskCafe.Size = new System.Drawing.Size(72, 16);
            this.chkTaskCafe.TabIndex = 35;
            this.chkTaskCafe.Text = "开心餐厅";
            this.chkTaskCafe.UseVisualStyleBackColor = true;
            this.chkTaskCafe.CheckedChanged += new System.EventHandler(this.chkTaskCafe_CheckedChanged);
            // 
            // chkTaskRich
            // 
            this.chkTaskRich.AutoSize = true;
            this.chkTaskRich.Location = new System.Drawing.Point(82, 60);
            this.chkTaskRich.Name = "chkTaskRich";
            this.chkTaskRich.Size = new System.Drawing.Size(72, 16);
            this.chkTaskRich.TabIndex = 34;
            this.chkTaskRich.Text = "超级大亨";
            this.chkTaskRich.UseVisualStyleBackColor = true;
            this.chkTaskRich.CheckedChanged += new System.EventHandler(this.chkTaskRich_CheckedChanged);
            // 
            // chkTaskFish
            // 
            this.chkTaskFish.AutoSize = true;
            this.chkTaskFish.Location = new System.Drawing.Point(11, 60);
            this.chkTaskFish.Name = "chkTaskFish";
            this.chkTaskFish.Size = new System.Drawing.Size(48, 16);
            this.chkTaskFish.TabIndex = 33;
            this.chkTaskFish.Text = "钓鱼";
            this.chkTaskFish.UseVisualStyleBackColor = true;
            this.chkTaskFish.CheckedChanged += new System.EventHandler(this.chkTaskFish_CheckedChanged);
            // 
            // chkTaskRanch
            // 
            this.chkTaskRanch.AutoSize = true;
            this.chkTaskRanch.Location = new System.Drawing.Point(130, 39);
            this.chkTaskRanch.Name = "chkTaskRanch";
            this.chkTaskRanch.Size = new System.Drawing.Size(48, 16);
            this.chkTaskRanch.TabIndex = 32;
            this.chkTaskRanch.Text = "牧场";
            this.chkTaskRanch.UseVisualStyleBackColor = true;
            this.chkTaskRanch.CheckedChanged += new System.EventHandler(this.chkTaskRanch_CheckedChanged);
            // 
            // chkWriteLogToFile
            // 
            this.chkWriteLogToFile.AutoSize = true;
            this.chkWriteLogToFile.Location = new System.Drawing.Point(87, 104);
            this.chkWriteLogToFile.Name = "chkWriteLogToFile";
            this.chkWriteLogToFile.Size = new System.Drawing.Size(108, 16);
            this.chkWriteLogToFile.TabIndex = 31;
            this.chkWriteLogToFile.Text = "保存日志到文件";
            this.chkWriteLogToFile.UseVisualStyleBackColor = true;
            // 
            // chkTaskGarden
            // 
            this.chkTaskGarden.AutoSize = true;
            this.chkTaskGarden.Location = new System.Drawing.Point(82, 39);
            this.chkTaskGarden.Name = "chkTaskGarden";
            this.chkTaskGarden.Size = new System.Drawing.Size(48, 16);
            this.chkTaskGarden.TabIndex = 30;
            this.chkTaskGarden.Text = "花园";
            this.chkTaskGarden.UseVisualStyleBackColor = true;
            this.chkTaskGarden.CheckedChanged += new System.EventHandler(this.chkTaskGarden_CheckedChanged);
            // 
            // chkTaskHouse
            // 
            this.chkTaskHouse.AutoSize = true;
            this.chkTaskHouse.Location = new System.Drawing.Point(130, 19);
            this.chkTaskHouse.Name = "chkTaskHouse";
            this.chkTaskHouse.Size = new System.Drawing.Size(60, 16);
            this.chkTaskHouse.TabIndex = 29;
            this.chkTaskHouse.Text = "买房子";
            this.chkTaskHouse.UseVisualStyleBackColor = true;
            this.chkTaskHouse.CheckedChanged += new System.EventHandler(this.chkTaskHouse_CheckedChanged);
            // 
            // lblReceiverEmail
            // 
            this.lblReceiverEmail.AutoSize = true;
            this.lblReceiverEmail.Location = new System.Drawing.Point(12, 123);
            this.lblReceiverEmail.Name = "lblReceiverEmail";
            this.lblReceiverEmail.Size = new System.Drawing.Size(65, 12);
            this.lblReceiverEmail.TabIndex = 28;
            this.lblReceiverEmail.Text = "接收邮箱：";
            // 
            // txtReceiverEmail
            // 
            this.txtReceiverEmail.Location = new System.Drawing.Point(18, 138);
            this.txtReceiverEmail.Name = "txtReceiverEmail";
            this.txtReceiverEmail.Size = new System.Drawing.Size(172, 21);
            this.txtReceiverEmail.TabIndex = 27;
            // 
            // chkSendLog
            // 
            this.chkSendLog.AutoSize = true;
            this.chkSendLog.Location = new System.Drawing.Point(11, 104);
            this.chkSendLog.Name = "chkSendLog";
            this.chkSendLog.Size = new System.Drawing.Size(72, 16);
            this.chkSendLog.TabIndex = 3;
            this.chkSendLog.Text = "发送日志";
            this.chkSendLog.UseVisualStyleBackColor = true;
            this.chkSendLog.CheckedChanged += new System.EventHandler(this.chkSendLog_CheckedChanged);
            // 
            // chkTaskSlave
            // 
            this.chkTaskSlave.AutoSize = true;
            this.chkTaskSlave.Location = new System.Drawing.Point(11, 39);
            this.chkTaskSlave.Name = "chkTaskSlave";
            this.chkTaskSlave.Size = new System.Drawing.Size(72, 16);
            this.chkTaskSlave.TabIndex = 2;
            this.chkTaskSlave.Text = "朋友买卖";
            this.chkTaskSlave.UseVisualStyleBackColor = true;
            this.chkTaskSlave.CheckedChanged += new System.EventHandler(this.chkTaskSlave_CheckedChanged);
            // 
            // chkTaskParking
            // 
            this.chkTaskParking.AutoSize = true;
            this.chkTaskParking.Location = new System.Drawing.Point(11, 19);
            this.chkTaskParking.Name = "chkTaskParking";
            this.chkTaskParking.Size = new System.Drawing.Size(60, 16);
            this.chkTaskParking.TabIndex = 0;
            this.chkTaskParking.Text = "争车位";
            this.chkTaskParking.UseVisualStyleBackColor = true;
            this.chkTaskParking.CheckedChanged += new System.EventHandler(this.chkTaskParking_CheckedChanged);
            // 
            // chkTaskBiting
            // 
            this.chkTaskBiting.AutoSize = true;
            this.chkTaskBiting.Location = new System.Drawing.Point(82, 19);
            this.chkTaskBiting.Name = "chkTaskBiting";
            this.chkTaskBiting.Size = new System.Drawing.Size(48, 16);
            this.chkTaskBiting.TabIndex = 1;
            this.chkTaskBiting.Text = "咬人";
            this.chkTaskBiting.UseVisualStyleBackColor = true;
            this.chkTaskBiting.CheckedChanged += new System.EventHandler(this.chkTaskBiting_CheckedChanged);
            // 
            // lblLoopTime
            // 
            this.lblLoopTime.AutoSize = true;
            this.lblLoopTime.Location = new System.Drawing.Point(8, 24);
            this.lblLoopTime.Name = "lblLoopTime";
            this.lblLoopTime.Size = new System.Drawing.Size(65, 12);
            this.lblLoopTime.TabIndex = 0;
            this.lblLoopTime.Text = "循环时间：";
            // 
            // grpLoopTime
            // 
            this.grpLoopTime.Controls.Add(this.grpRunMode);
            this.grpLoopTime.Controls.Add(this.grpValidation);
            this.grpLoopTime.Controls.Add(this.grpLoop);
            this.grpLoopTime.Controls.Add(this.groupTiming);
            this.grpLoopTime.Location = new System.Drawing.Point(461, 5);
            this.grpLoopTime.Name = "grpLoopTime";
            this.grpLoopTime.Size = new System.Drawing.Size(370, 268);
            this.grpLoopTime.TabIndex = 24;
            this.grpLoopTime.TabStop = false;
            this.grpLoopTime.Text = "运行设定";
            // 
            // grpRunMode
            // 
            this.grpRunMode.Controls.Add(this.rdbMultiLoop);
            this.grpRunMode.Controls.Add(this.rdbTiming);
            this.grpRunMode.Controls.Add(this.rdbSingleLoop);
            this.grpRunMode.Location = new System.Drawing.Point(10, 17);
            this.grpRunMode.Name = "grpRunMode";
            this.grpRunMode.Size = new System.Drawing.Size(188, 55);
            this.grpRunMode.TabIndex = 53;
            this.grpRunMode.TabStop = false;
            this.grpRunMode.Text = "执行方式";
            // 
            // rdbMultiLoop
            // 
            this.rdbMultiLoop.AutoSize = true;
            this.rdbMultiLoop.Location = new System.Drawing.Point(19, 34);
            this.rdbMultiLoop.Name = "rdbMultiLoop";
            this.rdbMultiLoop.Size = new System.Drawing.Size(83, 16);
            this.rdbMultiLoop.TabIndex = 4;
            this.rdbMultiLoop.TabStop = true;
            this.rdbMultiLoop.Text = "多账号循环";
            this.rdbMultiLoop.UseVisualStyleBackColor = true;
            // 
            // rdbTiming
            // 
            this.rdbTiming.AutoSize = true;
            this.rdbTiming.Location = new System.Drawing.Point(121, 15);
            this.rdbTiming.Name = "rdbTiming";
            this.rdbTiming.Size = new System.Drawing.Size(47, 16);
            this.rdbTiming.TabIndex = 5;
            this.rdbTiming.TabStop = true;
            this.rdbTiming.Text = "定时";
            this.rdbTiming.UseVisualStyleBackColor = true;
            this.rdbTiming.CheckedChanged += new System.EventHandler(this.rdbTiming_CheckedChanged);
            // 
            // rdbSingleLoop
            // 
            this.rdbSingleLoop.AutoSize = true;
            this.rdbSingleLoop.Location = new System.Drawing.Point(19, 15);
            this.rdbSingleLoop.Name = "rdbSingleLoop";
            this.rdbSingleLoop.Size = new System.Drawing.Size(83, 16);
            this.rdbSingleLoop.TabIndex = 6;
            this.rdbSingleLoop.TabStop = true;
            this.rdbSingleLoop.Text = "单账号循环";
            this.rdbSingleLoop.UseVisualStyleBackColor = true;
            // 
            // grpValidation
            // 
            this.grpValidation.Controls.Add(this.panel2);
            this.grpValidation.Location = new System.Drawing.Point(10, 207);
            this.grpValidation.Name = "grpValidation";
            this.grpValidation.Size = new System.Drawing.Size(188, 49);
            this.grpValidation.TabIndex = 53;
            this.grpValidation.TabStop = false;
            this.grpValidation.Text = "图片验证码";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbPopupValidationWindow);
            this.panel2.Controls.Add(this.rdbSkip);
            this.panel2.Location = new System.Drawing.Point(12, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(165, 24);
            this.panel2.TabIndex = 13;
            // 
            // rdbPopupValidationWindow
            // 
            this.rdbPopupValidationWindow.AutoSize = true;
            this.rdbPopupValidationWindow.Location = new System.Drawing.Point(61, 3);
            this.rdbPopupValidationWindow.Name = "rdbPopupValidationWindow";
            this.rdbPopupValidationWindow.Size = new System.Drawing.Size(95, 16);
            this.rdbPopupValidationWindow.TabIndex = 5;
            this.rdbPopupValidationWindow.TabStop = true;
            this.rdbPopupValidationWindow.Text = "弹出输入窗口";
            this.rdbPopupValidationWindow.UseVisualStyleBackColor = true;
            // 
            // rdbSkip
            // 
            this.rdbSkip.AutoSize = true;
            this.rdbSkip.Location = new System.Drawing.Point(3, 3);
            this.rdbSkip.Name = "rdbSkip";
            this.rdbSkip.Size = new System.Drawing.Size(47, 16);
            this.rdbSkip.TabIndex = 4;
            this.rdbSkip.TabStop = true;
            this.rdbSkip.Text = "跳过";
            this.rdbSkip.UseVisualStyleBackColor = true;
            // 
            // grpLoop
            // 
            this.grpLoop.Controls.Add(this.lblEnd);
            this.grpLoop.Controls.Add(this.timeForbiddenEnd);
            this.grpLoop.Controls.Add(this.chkForbidden);
            this.grpLoop.Controls.Add(this.timeForbiddenStart);
            this.grpLoop.Controls.Add(this.lblStart);
            this.grpLoop.Controls.Add(this.lblMinutes);
            this.grpLoop.Controls.Add(this.txtRoundTime);
            this.grpLoop.Controls.Add(this.lblLoopTime);
            this.grpLoop.Location = new System.Drawing.Point(10, 76);
            this.grpLoop.Name = "grpLoop";
            this.grpLoop.Size = new System.Drawing.Size(188, 126);
            this.grpLoop.TabIndex = 12;
            this.grpLoop.TabStop = false;
            this.grpLoop.Text = "循环";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(11, 104);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(65, 12);
            this.lblEnd.TabIndex = 23;
            this.lblEnd.Text = "结束时间：";
            // 
            // timeForbiddenEnd
            // 
            this.timeForbiddenEnd.Hour = -1;
            this.timeForbiddenEnd.Location = new System.Drawing.Point(85, 100);
            this.timeForbiddenEnd.Minute = -1;
            this.timeForbiddenEnd.Name = "timeForbiddenEnd";
            this.timeForbiddenEnd.Size = new System.Drawing.Size(86, 20);
            this.timeForbiddenEnd.TabIndex = 22;
            // 
            // chkForbidden
            // 
            this.chkForbidden.AutoSize = true;
            this.chkForbidden.Location = new System.Drawing.Point(13, 47);
            this.chkForbidden.Name = "chkForbidden";
            this.chkForbidden.Size = new System.Drawing.Size(144, 16);
            this.chkForbidden.TabIndex = 21;
            this.chkForbidden.Text = "在下列时间内禁止执行";
            this.chkForbidden.UseVisualStyleBackColor = true;
            this.chkForbidden.CheckedChanged += new System.EventHandler(this.chkForbidden_CheckedChanged);
            // 
            // timeForbiddenStart
            // 
            this.timeForbiddenStart.Hour = -1;
            this.timeForbiddenStart.Location = new System.Drawing.Point(85, 69);
            this.timeForbiddenStart.Minute = -1;
            this.timeForbiddenStart.Name = "timeForbiddenStart";
            this.timeForbiddenStart.Size = new System.Drawing.Size(86, 20);
            this.timeForbiddenStart.TabIndex = 10;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(11, 73);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(65, 12);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "开始时间：";
            // 
            // groupTiming
            // 
            this.groupTiming.Controls.Add(this.label11);
            this.groupTiming.Controls.Add(this.timeSelector10);
            this.groupTiming.Controls.Add(this.label10);
            this.groupTiming.Controls.Add(this.label9);
            this.groupTiming.Controls.Add(this.label8);
            this.groupTiming.Controls.Add(this.label7);
            this.groupTiming.Controls.Add(this.label6);
            this.groupTiming.Controls.Add(this.label5);
            this.groupTiming.Controls.Add(this.label4);
            this.groupTiming.Controls.Add(this.label3);
            this.groupTiming.Controls.Add(this.timeSelector9);
            this.groupTiming.Controls.Add(this.timeSelector8);
            this.groupTiming.Controls.Add(this.timeSelector7);
            this.groupTiming.Controls.Add(this.timeSelector6);
            this.groupTiming.Controls.Add(this.timeSelector5);
            this.groupTiming.Controls.Add(this.timeSelector4);
            this.groupTiming.Controls.Add(this.timeSelector3);
            this.groupTiming.Controls.Add(this.timeSelector2);
            this.groupTiming.Controls.Add(this.timeSelector1);
            this.groupTiming.Controls.Add(this.label2);
            this.groupTiming.Location = new System.Drawing.Point(219, 11);
            this.groupTiming.Name = "groupTiming";
            this.groupTiming.Size = new System.Drawing.Size(143, 251);
            this.groupTiming.TabIndex = 9;
            this.groupTiming.TabStop = false;
            this.groupTiming.Text = "定时";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 225);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "10";
            // 
            // timeSelector10
            // 
            this.timeSelector10.Hour = -1;
            this.timeSelector10.Location = new System.Drawing.Point(40, 222);
            this.timeSelector10.Minute = -1;
            this.timeSelector10.Name = "timeSelector10";
            this.timeSelector10.Size = new System.Drawing.Size(86, 20);
            this.timeSelector10.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 202);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "9";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "8";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "7";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "6";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "2";
            // 
            // timeSelector9
            // 
            this.timeSelector9.Hour = -1;
            this.timeSelector9.Location = new System.Drawing.Point(40, 199);
            this.timeSelector9.Minute = -1;
            this.timeSelector9.Name = "timeSelector9";
            this.timeSelector9.Size = new System.Drawing.Size(86, 20);
            this.timeSelector9.TabIndex = 16;
            // 
            // timeSelector8
            // 
            this.timeSelector8.Hour = -1;
            this.timeSelector8.Location = new System.Drawing.Point(40, 176);
            this.timeSelector8.Minute = -1;
            this.timeSelector8.Name = "timeSelector8";
            this.timeSelector8.Size = new System.Drawing.Size(86, 20);
            this.timeSelector8.TabIndex = 15;
            // 
            // timeSelector7
            // 
            this.timeSelector7.Hour = -1;
            this.timeSelector7.Location = new System.Drawing.Point(40, 153);
            this.timeSelector7.Minute = -1;
            this.timeSelector7.Name = "timeSelector7";
            this.timeSelector7.Size = new System.Drawing.Size(86, 20);
            this.timeSelector7.TabIndex = 14;
            // 
            // timeSelector6
            // 
            this.timeSelector6.Hour = -1;
            this.timeSelector6.Location = new System.Drawing.Point(40, 130);
            this.timeSelector6.Minute = -1;
            this.timeSelector6.Name = "timeSelector6";
            this.timeSelector6.Size = new System.Drawing.Size(86, 20);
            this.timeSelector6.TabIndex = 13;
            // 
            // timeSelector5
            // 
            this.timeSelector5.Hour = -1;
            this.timeSelector5.Location = new System.Drawing.Point(40, 107);
            this.timeSelector5.Minute = -1;
            this.timeSelector5.Name = "timeSelector5";
            this.timeSelector5.Size = new System.Drawing.Size(86, 20);
            this.timeSelector5.TabIndex = 12;
            // 
            // timeSelector4
            // 
            this.timeSelector4.Hour = -1;
            this.timeSelector4.Location = new System.Drawing.Point(40, 84);
            this.timeSelector4.Minute = -1;
            this.timeSelector4.Name = "timeSelector4";
            this.timeSelector4.Size = new System.Drawing.Size(86, 20);
            this.timeSelector4.TabIndex = 11;
            // 
            // timeSelector3
            // 
            this.timeSelector3.Hour = -1;
            this.timeSelector3.Location = new System.Drawing.Point(40, 61);
            this.timeSelector3.Minute = -1;
            this.timeSelector3.Name = "timeSelector3";
            this.timeSelector3.Size = new System.Drawing.Size(86, 20);
            this.timeSelector3.TabIndex = 10;
            // 
            // timeSelector2
            // 
            this.timeSelector2.Hour = -1;
            this.timeSelector2.Location = new System.Drawing.Point(40, 38);
            this.timeSelector2.Minute = -1;
            this.timeSelector2.Name = "timeSelector2";
            this.timeSelector2.Size = new System.Drawing.Size(86, 20);
            this.timeSelector2.TabIndex = 9;
            // 
            // timeSelector1
            // 
            this.timeSelector1.Hour = -1;
            this.timeSelector1.Location = new System.Drawing.Point(40, 15);
            this.timeSelector1.Minute = -1;
            this.timeSelector1.Name = "timeSelector1";
            this.timeSelector1.Size = new System.Drawing.Size(86, 20);
            this.timeSelector1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "1";
            // 
            // chkBreedAnimal
            // 
            this.chkBreedAnimal.AutoSize = true;
            this.chkBreedAnimal.Location = new System.Drawing.Point(12, 160);
            this.chkBreedAnimal.Name = "chkBreedAnimal";
            this.chkBreedAnimal.Size = new System.Drawing.Size(48, 16);
            this.chkBreedAnimal.TabIndex = 35;
            this.chkBreedAnimal.Text = "配种";
            this.chkBreedAnimal.UseVisualStyleBackColor = true;
            // 
            // chkHarvestProduct
            // 
            this.chkHarvestProduct.AutoSize = true;
            this.chkHarvestProduct.Location = new System.Drawing.Point(12, 120);
            this.chkHarvestProduct.Name = "chkHarvestProduct";
            this.chkHarvestProduct.Size = new System.Drawing.Size(96, 16);
            this.chkHarvestProduct.TabIndex = 34;
            this.chkHarvestProduct.Text = "收获农副产品";
            this.chkHarvestProduct.UseVisualStyleBackColor = true;
            // 
            // chkHelpMakeProduct
            // 
            this.chkHelpMakeProduct.AutoSize = true;
            this.chkHelpMakeProduct.Location = new System.Drawing.Point(190, 95);
            this.chkHelpMakeProduct.Name = "chkHelpMakeProduct";
            this.chkHelpMakeProduct.Size = new System.Drawing.Size(72, 16);
            this.chkHelpMakeProduct.TabIndex = 33;
            this.chkHelpMakeProduct.Text = "帮忙生产";
            this.chkHelpMakeProduct.UseVisualStyleBackColor = true;
            // 
            // chkMakeProduct
            // 
            this.chkMakeProduct.AutoSize = true;
            this.chkMakeProduct.Location = new System.Drawing.Point(12, 100);
            this.chkMakeProduct.Name = "chkMakeProduct";
            this.chkMakeProduct.Size = new System.Drawing.Size(48, 16);
            this.chkMakeProduct.TabIndex = 32;
            this.chkMakeProduct.Text = "生产";
            this.chkMakeProduct.UseVisualStyleBackColor = true;
            // 
            // chkStealProduct
            // 
            this.chkStealProduct.AutoSize = true;
            this.chkStealProduct.Location = new System.Drawing.Point(190, 113);
            this.chkStealProduct.Name = "chkStealProduct";
            this.chkStealProduct.Size = new System.Drawing.Size(84, 16);
            this.chkStealProduct.TabIndex = 31;
            this.chkStealProduct.Text = "偷农副产品";
            this.chkStealProduct.UseVisualStyleBackColor = true;
            // 
            // grpPark
            // 
            this.grpPark.Controls.Add(this.lblWarningPostCars);
            this.grpPark.Controls.Add(this.lblWarningParkCars);
            this.grpPark.Controls.Add(this.panelStartCar);
            this.grpPark.Controls.Add(this.panelOriginateMatch);
            this.grpPark.Controls.Add(this.chkJoinMatch);
            this.grpPark.Controls.Add(this.chkStartCar);
            this.grpPark.Controls.Add(this.chkOriginateMatch);
            this.grpPark.Controls.Add(this.chkCheerUp);
            this.grpPark.Controls.Add(this.chkParkMyCars);
            this.grpPark.Controls.Add(this.chkPostOthersCars);
            this.grpPark.Location = new System.Drawing.Point(7, 3);
            this.grpPark.Name = "grpPark";
            this.grpPark.Size = new System.Drawing.Size(586, 243);
            this.grpPark.TabIndex = 46;
            this.grpPark.TabStop = false;
            this.grpPark.Text = "争车位";
            // 
            // lblWarningPostCars
            // 
            this.lblWarningPostCars.AutoSize = true;
            this.lblWarningPostCars.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningPostCars.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPostCars.Location = new System.Drawing.Point(55, 41);
            this.lblWarningPostCars.Name = "lblWarningPostCars";
            this.lblWarningPostCars.Size = new System.Drawing.Size(11, 12);
            this.lblWarningPostCars.TabIndex = 41;
            this.lblWarningPostCars.Text = "*";
            // 
            // lblWarningParkCars
            // 
            this.lblWarningParkCars.AutoSize = true;
            this.lblWarningParkCars.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningParkCars.ForeColor = System.Drawing.Color.Red;
            this.lblWarningParkCars.Location = new System.Drawing.Point(66, 21);
            this.lblWarningParkCars.Name = "lblWarningParkCars";
            this.lblWarningParkCars.Size = new System.Drawing.Size(11, 12);
            this.lblWarningParkCars.TabIndex = 40;
            this.lblWarningParkCars.Text = "*";
            // 
            // panelStartCar
            // 
            this.panelStartCar.Controls.Add(this.lblStartCarTime);
            this.panelStartCar.Controls.Add(this.timeStartCarTime);
            this.panelStartCar.Location = new System.Drawing.Point(98, 124);
            this.panelStartCar.Name = "panelStartCar";
            this.panelStartCar.Size = new System.Drawing.Size(200, 28);
            this.panelStartCar.TabIndex = 39;
            // 
            // lblStartCarTime
            // 
            this.lblStartCarTime.AutoSize = true;
            this.lblStartCarTime.Location = new System.Drawing.Point(8, 2);
            this.lblStartCarTime.Name = "lblStartCarTime";
            this.lblStartCarTime.Size = new System.Drawing.Size(77, 12);
            this.lblStartCarTime.TabIndex = 32;
            this.lblStartCarTime.Text = "启动时间点：";
            // 
            // timeStartCarTime
            // 
            this.timeStartCarTime.Hour = -1;
            this.timeStartCarTime.Location = new System.Drawing.Point(86, 0);
            this.timeStartCarTime.Minute = -1;
            this.timeStartCarTime.Name = "timeStartCarTime";
            this.timeStartCarTime.Size = new System.Drawing.Size(86, 20);
            this.timeStartCarTime.TabIndex = 31;
            // 
            // panelOriginateMatch
            // 
            this.panelOriginateMatch.Controls.Add(this.lblMatch);
            this.panelOriginateMatch.Controls.Add(this.cmbOriginateTeamNum);
            this.panelOriginateMatch.Controls.Add(this.cmbOriginateMatchId);
            this.panelOriginateMatch.Controls.Add(this.label1);
            this.panelOriginateMatch.Location = new System.Drawing.Point(98, 56);
            this.panelOriginateMatch.Name = "panelOriginateMatch";
            this.panelOriginateMatch.Size = new System.Drawing.Size(218, 48);
            this.panelOriginateMatch.TabIndex = 38;
            // 
            // lblMatch
            // 
            this.lblMatch.AutoSize = true;
            this.lblMatch.Location = new System.Drawing.Point(8, 5);
            this.lblMatch.Name = "lblMatch";
            this.lblMatch.Size = new System.Drawing.Size(65, 12);
            this.lblMatch.TabIndex = 35;
            this.lblMatch.Text = "比赛线路：";
            // 
            // cmbOriginateTeamNum
            // 
            this.cmbOriginateTeamNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOriginateTeamNum.FormattingEnabled = true;
            this.cmbOriginateTeamNum.Items.AddRange(new object[] {
            "2",
            "3"});
            this.cmbOriginateTeamNum.Location = new System.Drawing.Point(91, 25);
            this.cmbOriginateTeamNum.Name = "cmbOriginateTeamNum";
            this.cmbOriginateTeamNum.Size = new System.Drawing.Size(48, 20);
            this.cmbOriginateTeamNum.TabIndex = 37;
            // 
            // cmbOriginateMatchId
            // 
            this.cmbOriginateMatchId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOriginateMatchId.FormattingEnabled = true;
            this.cmbOriginateMatchId.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbOriginateMatchId.Location = new System.Drawing.Point(74, 2);
            this.cmbOriginateMatchId.Name = "cmbOriginateMatchId";
            this.cmbOriginateMatchId.Size = new System.Drawing.Size(138, 20);
            this.cmbOriginateMatchId.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "参赛车队数：";
            // 
            // chkJoinMatch
            // 
            this.chkJoinMatch.AutoSize = true;
            this.chkJoinMatch.Location = new System.Drawing.Point(85, 20);
            this.chkJoinMatch.Name = "chkJoinMatch";
            this.chkJoinMatch.Size = new System.Drawing.Size(72, 16);
            this.chkJoinMatch.TabIndex = 30;
            this.chkJoinMatch.Text = "参加比赛";
            this.chkJoinMatch.UseVisualStyleBackColor = true;
            // 
            // chkStartCar
            // 
            this.chkStartCar.AutoSize = true;
            this.chkStartCar.Location = new System.Drawing.Point(85, 107);
            this.chkStartCar.Name = "chkStartCar";
            this.chkStartCar.Size = new System.Drawing.Size(72, 16);
            this.chkStartCar.TabIndex = 29;
            this.chkStartCar.Text = "启动赛车";
            this.chkStartCar.UseVisualStyleBackColor = true;
            this.chkStartCar.CheckedChanged += new System.EventHandler(this.chkStartCar_CheckedChanged);
            // 
            // chkOriginateMatch
            // 
            this.chkOriginateMatch.AutoSize = true;
            this.chkOriginateMatch.Location = new System.Drawing.Point(85, 40);
            this.chkOriginateMatch.Name = "chkOriginateMatch";
            this.chkOriginateMatch.Size = new System.Drawing.Size(72, 16);
            this.chkOriginateMatch.TabIndex = 28;
            this.chkOriginateMatch.Text = "发起比赛";
            this.chkOriginateMatch.UseVisualStyleBackColor = true;
            this.chkOriginateMatch.CheckedChanged += new System.EventHandler(this.chkOriginateMatch_CheckedChanged);
            // 
            // chkCheerUp
            // 
            this.chkCheerUp.AutoSize = true;
            this.chkCheerUp.Location = new System.Drawing.Point(12, 60);
            this.chkCheerUp.Name = "chkCheerUp";
            this.chkCheerUp.Size = new System.Drawing.Size(48, 16);
            this.chkCheerUp.TabIndex = 27;
            this.chkCheerUp.Text = "加油";
            this.chkCheerUp.UseVisualStyleBackColor = true;
            // 
            // chkParkMyCars
            // 
            this.chkParkMyCars.AutoSize = true;
            this.chkParkMyCars.Location = new System.Drawing.Point(12, 20);
            this.chkParkMyCars.Name = "chkParkMyCars";
            this.chkParkMyCars.Size = new System.Drawing.Size(60, 16);
            this.chkParkMyCars.TabIndex = 22;
            this.chkParkMyCars.Text = "占车位";
            this.chkParkMyCars.UseVisualStyleBackColor = true;
            // 
            // chkPostOthersCars
            // 
            this.chkPostOthersCars.AutoSize = true;
            this.chkPostOthersCars.Location = new System.Drawing.Point(12, 40);
            this.chkPostOthersCars.Name = "chkPostOthersCars";
            this.chkPostOthersCars.Size = new System.Drawing.Size(48, 16);
            this.chkPostOthersCars.TabIndex = 20;
            this.chkPostOthersCars.Text = "贴条";
            this.chkPostOthersCars.UseVisualStyleBackColor = true;
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(647, 299);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(79, 42);
            this.btnReload.TabIndex = 48;
            this.btnReload.Text = "重新载入";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // cmbBuySeedCount
            // 
            this.cmbBuySeedCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuySeedCount.FormattingEnabled = true;
            this.cmbBuySeedCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmbBuySeedCount.Location = new System.Drawing.Point(339, 50);
            this.cmbBuySeedCount.Name = "cmbBuySeedCount";
            this.cmbBuySeedCount.Size = new System.Drawing.Size(52, 20);
            this.cmbBuySeedCount.TabIndex = 39;
            // 
            // chkBuySeed
            // 
            this.chkBuySeed.AutoSize = true;
            this.chkBuySeed.Location = new System.Drawing.Point(230, 34);
            this.chkBuySeed.Name = "chkBuySeed";
            this.chkBuySeed.Size = new System.Drawing.Size(72, 16);
            this.chkBuySeed.TabIndex = 38;
            this.chkBuySeed.Text = "购买种子";
            this.chkBuySeed.UseVisualStyleBackColor = true;
            this.chkBuySeed.CheckedChanged += new System.EventHandler(this.chkBuySeed_CheckedChanged);
            // 
            // chkStealFruit
            // 
            this.chkStealFruit.AutoSize = true;
            this.chkStealFruit.Location = new System.Drawing.Point(6, 157);
            this.chkStealFruit.Name = "chkStealFruit";
            this.chkStealFruit.Size = new System.Drawing.Size(60, 16);
            this.chkStealFruit.TabIndex = 37;
            this.chkStealFruit.Text = "偷果实";
            this.chkStealFruit.UseVisualStyleBackColor = true;
            this.chkStealFruit.CheckedChanged += new System.EventHandler(this.chkStealFruit_CheckedChanged);
            // 
            // chkFarmSelf
            // 
            this.chkFarmSelf.AutoSize = true;
            this.chkFarmSelf.Location = new System.Drawing.Point(8, 16);
            this.chkFarmSelf.Name = "chkFarmSelf";
            this.chkFarmSelf.Size = new System.Drawing.Size(72, 16);
            this.chkFarmSelf.TabIndex = 36;
            this.chkFarmSelf.Text = "自家耕作";
            this.chkFarmSelf.UseVisualStyleBackColor = true;
            this.chkFarmSelf.CheckedChanged += new System.EventHandler(this.chkFarmSelf_CheckedChanged);
            // 
            // chkHarvestFruit
            // 
            this.chkHarvestFruit.AutoSize = true;
            this.chkHarvestFruit.Location = new System.Drawing.Point(230, 16);
            this.chkHarvestFruit.Name = "chkHarvestFruit";
            this.chkHarvestFruit.Size = new System.Drawing.Size(72, 16);
            this.chkHarvestFruit.TabIndex = 40;
            this.chkHarvestFruit.Text = "收获果实";
            this.chkHarvestFruit.UseVisualStyleBackColor = true;
            // 
            // grpGarden
            // 
            this.grpGarden.Controls.Add(this.lblWarningPresentFruit);
            this.grpGarden.Controls.Add(this.lblWarningFarmShared);
            this.grpGarden.Controls.Add(this.lblWarningStealFruit);
            this.grpGarden.Controls.Add(this.panelPresentFruit);
            this.grpGarden.Controls.Add(this.panelSellFruit);
            this.grpGarden.Controls.Add(this.chkStealUnknowFruit);
            this.grpGarden.Controls.Add(this.btnSetFruitList);
            this.grpGarden.Controls.Add(this.chkSowMySeedsFirst);
            this.grpGarden.Controls.Add(this.chkSellFruit);
            this.grpGarden.Controls.Add(this.chkPresentFruit);
            this.grpGarden.Controls.Add(this.panelFarmSelf);
            this.grpGarden.Controls.Add(this.panelFarmShared);
            this.grpGarden.Controls.Add(this.chkHelpOthers);
            this.grpGarden.Controls.Add(this.lblSeedsCount);
            this.grpGarden.Controls.Add(this.chkFarmShared);
            this.grpGarden.Controls.Add(this.cmbBuySeedCount);
            this.grpGarden.Controls.Add(this.chkHarvestFruit);
            this.grpGarden.Controls.Add(this.chkBuySeed);
            this.grpGarden.Controls.Add(this.chkFarmSelf);
            this.grpGarden.Controls.Add(this.chkStealFruit);
            this.grpGarden.Location = new System.Drawing.Point(7, 3);
            this.grpGarden.Name = "grpGarden";
            this.grpGarden.Size = new System.Drawing.Size(605, 250);
            this.grpGarden.TabIndex = 50;
            this.grpGarden.TabStop = false;
            this.grpGarden.Text = "花园";
            // 
            // lblWarningPresentFruit
            // 
            this.lblWarningPresentFruit.AutoSize = true;
            this.lblWarningPresentFruit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningPresentFruit.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPresentFruit.Location = new System.Drawing.Point(299, 137);
            this.lblWarningPresentFruit.Name = "lblWarningPresentFruit";
            this.lblWarningPresentFruit.Size = new System.Drawing.Size(11, 12);
            this.lblWarningPresentFruit.TabIndex = 63;
            this.lblWarningPresentFruit.Text = "*";
            // 
            // lblWarningFarmShared
            // 
            this.lblWarningFarmShared.AutoSize = true;
            this.lblWarningFarmShared.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningFarmShared.ForeColor = System.Drawing.Color.Red;
            this.lblWarningFarmShared.Location = new System.Drawing.Point(99, 73);
            this.lblWarningFarmShared.Name = "lblWarningFarmShared";
            this.lblWarningFarmShared.Size = new System.Drawing.Size(11, 12);
            this.lblWarningFarmShared.TabIndex = 62;
            this.lblWarningFarmShared.Text = "*";
            // 
            // lblWarningStealFruit
            // 
            this.lblWarningStealFruit.AutoSize = true;
            this.lblWarningStealFruit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningStealFruit.ForeColor = System.Drawing.Color.Red;
            this.lblWarningStealFruit.Location = new System.Drawing.Point(63, 158);
            this.lblWarningStealFruit.Name = "lblWarningStealFruit";
            this.lblWarningStealFruit.Size = new System.Drawing.Size(11, 12);
            this.lblWarningStealFruit.TabIndex = 61;
            this.lblWarningStealFruit.Text = "*";
            // 
            // panelPresentFruit
            // 
            this.panelPresentFruit.Controls.Add(this.lblPresentFruitCheckNum);
            this.panelPresentFruit.Controls.Add(this.lblPresentFruitCheckValue);
            this.panelPresentFruit.Controls.Add(this.txtPresentFruitNum);
            this.panelPresentFruit.Controls.Add(this.txtPresentFruitValue);
            this.panelPresentFruit.Controls.Add(this.chkPresentFruitCheckValue);
            this.panelPresentFruit.Controls.Add(this.chkPresentFruitCheckNum);
            this.panelPresentFruit.Controls.Add(this.rdbPresentFruitByPrice);
            this.panelPresentFruit.Controls.Add(this.cmbPresentFruitId);
            this.panelPresentFruit.Controls.Add(this.rdbPresentFruitCustom);
            this.panelPresentFruit.Location = new System.Drawing.Point(238, 154);
            this.panelPresentFruit.Name = "panelPresentFruit";
            this.panelPresentFruit.Size = new System.Drawing.Size(216, 90);
            this.panelPresentFruit.TabIndex = 51;
            // 
            // lblPresentFruitCheckNum
            // 
            this.lblPresentFruitCheckNum.AutoSize = true;
            this.lblPresentFruitCheckNum.Location = new System.Drawing.Point(172, 68);
            this.lblPresentFruitCheckNum.Name = "lblPresentFruitCheckNum";
            this.lblPresentFruitCheckNum.Size = new System.Drawing.Size(17, 12);
            this.lblPresentFruitCheckNum.TabIndex = 80;
            this.lblPresentFruitCheckNum.Text = "个";
            // 
            // lblPresentFruitCheckValue
            // 
            this.lblPresentFruitCheckValue.AutoSize = true;
            this.lblPresentFruitCheckValue.Location = new System.Drawing.Point(172, 23);
            this.lblPresentFruitCheckValue.Name = "lblPresentFruitCheckValue";
            this.lblPresentFruitCheckValue.Size = new System.Drawing.Size(17, 12);
            this.lblPresentFruitCheckValue.TabIndex = 79;
            this.lblPresentFruitCheckValue.Text = "万";
            // 
            // txtPresentFruitNum
            // 
            this.txtPresentFruitNum.Location = new System.Drawing.Point(109, 66);
            this.txtPresentFruitNum.Name = "txtPresentFruitNum";
            this.txtPresentFruitNum.Size = new System.Drawing.Size(62, 21);
            this.txtPresentFruitNum.TabIndex = 74;
            // 
            // txtPresentFruitValue
            // 
            this.txtPresentFruitValue.Location = new System.Drawing.Point(109, 20);
            this.txtPresentFruitValue.Name = "txtPresentFruitValue";
            this.txtPresentFruitValue.Size = new System.Drawing.Size(62, 21);
            this.txtPresentFruitValue.TabIndex = 78;
            // 
            // chkPresentFruitCheckValue
            // 
            this.chkPresentFruitCheckValue.AutoSize = true;
            this.chkPresentFruitCheckValue.Location = new System.Drawing.Point(16, 22);
            this.chkPresentFruitCheckValue.Name = "chkPresentFruitCheckValue";
            this.chkPresentFruitCheckValue.Size = new System.Drawing.Size(96, 16);
            this.chkPresentFruitCheckValue.TabIndex = 77;
            this.chkPresentFruitCheckValue.Text = "最低赠送价值";
            this.chkPresentFruitCheckValue.UseVisualStyleBackColor = true;
            // 
            // chkPresentFruitCheckNum
            // 
            this.chkPresentFruitCheckNum.AutoSize = true;
            this.chkPresentFruitCheckNum.Location = new System.Drawing.Point(16, 67);
            this.chkPresentFruitCheckNum.Name = "chkPresentFruitCheckNum";
            this.chkPresentFruitCheckNum.Size = new System.Drawing.Size(96, 16);
            this.chkPresentFruitCheckNum.TabIndex = 75;
            this.chkPresentFruitCheckNum.Text = "最低赠送数量";
            this.chkPresentFruitCheckNum.UseVisualStyleBackColor = true;
            // 
            // rdbPresentFruitByPrice
            // 
            this.rdbPresentFruitByPrice.AutoSize = true;
            this.rdbPresentFruitByPrice.Checked = true;
            this.rdbPresentFruitByPrice.Location = new System.Drawing.Point(3, 3);
            this.rdbPresentFruitByPrice.Name = "rdbPresentFruitByPrice";
            this.rdbPresentFruitByPrice.Size = new System.Drawing.Size(143, 16);
            this.rdbPresentFruitByPrice.TabIndex = 0;
            this.rdbPresentFruitByPrice.TabStop = true;
            this.rdbPresentFruitByPrice.Text = "优先赠送总价值最高的";
            this.rdbPresentFruitByPrice.UseVisualStyleBackColor = true;
            this.rdbPresentFruitByPrice.CheckedChanged += new System.EventHandler(this.rdbPresentFruitByPrice_CheckedChanged);
            // 
            // cmbPresentFruitId
            // 
            this.cmbPresentFruitId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresentFruitId.FormattingEnabled = true;
            this.cmbPresentFruitId.Location = new System.Drawing.Point(61, 44);
            this.cmbPresentFruitId.Name = "cmbPresentFruitId";
            this.cmbPresentFruitId.Size = new System.Drawing.Size(131, 20);
            this.cmbPresentFruitId.TabIndex = 45;
            // 
            // rdbPresentFruitCustom
            // 
            this.rdbPresentFruitCustom.AutoSize = true;
            this.rdbPresentFruitCustom.Location = new System.Drawing.Point(3, 45);
            this.rdbPresentFruitCustom.Name = "rdbPresentFruitCustom";
            this.rdbPresentFruitCustom.Size = new System.Drawing.Size(59, 16);
            this.rdbPresentFruitCustom.TabIndex = 1;
            this.rdbPresentFruitCustom.Text = "自定义";
            this.rdbPresentFruitCustom.UseVisualStyleBackColor = true;
            // 
            // panelSellFruit
            // 
            this.panelSellFruit.Controls.Add(this.panelSellForbiddenFruit);
            this.panelSellFruit.Controls.Add(this.lblLowCash);
            this.panelSellFruit.Controls.Add(this.txtLowCashLimit);
            this.panelSellFruit.Controls.Add(this.chkLowCash);
            this.panelSellFruit.Controls.Add(this.rdbSellAllFruit);
            this.panelSellFruit.Controls.Add(this.rdbFobiddenFruit);
            this.panelSellFruit.Location = new System.Drawing.Point(421, 32);
            this.panelSellFruit.Name = "panelSellFruit";
            this.panelSellFruit.Size = new System.Drawing.Size(178, 110);
            this.panelSellFruit.TabIndex = 60;
            // 
            // panelSellForbiddenFruit
            // 
            this.panelSellForbiddenFruit.Controls.Add(this.btnForbiddenFruitList);
            this.panelSellForbiddenFruit.Controls.Add(this.lblMaxSellLimit1);
            this.panelSellForbiddenFruit.Controls.Add(this.txtMaxSellLimit);
            this.panelSellForbiddenFruit.Controls.Add(this.lblMaxSellLimit2);
            this.panelSellForbiddenFruit.Location = new System.Drawing.Point(11, 56);
            this.panelSellForbiddenFruit.Name = "panelSellForbiddenFruit";
            this.panelSellForbiddenFruit.Size = new System.Drawing.Size(152, 50);
            this.panelSellForbiddenFruit.TabIndex = 61;
            // 
            // btnForbiddenFruitList
            // 
            this.btnForbiddenFruitList.Location = new System.Drawing.Point(12, 3);
            this.btnForbiddenFruitList.Name = "btnForbiddenFruitList";
            this.btnForbiddenFruitList.Size = new System.Drawing.Size(125, 23);
            this.btnForbiddenFruitList.TabIndex = 61;
            this.btnForbiddenFruitList.Text = "设定禁止出售的果实";
            this.btnForbiddenFruitList.UseVisualStyleBackColor = true;
            this.btnForbiddenFruitList.Click += new System.EventHandler(this.btnForbiddenFruitList_Click);
            // 
            // lblMaxSellLimit1
            // 
            this.lblMaxSellLimit1.AutoSize = true;
            this.lblMaxSellLimit1.Location = new System.Drawing.Point(14, 30);
            this.lblMaxSellLimit1.Name = "lblMaxSellLimit1";
            this.lblMaxSellLimit1.Size = new System.Drawing.Size(41, 12);
            this.lblMaxSellLimit1.TabIndex = 60;
            this.lblMaxSellLimit1.Text = "额度：";
            // 
            // txtMaxSellLimit
            // 
            this.txtMaxSellLimit.Location = new System.Drawing.Point(57, 26);
            this.txtMaxSellLimit.Name = "txtMaxSellLimit";
            this.txtMaxSellLimit.Size = new System.Drawing.Size(35, 21);
            this.txtMaxSellLimit.TabIndex = 61;
            this.txtMaxSellLimit.Text = "300";
            // 
            // lblMaxSellLimit2
            // 
            this.lblMaxSellLimit2.AutoSize = true;
            this.lblMaxSellLimit2.Location = new System.Drawing.Point(94, 30);
            this.lblMaxSellLimit2.Name = "lblMaxSellLimit2";
            this.lblMaxSellLimit2.Size = new System.Drawing.Size(17, 12);
            this.lblMaxSellLimit2.TabIndex = 62;
            this.lblMaxSellLimit2.Text = "万";
            // 
            // lblLowCash
            // 
            this.lblLowCash.AutoSize = true;
            this.lblLowCash.Location = new System.Drawing.Point(110, 5);
            this.lblLowCash.Name = "lblLowCash";
            this.lblLowCash.Size = new System.Drawing.Size(53, 12);
            this.lblLowCash.TabIndex = 65;
            this.lblLowCash.Text = "万时执行";
            // 
            // txtLowCashLimit
            // 
            this.txtLowCashLimit.Location = new System.Drawing.Point(75, 1);
            this.txtLowCashLimit.Name = "txtLowCashLimit";
            this.txtLowCashLimit.Size = new System.Drawing.Size(33, 21);
            this.txtLowCashLimit.TabIndex = 64;
            this.txtLowCashLimit.Text = "100";
            // 
            // chkLowCash
            // 
            this.chkLowCash.AutoSize = true;
            this.chkLowCash.Location = new System.Drawing.Point(5, 4);
            this.chkLowCash.Name = "chkLowCash";
            this.chkLowCash.Size = new System.Drawing.Size(72, 16);
            this.chkLowCash.TabIndex = 63;
            this.chkLowCash.Text = "现金不足";
            this.chkLowCash.UseVisualStyleBackColor = true;
            // 
            // rdbSellAllFruit
            // 
            this.rdbSellAllFruit.AutoSize = true;
            this.rdbSellAllFruit.Location = new System.Drawing.Point(5, 23);
            this.rdbSellAllFruit.Name = "rdbSellAllFruit";
            this.rdbSellAllFruit.Size = new System.Drawing.Size(143, 16);
            this.rdbSellAllFruit.TabIndex = 51;
            this.rdbSellAllFruit.Text = "出售所有仓库中的果实";
            this.rdbSellAllFruit.UseVisualStyleBackColor = true;
            this.rdbSellAllFruit.CheckedChanged += new System.EventHandler(this.rdbSellAllFruit_CheckedChanged);
            // 
            // rdbFobiddenFruit
            // 
            this.rdbFobiddenFruit.AutoSize = true;
            this.rdbFobiddenFruit.Checked = true;
            this.rdbFobiddenFruit.Location = new System.Drawing.Point(5, 39);
            this.rdbFobiddenFruit.Name = "rdbFobiddenFruit";
            this.rdbFobiddenFruit.Size = new System.Drawing.Size(71, 16);
            this.rdbFobiddenFruit.TabIndex = 59;
            this.rdbFobiddenFruit.TabStop = true;
            this.rdbFobiddenFruit.Text = "指定果实";
            this.rdbFobiddenFruit.UseVisualStyleBackColor = true;
            // 
            // chkStealUnknowFruit
            // 
            this.chkStealUnknowFruit.AutoSize = true;
            this.chkStealUnknowFruit.Location = new System.Drawing.Point(14, 200);
            this.chkStealUnknowFruit.Name = "chkStealUnknowFruit";
            this.chkStealUnknowFruit.Size = new System.Drawing.Size(84, 16);
            this.chkStealUnknowFruit.TabIndex = 54;
            this.chkStealUnknowFruit.Text = "偷未知果实";
            this.chkStealUnknowFruit.UseVisualStyleBackColor = true;
            // 
            // btnSetFruitList
            // 
            this.btnSetFruitList.Location = new System.Drawing.Point(14, 174);
            this.btnSetFruitList.Name = "btnSetFruitList";
            this.btnSetFruitList.Size = new System.Drawing.Size(128, 23);
            this.btnSetFruitList.TabIndex = 53;
            this.btnSetFruitList.Text = "设定禁止偷窃的果实";
            this.btnSetFruitList.UseVisualStyleBackColor = true;
            this.btnSetFruitList.Click += new System.EventHandler(this.btnSetFruitList_Click);
            // 
            // chkSowMySeedsFirst
            // 
            this.chkSowMySeedsFirst.AutoSize = true;
            this.chkSowMySeedsFirst.Location = new System.Drawing.Point(8, 132);
            this.chkSowMySeedsFirst.Name = "chkSowMySeedsFirst";
            this.chkSowMySeedsFirst.Size = new System.Drawing.Size(132, 16);
            this.chkSowMySeedsFirst.TabIndex = 52;
            this.chkSowMySeedsFirst.Text = "优先播种已有的种子";
            this.chkSowMySeedsFirst.UseVisualStyleBackColor = true;
            // 
            // chkSellFruit
            // 
            this.chkSellFruit.AutoSize = true;
            this.chkSellFruit.Location = new System.Drawing.Point(412, 16);
            this.chkSellFruit.Name = "chkSellFruit";
            this.chkSellFruit.Size = new System.Drawing.Size(72, 16);
            this.chkSellFruit.TabIndex = 51;
            this.chkSellFruit.Text = "出售果实";
            this.chkSellFruit.UseVisualStyleBackColor = true;
            this.chkSellFruit.CheckedChanged += new System.EventHandler(this.chkSellFruit_CheckedChanged);
            // 
            // chkPresentFruit
            // 
            this.chkPresentFruit.AutoSize = true;
            this.chkPresentFruit.Location = new System.Drawing.Point(230, 136);
            this.chkPresentFruit.Name = "chkPresentFruit";
            this.chkPresentFruit.Size = new System.Drawing.Size(72, 16);
            this.chkPresentFruit.TabIndex = 50;
            this.chkPresentFruit.Text = "赠送果实";
            this.chkPresentFruit.UseVisualStyleBackColor = true;
            this.chkPresentFruit.CheckedChanged += new System.EventHandler(this.chkPresentFruit_CheckedChanged);
            // 
            // panelFarmSelf
            // 
            this.panelFarmSelf.Controls.Add(this.rdbExpensiveFarmSelf);
            this.panelFarmSelf.Controls.Add(this.cmbCustomFarmSelf);
            this.panelFarmSelf.Controls.Add(this.rdbCustomFarmSelf);
            this.panelFarmSelf.Controls.Add(this.lblFarmSelf);
            this.panelFarmSelf.Location = new System.Drawing.Point(14, 31);
            this.panelFarmSelf.Name = "panelFarmSelf";
            this.panelFarmSelf.Size = new System.Drawing.Size(197, 40);
            this.panelFarmSelf.TabIndex = 49;
            // 
            // rdbExpensiveFarmSelf
            // 
            this.rdbExpensiveFarmSelf.AutoSize = true;
            this.rdbExpensiveFarmSelf.Checked = true;
            this.rdbExpensiveFarmSelf.Location = new System.Drawing.Point(36, 1);
            this.rdbExpensiveFarmSelf.Name = "rdbExpensiveFarmSelf";
            this.rdbExpensiveFarmSelf.Size = new System.Drawing.Size(143, 16);
            this.rdbExpensiveFarmSelf.TabIndex = 0;
            this.rdbExpensiveFarmSelf.TabStop = true;
            this.rdbExpensiveFarmSelf.Text = "等级所允许买的最贵的";
            this.rdbExpensiveFarmSelf.UseVisualStyleBackColor = true;
            this.rdbExpensiveFarmSelf.CheckedChanged += new System.EventHandler(this.rdbExpensiveFarmSelf_CheckedChanged);
            // 
            // cmbCustomFarmSelf
            // 
            this.cmbCustomFarmSelf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomFarmSelf.FormattingEnabled = true;
            this.cmbCustomFarmSelf.Location = new System.Drawing.Point(91, 17);
            this.cmbCustomFarmSelf.Name = "cmbCustomFarmSelf";
            this.cmbCustomFarmSelf.Size = new System.Drawing.Size(103, 20);
            this.cmbCustomFarmSelf.TabIndex = 45;
            // 
            // rdbCustomFarmSelf
            // 
            this.rdbCustomFarmSelf.AutoSize = true;
            this.rdbCustomFarmSelf.Location = new System.Drawing.Point(36, 19);
            this.rdbCustomFarmSelf.Name = "rdbCustomFarmSelf";
            this.rdbCustomFarmSelf.Size = new System.Drawing.Size(59, 16);
            this.rdbCustomFarmSelf.TabIndex = 1;
            this.rdbCustomFarmSelf.Text = "自定义";
            this.rdbCustomFarmSelf.UseVisualStyleBackColor = true;
            // 
            // lblFarmSelf
            // 
            this.lblFarmSelf.AutoSize = true;
            this.lblFarmSelf.Location = new System.Drawing.Point(1, 4);
            this.lblFarmSelf.Name = "lblFarmSelf";
            this.lblFarmSelf.Size = new System.Drawing.Size(41, 12);
            this.lblFarmSelf.TabIndex = 50;
            this.lblFarmSelf.Text = "种子：";
            // 
            // panelFarmShared
            // 
            this.panelFarmShared.Controls.Add(this.cmbCustomFarmShared);
            this.panelFarmShared.Controls.Add(this.rdbCustomFarmShared);
            this.panelFarmShared.Controls.Add(this.rdbExpensiveFarmShared);
            this.panelFarmShared.Controls.Add(this.lblFarmShared);
            this.panelFarmShared.Location = new System.Drawing.Point(14, 88);
            this.panelFarmShared.Name = "panelFarmShared";
            this.panelFarmShared.Size = new System.Drawing.Size(197, 40);
            this.panelFarmShared.TabIndex = 48;
            // 
            // cmbCustomFarmShared
            // 
            this.cmbCustomFarmShared.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomFarmShared.FormattingEnabled = true;
            this.cmbCustomFarmShared.Location = new System.Drawing.Point(91, 18);
            this.cmbCustomFarmShared.Name = "cmbCustomFarmShared";
            this.cmbCustomFarmShared.Size = new System.Drawing.Size(103, 20);
            this.cmbCustomFarmShared.TabIndex = 45;
            // 
            // rdbCustomFarmShared
            // 
            this.rdbCustomFarmShared.AutoSize = true;
            this.rdbCustomFarmShared.Location = new System.Drawing.Point(36, 19);
            this.rdbCustomFarmShared.Name = "rdbCustomFarmShared";
            this.rdbCustomFarmShared.Size = new System.Drawing.Size(59, 16);
            this.rdbCustomFarmShared.TabIndex = 1;
            this.rdbCustomFarmShared.Text = "自定义";
            this.rdbCustomFarmShared.UseVisualStyleBackColor = true;
            // 
            // rdbExpensiveFarmShared
            // 
            this.rdbExpensiveFarmShared.AutoSize = true;
            this.rdbExpensiveFarmShared.Checked = true;
            this.rdbExpensiveFarmShared.Location = new System.Drawing.Point(36, 1);
            this.rdbExpensiveFarmShared.Name = "rdbExpensiveFarmShared";
            this.rdbExpensiveFarmShared.Size = new System.Drawing.Size(143, 16);
            this.rdbExpensiveFarmShared.TabIndex = 0;
            this.rdbExpensiveFarmShared.TabStop = true;
            this.rdbExpensiveFarmShared.Text = "等级所允许买的最贵的";
            this.rdbExpensiveFarmShared.UseVisualStyleBackColor = true;
            this.rdbExpensiveFarmShared.CheckedChanged += new System.EventHandler(this.rdbExpensiveFarmShared_CheckedChanged);
            // 
            // lblFarmShared
            // 
            this.lblFarmShared.AutoSize = true;
            this.lblFarmShared.Location = new System.Drawing.Point(1, 4);
            this.lblFarmShared.Name = "lblFarmShared";
            this.lblFarmShared.Size = new System.Drawing.Size(41, 12);
            this.lblFarmShared.TabIndex = 43;
            this.lblFarmShared.Text = "种子：";
            // 
            // chkHelpOthers
            // 
            this.chkHelpOthers.AutoSize = true;
            this.chkHelpOthers.Location = new System.Drawing.Point(230, 73);
            this.chkHelpOthers.Name = "chkHelpOthers";
            this.chkHelpOthers.Size = new System.Drawing.Size(108, 16);
            this.chkHelpOthers.TabIndex = 47;
            this.chkHelpOthers.Text = "去好友花园帮忙";
            this.chkHelpOthers.UseVisualStyleBackColor = true;
            // 
            // lblSeedsCount
            // 
            this.lblSeedsCount.AutoSize = true;
            this.lblSeedsCount.Location = new System.Drawing.Point(249, 53);
            this.lblSeedsCount.Name = "lblSeedsCount";
            this.lblSeedsCount.Size = new System.Drawing.Size(89, 12);
            this.lblSeedsCount.TabIndex = 44;
            this.lblSeedsCount.Text = "每次够买个数：";
            // 
            // chkFarmShared
            // 
            this.chkFarmShared.AutoSize = true;
            this.chkFarmShared.Location = new System.Drawing.Point(8, 72);
            this.chkFarmShared.Name = "chkFarmShared";
            this.chkFarmShared.Size = new System.Drawing.Size(96, 16);
            this.chkFarmShared.TabIndex = 41;
            this.chkFarmShared.Text = "播种爱心地块";
            this.chkFarmShared.UseVisualStyleBackColor = true;
            this.chkFarmShared.CheckedChanged += new System.EventHandler(this.chkFarmShared_CheckedChanged);
            // 
            // grpRanch
            // 
            this.grpRanch.Controls.Add(this.lblWarningHelpRanch);
            this.grpRanch.Controls.Add(this.lblWarningStealProduct);
            this.grpRanch.Controls.Add(this.lblWarningPresentProduct);
            this.grpRanch.Controls.Add(this.panelSellProduct);
            this.grpRanch.Controls.Add(this.panelPresentProduct);
            this.grpRanch.Controls.Add(this.panelBuyCalf);
            this.grpRanch.Controls.Add(this.txtBambooNum);
            this.grpRanch.Controls.Add(this.lblBambooNum);
            this.grpRanch.Controls.Add(this.chkHelpAddBamboo);
            this.grpRanch.Controls.Add(this.chkAddBamboo);
            this.grpRanch.Controls.Add(this.lblCarrotNum);
            this.grpRanch.Controls.Add(this.txtCarrotNum);
            this.grpRanch.Controls.Add(this.chkHelpAddCarrot);
            this.grpRanch.Controls.Add(this.chkAddCarrot);
            this.grpRanch.Controls.Add(this.chkSellProduct);
            this.grpRanch.Controls.Add(this.chkPresentProduct);
            this.grpRanch.Controls.Add(this.chkHarvestAnimal);
            this.grpRanch.Controls.Add(this.lblFoodNum);
            this.grpRanch.Controls.Add(this.txtFoodNum);
            this.grpRanch.Controls.Add(this.chkBreedAnimal);
            this.grpRanch.Controls.Add(this.chkBuyCalf);
            this.grpRanch.Controls.Add(this.chkHelpMakeProduct);
            this.grpRanch.Controls.Add(this.chkHarvestProduct);
            this.grpRanch.Controls.Add(this.chkMakeProduct);
            this.grpRanch.Controls.Add(this.chkHelpAddGrass);
            this.grpRanch.Controls.Add(this.chkStealProduct);
            this.grpRanch.Controls.Add(this.chkAddGrass);
            this.grpRanch.Controls.Add(this.chkHelpAddWater);
            this.grpRanch.Controls.Add(this.chkAddWater);
            this.grpRanch.Location = new System.Drawing.Point(7, 3);
            this.grpRanch.Name = "grpRanch";
            this.grpRanch.Size = new System.Drawing.Size(608, 246);
            this.grpRanch.TabIndex = 51;
            this.grpRanch.TabStop = false;
            this.grpRanch.Text = "牧场";
            // 
            // lblWarningHelpRanch
            // 
            this.lblWarningHelpRanch.AutoSize = true;
            this.lblWarningHelpRanch.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningHelpRanch.ForeColor = System.Drawing.Color.Red;
            this.lblWarningHelpRanch.Location = new System.Drawing.Point(272, 21);
            this.lblWarningHelpRanch.Name = "lblWarningHelpRanch";
            this.lblWarningHelpRanch.Size = new System.Drawing.Size(11, 12);
            this.lblWarningHelpRanch.TabIndex = 66;
            this.lblWarningHelpRanch.Text = "*";
            // 
            // lblWarningStealProduct
            // 
            this.lblWarningStealProduct.AutoSize = true;
            this.lblWarningStealProduct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningStealProduct.ForeColor = System.Drawing.Color.Red;
            this.lblWarningStealProduct.Location = new System.Drawing.Point(272, 114);
            this.lblWarningStealProduct.Name = "lblWarningStealProduct";
            this.lblWarningStealProduct.Size = new System.Drawing.Size(11, 12);
            this.lblWarningStealProduct.TabIndex = 65;
            this.lblWarningStealProduct.Text = "*";
            // 
            // lblWarningPresentProduct
            // 
            this.lblWarningPresentProduct.AutoSize = true;
            this.lblWarningPresentProduct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningPresentProduct.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPresentProduct.Location = new System.Drawing.Point(283, 132);
            this.lblWarningPresentProduct.Name = "lblWarningPresentProduct";
            this.lblWarningPresentProduct.Size = new System.Drawing.Size(11, 12);
            this.lblWarningPresentProduct.TabIndex = 64;
            this.lblWarningPresentProduct.Text = "*";
            // 
            // panelSellProduct
            // 
            this.panelSellProduct.Controls.Add(this.panelSellProductForbidden);
            this.panelSellProduct.Controls.Add(this.lblSellProductLowCash);
            this.panelSellProduct.Controls.Add(this.txtSellProductLowCashLimit);
            this.panelSellProduct.Controls.Add(this.chkSellProductLowCash);
            this.panelSellProduct.Controls.Add(this.rdbSellAllProducts);
            this.panelSellProduct.Controls.Add(this.rdbSellForbiddenProduct);
            this.panelSellProduct.Location = new System.Drawing.Point(423, 40);
            this.panelSellProduct.Name = "panelSellProduct";
            this.panelSellProduct.Size = new System.Drawing.Size(179, 120);
            this.panelSellProduct.TabIndex = 63;
            // 
            // panelSellProductForbidden
            // 
            this.panelSellProductForbidden.Controls.Add(this.btnSellProductForbiddenList);
            this.panelSellProductForbidden.Controls.Add(this.lblSellProductMaxLimit);
            this.panelSellProductForbidden.Controls.Add(this.txtSellProductMaxLimit);
            this.panelSellProductForbidden.Controls.Add(this.lblSellProductMaxLimit2);
            this.panelSellProductForbidden.Location = new System.Drawing.Point(11, 63);
            this.panelSellProductForbidden.Name = "panelSellProductForbidden";
            this.panelSellProductForbidden.Size = new System.Drawing.Size(161, 50);
            this.panelSellProductForbidden.TabIndex = 61;
            // 
            // btnSellProductForbiddenList
            // 
            this.btnSellProductForbiddenList.Location = new System.Drawing.Point(9, 2);
            this.btnSellProductForbiddenList.Name = "btnSellProductForbiddenList";
            this.btnSellProductForbiddenList.Size = new System.Drawing.Size(146, 23);
            this.btnSellProductForbiddenList.TabIndex = 61;
            this.btnSellProductForbiddenList.Text = "设定禁止出售的农副产品";
            this.btnSellProductForbiddenList.UseVisualStyleBackColor = true;
            this.btnSellProductForbiddenList.Click += new System.EventHandler(this.btnSellProductForbiddenList_Click);
            // 
            // lblSellProductMaxLimit
            // 
            this.lblSellProductMaxLimit.AutoSize = true;
            this.lblSellProductMaxLimit.Location = new System.Drawing.Point(11, 29);
            this.lblSellProductMaxLimit.Name = "lblSellProductMaxLimit";
            this.lblSellProductMaxLimit.Size = new System.Drawing.Size(41, 12);
            this.lblSellProductMaxLimit.TabIndex = 60;
            this.lblSellProductMaxLimit.Text = "额度：";
            // 
            // txtSellProductMaxLimit
            // 
            this.txtSellProductMaxLimit.Location = new System.Drawing.Point(55, 25);
            this.txtSellProductMaxLimit.Name = "txtSellProductMaxLimit";
            this.txtSellProductMaxLimit.Size = new System.Drawing.Size(35, 21);
            this.txtSellProductMaxLimit.TabIndex = 61;
            // 
            // lblSellProductMaxLimit2
            // 
            this.lblSellProductMaxLimit2.AutoSize = true;
            this.lblSellProductMaxLimit2.Location = new System.Drawing.Point(93, 29);
            this.lblSellProductMaxLimit2.Name = "lblSellProductMaxLimit2";
            this.lblSellProductMaxLimit2.Size = new System.Drawing.Size(17, 12);
            this.lblSellProductMaxLimit2.TabIndex = 62;
            this.lblSellProductMaxLimit2.Text = "万";
            // 
            // lblSellProductLowCash
            // 
            this.lblSellProductLowCash.AutoSize = true;
            this.lblSellProductLowCash.Location = new System.Drawing.Point(110, 7);
            this.lblSellProductLowCash.Name = "lblSellProductLowCash";
            this.lblSellProductLowCash.Size = new System.Drawing.Size(53, 12);
            this.lblSellProductLowCash.TabIndex = 65;
            this.lblSellProductLowCash.Text = "万时执行";
            // 
            // txtSellProductLowCashLimit
            // 
            this.txtSellProductLowCashLimit.Location = new System.Drawing.Point(75, 3);
            this.txtSellProductLowCashLimit.Name = "txtSellProductLowCashLimit";
            this.txtSellProductLowCashLimit.Size = new System.Drawing.Size(33, 21);
            this.txtSellProductLowCashLimit.TabIndex = 64;
            // 
            // chkSellProductLowCash
            // 
            this.chkSellProductLowCash.AutoSize = true;
            this.chkSellProductLowCash.Location = new System.Drawing.Point(5, 6);
            this.chkSellProductLowCash.Name = "chkSellProductLowCash";
            this.chkSellProductLowCash.Size = new System.Drawing.Size(72, 16);
            this.chkSellProductLowCash.TabIndex = 63;
            this.chkSellProductLowCash.Text = "现金不足";
            this.chkSellProductLowCash.UseVisualStyleBackColor = true;
            // 
            // rdbSellAllProducts
            // 
            this.rdbSellAllProducts.AutoSize = true;
            this.rdbSellAllProducts.Location = new System.Drawing.Point(5, 26);
            this.rdbSellAllProducts.Name = "rdbSellAllProducts";
            this.rdbSellAllProducts.Size = new System.Drawing.Size(167, 16);
            this.rdbSellAllProducts.TabIndex = 51;
            this.rdbSellAllProducts.Text = "出售所有仓库中的农副产品";
            this.rdbSellAllProducts.UseVisualStyleBackColor = true;
            this.rdbSellAllProducts.CheckedChanged += new System.EventHandler(this.rdbSellAllProducts_CheckedChanged);
            // 
            // rdbSellForbiddenProduct
            // 
            this.rdbSellForbiddenProduct.AutoSize = true;
            this.rdbSellForbiddenProduct.Checked = true;
            this.rdbSellForbiddenProduct.Location = new System.Drawing.Point(6, 46);
            this.rdbSellForbiddenProduct.Name = "rdbSellForbiddenProduct";
            this.rdbSellForbiddenProduct.Size = new System.Drawing.Size(95, 16);
            this.rdbSellForbiddenProduct.TabIndex = 59;
            this.rdbSellForbiddenProduct.TabStop = true;
            this.rdbSellForbiddenProduct.Text = "指定农副产品";
            this.rdbSellForbiddenProduct.UseVisualStyleBackColor = true;
            // 
            // panelPresentProduct
            // 
            this.panelPresentProduct.Controls.Add(this.lblPresentProductCheckValue);
            this.panelPresentProduct.Controls.Add(this.txtPresentProductValue);
            this.panelPresentProduct.Controls.Add(this.chkPresentProductCheckValue);
            this.panelPresentProduct.Controls.Add(this.lblPresentProductCheckNum);
            this.panelPresentProduct.Controls.Add(this.txtPresentProductNum);
            this.panelPresentProduct.Controls.Add(this.chkPresentProductCheckNum);
            this.panelPresentProduct.Controls.Add(this.rdbPresentProductByPrice);
            this.panelPresentProduct.Controls.Add(this.cmbAnimalProducts);
            this.panelPresentProduct.Controls.Add(this.rdbPresentProductCustom);
            this.panelPresentProduct.Location = new System.Drawing.Point(201, 147);
            this.panelPresentProduct.Name = "panelPresentProduct";
            this.panelPresentProduct.Size = new System.Drawing.Size(196, 93);
            this.panelPresentProduct.TabIndex = 62;
            // 
            // lblPresentProductCheckValue
            // 
            this.lblPresentProductCheckValue.AutoSize = true;
            this.lblPresentProductCheckValue.Location = new System.Drawing.Point(172, 25);
            this.lblPresentProductCheckValue.Name = "lblPresentProductCheckValue";
            this.lblPresentProductCheckValue.Size = new System.Drawing.Size(17, 12);
            this.lblPresentProductCheckValue.TabIndex = 86;
            this.lblPresentProductCheckValue.Text = "万";
            // 
            // txtPresentProductValue
            // 
            this.txtPresentProductValue.Location = new System.Drawing.Point(108, 20);
            this.txtPresentProductValue.Name = "txtPresentProductValue";
            this.txtPresentProductValue.Size = new System.Drawing.Size(62, 21);
            this.txtPresentProductValue.TabIndex = 85;
            // 
            // chkPresentProductCheckValue
            // 
            this.chkPresentProductCheckValue.AutoSize = true;
            this.chkPresentProductCheckValue.Location = new System.Drawing.Point(14, 23);
            this.chkPresentProductCheckValue.Name = "chkPresentProductCheckValue";
            this.chkPresentProductCheckValue.Size = new System.Drawing.Size(96, 16);
            this.chkPresentProductCheckValue.TabIndex = 84;
            this.chkPresentProductCheckValue.Text = "最低赠送价值";
            this.chkPresentProductCheckValue.UseVisualStyleBackColor = true;
            // 
            // lblPresentProductCheckNum
            // 
            this.lblPresentProductCheckNum.AutoSize = true;
            this.lblPresentProductCheckNum.Location = new System.Drawing.Point(172, 71);
            this.lblPresentProductCheckNum.Name = "lblPresentProductCheckNum";
            this.lblPresentProductCheckNum.Size = new System.Drawing.Size(17, 12);
            this.lblPresentProductCheckNum.TabIndex = 83;
            this.lblPresentProductCheckNum.Text = "个";
            // 
            // txtPresentProductNum
            // 
            this.txtPresentProductNum.Location = new System.Drawing.Point(108, 67);
            this.txtPresentProductNum.Name = "txtPresentProductNum";
            this.txtPresentProductNum.Size = new System.Drawing.Size(62, 21);
            this.txtPresentProductNum.TabIndex = 81;
            // 
            // chkPresentProductCheckNum
            // 
            this.chkPresentProductCheckNum.AutoSize = true;
            this.chkPresentProductCheckNum.Location = new System.Drawing.Point(14, 69);
            this.chkPresentProductCheckNum.Name = "chkPresentProductCheckNum";
            this.chkPresentProductCheckNum.Size = new System.Drawing.Size(96, 16);
            this.chkPresentProductCheckNum.TabIndex = 82;
            this.chkPresentProductCheckNum.Text = "最低赠送数量";
            this.chkPresentProductCheckNum.UseVisualStyleBackColor = true;
            // 
            // rdbPresentProductByPrice
            // 
            this.rdbPresentProductByPrice.AutoSize = true;
            this.rdbPresentProductByPrice.Checked = true;
            this.rdbPresentProductByPrice.Location = new System.Drawing.Point(3, 3);
            this.rdbPresentProductByPrice.Name = "rdbPresentProductByPrice";
            this.rdbPresentProductByPrice.Size = new System.Drawing.Size(143, 16);
            this.rdbPresentProductByPrice.TabIndex = 0;
            this.rdbPresentProductByPrice.TabStop = true;
            this.rdbPresentProductByPrice.Text = "优先赠送总价值最高的";
            this.rdbPresentProductByPrice.UseVisualStyleBackColor = true;
            this.rdbPresentProductByPrice.CheckedChanged += new System.EventHandler(this.rdbPresentProductByPrice_CheckedChanged);
            // 
            // cmbAnimalProducts
            // 
            this.cmbAnimalProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimalProducts.FormattingEnabled = true;
            this.cmbAnimalProducts.Location = new System.Drawing.Point(63, 44);
            this.cmbAnimalProducts.Name = "cmbAnimalProducts";
            this.cmbAnimalProducts.Size = new System.Drawing.Size(116, 20);
            this.cmbAnimalProducts.TabIndex = 45;
            // 
            // rdbPresentProductCustom
            // 
            this.rdbPresentProductCustom.AutoSize = true;
            this.rdbPresentProductCustom.Location = new System.Drawing.Point(3, 45);
            this.rdbPresentProductCustom.Name = "rdbPresentProductCustom";
            this.rdbPresentProductCustom.Size = new System.Drawing.Size(59, 16);
            this.rdbPresentProductCustom.TabIndex = 1;
            this.rdbPresentProductCustom.Text = "自定义";
            this.rdbPresentProductCustom.UseVisualStyleBackColor = true;
            // 
            // panelBuyCalf
            // 
            this.panelBuyCalf.Controls.Add(this.rdbBuyCalfByPrice);
            this.panelBuyCalf.Controls.Add(this.cmbBuyCalfCustom);
            this.panelBuyCalf.Controls.Add(this.rdbBuyCalfCustom);
            this.panelBuyCalf.Location = new System.Drawing.Point(14, 195);
            this.panelBuyCalf.Name = "panelBuyCalf";
            this.panelBuyCalf.Size = new System.Drawing.Size(176, 45);
            this.panelBuyCalf.TabIndex = 61;
            // 
            // rdbBuyCalfByPrice
            // 
            this.rdbBuyCalfByPrice.AutoSize = true;
            this.rdbBuyCalfByPrice.Checked = true;
            this.rdbBuyCalfByPrice.Location = new System.Drawing.Point(3, 3);
            this.rdbBuyCalfByPrice.Name = "rdbBuyCalfByPrice";
            this.rdbBuyCalfByPrice.Size = new System.Drawing.Size(131, 16);
            this.rdbBuyCalfByPrice.TabIndex = 0;
            this.rdbBuyCalfByPrice.TabStop = true;
            this.rdbBuyCalfByPrice.Text = "优先购买价格最高的";
            this.rdbBuyCalfByPrice.UseVisualStyleBackColor = true;
            this.rdbBuyCalfByPrice.CheckedChanged += new System.EventHandler(this.rdbBuyCalfByPrice_CheckedChanged);
            // 
            // cmbBuyCalfCustom
            // 
            this.cmbBuyCalfCustom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuyCalfCustom.FormattingEnabled = true;
            this.cmbBuyCalfCustom.Location = new System.Drawing.Point(57, 21);
            this.cmbBuyCalfCustom.Name = "cmbBuyCalfCustom";
            this.cmbBuyCalfCustom.Size = new System.Drawing.Size(111, 20);
            this.cmbBuyCalfCustom.TabIndex = 45;
            // 
            // rdbBuyCalfCustom
            // 
            this.rdbBuyCalfCustom.AutoSize = true;
            this.rdbBuyCalfCustom.Location = new System.Drawing.Point(3, 21);
            this.rdbBuyCalfCustom.Name = "rdbBuyCalfCustom";
            this.rdbBuyCalfCustom.Size = new System.Drawing.Size(59, 16);
            this.rdbBuyCalfCustom.TabIndex = 1;
            this.rdbBuyCalfCustom.Text = "自定义";
            this.rdbBuyCalfCustom.UseVisualStyleBackColor = true;
            // 
            // txtBambooNum
            // 
            this.txtBambooNum.Location = new System.Drawing.Point(345, 76);
            this.txtBambooNum.Name = "txtBambooNum";
            this.txtBambooNum.Size = new System.Drawing.Size(59, 21);
            this.txtBambooNum.TabIndex = 60;
            // 
            // lblBambooNum
            // 
            this.lblBambooNum.AutoSize = true;
            this.lblBambooNum.Location = new System.Drawing.Point(304, 79);
            this.lblBambooNum.Name = "lblBambooNum";
            this.lblBambooNum.Size = new System.Drawing.Size(41, 12);
            this.lblBambooNum.TabIndex = 59;
            this.lblBambooNum.Text = "数量：";
            // 
            // chkHelpAddBamboo
            // 
            this.chkHelpAddBamboo.AutoSize = true;
            this.chkHelpAddBamboo.Location = new System.Drawing.Point(190, 77);
            this.chkHelpAddBamboo.Name = "chkHelpAddBamboo";
            this.chkHelpAddBamboo.Size = new System.Drawing.Size(96, 16);
            this.chkHelpAddBamboo.TabIndex = 58;
            this.chkHelpAddBamboo.Text = "帮好友添竹子";
            this.chkHelpAddBamboo.UseVisualStyleBackColor = true;
            // 
            // chkAddBamboo
            // 
            this.chkAddBamboo.AutoSize = true;
            this.chkAddBamboo.Location = new System.Drawing.Point(12, 80);
            this.chkAddBamboo.Name = "chkAddBamboo";
            this.chkAddBamboo.Size = new System.Drawing.Size(60, 16);
            this.chkAddBamboo.TabIndex = 57;
            this.chkAddBamboo.Text = "添竹子";
            this.chkAddBamboo.UseVisualStyleBackColor = true;
            // 
            // lblCarrotNum
            // 
            this.lblCarrotNum.AutoSize = true;
            this.lblCarrotNum.Location = new System.Drawing.Point(304, 58);
            this.lblCarrotNum.Name = "lblCarrotNum";
            this.lblCarrotNum.Size = new System.Drawing.Size(41, 12);
            this.lblCarrotNum.TabIndex = 56;
            this.lblCarrotNum.Text = "数量：";
            // 
            // txtCarrotNum
            // 
            this.txtCarrotNum.Location = new System.Drawing.Point(345, 54);
            this.txtCarrotNum.Name = "txtCarrotNum";
            this.txtCarrotNum.Size = new System.Drawing.Size(59, 21);
            this.txtCarrotNum.TabIndex = 55;
            // 
            // chkHelpAddCarrot
            // 
            this.chkHelpAddCarrot.AutoSize = true;
            this.chkHelpAddCarrot.Location = new System.Drawing.Point(190, 58);
            this.chkHelpAddCarrot.Name = "chkHelpAddCarrot";
            this.chkHelpAddCarrot.Size = new System.Drawing.Size(108, 16);
            this.chkHelpAddCarrot.TabIndex = 54;
            this.chkHelpAddCarrot.Text = "帮好友添胡萝卜";
            this.chkHelpAddCarrot.UseVisualStyleBackColor = true;
            // 
            // chkAddCarrot
            // 
            this.chkAddCarrot.AutoSize = true;
            this.chkAddCarrot.Location = new System.Drawing.Point(12, 60);
            this.chkAddCarrot.Name = "chkAddCarrot";
            this.chkAddCarrot.Size = new System.Drawing.Size(72, 16);
            this.chkAddCarrot.TabIndex = 53;
            this.chkAddCarrot.Text = "添胡萝卜";
            this.chkAddCarrot.UseVisualStyleBackColor = true;
            // 
            // chkSellProduct
            // 
            this.chkSellProduct.AutoSize = true;
            this.chkSellProduct.Location = new System.Drawing.Point(417, 20);
            this.chkSellProduct.Name = "chkSellProduct";
            this.chkSellProduct.Size = new System.Drawing.Size(96, 16);
            this.chkSellProduct.TabIndex = 52;
            this.chkSellProduct.Text = "出售农副产品";
            this.chkSellProduct.UseVisualStyleBackColor = true;
            this.chkSellProduct.CheckedChanged += new System.EventHandler(this.chkSellProduct_CheckedChanged);
            // 
            // chkPresentProduct
            // 
            this.chkPresentProduct.AutoSize = true;
            this.chkPresentProduct.Location = new System.Drawing.Point(190, 131);
            this.chkPresentProduct.Name = "chkPresentProduct";
            this.chkPresentProduct.Size = new System.Drawing.Size(96, 16);
            this.chkPresentProduct.TabIndex = 51;
            this.chkPresentProduct.Text = "赠送农副产品";
            this.chkPresentProduct.UseVisualStyleBackColor = true;
            this.chkPresentProduct.CheckedChanged += new System.EventHandler(this.chkPresentProduct_CheckedChanged);
            // 
            // chkHarvestAnimal
            // 
            this.chkHarvestAnimal.AutoSize = true;
            this.chkHarvestAnimal.Location = new System.Drawing.Point(12, 140);
            this.chkHarvestAnimal.Name = "chkHarvestAnimal";
            this.chkHarvestAnimal.Size = new System.Drawing.Size(72, 16);
            this.chkHarvestAnimal.TabIndex = 38;
            this.chkHarvestAnimal.Text = "收获动物";
            this.chkHarvestAnimal.UseVisualStyleBackColor = true;
            // 
            // lblFoodNum
            // 
            this.lblFoodNum.AutoSize = true;
            this.lblFoodNum.Location = new System.Drawing.Point(304, 37);
            this.lblFoodNum.Name = "lblFoodNum";
            this.lblFoodNum.Size = new System.Drawing.Size(41, 12);
            this.lblFoodNum.TabIndex = 37;
            this.lblFoodNum.Text = "数量：";
            // 
            // txtFoodNum
            // 
            this.txtFoodNum.Location = new System.Drawing.Point(345, 32);
            this.txtFoodNum.Name = "txtFoodNum";
            this.txtFoodNum.Size = new System.Drawing.Size(59, 21);
            this.txtFoodNum.TabIndex = 36;
            // 
            // chkBuyCalf
            // 
            this.chkBuyCalf.AutoSize = true;
            this.chkBuyCalf.Location = new System.Drawing.Point(12, 178);
            this.chkBuyCalf.Name = "chkBuyCalf";
            this.chkBuyCalf.Size = new System.Drawing.Size(72, 16);
            this.chkBuyCalf.TabIndex = 32;
            this.chkBuyCalf.Text = "购买幼崽";
            this.chkBuyCalf.UseVisualStyleBackColor = true;
            this.chkBuyCalf.CheckedChanged += new System.EventHandler(this.chkBuyCalf_CheckedChanged);
            // 
            // chkHelpAddGrass
            // 
            this.chkHelpAddGrass.AutoSize = true;
            this.chkHelpAddGrass.Location = new System.Drawing.Point(190, 38);
            this.chkHelpAddGrass.Name = "chkHelpAddGrass";
            this.chkHelpAddGrass.Size = new System.Drawing.Size(96, 16);
            this.chkHelpAddGrass.TabIndex = 31;
            this.chkHelpAddGrass.Text = "帮好友添牧草";
            this.chkHelpAddGrass.UseVisualStyleBackColor = true;
            // 
            // chkAddGrass
            // 
            this.chkAddGrass.AutoSize = true;
            this.chkAddGrass.Location = new System.Drawing.Point(12, 40);
            this.chkAddGrass.Name = "chkAddGrass";
            this.chkAddGrass.Size = new System.Drawing.Size(60, 16);
            this.chkAddGrass.TabIndex = 30;
            this.chkAddGrass.Text = "添牧草";
            this.chkAddGrass.UseVisualStyleBackColor = true;
            // 
            // chkHelpAddWater
            // 
            this.chkHelpAddWater.AutoSize = true;
            this.chkHelpAddWater.Location = new System.Drawing.Point(190, 19);
            this.chkHelpAddWater.Name = "chkHelpAddWater";
            this.chkHelpAddWater.Size = new System.Drawing.Size(84, 16);
            this.chkHelpAddWater.TabIndex = 29;
            this.chkHelpAddWater.Text = "帮好友添水";
            this.chkHelpAddWater.UseVisualStyleBackColor = true;
            // 
            // chkAddWater
            // 
            this.chkAddWater.AutoSize = true;
            this.chkAddWater.Location = new System.Drawing.Point(12, 20);
            this.chkAddWater.Name = "chkAddWater";
            this.chkAddWater.Size = new System.Drawing.Size(48, 16);
            this.chkAddWater.TabIndex = 28;
            this.chkAddWater.Text = "添水";
            this.chkAddWater.UseVisualStyleBackColor = true;
            // 
            // tabGames
            // 
            this.tabGames.Controls.Add(this.tabPagePark);
            this.tabGames.Controls.Add(this.tabPageBite);
            this.tabGames.Controls.Add(this.tabPageSlave);
            this.tabGames.Controls.Add(this.tabPageHouse);
            this.tabGames.Controls.Add(this.tabPageGarden);
            this.tabGames.Controls.Add(this.tabPageRanch);
            this.tabGames.Controls.Add(this.tabPageFish);
            this.tabGames.Controls.Add(this.tabPageRich);
            this.tabGames.Controls.Add(this.tabPageCafe);
            this.tabGames.Location = new System.Drawing.Point(9, 279);
            this.tabGames.Name = "tabGames";
            this.tabGames.SelectedIndex = 0;
            this.tabGames.Size = new System.Drawing.Size(626, 284);
            this.tabGames.TabIndex = 52;
            // 
            // tabPagePark
            // 
            this.tabPagePark.Controls.Add(this.grpPark);
            this.tabPagePark.Location = new System.Drawing.Point(4, 21);
            this.tabPagePark.Name = "tabPagePark";
            this.tabPagePark.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePark.Size = new System.Drawing.Size(618, 259);
            this.tabPagePark.TabIndex = 0;
            this.tabPagePark.Text = "争车位";
            this.tabPagePark.UseVisualStyleBackColor = true;
            // 
            // tabPageBite
            // 
            this.tabPageBite.Controls.Add(this.grpBite);
            this.tabPageBite.Location = new System.Drawing.Point(4, 21);
            this.tabPageBite.Name = "tabPageBite";
            this.tabPageBite.Size = new System.Drawing.Size(618, 259);
            this.tabPageBite.TabIndex = 5;
            this.tabPageBite.Text = "咬人";
            this.tabPageBite.UseVisualStyleBackColor = true;
            // 
            // grpBite
            // 
            this.grpBite.Controls.Add(this.lblWarningProtect);
            this.grpBite.Controls.Add(this.lblWarningRecover);
            this.grpBite.Controls.Add(this.lblWarningBite);
            this.grpBite.Controls.Add(this.chkProtectFriend);
            this.grpBite.Controls.Add(this.chkAutoRecover);
            this.grpBite.Controls.Add(this.chkApproveRecovery);
            this.grpBite.Controls.Add(this.chkBiteOthers);
            this.grpBite.Location = new System.Drawing.Point(7, 3);
            this.grpBite.Name = "grpBite";
            this.grpBite.Size = new System.Drawing.Size(584, 239);
            this.grpBite.TabIndex = 46;
            this.grpBite.TabStop = false;
            this.grpBite.Text = "咬人";
            // 
            // lblWarningProtect
            // 
            this.lblWarningProtect.AutoSize = true;
            this.lblWarningProtect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningProtect.ForeColor = System.Drawing.Color.Red;
            this.lblWarningProtect.Location = new System.Drawing.Point(226, 42);
            this.lblWarningProtect.Name = "lblWarningProtect";
            this.lblWarningProtect.Size = new System.Drawing.Size(11, 12);
            this.lblWarningProtect.TabIndex = 43;
            this.lblWarningProtect.Text = "*";
            // 
            // lblWarningRecover
            // 
            this.lblWarningRecover.AutoSize = true;
            this.lblWarningRecover.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningRecover.ForeColor = System.Drawing.Color.Red;
            this.lblWarningRecover.Location = new System.Drawing.Point(78, 42);
            this.lblWarningRecover.Name = "lblWarningRecover";
            this.lblWarningRecover.Size = new System.Drawing.Size(11, 12);
            this.lblWarningRecover.TabIndex = 42;
            this.lblWarningRecover.Text = "*";
            // 
            // lblWarningBite
            // 
            this.lblWarningBite.AutoSize = true;
            this.lblWarningBite.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningBite.ForeColor = System.Drawing.Color.Red;
            this.lblWarningBite.Location = new System.Drawing.Point(214, 21);
            this.lblWarningBite.Name = "lblWarningBite";
            this.lblWarningBite.Size = new System.Drawing.Size(11, 12);
            this.lblWarningBite.TabIndex = 41;
            this.lblWarningBite.Text = "*";
            // 
            // chkProtectFriend
            // 
            this.chkProtectFriend.AutoSize = true;
            this.chkProtectFriend.Location = new System.Drawing.Point(160, 40);
            this.chkProtectFriend.Name = "chkProtectFriend";
            this.chkProtectFriend.Size = new System.Drawing.Size(72, 16);
            this.chkProtectFriend.TabIndex = 29;
            this.chkProtectFriend.Text = "保护好友";
            this.chkProtectFriend.UseVisualStyleBackColor = true;
            // 
            // chkAutoRecover
            // 
            this.chkAutoRecover.AutoSize = true;
            this.chkAutoRecover.Location = new System.Drawing.Point(12, 40);
            this.chkAutoRecover.Name = "chkAutoRecover";
            this.chkAutoRecover.Size = new System.Drawing.Size(72, 16);
            this.chkAutoRecover.TabIndex = 24;
            this.chkAutoRecover.Text = "自动休息";
            this.chkAutoRecover.UseVisualStyleBackColor = true;
            // 
            // chkApproveRecovery
            // 
            this.chkApproveRecovery.AutoSize = true;
            this.chkApproveRecovery.Location = new System.Drawing.Point(12, 20);
            this.chkApproveRecovery.Name = "chkApproveRecovery";
            this.chkApproveRecovery.Size = new System.Drawing.Size(132, 16);
            this.chkApproveRecovery.TabIndex = 22;
            this.chkApproveRecovery.Text = "同意别人在我家休息";
            this.chkApproveRecovery.UseVisualStyleBackColor = true;
            // 
            // chkBiteOthers
            // 
            this.chkBiteOthers.AutoSize = true;
            this.chkBiteOthers.Location = new System.Drawing.Point(160, 20);
            this.chkBiteOthers.Name = "chkBiteOthers";
            this.chkBiteOthers.Size = new System.Drawing.Size(60, 16);
            this.chkBiteOthers.TabIndex = 20;
            this.chkBiteOthers.Text = "咬别人";
            this.chkBiteOthers.UseVisualStyleBackColor = true;
            // 
            // tabPageSlave
            // 
            this.tabPageSlave.Controls.Add(this.grpSlave);
            this.tabPageSlave.Location = new System.Drawing.Point(4, 21);
            this.tabPageSlave.Name = "tabPageSlave";
            this.tabPageSlave.Size = new System.Drawing.Size(618, 259);
            this.tabPageSlave.TabIndex = 6;
            this.tabPageSlave.Text = "朋友买卖";
            this.tabPageSlave.UseVisualStyleBackColor = true;
            // 
            // grpSlave
            // 
            this.grpSlave.Controls.Add(this.lblWarningBuySlave);
            this.grpSlave.Controls.Add(this.chkFawnMaster);
            this.grpSlave.Controls.Add(this.chkBuySlave);
            this.grpSlave.Controls.Add(this.chkBuyLowPriceSlave);
            this.grpSlave.Controls.Add(this.txtNickName);
            this.grpSlave.Controls.Add(this.chkPropitiateSlave);
            this.grpSlave.Controls.Add(this.lblNickName);
            this.grpSlave.Controls.Add(this.chkAfflictSlave);
            this.grpSlave.Controls.Add(this.lblMaxSlaves);
            this.grpSlave.Controls.Add(this.chkReleaseSlave);
            this.grpSlave.Controls.Add(this.cmbMaxSlaves);
            this.grpSlave.Location = new System.Drawing.Point(7, 3);
            this.grpSlave.Name = "grpSlave";
            this.grpSlave.Size = new System.Drawing.Size(584, 239);
            this.grpSlave.TabIndex = 48;
            this.grpSlave.TabStop = false;
            this.grpSlave.Text = "朋友买卖";
            // 
            // lblWarningBuySlave
            // 
            this.lblWarningBuySlave.AutoSize = true;
            this.lblWarningBuySlave.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningBuySlave.ForeColor = System.Drawing.Color.Red;
            this.lblWarningBuySlave.Location = new System.Drawing.Point(129, 61);
            this.lblWarningBuySlave.Name = "lblWarningBuySlave";
            this.lblWarningBuySlave.Size = new System.Drawing.Size(11, 12);
            this.lblWarningBuySlave.TabIndex = 44;
            this.lblWarningBuySlave.Text = "*";
            // 
            // chkFawnMaster
            // 
            this.chkFawnMaster.AutoSize = true;
            this.chkFawnMaster.Location = new System.Drawing.Point(14, 20);
            this.chkFawnMaster.Name = "chkFawnMaster";
            this.chkFawnMaster.Size = new System.Drawing.Size(72, 16);
            this.chkFawnMaster.TabIndex = 25;
            this.chkFawnMaster.Text = "讨好主人";
            this.chkFawnMaster.UseVisualStyleBackColor = true;
            // 
            // chkBuySlave
            // 
            this.chkBuySlave.AutoSize = true;
            this.chkBuySlave.Location = new System.Drawing.Point(14, 60);
            this.chkBuySlave.Name = "chkBuySlave";
            this.chkBuySlave.Size = new System.Drawing.Size(120, 16);
            this.chkBuySlave.TabIndex = 24;
            this.chkBuySlave.Text = "购买名单中的奴隶";
            this.chkBuySlave.UseVisualStyleBackColor = true;
            // 
            // chkBuyLowPriceSlave
            // 
            this.chkBuyLowPriceSlave.AutoSize = true;
            this.chkBuyLowPriceSlave.Location = new System.Drawing.Point(14, 40);
            this.chkBuyLowPriceSlave.Name = "chkBuyLowPriceSlave";
            this.chkBuyLowPriceSlave.Size = new System.Drawing.Size(96, 16);
            this.chkBuyLowPriceSlave.TabIndex = 23;
            this.chkBuyLowPriceSlave.Text = "购买低价奴隶";
            this.chkBuyLowPriceSlave.UseVisualStyleBackColor = true;
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(59, 105);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(148, 21);
            this.txtNickName.TabIndex = 16;
            // 
            // chkPropitiateSlave
            // 
            this.chkPropitiateSlave.AutoSize = true;
            this.chkPropitiateSlave.Location = new System.Drawing.Point(185, 20);
            this.chkPropitiateSlave.Name = "chkPropitiateSlave";
            this.chkPropitiateSlave.Size = new System.Drawing.Size(72, 16);
            this.chkPropitiateSlave.TabIndex = 14;
            this.chkPropitiateSlave.Text = "安抚奴隶";
            this.chkPropitiateSlave.UseVisualStyleBackColor = true;
            // 
            // lblNickName
            // 
            this.lblNickName.AutoSize = true;
            this.lblNickName.Location = new System.Drawing.Point(12, 105);
            this.lblNickName.Name = "lblNickName";
            this.lblNickName.Size = new System.Drawing.Size(41, 12);
            this.lblNickName.TabIndex = 15;
            this.lblNickName.Text = "昵称：";
            // 
            // chkAfflictSlave
            // 
            this.chkAfflictSlave.AutoSize = true;
            this.chkAfflictSlave.Location = new System.Drawing.Point(185, 40);
            this.chkAfflictSlave.Name = "chkAfflictSlave";
            this.chkAfflictSlave.Size = new System.Drawing.Size(60, 16);
            this.chkAfflictSlave.TabIndex = 15;
            this.chkAfflictSlave.Text = "整奴隶";
            this.chkAfflictSlave.UseVisualStyleBackColor = true;
            // 
            // lblMaxSlaves
            // 
            this.lblMaxSlaves.AutoSize = true;
            this.lblMaxSlaves.Location = new System.Drawing.Point(12, 85);
            this.lblMaxSlaves.Name = "lblMaxSlaves";
            this.lblMaxSlaves.Size = new System.Drawing.Size(77, 12);
            this.lblMaxSlaves.TabIndex = 1;
            this.lblMaxSlaves.Text = "奴隶数上限：";
            // 
            // chkReleaseSlave
            // 
            this.chkReleaseSlave.AutoSize = true;
            this.chkReleaseSlave.Location = new System.Drawing.Point(185, 60);
            this.chkReleaseSlave.Name = "chkReleaseSlave";
            this.chkReleaseSlave.Size = new System.Drawing.Size(72, 16);
            this.chkReleaseSlave.TabIndex = 16;
            this.chkReleaseSlave.Text = "释放奴隶";
            this.chkReleaseSlave.UseVisualStyleBackColor = true;
            // 
            // cmbMaxSlaves
            // 
            this.cmbMaxSlaves.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaxSlaves.FormattingEnabled = true;
            this.cmbMaxSlaves.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbMaxSlaves.Location = new System.Drawing.Point(92, 82);
            this.cmbMaxSlaves.Name = "cmbMaxSlaves";
            this.cmbMaxSlaves.Size = new System.Drawing.Size(80, 20);
            this.cmbMaxSlaves.TabIndex = 0;
            // 
            // tabPageHouse
            // 
            this.tabPageHouse.Controls.Add(this.grpHouse);
            this.tabPageHouse.Location = new System.Drawing.Point(4, 21);
            this.tabPageHouse.Name = "tabPageHouse";
            this.tabPageHouse.Size = new System.Drawing.Size(618, 259);
            this.tabPageHouse.TabIndex = 7;
            this.tabPageHouse.Text = "买房子";
            this.tabPageHouse.UseVisualStyleBackColor = true;
            // 
            // grpHouse
            // 
            this.grpHouse.Controls.Add(this.lblWarningRobFriends);
            this.grpHouse.Controls.Add(this.lblWarningStayHouse);
            this.grpHouse.Controls.Add(this.chkDriveFriends);
            this.grpHouse.Controls.Add(this.chkRobFreeFriends);
            this.grpHouse.Controls.Add(this.chkRobFriends);
            this.grpHouse.Controls.Add(this.chkStayHouse);
            this.grpHouse.Controls.Add(this.chkDoJob);
            this.grpHouse.Location = new System.Drawing.Point(7, 3);
            this.grpHouse.Name = "grpHouse";
            this.grpHouse.Size = new System.Drawing.Size(584, 236);
            this.grpHouse.TabIndex = 50;
            this.grpHouse.TabStop = false;
            this.grpHouse.Text = "买房子";
            // 
            // lblWarningRobFriends
            // 
            this.lblWarningRobFriends.AutoSize = true;
            this.lblWarningRobFriends.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningRobFriends.ForeColor = System.Drawing.Color.Red;
            this.lblWarningRobFriends.Location = new System.Drawing.Point(116, 61);
            this.lblWarningRobFriends.Name = "lblWarningRobFriends";
            this.lblWarningRobFriends.Size = new System.Drawing.Size(11, 12);
            this.lblWarningRobFriends.TabIndex = 46;
            this.lblWarningRobFriends.Text = "*";
            // 
            // lblWarningStayHouse
            // 
            this.lblWarningStayHouse.AutoSize = true;
            this.lblWarningStayHouse.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningStayHouse.ForeColor = System.Drawing.Color.Red;
            this.lblWarningStayHouse.Location = new System.Drawing.Point(67, 41);
            this.lblWarningStayHouse.Name = "lblWarningStayHouse";
            this.lblWarningStayHouse.Size = new System.Drawing.Size(11, 12);
            this.lblWarningStayHouse.TabIndex = 45;
            this.lblWarningStayHouse.Text = "*";
            // 
            // chkDriveFriends
            // 
            this.chkDriveFriends.AutoSize = true;
            this.chkDriveFriends.Location = new System.Drawing.Point(12, 104);
            this.chkDriveFriends.Name = "chkDriveFriends";
            this.chkDriveFriends.Size = new System.Drawing.Size(96, 16);
            this.chkDriveFriends.TabIndex = 35;
            this.chkDriveFriends.Text = "允许驱赶好友";
            this.chkDriveFriends.UseVisualStyleBackColor = true;
            // 
            // chkRobFreeFriends
            // 
            this.chkRobFreeFriends.AutoSize = true;
            this.chkRobFreeFriends.Location = new System.Drawing.Point(12, 82);
            this.chkRobFreeFriends.Name = "chkRobFreeFriends";
            this.chkRobFreeFriends.Size = new System.Drawing.Size(120, 16);
            this.chkRobFreeFriends.TabIndex = 34;
            this.chkRobFreeFriends.Text = "抢露宿街头的好友";
            this.chkRobFreeFriends.UseVisualStyleBackColor = true;
            // 
            // chkRobFriends
            // 
            this.chkRobFriends.AutoSize = true;
            this.chkRobFriends.Location = new System.Drawing.Point(12, 60);
            this.chkRobFriends.Name = "chkRobFriends";
            this.chkRobFriends.Size = new System.Drawing.Size(108, 16);
            this.chkRobFriends.TabIndex = 33;
            this.chkRobFriends.Text = "抢名单中的好友";
            this.chkRobFriends.UseVisualStyleBackColor = true;
            // 
            // chkStayHouse
            // 
            this.chkStayHouse.AutoSize = true;
            this.chkStayHouse.Location = new System.Drawing.Point(12, 40);
            this.chkStayHouse.Name = "chkStayHouse";
            this.chkStayHouse.Size = new System.Drawing.Size(60, 16);
            this.chkStayHouse.TabIndex = 32;
            this.chkStayHouse.Text = "住房子";
            this.chkStayHouse.UseVisualStyleBackColor = true;
            // 
            // chkDoJob
            // 
            this.chkDoJob.AutoSize = true;
            this.chkDoJob.Location = new System.Drawing.Point(12, 20);
            this.chkDoJob.Name = "chkDoJob";
            this.chkDoJob.Size = new System.Drawing.Size(48, 16);
            this.chkDoJob.TabIndex = 27;
            this.chkDoJob.Text = "打工";
            this.chkDoJob.UseVisualStyleBackColor = true;
            // 
            // tabPageGarden
            // 
            this.tabPageGarden.Controls.Add(this.grpGarden);
            this.tabPageGarden.Location = new System.Drawing.Point(4, 21);
            this.tabPageGarden.Name = "tabPageGarden";
            this.tabPageGarden.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGarden.Size = new System.Drawing.Size(618, 259);
            this.tabPageGarden.TabIndex = 1;
            this.tabPageGarden.Text = "花园";
            this.tabPageGarden.UseVisualStyleBackColor = true;
            // 
            // tabPageRanch
            // 
            this.tabPageRanch.Controls.Add(this.grpRanch);
            this.tabPageRanch.Location = new System.Drawing.Point(4, 21);
            this.tabPageRanch.Name = "tabPageRanch";
            this.tabPageRanch.Size = new System.Drawing.Size(618, 259);
            this.tabPageRanch.TabIndex = 2;
            this.tabPageRanch.Text = "牧场";
            this.tabPageRanch.UseVisualStyleBackColor = true;
            // 
            // tabPageFish
            // 
            this.tabPageFish.Controls.Add(this.grpFish);
            this.tabPageFish.Location = new System.Drawing.Point(4, 21);
            this.tabPageFish.Name = "tabPageFish";
            this.tabPageFish.Size = new System.Drawing.Size(618, 259);
            this.tabPageFish.TabIndex = 3;
            this.tabPageFish.Text = "钓鱼";
            this.tabPageFish.UseVisualStyleBackColor = true;
            // 
            // grpFish
            // 
            this.grpFish.Controls.Add(this.lblWarningFishing);
            this.grpFish.Controls.Add(this.lblWarningHelpFish);
            this.grpFish.Controls.Add(this.lblWarningPresentFish);
            this.grpFish.Controls.Add(this.panelBuyUpdateTackle);
            this.grpFish.Controls.Add(this.panelSellFish);
            this.grpFish.Controls.Add(this.chkBangKeJing);
            this.grpFish.Controls.Add(this.panelPresentFish);
            this.grpFish.Controls.Add(this.panelBuyFish);
            this.grpFish.Controls.Add(this.panelNetSelfFish);
            this.grpFish.Controls.Add(this.chkNetSelfFish);
            this.grpFish.Controls.Add(this.chkUpdateFishPond);
            this.grpFish.Controls.Add(this.chkBuyUpdateTackle);
            this.grpFish.Controls.Add(this.chkFishing);
            this.grpFish.Controls.Add(this.chkSellFish);
            this.grpFish.Controls.Add(this.chkPresentFish);
            this.grpFish.Controls.Add(this.chkTreatFish);
            this.grpFish.Controls.Add(this.chkHelpFish);
            this.grpFish.Controls.Add(this.chkHarvestFish);
            this.grpFish.Controls.Add(this.chkBuyFish);
            this.grpFish.Controls.Add(this.chkShake);
            this.grpFish.Location = new System.Drawing.Point(7, 3);
            this.grpFish.Name = "grpFish";
            this.grpFish.Size = new System.Drawing.Size(589, 237);
            this.grpFish.TabIndex = 52;
            this.grpFish.TabStop = false;
            this.grpFish.Text = "钓鱼";
            // 
            // lblWarningFishing
            // 
            this.lblWarningFishing.AutoSize = true;
            this.lblWarningFishing.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningFishing.ForeColor = System.Drawing.Color.Red;
            this.lblWarningFishing.Location = new System.Drawing.Point(298, 81);
            this.lblWarningFishing.Name = "lblWarningFishing";
            this.lblWarningFishing.Size = new System.Drawing.Size(11, 12);
            this.lblWarningFishing.TabIndex = 88;
            this.lblWarningFishing.Text = "*";
            // 
            // lblWarningHelpFish
            // 
            this.lblWarningHelpFish.AutoSize = true;
            this.lblWarningHelpFish.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningHelpFish.ForeColor = System.Drawing.Color.Red;
            this.lblWarningHelpFish.Location = new System.Drawing.Point(297, 121);
            this.lblWarningHelpFish.Name = "lblWarningHelpFish";
            this.lblWarningHelpFish.Size = new System.Drawing.Size(11, 12);
            this.lblWarningHelpFish.TabIndex = 87;
            this.lblWarningHelpFish.Text = "*";
            // 
            // lblWarningPresentFish
            // 
            this.lblWarningPresentFish.AutoSize = true;
            this.lblWarningPresentFish.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningPresentFish.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPresentFish.Location = new System.Drawing.Point(286, 141);
            this.lblWarningPresentFish.Name = "lblWarningPresentFish";
            this.lblWarningPresentFish.Size = new System.Drawing.Size(11, 12);
            this.lblWarningPresentFish.TabIndex = 86;
            this.lblWarningPresentFish.Text = "*";
            // 
            // panelBuyUpdateTackle
            // 
            this.panelBuyUpdateTackle.Controls.Add(this.lblMaxTackles);
            this.panelBuyUpdateTackle.Controls.Add(this.cmbMaxTackles);
            this.panelBuyUpdateTackle.Location = new System.Drawing.Point(240, 35);
            this.panelBuyUpdateTackle.Name = "panelBuyUpdateTackle";
            this.panelBuyUpdateTackle.Size = new System.Drawing.Size(136, 25);
            this.panelBuyUpdateTackle.TabIndex = 85;
            // 
            // lblMaxTackles
            // 
            this.lblMaxTackles.AutoSize = true;
            this.lblMaxTackles.Location = new System.Drawing.Point(2, 5);
            this.lblMaxTackles.Name = "lblMaxTackles";
            this.lblMaxTackles.Size = new System.Drawing.Size(77, 12);
            this.lblMaxTackles.TabIndex = 83;
            this.lblMaxTackles.Text = "鱼竿数上限：";
            // 
            // cmbMaxTackles
            // 
            this.cmbMaxTackles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaxTackles.FormattingEnabled = true;
            this.cmbMaxTackles.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbMaxTackles.Location = new System.Drawing.Point(81, 2);
            this.cmbMaxTackles.Name = "cmbMaxTackles";
            this.cmbMaxTackles.Size = new System.Drawing.Size(48, 20);
            this.cmbMaxTackles.TabIndex = 82;
            // 
            // panelSellFish
            // 
            this.panelSellFish.Controls.Add(this.panelSellFishForbidden);
            this.panelSellFish.Controls.Add(this.lblSellFishLowCash);
            this.panelSellFish.Controls.Add(this.txtSellFishLowCashLimit);
            this.panelSellFish.Controls.Add(this.chkSellFishLowCash);
            this.panelSellFish.Controls.Add(this.rdbSellAllFish);
            this.panelSellFish.Controls.Add(this.rdbForbiddenFish);
            this.panelSellFish.Location = new System.Drawing.Point(382, 36);
            this.panelSellFish.Name = "panelSellFish";
            this.panelSellFish.Size = new System.Drawing.Size(207, 137);
            this.panelSellFish.TabIndex = 64;
            // 
            // panelSellFishForbidden
            // 
            this.panelSellFishForbidden.Controls.Add(this.lblSellFishMaxLimit2);
            this.panelSellFishForbidden.Controls.Add(this.lblSellFishCheckValue);
            this.panelSellFishForbidden.Controls.Add(this.txtSellFishMaxLimit);
            this.panelSellFishForbidden.Controls.Add(this.btnSellFishForbiddenList);
            this.panelSellFishForbidden.Controls.Add(this.lblSellFishMaxLimit);
            this.panelSellFishForbidden.Controls.Add(this.chkSellFishCheckValue);
            this.panelSellFishForbidden.Controls.Add(this.txtSellFishValue);
            this.panelSellFishForbidden.Location = new System.Drawing.Point(7, 59);
            this.panelSellFishForbidden.Name = "panelSellFishForbidden";
            this.panelSellFishForbidden.Size = new System.Drawing.Size(200, 73);
            this.panelSellFishForbidden.TabIndex = 61;
            // 
            // lblSellFishMaxLimit2
            // 
            this.lblSellFishMaxLimit2.AutoSize = true;
            this.lblSellFishMaxLimit2.Location = new System.Drawing.Point(97, 52);
            this.lblSellFishMaxLimit2.Name = "lblSellFishMaxLimit2";
            this.lblSellFishMaxLimit2.Size = new System.Drawing.Size(17, 12);
            this.lblSellFishMaxLimit2.TabIndex = 62;
            this.lblSellFishMaxLimit2.Text = "万";
            // 
            // lblSellFishCheckValue
            // 
            this.lblSellFishCheckValue.AutoSize = true;
            this.lblSellFishCheckValue.Location = new System.Drawing.Point(159, 28);
            this.lblSellFishCheckValue.Name = "lblSellFishCheckValue";
            this.lblSellFishCheckValue.Size = new System.Drawing.Size(41, 12);
            this.lblSellFishCheckValue.TabIndex = 76;
            this.lblSellFishCheckValue.Text = "元的鱼";
            // 
            // txtSellFishMaxLimit
            // 
            this.txtSellFishMaxLimit.Location = new System.Drawing.Point(56, 46);
            this.txtSellFishMaxLimit.Name = "txtSellFishMaxLimit";
            this.txtSellFishMaxLimit.Size = new System.Drawing.Size(35, 21);
            this.txtSellFishMaxLimit.TabIndex = 61;
            // 
            // btnSellFishForbiddenList
            // 
            this.btnSellFishForbiddenList.Location = new System.Drawing.Point(5, 1);
            this.btnSellFishForbiddenList.Name = "btnSellFishForbiddenList";
            this.btnSellFishForbiddenList.Size = new System.Drawing.Size(124, 23);
            this.btnSellFishForbiddenList.TabIndex = 61;
            this.btnSellFishForbiddenList.Text = "设定禁止出售的鱼";
            this.btnSellFishForbiddenList.UseVisualStyleBackColor = true;
            this.btnSellFishForbiddenList.Click += new System.EventHandler(this.btnSellFishForbiddenList_Click);
            // 
            // lblSellFishMaxLimit
            // 
            this.lblSellFishMaxLimit.AutoSize = true;
            this.lblSellFishMaxLimit.Location = new System.Drawing.Point(10, 49);
            this.lblSellFishMaxLimit.Name = "lblSellFishMaxLimit";
            this.lblSellFishMaxLimit.Size = new System.Drawing.Size(41, 12);
            this.lblSellFishMaxLimit.TabIndex = 60;
            this.lblSellFishMaxLimit.Text = "额度：";
            // 
            // chkSellFishCheckValue
            // 
            this.chkSellFishCheckValue.AutoSize = true;
            this.chkSellFishCheckValue.Location = new System.Drawing.Point(12, 27);
            this.chkSellFishCheckValue.Name = "chkSellFishCheckValue";
            this.chkSellFishCheckValue.Size = new System.Drawing.Size(90, 16);
            this.chkSellFishCheckValue.TabIndex = 74;
            this.chkSellFishCheckValue.Text = "只出售价值<";
            this.chkSellFishCheckValue.UseVisualStyleBackColor = true;
            // 
            // txtSellFishValue
            // 
            this.txtSellFishValue.Location = new System.Drawing.Point(103, 24);
            this.txtSellFishValue.Name = "txtSellFishValue";
            this.txtSellFishValue.Size = new System.Drawing.Size(53, 21);
            this.txtSellFishValue.TabIndex = 75;
            // 
            // lblSellFishLowCash
            // 
            this.lblSellFishLowCash.AutoSize = true;
            this.lblSellFishLowCash.Location = new System.Drawing.Point(110, 7);
            this.lblSellFishLowCash.Name = "lblSellFishLowCash";
            this.lblSellFishLowCash.Size = new System.Drawing.Size(53, 12);
            this.lblSellFishLowCash.TabIndex = 65;
            this.lblSellFishLowCash.Text = "万时执行";
            // 
            // txtSellFishLowCashLimit
            // 
            this.txtSellFishLowCashLimit.Location = new System.Drawing.Point(75, 3);
            this.txtSellFishLowCashLimit.Name = "txtSellFishLowCashLimit";
            this.txtSellFishLowCashLimit.Size = new System.Drawing.Size(33, 21);
            this.txtSellFishLowCashLimit.TabIndex = 64;
            // 
            // chkSellFishLowCash
            // 
            this.chkSellFishLowCash.AutoSize = true;
            this.chkSellFishLowCash.Location = new System.Drawing.Point(5, 6);
            this.chkSellFishLowCash.Name = "chkSellFishLowCash";
            this.chkSellFishLowCash.Size = new System.Drawing.Size(72, 16);
            this.chkSellFishLowCash.TabIndex = 63;
            this.chkSellFishLowCash.Text = "现金不足";
            this.chkSellFishLowCash.UseVisualStyleBackColor = true;
            // 
            // rdbSellAllFish
            // 
            this.rdbSellAllFish.AutoSize = true;
            this.rdbSellAllFish.Location = new System.Drawing.Point(5, 26);
            this.rdbSellAllFish.Name = "rdbSellAllFish";
            this.rdbSellAllFish.Size = new System.Drawing.Size(131, 16);
            this.rdbSellAllFish.TabIndex = 51;
            this.rdbSellAllFish.Text = "出售所有仓库中的鱼";
            this.rdbSellAllFish.UseVisualStyleBackColor = true;
            this.rdbSellAllFish.CheckedChanged += new System.EventHandler(this.rdbSellAllFish_CheckedChanged);
            // 
            // rdbForbiddenFish
            // 
            this.rdbForbiddenFish.AutoSize = true;
            this.rdbForbiddenFish.Checked = true;
            this.rdbForbiddenFish.Location = new System.Drawing.Point(5, 42);
            this.rdbForbiddenFish.Name = "rdbForbiddenFish";
            this.rdbForbiddenFish.Size = new System.Drawing.Size(59, 16);
            this.rdbForbiddenFish.TabIndex = 59;
            this.rdbForbiddenFish.TabStop = true;
            this.rdbForbiddenFish.Text = "指定鱼";
            this.rdbForbiddenFish.UseVisualStyleBackColor = true;
            // 
            // chkBangKeJing
            // 
            this.chkBangKeJing.AutoSize = true;
            this.chkBangKeJing.Location = new System.Drawing.Point(230, 100);
            this.chkBangKeJing.Name = "chkBangKeJing";
            this.chkBangKeJing.Size = new System.Drawing.Size(108, 16);
            this.chkBangKeJing.TabIndex = 63;
            this.chkBangKeJing.Text = "给蚌壳精输真气";
            this.chkBangKeJing.UseVisualStyleBackColor = true;
            // 
            // panelPresentFish
            // 
            this.panelPresentFish.Controls.Add(this.rdbPresentFishCheap);
            this.panelPresentFish.Controls.Add(this.rdbPresentFishExpensive);
            this.panelPresentFish.Controls.Add(this.lblPresentFishCheap);
            this.panelPresentFish.Controls.Add(this.chkPresentFishCheckValue);
            this.panelPresentFish.Controls.Add(this.btnPresentFishForbiddenList);
            this.panelPresentFish.Controls.Add(this.lblPresentFishValue);
            this.panelPresentFish.Controls.Add(this.txtPresentFishValue);
            this.panelPresentFish.Location = new System.Drawing.Point(232, 159);
            this.panelPresentFish.Name = "panelPresentFish";
            this.panelPresentFish.Size = new System.Drawing.Size(207, 73);
            this.panelPresentFish.TabIndex = 62;
            // 
            // rdbPresentFishCheap
            // 
            this.rdbPresentFishCheap.AutoSize = true;
            this.rdbPresentFishCheap.Location = new System.Drawing.Point(72, 28);
            this.rdbPresentFishCheap.Name = "rdbPresentFishCheap";
            this.rdbPresentFishCheap.Size = new System.Drawing.Size(71, 16);
            this.rdbPresentFishCheap.TabIndex = 76;
            this.rdbPresentFishCheap.TabStop = true;
            this.rdbPresentFishCheap.Text = "最便宜的";
            this.rdbPresentFishCheap.UseVisualStyleBackColor = true;
            // 
            // rdbPresentFishExpensive
            // 
            this.rdbPresentFishExpensive.AutoSize = true;
            this.rdbPresentFishExpensive.Location = new System.Drawing.Point(145, 29);
            this.rdbPresentFishExpensive.Name = "rdbPresentFishExpensive";
            this.rdbPresentFishExpensive.Size = new System.Drawing.Size(59, 16);
            this.rdbPresentFishExpensive.TabIndex = 75;
            this.rdbPresentFishExpensive.TabStop = true;
            this.rdbPresentFishExpensive.Text = "最贵的";
            this.rdbPresentFishExpensive.UseVisualStyleBackColor = true;
            // 
            // lblPresentFishCheap
            // 
            this.lblPresentFishCheap.AutoSize = true;
            this.lblPresentFishCheap.Location = new System.Drawing.Point(10, 31);
            this.lblPresentFishCheap.Name = "lblPresentFishCheap";
            this.lblPresentFishCheap.Size = new System.Drawing.Size(65, 12);
            this.lblPresentFishCheap.TabIndex = 74;
            this.lblPresentFishCheap.Text = "赠送顺序：";
            // 
            // chkPresentFishCheckValue
            // 
            this.chkPresentFishCheckValue.AutoSize = true;
            this.chkPresentFishCheckValue.Location = new System.Drawing.Point(12, 50);
            this.chkPresentFishCheckValue.Name = "chkPresentFishCheckValue";
            this.chkPresentFishCheckValue.Size = new System.Drawing.Size(84, 16);
            this.chkPresentFishCheckValue.TabIndex = 73;
            this.chkPresentFishCheckValue.Text = "只送价值>=";
            this.chkPresentFishCheckValue.UseVisualStyleBackColor = true;
            // 
            // btnPresentFishForbiddenList
            // 
            this.btnPresentFishForbiddenList.Location = new System.Drawing.Point(5, 3);
            this.btnPresentFishForbiddenList.Name = "btnPresentFishForbiddenList";
            this.btnPresentFishForbiddenList.Size = new System.Drawing.Size(132, 23);
            this.btnPresentFishForbiddenList.TabIndex = 63;
            this.btnPresentFishForbiddenList.Text = "设定禁止赠送的鱼";
            this.btnPresentFishForbiddenList.UseVisualStyleBackColor = true;
            this.btnPresentFishForbiddenList.Click += new System.EventHandler(this.btnPresentFishForbiddenList_Click);
            // 
            // lblPresentFishValue
            // 
            this.lblPresentFishValue.AutoSize = true;
            this.lblPresentFishValue.Location = new System.Drawing.Point(160, 50);
            this.lblPresentFishValue.Name = "lblPresentFishValue";
            this.lblPresentFishValue.Size = new System.Drawing.Size(41, 12);
            this.lblPresentFishValue.TabIndex = 73;
            this.lblPresentFishValue.Text = "元的鱼";
            // 
            // txtPresentFishValue
            // 
            this.txtPresentFishValue.Location = new System.Drawing.Point(96, 48);
            this.txtPresentFishValue.Name = "txtPresentFishValue";
            this.txtPresentFishValue.Size = new System.Drawing.Size(62, 21);
            this.txtPresentFishValue.TabIndex = 70;
            // 
            // panelBuyFish
            // 
            this.panelBuyFish.Controls.Add(this.cmbMaxFishes);
            this.panelBuyFish.Controls.Add(this.lblMaxFishes);
            this.panelBuyFish.Controls.Add(this.rdbBuyFishByRank);
            this.panelBuyFish.Controls.Add(this.cmbBuyFishFishId);
            this.panelBuyFish.Controls.Add(this.rdbBuyFishCustom);
            this.panelBuyFish.Location = new System.Drawing.Point(16, 161);
            this.panelBuyFish.Name = "panelBuyFish";
            this.panelBuyFish.Size = new System.Drawing.Size(180, 70);
            this.panelBuyFish.TabIndex = 61;
            // 
            // cmbMaxFishes
            // 
            this.cmbMaxFishes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaxFishes.FormattingEnabled = true;
            this.cmbMaxFishes.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmbMaxFishes.Location = new System.Drawing.Point(71, 45);
            this.cmbMaxFishes.Name = "cmbMaxFishes";
            this.cmbMaxFishes.Size = new System.Drawing.Size(48, 20);
            this.cmbMaxFishes.TabIndex = 85;
            // 
            // lblMaxFishes
            // 
            this.lblMaxFishes.AutoSize = true;
            this.lblMaxFishes.Location = new System.Drawing.Point(4, 48);
            this.lblMaxFishes.Name = "lblMaxFishes";
            this.lblMaxFishes.Size = new System.Drawing.Size(65, 12);
            this.lblMaxFishes.TabIndex = 84;
            this.lblMaxFishes.Text = "鱼数上限：";
            // 
            // rdbBuyFishByRank
            // 
            this.rdbBuyFishByRank.AutoSize = true;
            this.rdbBuyFishByRank.Checked = true;
            this.rdbBuyFishByRank.Location = new System.Drawing.Point(3, 1);
            this.rdbBuyFishByRank.Name = "rdbBuyFishByRank";
            this.rdbBuyFishByRank.Size = new System.Drawing.Size(143, 16);
            this.rdbBuyFishByRank.TabIndex = 0;
            this.rdbBuyFishByRank.TabStop = true;
            this.rdbBuyFishByRank.Text = "等级所允许买的最贵的";
            this.rdbBuyFishByRank.UseVisualStyleBackColor = true;
            this.rdbBuyFishByRank.CheckedChanged += new System.EventHandler(this.rdbBuyFishByRank_CheckedChanged);
            // 
            // cmbBuyFishFishId
            // 
            this.cmbBuyFishFishId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuyFishFishId.FormattingEnabled = true;
            this.cmbBuyFishFishId.Location = new System.Drawing.Point(62, 22);
            this.cmbBuyFishFishId.Name = "cmbBuyFishFishId";
            this.cmbBuyFishFishId.Size = new System.Drawing.Size(103, 20);
            this.cmbBuyFishFishId.TabIndex = 45;
            // 
            // rdbBuyFishCustom
            // 
            this.rdbBuyFishCustom.AutoSize = true;
            this.rdbBuyFishCustom.Location = new System.Drawing.Point(3, 23);
            this.rdbBuyFishCustom.Name = "rdbBuyFishCustom";
            this.rdbBuyFishCustom.Size = new System.Drawing.Size(59, 16);
            this.rdbBuyFishCustom.TabIndex = 1;
            this.rdbBuyFishCustom.Text = "自定义";
            this.rdbBuyFishCustom.UseVisualStyleBackColor = true;
            // 
            // panelNetSelfFish
            // 
            this.panelNetSelfFish.Controls.Add(this.lblNetSelfFishMaturePercentage);
            this.panelNetSelfFish.Controls.Add(this.lblNetSelfFishCheap);
            this.panelNetSelfFish.Controls.Add(this.lblNetSelfFishMature);
            this.panelNetSelfFish.Controls.Add(this.rdbNetSelfFishExpensive);
            this.panelNetSelfFish.Controls.Add(this.txtNetSelfFishMature);
            this.panelNetSelfFish.Controls.Add(this.rdbNetSelfFishCheap);
            this.panelNetSelfFish.Location = new System.Drawing.Point(12, 95);
            this.panelNetSelfFish.Name = "panelNetSelfFish";
            this.panelNetSelfFish.Size = new System.Drawing.Size(202, 46);
            this.panelNetSelfFish.TabIndex = 60;
            // 
            // lblNetSelfFishMaturePercentage
            // 
            this.lblNetSelfFishMaturePercentage.AutoSize = true;
            this.lblNetSelfFishMaturePercentage.Location = new System.Drawing.Point(101, 22);
            this.lblNetSelfFishMaturePercentage.Name = "lblNetSelfFishMaturePercentage";
            this.lblNetSelfFishMaturePercentage.Size = new System.Drawing.Size(11, 12);
            this.lblNetSelfFishMaturePercentage.TabIndex = 73;
            this.lblNetSelfFishMaturePercentage.Text = "%";
            // 
            // lblNetSelfFishCheap
            // 
            this.lblNetSelfFishCheap.AutoSize = true;
            this.lblNetSelfFishCheap.Location = new System.Drawing.Point(8, 6);
            this.lblNetSelfFishCheap.Name = "lblNetSelfFishCheap";
            this.lblNetSelfFishCheap.Size = new System.Drawing.Size(65, 12);
            this.lblNetSelfFishCheap.TabIndex = 61;
            this.lblNetSelfFishCheap.Text = "收鱼顺序：";
            // 
            // lblNetSelfFishMature
            // 
            this.lblNetSelfFishMature.AutoSize = true;
            this.lblNetSelfFishMature.Location = new System.Drawing.Point(8, 23);
            this.lblNetSelfFishMature.Name = "lblNetSelfFishMature";
            this.lblNetSelfFishMature.Size = new System.Drawing.Size(53, 12);
            this.lblNetSelfFishMature.TabIndex = 72;
            this.lblNetSelfFishMature.Text = "成熟度：";
            // 
            // rdbNetSelfFishExpensive
            // 
            this.rdbNetSelfFishExpensive.AutoSize = true;
            this.rdbNetSelfFishExpensive.Location = new System.Drawing.Point(147, 3);
            this.rdbNetSelfFishExpensive.Name = "rdbNetSelfFishExpensive";
            this.rdbNetSelfFishExpensive.Size = new System.Drawing.Size(59, 16);
            this.rdbNetSelfFishExpensive.TabIndex = 1;
            this.rdbNetSelfFishExpensive.TabStop = true;
            this.rdbNetSelfFishExpensive.Text = "最贵的";
            this.rdbNetSelfFishExpensive.UseVisualStyleBackColor = true;
            // 
            // txtNetSelfFishMature
            // 
            this.txtNetSelfFishMature.Location = new System.Drawing.Point(67, 20);
            this.txtNetSelfFishMature.Name = "txtNetSelfFishMature";
            this.txtNetSelfFishMature.Size = new System.Drawing.Size(28, 21);
            this.txtNetSelfFishMature.TabIndex = 70;
            // 
            // rdbNetSelfFishCheap
            // 
            this.rdbNetSelfFishCheap.AutoSize = true;
            this.rdbNetSelfFishCheap.Location = new System.Drawing.Point(75, 3);
            this.rdbNetSelfFishCheap.Name = "rdbNetSelfFishCheap";
            this.rdbNetSelfFishCheap.Size = new System.Drawing.Size(71, 16);
            this.rdbNetSelfFishCheap.TabIndex = 0;
            this.rdbNetSelfFishCheap.TabStop = true;
            this.rdbNetSelfFishCheap.Text = "最便宜的";
            this.rdbNetSelfFishCheap.UseVisualStyleBackColor = true;
            // 
            // chkNetSelfFish
            // 
            this.chkNetSelfFish.AutoSize = true;
            this.chkNetSelfFish.Location = new System.Drawing.Point(12, 80);
            this.chkNetSelfFish.Name = "chkNetSelfFish";
            this.chkNetSelfFish.Size = new System.Drawing.Size(72, 16);
            this.chkNetSelfFish.TabIndex = 59;
            this.chkNetSelfFish.Text = "自家收鱼";
            this.chkNetSelfFish.UseVisualStyleBackColor = true;
            this.chkNetSelfFish.CheckedChanged += new System.EventHandler(this.chkNetSelfFish_CheckedChanged);
            // 
            // chkUpdateFishPond
            // 
            this.chkUpdateFishPond.AutoSize = true;
            this.chkUpdateFishPond.Location = new System.Drawing.Point(12, 60);
            this.chkUpdateFishPond.Name = "chkUpdateFishPond";
            this.chkUpdateFishPond.Size = new System.Drawing.Size(72, 16);
            this.chkUpdateFishPond.TabIndex = 58;
            this.chkUpdateFishPond.Text = "扩容鱼塘";
            this.chkUpdateFishPond.UseVisualStyleBackColor = true;
            // 
            // chkBuyUpdateTackle
            // 
            this.chkBuyUpdateTackle.AutoSize = true;
            this.chkBuyUpdateTackle.Location = new System.Drawing.Point(230, 20);
            this.chkBuyUpdateTackle.Name = "chkBuyUpdateTackle";
            this.chkBuyUpdateTackle.Size = new System.Drawing.Size(102, 16);
            this.chkBuyUpdateTackle.TabIndex = 57;
            this.chkBuyUpdateTackle.Text = "购买/升级鱼竿";
            this.chkBuyUpdateTackle.UseVisualStyleBackColor = true;
            this.chkBuyUpdateTackle.CheckedChanged += new System.EventHandler(this.chkBuyUpdateTackle_CheckedChanged);
            // 
            // chkFishing
            // 
            this.chkFishing.AutoSize = true;
            this.chkFishing.Location = new System.Drawing.Point(230, 80);
            this.chkFishing.Name = "chkFishing";
            this.chkFishing.Size = new System.Drawing.Size(72, 16);
            this.chkFishing.TabIndex = 53;
            this.chkFishing.Text = "下杆钓鱼";
            this.chkFishing.UseVisualStyleBackColor = true;
            // 
            // chkSellFish
            // 
            this.chkSellFish.AutoSize = true;
            this.chkSellFish.Location = new System.Drawing.Point(376, 18);
            this.chkSellFish.Name = "chkSellFish";
            this.chkSellFish.Size = new System.Drawing.Size(60, 16);
            this.chkSellFish.TabIndex = 52;
            this.chkSellFish.Text = "出售鱼";
            this.chkSellFish.UseVisualStyleBackColor = true;
            this.chkSellFish.CheckedChanged += new System.EventHandler(this.chkSellFish_CheckedChanged);
            // 
            // chkPresentFish
            // 
            this.chkPresentFish.AutoSize = true;
            this.chkPresentFish.Location = new System.Drawing.Point(230, 140);
            this.chkPresentFish.Name = "chkPresentFish";
            this.chkPresentFish.Size = new System.Drawing.Size(60, 16);
            this.chkPresentFish.TabIndex = 51;
            this.chkPresentFish.Text = "赠送鱼";
            this.chkPresentFish.UseVisualStyleBackColor = true;
            this.chkPresentFish.CheckedChanged += new System.EventHandler(this.chkPresentFish_CheckedChanged);
            // 
            // chkTreatFish
            // 
            this.chkTreatFish.AutoSize = true;
            this.chkTreatFish.Location = new System.Drawing.Point(12, 40);
            this.chkTreatFish.Name = "chkTreatFish";
            this.chkTreatFish.Size = new System.Drawing.Size(48, 16);
            this.chkTreatFish.TabIndex = 38;
            this.chkTreatFish.Text = "治病";
            this.chkTreatFish.UseVisualStyleBackColor = true;
            // 
            // chkHelpFish
            // 
            this.chkHelpFish.AutoSize = true;
            this.chkHelpFish.Location = new System.Drawing.Point(230, 120);
            this.chkHelpFish.Name = "chkHelpFish";
            this.chkHelpFish.Size = new System.Drawing.Size(72, 16);
            this.chkHelpFish.TabIndex = 34;
            this.chkHelpFish.Text = "帮忙收鱼";
            this.chkHelpFish.UseVisualStyleBackColor = true;
            // 
            // chkHarvestFish
            // 
            this.chkHarvestFish.AutoSize = true;
            this.chkHarvestFish.Location = new System.Drawing.Point(230, 60);
            this.chkHarvestFish.Name = "chkHarvestFish";
            this.chkHarvestFish.Size = new System.Drawing.Size(72, 16);
            this.chkHarvestFish.TabIndex = 32;
            this.chkHarvestFish.Text = "拉杆收鱼";
            this.chkHarvestFish.UseVisualStyleBackColor = true;
            // 
            // chkBuyFish
            // 
            this.chkBuyFish.AutoSize = true;
            this.chkBuyFish.Location = new System.Drawing.Point(12, 143);
            this.chkBuyFish.Name = "chkBuyFish";
            this.chkBuyFish.Size = new System.Drawing.Size(72, 16);
            this.chkBuyFish.TabIndex = 30;
            this.chkBuyFish.Text = "购买鱼苗";
            this.chkBuyFish.UseVisualStyleBackColor = true;
            this.chkBuyFish.CheckedChanged += new System.EventHandler(this.chkBuyFish_CheckedChanged);
            // 
            // chkShake
            // 
            this.chkShake.AutoSize = true;
            this.chkShake.Location = new System.Drawing.Point(12, 20);
            this.chkShake.Name = "chkShake";
            this.chkShake.Size = new System.Drawing.Size(48, 16);
            this.chkShake.TabIndex = 28;
            this.chkShake.Text = "转盘";
            this.chkShake.UseVisualStyleBackColor = true;
            // 
            // tabPageRich
            // 
            this.tabPageRich.Controls.Add(this.grpRich);
            this.tabPageRich.Location = new System.Drawing.Point(4, 21);
            this.tabPageRich.Name = "tabPageRich";
            this.tabPageRich.Size = new System.Drawing.Size(618, 259);
            this.tabPageRich.TabIndex = 4;
            this.tabPageRich.Text = "超级大亨";
            this.tabPageRich.UseVisualStyleBackColor = true;
            // 
            // grpRich
            // 
            this.grpRich.Controls.Add(this.lblRich);
            this.grpRich.Controls.Add(this.panelBuyAsset);
            this.grpRich.Controls.Add(this.chkBuyAsset);
            this.grpRich.Controls.Add(this.chkSellAsset);
            this.grpRich.Location = new System.Drawing.Point(7, 3);
            this.grpRich.Name = "grpRich";
            this.grpRich.Size = new System.Drawing.Size(584, 237);
            this.grpRich.TabIndex = 53;
            this.grpRich.TabStop = false;
            this.grpRich.Text = "超级大亨";
            // 
            // lblRich
            // 
            this.lblRich.AutoSize = true;
            this.lblRich.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblRich.ForeColor = System.Drawing.Color.Red;
            this.lblRich.Location = new System.Drawing.Point(107, 20);
            this.lblRich.Name = "lblRich";
            this.lblRich.Size = new System.Drawing.Size(413, 12);
            this.lblRich.TabIndex = 64;
            this.lblRich.Text = "*请在工具->更新数据->超级大亨->推荐买卖率表中， 配置购买率和出售率。";
            // 
            // panelBuyAsset
            // 
            this.panelBuyAsset.Controls.Add(this.lblIfMinimum);
            this.panelBuyAsset.Controls.Add(this.lblGiveUpMinimum);
            this.panelBuyAsset.Controls.Add(this.txtGiveUpMinimum);
            this.panelBuyAsset.Controls.Add(this.chkGiveUpIfMinimum);
            this.panelBuyAsset.Controls.Add(this.lblAdvancedPurchase);
            this.panelBuyAsset.Controls.Add(this.chkAdvancedPurchase);
            this.panelBuyAsset.Controls.Add(this.lblGiveUpAssetCount);
            this.panelBuyAsset.Controls.Add(this.txtGiveUpAssetCount);
            this.panelBuyAsset.Controls.Add(this.chkGiveUpIfMyAsset);
            this.panelBuyAsset.Controls.Add(this.btnSetBuyAssetsList);
            this.panelBuyAsset.Controls.Add(this.lblGiveUpRatio);
            this.panelBuyAsset.Controls.Add(this.txtGiveUpRatio);
            this.panelBuyAsset.Controls.Add(this.chkGiveUpIfRatio);
            this.panelBuyAsset.Controls.Add(this.panelBuyAssetCheap);
            this.panelBuyAsset.Controls.Add(this.lblBuyAssetCheap);
            this.panelBuyAsset.Location = new System.Drawing.Point(12, 58);
            this.panelBuyAsset.Name = "panelBuyAsset";
            this.panelBuyAsset.Size = new System.Drawing.Size(502, 154);
            this.panelBuyAsset.TabIndex = 55;
            // 
            // lblIfMinimum
            // 
            this.lblIfMinimum.AutoSize = true;
            this.lblIfMinimum.ForeColor = System.Drawing.Color.Red;
            this.lblIfMinimum.Location = new System.Drawing.Point(267, 85);
            this.lblIfMinimum.Name = "lblIfMinimum";
            this.lblIfMinimum.Size = new System.Drawing.Size(143, 12);
            this.lblIfMinimum.TabIndex = 67;
            this.lblIfMinimum.Text = "*仅第二次及以后购买时。";
            // 
            // lblGiveUpMinimum
            // 
            this.lblGiveUpMinimum.AutoSize = true;
            this.lblGiveUpMinimum.Location = new System.Drawing.Point(178, 85);
            this.lblGiveUpMinimum.Name = "lblGiveUpMinimum";
            this.lblGiveUpMinimum.Size = new System.Drawing.Size(77, 12);
            this.lblGiveUpMinimum.TabIndex = 66;
            this.lblGiveUpMinimum.Text = "时，放弃购买";
            // 
            // txtGiveUpMinimum
            // 
            this.txtGiveUpMinimum.Location = new System.Drawing.Point(136, 82);
            this.txtGiveUpMinimum.Name = "txtGiveUpMinimum";
            this.txtGiveUpMinimum.Size = new System.Drawing.Size(40, 21);
            this.txtGiveUpMinimum.TabIndex = 65;
            // 
            // chkGiveUpIfMinimum
            // 
            this.chkGiveUpIfMinimum.AutoSize = true;
            this.chkGiveUpIfMinimum.Location = new System.Drawing.Point(18, 84);
            this.chkGiveUpIfMinimum.Name = "chkGiveUpIfMinimum";
            this.chkGiveUpIfMinimum.Size = new System.Drawing.Size(114, 16);
            this.chkGiveUpIfMinimum.TabIndex = 64;
            this.chkGiveUpIfMinimum.Text = "尝试购买数量 <=";
            this.chkGiveUpIfMinimum.UseVisualStyleBackColor = true;
            // 
            // lblAdvancedPurchase
            // 
            this.lblAdvancedPurchase.AutoSize = true;
            this.lblAdvancedPurchase.ForeColor = System.Drawing.Color.Red;
            this.lblAdvancedPurchase.Location = new System.Drawing.Point(156, 129);
            this.lblAdvancedPurchase.Name = "lblAdvancedPurchase";
            this.lblAdvancedPurchase.Size = new System.Drawing.Size(335, 12);
            this.lblAdvancedPurchase.TabIndex = 63;
            this.lblAdvancedPurchase.Text = "*请在工具->更新数据->超级大亨->高级购买数量控制中配置。";
            // 
            // chkAdvancedPurchase
            // 
            this.chkAdvancedPurchase.AutoSize = true;
            this.chkAdvancedPurchase.Location = new System.Drawing.Point(18, 128);
            this.chkAdvancedPurchase.Name = "chkAdvancedPurchase";
            this.chkAdvancedPurchase.Size = new System.Drawing.Size(120, 16);
            this.chkAdvancedPurchase.TabIndex = 62;
            this.chkAdvancedPurchase.Text = "高级购买数量控制";
            this.chkAdvancedPurchase.UseVisualStyleBackColor = true;
            // 
            // lblGiveUpAssetCount
            // 
            this.lblGiveUpAssetCount.AutoSize = true;
            this.lblGiveUpAssetCount.Location = new System.Drawing.Point(178, 107);
            this.lblGiveUpAssetCount.Name = "lblGiveUpAssetCount";
            this.lblGiveUpAssetCount.Size = new System.Drawing.Size(89, 12);
            this.lblGiveUpAssetCount.TabIndex = 61;
            this.lblGiveUpAssetCount.Text = "种时，放弃购买";
            // 
            // txtGiveUpAssetCount
            // 
            this.txtGiveUpAssetCount.Location = new System.Drawing.Point(136, 104);
            this.txtGiveUpAssetCount.Name = "txtGiveUpAssetCount";
            this.txtGiveUpAssetCount.Size = new System.Drawing.Size(40, 21);
            this.txtGiveUpAssetCount.TabIndex = 59;
            // 
            // chkGiveUpIfMyAsset
            // 
            this.chkGiveUpIfMyAsset.AutoSize = true;
            this.chkGiveUpIfMyAsset.Location = new System.Drawing.Point(18, 106);
            this.chkGiveUpIfMyAsset.Name = "chkGiveUpIfMyAsset";
            this.chkGiveUpIfMyAsset.Size = new System.Drawing.Size(114, 16);
            this.chkGiveUpIfMyAsset.TabIndex = 60;
            this.chkGiveUpIfMyAsset.Text = "拥有的资产项 >=";
            this.chkGiveUpIfMyAsset.UseVisualStyleBackColor = true;
            // 
            // btnSetBuyAssetsList
            // 
            this.btnSetBuyAssetsList.Location = new System.Drawing.Point(18, 5);
            this.btnSetBuyAssetsList.Name = "btnSetBuyAssetsList";
            this.btnSetBuyAssetsList.Size = new System.Drawing.Size(120, 23);
            this.btnSetBuyAssetsList.TabIndex = 54;
            this.btnSetBuyAssetsList.Text = "设定购买的资产";
            this.btnSetBuyAssetsList.UseVisualStyleBackColor = true;
            this.btnSetBuyAssetsList.Click += new System.EventHandler(this.btnSetBuyAssetsList_Click);
            // 
            // lblGiveUpRatio
            // 
            this.lblGiveUpRatio.AutoSize = true;
            this.lblGiveUpRatio.Location = new System.Drawing.Point(183, 63);
            this.lblGiveUpRatio.Name = "lblGiveUpRatio";
            this.lblGiveUpRatio.Size = new System.Drawing.Size(89, 12);
            this.lblGiveUpRatio.TabIndex = 58;
            this.lblGiveUpRatio.Text = "% 时，放弃购买";
            // 
            // txtGiveUpRatio
            // 
            this.txtGiveUpRatio.Location = new System.Drawing.Point(140, 60);
            this.txtGiveUpRatio.Name = "txtGiveUpRatio";
            this.txtGiveUpRatio.Size = new System.Drawing.Size(40, 21);
            this.txtGiveUpRatio.TabIndex = 0;
            // 
            // chkGiveUpIfRatio
            // 
            this.chkGiveUpIfRatio.AutoSize = true;
            this.chkGiveUpIfRatio.Location = new System.Drawing.Point(18, 62);
            this.chkGiveUpIfRatio.Name = "chkGiveUpIfRatio";
            this.chkGiveUpIfRatio.Size = new System.Drawing.Size(120, 16);
            this.chkGiveUpIfRatio.TabIndex = 56;
            this.chkGiveUpIfRatio.Text = "现金/总资产比 <=";
            this.chkGiveUpIfRatio.UseVisualStyleBackColor = true;
            // 
            // panelBuyAssetCheap
            // 
            this.panelBuyAssetCheap.Controls.Add(this.rdbBuyAssetExpensive);
            this.panelBuyAssetCheap.Controls.Add(this.rdbBuyAssetCheap);
            this.panelBuyAssetCheap.Location = new System.Drawing.Point(87, 31);
            this.panelBuyAssetCheap.Name = "panelBuyAssetCheap";
            this.panelBuyAssetCheap.Size = new System.Drawing.Size(201, 25);
            this.panelBuyAssetCheap.TabIndex = 56;
            // 
            // rdbBuyAssetExpensive
            // 
            this.rdbBuyAssetExpensive.AutoSize = true;
            this.rdbBuyAssetExpensive.Location = new System.Drawing.Point(103, 6);
            this.rdbBuyAssetExpensive.Name = "rdbBuyAssetExpensive";
            this.rdbBuyAssetExpensive.Size = new System.Drawing.Size(95, 16);
            this.rdbBuyAssetExpensive.TabIndex = 1;
            this.rdbBuyAssetExpensive.TabStop = true;
            this.rdbBuyAssetExpensive.Text = "价格由高到低";
            this.rdbBuyAssetExpensive.UseVisualStyleBackColor = true;
            // 
            // rdbBuyAssetCheap
            // 
            this.rdbBuyAssetCheap.AutoSize = true;
            this.rdbBuyAssetCheap.Location = new System.Drawing.Point(3, 6);
            this.rdbBuyAssetCheap.Name = "rdbBuyAssetCheap";
            this.rdbBuyAssetCheap.Size = new System.Drawing.Size(95, 16);
            this.rdbBuyAssetCheap.TabIndex = 0;
            this.rdbBuyAssetCheap.TabStop = true;
            this.rdbBuyAssetCheap.Text = "价格由低到高";
            this.rdbBuyAssetCheap.UseVisualStyleBackColor = true;
            // 
            // lblBuyAssetCheap
            // 
            this.lblBuyAssetCheap.AutoSize = true;
            this.lblBuyAssetCheap.Location = new System.Drawing.Point(16, 37);
            this.lblBuyAssetCheap.Name = "lblBuyAssetCheap";
            this.lblBuyAssetCheap.Size = new System.Drawing.Size(65, 12);
            this.lblBuyAssetCheap.TabIndex = 57;
            this.lblBuyAssetCheap.Text = "购买顺序：";
            // 
            // chkBuyAsset
            // 
            this.chkBuyAsset.AutoSize = true;
            this.chkBuyAsset.Location = new System.Drawing.Point(12, 40);
            this.chkBuyAsset.Name = "chkBuyAsset";
            this.chkBuyAsset.Size = new System.Drawing.Size(72, 16);
            this.chkBuyAsset.TabIndex = 38;
            this.chkBuyAsset.Text = "购买资产";
            this.chkBuyAsset.UseVisualStyleBackColor = true;
            this.chkBuyAsset.CheckedChanged += new System.EventHandler(this.chkBuyAsset_CheckedChanged);
            // 
            // chkSellAsset
            // 
            this.chkSellAsset.AutoSize = true;
            this.chkSellAsset.Location = new System.Drawing.Point(12, 20);
            this.chkSellAsset.Name = "chkSellAsset";
            this.chkSellAsset.Size = new System.Drawing.Size(72, 16);
            this.chkSellAsset.TabIndex = 28;
            this.chkSellAsset.Text = "出售资产";
            this.chkSellAsset.UseVisualStyleBackColor = true;
            // 
            // tabPageCafe
            // 
            this.tabPageCafe.Controls.Add(this.grpCafe);
            this.tabPageCafe.Location = new System.Drawing.Point(4, 21);
            this.tabPageCafe.Name = "tabPageCafe";
            this.tabPageCafe.Size = new System.Drawing.Size(618, 259);
            this.tabPageCafe.TabIndex = 8;
            this.tabPageCafe.Text = "开心餐厅";
            this.tabPageCafe.UseVisualStyleBackColor = true;
            // 
            // grpCafe
            // 
            this.grpCafe.Controls.Add(this.lblWarningPurchaseFood);
            this.grpCafe.Controls.Add(this.lblWarningPresentFood);
            this.grpCafe.Controls.Add(this.lblWarningHire);
            this.grpCafe.Controls.Add(this.chkHelpFriend);
            this.grpCafe.Controls.Add(this.panelHire);
            this.grpCafe.Controls.Add(this.panelPurchaseFood);
            this.grpCafe.Controls.Add(this.chkPurchaseFood);
            this.grpCafe.Controls.Add(this.panelSellFood);
            this.grpCafe.Controls.Add(this.chkSellFood);
            this.grpCafe.Controls.Add(this.chkBoxClean);
            this.grpCafe.Controls.Add(this.panelPresentFood);
            this.grpCafe.Controls.Add(this.panelCook);
            this.grpCafe.Controls.Add(this.chkPresentFood);
            this.grpCafe.Controls.Add(this.chkHire);
            this.grpCafe.Controls.Add(this.chkCook);
            this.grpCafe.Location = new System.Drawing.Point(7, 3);
            this.grpCafe.Name = "grpCafe";
            this.grpCafe.Size = new System.Drawing.Size(608, 243);
            this.grpCafe.TabIndex = 47;
            this.grpCafe.TabStop = false;
            this.grpCafe.Text = "开心餐厅";
            // 
            // lblWarningPurchaseFood
            // 
            this.lblWarningPurchaseFood.AutoSize = true;
            this.lblWarningPurchaseFood.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningPurchaseFood.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPurchaseFood.Location = new System.Drawing.Point(377, 176);
            this.lblWarningPurchaseFood.Name = "lblWarningPurchaseFood";
            this.lblWarningPurchaseFood.Size = new System.Drawing.Size(11, 12);
            this.lblWarningPurchaseFood.TabIndex = 90;
            this.lblWarningPurchaseFood.Text = "*";
            // 
            // lblWarningPresentFood
            // 
            this.lblWarningPresentFood.AutoSize = true;
            this.lblWarningPresentFood.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningPresentFood.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPresentFood.Location = new System.Drawing.Point(377, 41);
            this.lblWarningPresentFood.Name = "lblWarningPresentFood";
            this.lblWarningPresentFood.Size = new System.Drawing.Size(11, 12);
            this.lblWarningPresentFood.TabIndex = 89;
            this.lblWarningPresentFood.Text = "*";
            // 
            // lblWarningHire
            // 
            this.lblWarningHire.AutoSize = true;
            this.lblWarningHire.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningHire.ForeColor = System.Drawing.Color.Red;
            this.lblWarningHire.Location = new System.Drawing.Point(91, 135);
            this.lblWarningHire.Name = "lblWarningHire";
            this.lblWarningHire.Size = new System.Drawing.Size(11, 12);
            this.lblWarningHire.TabIndex = 88;
            this.lblWarningHire.Text = "*";
            // 
            // chkHelpFriend
            // 
            this.chkHelpFriend.AutoSize = true;
            this.chkHelpFriend.Location = new System.Drawing.Point(309, 20);
            this.chkHelpFriend.Name = "chkHelpFriend";
            this.chkHelpFriend.Size = new System.Drawing.Size(72, 16);
            this.chkHelpFriend.TabIndex = 85;
            this.chkHelpFriend.Text = "帮忙好友";
            this.chkHelpFriend.UseVisualStyleBackColor = true;
            // 
            // panelHire
            // 
            this.panelHire.Controls.Add(this.lblMaxEmployees);
            this.panelHire.Controls.Add(this.cmbMaxEmployees);
            this.panelHire.Location = new System.Drawing.Point(25, 151);
            this.panelHire.Name = "panelHire";
            this.panelHire.Size = new System.Drawing.Size(145, 25);
            this.panelHire.TabIndex = 84;
            // 
            // lblMaxEmployees
            // 
            this.lblMaxEmployees.AutoSize = true;
            this.lblMaxEmployees.Location = new System.Drawing.Point(2, 5);
            this.lblMaxEmployees.Name = "lblMaxEmployees";
            this.lblMaxEmployees.Size = new System.Drawing.Size(77, 12);
            this.lblMaxEmployees.TabIndex = 83;
            this.lblMaxEmployees.Text = "员工数上限：";
            // 
            // cmbMaxEmployees
            // 
            this.cmbMaxEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaxEmployees.FormattingEnabled = true;
            this.cmbMaxEmployees.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbMaxEmployees.Location = new System.Drawing.Point(86, 2);
            this.cmbMaxEmployees.Name = "cmbMaxEmployees";
            this.cmbMaxEmployees.Size = new System.Drawing.Size(48, 20);
            this.cmbMaxEmployees.TabIndex = 82;
            // 
            // panelPurchaseFood
            // 
            this.panelPurchaseFood.Controls.Add(this.rdbPurchaseFoodByRefPrice);
            this.panelPurchaseFood.Controls.Add(this.rdbPurchaseFoodIgnorePrice);
            this.panelPurchaseFood.Location = new System.Drawing.Point(320, 193);
            this.panelPurchaseFood.Name = "panelPurchaseFood";
            this.panelPurchaseFood.Size = new System.Drawing.Size(249, 45);
            this.panelPurchaseFood.TabIndex = 81;
            // 
            // rdbPurchaseFoodByRefPrice
            // 
            this.rdbPurchaseFoodByRefPrice.AutoSize = true;
            this.rdbPurchaseFoodByRefPrice.Checked = true;
            this.rdbPurchaseFoodByRefPrice.Location = new System.Drawing.Point(3, 3);
            this.rdbPurchaseFoodByRefPrice.Name = "rdbPurchaseFoodByRefPrice";
            this.rdbPurchaseFoodByRefPrice.Size = new System.Drawing.Size(215, 16);
            this.rdbPurchaseFoodByRefPrice.TabIndex = 0;
            this.rdbPurchaseFoodByRefPrice.TabStop = true;
            this.rdbPurchaseFoodByRefPrice.Text = "按照菜肴交易表中设定的收购价交易";
            this.rdbPurchaseFoodByRefPrice.UseVisualStyleBackColor = true;
            // 
            // rdbPurchaseFoodIgnorePrice
            // 
            this.rdbPurchaseFoodIgnorePrice.AutoSize = true;
            this.rdbPurchaseFoodIgnorePrice.Location = new System.Drawing.Point(3, 21);
            this.rdbPurchaseFoodIgnorePrice.Name = "rdbPurchaseFoodIgnorePrice";
            this.rdbPurchaseFoodIgnorePrice.Size = new System.Drawing.Size(167, 16);
            this.rdbPurchaseFoodIgnorePrice.TabIndex = 1;
            this.rdbPurchaseFoodIgnorePrice.Text = "忽略当前市场价，直接收购";
            this.rdbPurchaseFoodIgnorePrice.UseVisualStyleBackColor = true;
            // 
            // chkPurchaseFood
            // 
            this.chkPurchaseFood.AutoSize = true;
            this.chkPurchaseFood.Location = new System.Drawing.Point(309, 175);
            this.chkPurchaseFood.Name = "chkPurchaseFood";
            this.chkPurchaseFood.Size = new System.Drawing.Size(72, 16);
            this.chkPurchaseFood.TabIndex = 80;
            this.chkPurchaseFood.Text = "收购食物";
            this.chkPurchaseFood.UseVisualStyleBackColor = true;
            this.chkPurchaseFood.CheckedChanged += new System.EventHandler(this.chkPurchaseFood_CheckedChanged);
            // 
            // panelSellFood
            // 
            this.panelSellFood.Controls.Add(this.rdbSellFoodByRefPrice);
            this.panelSellFood.Controls.Add(this.rdbSellFoodIgnorePrice);
            this.panelSellFood.Location = new System.Drawing.Point(21, 194);
            this.panelSellFood.Name = "panelSellFood";
            this.panelSellFood.Size = new System.Drawing.Size(249, 45);
            this.panelSellFood.TabIndex = 79;
            // 
            // rdbSellFoodByRefPrice
            // 
            this.rdbSellFoodByRefPrice.AutoSize = true;
            this.rdbSellFoodByRefPrice.Checked = true;
            this.rdbSellFoodByRefPrice.Location = new System.Drawing.Point(3, 3);
            this.rdbSellFoodByRefPrice.Name = "rdbSellFoodByRefPrice";
            this.rdbSellFoodByRefPrice.Size = new System.Drawing.Size(215, 16);
            this.rdbSellFoodByRefPrice.TabIndex = 0;
            this.rdbSellFoodByRefPrice.TabStop = true;
            this.rdbSellFoodByRefPrice.Text = "按照菜肴交易表中设定的出售价交易";
            this.rdbSellFoodByRefPrice.UseVisualStyleBackColor = true;
            // 
            // rdbSellFoodIgnorePrice
            // 
            this.rdbSellFoodIgnorePrice.AutoSize = true;
            this.rdbSellFoodIgnorePrice.Location = new System.Drawing.Point(3, 21);
            this.rdbSellFoodIgnorePrice.Name = "rdbSellFoodIgnorePrice";
            this.rdbSellFoodIgnorePrice.Size = new System.Drawing.Size(167, 16);
            this.rdbSellFoodIgnorePrice.TabIndex = 1;
            this.rdbSellFoodIgnorePrice.Text = "忽略当前市场价，直接出售";
            this.rdbSellFoodIgnorePrice.UseVisualStyleBackColor = true;
            // 
            // chkSellFood
            // 
            this.chkSellFood.AutoSize = true;
            this.chkSellFood.Location = new System.Drawing.Point(12, 178);
            this.chkSellFood.Name = "chkSellFood";
            this.chkSellFood.Size = new System.Drawing.Size(72, 16);
            this.chkSellFood.TabIndex = 78;
            this.chkSellFood.Text = "出售食物";
            this.chkSellFood.UseVisualStyleBackColor = true;
            this.chkSellFood.CheckedChanged += new System.EventHandler(this.chkSellFood_CheckedChanged);
            // 
            // chkBoxClean
            // 
            this.chkBoxClean.AutoSize = true;
            this.chkBoxClean.Location = new System.Drawing.Point(12, 16);
            this.chkBoxClean.Name = "chkBoxClean";
            this.chkBoxClean.Size = new System.Drawing.Size(108, 16);
            this.chkBoxClean.TabIndex = 77;
            this.chkBoxClean.Text = "装盘和清理灶台";
            this.chkBoxClean.UseVisualStyleBackColor = true;
            // 
            // panelPresentFood
            // 
            this.panelPresentFood.Controls.Add(this.btnPresentForbiddenFoodList);
            this.panelPresentFood.Controls.Add(this.txtPresentFoodMessage);
            this.panelPresentFood.Controls.Add(this.lblPresentFoodMessage);
            this.panelPresentFood.Controls.Add(this.lblPresentFoodLowCount);
            this.panelPresentFood.Controls.Add(this.txtPresentFoodLowCountLimit);
            this.panelPresentFood.Controls.Add(this.chkPresentFoodLowCount);
            this.panelPresentFood.Controls.Add(this.cmbPresentFoodDishId);
            this.panelPresentFood.Controls.Add(this.rdbPresentFoodDishId);
            this.panelPresentFood.Controls.Add(this.rdbPresentFoodByCount);
            this.panelPresentFood.Controls.Add(this.chkPresentLowCash);
            this.panelPresentFood.Controls.Add(this.txtPresentLowCashLimit);
            this.panelPresentFood.Controls.Add(this.lblPresentFoodRatioPercentage);
            this.panelPresentFood.Controls.Add(this.lblPresentFoodRatio);
            this.panelPresentFood.Controls.Add(this.txtPresentFoodRatio);
            this.panelPresentFood.Controls.Add(this.lblPresentLowCash);
            this.panelPresentFood.Location = new System.Drawing.Point(314, 55);
            this.panelPresentFood.Name = "panelPresentFood";
            this.panelPresentFood.Size = new System.Drawing.Size(288, 120);
            this.panelPresentFood.TabIndex = 76;
            // 
            // btnPresentForbiddenFoodList
            // 
            this.btnPresentForbiddenFoodList.Location = new System.Drawing.Point(156, 0);
            this.btnPresentForbiddenFoodList.Name = "btnPresentForbiddenFoodList";
            this.btnPresentForbiddenFoodList.Size = new System.Drawing.Size(129, 23);
            this.btnPresentForbiddenFoodList.TabIndex = 80;
            this.btnPresentForbiddenFoodList.Text = "设定禁止赠送的食物";
            this.btnPresentForbiddenFoodList.UseVisualStyleBackColor = true;
            this.btnPresentForbiddenFoodList.Click += new System.EventHandler(this.btnPresentForbiddenFoodList_Click);
            // 
            // txtPresentFoodMessage
            // 
            this.txtPresentFoodMessage.Location = new System.Drawing.Point(169, 49);
            this.txtPresentFoodMessage.Name = "txtPresentFoodMessage";
            this.txtPresentFoodMessage.Size = new System.Drawing.Size(94, 21);
            this.txtPresentFoodMessage.TabIndex = 79;
            // 
            // lblPresentFoodMessage
            // 
            this.lblPresentFoodMessage.AutoSize = true;
            this.lblPresentFoodMessage.Location = new System.Drawing.Point(109, 52);
            this.lblPresentFoodMessage.Name = "lblPresentFoodMessage";
            this.lblPresentFoodMessage.Size = new System.Drawing.Size(65, 12);
            this.lblPresentFoodMessage.TabIndex = 78;
            this.lblPresentFoodMessage.Text = "消息内容：";
            // 
            // lblPresentFoodLowCount
            // 
            this.lblPresentFoodLowCount.AutoSize = true;
            this.lblPresentFoodLowCount.Location = new System.Drawing.Point(186, 100);
            this.lblPresentFoodLowCount.Name = "lblPresentFoodLowCount";
            this.lblPresentFoodLowCount.Size = new System.Drawing.Size(77, 12);
            this.lblPresentFoodLowCount.TabIndex = 77;
            this.lblPresentFoodLowCount.Text = "种时禁止执行";
            // 
            // txtPresentFoodLowCountLimit
            // 
            this.txtPresentFoodLowCountLimit.Location = new System.Drawing.Point(149, 95);
            this.txtPresentFoodLowCountLimit.Name = "txtPresentFoodLowCountLimit";
            this.txtPresentFoodLowCountLimit.Size = new System.Drawing.Size(31, 21);
            this.txtPresentFoodLowCountLimit.TabIndex = 76;
            // 
            // chkPresentFoodLowCount
            // 
            this.chkPresentFoodLowCount.AutoSize = true;
            this.chkPresentFoodLowCount.Location = new System.Drawing.Point(8, 97);
            this.chkPresentFoodLowCount.Name = "chkPresentFoodLowCount";
            this.chkPresentFoodLowCount.Size = new System.Drawing.Size(144, 16);
            this.chkPresentFoodLowCount.TabIndex = 75;
            this.chkPresentFoodLowCount.Text = "餐台上食物种类数低于";
            this.chkPresentFoodLowCount.UseVisualStyleBackColor = true;
            // 
            // cmbPresentFoodDishId
            // 
            this.cmbPresentFoodDishId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresentFoodDishId.FormattingEnabled = true;
            this.cmbPresentFoodDishId.Location = new System.Drawing.Point(67, 26);
            this.cmbPresentFoodDishId.Name = "cmbPresentFoodDishId";
            this.cmbPresentFoodDishId.Size = new System.Drawing.Size(134, 20);
            this.cmbPresentFoodDishId.TabIndex = 74;
            // 
            // rdbPresentFoodDishId
            // 
            this.rdbPresentFoodDishId.AutoSize = true;
            this.rdbPresentFoodDishId.Location = new System.Drawing.Point(3, 27);
            this.rdbPresentFoodDishId.Name = "rdbPresentFoodDishId";
            this.rdbPresentFoodDishId.Size = new System.Drawing.Size(59, 16);
            this.rdbPresentFoodDishId.TabIndex = 73;
            this.rdbPresentFoodDishId.Text = "自定义";
            this.rdbPresentFoodDishId.UseVisualStyleBackColor = true;
            this.rdbPresentFoodDishId.CheckedChanged += new System.EventHandler(this.rdbPresentFoodDishId_CheckedChanged);
            // 
            // rdbPresentFoodByCount
            // 
            this.rdbPresentFoodByCount.AutoSize = true;
            this.rdbPresentFoodByCount.Checked = true;
            this.rdbPresentFoodByCount.Location = new System.Drawing.Point(3, 4);
            this.rdbPresentFoodByCount.Name = "rdbPresentFoodByCount";
            this.rdbPresentFoodByCount.Size = new System.Drawing.Size(155, 16);
            this.rdbPresentFoodByCount.TabIndex = 72;
            this.rdbPresentFoodByCount.TabStop = true;
            this.rdbPresentFoodByCount.Text = "优先赠送数量最多的食物";
            this.rdbPresentFoodByCount.UseVisualStyleBackColor = true;
            this.rdbPresentFoodByCount.CheckedChanged += new System.EventHandler(this.rdbPresentFoodByCount_CheckedChanged);
            // 
            // chkPresentLowCash
            // 
            this.chkPresentLowCash.AutoSize = true;
            this.chkPresentLowCash.Location = new System.Drawing.Point(8, 74);
            this.chkPresentLowCash.Name = "chkPresentLowCash";
            this.chkPresentLowCash.Size = new System.Drawing.Size(72, 16);
            this.chkPresentLowCash.TabIndex = 66;
            this.chkPresentLowCash.Text = "现金不足";
            this.chkPresentLowCash.UseVisualStyleBackColor = true;
            // 
            // txtPresentLowCashLimit
            // 
            this.txtPresentLowCashLimit.Location = new System.Drawing.Point(82, 72);
            this.txtPresentLowCashLimit.Name = "txtPresentLowCashLimit";
            this.txtPresentLowCashLimit.Size = new System.Drawing.Size(88, 21);
            this.txtPresentLowCashLimit.TabIndex = 67;
            // 
            // lblPresentFoodRatioPercentage
            // 
            this.lblPresentFoodRatioPercentage.AutoSize = true;
            this.lblPresentFoodRatioPercentage.Location = new System.Drawing.Point(92, 52);
            this.lblPresentFoodRatioPercentage.Name = "lblPresentFoodRatioPercentage";
            this.lblPresentFoodRatioPercentage.Size = new System.Drawing.Size(11, 12);
            this.lblPresentFoodRatioPercentage.TabIndex = 70;
            this.lblPresentFoodRatioPercentage.Text = "%";
            // 
            // lblPresentFoodRatio
            // 
            this.lblPresentFoodRatio.AutoSize = true;
            this.lblPresentFoodRatio.Location = new System.Drawing.Point(6, 52);
            this.lblPresentFoodRatio.Name = "lblPresentFoodRatio";
            this.lblPresentFoodRatio.Size = new System.Drawing.Size(53, 12);
            this.lblPresentFoodRatio.TabIndex = 71;
            this.lblPresentFoodRatio.Text = "赠送率：";
            // 
            // txtPresentFoodRatio
            // 
            this.txtPresentFoodRatio.Location = new System.Drawing.Point(59, 49);
            this.txtPresentFoodRatio.Name = "txtPresentFoodRatio";
            this.txtPresentFoodRatio.Size = new System.Drawing.Size(28, 21);
            this.txtPresentFoodRatio.TabIndex = 69;
            // 
            // lblPresentLowCash
            // 
            this.lblPresentLowCash.AutoSize = true;
            this.lblPresentLowCash.Location = new System.Drawing.Point(171, 76);
            this.lblPresentLowCash.Name = "lblPresentLowCash";
            this.lblPresentLowCash.Size = new System.Drawing.Size(77, 12);
            this.lblPresentLowCash.TabIndex = 68;
            this.lblPresentLowCash.Text = "元时禁止执行";
            // 
            // panelCook
            // 
            this.panelCook.Controls.Add(this.chkCookMedlarFirst);
            this.panelCook.Controls.Add(this.chkCookCrabFirst);
            this.panelCook.Controls.Add(this.chkCookPineappleFirst);
            this.panelCook.Controls.Add(this.chkCookTomatoFirst);
            this.panelCook.Controls.Add(this.cmbCookDishId);
            this.panelCook.Controls.Add(this.lblCookDishId);
            this.panelCook.Controls.Add(this.chkCookLowCash);
            this.panelCook.Controls.Add(this.lblCookLowCash);
            this.panelCook.Controls.Add(this.txtCookLowCashLimit);
            this.panelCook.Location = new System.Drawing.Point(23, 47);
            this.panelCook.Name = "panelCook";
            this.panelCook.Size = new System.Drawing.Size(280, 86);
            this.panelCook.TabIndex = 75;
            // 
            // chkCookMedlarFirst
            // 
            this.chkCookMedlarFirst.AutoSize = true;
            this.chkCookMedlarFirst.Location = new System.Drawing.Point(141, 5);
            this.chkCookMedlarFirst.Name = "chkCookMedlarFirst";
            this.chkCookMedlarFirst.Size = new System.Drawing.Size(132, 16);
            this.chkCookMedlarFirst.TabIndex = 82;
            this.chkCookMedlarFirst.Text = "优先兑换枸杞银耳羹";
            this.chkCookMedlarFirst.UseVisualStyleBackColor = true;
            // 
            // chkCookCrabFirst
            // 
            this.chkCookCrabFirst.AutoSize = true;
            this.chkCookCrabFirst.Location = new System.Drawing.Point(3, 20);
            this.chkCookCrabFirst.Name = "chkCookCrabFirst";
            this.chkCookCrabFirst.Size = new System.Drawing.Size(132, 16);
            this.chkCookCrabFirst.TabIndex = 81;
            this.chkCookCrabFirst.Text = "优先兑换清蒸大闸蟹";
            this.chkCookCrabFirst.UseVisualStyleBackColor = true;
            // 
            // chkCookPineappleFirst
            // 
            this.chkCookPineappleFirst.AutoSize = true;
            this.chkCookPineappleFirst.Location = new System.Drawing.Point(141, 20);
            this.chkCookPineappleFirst.Name = "chkCookPineappleFirst";
            this.chkCookPineappleFirst.Size = new System.Drawing.Size(132, 16);
            this.chkCookPineappleFirst.TabIndex = 80;
            this.chkCookPineappleFirst.Text = "优先兑换菠萝古老肉";
            this.chkCookPineappleFirst.UseVisualStyleBackColor = true;
            // 
            // chkCookTomatoFirst
            // 
            this.chkCookTomatoFirst.AutoSize = true;
            this.chkCookTomatoFirst.Location = new System.Drawing.Point(3, 5);
            this.chkCookTomatoFirst.Name = "chkCookTomatoFirst";
            this.chkCookTomatoFirst.Size = new System.Drawing.Size(120, 16);
            this.chkCookTomatoFirst.TabIndex = 79;
            this.chkCookTomatoFirst.Text = "优先兑换番茄炒蛋";
            this.chkCookTomatoFirst.UseVisualStyleBackColor = true;
            // 
            // cmbCookDishId
            // 
            this.cmbCookDishId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCookDishId.FormattingEnabled = true;
            this.cmbCookDishId.Location = new System.Drawing.Point(51, 38);
            this.cmbCookDishId.Name = "cmbCookDishId";
            this.cmbCookDishId.Size = new System.Drawing.Size(126, 20);
            this.cmbCookDishId.TabIndex = 78;
            // 
            // lblCookDishId
            // 
            this.lblCookDishId.AutoSize = true;
            this.lblCookDishId.Location = new System.Drawing.Point(3, 46);
            this.lblCookDishId.Name = "lblCookDishId";
            this.lblCookDishId.Size = new System.Drawing.Size(41, 12);
            this.lblCookDishId.TabIndex = 75;
            this.lblCookDishId.Text = "菜名：";
            // 
            // chkCookLowCash
            // 
            this.chkCookLowCash.AutoSize = true;
            this.chkCookLowCash.Location = new System.Drawing.Point(3, 64);
            this.chkCookLowCash.Name = "chkCookLowCash";
            this.chkCookLowCash.Size = new System.Drawing.Size(72, 16);
            this.chkCookLowCash.TabIndex = 72;
            this.chkCookLowCash.Text = "现金不足";
            this.chkCookLowCash.UseVisualStyleBackColor = true;
            // 
            // lblCookLowCash
            // 
            this.lblCookLowCash.AutoSize = true;
            this.lblCookLowCash.Location = new System.Drawing.Point(170, 65);
            this.lblCookLowCash.Name = "lblCookLowCash";
            this.lblCookLowCash.Size = new System.Drawing.Size(77, 12);
            this.lblCookLowCash.TabIndex = 74;
            this.lblCookLowCash.Text = "元时禁止执行";
            // 
            // txtCookLowCashLimit
            // 
            this.txtCookLowCashLimit.Location = new System.Drawing.Point(79, 62);
            this.txtCookLowCashLimit.Name = "txtCookLowCashLimit";
            this.txtCookLowCashLimit.Size = new System.Drawing.Size(88, 21);
            this.txtCookLowCashLimit.TabIndex = 73;
            // 
            // chkPresentFood
            // 
            this.chkPresentFood.AutoSize = true;
            this.chkPresentFood.Location = new System.Drawing.Point(309, 40);
            this.chkPresentFood.Name = "chkPresentFood";
            this.chkPresentFood.Size = new System.Drawing.Size(72, 16);
            this.chkPresentFood.TabIndex = 24;
            this.chkPresentFood.Text = "赠送食物";
            this.chkPresentFood.UseVisualStyleBackColor = true;
            this.chkPresentFood.CheckedChanged += new System.EventHandler(this.chkPresentFood_CheckedChanged);
            // 
            // chkHire
            // 
            this.chkHire.AutoSize = true;
            this.chkHire.Location = new System.Drawing.Point(12, 134);
            this.chkHire.Name = "chkHire";
            this.chkHire.Size = new System.Drawing.Size(84, 16);
            this.chkHire.TabIndex = 23;
            this.chkHire.Text = "雇佣新员工";
            this.chkHire.UseVisualStyleBackColor = true;
            this.chkHire.CheckedChanged += new System.EventHandler(this.chkHire_CheckedChanged);
            // 
            // chkCook
            // 
            this.chkCook.AutoSize = true;
            this.chkCook.Location = new System.Drawing.Point(12, 32);
            this.chkCook.Name = "chkCook";
            this.chkCook.Size = new System.Drawing.Size(48, 16);
            this.chkCook.TabIndex = 22;
            this.chkCook.Text = "炒菜";
            this.chkCook.UseVisualStyleBackColor = true;
            this.chkCook.CheckedChanged += new System.EventHandler(this.chkCook_CheckedChanged);
            // 
            // lblWarningNotes
            // 
            this.lblWarningNotes.AutoSize = true;
            this.lblWarningNotes.ForeColor = System.Drawing.Color.Red;
            this.lblWarningNotes.Location = new System.Drawing.Point(641, 358);
            this.lblWarningNotes.Name = "lblWarningNotes";
            this.lblWarningNotes.Size = new System.Drawing.Size(155, 12);
            this.lblWarningNotes.TabIndex = 41;
            this.lblWarningNotes.Text = "带*号表示可以设定黑白名单";
            // 
            // FrmTaskEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(843, 575);
            this.Controls.Add(this.lblWarningNotes);
            this.Controls.Add(this.tabGames);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.grpTasks);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpAccounts);
            this.Controls.Add(this.grpLoopTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 372);
            this.Name = "FrmTaskEditor";
            this.Load += new System.EventHandler(this.FrmTaskEditor_Load);
            this.grpAccounts.ResumeLayout(false);
            this.grpAccounts.PerformLayout();
            this.grpTasks.ResumeLayout(false);
            this.grpTasks.PerformLayout();
            this.grpLoopTime.ResumeLayout(false);
            this.grpRunMode.ResumeLayout(false);
            this.grpRunMode.PerformLayout();
            this.grpValidation.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpLoop.ResumeLayout(false);
            this.grpLoop.PerformLayout();
            this.groupTiming.ResumeLayout(false);
            this.groupTiming.PerformLayout();
            this.grpPark.ResumeLayout(false);
            this.grpPark.PerformLayout();
            this.panelStartCar.ResumeLayout(false);
            this.panelStartCar.PerformLayout();
            this.panelOriginateMatch.ResumeLayout(false);
            this.panelOriginateMatch.PerformLayout();
            this.grpGarden.ResumeLayout(false);
            this.grpGarden.PerformLayout();
            this.panelPresentFruit.ResumeLayout(false);
            this.panelPresentFruit.PerformLayout();
            this.panelSellFruit.ResumeLayout(false);
            this.panelSellFruit.PerformLayout();
            this.panelSellForbiddenFruit.ResumeLayout(false);
            this.panelSellForbiddenFruit.PerformLayout();
            this.panelFarmSelf.ResumeLayout(false);
            this.panelFarmSelf.PerformLayout();
            this.panelFarmShared.ResumeLayout(false);
            this.panelFarmShared.PerformLayout();
            this.grpRanch.ResumeLayout(false);
            this.grpRanch.PerformLayout();
            this.panelSellProduct.ResumeLayout(false);
            this.panelSellProduct.PerformLayout();
            this.panelSellProductForbidden.ResumeLayout(false);
            this.panelSellProductForbidden.PerformLayout();
            this.panelPresentProduct.ResumeLayout(false);
            this.panelPresentProduct.PerformLayout();
            this.panelBuyCalf.ResumeLayout(false);
            this.panelBuyCalf.PerformLayout();
            this.tabGames.ResumeLayout(false);
            this.tabPagePark.ResumeLayout(false);
            this.tabPageBite.ResumeLayout(false);
            this.grpBite.ResumeLayout(false);
            this.grpBite.PerformLayout();
            this.tabPageSlave.ResumeLayout(false);
            this.grpSlave.ResumeLayout(false);
            this.grpSlave.PerformLayout();
            this.tabPageHouse.ResumeLayout(false);
            this.grpHouse.ResumeLayout(false);
            this.grpHouse.PerformLayout();
            this.tabPageGarden.ResumeLayout(false);
            this.tabPageRanch.ResumeLayout(false);
            this.tabPageFish.ResumeLayout(false);
            this.grpFish.ResumeLayout(false);
            this.grpFish.PerformLayout();
            this.panelBuyUpdateTackle.ResumeLayout(false);
            this.panelBuyUpdateTackle.PerformLayout();
            this.panelSellFish.ResumeLayout(false);
            this.panelSellFish.PerformLayout();
            this.panelSellFishForbidden.ResumeLayout(false);
            this.panelSellFishForbidden.PerformLayout();
            this.panelPresentFish.ResumeLayout(false);
            this.panelPresentFish.PerformLayout();
            this.panelBuyFish.ResumeLayout(false);
            this.panelBuyFish.PerformLayout();
            this.panelNetSelfFish.ResumeLayout(false);
            this.panelNetSelfFish.PerformLayout();
            this.tabPageRich.ResumeLayout(false);
            this.grpRich.ResumeLayout(false);
            this.grpRich.PerformLayout();
            this.panelBuyAsset.ResumeLayout(false);
            this.panelBuyAsset.PerformLayout();
            this.panelBuyAssetCheap.ResumeLayout(false);
            this.panelBuyAssetCheap.PerformLayout();
            this.tabPageCafe.ResumeLayout(false);
            this.grpCafe.ResumeLayout(false);
            this.grpCafe.PerformLayout();
            this.panelHire.ResumeLayout(false);
            this.panelHire.PerformLayout();
            this.panelPurchaseFood.ResumeLayout(false);
            this.panelPurchaseFood.PerformLayout();
            this.panelSellFood.ResumeLayout(false);
            this.panelSellFood.PerformLayout();
            this.panelPresentFood.ResumeLayout(false);
            this.panelPresentFood.PerformLayout();
            this.panelCook.ResumeLayout(false);
            this.panelCook.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectedAccounts;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.TextBox txtRoundTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUnselectOne;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectOne;
        private System.Windows.Forms.ListBox lstSelectedAccounts;
        private System.Windows.Forms.ListBox lstAllAccounts;
        private System.Windows.Forms.GroupBox grpAccounts;
        private System.Windows.Forms.GroupBox grpTasks;
        private System.Windows.Forms.CheckBox chkTaskSlave;
        private System.Windows.Forms.CheckBox chkTaskParking;
        private System.Windows.Forms.CheckBox chkTaskBiting;
        private System.Windows.Forms.Label lblLoopTime;
        private System.Windows.Forms.GroupBox grpLoopTime;
        private System.Windows.Forms.GroupBox grpPark;
        private System.Windows.Forms.CheckBox chkParkMyCars;
        private System.Windows.Forms.CheckBox chkPostOthersCars;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.RadioButton rdbTiming;
        private System.Windows.Forms.RadioButton rdbMultiLoop;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector1;
        private System.Windows.Forms.GroupBox groupTiming;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector6;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector5;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector4;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector3;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector2;
        private System.Windows.Forms.Label label2;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector9;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector8;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector7;
        private System.Windows.Forms.Label label11;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeSelector10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpLoop;
        private System.Windows.Forms.CheckBox chkForbidden;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeForbiddenStart;
        private System.Windows.Forms.Label lblStart;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeForbiddenEnd;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.CheckBox chkSendLog;
        private System.Windows.Forms.Label lblReceiverEmail;
        private System.Windows.Forms.TextBox txtReceiverEmail;
        private System.Windows.Forms.CheckBox chkCheerUp;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.CheckBox chkTaskHouse;
        private System.Windows.Forms.CheckBox chkOriginateMatch;
        private System.Windows.Forms.CheckBox chkStartCar;
        private System.Windows.Forms.CheckBox chkJoinMatch;
        private System.Windows.Forms.CheckBox chkFarmSelf;
        private System.Windows.Forms.CheckBox chkStealFruit;
        private System.Windows.Forms.ComboBox cmbBuySeedCount;
        private System.Windows.Forms.CheckBox chkBuySeed;
        private System.Windows.Forms.CheckBox chkHarvestFruit;
        private System.Windows.Forms.GroupBox grpGarden;
        private System.Windows.Forms.CheckBox chkTaskGarden;
        private System.Windows.Forms.CheckBox chkFarmShared;
        private System.Windows.Forms.Label lblFarmShared;
        private System.Windows.Forms.Label lblSeedsCount;
        private System.Windows.Forms.ComboBox cmbCustomFarmShared;
        private System.Windows.Forms.CheckBox chkHelpOthers;
        private System.Windows.Forms.Panel panelFarmShared;
        private System.Windows.Forms.RadioButton rdbCustomFarmShared;
        private System.Windows.Forms.RadioButton rdbExpensiveFarmShared;
        private System.Windows.Forms.Panel panelFarmSelf;
        private System.Windows.Forms.ComboBox cmbCustomFarmSelf;
        private System.Windows.Forms.RadioButton rdbCustomFarmSelf;
        private System.Windows.Forms.RadioButton rdbExpensiveFarmSelf;
        private System.Windows.Forms.Label lblFarmSelf;
        private System.Windows.Forms.CheckBox chkPresentFruit;
        private System.Windows.Forms.CheckBox chkSellFruit;
        private System.Windows.Forms.CheckBox chkWriteLogToFile;
        private System.Windows.Forms.GroupBox grpRanch;
        private System.Windows.Forms.CheckBox chkAddWater;
        private System.Windows.Forms.CheckBox chkTaskRanch;
        private System.Windows.Forms.CheckBox chkBuyCalf;
        private System.Windows.Forms.CheckBox chkHelpAddGrass;
        private System.Windows.Forms.CheckBox chkAddGrass;
        private System.Windows.Forms.CheckBox chkHelpAddWater;
        private System.Windows.Forms.CheckBox chkStealProduct;
        private System.Windows.Forms.CheckBox chkHelpMakeProduct;
        private System.Windows.Forms.CheckBox chkMakeProduct;
        private System.Windows.Forms.CheckBox chkHarvestProduct;
        private System.Windows.Forms.CheckBox chkBreedAnimal;
        private System.Windows.Forms.TabControl tabGames;
        private System.Windows.Forms.TabPage tabPagePark;
        private System.Windows.Forms.TabPage tabPageGarden;
        private System.Windows.Forms.TabPage tabPageRanch;
        private System.Windows.Forms.Label lblFoodNum;
        private System.Windows.Forms.TextBox txtFoodNum;
        private System.Windows.Forms.CheckBox chkHarvestAnimal;
        private System.Windows.Forms.CheckBox chkSowMySeedsFirst;
        private System.Windows.Forms.Button btnSetFruitList;
        private System.Windows.Forms.CheckBox chkStealUnknowFruit;
        private System.Windows.Forms.CheckBox chkSellProduct;
        private System.Windows.Forms.CheckBox chkPresentProduct;
        private System.Windows.Forms.CheckBox chkHelpAddCarrot;
        private System.Windows.Forms.CheckBox chkAddCarrot;
        private System.Windows.Forms.Label lblCarrotNum;
        private System.Windows.Forms.TextBox txtCarrotNum;
        private System.Windows.Forms.CheckBox chkAddBamboo;
        private System.Windows.Forms.CheckBox chkHelpAddBamboo;
        private System.Windows.Forms.TextBox txtBambooNum;
        private System.Windows.Forms.Label lblBambooNum;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdbPopupValidationWindow;
        private System.Windows.Forms.RadioButton rdbSkip;
        private System.Windows.Forms.GroupBox grpValidation;
        private System.Windows.Forms.Label lblStartCarTime;
        private Johnny.Controls.Windows.DateTimer.TimeSelector timeStartCarTime;
        private System.Windows.Forms.Panel panelSellFruit;
        private System.Windows.Forms.RadioButton rdbFobiddenFruit;
        private System.Windows.Forms.RadioButton rdbSellAllFruit;
        private System.Windows.Forms.TextBox txtMaxSellLimit;
        private System.Windows.Forms.Label lblMaxSellLimit1;
        private System.Windows.Forms.CheckBox chkLowCash;
        private System.Windows.Forms.Label lblMaxSellLimit2;
        private System.Windows.Forms.Panel panelSellForbiddenFruit;
        private System.Windows.Forms.Label lblLowCash;
        private System.Windows.Forms.TextBox txtLowCashLimit;
        private System.Windows.Forms.Button btnForbiddenFruitList;
        private System.Windows.Forms.CheckBox chkTaskFish;
        private System.Windows.Forms.TabPage tabPageFish;
        private System.Windows.Forms.GroupBox grpFish;
        private System.Windows.Forms.CheckBox chkBuyUpdateTackle;
        private System.Windows.Forms.CheckBox chkFishing;
        private System.Windows.Forms.CheckBox chkSellFish;
        private System.Windows.Forms.CheckBox chkPresentFish;
        private System.Windows.Forms.CheckBox chkTreatFish;
        private System.Windows.Forms.CheckBox chkHelpFish;
        private System.Windows.Forms.CheckBox chkHarvestFish;
        private System.Windows.Forms.CheckBox chkBuyFish;
        private System.Windows.Forms.CheckBox chkShake;
        private System.Windows.Forms.TabPage tabPageRich;
        private System.Windows.Forms.GroupBox grpRich;
        private System.Windows.Forms.CheckBox chkBuyAsset;
        private System.Windows.Forms.CheckBox chkSellAsset;
        private System.Windows.Forms.CheckBox chkTaskRich;
        private System.Windows.Forms.Panel panelBuyAsset;
        private System.Windows.Forms.Button btnSetBuyAssetsList;
        private System.Windows.Forms.RadioButton rdbSingleLoop;
        private System.Windows.Forms.Panel panelBuyAssetCheap;
        private System.Windows.Forms.RadioButton rdbBuyAssetExpensive;
        private System.Windows.Forms.RadioButton rdbBuyAssetCheap;
        private System.Windows.Forms.Label lblBuyAssetCheap;
        private System.Windows.Forms.Label lblGiveUpAssetCount;
        private System.Windows.Forms.TextBox txtGiveUpAssetCount;
        private System.Windows.Forms.CheckBox chkGiveUpIfMyAsset;
        private System.Windows.Forms.Label lblGiveUpRatio;
        private System.Windows.Forms.TextBox txtGiveUpRatio;
        private System.Windows.Forms.CheckBox chkGiveUpIfRatio;
        private System.Windows.Forms.CheckBox chkAdvancedPurchase;
        private System.Windows.Forms.Label lblRich;
        private System.Windows.Forms.Label lblAdvancedPurchase;
        private System.Windows.Forms.CheckBox chkUpdateFishPond;
        private System.Windows.Forms.GroupBox grpRunMode;
        private System.Windows.Forms.CheckBox chkGiveUpIfMinimum;
        private System.Windows.Forms.Label lblGiveUpMinimum;
        private System.Windows.Forms.TextBox txtGiveUpMinimum;
        private System.Windows.Forms.Label lblIfMinimum;
        private System.Windows.Forms.TabPage tabPageBite;
        private System.Windows.Forms.GroupBox grpBite;
        private System.Windows.Forms.CheckBox chkProtectFriend;
        private System.Windows.Forms.CheckBox chkAutoRecover;
        private System.Windows.Forms.CheckBox chkApproveRecovery;
        private System.Windows.Forms.CheckBox chkBiteOthers;
        private System.Windows.Forms.TabPage tabPageSlave;
        private System.Windows.Forms.GroupBox grpSlave;
        private System.Windows.Forms.CheckBox chkFawnMaster;
        private System.Windows.Forms.CheckBox chkBuySlave;
        private System.Windows.Forms.CheckBox chkBuyLowPriceSlave;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.CheckBox chkPropitiateSlave;
        private System.Windows.Forms.Label lblNickName;
        private System.Windows.Forms.CheckBox chkAfflictSlave;
        private System.Windows.Forms.Label lblMaxSlaves;
        private System.Windows.Forms.CheckBox chkReleaseSlave;
        private System.Windows.Forms.ComboBox cmbMaxSlaves;
        private System.Windows.Forms.TabPage tabPageHouse;
        private System.Windows.Forms.GroupBox grpHouse;
        private System.Windows.Forms.CheckBox chkDriveFriends;
        private System.Windows.Forms.CheckBox chkRobFreeFriends;
        private System.Windows.Forms.CheckBox chkRobFriends;
        private System.Windows.Forms.CheckBox chkStayHouse;
        private System.Windows.Forms.CheckBox chkDoJob;
        private System.Windows.Forms.ComboBox cmbOriginateMatchId;
        private System.Windows.Forms.CheckBox chkTaskCafe;
        private System.Windows.Forms.TabPage tabPageCafe;
        private System.Windows.Forms.GroupBox grpCafe;
        private System.Windows.Forms.CheckBox chkCook;
        private System.Windows.Forms.CheckBox chkHire;
        private System.Windows.Forms.CheckBox chkPresentFood;
        private System.Windows.Forms.Label lblPresentLowCash;
        private System.Windows.Forms.TextBox txtPresentLowCashLimit;
        private System.Windows.Forms.CheckBox chkPresentLowCash;
        private System.Windows.Forms.Label lblPresentFoodRatio;
        private System.Windows.Forms.Label lblPresentFoodRatioPercentage;
        private System.Windows.Forms.TextBox txtPresentFoodRatio;
        private System.Windows.Forms.Panel panelCook;
        private System.Windows.Forms.CheckBox chkCookLowCash;
        private System.Windows.Forms.Label lblCookLowCash;
        private System.Windows.Forms.TextBox txtCookLowCashLimit;
        private System.Windows.Forms.Panel panelPresentFood;
        private System.Windows.Forms.ComboBox cmbCookDishId;
        private System.Windows.Forms.Label lblCookDishId;
        private System.Windows.Forms.CheckBox chkBoxClean;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMatch;
        private System.Windows.Forms.ComboBox cmbOriginateTeamNum;
        private System.Windows.Forms.Panel panelOriginateMatch;
        private System.Windows.Forms.Panel panelStartCar;
        private System.Windows.Forms.Panel panelPresentFruit;
        private System.Windows.Forms.RadioButton rdbPresentFruitByPrice;
        private System.Windows.Forms.ComboBox cmbPresentFruitId;
        private System.Windows.Forms.RadioButton rdbPresentFruitCustom;
        private System.Windows.Forms.Panel panelBuyCalf;
        private System.Windows.Forms.RadioButton rdbBuyCalfByPrice;
        private System.Windows.Forms.ComboBox cmbBuyCalfCustom;
        private System.Windows.Forms.RadioButton rdbBuyCalfCustom;
        private System.Windows.Forms.ComboBox cmbPresentFoodDishId;
        private System.Windows.Forms.RadioButton rdbPresentFoodDishId;
        private System.Windows.Forms.RadioButton rdbPresentFoodByCount;
        private System.Windows.Forms.Label lblPresentFoodLowCount;
        private System.Windows.Forms.TextBox txtPresentFoodLowCountLimit;
        private System.Windows.Forms.CheckBox chkPresentFoodLowCount;
        private System.Windows.Forms.TextBox txtPresentFoodMessage;
        private System.Windows.Forms.Label lblPresentFoodMessage;
        private System.Windows.Forms.Panel panelSellFood;
        private System.Windows.Forms.RadioButton rdbSellFoodByRefPrice;
        private System.Windows.Forms.RadioButton rdbSellFoodIgnorePrice;
        private System.Windows.Forms.CheckBox chkSellFood;
        private System.Windows.Forms.Button btnPresentForbiddenFoodList;
        private System.Windows.Forms.CheckBox chkCookTomatoFirst;
        private System.Windows.Forms.Panel panelPresentProduct;
        private System.Windows.Forms.RadioButton rdbPresentProductByPrice;
        private System.Windows.Forms.ComboBox cmbAnimalProducts;
        private System.Windows.Forms.RadioButton rdbPresentProductCustom;
        private System.Windows.Forms.Panel panelPurchaseFood;
        private System.Windows.Forms.RadioButton rdbPurchaseFoodByRefPrice;
        private System.Windows.Forms.RadioButton rdbPurchaseFoodIgnorePrice;
        private System.Windows.Forms.CheckBox chkPurchaseFood;
        private System.Windows.Forms.CheckBox chkNetSelfFish;
        private System.Windows.Forms.Label lblNetSelfFishCheap;
        private System.Windows.Forms.Panel panelNetSelfFish;
        private System.Windows.Forms.RadioButton rdbNetSelfFishExpensive;
        private System.Windows.Forms.RadioButton rdbNetSelfFishCheap;
        private System.Windows.Forms.Label lblNetSelfFishMaturePercentage;
        private System.Windows.Forms.Label lblNetSelfFishMature;
        private System.Windows.Forms.TextBox txtNetSelfFishMature;
        private System.Windows.Forms.Panel panelBuyFish;
        private System.Windows.Forms.RadioButton rdbBuyFishByRank;
        private System.Windows.Forms.ComboBox cmbBuyFishFishId;
        private System.Windows.Forms.RadioButton rdbBuyFishCustom;
        private System.Windows.Forms.Panel panelPresentFish;
        private System.Windows.Forms.Label lblPresentFishValue;
        private System.Windows.Forms.TextBox txtPresentFishValue;
        private System.Windows.Forms.CheckBox chkPresentFishCheckValue;
        private System.Windows.Forms.Button btnPresentFishForbiddenList;
        private System.Windows.Forms.RadioButton rdbPresentFishCheap;
        private System.Windows.Forms.RadioButton rdbPresentFishExpensive;
        private System.Windows.Forms.Label lblPresentFishCheap;
        private System.Windows.Forms.CheckBox chkBangKeJing;
        private System.Windows.Forms.Panel panelSellFish;
        private System.Windows.Forms.Panel panelSellFishForbidden;
        private System.Windows.Forms.Button btnSellFishForbiddenList;
        private System.Windows.Forms.Label lblSellFishMaxLimit;
        private System.Windows.Forms.TextBox txtSellFishMaxLimit;
        private System.Windows.Forms.Label lblSellFishMaxLimit2;
        private System.Windows.Forms.Label lblSellFishLowCash;
        private System.Windows.Forms.TextBox txtSellFishLowCashLimit;
        private System.Windows.Forms.CheckBox chkSellFishLowCash;
        private System.Windows.Forms.RadioButton rdbSellAllFish;
        private System.Windows.Forms.RadioButton rdbForbiddenFish;
        private System.Windows.Forms.Label lblSellFishCheckValue;
        private System.Windows.Forms.TextBox txtSellFishValue;
        private System.Windows.Forms.CheckBox chkSellFishCheckValue;
        private System.Windows.Forms.Panel panelHire;
        private System.Windows.Forms.Label lblMaxEmployees;
        private System.Windows.Forms.ComboBox cmbMaxEmployees;
        private System.Windows.Forms.CheckBox chkCookPineappleFirst;
        private System.Windows.Forms.ComboBox cmbMaxFishes;
        private System.Windows.Forms.Label lblMaxFishes;
        private System.Windows.Forms.Panel panelBuyUpdateTackle;
        private System.Windows.Forms.Label lblMaxTackles;
        private System.Windows.Forms.ComboBox cmbMaxTackles;
        private System.Windows.Forms.Panel panelSellProduct;
        private System.Windows.Forms.Panel panelSellProductForbidden;
        private System.Windows.Forms.Button btnSellProductForbiddenList;
        private System.Windows.Forms.Label lblSellProductMaxLimit;
        private System.Windows.Forms.TextBox txtSellProductMaxLimit;
        private System.Windows.Forms.Label lblSellProductMaxLimit2;
        private System.Windows.Forms.Label lblSellProductLowCash;
        private System.Windows.Forms.TextBox txtSellProductLowCashLimit;
        private System.Windows.Forms.CheckBox chkSellProductLowCash;
        private System.Windows.Forms.RadioButton rdbSellAllProducts;
        private System.Windows.Forms.RadioButton rdbSellForbiddenProduct;
        private System.Windows.Forms.CheckBox chkHelpFriend;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblWarningPostCars;
        private System.Windows.Forms.Label lblWarningParkCars;
        private System.Windows.Forms.Label lblWarningRecover;
        private System.Windows.Forms.Label lblWarningBite;
        private System.Windows.Forms.Label lblWarningProtect;
        private System.Windows.Forms.Label lblWarningBuySlave;
        private System.Windows.Forms.Label lblWarningStayHouse;
        private System.Windows.Forms.Label lblWarningRobFriends;
        private System.Windows.Forms.Label lblWarningPresentFruit;
        private System.Windows.Forms.Label lblWarningFarmShared;
        private System.Windows.Forms.Label lblWarningStealFruit;
        private System.Windows.Forms.Label lblWarningStealProduct;
        private System.Windows.Forms.Label lblWarningPresentProduct;
        private System.Windows.Forms.Label lblWarningHelpRanch;
        private System.Windows.Forms.Label lblWarningPresentFish;
        private System.Windows.Forms.Label lblWarningFishing;
        private System.Windows.Forms.Label lblWarningHelpFish;
        private System.Windows.Forms.Label lblWarningPresentFood;
        private System.Windows.Forms.Label lblWarningHire;
        private System.Windows.Forms.Label lblWarningPurchaseFood;
        private System.Windows.Forms.Label lblWarningNotes;
        private System.Windows.Forms.CheckBox chkPresentFruitCheckNum;
        private System.Windows.Forms.TextBox txtPresentFruitNum;
        private System.Windows.Forms.TextBox txtPresentFruitValue;
        private System.Windows.Forms.CheckBox chkPresentFruitCheckValue;
        private System.Windows.Forms.Label lblPresentFruitCheckNum;
        private System.Windows.Forms.Label lblPresentFruitCheckValue;
        private System.Windows.Forms.Label lblPresentProductCheckNum;
        private System.Windows.Forms.TextBox txtPresentProductNum;
        private System.Windows.Forms.CheckBox chkPresentProductCheckNum;
        private System.Windows.Forms.Label lblPresentProductCheckValue;
        private System.Windows.Forms.TextBox txtPresentProductValue;
        private System.Windows.Forms.CheckBox chkPresentProductCheckValue;
        private System.Windows.Forms.CheckBox chkCookMedlarFirst;
        private System.Windows.Forms.CheckBox chkCookCrabFirst;

    }
}