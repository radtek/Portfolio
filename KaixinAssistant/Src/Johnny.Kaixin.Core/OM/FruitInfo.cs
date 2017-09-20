using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    [Serializable]
    public class FruitInfo
    {
        private int _fruitid;
        private string _name;
        private int _sellprice;
        private int _num;

        public FruitInfo()
        {
        }

        public int FruitId
        {
            get { return _fruitid; }
            set { _fruitid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int SellPrice
        {
            get { return _sellprice; }
            set { _sellprice = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public override string ToString()
        {
            return _name + "(" + _sellprice.ToString() + ")";
        }
    }
}
