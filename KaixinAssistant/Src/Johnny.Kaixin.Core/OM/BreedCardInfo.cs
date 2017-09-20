using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class BreedCardInfo
    {
        private int _fuid;
        private int _num;
        private string _tname;
        private string _real_name;
        private string _tipstext;

        public BreedCardInfo()
        { }

        public int Fuid
        {
            get { return _fuid; }
            set { _fuid = value; }
        }

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public string TName
        {
            get { return _tname; }
            set { _tname = value; }
        }

        public string RealName
        {
            get { return _real_name; }
            set { _real_name = value; }
        }

        public string TipsText
        {
            get { return _tipstext; }
            set { _tipstext = value; }
        }
    }
}
