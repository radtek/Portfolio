using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class PresentInfo
    {
        //<data>
        //  <ret>succ</ret>
        //  <name>ºÚÃµ¹å</name>
        //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop/meigui_b.swf</fruitpic>
        //  <fruit_minprice>5900</fruit_minprice>
        //  <fruit_maxprice>6100</fruit_maxprice>
        //  <fruitnum>3</fruitnum>
        //  <selfnum>3</selfnum>
        //  <bpresent>1</bpresent>
        //  <fruitprice>6000</fruitprice>
        //</data>
        private int _seedid;
        private string _ret;
        private string _name;
        private string _fruitpic;
        private int _fruit_minprice;
        private int _fruit_maxprice;
        private int _fruitnum;
        private int _selfnum;
        private int _bpresent;
        private int _fruitprice;

        public PresentInfo()
        { }

        public int SeedId
        {
            get { return _seedid; }
            set { _seedid = value; }
        }

        public string Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string FruitPic
        {
            get { return _fruitpic; }
            set { _fruitpic = value; }
        }

        public int FruitMinPrice
        {
            get { return _fruit_minprice; }
            set { _fruit_minprice = value; }
        }

        public int FruitMaxPrice
        {
            get { return _fruit_maxprice; }
            set { _fruit_maxprice = value; }
        }

        public int FruitNum
        {
            get { return _fruitnum; }
            set { _fruitnum = value; }
        }

        public int SelfNum
        {
            get { return _selfnum; }
            set { _selfnum = value; }
        }

        public int BPresent
        {
            get { return _bpresent; }
            set { _bpresent = value; }
        }

        public int FruitPrice
        {
            get { return _fruitprice; }
            set { _fruitprice = value; }
        }

        public int SellSum
        {
            get 
            {
                return _selfnum * _fruitprice;
            }            
        }        
    }
}
