using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

using WeifenLuo.WinFormsUI.Docking;
using Johnny.Kaixin.Helper;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmTaskEditor : FrmBaseCloseMenu
    {
        private string _taskid;
        private string _taskname;
        private string _groupname;
        private Collection<AccountInfo> _accounts;
        private Collection<SeedInfo> _allseedslist;
        private Collection<FruitInfo> _allfruitslist;
        private Collection<DishInfo> _alldisheslist;
        private Collection<FishMaturedInfo> _allfisheslist;
        private Collection<int> _stealforbiddenfruitslist;
        private Collection<int> _sellforbiddenfruitslist;
        private Collection<int> _presentforbiddenfoodlist;
        private Collection<int> _presentforbiddenfishlist;
        private Collection<int> _sellforbiddenfishlist;
        private Collection<ProductInfo> _sellproductforbiddenlist;
        private Collection<ProductInfo> _allproductslist;

        //rich
        private Collection<AssetInfo> _allassetslist;
        private Collection<int> _buyassetslist; //购买资产列表

        public delegate void TaskSavedEventHandler(string taskid, string taskname);
        public event TaskSavedEventHandler taskSaved;

        public FrmTaskEditor()
        {
            InitializeComponent();
            _stealforbiddenfruitslist = new Collection<int>();
            _sellforbiddenfruitslist = new Collection<int>();
            _presentforbiddenfoodlist = new Collection<int>();
            _presentforbiddenfishlist = new Collection<int>();
            _sellforbiddenfishlist = new Collection<int>();
            _sellproductforbiddenlist = new Collection<ProductInfo>();
            _buyassetslist = new Collection<int>();
        }

        protected override string GetPersistString()
        {
            return GetType().ToString() + "," + _taskid + "," + _taskname + "," + _groupname + "," + Text;
        }

        #region FrmTaskEditor_Load
        private void FrmTaskEditor_Load(object sender, EventArgs e)
        {
            try
            {
                BuildCmbMatches();
                BuildCmbSeeds();
                BuildCmbPresentFruitSeedId();
                BuildCmbBuyCalfCustom();
                BuildCmbAnimalProducts();
                BuildCmbBuyFishFishId();
                BuildCmbCookDishes();
                BuildCmbGroup();
                chkForbidden_CheckedChanged(sender, e);
                rdbTiming_CheckedChanged(null, null);
                chkTaskParking_CheckedChanged(sender, e);
                chkTaskBiting_CheckedChanged(sender, e);
                chkTaskSlave_CheckedChanged(sender, e);
                chkTaskHouse_CheckedChanged(sender, e);
                chkTaskGarden_CheckedChanged(null, null);
                chkTaskRanch_CheckedChanged(null, null);
                chkTaskFish_CheckedChanged(null, null);
                chkTaskRich_CheckedChanged(null, null);
                chkTaskCafe_CheckedChanged(null, null);
                chkSendLog_CheckedChanged(null, null);
                //park
                chkOriginateMatch_CheckedChanged(null, null);
                chkStartCar_CheckedChanged(null, null);
                //garden
                chkFarmSelf_CheckedChanged(null, null);
                rdbExpensiveFarmSelf_CheckedChanged(null, null);
                chkFarmShared_CheckedChanged(null, null);
                rdbExpensiveFarmShared_CheckedChanged(null, null);
                chkPresentFruit_CheckedChanged(null, null);
                rdbPresentFruitByPrice_CheckedChanged(null, null);
                chkStealFruit_CheckedChanged(null, null);
                chkBuySeed_CheckedChanged(null, null);
                chkSellFruit_CheckedChanged(null, null);
                //ranch
                chkBuyCalf_CheckedChanged(null, null);
                rdbBuyCalfByPrice_CheckedChanged(null, null);
                chkPresentProduct_CheckedChanged(null, null);
                rdbPresentProductByPrice_CheckedChanged(null, null);
                chkSellProduct_CheckedChanged(null, null);
                rdbSellAllProducts_CheckedChanged(null, null);
                //fish
                chkNetSelfFish_CheckedChanged(null, null);
                chkBuyFish_CheckedChanged(null, null);
                rdbBuyFishByRank_CheckedChanged(null, null);
                chkBuyUpdateTackle_CheckedChanged(null, null);
                chkPresentFish_CheckedChanged(null, null);
                chkSellFish_CheckedChanged(null, null);
                rdbSellAllFish_CheckedChanged(null, null);
                //rich
                chkBuyAsset_CheckedChanged(null, null);
                //cafe
                chkCook_CheckedChanged(null, null);
                chkHire_CheckedChanged(null, null);
                chkPresentFood_CheckedChanged(null, null);
                rdbPresentFoodByCount_CheckedChanged(null, null);
                chkSellFood_CheckedChanged(null, null);
                chkPurchaseFood_CheckedChanged(null, null);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroup.Items.Count == 0 || cmbGroup.Text == string.Empty)
                {
                    MessageBox.Show("请先选择组！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbGroup.Select();
                    return;
                }

                if ((rdbSingleLoop.Checked || rdbMultiLoop.Checked) && (!DataValidation.IsInt32(txtRoundTime.Text)))
                {
                    MessageBox.Show("循环时间必须为整数！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRoundTime.Select();
                    return;
                }

                if (chkForbidden.Checked && timeForbiddenStart.Hour == -1 && timeForbiddenStart.Minute == -1)
                {
                    MessageBox.Show("时间不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timeForbiddenStart.Select();
                    return;
                }

                if (chkForbidden.Checked && (!CheckTimeControl(timeForbiddenStart)))
                {
                    return;
                }

                if (chkForbidden.Checked && timeForbiddenEnd.Hour == -1 && timeForbiddenEnd.Minute == -1)
                {
                    MessageBox.Show("时间不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timeForbiddenEnd.Select();
                    return;
                }

                if (chkForbidden.Checked && (!CheckTimeControl(timeForbiddenEnd)))
                {
                    return;
                }

                if (!CheckTimeControl(timeSelector1))
                {                    
                    return;
                }

                if (!CheckTimeControl(timeSelector2))
                {                   
                    return;
                }

                if (!CheckTimeControl(timeSelector3))
                {                   
                    return;
                }

                if (!CheckTimeControl(timeSelector4))
                {                   
                    return;
                }

                if (!CheckTimeControl(timeSelector5))
                {                    
                    return;
                }

                if (!CheckTimeControl(timeSelector6))
                {                   
                    return;
                }

                if (!CheckTimeControl(timeSelector7))
                {                   
                    return;
                }

                if (!CheckTimeControl(timeSelector8))
                {                    
                    return;
                }

                if (!CheckTimeControl(timeSelector9))
                {                    
                    return;
                }

                if (!CheckTimeControl(timeSelector10))
                {                   
                    return;
                }

                if (chkSendLog.Checked && DataValidation.IsNullOrEmpty(txtReceiverEmail.Text))
                {
                    MessageBox.Show("接收邮箱不能为空！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtReceiverEmail.Select();
                    return;
                }

                if (chkSendLog.Checked && !DataValidation.IsEmail(txtReceiverEmail.Text))
                {
                    MessageBox.Show("Email格式不正确！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtReceiverEmail.Select();
                    return;
                }

                if (chkTaskGarden.Checked && chkPresentFruit.Checked && rdbPresentFruitByPrice.Checked && chkPresentFruitCheckValue.Checked && (!DataValidation.IsInt32(txtPresentFruitValue.Text) || DataConvert.GetInt32(txtPresentFruitValue.Text) < 1))
                {
                    MessageBox.Show("赠送果实的价值必须为大于0的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageGarden;
                    txtPresentFruitValue.Select();
                    return;
                }

                if (chkTaskGarden.Checked && chkPresentFruit.Checked && rdbPresentFruitCustom.Checked && (!DataValidation.IsInt32(txtPresentFruitNum.Text) || DataConvert.GetInt32(txtPresentFruitNum.Text) < 1))
                {
                    MessageBox.Show("赠送果实的数量必须为大于0的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageGarden;
                    txtPresentFruitNum.Select();
                    return;
                }

                if (chkTaskGarden.Checked && (!DataValidation.IsInt32(txtLowCashLimit.Text)))
                {
                    MessageBox.Show("出售果实的现金阀值（单位：万）必须为数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageGarden;
                    txtLowCashLimit.Select();
                    return;
                }

                if (chkTaskGarden.Checked && (!DataValidation.IsInt32(txtMaxSellLimit.Text)))
                {
                    MessageBox.Show("出售的额度（单位：万）必须为数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageGarden;
                    txtMaxSellLimit.Select();
                    return;
                }

                if (chkTaskRanch.Checked && (!DataValidation.IsInt32(txtFoodNum.Text) || DataConvert.GetInt32(txtFoodNum.Text) > 200 || DataConvert.GetInt32(txtFoodNum.Text) < 1))
                {
                    MessageBox.Show("牧草的数量必须为数字，而且在1~200范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRanch;
                    txtFoodNum.Select();
                    return;
                }

                if (chkTaskRanch.Checked && (!DataValidation.IsInt32(txtCarrotNum.Text) || DataConvert.GetInt32(txtCarrotNum.Text) > 200 || DataConvert.GetInt32(txtCarrotNum.Text) < 1))
                {
                    MessageBox.Show("胡萝卜的数量必须为数字，而且在1~200范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRanch;
                    txtCarrotNum.Select();
                    return;
                }

                if (chkTaskRanch.Checked && chkPresentProduct.Checked && rdbPresentProductByPrice.Checked && chkPresentProductCheckValue.Checked && (!DataValidation.IsInt32(txtPresentProductValue.Text) || DataConvert.GetInt32(txtPresentProductValue.Text) < 1))
                {
                    MessageBox.Show("赠送农副产品的价值必须为大于0的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRanch;
                    txtPresentProductValue.Select();
                    return;
                }

                if (chkTaskRanch.Checked && chkPresentProduct.Checked && rdbPresentProductCustom.Checked && (!DataValidation.IsInt32(txtPresentProductNum.Text) || DataConvert.GetInt32(txtPresentProductNum.Text) < 1))
                {
                    MessageBox.Show("赠送农副产品的数量必须为大于0的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRanch;
                    txtPresentProductNum.Select();
                    return;
                }

                if (chkTaskRanch.Checked && (!DataValidation.IsInt32(txtSellProductLowCashLimit.Text)))
                {
                    MessageBox.Show("出售农副产品的现金阀值（单位：万）必须为数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRanch;
                    txtSellProductLowCashLimit.Select();
                    return;
                }

                if (chkTaskRanch.Checked && (!DataValidation.IsInt32(txtSellProductMaxLimit.Text)))
                {
                    MessageBox.Show("出售的额度（单位：万）必须为数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRanch;
                    txtSellProductMaxLimit.Select();
                    return;
                }

                if (chkTaskFish.Checked && chkNetSelfFish.Checked && (!DataValidation.IsInt32(txtNetSelfFishMature.Text) || DataConvert.GetInt32(txtNetSelfFishMature.Text) < 0 || DataConvert.GetInt32(txtNetSelfFishMature.Text) > 100))
                {
                    MessageBox.Show("成熟度必须为数字，而且在0~100范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageFish;
                    txtNetSelfFishMature.Select();
                    return;
                }

                if (chkTaskFish.Checked && chkPresentFish.Checked && (!DataValidation.IsInt32(txtPresentFishValue.Text) || DataConvert.GetInt32(txtPresentFishValue.Text) < 1))
                {
                    MessageBox.Show("鱼的价值必须为大于0的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageFish;
                    txtPresentFishValue.Select();
                    return;
                }

                if (chkTaskFish.Checked && chkSellFish.Checked && (!DataValidation.IsInt32(txtSellFishLowCashLimit.Text)))
                {
                    MessageBox.Show("出售鱼的现金阀值（单位：万）必须为数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageFish;
                    txtSellFishLowCashLimit.Select();
                    return;
                }

                if (chkTaskFish.Checked && chkSellFish.Checked && (!DataValidation.IsInt32(txtSellFishValue.Text) || DataConvert.GetInt32(txtSellFishValue.Text) < 1))
                {
                    MessageBox.Show("鱼的价值必须为大于0的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageFish;
                    txtSellFishValue.Select();
                    return;
                }

                if (chkTaskFish.Checked && chkSellFish.Checked && (!DataValidation.IsInt32(txtSellFishMaxLimit.Text)))
                {
                    MessageBox.Show("出售鱼的额度（单位：万）必须为数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageFish;
                    txtSellFishMaxLimit.Select();
                    return;
                }


                if (chkTaskRich.Checked && chkGiveUpIfRatio.Checked && (!DataValidation.IsInt32(txtGiveUpRatio.Text) || DataConvert.GetInt32(txtGiveUpRatio.Text) < 0 || DataConvert.GetInt32(txtGiveUpRatio.Text) > 100))
                {
                    MessageBox.Show("现金/总资产比必须为数字，而且在0~100范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRich;
                    txtGiveUpRatio.Select();
                    return;
                }

                if (chkTaskRich.Checked && chkGiveUpIfMinimum.Checked && (!DataValidation.IsInt32(txtGiveUpMinimum.Text) || DataConvert.GetInt32(txtGiveUpMinimum.Text) <= 0 || DataConvert.GetInt32(txtGiveUpMinimum.Text) > 1000))
                {
                    MessageBox.Show("连续最小购买数必须为数字，而且在1~1000范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRich;
                    txtGiveUpMinimum.Select();
                    return;
                }

                if (chkTaskRich.Checked && chkGiveUpIfMyAsset.Checked && (!DataValidation.IsInt32(txtGiveUpAssetCount.Text) || DataConvert.GetInt32(txtGiveUpAssetCount.Text) <= 0 || DataConvert.GetInt32(txtGiveUpAssetCount.Text) > 100))
                {
                    MessageBox.Show("资产项目数必须为数字，而且在1~100范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageRich;
                    txtGiveUpAssetCount.Select();
                    return;
                }

                if (chkTaskCafe.Checked && chkCook.Checked && (!DataValidation.IsInt64(txtCookLowCashLimit.Text) || DataConvert.GetInt64(txtCookLowCashLimit.Text) <= 0))
                {
                    MessageBox.Show("炒菜最低现金阀值必须为大于1的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageCafe;
                    txtCookLowCashLimit.Select();
                    return;
                }

                if (chkTaskCafe.Checked && chkPresentFood.Checked && (!DataValidation.IsInt32(txtPresentFoodRatio.Text) || DataConvert.GetInt32(txtPresentFoodRatio.Text) <= 0 || DataConvert.GetInt32(txtPresentFoodRatio.Text) > 100))
                {
                    MessageBox.Show("赠送率必须为数字，而且在1~100范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageCafe;
                    txtPresentFoodRatio.Select();
                    return;
                }

                if (chkTaskCafe.Checked && chkPresentFood.Checked && (!DataValidation.IsInt64(txtPresentLowCashLimit.Text) || DataConvert.GetInt64(txtPresentLowCashLimit.Text) <= 0))
                {
                    MessageBox.Show("赠送最低现金阀值必须为大于1的数字！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageCafe;
                    txtPresentLowCashLimit.Select();
                    return;
                }

                if (chkTaskCafe.Checked && chkPresentFood.Checked && (!DataValidation.IsInt32(txtPresentFoodLowCountLimit.Text) || DataConvert.GetInt32(txtPresentFoodLowCountLimit.Text) <= 0 || DataConvert.GetInt32(txtPresentFoodLowCountLimit.Text) > 20))
                {
                    MessageBox.Show("最低食物种类数必须为数字，而且在1~20范围之内！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGames.SelectedTab = tabPageCafe;
                    txtPresentFoodLowCountLimit.Select();
                    return;
                }

                if (!chkTaskParking.Checked && !chkTaskBiting.Checked && !chkTaskSlave.Checked && !chkTaskHouse.Checked && !chkTaskGarden.Checked && !chkTaskRanch.Checked && !chkTaskFish.Checked && !chkTaskRich.Checked && !chkTaskCafe.Checked)
                {
                    MessageBox.Show("请至少选择一个要执行的操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkTaskParking.Select();
                    return;
                }

                //park
                if (chkTaskParking.Checked)
                {
                    if (!chkParkMyCars.Checked && !chkPostOthersCars.Checked && !chkCheerUp.Checked && !chkJoinMatch.Checked && !chkOriginateMatch.Checked && !chkStartCar.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[争车位]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkParkMyCars.Select();
                        return;
                    }
                }

                //bite
                if (chkTaskBiting.Checked)
                {
                    if (!chkApproveRecovery.Checked && !chkBiteOthers.Checked && !chkAutoRecover.Checked && !chkProtectFriend.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[咬人]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkApproveRecovery.Select();
                        return;
                    }
                }

                //slave
                if (chkTaskSlave.Checked)
                {
                    if (!chkBuySlave.Checked && !chkBuyLowPriceSlave.Checked && !chkFawnMaster.Checked && !chkPropitiateSlave.Checked && !chkAfflictSlave.Checked & !chkReleaseSlave.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[朋友买卖]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkBuySlave.Select();
                        return;
                    }
                }

                //house
                if (chkTaskHouse.Checked)
                {
                    if (!chkDoJob.Checked && !chkStayHouse.Checked && !chkRobFriends.Checked && !chkRobFreeFriends.Checked && !chkDriveFriends.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[买房子]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkDoJob.Select();
                        return;
                    }
                }

                //garden
                if (chkTaskGarden.Checked)
                {
                    if (!chkFarmSelf.Checked && !chkHarvestFruit.Checked && !chkPresentFruit.Checked && !chkSellFruit.Checked && !chkHelpOthers.Checked && !chkFarmShared.Checked && !chkStealFruit.Checked && !chkBuySeed.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[花园]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkFarmSelf.Select();
                        return;
                    }
                }

                //ranch
                if (chkTaskRanch.Checked)
                {
                    if (!chkHarvestProduct.Checked && !chkHarvestAnimal.Checked && !chkAddWater.Checked && !chkHelpAddWater.Checked && !chkAddGrass.Checked && !chkHelpAddGrass.Checked && !chkBuyCalf.Checked && !chkStealProduct.Checked && !chkMakeProduct.Checked && !chkHelpMakeProduct.Checked && !chkBreedAnimal.Checked && !chkPresentProduct.Checked && !chkSellProduct.Checked && !chkAddCarrot.Checked && !chkHelpAddCarrot.Checked && !chkAddBamboo.Checked && !chkHelpAddBamboo.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[牧场]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkAddWater.Select();
                        return;
                    }
                }

                //fish
                if (chkTaskFish.Checked)
                {
                    if (!chkShake.Checked && !chkTreatFish.Checked && !chkUpdateFishPond.Checked && !chkBangKeJing.Checked && !chkBuyFish.Checked && !chkFishing.Checked && !chkBuyUpdateTackle.Checked && !chkHarvestFish.Checked && !chkNetSelfFish.Checked && !chkHelpFish.Checked && !chkPresentFish.Checked && !chkSellFish.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[钓鱼]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkShake.Select();
                        return;
                    }
                }

                //rich
                if (chkTaskRich.Checked)
                {
                    if (!chkSellAsset.Checked && !chkBuyAsset.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[超级大亨]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkSellAsset.Select();
                        return;
                    }
                }

                //cafe
                if (chkTaskCafe.Checked)
                {
                    if (!chkBoxClean.Checked && !chkCook.Checked && !chkHire.Checked && !chkHelpFriend.Checked && !chkPresentFood.Checked && !chkPurchaseFood.Checked && !chkSellFood.Checked)
                    {
                        MessageBox.Show("请至少选择一个要执行的[开心餐厅]操作！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        chkBoxClean.Select();
                        return;
                    }
                }

                TaskInfo taskitem = new TaskInfo();
                //taskitem.
                taskitem.TaskId = _taskid;
                taskitem.TaskName = _taskname;
                taskitem.GroupName = cmbGroup.Text;
                if (rdbSingleLoop.Checked)
                    taskitem.RunMode = EnumRunMode.SingleLoop;
                else if (rdbMultiLoop.Checked)
                    taskitem.RunMode = EnumRunMode.MultiLoop;
                else if (rdbTiming.Checked)
                    taskitem.RunMode = EnumRunMode.Timing;

                taskitem.RoundTime = DataConvert.GetInt32(txtRoundTime.Text);
                taskitem.Forbidden = chkForbidden.Checked;
                taskitem.ForbiddenStart = new TimeInfo(timeForbiddenStart.Hour, timeForbiddenStart.Minute);
                taskitem.ForbiddenEnd = new TimeInfo(timeForbiddenEnd.Hour, timeForbiddenEnd.Minute);

                if (timeSelector1.Hour >= 0 && timeSelector1.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector1.Hour, timeSelector1.Minute));
                if (timeSelector2.Hour >= 0 && timeSelector2.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector2.Hour, timeSelector2.Minute));
                if (timeSelector3.Hour >= 0 && timeSelector3.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector3.Hour, timeSelector3.Minute));
                if (timeSelector4.Hour >= 0 && timeSelector4.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector4.Hour, timeSelector4.Minute));
                if (timeSelector5.Hour >= 0 && timeSelector5.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector5.Hour, timeSelector5.Minute));
                if (timeSelector6.Hour >= 0 && timeSelector6.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector6.Hour, timeSelector6.Minute));
                if (timeSelector7.Hour >= 0 && timeSelector7.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector7.Hour, timeSelector7.Minute));
                if (timeSelector8.Hour >= 0 && timeSelector8.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector8.Hour, timeSelector8.Minute));
                if (timeSelector9.Hour >= 0 && timeSelector9.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector9.Hour, timeSelector9.Minute));
                if (timeSelector10.Hour >= 0 && timeSelector10.Minute >= 0)
                    taskitem.StartTimes.Add(new TimeInfo(timeSelector10.Hour, timeSelector10.Minute));
                if (rdbTiming.Checked && taskitem.StartTimes.Count <= 0)
                {
                    MessageBox.Show("定时执行时，请至少设置一个时间！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timeSelector1.Select();
                    return;
                }

                taskitem.ExecutePark = chkTaskParking.Checked;
                taskitem.ExecuteBite = chkTaskBiting.Checked;
                taskitem.ExecuteSlave = chkTaskSlave.Checked;
                taskitem.ExecuteHouse = chkTaskHouse.Checked;
                taskitem.ExecuteGarden = chkTaskGarden.Checked;
                taskitem.ExecuteRanch = chkTaskRanch.Checked;
                taskitem.ExecuteFish = chkTaskFish.Checked;
                taskitem.ExecuteRich = chkTaskRich.Checked;
                taskitem.ExecuteCafe = chkTaskCafe.Checked;
                taskitem.SendLog = chkSendLog.Checked;
                taskitem.ReceiverEmail = txtReceiverEmail.Text;
                taskitem.WriteLogToFile = chkWriteLogToFile.Checked;
                taskitem.SkipValidation = rdbSkip.Checked;
                //park                
                taskitem.ParkMyCars = chkParkMyCars.Checked;
                taskitem.PostOthersCars = chkPostOthersCars.Checked;
                taskitem.CheerUp = chkCheerUp.Checked;
                taskitem.StartCar = chkStartCar.Checked;
                taskitem.OriginateMatch = chkOriginateMatch.Checked;
                MatchInfo originateMatchId = cmbOriginateMatchId.SelectedItem as MatchInfo;
                if (originateMatchId == null)
                    taskitem.OriginateMatchId = 1;
                else
                    taskitem.OriginateMatchId = originateMatchId.MatchId;
                taskitem.OriginateTeamNum = cmbOriginateTeamNum.SelectedIndex + 2;
                taskitem.JoinMatch = chkJoinMatch.Checked;
                taskitem.StartCarTime = new TimeInfo(timeStartCarTime.Hour, timeStartCarTime.Minute);
                //bite
                taskitem.ApproveRecovery = chkApproveRecovery.Checked;
                taskitem.BiteOthers = chkBiteOthers.Checked;
                taskitem.AutoRecover = chkAutoRecover.Checked;
                taskitem.ProtectFriend = chkProtectFriend.Checked;
                //slave
                taskitem.BuySlave = chkBuySlave.Checked;
                taskitem.BuyLowPriceSlave = chkBuyLowPriceSlave.Checked;
                taskitem.FawnMaster = chkFawnMaster.Checked;
                taskitem.PropitiateSlave = chkPropitiateSlave.Checked;
                taskitem.AfflictSlave = chkAfflictSlave.Checked;
                taskitem.ReleaseSlave = chkReleaseSlave.Checked;
                taskitem.MaxSlaves = cmbMaxSlaves.SelectedIndex + 1;
                taskitem.NickName = txtNickName.Text;
                //house
                taskitem.DoJob = chkDoJob.Checked;
                taskitem.StayHouse = chkStayHouse.Checked;
                taskitem.RobFriends = chkRobFriends.Checked;
                taskitem.RobFreeFriends = chkRobFreeFriends.Checked;
                taskitem.DriveFriends = chkDriveFriends.Checked;
                //garden
                taskitem.FarmSelf = chkFarmSelf.Checked;
                taskitem.ExpensiveFarmSelf = rdbExpensiveFarmSelf.Checked;
                SeedInfo seedCustomFarmSelf = cmbCustomFarmSelf.SelectedItem as SeedInfo;
                if (seedCustomFarmSelf == null)
                    taskitem.CustomFarmSelf = 1;
                else
                    taskitem.CustomFarmSelf = seedCustomFarmSelf.SeedId;
                taskitem.FarmShared = chkFarmShared.Checked;
                taskitem.ExpensiveFarmShared = rdbExpensiveFarmShared.Checked;
                SeedInfo seedCustomFarmShared = cmbCustomFarmShared.SelectedItem as SeedInfo;
                if (seedCustomFarmShared == null)
                    taskitem.CustomFarmShared = 1;
                else
                    taskitem.CustomFarmShared = seedCustomFarmShared.SeedId;
                taskitem.HarvestFruit = chkHarvestFruit.Checked;
                taskitem.PresentFruit = chkPresentFruit.Checked;
                taskitem.PresentFruitByPrice = rdbPresentFruitByPrice.Checked;
                taskitem.PresentFruitCheckValue = chkPresentFruitCheckValue.Checked;
                taskitem.PresentFruitValue = DataConvert.GetInt32(txtPresentFruitValue.Text);
                FruitInfo presentFruit = cmbPresentFruitId.SelectedItem as FruitInfo;
                if (presentFruit == null)
                    taskitem.PresentFruitId = 1;
                else
                    taskitem.PresentFruitId = presentFruit.FruitId;
                taskitem.PresentFruitCheckNum = chkPresentFruitCheckNum.Checked;
                taskitem.PresentFruitNum = DataConvert.GetInt32(txtPresentFruitNum.Text);
                taskitem.SellFruit = chkSellFruit.Checked;
                taskitem.LowCash = chkLowCash.Checked;
                taskitem.LowCashLimit = DataConvert.GetInt32(txtLowCashLimit.Text);
                taskitem.SellAllFruit = rdbSellAllFruit.Checked;
                taskitem.MaxSellLimit = DataConvert.GetInt32(txtMaxSellLimit.Text);
                taskitem.SellForbiddennFruitsList = _sellforbiddenfruitslist;
                taskitem.BuySeed = chkBuySeed.Checked;
                taskitem.BuySeedCount = cmbBuySeedCount.SelectedIndex + 1;
                taskitem.HelpOthers = chkHelpOthers.Checked;
                taskitem.StealFruit = chkStealFruit.Checked;
                //taskitem.StealPrice = DataConvert.GetInt32(JsonHelper.GetMidLast(cmbStealPrice.Text, "(", ")"));
                taskitem.SowMySeedsFirst = chkSowMySeedsFirst.Checked;
                taskitem.StealUnknowFruit = chkStealUnknowFruit.Checked;
                taskitem.StealForbiddenFruitsList = _stealforbiddenfruitslist;

                //ranch
                taskitem.HarvestProduct = chkHarvestProduct.Checked;
                taskitem.HarvestAnimal = chkHarvestAnimal.Checked;
                taskitem.AddWater = chkAddWater.Checked;
                taskitem.HelpAddWater = chkHelpAddWater.Checked;
                taskitem.AddGrass = chkAddGrass.Checked;
                taskitem.HelpAddGrass = chkHelpAddGrass.Checked;
                taskitem.BuyCalf = chkBuyCalf.Checked;
                taskitem.BuyCalfByPrice = rdbBuyCalfByPrice.Checked;
                CalfInfo calfBuyCalfCustom = cmbBuyCalfCustom.SelectedItem as CalfInfo;
                if (calfBuyCalfCustom == null)
                    taskitem.BuyCalfCustom = 1;
                else
                    taskitem.BuyCalfCustom = calfBuyCalfCustom.AId;
                taskitem.StealProduct = chkStealProduct.Checked;
                taskitem.MakeProduct = chkMakeProduct.Checked;
                taskitem.HelpMakeProduct = chkHelpMakeProduct.Checked;
                taskitem.BreedAnimal = chkBreedAnimal.Checked;
                taskitem.FoodNum = DataConvert.GetInt32(txtFoodNum.Text);
                taskitem.PresentProduct = chkPresentProduct.Checked;
                taskitem.PresentProductByPrice = rdbPresentProductByPrice.Checked;
                taskitem.PresentProductCheckValue = chkPresentProductCheckValue.Checked;
                taskitem.PresentProductValue = DataConvert.GetInt32(txtPresentProductValue.Text);
                ProductInfo productPresentProductCustom = cmbAnimalProducts.SelectedItem as ProductInfo;
                if (productPresentProductCustom == null)
                {
                    taskitem.PresentProductAid = 1;
                    taskitem.PresentProductType = 0;
                }
                else
                {
                    taskitem.PresentProductAid = productPresentProductCustom.Aid;
                    taskitem.PresentProductType = productPresentProductCustom.Type;
                }
                taskitem.PresentProductCheckNum = chkPresentProductCheckNum.Checked;
                taskitem.PresentProductNum = DataConvert.GetInt32(txtPresentProductNum.Text);
                taskitem.SellProduct = chkSellProduct.Checked;
                taskitem.SellProductLowCash = chkSellProductLowCash.Checked;
                taskitem.SellProductLowCashLimit = DataConvert.GetInt32(txtSellProductLowCashLimit.Text);
                taskitem.SellAllProducts = rdbSellAllProducts.Checked;
                taskitem.SellProductMaxLimit = DataConvert.GetInt32(txtSellProductMaxLimit.Text);
                taskitem.SellProductForbiddenList = _sellproductforbiddenlist;
                taskitem.AddCarrot = chkAddCarrot.Checked;
                taskitem.HelpAddCarrot = chkHelpAddCarrot.Checked;
                taskitem.CarrotNum = DataConvert.GetInt32(txtCarrotNum.Text);
                taskitem.AddBamboo = chkAddBamboo.Checked;
                taskitem.HelpAddBamboo = chkHelpAddBamboo.Checked;
                taskitem.BambooNum = DataConvert.GetInt32(txtBambooNum.Text);

                //fish
                taskitem.Shake = chkShake.Checked;
                taskitem.TreatFish = chkTreatFish.Checked;
                taskitem.UpdateFishPond = chkUpdateFishPond.Checked;
                taskitem.BangKeJing = chkBangKeJing.Checked;
                taskitem.BuyFish = chkBuyFish.Checked;
                taskitem.MaxFishes = cmbMaxFishes.SelectedIndex + 1;
                taskitem.BuyFishByRank = rdbBuyFishByRank.Checked;
                FishFryInfo fishfryBuyFish = cmbBuyFishFishId.SelectedItem as FishFryInfo;
                if (fishfryBuyFish == null)
                    taskitem.BuyFishFishId = 1;
                else
                    taskitem.BuyFishFishId = fishfryBuyFish.FId;
                taskitem.Fishing = chkFishing.Checked;
                taskitem.BuyUpdateTackle = chkBuyUpdateTackle.Checked;
                taskitem.MaxTackles = cmbMaxTackles.SelectedIndex + 1;
                taskitem.HarvestFish = chkHarvestFish.Checked;
                taskitem.NetSelfFish = chkNetSelfFish.Checked;
                taskitem.NetSelfFishCheap = rdbNetSelfFishCheap.Checked;
                taskitem.NetSelfFishMature = DataConvert.GetInt32(txtNetSelfFishMature.Text);
                taskitem.HelpFish = chkHelpFish.Checked;
                taskitem.PresentFish = chkPresentFish.Checked;
                taskitem.PresentFishCheap = rdbPresentFishCheap.Checked;
                taskitem.PresentFishCheckValue = chkPresentFishCheckValue.Checked;
                taskitem.PresentFishValue = DataConvert.GetInt32(txtPresentFishValue.Text);
                taskitem.PresentFishForbiddenList = _presentforbiddenfishlist;
                taskitem.SellFish = chkSellFish.Checked;
                taskitem.SellFishLowCash = chkSellFishLowCash.Checked;
                taskitem.SellFishLowCashLimit = DataConvert.GetInt32(txtSellFishLowCashLimit.Text);
                taskitem.SellAllFish = rdbSellAllFish.Checked;
                taskitem.SellFishCheckValue = chkSellFishCheckValue.Checked;
                taskitem.SellFishValue = DataConvert.GetInt32(txtSellFishValue.Text);
                taskitem.SellFishMaxLimit = DataConvert.GetInt32(txtSellFishMaxLimit.Text);
                taskitem.SellFishForbiddenList = _sellforbiddenfishlist;

                //rich
                taskitem.SellAsset = chkSellAsset.Checked;
                taskitem.BuyAsset = chkBuyAsset.Checked;
                taskitem.BuyAssetCheap = rdbBuyAssetCheap.Checked;
                taskitem.GiveUpIfRatio = chkGiveUpIfRatio.Checked;
                taskitem.GiveUpRatio = DataConvert.GetInt32(txtGiveUpRatio.Text);
                taskitem.GiveUpIfMinimum = chkGiveUpIfMinimum.Checked;
                taskitem.GiveUpMinimum = DataConvert.GetInt32(txtGiveUpMinimum.Text);
                taskitem.GiveUpIfMyAsset = chkGiveUpIfMyAsset.Checked;
                taskitem.GiveUpAssetCount = DataConvert.GetInt32(txtGiveUpAssetCount.Text);
                taskitem.AdvancedPurchase = chkAdvancedPurchase.Checked;          
                taskitem.BuyAssetsList = _buyassetslist;

                //cafe
                taskitem.BoxClean = chkBoxClean.Checked;
                taskitem.Cook = chkCook.Checked;
                taskitem.CookTomatoFirst = chkCookTomatoFirst.Checked;
                taskitem.CookMedlarFirst = chkCookMedlarFirst.Checked;
                taskitem.CookCrabFirst = chkCookCrabFirst.Checked;
                taskitem.CookPineappleFirst = chkCookPineappleFirst.Checked;
                DishInfo cookDishId = cmbCookDishId.SelectedItem as DishInfo;
                if (cookDishId == null)
                    taskitem.CookDishId = 4;
                else
                    taskitem.CookDishId = cookDishId.DishId;
                taskitem.CookLowCash = chkCookLowCash.Checked;
                taskitem.CookLowCashLimit = DataConvert.GetInt64(txtCookLowCashLimit.Text);
                taskitem.Hire = chkHire.Checked;
                taskitem.MaxEmployees = cmbMaxEmployees.SelectedIndex + 1;
                taskitem.HelpFriend = chkHelpFriend.Checked;
                taskitem.PresentFood = chkPresentFood.Checked;
                taskitem.PresentForbiddenFoodList = _presentforbiddenfoodlist;
                taskitem.PresentFoodByCount = rdbPresentFoodByCount.Checked;
                DishInfo presentDishId = cmbPresentFoodDishId.SelectedItem as DishInfo;
                if (presentDishId == null)
                    taskitem.PresentFoodDishId = 4;
                else
                    taskitem.PresentFoodDishId = presentDishId.DishId;
                taskitem.PresentFoodRatio = DataConvert.GetInt32(txtPresentFoodRatio.Text);
                taskitem.PresentFoodMessage = DataConvert.GetString(txtPresentFoodMessage.Text);
                taskitem.PresentLowCash = chkPresentLowCash.Checked;
                taskitem.PresentLowCashLimit = DataConvert.GetInt64(txtPresentLowCashLimit.Text);
                taskitem.PresentFoodLowCount = chkPresentFoodLowCount.Checked;
                taskitem.PresentFoodLowCountLimit = DataConvert.GetInt32(txtPresentFoodLowCountLimit.Text);
                taskitem.SellFood = chkSellFood.Checked;
                taskitem.SellFoodByRefPrice = rdbSellFoodByRefPrice.Checked;
                taskitem.PurchaseFood = chkPurchaseFood.Checked;
                taskitem.PurchaseFoodByRefPrice = rdbPurchaseFoodByRefPrice.Checked;

                //accounts
                foreach (object item in lstSelectedAccounts.Items)
                {
                    AccountInfo user = item as AccountInfo;
                    if (user != null)
                    {
                        taskitem.Accounts.Add(user);
                    }
                }

                if (rdbSingleLoop.Checked && taskitem.Accounts.Count!=1)
                {
                    MessageBox.Show("运行模式是单账号循环时，只能选择一个账号！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lstSelectedAccounts.Select();
                    return;
                }

                if (rdbSingleLoop.Checked && taskitem.RoundTime > 15)
                {
                    MessageBox.Show("运行模式是单账号循环时，循环时间不能超过15分钟！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRoundTime.Select();
                    return;
                }

                if (ConfigCtrl.SetTask(_taskid, _taskname, taskitem))
                    MessageBox.Show("配置信息保存成功！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("配置信息保存失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (taskSaved != null)
                    taskSaved(_taskid, _taskname);
                
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region CheckTimeControl
        private bool CheckTimeControl(Johnny.Controls.Windows.DateTimer.TimeSelector timer)
        {
            if (timer.Hour == -1 && timer.Minute == -1)
                return true;
            else if (timer.Hour >= 0 && timer.Minute >= 0)
                return true;

            MessageBox.Show("时间设定不正确！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            timer.Select();
            return false;
        }

        #endregion

        #region btnReload_Click
        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTaskConfig();
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region list Select Event
        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllAccounts.SelectedItems)
                {
                    lstSelectedAccounts.Items.Add(item);
                }
                for (int ix = lstAllAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstAllAccounts.Items.Remove(lstAllAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstAllAccounts.Items)
                {
                    lstSelectedAccounts.Items.Add(item);
                }
                for (int ix = lstAllAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstAllAccounts.Items.Remove(lstAllAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void btnUnselectOne_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstSelectedAccounts.SelectedItems)
                {
                    lstAllAccounts.Items.Add(item);
                }
                for (int ix = lstSelectedAccounts.SelectedItems.Count - 1; ix >= 0; ix--)
                    lstSelectedAccounts.Items.Remove(lstSelectedAccounts.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in lstSelectedAccounts.Items)
                {
                    lstAllAccounts.Items.Add(item);
                }
                for (int ix = lstSelectedAccounts.Items.Count - 1; ix >= 0; ix--)
                    lstSelectedAccounts.Items.Remove(lstSelectedAccounts.Items[0]);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion                

        #region BuildCmbMatches
        private void BuildCmbMatches()
        {
            cmbOriginateMatchId.Items.Clear();

            Collection<MatchInfo> matches = ConfigCtrl.GetMatches();
            if (matches != null)
            {
                foreach (MatchInfo match in matches)
                {
                    cmbOriginateMatchId.Items.Add(match);
                }
            }
            if (cmbOriginateMatchId.Items.Count > 0)
                cmbOriginateMatchId.SelectedIndex = 0;
        }
        #endregion

        #region BuildCmbSeeds
        private void BuildCmbSeeds()
        {
            cmbCustomFarmSelf.Items.Clear();
            cmbCustomFarmShared.Items.Clear();            
            
            Collection<SeedInfo> seeds = ConfigCtrl.GetSeedsInShop();
            if (seeds != null)
            {
                foreach (SeedInfo seed in seeds)
                {
                    cmbCustomFarmSelf.Items.Add(seed);
                    cmbCustomFarmShared.Items.Add(seed);
                }
            }
            if (cmbCustomFarmSelf.Items.Count > 0)
                cmbCustomFarmSelf.SelectedIndex = 1;
            if (cmbCustomFarmShared.Items.Count > 0)
                cmbCustomFarmShared.SelectedIndex = 1;
           
        }
        #endregion
        
        #region BuildCmbPresentFruitSeedId
        private void BuildCmbPresentFruitSeedId()
        {
            cmbPresentFruitId.Items.Clear();

            Collection<FruitInfo> fruits = ConfigCtrl.GetFruits();
            if (fruits != null)
            {
                foreach (FruitInfo fruit in fruits)
                {
                    cmbPresentFruitId.Items.Add(fruit);
                }
            }
        }
        #endregion

        #region BuildCmbBuyFishFishId
        private void BuildCmbBuyFishFishId()
        {
            cmbBuyFishFishId.Items.Clear();

            Collection<FishFryInfo> fishfrys = ConfigCtrl.GetFishFrysInShop();
            if (fishfrys != null)
            {
                foreach (FishFryInfo fishfry in fishfrys)
                {
                    cmbBuyFishFishId.Items.Add(fishfry);
                }
            }
        }
        #endregion

        #region BuildCmbBuyCalfCustom
        private void BuildCmbBuyCalfCustom()
        {
            cmbBuyCalfCustom.Items.Clear();

            Collection<CalfInfo> calves = ConfigCtrl.GetCalvesInShop();
            if (calves != null)
            {
                foreach (CalfInfo calf in calves)
                {
                    cmbBuyCalfCustom.Items.Add(calf);
                }
            }
        }
        #endregion

        #region BuildCmbAnimalProducts
        private void BuildCmbAnimalProducts()
        {
            cmbAnimalProducts.Items.Clear();

            Collection<ProductInfo> products = ConfigCtrl.GetAnimalProducts();
            if (products != null)
            {
                foreach (ProductInfo product in products)
                {
                    cmbAnimalProducts.Items.Add(product);
                }
            }
        }
        #endregion

        #region BuildCmbCookDishes
        private void BuildCmbCookDishes()
        {
            cmbCookDishId.Items.Clear();
            cmbPresentFoodDishId.Items.Clear();

            Collection<DishInfo> dishes = ConfigCtrl.GetDishesInMenu();
            if (dishes != null)
            {
                foreach (DishInfo dish in dishes)
                {
                    cmbPresentFoodDishId.Items.Add(dish);
                    cmbCookDishId.Items.Add(dish);
                }
            }
        }
        #endregion

        #region BuildCmbGroup
        private void BuildCmbGroup()
        {
            string[] groups = ConfigCtrl.GetGroups();
            if (groups!=null)
            {
                foreach (string group in groups)
                {
                    cmbGroup.Items.Add(group);
                }
            }
            
            LoadTaskConfig();
        }
        #endregion
        
        #region LoadTaskConfig
        private void LoadTaskConfig()
        {
            if (!String.IsNullOrEmpty(_groupname))
            {
                for (int ix = 0; ix < cmbGroup.Items.Count; ix++)
                {
                    if (cmbGroup.Items[ix].ToString() == _groupname)
                        cmbGroup.SelectedIndex = ix;
                }
            }
            else if (cmbGroup.Items.Count > 0)
                cmbGroup.SelectedIndex = 0;

            timeSelector1.Clear();
            timeSelector2.Clear();
            timeSelector3.Clear();
            timeSelector4.Clear();
            timeSelector5.Clear();
            timeSelector6.Clear();
            timeSelector7.Clear();
            timeSelector8.Clear();
            timeSelector9.Clear();
            timeSelector10.Clear();

            //load config info            
            TaskInfo taskitem = ConfigCtrl.GetTask(_taskid, _taskname, false);
            if (taskitem == null)
            {
                MessageBox.Show("读取任务配置文件失败！", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (taskitem.RunMode == EnumRunMode.SingleLoop)
            {
                rdbSingleLoop.Checked = true;
                rdbMultiLoop.Checked = false;
                rdbTiming.Checked = false;
            }
            else if (taskitem.RunMode == EnumRunMode.MultiLoop)
            {
                rdbSingleLoop.Checked = false;
                rdbMultiLoop.Checked = true;
                rdbTiming.Checked = false;
            }
            else
            {
                rdbSingleLoop.Checked = false;
                rdbMultiLoop.Checked = false;
                rdbTiming.Checked = true;
            }

            for (int ix = 0; ix < taskitem.StartTimes.Count; ix++)
            {
                TimeInfo time = taskitem.StartTimes[ix];
                switch (ix)
                {
                    case 0:
                        timeSelector1.Hour = time.Hour;
                        timeSelector1.Minute = time.Minute;
                        break;
                    case 1:
                        timeSelector2.Hour = time.Hour;
                        timeSelector2.Minute = time.Minute;
                        break;
                    case 2:
                        timeSelector3.Hour = time.Hour;
                        timeSelector3.Minute = time.Minute;
                        break;
                    case 3:
                        timeSelector4.Hour = time.Hour;
                        timeSelector4.Minute = time.Minute;
                        break;
                    case 4:
                        timeSelector5.Hour = time.Hour;
                        timeSelector5.Minute = time.Minute;
                        break;
                    case 5:
                        timeSelector6.Hour = time.Hour;
                        timeSelector6.Minute = time.Minute;
                        break;
                    case 6:
                        timeSelector7.Hour = time.Hour;
                        timeSelector7.Minute = time.Minute;
                        break;
                    case 7:
                        timeSelector8.Hour = time.Hour;
                        timeSelector8.Minute = time.Minute;
                        break;
                    case 8:
                        timeSelector9.Hour = time.Hour;
                        timeSelector9.Minute = time.Minute;
                        break;
                    case 9:
                        timeSelector10.Hour = time.Hour;
                        timeSelector10.Minute = time.Minute;
                        break;
                }
            }
            txtRoundTime.Text = taskitem.RoundTime.ToString();
            chkForbidden.Checked = taskitem.Forbidden;
            timeForbiddenStart.Hour = taskitem.ForbiddenStart.Hour;
            timeForbiddenStart.Minute = taskitem.ForbiddenStart.Minute;
            timeForbiddenEnd.Hour = taskitem.ForbiddenEnd.Hour;
            timeForbiddenEnd.Minute = taskitem.ForbiddenEnd.Minute;
            chkTaskParking.Checked = taskitem.ExecutePark;
            chkTaskBiting.Checked = taskitem.ExecuteBite;
            chkTaskSlave.Checked = taskitem.ExecuteSlave;
            chkTaskHouse.Checked = taskitem.ExecuteHouse;
            chkTaskGarden.Checked = taskitem.ExecuteGarden;
            chkTaskRanch.Checked = taskitem.ExecuteRanch;
            chkTaskFish.Checked = taskitem.ExecuteFish;
            chkTaskRich.Checked = taskitem.ExecuteRich;
            chkTaskCafe.Checked = taskitem.ExecuteCafe;
            chkSendLog.Checked = taskitem.SendLog;
            txtReceiverEmail.Text = taskitem.ReceiverEmail;
            chkWriteLogToFile.Checked = taskitem.WriteLogToFile;
            rdbSkip.Checked = taskitem.SkipValidation;
            rdbPopupValidationWindow.Checked = !taskitem.SkipValidation;
            //park
            chkParkMyCars.Checked = taskitem.ParkMyCars;
            chkPostOthersCars.Checked = taskitem.PostOthersCars;
            chkCheerUp.Checked = taskitem.CheerUp;
            chkStartCar.Checked = taskitem.StartCar;
            chkOriginateMatch.Checked = taskitem.OriginateMatch;
            for (int ix = 0; ix < cmbOriginateMatchId.Items.Count; ix++)
            {
                MatchInfo match = cmbOriginateMatchId.Items[ix] as MatchInfo;
                if (match != null && match.MatchId == taskitem.OriginateMatchId)
                {
                    cmbOriginateMatchId.SelectedItem = cmbOriginateMatchId.Items[ix];
                    break;
                }
            }
            cmbOriginateTeamNum.SelectedIndex = taskitem.OriginateTeamNum - 2;
            chkJoinMatch.Checked = taskitem.JoinMatch;
            timeStartCarTime.Hour = taskitem.StartCarTime.Hour;
            timeStartCarTime.Minute = taskitem.StartCarTime.Minute;
            //bite
            chkApproveRecovery.Checked = taskitem.ApproveRecovery;
            chkBiteOthers.Checked = taskitem.BiteOthers;
            chkAutoRecover.Checked = taskitem.AutoRecover;
            chkProtectFriend.Checked = taskitem.ProtectFriend;
            //slave
            chkBuySlave.Checked = taskitem.BuySlave;
            chkBuyLowPriceSlave.Checked = taskitem.BuyLowPriceSlave;
            chkFawnMaster.Checked = taskitem.FawnMaster;
            chkPropitiateSlave.Checked = taskitem.PropitiateSlave;
            chkAfflictSlave.Checked = taskitem.AfflictSlave;
            chkReleaseSlave.Checked = taskitem.ReleaseSlave;
            cmbMaxSlaves.SelectedIndex = taskitem.MaxSlaves - 1;
            txtNickName.Text = taskitem.NickName;
            //house
            chkDoJob.Checked = taskitem.DoJob;
            chkStayHouse.Checked = taskitem.StayHouse;
            chkRobFriends.Checked = taskitem.RobFriends;
            chkRobFreeFriends.Checked = taskitem.RobFreeFriends;
            chkDriveFriends.Checked = taskitem.DriveFriends;
            //garden
            chkFarmSelf.Checked = taskitem.FarmSelf;
            rdbExpensiveFarmSelf.Checked = taskitem.ExpensiveFarmSelf;
            rdbCustomFarmSelf.Checked = !taskitem.ExpensiveFarmSelf;
            for (int ix = 0; ix < cmbCustomFarmSelf.Items.Count; ix++)
            {
                SeedInfo seed = cmbCustomFarmSelf.Items[ix] as SeedInfo;
                if (seed != null && seed.SeedId == taskitem.CustomFarmSelf)
                {
                    cmbCustomFarmSelf.SelectedItem = cmbCustomFarmSelf.Items[ix];
                    break;
                }
            }
            chkFarmShared.Checked = taskitem.FarmShared;
            rdbExpensiveFarmShared.Checked = taskitem.ExpensiveFarmShared;
            rdbCustomFarmShared.Checked = !taskitem.ExpensiveFarmShared;
            for (int ix = 0; ix < cmbCustomFarmShared.Items.Count; ix++)
            {
                SeedInfo seed = cmbCustomFarmShared.Items[ix] as SeedInfo;
                if (seed != null && seed.SeedId == taskitem.CustomFarmShared)
                {
                    cmbCustomFarmShared.SelectedItem = cmbCustomFarmShared.Items[ix];
                    break;
                }
            }
            chkHarvestFruit.Checked = taskitem.HarvestFruit;
            chkPresentFruit.Checked = taskitem.PresentFruit;
            rdbPresentFruitByPrice.Checked = taskitem.PresentFruitByPrice;
            chkPresentFruitCheckValue.Checked = taskitem.PresentFruitCheckValue;
            txtPresentFruitValue.Text = DataConvert.GetString(taskitem.PresentFruitValue);
            rdbPresentFruitCustom.Checked = !taskitem.PresentFruitByPrice;
            for (int ix = 0; ix < cmbPresentFruitId.Items.Count; ix++)
            {
                FruitInfo fruit = cmbPresentFruitId.Items[ix] as FruitInfo;
                if (fruit != null && fruit.FruitId == taskitem.PresentFruitId)
                {
                    cmbPresentFruitId.SelectedItem = cmbPresentFruitId.Items[ix];
                    break;
                }
            }
            chkPresentFruitCheckNum.Checked = taskitem.PresentFruitCheckNum;
            txtPresentFruitNum.Text = DataConvert.GetString(taskitem.PresentFruitNum);
            chkSellFruit.Checked = taskitem.SellFruit;
            chkLowCash.Checked = taskitem.LowCash;
            txtLowCashLimit.Text = DataConvert.GetString(taskitem.LowCashLimit);
            rdbSellAllFruit.Checked = taskitem.SellAllFruit;
            rdbFobiddenFruit.Checked = !taskitem.SellAllFruit;
            txtMaxSellLimit.Text = DataConvert.GetString(taskitem.MaxSellLimit);
            _sellforbiddenfruitslist = taskitem.SellForbiddennFruitsList;
            chkBuySeed.Checked = taskitem.BuySeed;
            cmbBuySeedCount.SelectedIndex = taskitem.BuySeedCount - 1;
            chkHelpOthers.Checked = taskitem.HelpOthers;
            chkStealFruit.Checked = taskitem.StealFruit;
            chkStealUnknowFruit.Checked = taskitem.StealUnknowFruit;
            //偷果实
            //foreach (SeedInfo seed in ConfigCtrl.GetSeedsInShop(true))
            //{
            //    cmbStealPrice.Items.Add(seed.Name + "(" + seed.SellPrice.ToString() + ")");
            //    if (seed.SellPrice == taskitem.StealPrice)
            //        cmbStealPrice.SelectedItem = cmbStealPrice.Items[cmbStealPrice.Items.Count - 1];
            //}
            //if (cmbStealPrice.SelectedIndex < 0)
            //    cmbStealPrice.SelectedIndex = 1;
            chkSowMySeedsFirst.Checked = taskitem.SowMySeedsFirst;

            //ranch
            chkHarvestProduct.Checked = taskitem.HarvestProduct;
            chkHarvestAnimal.Checked = taskitem.HarvestAnimal;
            chkAddWater.Checked = taskitem.AddWater;
            chkHelpAddWater.Checked = taskitem.HelpAddWater;
            chkAddGrass.Checked = taskitem.AddGrass;
            chkHelpAddGrass.Checked = taskitem.HelpAddGrass;
            chkBuyCalf.Checked = taskitem.BuyCalf;
            rdbBuyCalfByPrice.Checked = taskitem.BuyCalfByPrice;
            rdbBuyCalfCustom.Checked = !taskitem.BuyCalfByPrice;
            for (int ix = 0; ix < cmbBuyCalfCustom.Items.Count; ix++)
            {
                CalfInfo calf = cmbBuyCalfCustom.Items[ix] as CalfInfo;
                if (calf != null && calf.AId == taskitem.BuyCalfCustom)
                {
                    cmbBuyCalfCustom.SelectedItem = cmbBuyCalfCustom.Items[ix];
                    break;
                }
            }
            chkStealProduct.Checked = taskitem.StealProduct;
            chkMakeProduct.Checked = taskitem.MakeProduct;
            chkHelpMakeProduct.Checked = taskitem.HelpMakeProduct;
            chkBreedAnimal.Checked = taskitem.BreedAnimal;
            txtFoodNum.Text = DataConvert.GetString(taskitem.FoodNum);
            chkPresentProduct.Checked = taskitem.PresentProduct;
            rdbPresentProductByPrice.Checked = taskitem.PresentProductByPrice;
            chkPresentProductCheckValue.Checked = taskitem.PresentProductCheckValue;
            txtPresentProductValue.Text = DataConvert.GetString(taskitem.PresentProductValue);
            rdbPresentProductCustom.Checked = !taskitem.PresentProductByPrice;
            for (int ix = 0; ix < cmbAnimalProducts.Items.Count; ix++)
            {
                ProductInfo product = cmbAnimalProducts.Items[ix] as ProductInfo;
                if (product != null && product.Aid == taskitem.PresentProductAid && product.Type == taskitem.PresentProductType)
                {
                    cmbAnimalProducts.SelectedItem = cmbAnimalProducts.Items[ix];
                    break;
                }
            }
            chkPresentProductCheckNum.Checked = taskitem.PresentProductCheckNum;
            txtPresentProductNum.Text = DataConvert.GetString(taskitem.PresentProductNum);
            chkSellProduct.Checked = taskitem.SellProduct;
            chkSellProductLowCash.Checked = taskitem.SellProductLowCash;
            txtSellProductLowCashLimit.Text = DataConvert.GetString(taskitem.SellProductLowCashLimit);
            rdbSellAllProducts.Checked = taskitem.SellAllProducts;
            rdbSellForbiddenProduct.Checked = !taskitem.SellAllProducts;
            txtSellProductMaxLimit.Text = DataConvert.GetString(taskitem.SellProductMaxLimit);
            _sellproductforbiddenlist = taskitem.SellProductForbiddenList;
            _allproductslist = ConfigCtrl.GetAnimalProducts();
            chkAddCarrot.Checked = taskitem.AddCarrot;
            chkHelpAddCarrot.Checked = taskitem.HelpAddCarrot;
            txtCarrotNum.Text = DataConvert.GetString(taskitem.CarrotNum);
            chkAddBamboo.Checked = taskitem.AddBamboo;
            chkHelpAddBamboo.Checked = taskitem.HelpAddBamboo;
            txtBambooNum.Text = DataConvert.GetString(taskitem.BambooNum);
            //steal seeds
            _allseedslist = ConfigCtrl.GetSeedsInShop();
            _allfruitslist = ConfigCtrl.GetFruits();
            _stealforbiddenfruitslist = taskitem.StealForbiddenFruitsList;

            //fish
            chkShake.Checked = taskitem.Shake;
            chkTreatFish.Checked = taskitem.TreatFish;
            chkUpdateFishPond.Checked = taskitem.UpdateFishPond;
            chkBangKeJing.Checked = taskitem.BangKeJing;
            chkBuyFish.Checked = taskitem.BuyFish;
            cmbMaxFishes.SelectedIndex = taskitem.MaxFishes - 1;
            rdbBuyFishByRank.Checked = taskitem.BuyFishByRank;
            rdbBuyFishCustom.Checked = !taskitem.BuyFishByRank;
            for (int ix = 0; ix < cmbBuyFishFishId.Items.Count; ix++)
            {
                FishFryInfo fishfry = cmbBuyFishFishId.Items[ix] as FishFryInfo;
                if (fishfry != null && fishfry.FId == taskitem.BuyFishFishId)
                {
                    cmbBuyFishFishId.SelectedItem = cmbBuyFishFishId.Items[ix];
                    break;
                }
            }
            chkFishing.Checked = taskitem.Fishing;
            chkBuyUpdateTackle.Checked = taskitem.BuyUpdateTackle;
            cmbMaxTackles.SelectedIndex = taskitem.MaxTackles - 1;
            chkHarvestFish.Checked = taskitem.HarvestFish;
            chkNetSelfFish.Checked = taskitem.NetSelfFish;
            rdbNetSelfFishCheap.Checked = taskitem.NetSelfFishCheap;
            rdbNetSelfFishExpensive.Checked = !taskitem.NetSelfFishCheap;
            txtNetSelfFishMature.Text = DataConvert.GetString(taskitem.NetSelfFishMature);
            chkHelpFish.Checked = taskitem.HelpFish;
            chkPresentFish.Checked = taskitem.PresentFish;
            rdbPresentFishCheap.Checked = taskitem.PresentFishCheap;
            rdbPresentFishExpensive.Checked = !taskitem.PresentFishCheap;
            chkPresentFishCheckValue.Checked = taskitem.PresentFishCheckValue;
            txtPresentFishValue.Text = DataConvert.GetString(taskitem.PresentFishValue);
            _presentforbiddenfishlist = taskitem.PresentFishForbiddenList;
            chkSellFish.Checked = taskitem.SellFish;
            chkSellFishLowCash.Checked = taskitem.SellFishLowCash;
            txtSellFishLowCashLimit.Text = DataConvert.GetString(taskitem.SellFishLowCashLimit);
            rdbSellAllFish.Checked = taskitem.SellAllFish;
            rdbForbiddenFish.Checked = !taskitem.SellAllFish;
            chkSellFishCheckValue.Checked = taskitem.SellFishCheckValue;
            txtSellFishValue.Text = DataConvert.GetString(taskitem.SellFishValue);
            txtSellFishMaxLimit.Text = DataConvert.GetString(taskitem.SellFishMaxLimit);
            _sellforbiddenfishlist = taskitem.SellFishForbiddenList;

            //rich
            chkSellAsset.Checked = taskitem.SellAsset;
            chkBuyAsset.Checked = taskitem.BuyAsset;
            if (taskitem.BuyAssetCheap)
            {
                rdbBuyAssetCheap.Checked = true;
                rdbBuyAssetExpensive.Checked = false;
            }
            else
            {
                rdbBuyAssetCheap.Checked = false;
                rdbBuyAssetExpensive.Checked = true;
            }
            chkGiveUpIfRatio.Checked = taskitem.GiveUpIfRatio;
            txtGiveUpRatio.Text = DataConvert.GetString(taskitem.GiveUpRatio);
            chkGiveUpIfMinimum.Checked = taskitem.GiveUpIfMinimum;
            txtGiveUpMinimum.Text = DataConvert.GetString(taskitem.GiveUpMinimum);
            chkGiveUpIfMyAsset.Checked = taskitem.GiveUpIfMyAsset;
            txtGiveUpAssetCount.Text = DataConvert.GetString(taskitem.GiveUpAssetCount);
            chkAdvancedPurchase.Checked = taskitem.AdvancedPurchase;
            _allassetslist = ConfigCtrl.GetAssetsInShop();
            _buyassetslist = taskitem.BuyAssetsList;

            //cafe
            chkBoxClean.Checked = taskitem.BoxClean;
            chkCook.Checked = taskitem.Cook;
            chkCookTomatoFirst.Checked = taskitem.CookTomatoFirst;
            chkCookMedlarFirst.Checked = taskitem.CookMedlarFirst;
            chkCookCrabFirst.Checked = taskitem.CookCrabFirst;
            chkCookPineappleFirst.Checked = taskitem.CookPineappleFirst;
            for (int ix = 0; ix < cmbCookDishId.Items.Count; ix++)
            {
                DishInfo dish = cmbCookDishId.Items[ix] as DishInfo;
                if (dish != null && dish.DishId == taskitem.CookDishId)
                {
                    cmbCookDishId.SelectedItem = cmbCookDishId.Items[ix];
                    break;
                }
            }
            chkCookLowCash.Checked = taskitem.CookLowCash;
            txtCookLowCashLimit.Text = DataConvert.GetString(taskitem.CookLowCashLimit);
            chkHire.Checked = taskitem.Hire;
            cmbMaxEmployees.SelectedIndex = taskitem.MaxEmployees - 1;
            chkHelpFriend.Checked = taskitem.HelpFriend;
            chkPresentFood.Checked = taskitem.PresentFood;
            _presentforbiddenfoodlist = taskitem.PresentForbiddenFoodList;
            rdbPresentFoodByCount.Checked = taskitem.PresentFoodByCount;
            rdbPresentFoodDishId.Checked = !taskitem.PresentFoodByCount;
            for (int ix = 0; ix < cmbPresentFoodDishId.Items.Count; ix++)
            {
                DishInfo dish = cmbPresentFoodDishId.Items[ix] as DishInfo;
                if (dish != null && dish.DishId == taskitem.PresentFoodDishId)
                {
                    cmbPresentFoodDishId.SelectedItem = cmbPresentFoodDishId.Items[ix];
                    break;
                }
            }
            txtPresentFoodRatio.Text = DataConvert.GetString(taskitem.PresentFoodRatio);
            txtPresentFoodMessage.Text = DataConvert.GetString(taskitem.PresentFoodMessage);
            chkPresentLowCash.Checked = taskitem.PresentLowCash;
            txtPresentLowCashLimit.Text = DataConvert.GetString(taskitem.PresentLowCashLimit);
            chkPresentFoodLowCount.Checked = taskitem.PresentFoodLowCount;
            txtPresentFoodLowCountLimit.Text = DataConvert.GetString(taskitem.PresentFoodLowCountLimit);
            chkSellFood.Checked = taskitem.SellFood;
            rdbSellFoodByRefPrice.Checked = taskitem.SellFoodByRefPrice;
            rdbSellFoodIgnorePrice.Checked = !taskitem.SellFoodByRefPrice;
            chkPurchaseFood.Checked = taskitem.PurchaseFood;
            rdbPurchaseFoodByRefPrice.Checked = taskitem.PurchaseFoodByRefPrice;
            rdbPurchaseFoodIgnorePrice.Checked = !taskitem.PurchaseFoodByRefPrice;
            _alldisheslist = ConfigCtrl.GetDishesInMenu();
            _allfisheslist = ConfigCtrl.GetFishMaturedInMarket();

            //account
            if (!String.IsNullOrEmpty(taskitem.GroupName))
                _accounts = ConfigCtrl.GetAccounts(taskitem.GroupName);
            else if (cmbGroup.Items.Count > 0 && cmbGroup.SelectedText != string.Empty)
                _accounts = ConfigCtrl.GetAccounts(cmbGroup.SelectedText);
            else
                return;

            lstAllAccounts.Items.Clear();
            lstSelectedAccounts.Items.Clear();

            if (_accounts != null)
            {
                lstAllAccounts.Items.Clear();
                bool isAdded;
                foreach (AccountInfo user in _accounts)
                {
                    isAdded = false;
                    if (taskitem.Accounts != null)
                    {
                        foreach (AccountInfo selecteditem in taskitem.Accounts)
                        {
                            if (selecteditem.Email == user.Email && selecteditem.Password == user.Password && selecteditem.UserName == user.UserName && selecteditem.UserId == user.UserId)
                            {
                                lstSelectedAccounts.Items.Add(user);
                                isAdded = true;
                                break;
                            }
                        }
                    }

                    if (!isAdded)
                        lstAllAccounts.Items.Add(user);
                }
            }
        }
        #endregion        

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroup.Items.Count > 0 && cmbGroup.Text != string.Empty)
            {
                _accounts = ConfigCtrl.GetAccounts(cmbGroup.Text);
                lstAllAccounts.Items.Clear();
                lstSelectedAccounts.Items.Clear();
                if (_accounts != null)
                {
                    foreach (AccountInfo user in _accounts)
                    {
                        lstAllAccounts.Items.Add(user);
                    }
                }
            }
        }
        #endregion

        #region rdbTiming_CheckedChanged
        private void rdbTiming_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpLoop.Enabled = !rdbTiming.Checked;
                groupTiming.Enabled = rdbTiming.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbTiming_CheckedChanged", ex);
            }
        }
        #endregion

        #region chkForbidden_CheckedChanged
        private void chkForbidden_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblStart.Enabled = chkForbidden.Checked;
                lblEnd.Enabled = chkForbidden.Checked;
                timeForbiddenStart.Enabled = chkForbidden.Checked;
                timeForbiddenEnd.Enabled = chkForbidden.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region chkSendLog_CheckedChanged
        private void chkSendLog_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReceiverEmail.Enabled = chkSendLog.Checked;
                txtReceiverEmail.Enabled = chkSendLog.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion
        
        #region chkFarmShared_CheckedChanged
        private void chkFarmSelf_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelFarmSelf.Enabled = chkFarmSelf.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region chkFarmShared_CheckedChanged
        private void chkFarmShared_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelFarmShared.Enabled = chkFarmShared.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region chkStealFruit_CheckedChanged
        private void chkStealFruit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkStealUnknowFruit.Enabled = chkStealFruit.Checked;
                btnSetFruitList.Enabled = chkStealFruit.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region chkBuySeed_CheckedChanged
        private void chkBuySeed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblSeedsCount.Enabled = chkBuySeed.Checked;
                cmbBuySeedCount.Enabled = chkBuySeed.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }
        #endregion

        #region btnSetFruitList_Click
        private void btnSetFruitList_Click(object sender, EventArgs e)
        {
            try
            {
                DlgStealFruitSelection dlgstealfruits = new DlgStealFruitSelection();
                dlgstealfruits.Caption = "选择偷窃的果实";
                dlgstealfruits.SelectedTitle = "禁止偷以下列表中的果实：";
                dlgstealfruits.SelectedFruits = _stealforbiddenfruitslist;
                dlgstealfruits.AllFruits = _allfruitslist;
                if (dlgstealfruits.ShowDialog() == DialogResult.OK)
                {
                    _stealforbiddenfruitslist = dlgstealfruits.SelectedFruits;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.btnSetFruitList_Click", ex);
            }
        }
        #endregion

        #region btnForbiddenFruitList_Click
        private void btnForbiddenFruitList_Click(object sender, EventArgs e)
        {
            try
            {
                DlgStealFruitSelection dlgstealfruits = new DlgStealFruitSelection();
                dlgstealfruits.Caption = "选择禁止出售的果实";
                dlgstealfruits.SelectedTitle = "禁止出售以下列表中的果实：";
                dlgstealfruits.SelectedFruits = _sellforbiddenfruitslist;
                dlgstealfruits.AllFruits = _allfruitslist;
                if (dlgstealfruits.ShowDialog() == DialogResult.OK)
                {
                    _sellforbiddenfruitslist = dlgstealfruits.SelectedFruits;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.btnForbiddenFruitList_Click", ex);
            }
        }
        #endregion

        #region Task CheckBox
        private void chkTaskParking_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpPark.Enabled = chkTaskParking.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkTaskBiting_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpBite.Enabled = chkTaskBiting.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkTaskSlave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpSlave.Enabled = chkTaskSlave.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkTaskHouse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpHouse.Enabled = chkTaskHouse.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkTaskGarden_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpGarden.Enabled = chkTaskGarden.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkTaskRanch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpRanch.Enabled = chkTaskRanch.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkTaskFish_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpFish.Enabled = chkTaskFish.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkTaskRich_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpRich.Enabled = chkTaskRich.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkTaskRich_CheckedChanged", ex);
            }
        }

        private void chkTaskCafe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                grpCafe.Enabled = chkTaskCafe.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkTaskCafe_CheckedChanged", ex);
            }
        }  
        #endregion
        
        #region Park
        private void chkOriginateMatch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelOriginateMatch.Enabled = chkOriginateMatch.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkOriginateMatch_CheckedChanged", ex);
            }
        }

        private void chkStartCar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelStartCar.Enabled = chkStartCar.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkStartCar_CheckedChanged", ex);
            }
        }
        #endregion

        #region Garden
        private void rdbExpensiveFarmSelf_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCustomFarmSelf.Enabled = !rdbExpensiveFarmSelf.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void rdbExpensiveFarmShared_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCustomFarmShared.Enabled = !rdbExpensiveFarmShared.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor", ex);
            }
        }

        private void chkPresentFruit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelPresentFruit.Enabled = chkPresentFruit.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkPresentFruit_CheckedChanged", ex);
            }
        }

        private void rdbPresentFruitByPrice_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkPresentFruitCheckValue.Enabled = rdbPresentFruitByPrice.Checked;
                txtPresentFruitValue.Enabled = rdbPresentFruitByPrice.Checked;
                lblPresentFruitCheckValue.Enabled = rdbPresentFruitByPrice.Checked;
                cmbPresentFruitId.Enabled = !rdbPresentFruitByPrice.Checked;
                chkPresentFruitCheckNum.Enabled = !rdbPresentFruitByPrice.Checked;
                txtPresentFruitNum.Enabled = !rdbPresentFruitByPrice.Checked;
                lblPresentFruitCheckNum.Enabled = !rdbPresentFruitByPrice.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbPresentFruitByPrice_CheckedChanged", ex);
            }
        }

        private void chkSellFruit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelSellFruit.Enabled = chkSellFruit.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkSellFruit_CheckedChanged", ex);
            }
        }

        private void rdbSellAllFruit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelSellForbiddenFruit.Enabled = !rdbSellAllFruit.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbSellAllFruit_CheckedChanged", ex);
            }
        }
        #endregion

        #region Ranch
        private void chkBuyCalf_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelBuyCalf.Enabled = chkBuyCalf.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkBuyCalf_CheckedChanged", ex);
            }
        }

        private void rdbBuyCalfByPrice_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbBuyCalfCustom.Enabled = !rdbBuyCalfByPrice.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbBuyCalfByPrice_CheckedChanged", ex);
            }
        }

        private void chkPresentProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelPresentProduct.Enabled = chkPresentProduct.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkPresentProduct_CheckedChanged", ex);
            }
        }

        private void rdbPresentProductByPrice_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkPresentProductCheckValue.Enabled = rdbPresentProductByPrice.Checked;
                txtPresentProductValue.Enabled = rdbPresentProductByPrice.Checked;
                lblPresentProductCheckValue.Enabled = rdbPresentProductByPrice.Checked;
                cmbAnimalProducts.Enabled = !rdbPresentProductByPrice.Checked;
                chkPresentProductCheckNum.Enabled = !rdbPresentProductByPrice.Checked;
                txtPresentProductNum.Enabled = !rdbPresentProductByPrice.Checked;
                lblPresentProductCheckNum.Enabled = !rdbPresentProductByPrice.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbPresentProductByPrice_CheckedChanged", ex);
            }
        }

        private void chkSellProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelSellProduct.Enabled = chkSellProduct.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkSellProduct_CheckedChanged", ex);
            }
        }

        private void rdbSellAllProducts_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelSellProductForbidden.Enabled = !rdbSellAllProducts.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbSellAllProducts_CheckedChanged", ex);
            }
        }

        private void btnSellProductForbiddenList_Click(object sender, EventArgs e)
        {
            try
            {
                DlgSellProductSelection dlgsellproducts = new DlgSellProductSelection();
                dlgsellproducts.Caption = "选择禁止出售的农副产品";
                dlgsellproducts.SelectedTitle = "禁止出售以下列表中的农副产品：";
                dlgsellproducts.SelectedProducts = _sellproductforbiddenlist;
                dlgsellproducts.AllProducts = _allproductslist;
                if (dlgsellproducts.ShowDialog() == DialogResult.OK)
                {
                    _sellproductforbiddenlist = dlgsellproducts.SelectedProducts;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.btnSellProductForbiddenList_Click", ex);
            }
        }
        #endregion

        #region Fish
        private void chkNetSelfFish_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelNetSelfFish.Enabled = chkNetSelfFish.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkNetSelfFish_CheckedChanged", ex);
            }
        }

        private void chkBuyFish_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelBuyFish.Enabled = chkBuyFish.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkBuyFish_CheckedChanged", ex);
            }
        }

        private void rdbBuyFishByRank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbBuyFishFishId.Enabled = !rdbBuyFishByRank.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbBuyFishByRank_CheckedChanged", ex);
            }
        }

        private void chkBuyUpdateTackle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelBuyUpdateTackle.Enabled = chkBuyUpdateTackle.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkBuyUpdateTackle_CheckedChanged", ex);
            }
        }

        private void chkPresentFish_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelPresentFish.Enabled = chkPresentFish.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkPresentFish_CheckedChanged", ex);
            }
        }

        private void btnPresentFishForbiddenList_Click(object sender, EventArgs e)
        {
            try
            {
                DlgPresentFishSelection dlgpresentfishes = new DlgPresentFishSelection();
                dlgpresentfishes.Caption = "选择禁止赠送的鱼";
                dlgpresentfishes.SelectedTitle = "禁止赠送以下列表中的鱼：";
                dlgpresentfishes.SelectedFishes = _presentforbiddenfishlist;
                dlgpresentfishes.AllFishes = _allfisheslist;
                if (dlgpresentfishes.ShowDialog() == DialogResult.OK)
                {
                    _presentforbiddenfishlist = dlgpresentfishes.SelectedFishes;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.btnPresentFishForbiddenList_Click", ex);
            }
        }

        private void chkSellFish_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelSellFish.Enabled = chkSellFish.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkSellFish_CheckedChanged", ex);
            }
        }

        private void rdbSellAllFish_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelSellFishForbidden.Enabled = !rdbSellAllFish.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbSellAllFish_CheckedChanged", ex);
            }
        }

        private void btnSellFishForbiddenList_Click(object sender, EventArgs e)
        {
            try
            {
                DlgPresentFishSelection dlgsellfishes = new DlgPresentFishSelection();
                dlgsellfishes.Caption = "选择禁止出售的鱼";
                dlgsellfishes.SelectedTitle = "禁止出售以下列表中的鱼：";
                dlgsellfishes.SelectedFishes = _sellforbiddenfishlist;
                dlgsellfishes.AllFishes = _allfisheslist;
                if (dlgsellfishes.ShowDialog() == DialogResult.OK)
                {
                    _sellforbiddenfishlist = dlgsellfishes.SelectedFishes;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.btnSellFishForbiddenList_Click", ex);
            }
        }

        #endregion

        #region Rich
        private void chkBuyAsset_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelBuyAsset.Enabled = chkBuyAsset.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkBuyAsset_CheckedChanged", ex);
            }
        }

        private void btnSetBuyAssetsList_Click(object sender, EventArgs e)
        {
            try
            {
                DlgBuyAssetsSelection dlgbuyassets = new DlgBuyAssetsSelection();
                dlgbuyassets.Caption = "选择购买的资产";
                dlgbuyassets.SelectedTitle = "只购买以下列表中的资产：";
                dlgbuyassets.SelectedAssets = _buyassetslist;
                dlgbuyassets.AllAssets = _allassetslist;
                if (dlgbuyassets.ShowDialog() == DialogResult.OK)
                {
                    _buyassetslist = dlgbuyassets.SelectedAssets;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.btnSetBuyAssetsList_Click", ex);
            }
        }
        #endregion

        #region Cafe

        private void chkCook_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelCook.Enabled = chkCook.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkCook_CheckedChanged", ex);
            }
        }

        private void chkHire_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelHire.Enabled = chkHire.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkHire_CheckedChanged", ex);
            }
        }

        private void chkPresentFood_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelPresentFood.Enabled = chkPresentFood.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkPresentFood_CheckedChanged", ex);
            }
        }

        private void rdbPresentFoodByCount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbPresentFoodDishId.Enabled = !rdbPresentFoodByCount.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbPresentFoodByCount_CheckedChanged", ex);
            }
        }


        private void rdbPresentFoodDishId_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnPresentForbiddenFoodList.Enabled = !rdbPresentFoodDishId.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.rdbPresentFoodDishId_CheckedChanged", ex);
            }
        }

        private void btnPresentForbiddenFoodList_Click(object sender, EventArgs e)
        {
            try
            {
                DlgPresentFoodSelection dlgpresentfood = new DlgPresentFoodSelection();
                dlgpresentfood.Caption = "选择禁止赠送的菜肴";
                dlgpresentfood.SelectedTitle = "禁止赠送以下列表中的菜肴：";
                dlgpresentfood.SelectedDishes = _presentforbiddenfoodlist;
                dlgpresentfood.AllDishes = _alldisheslist;
                if (dlgpresentfood.ShowDialog() == DialogResult.OK)
                {
                    _presentforbiddenfoodlist = dlgpresentfood.SelectedDishes;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.btnPresentForbiddenFoodList_Click", ex);
            }
        }
     
        private void chkSellFood_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelSellFood.Enabled = chkSellFood.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkSellFood_CheckedChanged", ex);
            }
        }

        private void chkPurchaseFood_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                panelPurchaseFood.Enabled = chkPurchaseFood.Checked;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmTaskEditor.chkPurchaseFood_CheckedChanged", ex);
            }
        }
        #endregion
        
        #region Properties
        public string TaskId
        {
            get { return _taskid; }
            set { _taskid = value; }
        }

        public string TaskName
        {
            get { return _taskname; }
            set { _taskname = value; }
        }

        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }
        #endregion        
              
    }
}