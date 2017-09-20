using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FishMaturedInfo
    {
        private int _fid;
        private string _name;
        private decimal _price;
        private decimal _weight;
        private decimal _maxweight;

        private int _uid;
        private int _tnum;        

        public FishMaturedInfo()
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

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public decimal MaxWeight
        {
            get { return _maxweight; }
            set { _maxweight = value; }
        }

        public int UId
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public int TNnum
        {
            get { return _tnum; }
            set { _tnum = value; }
        }

        public override string ToString()
        {
            return _name + "(" + _price.ToString() + ")";
        }
    }
}
