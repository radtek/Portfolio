using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class DinnerTableInfo
    {
        private long _orderid;
        private string _name;
        private int _foodnum;
        private int _dishid;
        private int _num;
        private int _resver;

        public DinnerTableInfo()
        { }

        public long OrderId
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int FoodNum
        {
            get { return _foodnum; }
            set { _foodnum = value; }
        }

        public int DishId
        {
            get { return _dishid; }
            set { _dishid = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public int Resver
        {
            get { return _resver; }
            set { _resver = value; }
        }

        public override string ToString()
        {
            return _name + "(" + _num.ToString() + ")";
        }
    }
}
