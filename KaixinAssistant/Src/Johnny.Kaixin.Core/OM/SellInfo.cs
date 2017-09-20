using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class SellInfo
    {
        //<data>
        //  <ret>succ</ret>
        //  <goodsname>ºúÂÜ²·</goodsname>
        //  <totalprice>70</totalprice>
        //  <num>2</num>
        //  <pic>http://img.kaixin001.com.cn//i2/house/garden/crop/huluobo.swf</pic>
        //  <all>0</all>
        //</data>  
        private string _ret;
        private string _goodsname;
        private long _totalprice;
        private int _num;
        private string _pic;
        private int _all;        

        public SellInfo()
        { }

        public string Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }

        public string GoodsName
        {
            get { return _goodsname; }
            set { _goodsname = value; }
        }

        public long TotalPrice
        {
            get { return _totalprice; }
            set { _totalprice = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        public int All
        {
            get { return _all; }
            set { _all = value; }
        }        
    }
}
