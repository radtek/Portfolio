using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    [Serializable]
    public class ProductInfo
    {
    //<item>
    //  <aid>1</aid>
    //  <num>1059</num>
    //  <type>0</type>
    //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/egg.swf</pic>
    //  <name>¼¦µ°</name>
    //</item>
    //<item>
    //  <aid>1</aid>
    //  <num>371</num>
    //  <type>1</type>
    //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/egg_vip.swf</pic>
    //  <name>²ñ¼¦µ°</name>
    //</item>
    //<item>
    //  <aid>1</aid>
    //  <num>7</num>
    //  <type>2</type>
    //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/chichen1.swf</pic>
    //  <name>Õû¼¦</name>
    //</item>
    //<item>
    //  <aid>2</aid>
    //  <num>7</num>
    //  <type>2</type>
    //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/meat1.swf</pic>
    //  <name>ÕûÖí</name>
    //</item>
    //<item>
    //  <aid>3</aid>
    //  <num>84</num>
    //  <type>0</type>
    //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/milk.swf</pic>
    //  <name>Å£ÄÌ</name>
    //</item>

        private int _aid;
        private int _num;
        private int _type;
        private string _pic;        
        private string _name;
        private int _price;

        public ProductInfo()
        { }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
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

        public override string ToString()
        {
            return _name + "(" + _price.ToString() + ")";
        }

    }
}
