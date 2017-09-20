using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class CalfInfo
    {
        private int _aid;
        private string _name;
        private string _skey;
        private int _price;        
        //private int _num;
        //private int _sellprice;

        public CalfInfo()
        { }

        public int AId
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string SKey
        {
            get { return _skey; }
            set { _skey = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        //public int Num
        //{
        //    get { return _num; }
        //    set { _num = value; }
        //}

        //public int SellPrice
        //{
        //    get { return _sellprice; }
        //    set { _sellprice = value; }
        //}

        public override string ToString()
        {
            return _name + "(" + _price.ToString() + ")";
        }
    }
}
