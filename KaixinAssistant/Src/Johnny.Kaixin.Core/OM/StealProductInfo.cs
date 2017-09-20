using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class StealProductInfo
    {
        //<data>
        //  <ptype>1</ptype>
        //  <skey>hen</skey>
        //  <action>steal</action>
        //  <num>1</num>
        //  <ppic>http://img.kaixin001.com.cn//i2/house/ranch/animals/egg.swf</ppic>
        //  <ret>succ</ret>
        //</data>

        //<data>
        //  <ptype>1</ptype>
        //  <skey>hen</skey>
        //  <action>steal</action>
        //  <ret>fail</ret>
        //  <reason>已偷过，做人要厚道</reason>
        //</data>

        private int _ptype;
        private string _skey;
        private string _action;
        private int _num;        
        private string _ret;
        private string _reason;

        public StealProductInfo()
        { }

        public int PType
        {
            get { return _ptype; }
            set { _ptype = value; }
        }

        public string SKey
        {
            get { return _skey; }
            set { _skey = value; }
        }

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
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

    }
}
