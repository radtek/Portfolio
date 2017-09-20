using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FoodInfo
    {
        private int _seedid;
        private string _name;
        private int _num;

        public FoodInfo()
        { }
        
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

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public override string ToString()
        {
            return _name + "(" + _seedid.ToString() + ")";
        }
    }
}
