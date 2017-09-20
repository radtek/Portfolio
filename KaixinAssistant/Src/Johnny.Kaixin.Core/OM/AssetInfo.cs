using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    [Serializable]
    public class AssetInfo
    {
        private int _iid;
        private string _name;
        private long _standardprice;
        private decimal _buyratio;
        private long _buyprice;
        private decimal _sellratio;
        private long _sellprice;
        private string _description;

        private long _currentprice;   //当前市场价

        public AssetInfo()
        { }

        public int IId
        {
            get { return _iid; }
            set { _iid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public long StandardPrice
        {
            get { return _standardprice; }
            set { _standardprice = value; }
        }

        public decimal BuyRatio
        {
            get { return _buyratio; }
            set { _buyratio = value; }
        }

        public long BuyPrice
        {
            get { return _buyprice; }
            set { _buyprice = value; }
        }

        public decimal SellRatio
        {
            get { return _sellratio; }
            set { _sellratio = value; }
        }

        public long SellPrice
        {
            get { return _sellprice; }
            set { _sellprice = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public long CurrentPrice
        {
            get { return _currentprice; }
            set { _currentprice = value; }
        }

        public override string ToString()
        {
            return string.Concat(new object[] { this._name, "(", this._iid.ToString(), ")", "--当前价：", this._currentprice.ToString() });
        }

        public AssetInfo Clone()
        {
            AssetInfo asset = new AssetInfo();
            asset.IId = this._iid;
            asset.Name = this._name;
            asset.Description = this._description;
            asset.StandardPrice = this._standardprice;
            asset.CurrentPrice = this._currentprice;
            return asset;
        }
    }
}
