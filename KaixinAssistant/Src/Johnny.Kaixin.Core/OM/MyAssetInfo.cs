using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class MyAssetInfo
    {
        private int _iid;
        private string _name;
        private double _buyprice;
        private double _currentprice;
        private int _assetnum;

        public MyAssetInfo()
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

        public double BuyPrice
        {
            get { return _buyprice; }
            set { _buyprice = value; }
        }

        public double CurrentPrice
        {
            get { return _currentprice; }
            set { _currentprice = value; }
        }

        public int AssetNum
        {
            get { return _assetnum; }
            set { _assetnum = value; }
        }
        
        public override string ToString()
        {
            return string.Concat(new object[] { this._name, "(", this._iid.ToString(), ")", "--买入价：", this._buyprice.ToString(), "元","--当前价：", this._currentprice.ToString()});
        }
    }
}
