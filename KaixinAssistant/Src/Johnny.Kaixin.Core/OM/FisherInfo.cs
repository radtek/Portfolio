using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FisherInfo
    {
        private int _uid;
        private string _name;
        private int _pos;
        private long _tackleid;
        private int _fstat; // 1:ø…¿≠∏À -1:∏’œ¬∏À
        private int _bfish; //
        private string _ttitle;

        public FisherInfo()
        { }

        public int UId
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public long TackleId
        {
            get { return _tackleid; }
            set { _tackleid = value; }
        }

        public int FStat
        {
            get { return _fstat; }
            set { _fstat = value; }
        }

        public int BFish
        {
            get { return _bfish; }
            set { _bfish = value; }
        }

        public string TTitle
        {
            get { return _ttitle; }
            set { _ttitle = value; }
        }
    }
}
