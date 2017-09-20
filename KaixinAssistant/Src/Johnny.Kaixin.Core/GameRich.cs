using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data;

using System.Net.Json;
using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class GameRich : KaixinBase
    {
        private Collection<FriendInfo> _allRichFriendsList;  //所有超级大亨的好友        
        private Collection<MyAssetInfo> _myAssetsList;  //我的资产
        private Collection<AssetInfo> _assetsMarketList; //资产在市场上的当前价格
        private Collection<AssetInfo> _assetsList;
        private Collection<AdvancedPurchaseInfo> _advancedPurchaseList;
        private FortuneRank _myFortuneRank;
        //20,100,200,500,1000

        private bool _canbuyasset;
        private long _cash;
        private long _assetcash;
        private bool _cantransaction; //每4小时最多只能进行6笔交易
        private bool _transactedasset;
        private bool _isfirsttimebuy;

        //public delegate void AllRichFriendsFetchedEventHandler(Collection<FriendInfo> allbigslotfriends);
        //public event AllRichFriendsFetchedEventHandler AllRichFriendsFetched;

        //public delegate void MyAssetsFetchedEventHandler(Collection<MyAssetInfo> myassets);
        //public event MyAssetsFetchedEventHandler MyAssetsFetched;

        public GameRich()
        {
            this._allRichFriendsList = new Collection<FriendInfo>();            
            this._myAssetsList = new Collection<MyAssetInfo>();
            this._assetsMarketList = new Collection<AssetInfo>();
            this._assetsList = new Collection<AssetInfo>();
            this._canbuyasset = true;
            this._cantransaction = true;
            this._transactedasset = false;
            this._isfirsttimebuy = true;
            this._myFortuneRank = FortuneRank.Worker;
        }

        #region Initialize
        public void Initialize()
        {
            try
            {
                //bigslot
                SetMessageLn("正在初始化[超级大亨]...");

                string content = RequestRichHomePage(true);

                //all fish friends
                //content = RequestAllFishFriends();
                //ReadAllFishFriends(content, false);
                //SetMessage("[所有钓鱼的好友]信息下载成功！");
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (ThreadInterruptedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Write("GameRich.Initialize", ex, LogSeverity.Error);
                SetMessage(" 初始化[超级大亨]失败！错误：" + ex.Message);
            }
        }
        #endregion

        #region RunRich
        public void RunRich()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_RICH;

                SetMessageLn("开始超级大亨...");

                //rich
                string contentHome = RequestRichHomePage(false);

                //我的财富等级
                GetMyFuntuneRank(contentHome, true);
                //我的现金
                GetMyCash(contentHome, true);

                //我的资产
                GetMyAssets(false);

                if (Task.SellAsset)
                    SellAsset();

                if (Task.BuyAsset)
                    BuyAsset();

                SetMessageLn("超级大亨完成！");

            });
            base.ExecuteTryCatchBlock(th, "发生异常，超级大亨失败！");
        }
        #endregion

        #region GetMyFuntuneRank
        private void GetMyFuntuneRank(string content, bool printMessage)
        {
            try
            {
                //<div class="l120_s" title=""><img src="http://pic.kaixin001.com/logo/58/82/120_2588258_1.jpg" /></div>
                //<a href="/home/?uid=2588258" class="sl noline f14"><b>庄荣-johnny</b></a><br>千万富翁

                //<div class="mt20"><img src="http://img.kaixin001.com.cn/i3/dh/icon_16.gif" align="absmiddle" /><b class="f12 c3">在打拼的好友</b></div>

                string strRank = "";
                string rankinfo = JsonHelper.GetMid(content, "<div class=\"l120_s\" title=\"\">", "<div class=\"mt20\">");
                if (rankinfo.IndexOf("百万富翁") > -1)
                {
                    _myFortuneRank = FortuneRank.Millionaire;
                    strRank = "百万富翁";
                }
                else if (rankinfo.IndexOf("千万富翁") > -1)
                {
                    _myFortuneRank = FortuneRank.Multimillionaire;
                    strRank = "千万富翁";
                }
                else if (rankinfo.IndexOf("亿万富翁") > -1)
                {
                    _myFortuneRank = FortuneRank.Billionaire;
                    strRank = "亿万富翁";
                }
                else if (rankinfo.IndexOf("超级富翁") > -1)
                {
                    _myFortuneRank = FortuneRank.SuperBillionaire;
                    strRank = "超级富翁";
                }
                else
                {
                    _myFortuneRank = FortuneRank.Worker;
                    strRank = "打工者";
                }

                if (printMessage)
                    SetMessageLn(string.Format("我的财富等级：{0}", strRank));
            }
            catch (Exception ex)
            {
                SetMessageLn("读取我的财富等级失败！");
                LogHelper.Write("GameRich.GetMyFuntuneRank", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region GetMyCash
        private void GetMyCash(string content, bool printMessage)
        {
            try
            {
                //<li><b>现金：7000元</b><a href="/!rich/market.php">投资</a><br></li>
                //<li><b>现金：32亿1600万2334元</b><a href="/!rich/market.php">投资</a><br></li>
                //<li><b>现金：2万元</b><a href="/!rich/market.php">投资</a><br></li>
                //<li><b>资产：1802万元</b><a href="/!rich/myitem.php">出售</a><br></li>
                string strcash = JsonHelper.GetMid(content, "<li><b>现金：", "元</b>");
                _cash = ParseCash(strcash);
                if (printMessage)
                    SetMessageLn(string.Format("我的现金：{0}元", _cash));
                string strassetcash = JsonHelper.GetMid(content, "<li><b>资产：", "元</b>");
                _assetcash = ParseCash(strassetcash);
                if (printMessage)
                    SetMessageLn(string.Format("我的资产：{0}元", _assetcash));

            }
            catch (Exception ex)
            {
                SetMessageLn("读取大亨现金失败！");
                LogHelper.Write("GameRich.GetMyCash", content, ex, LogSeverity.Error);
                _cash = 0;
            }
        }

        private long ParseCash(string content)
        {
            int index1 = content.IndexOf("亿");
            int index2 = content.IndexOf("万");

            if (index1 < 0 && index2 < 0)
                return DataConvert.GetInt64(content);

            string[] money = new string[3];
            if (index1 > -1)
                money[0] = content.Substring(0, index1);
            else
                money[0] = "0";
            if (index2 > -1)
            {
                if (index2 - index1 - 1 <= 0)
                    money[1] = "0";
                else
                    money[1] = content.Substring(index1 + 1, index2 - index1 - 1);
            }
            else
                money[1] = "0";

            if (content.Length - index2 - 2 <= 0)
                money[2] = "0";
            else
                money[2] = content.Substring(index2 + 1, content.Length - index2 - 2);

            return DataConvert.GetInt64(money[0]) * 100000000 + DataConvert.GetInt64(money[1]) * 10000 + DataConvert.GetInt64(money[2]);
        }
        #endregion

        #region GetMyAssets
        private void GetMyAssets(bool printMessage)
        {
            if (printMessage)
                SetMessageLn("读取我的资产信息...");

            this._myAssetsList.Clear();

            int page = 0;
            int maxrequest = 0;
            do
            {
                //防止死循环
                if (maxrequest > 20)
                    return;
                maxrequest++;

                int num;
                HH.DelayedTime = Constants.DELAY_1SECONDS;
                string content = HH.Get("http://www.kaixin001.com/!rich/myitem.php&start=" + page);
                
                content = JsonHelper.GetMid(content, "<div class=\"list_zc\">", "<div class=\"tac mt30\">");
                if (content != null)
                {                    
                    if (content == "\r\n\r\n\t\t\t<div class=\"c\"></div>\r\n\t\t</div>\r\n\t\t")
                        return;

                    if (printMessage)
                        SetMessageLn("第" + (page / 20 + 1).ToString() + "页");

                    for (string info = JsonHelper.GetMid(content, "<ul>", "</ul>", out num); info != null; info = JsonHelper.GetMid(content, "<ul>", "</ul>", out num))
                    {
                        content = content.Substring(num);
                        string detail = JsonHelper.GetMid(info, "<a href=\"javascript:sell(", ");\"");
                        string[] details = detail.Split(',');
                        string assetnum = JsonHelper.GetMid(info, "</b> ", " <img src="); //<li class="tac mt5" ><b>石油天然气公司</b> 100家 <img src="http://img.kaixin001.com.cn/i3/dh/icon_u.gif" /></li>
                        if (assetnum==null)
                            assetnum = JsonHelper.GetMid(info, "</b> ", " </li>"); //<li class="tac mt5" style="margin-top:14px"><b>诺亚方舟船票</b> 2张 </li>
                        if (details != null && details.Length == 3)
                        {
                            MyAssetInfo myasset = new MyAssetInfo();
                            myasset.IId = DataConvert.GetInt32(details[0]);
                            myasset.BuyPrice = DataConvert.GetDouble(details[1]);
                            myasset.CurrentPrice = DataConvert.GetDouble(JsonHelper.GetMid(details[2], "'", "'"));
                            myasset.AssetNum = DataConvert.GetInt32(assetnum.Substring(0, assetnum.Length - 1));
                            this._myAssetsList.Add(myasset);
                            if (printMessage)
                                SetMessageLn(myasset.ToString());
                        }
                    }
                    page += 20;
                }
                else
                {
                    return;
                }
            }
            while (true);
        }
        #endregion
                
        #region SellAsset
        private void SellAsset()
        {
            //出售资产
            SetMessageLn("出售资产...");

            if (this._myAssetsList.Count <= 0)
            {
                SetMessage("你没有任何资产可以出售！");
                return;
            }

            int num = 0;
            foreach (MyAssetInfo myasset in this._myAssetsList)
            {
                try
                {
                    //诺亚方舟船票
                    if (myasset.IId == 145)
                    {
                        SetMessageLn(string.Format("#{0}诺亚方舟船票：价值100亿，无法出售，跳过", ++num));
                        continue;
                    }

                    if (_cantransaction == false)
                        return;

                    AssetInfo asset = GetAssetById(myasset.IId);
                    if (asset != null)
                    {
                        SetMessageLn(string.Format("#{0}{1} 购买价格：{2}元 当前价格：{3}元 数量：{4}", ++num, asset.Name, myasset.BuyPrice, myasset.CurrentPrice, myasset.AssetNum));
                        if (asset.SellPrice <= myasset.CurrentPrice)
                        {
                            SetMessage(string.Format(" 当前的价格{0} >= 推荐出售价格{1}，尝试出售...", myasset.CurrentPrice, asset.SellPrice));
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            string content = HH.Post(string.Format("http://www.kaixin001.com/!rich/!sell.php?iid={0}&cost_price={1}&sell_price={2}&t=0.9385619927258357", myasset.IId, myasset.BuyPrice, myasset.CurrentPrice), string.Format("iid={0}&num={1}&sell_price={2}", myasset.IId, myasset.AssetNum, myasset.CurrentPrice));
                            if (GetSellAssetFeedback(content))
                                _transactedasset = true;
                        }
                        else
                        {
                            SetMessage(string.Format(" 当前的价格{0} < 推荐出售价格{1}，跳过", myasset.CurrentPrice, asset.SellPrice));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Write("GameRich.SellAsset", ex, LogSeverity.Error);
                    continue;
                }
            }
        }
        #endregion

        #region GetSellAssetFeedback
        private bool GetSellAssetFeedback(string content)
        {
            try
            {
                //LogHelper.Write("GameSlave.GetSellAssetFeedback", content, LogSeverity.Warn);
                //<script type="text/javascript">
                //    $("display_99").style.display = "block";
                //    try{
                //        $('num').selectedIndex = $('num').length - 1;
                //    }catch(e){}
                //</script>
                bool ret = false;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "text/javascript\">\r\n\t$(\"", "\").style.display");
                if (strdivid != null)
                {
                    if (strdivid.Equals("display_99"))
                        ret = true;
                    //<div id="display_99" class="s_windows" style="display:none">
                    //  <form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //    <img src="http://img.kaixin001.com.cn/i3/dh/icon_jb.gif" class="l"/>
                    //    <div class="l r_cont" style="width:300px;"><b>售出成功！</b><br>
                    //      你成功售出了0个 赌场！<br>
                    //      此笔投资共<span class="c_c30">0元</span><br>
                    //      <span style="display:none">你获得虚拟红包奖励<span class="c_c30">金额0元</span><br></span>
                    //      你的现金余额：<span class="c_c30">464亿7276万元</span>
                    //      <div style="margin:20px 0 30px 0px">
                    //        <div class="bbs1 mt5" style="margin-left:0px;width:83px;">
                    //          <input type="button" class="bb1-12" value="确定" style="width:83px" onclick="javascript:new parent.dialog().reset();parent.location.reload();"/>
                    //        </div>
                    //      </div>
                    //    </div>
                    //    <div class="c"></div>
                    //  </form>
                    //</div>
                    //<div id="display_2" class="s_windows" style="display:none">
                    //  <form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //    <img src="http://img.kaixin001.com.cn/i3/dh/icon_js.gif" class="l"/>
                    //    <div class="l r_cont" style="width:250px;"><b>售出失败！</b><br>
                    //      每4小时最多只能进行6笔交易!<br />
                    //      距离下次可交易时间还剩1小时40分。
                    //    </div>
                    //    <div class="c"></div>
                    //    <div style="display:block; * margin-top:10px;" class="bonus_sell"><img src="http://img.kaixin001.com.cn/i3/dh/slamp.png" /> 提示：你还可以进行一次红包标识商品交易。</div>
                    //    <div class="bbs1 mt5" style="margin:20px 0 0 140px;* margin-top:10px; display:inline;width:83px;border:none">
                    //      <input type="button" class="bb1-12" value="返回" onclick="new parent.dialog().reset();" style="width:83px"/>
                    //    </div>
                    //    <div class="c"></div>
                    //  </form>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, strdivid, "<div class=\"c\"></div>");
                    int index = strmsg.IndexOf(">");
                    string strmsg2 = strmsg.Substring(index + 1);
                    //strmsg = JsonHelper.GetFirstLast(strmsg2, "<div class=\"l r_cont\" style=\"width:300px;\">", "<div class=\"c\"></div>");
                    //if (String.IsNullOrEmpty(strmsg))
                    //    strmsg = JsonHelper.GetMid(strmsg2, "<div class=\"l r_cont\" style=\"width:250px;\">", "<div class=\"c\"></div>");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg2);
                    if (strmsg.IndexOf("每4小时最多只能进行6笔交易") > -1)
                        _cantransaction = false;
                }
                if (String.IsNullOrEmpty(strmsg))
                    LogHelper.Write("GameRich.GetSellAssetFeedback", content, LogSeverity.Info);
                SetMessage(" " + strmsg);
                return ret;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (ThreadInterruptedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Write("GameSlave.GetSellAssetFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region GetAssetsMarket
        private void GetAssetsMarket(bool printMessage)
        {
            if (printMessage)
                SetMessageLn("读取市场上的资产信息...");

            this._assetsMarketList.Clear();

            if (Task.BuyAssetsList.Count <= 10)
            {
                string content = "";
                for (int ix = 0; ix < Task.BuyAssetsList.Count; ix++)
                {
                    AssetInfo asset = CreateAssetById(Task.BuyAssetsList[ix]);
                    if (asset != null)
                    {
                        content = HH.Get(string.Format("http://www.kaixin001.com/!rich/!api_item_price.php?rt=xml&iid={0}", asset.IId));
                        asset.CurrentPrice = DataConvert.GetInt64(JsonHelper.GetMidLast(content, "<price>", "</price>"));
                        this._assetsMarketList.Add(asset);
                    }
                }
            }
            else
            {
                int page = 0;
                int maxrequest = 0;
                int[] cateids = null;
                if (_myFortuneRank != FortuneRank.Worker)
                    cateids = new int[] { 2, 4, 6, 7, 8, 9, 10, 11 };
                else
                    cateids = new int[] { 1, 11 };

                for (int ix = 0; ix < cateids.Length; ix++)
                {
                    page = 0;
                    maxrequest = 0;
                    do
                    {
                        //防止死循环
                        if (maxrequest > 20)
                            break;
                        maxrequest++;

                        HH.DelayedTime = Constants.DELAY_1SECONDS;
                        string content = HH.Get(string.Format("http://www.kaixin001.com/!rich/market.php?cateid={0}&start={1}", cateids[ix], page));

                        content = JsonHelper.GetMid(content, "<div class=\"list_zc\">", "<div class=\"tac mt30\">");
                        if (!ReadAssetsMarket(content, printMessage, ref page))
                            break;
                    }
                    while (true);
                }
            }
        }
        #endregion

        #region ReadAssetsMarket
        private bool ReadAssetsMarket(string content, bool printMessage, ref int page)
        {
            if (content != null)
            {
                if (content == "\r\n\r\n\t\t\t<div class=\"c\"></div>\r\n\t\t</div>\r\n\t\t")
                    return false;
                if (content == "\n<div class=\"c\"></div>\n</div>\n")
                    return false;

                if (printMessage)
                    SetMessageLn("第" + (page / 20 + 1).ToString() + "页");

                int num;
                for (string info = JsonHelper.GetMid(content, "<ul >", "</ul>", out num); info != null; info = JsonHelper.GetMid(content, "<ul >", "</ul>", out num))
                {
                    content = content.Substring(num);
                    string detail = JsonHelper.GetMid(info, "<a href=\"javascript:purchase(", ");\"");
                    string[] details = detail.Split(',');
                    if (details != null && details.Length == 2)
                    {
                        AssetInfo asset = new AssetInfo();
                        asset.IId = DataConvert.GetInt32(details[0]);
                        asset.CurrentPrice = DataConvert.GetInt64(JsonHelper.GetMid(details[1], "'", "'"));
                        asset.Name = JsonHelper.GetMid(info, "<li class=\"tac mt5\"><b>", "</b></li>");
                        if (asset.IId != 0)
                            this._assetsMarketList.Add(asset);
                        if (printMessage)
                            SetMessageLn(asset.ToString());
                    }
                }
                page += 20;
            }
            else
            {
                return false;
            }

            return true;
        }
        #endregion

        #region BuyAsset
        private void BuyAsset()
        {
            //购买资产
            SetMessageLn("购买资产...");

            if (_cantransaction == false)
            {
                SetMessage("由于每4小时最多只能进行6笔交易，暂不执行。");
                return;
            }

            if (!CanBuyIfMyAsset())
                return;

            if (Task.BuyAssetsList.Count <= 0)
            {
                SetMessage("没有设定需要购买的资产列表！");
                return;
            }

            //我的现金
            string content = RequestRichHomePage(false);
            GetMyCash(content, false);

            if (!CanBuyIfRatio())
                return;

            //当前市场上资产价格
            GetAssetsMarket(false);

            Collection<AssetInfo> buylist = BuildBuyAssetsList();
            if (buylist.Count <= 0)
                return;

            _canbuyasset = true;
            if (Task.BuyAssetCheap)
            {
                buylist = SortBuyAssetsListByStandardPrice(buylist, false);
                int num = 0;
                foreach (AssetInfo asset in buylist)
                {
                    try
                    {
                        if (_cantransaction == false)
                            return;
                        if (!CanBuyIfRatio())
                            return;
                        if (!CanBuyIfMyAsset())
                            return;
                        BuyTheAsset(asset.IId, ++num);
                        if (_canbuyasset == false)
                            break;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GameRich.BuyAsset", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
            else
            {
                buylist = SortBuyAssetsListByStandardPrice(buylist, true);
                int num = 0;
                foreach (AssetInfo asset in buylist)
                {
                    try
                    {
                        if (_cantransaction == false)
                            return;
                        if (!CanBuyIfRatio())
                            return;
                        if (!CanBuyIfMyAsset())
                            return;
                        BuyTheAsset(asset.IId, ++num);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("GameRich.BuyAsset", ex, LogSeverity.Error);
                        continue;
                    }
                }
            }
        }
        #endregion

        #region CanBuyIfRatio
        private bool CanBuyIfRatio()
        {
            if (Task.GiveUpIfRatio)
            {
                decimal ratio = _cash * 100 / (_cash + _assetcash);
                ratio = Math.Round(ratio, 0);
                if (ratio <= Task.GiveUpRatio)
                {
                    SetMessageLn(string.Format("现金({0})/总资产({1})比：{2} <= {3}，放弃购买", _cash, (_cash + _assetcash), ratio, Task.GiveUpRatio));
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region CanBuyIfMyAsset
        private bool CanBuyIfMyAsset()
        {
            if (Task.GiveUpIfMyAsset)
            {
                if (_transactedasset == true)
                {
                    GetMyAssets(false);
                    _transactedasset = false;
                }

                if (_myAssetsList.Count >= Task.GiveUpAssetCount)
                {
                    SetMessageLn(string.Format("拥有的资产项{0} >= {1}，放弃购买", _myAssetsList.Count, Task.GiveUpAssetCount));
                    return false;
                }                
            }

            return true;
        }
        #endregion

        #region BuyTheAsset
        private void BuyTheAsset(int assetid, int num)
        {
            AssetInfo assetrecommend = GetAssetById(assetid);
            AssetInfo asset = GetAssetMarketById(assetid);

            if (assetrecommend != null && asset != null)
            {
                SetMessageLn(string.Format("#{0}{1} 当前价格：{2}元", num, assetrecommend.Name, asset.CurrentPrice));
                if (assetrecommend.BuyPrice >= asset.CurrentPrice)
                {
                    SetMessage(string.Format(" <= 购买推荐价格{0}，尝试购买...", assetrecommend.BuyPrice));

                    if (_cash < asset.CurrentPrice)
                    {
                        SetMessage(string.Format("现金{0}不够", _cash));
                        return;
                    }

                    _isfirsttimebuy = true;

                    //现金能买的数量
                    long tempcount = _cash / asset.CurrentPrice;
                    long minCount = 0;
                    do
                    {
                        if (_cantransaction == false)
                            return;

                        if (Task.AdvancedPurchase)
                        {
                            minCount = GetMinimumPurchaseCount(_advancedPurchaseList, _cash + _assetcash, asset.CurrentPrice);
                            if (tempcount < minCount)
                            {
                                SetMessage(string.Format("根据总资产({0})和当前资产单价({1})确定的最小购买数{2} > 尝试购买数{3}，停止购买", _cash + _assetcash, asset.CurrentPrice, minCount, tempcount));
                                return;
                            }
                        }

                        if (_isfirsttimebuy == false && Task.GiveUpIfMinimum && Task.GiveUpMinimum >= tempcount)
                        {
                            SetMessageLn(string.Format("尝试购买数{0} <= 连续最小购买数{1}，停止购买", tempcount, Task.GiveUpMinimum));
                            return;
                        }

                        if (_myFortuneRank == FortuneRank.Worker)
                        {
                            if (tempcount >= 1000)
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, 1000);
                                tempcount -= 1000;
                            }
                            else if (tempcount >= 500)
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, 500);
                                tempcount -= 500;
                            }
                            else if (tempcount >= 200)
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, 200);
                                tempcount -= 200;
                            }
                            else if (tempcount >= 100)
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, 100);
                                tempcount -= 100;
                            }
                            else if (tempcount >= 20)
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, 20);
                                tempcount -= 20;
                            }
                            else
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, tempcount);
                                tempcount = 0;
                                return;
                            }
                        }
                        else
                        {
                            if (tempcount >= 20)
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, 20);
                                tempcount -= 20;
                            }
                            else
                            {
                                BuyAssetAccordingCount(asset.IId, assetrecommend.Name, asset.CurrentPrice, tempcount);
                                tempcount = 0;
                                return;
                            }
                        }
                    }
                    while (tempcount > 0);
                }
                else
                {
                    SetMessage(string.Format(" > 购买推荐价格{0}，跳过", assetrecommend.BuyPrice));
                }
            }
        }
        #endregion

        #region BuyAssetAccordingCount
        private void BuyAssetAccordingCount(int assetid, string name, long purchaseprice, long count)
        {
            SetMessageLn(string.Format("购买{0}个{1}", count, name));
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            HH.Get(string.Format("http://www.kaixin001.com/!rich/!purchase.php?iid={0}&purchase_price={1}&t=0.9090718055919508", assetid, purchaseprice));

            HH.DelayedTime = Constants.DELAY_2SECONDS;
            string content = HH.Post(string.Format("http://www.kaixin001.com/!rich/!purchase.php?iid={0}&purchase_price={1}&t=0.13999783978033764", assetid, purchaseprice), string.Format("iid={0}&purchase_price={1}&num={2}", assetid, purchaseprice, count));
            if (GetBuyAssetFeedback(content))
            {
                _isfirsttimebuy = false;
                _transactedasset = true;
                _cash -= count * purchaseprice;
            }
        }
        #endregion

        #region GetBuyAssetFeedback
        private bool GetBuyAssetFeedback(string content)
        {
            try
            {
                //LogHelper.Write("GameSlave.GetBuyAssetFeedback", content, LogSeverity.Warn);
                //<script type="text/javascript">
                //    $("display_99").style.display = "block";
                //    try{
                //        $('num').selectedIndex = $('num').length - 1;
                //    }catch(e){}
                //</script>
                bool ret = false;
                string strmsg = "";
                string strdivid = JsonHelper.GetMid(content, "text/javascript\">\n$(\"", "\").style.display");
                if (strdivid != null)
                {
                    if (strdivid.Equals("display_99"))
                        ret = true;
                    //<div id="display_99" style="display:none;border:none;" class="s_windows">
                    //<form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //<img src="http://img.kaixin001.com.cn/i3/dh/icon_jb.gif" class="l"/>
                    //<div class="l r_cont" style="width:300px;"><b>你成功购进了 0个 天翼3G大礼包！</b><br>
                    //你的现金余额：<span class="c_c30">5000元</span>
                    //<div style="display:none">
                    //0内还可以交易<span class="c_c30">0</span>次!<br /></div>
                    //<div style="display:block">
                    //4小时内你已进行6笔交易!<br />
                    //距离下次可交易时间还剩0。</div>
                    //</div>
                    //<div class="c"></div>
                    //<div style="display:none" class="bonus_sell"><img src="http://img.kaixin001.com.cn/i3/dh/slamp.png" /> 提示：你还可以进行一次红包标识商品交易。</div>
                    //<div class="bbs1 mt5" style="margin:10px 0 0 140px;display:inline;width:83px;border:none">
                    //<input type="button" class="bb1-12" value="继续购进" style="width:83px;" onclick="javascript:new parent.dialog().reset();parent.location.reload();"/>
                    //</div>
                    //<div style="width:15px;" class="l">&nbsp;</div>
                    //<a href="javascript:new parent.dialog().reset();parent.location='/!rich/myitem.php'" class="c6 l f12 mt10" style="*margin-left:15px;">查看我的资产</a>
                    //<div class="c"></div>
                    //</form>
                    //</div>
                    //<div id="display_2" style="display:none;border:none;" class="s_windows">
                    //<form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //<img src="http://img.kaixin001.com.cn/i3/dh/icon_js.gif" class="l"/>
                    //<div class="l r_cont" style="width:240px;"><b>购买失败！</b><br>
                    //每4小时最多只能进行6笔交易!<br />
                    //距离下次可交易时间还剩3小时17分。
                    //</div>
                    //<div class="c"></div>
                    //<div style="display:none" class="bonus_sell"><img src="http://img.kaixin001.com.cn/i3/dh/slamp.png" /> 提示：你还可以进行一次红包标识商品交易。</div>
                    //<div class="bbs1 mt5" style="margin:20px 0 0 140px;display:inline;width:83px;border:none">
                    //<input type="button" class="bb1-12" value="返回" onclick="new parent.dialog().reset();" style="width:83px"/>
                    //</div>
                    //<div class="c"></div>
                    //</form>
                    //</div>
                    //<div id="display_5" style="display:none;border:none;" class="s_windows">
                    //<form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //<img src="http://img.kaixin001.com.cn/i3/dh/icon_js.gif" class="l"/>
                    //<div class="l r_cont" style="width:240px;"><b>你的现金不足！</b><br>
                    //你的现金余额：<span class="c_c30">564亿4776万元</span>
                    //<div style="margin:50px 0 30px 0px;">
                    //<div class="bbs1 mt5" style="margin-left:20px;display:inline;width:83px;">
                    //<input type="button" class="bb1-12" value="返回" onclick="new parent.dialog().reset();" style="width:83px"/>
                    //</div>
                    //<div class="c"></div>
                    //</div>
                    //</div>
                    //<div class="c"></div>
                    //</form>
                    //</div>
                    strmsg = JsonHelper.GetMid(content, strdivid, "<div class=\"c\"></div>");
                    int index = strmsg.IndexOf(">");
                    string strmsg2 = strmsg.Substring(index + 1);
                    strmsg = JsonHelper.GetMid(strmsg2, "<div class=\"l r_cont\" style=\"width:300px;\">", "br /></div>");
                    if (String.IsNullOrEmpty(strmsg))
                        strmsg = JsonHelper.GetMid(strmsg2, "<div class=\"l r_cont\" style=\"width:240px;\">", "</div>");
                    strmsg = JsonHelper.FiltrateHtmlTags(strmsg);
                    if (strmsg.IndexOf("每4小时最多只能进行6笔交易") > -1 || strmsg.Contains("物品价格已经变化"))
                        _cantransaction = false;
                }

                if (String.IsNullOrEmpty(strmsg))
                    LogHelper.Write("GameRich.GetBuyAssetFeedback", content, LogSeverity.Info);
                SetMessage(" " + strmsg);
                return ret;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (ThreadInterruptedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Write("GameRich.GetBuyAssetFeedback", content, ex, LogSeverity.Error);
                return true;
            }
        }
        #endregion

        #region GetAssetById
        private AssetInfo GetAssetById(int iid)
        {
            foreach (AssetInfo asset in _assetsList)
            {
                if (asset.IId == iid)
                {
                    return asset;
                }
            }
            return null;
        }
        #endregion

        #region GetAssetMarketById
        private AssetInfo GetAssetMarketById(int iid)
        {
            foreach (AssetInfo asset in _assetsMarketList)
            {
                if (asset.IId == iid)
                {
                    return asset;
                }
            }
            return null;
        }
        #endregion

        #region BuildBuyAssetsList
        private Collection<AssetInfo> BuildBuyAssetsList()
        {
            Collection<AssetInfo> assets = new Collection<AssetInfo>();

            if (Task.BuyAssetsList.Count <= 0)
                return assets;           

            for (int ix = 0; ix < Task.BuyAssetsList.Count; ix++)
            {
                AssetInfo asset = CreateAssetById(Task.BuyAssetsList[ix]);
                if (asset != null)
                    assets.Add(asset);
            }
            return assets;
        }
        #endregion

        #region CreateAssetById
        private AssetInfo CreateAssetById(int iid)
        {
            foreach (AssetInfo asset in _assetsList)
            {
                if (asset.IId == iid)
                {
                    return asset.Clone();
                }
            }
            return null;
        }
        #endregion

        #region SortBuyAssetsListByStandardPrice
        private Collection<AssetInfo> SortBuyAssetsListByStandardPrice(Collection<AssetInfo> assets, bool des)
        {
            for (int ix = 0; ix < assets.Count; ix++)
            {
                for (int iy = ix + 1; iy < assets.Count; iy++)
                {
                    if (des)
                    {
                        if (assets[ix].StandardPrice < assets[iy].StandardPrice)
                        {
                            AssetInfo temp = assets[ix];
                            assets[ix] = assets[iy];
                            assets[iy] = temp;
                        }
                    }
                    else
                    {
                        if (assets[ix].StandardPrice > assets[iy].StandardPrice)
                        {
                            AssetInfo temp = assets[ix];
                            assets[ix] = assets[iy];
                            assets[iy] = temp;
                        }
                    }
                }
            }

            return assets;
        }
        #endregion

        #region GetMinimumPurchaseCount
        /// <summary>
        /// 根据现金和物品价格，确定最小购买数。
        /// </summary>
        /// <param name="cash"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public long GetMinimumPurchaseCount(Collection<AdvancedPurchaseInfo> advancedPurchaseList, long cash, long price)
        {
            long standardcash = 0;

            //获取价格
            for (int ix = 0; ix < advancedPurchaseList.Count; ix++)
            {
                if (cash <= advancedPurchaseList[ix].Cash)
                {
                    if (ix == 0)
                    {
                        standardcash = advancedPurchaseList[ix].Cash;
                    }
                    else
                    {
                        standardcash = advancedPurchaseList[ix - 1].Cash;
                    }
                    break;
                }
            }

            //确定最小购买数
            for (int iy = 0; iy < advancedPurchaseList.Count; iy++)
            {
                if (standardcash == advancedPurchaseList[iy].Cash && price <= advancedPurchaseList[iy].Price)
                    return advancedPurchaseList[iy].Count;
            }
            return 1;
        }
        #endregion

        #region Request

        public string RequestRichHomePage(bool IsInitial)
        {
            string content = HH.Get("http://www.kaixin001.com/!rich/index.php");
            if (content.IndexOf("<title>添加组件 - 开心网</title>") != -1)
            {
                SetMessageLn("还未安装超级大亨组件,尝试安装中...");
                HH.Post("http://www.kaixin001.com/app/install.php", "aid=1092&isinstall=1");
                content = HH.Get("http://www.kaixin001.com/!rich/index.php");
            }
            this._verifyCode = JsonHelper.GetMid(content, "g_verify = \"", "\"");
            return content;
        }

        public string RequestMyAssets()
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get("http://www.kaixin001.com/!rich/myitem.php");
        }

        public string RequestTheAsset(int iid, double currentprice)
        {
            HH.DelayedTime = Constants.DELAY_1SECONDS;
            return HH.Get(string.Format("http://www.kaixin001.com/!rich/!purchase.php?iid={0}&purchase_price={1}&t=0.9960790519692878", iid, currentprice));
        }  
        
        #endregion

        #region Properties
        public Collection<FriendInfo> AllRichFriendsList
        {
            get { return this._allRichFriendsList; }
        }

        public Collection<AssetInfo> AssetsList
        {
            get { return _assetsList; }
            set { _assetsList = value; }
        }

        public Collection<AdvancedPurchaseInfo> AdvancedPurchaseList
        {
            get { return _advancedPurchaseList; }
            set { _advancedPurchaseList = value; }
        }
        #endregion
    }
}
