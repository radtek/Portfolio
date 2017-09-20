using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    [Serializable]
    public class SeedInfo
    {
        private int _seedid;
        private string _name;
        private int _price;
        private int _num;
        private int _sellprice;
        private bool _valid;

        public SeedInfo()
        {
            _valid = true;
        }
        
        public int SeedId
        {
            get { return _seedid; }
            set { _seedid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public int SellPrice
        {
            get { return _sellprice; }
            set { _sellprice = value; }
        }

        public bool Valid
        {
            get { return _valid; }
            set { _valid = value; }
        }

        public override string ToString()
        {
            return _name + "(" + _price.ToString() + ")";
        }
    }
}
