using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FishTackleInfo
    {
        private int _tid;
        private string _name;
        private int _price;
        private int _rank;
        private int _fmweight;

        //我的鱼竿
        private int _buse;
        private int _tackleid;
        private int _status; //鱼竿状态 0:可买 1:已拥有 -1:等级未到，不可买
        private string _title;

        public FishTackleInfo()
        { }

        public int TId
        {
            get { return _tid; }
            set { _tid = value; }
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

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public int FMWeight
        {
            get { return _fmweight; }
            set { _fmweight = value; }
        }

        public int BUse
        {
            get { return _buse; }
            set { _buse = value; }
        }

        public int TackleId
        {
            get { return _tackleid; }
            set { _tackleid = value; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public override string ToString()
        {
            return _name + "(" + _price.ToString() + ")";
        }
    }
}
