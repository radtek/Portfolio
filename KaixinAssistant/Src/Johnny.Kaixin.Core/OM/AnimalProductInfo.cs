using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    //农副产品：鸡蛋，牛奶
    public class AnimalProductInfo
    {
      //<product2>
      //  <item>
      //    <uid>6195212</uid>
      //    <aid>1</aid>
      //    <type>1</type>
      //    <num>250</num>
      //    <stealnum>1</stealnum>
      //    <mtime>2009-05-08 19:55:25</mtime>
      //    <ppic>http://img.kaixin001.com.cn/i2/house/ranch/animals/egg_vip.swf</ppic>
      //    <tname>&lt;img src='http://img.kaixin001.com.cn/i2/house/ranch/animals/egg_vip.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;柴鸡蛋</tname>
      //    <skey>hen</skey>
      //    <pname>鸡蛋</pname>
      //    <tips>剩余数量：249&lt;br&gt;&lt;font color='#666666'&gt;距过保鲜期：还有1天6小时38分&lt;/font&gt;</tips>
      //  </item>
      //</product2>
          //<product2>
          //  <item>
          //    <uid>6208872</uid>
          //    <aid>3</aid>
          //    <type>0</type>
          //    <num>48</num>
          //    <stealnum>0</stealnum>
          //    <mtime>2009-05-20 17:00:26</mtime>
          //    <ppic>http://img.kaixin001.com.cn/i2/house/ranch/animals/milk.swf</ppic>
          //    <tname>&lt;img src='http://img.kaixin001.com.cn/i2/house/ranch/animals/milk.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;牛奶</tname>
          //    <skey>cow</skey>
          //    <pname>牛奶</pname>
          //    <tips>剩余数量：48&lt;br&gt;&lt;font color='#666666'&gt;距过保鲜期：还有1天20小时7分&lt;/font&gt;</tips>
          //    <oa>0</oa>
          //  </item>
          //</product2>
        private int _uid;
        private int _aid;
        private int _type;
        private int _num;
        private int _stealnum;
        private string _mtime;
        private string _ppic;
        private string _tname;
        private string _skey;
        private string _pname;
        private string _tips;
        private string _oa;

        public AnimalProductInfo()
        { }

        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public int StealNum
        {
            get { return _stealnum; }
            set { _stealnum = value; }
        }

        public string MTtime
        {
            get { return _mtime; }
            set { _mtime = value; }
        }

        public string Ppic
        {
            get { return _ppic; }
            set { _ppic = value; }
        }

        public string TName
        {
            get { return _tname; }
            set { _tname = value; }
        }

        public string SKey
        {
            get { return _skey; }
            set { _skey = value; }
        }

        public string PName
        {
            get { return _pname; }
            set { _pname = value; }
        }

        public string Tips
        {
            get { return _tips; }
            set { _tips = value; }
        }

        public string Oa
        {
            get { return _oa; }
            set { _oa = value; }
        }
    }
}
