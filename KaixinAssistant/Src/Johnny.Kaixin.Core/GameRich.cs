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
        private Collection<FriendInfo> _allRichFriendsList;  //���г������ĺ���        
        private Collection<MyAssetInfo> _myAssetsList;  //�ҵ��ʲ�
        private Collection<AssetInfo> _assetsMarketList; //�ʲ����г��ϵĵ�ǰ�۸�
        private Collection<AssetInfo> _assetsList;
        private Collection<AdvancedPurchaseInfo> _advancedPurchaseList;
        private FortuneRank _myFortuneRank;
        //20,100,200,500,1000

        private bool _canbuyasset;
        private long _cash;
        private long _assetcash;
        private bool _cantransaction; //ÿ4Сʱ���ֻ�ܽ���6�ʽ���
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
                SetMessageLn("���ڳ�ʼ��[�������]...");

                string content = RequestRichHomePage(true);

                //all fish friends
                //content = RequestAllFishFriends();
                //ReadAllFishFriends(content, false);
                //SetMessage("[���е���ĺ���]��Ϣ���سɹ���");
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
                SetMessage(" ��ʼ��[�������]ʧ�ܣ�����" + ex.Message);
            }
        }
        #endregion

        #region RunRich
        public void RunRich()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_RICH;

                SetMessageLn("��ʼ�������...");

                //rich
                string contentHome = RequestRichHomePage(false);

                //�ҵĲƸ��ȼ�
                GetMyFuntuneRank(contentHome, true);
                //�ҵ��ֽ�
                GetMyCash(contentHome, true);

                //�ҵ��ʲ�
                GetMyAssets(false);

                if (Task.SellAsset)
                    SellAsset();

                if (Task.BuyAsset)
                    BuyAsset();

                SetMessageLn("���������ɣ�");

            });
            base.ExecuteTryCatchBlock(th, "�����쳣���������ʧ�ܣ�");
        }
        #endregion

        #region GetMyFuntuneRank
        private void GetMyFuntuneRank(string content, bool printMessage)
        {
            try
            {
                //<div class="l120_s" title=""><img src="http://pic.kaixin001.com/logo/58/82/120_2588258_1.jpg" /></div>
                //<a href="/home/?uid=2588258" class="sl noline f14"><b>ׯ��-johnny</b></a><br>ǧ����

                //<div class="mt20"><img src="http://img.kaixin001.com.cn/i3/dh/icon_16.gif" align="absmiddle" /><b class="f12 c3">�ڴ�ƴ�ĺ���</b></div>

                string strRank = "";
                string rankinfo = JsonHelper.GetMid(content, "<div class=\"l120_s\" title=\"\">", "<div class=\"mt20\">");
                if (rankinfo.IndexOf("������") > -1)
                {
                    _myFortuneRank = FortuneRank.Millionaire;
                    strRank = "������";
                }
                else if (rankinfo.IndexOf("ǧ����") > -1)
                {
                    _myFortuneRank = FortuneRank.Multimillionaire;
                    strRank = "ǧ����";
                }
                else if (rankinfo.IndexOf("������") > -1)
                {
                    _myFortuneRank = FortuneRank.Billionaire;
                    strRank = "������";
                }
                else if (rankinfo.IndexOf("��������") > -1)
                {
                    _myFortuneRank = FortuneRank.SuperBillionaire;
                    strRank = "��������";
                }
                else
                {
                    _myFortuneRank = FortuneRank.Worker;
                    strRank = "����";
                }

                if (printMessage)
                    SetMessageLn(string.Format("�ҵĲƸ��ȼ���{0}", strRank));
            }
            catch (Exception ex)
            {
                SetMessageLn("��ȡ�ҵĲƸ��ȼ�ʧ�ܣ�");
                LogHelper.Write("GameRich.GetMyFuntuneRank", content, ex, LogSeverity.Error);
            }
        }
        #endregion

        #region GetMyCash
        private void GetMyCash(string content, bool printMessage)
        {
            try
            {
                //<li><b>�ֽ�7000Ԫ</b><a href="/!rich/market.php">Ͷ��</a><br></li>
                //<li><b>�ֽ�32��1600��2334Ԫ</b><a href="/!rich/market.php">Ͷ��</a><br></li>
                //<li><b>�ֽ�2��Ԫ</b><a href="/!rich/market.php">Ͷ��</a><br></li>
                //<li><b>�ʲ���1802��Ԫ</b><a href="/!rich/myitem.php">����</a><br></li>
                string strcash = JsonHelper.GetMid(content, "<li><b>�ֽ�", "Ԫ</b>");
                _cash = ParseCash(strcash);
                if (printMessage)
                    SetMessageLn(string.Format("�ҵ��ֽ�{0}Ԫ", _cash));
                string strassetcash = JsonHelper.GetMid(content, "<li><b>�ʲ���", "Ԫ</b>");
                _assetcash = ParseCash(strassetcash);
                if (printMessage)
                    SetMessageLn(string.Format("�ҵ��ʲ���{0}Ԫ", _assetcash));

            }
            catch (Exception ex)
            {
                SetMessageLn("��ȡ����ֽ�ʧ�ܣ�");
                LogHelper.Write("GameRich.GetMyCash", content, ex, LogSeverity.Error);
                _cash = 0;
            }
        }

        private long ParseCash(string content)
        {
            int index1 = content.IndexOf("��");
            int index2 = content.IndexOf("��");

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
                SetMessageLn("��ȡ�ҵ��ʲ���Ϣ...");

            this._myAssetsList.Clear();

            int page = 0;
            int maxrequest = 0;
            do
            {
                //��ֹ��ѭ��
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
                        SetMessageLn("��" + (page / 20 + 1).ToString() + "ҳ");

                    for (string info = JsonHelper.GetMid(content, "<ul>", "</ul>", out num); info != null; info = JsonHelper.GetMid(content, "<ul>", "</ul>", out num))
                    {
                        content = content.Substring(num);
                        string detail = JsonHelper.GetMid(info, "<a href=\"javascript:sell(", ");\"");
                        string[] details = detail.Split(',');
                        string assetnum = JsonHelper.GetMid(info, "</b> ", " <img src="); //<li class="tac mt5" ><b>ʯ����Ȼ����˾</b> 100�� <img src="http://img.kaixin001.com.cn/i3/dh/icon_u.gif" /></li>
                        if (assetnum==null)
                            assetnum = JsonHelper.GetMid(info, "</b> ", " </li>"); //<li class="tac mt5" style="margin-top:14px"><b>ŵ�Ƿ��۴�Ʊ</b> 2�� </li>
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
            //�����ʲ�
            SetMessageLn("�����ʲ�...");

            if (this._myAssetsList.Count <= 0)
            {
                SetMessage("��û���κ��ʲ����Գ��ۣ�");
                return;
            }

            int num = 0;
            foreach (MyAssetInfo myasset in this._myAssetsList)
            {
                try
                {
                    //ŵ�Ƿ��۴�Ʊ
                    if (myasset.IId == 145)
                    {
                        SetMessageLn(string.Format("#{0}ŵ�Ƿ��۴�Ʊ����ֵ100�ڣ��޷����ۣ�����", ++num));
                        continue;
                    }

                    if (_cantransaction == false)
                        return;

                    AssetInfo asset = GetAssetById(myasset.IId);
                    if (asset != null)
                    {
                        SetMessageLn(string.Format("#{0}{1} ����۸�{2}Ԫ ��ǰ�۸�{3}Ԫ ������{4}", ++num, asset.Name, myasset.BuyPrice, myasset.CurrentPrice, myasset.AssetNum));
                        if (asset.SellPrice <= myasset.CurrentPrice)
                        {
                            SetMessage(string.Format(" ��ǰ�ļ۸�{0} >= �Ƽ����ۼ۸�{1}�����Գ���...", myasset.CurrentPrice, asset.SellPrice));
                            HH.DelayedTime = Constants.DELAY_2SECONDS;
                            string content = HH.Post(string.Format("http://www.kaixin001.com/!rich/!sell.php?iid={0}&cost_price={1}&sell_price={2}&t=0.9385619927258357", myasset.IId, myasset.BuyPrice, myasset.CurrentPrice), string.Format("iid={0}&num={1}&sell_price={2}", myasset.IId, myasset.AssetNum, myasset.CurrentPrice));
                            if (GetSellAssetFeedback(content))
                                _transactedasset = true;
                        }
                        else
                        {
                            SetMessage(string.Format(" ��ǰ�ļ۸�{0} < �Ƽ����ۼ۸�{1}������", myasset.CurrentPrice, asset.SellPrice));
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
                    //    <div class="l r_cont" style="width:300px;"><b>�۳��ɹ���</b><br>
                    //      ��ɹ��۳���0�� �ĳ���<br>
                    //      �˱�Ͷ�ʹ�<span class="c_c30">0Ԫ</span><br>
                    //      <span style="display:none">��������������<span class="c_c30">���0Ԫ</span><br></span>
                    //      ����ֽ���<span class="c_c30">464��7276��Ԫ</span>
                    //      <div style="margin:20px 0 30px 0px">
                    //        <div class="bbs1 mt5" style="margin-left:0px;width:83px;">
                    //          <input type="button" class="bb1-12" value="ȷ��" style="width:83px" onclick="javascript:new parent.dialog().reset();parent.location.reload();"/>
                    //        </div>
                    //      </div>
                    //    </div>
                    //    <div class="c"></div>
                    //  </form>
                    //</div>
                    //<div id="display_2" class="s_windows" style="display:none">
                    //  <form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //    <img src="http://img.kaixin001.com.cn/i3/dh/icon_js.gif" class="l"/>
                    //    <div class="l r_cont" style="width:250px;"><b>�۳�ʧ�ܣ�</b><br>
                    //      ÿ4Сʱ���ֻ�ܽ���6�ʽ���!<br />
                    //      �����´οɽ���ʱ�仹ʣ1Сʱ40�֡�
                    //    </div>
                    //    <div class="c"></div>
                    //    <div style="display:block; * margin-top:10px;" class="bonus_sell"><img src="http://img.kaixin001.com.cn/i3/dh/slamp.png" /> ��ʾ���㻹���Խ���һ�κ����ʶ��Ʒ���ס�</div>
                    //    <div class="bbs1 mt5" style="margin:20px 0 0 140px;* margin-top:10px; display:inline;width:83px;border:none">
                    //      <input type="button" class="bb1-12" value="����" onclick="new parent.dialog().reset();" style="width:83px"/>
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
                    if (strmsg.IndexOf("ÿ4Сʱ���ֻ�ܽ���6�ʽ���") > -1)
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
                SetMessageLn("��ȡ�г��ϵ��ʲ���Ϣ...");

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
                        //��ֹ��ѭ��
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
                    SetMessageLn("��" + (page / 20 + 1).ToString() + "ҳ");

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
            //�����ʲ�
            SetMessageLn("�����ʲ�...");

            if (_cantransaction == false)
            {
                SetMessage("����ÿ4Сʱ���ֻ�ܽ���6�ʽ��ף��ݲ�ִ�С�");
                return;
            }

            if (!CanBuyIfMyAsset())
                return;

            if (Task.BuyAssetsList.Count <= 0)
            {
                SetMessage("û���趨��Ҫ������ʲ��б�");
                return;
            }

            //�ҵ��ֽ�
            string content = RequestRichHomePage(false);
            GetMyCash(content, false);

            if (!CanBuyIfRatio())
                return;

            //��ǰ�г����ʲ��۸�
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
                    SetMessageLn(string.Format("�ֽ�({0})/���ʲ�({1})�ȣ�{2} <= {3}����������", _cash, (_cash + _assetcash), ratio, Task.GiveUpRatio));
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
                    SetMessageLn(string.Format("ӵ�е��ʲ���{0} >= {1}����������", _myAssetsList.Count, Task.GiveUpAssetCount));
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
                SetMessageLn(string.Format("#{0}{1} ��ǰ�۸�{2}Ԫ", num, assetrecommend.Name, asset.CurrentPrice));
                if (assetrecommend.BuyPrice >= asset.CurrentPrice)
                {
                    SetMessage(string.Format(" <= �����Ƽ��۸�{0}�����Թ���...", assetrecommend.BuyPrice));

                    if (_cash < asset.CurrentPrice)
                    {
                        SetMessage(string.Format("�ֽ�{0}����", _cash));
                        return;
                    }

                    _isfirsttimebuy = true;

                    //�ֽ����������
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
                                SetMessage(string.Format("�������ʲ�({0})�͵�ǰ�ʲ�����({1})ȷ������С������{2} > ���Թ�����{3}��ֹͣ����", _cash + _assetcash, asset.CurrentPrice, minCount, tempcount));
                                return;
                            }
                        }

                        if (_isfirsttimebuy == false && Task.GiveUpIfMinimum && Task.GiveUpMinimum >= tempcount)
                        {
                            SetMessageLn(string.Format("���Թ�����{0} <= ������С������{1}��ֹͣ����", tempcount, Task.GiveUpMinimum));
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
                    SetMessage(string.Format(" > �����Ƽ��۸�{0}������", assetrecommend.BuyPrice));
                }
            }
        }
        #endregion

        #region BuyAssetAccordingCount
        private void BuyAssetAccordingCount(int assetid, string name, long purchaseprice, long count)
        {
            SetMessageLn(string.Format("����{0}��{1}", count, name));
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
                    //<div class="l r_cont" style="width:300px;"><b>��ɹ������� 0�� ����3G�������</b><br>
                    //����ֽ���<span class="c_c30">5000Ԫ</span>
                    //<div style="display:none">
                    //0�ڻ����Խ���<span class="c_c30">0</span>��!<br /></div>
                    //<div style="display:block">
                    //4Сʱ�����ѽ���6�ʽ���!<br />
                    //�����´οɽ���ʱ�仹ʣ0��</div>
                    //</div>
                    //<div class="c"></div>
                    //<div style="display:none" class="bonus_sell"><img src="http://img.kaixin001.com.cn/i3/dh/slamp.png" /> ��ʾ���㻹���Խ���һ�κ����ʶ��Ʒ���ס�</div>
                    //<div class="bbs1 mt5" style="margin:10px 0 0 140px;display:inline;width:83px;border:none">
                    //<input type="button" class="bb1-12" value="��������" style="width:83px;" onclick="javascript:new parent.dialog().reset();parent.location.reload();"/>
                    //</div>
                    //<div style="width:15px;" class="l">&nbsp;</div>
                    //<a href="javascript:new parent.dialog().reset();parent.location='/!rich/myitem.php'" class="c6 l f12 mt10" style="*margin-left:15px;">�鿴�ҵ��ʲ�</a>
                    //<div class="c"></div>
                    //</form>
                    //</div>
                    //<div id="display_2" style="display:none;border:none;" class="s_windows">
                    //<form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //<img src="http://img.kaixin001.com.cn/i3/dh/icon_js.gif" class="l"/>
                    //<div class="l r_cont" style="width:240px;"><b>����ʧ�ܣ�</b><br>
                    //ÿ4Сʱ���ֻ�ܽ���6�ʽ���!<br />
                    //�����´οɽ���ʱ�仹ʣ3Сʱ17�֡�
                    //</div>
                    //<div class="c"></div>
                    //<div style="display:none" class="bonus_sell"><img src="http://img.kaixin001.com.cn/i3/dh/slamp.png" /> ��ʾ���㻹���Խ���һ�κ����ʶ��Ʒ���ס�</div>
                    //<div class="bbs1 mt5" style="margin:20px 0 0 140px;display:inline;width:83px;border:none">
                    //<input type="button" class="bb1-12" value="����" onclick="new parent.dialog().reset();" style="width:83px"/>
                    //</div>
                    //<div class="c"></div>
                    //</form>
                    //</div>
                    //<div id="display_5" style="display:none;border:none;" class="s_windows">
                    //<form id="form1" name="form1" method="post" action="" class="ml25 mt30">
                    //<img src="http://img.kaixin001.com.cn/i3/dh/icon_js.gif" class="l"/>
                    //<div class="l r_cont" style="width:240px;"><b>����ֽ��㣡</b><br>
                    //����ֽ���<span class="c_c30">564��4776��Ԫ</span>
                    //<div style="margin:50px 0 30px 0px;">
                    //<div class="bbs1 mt5" style="margin-left:20px;display:inline;width:83px;">
                    //<input type="button" class="bb1-12" value="����" onclick="new parent.dialog().reset();" style="width:83px"/>
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
                    if (strmsg.IndexOf("ÿ4Сʱ���ֻ�ܽ���6�ʽ���") > -1 || strmsg.Contains("��Ʒ�۸��Ѿ��仯"))
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
        /// �����ֽ����Ʒ�۸�ȷ����С��������
        /// </summary>
        /// <param name="cash"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public long GetMinimumPurchaseCount(Collection<AdvancedPurchaseInfo> advancedPurchaseList, long cash, long price)
        {
            long standardcash = 0;

            //��ȡ�۸�
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

            //ȷ����С������
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
            if (content.IndexOf("<title>������ - ������</title>") != -1)
            {
                SetMessageLn("��δ��װ����������,���԰�װ��...");
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
