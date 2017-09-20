using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class RankSeedInfo
    {
        private int _rank;
        private int _seedid;
        private string _name;

        public RankSeedInfo()
        { }

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
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

        //public override string ToString()
        //{
        //    return _name + "(" + _price.ToString() + ")";
        //}
    }
}
