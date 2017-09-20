using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class StealInfo
    {
        //<data>
        //  <anti>0</anti>
        //  <leftnum>2</leftnum>
        //  <stealnum>2</stealnum>
        //  <num>2</num>
        //  <seedname>¶¬³æÏÄ²Ý</seedname>
        //  <fruitpic>http://img.kaixin001.com.cn//i2/house/garden/crop2/dongchongxiacao.swf</fruitpic>
        //  <ret>succ</ret>
        //</data>
        private int _anti;
        private int _leftnum;
        private int _stealnum;
        private int _num;
        private string _seedname;
        private string _fruitpic;
        private string _ret;

        public StealInfo()
        { }

        public int Anti
        {
            get { return _anti; }
            set { _anti = value; }
        }

        public int LeftNum
        {
            get { return _leftnum; }
            set { _leftnum = value; }
        }

        public int StealNum
        {
            get { return _stealnum; }
            set { _stealnum = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public string SeedName
        {
            get { return _seedname; }
            set { _seedname = value; }
        }

        public string FruitPic
        {
            get { return _fruitpic; }
            set { _fruitpic = value; }
        }

        public string Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }
    }
}
