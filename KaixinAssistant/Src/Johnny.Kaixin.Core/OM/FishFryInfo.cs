using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FishFryInfo
    {
        private int _fid;
        private string _name;
        private int _price;
        private decimal _fweight;
        private decimal _mprice;
        private int _rank;

        private decimal _maxweight;

        public FishFryInfo()
        { }

        public int FId
        {
            get { return _fid; }
            set { _fid = value; }
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

        public decimal FWeight
        {
            get { return _fweight; }
            set { _fweight = value; }
        }

        public decimal MPrice
        {
            get { return _mprice; }
            set { _mprice = value; }
        }

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public decimal MaxWeight
        {
            get { return _maxweight; }
            set { _maxweight = value; }
        }

        public override string ToString()
        {
            return _name + "(" + _price.ToString() + ")";
        }
    }
}
