using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FishInfo
    {
        private int _fid;
        private long _fishid;
        private int _bproduct;
        private string _tips;

        private int _uid;
        //private int _num;
        private int _tnum;

        private decimal _currentweight;
        private decimal _maxweight;

        private string _producturl;

        public FishInfo()
        { }

        public int FId
        {
            get { return _fid; }
            set { _fid = value; }
        }

        public long FishId
        {
            get { return _fishid; }
            set { _fishid = value; }
        }

        public int BProduct
        {
            get { return _bproduct; }
            set { _bproduct = value; }
        }

        public string Tips
        {
            get { return _tips; }
            set { _tips = value; }
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

        public decimal CurrentWeight
        {
            get { return _currentweight; }
            set { _currentweight = value; }
        }

        public decimal MaxWeight
        {
            get { return _maxweight; }
            set { _maxweight = value; }
        }

        public string ProductUrl
        {
            get { return _producturl; }
            set { _producturl = value; }
        }
    }
}
