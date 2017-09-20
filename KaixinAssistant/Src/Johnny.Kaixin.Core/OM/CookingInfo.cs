using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class CookingInfo
    {
        private long _orderid;
        private int _dishid;
        private string _name;
        private int _foodnum;
        private int _stage;
        private int _step;
        private int _resver;

        public CookingInfo()
        { }

        public long OrderId
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

        public int DishId
        {
            get { return _dishid; }
            set { _dishid = value; }
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

        public int Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public int Resver
        {
            get { return _resver; }
            set { _resver = value; }
        }
    }
}
