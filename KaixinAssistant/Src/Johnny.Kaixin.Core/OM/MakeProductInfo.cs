using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class MakeProductInfo
    {
        //<data>
        //  <action>product</action>
        //  <ret>succ</ret>
        //  <skey>hen</skey>
        //  <ptips>已成功将高宇的芦花母鸡赶去产�?lt;br&gt;产蛋需10分种�?0分钟后再来偷</ptips>
        //  <bproduct>1</bproduct>
        //  <leftptime>10</leftptime>
        //  <tips>&lt;font color='#FF0000'&gt;产蛋�?lt;/font&gt;&lt;br&gt;预计产量�?0&lt;br&gt;&lt;font color='#666666'&gt;距离可收获还�?0�?lt;/font&gt;</tips>
        //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic>
        //  <tname>&lt;img src='http://img.kaixin001.com.cn//i2/house/ranch/animals/hen_logo1.swf' width='25' height='25' hspace='0' vspace='0'&gt;&lt;br&gt;&lt;br&gt;芦花母鸡</tname>
        //</data>

        //<data>
        //  <action>product</action>
        //  <ret>fail</ret>
        //  <reason>�ö��ﰤ���У���������</reason>
        //</data>

        private string _action;
        private string _ret;
        private string _reason;
        private string _skey;
        private string _ptips;
        private int _bproduct;
        private int _leftptime;
        private string _tips;
        private string _pic;
        private string _tname;

        public MakeProductInfo()
        { }

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public string Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        public string SKey
        {
            get { return _skey; }
            set { _skey = value; }
        }

        public string PTips
        {
            get { return _ptips; }
            set { _ptips = value; }
        }

        public int BProduct
        {
            get { return _bproduct; }
            set { _bproduct = value; }
        }

        public int LeftPTime
        {
            get { return _leftptime; }
            set { _leftptime = value; }
        }

        public string Tips
        {
            get { return _tips; }
            set { _tips = value; }
        }

        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        public string TName
        {
            get { return _tname; }
            set { _tname = value; }
        }

    }
}
