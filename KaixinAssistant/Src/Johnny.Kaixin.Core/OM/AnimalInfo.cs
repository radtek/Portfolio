using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class AnimalInfo
    {
        private long _animalsid;
        private int _status;
        private int _bproduct;
        private int _bstat;
        private string _tips;
        private string _aname;
        private string _paction;

        public AnimalInfo()
        { }

        public long AnimalSid
        {
            get { return _animalsid; }
            set { _animalsid = value; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int BProduct
        {
            get { return _bproduct; }
            set { _bproduct = value; }
        }

        public int BStat
        {
            get { return _bstat; }
            set { _bstat = value; }
        }
             
        public string Tips
        {
            get { return _tips; }
            set { _tips = value; }
        }

        public string AName
        {
            get { return _aname; }
            set { _aname = value; }
        }

        public string PAction
        {
            get { return _paction; }
            set { _paction = value; }
        }
    }
}
