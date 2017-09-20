using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class DelayInfo
    {
        private int? _delayedtime;
        private int? _timeout;
        private int? _trytimes;

        public DelayInfo()
        { }

        public int? DelayedTime
        {
            get { return _delayedtime; }
            set { _delayedtime = value; }
        }
        public int? TimeOut
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
        public int? TryTimes
        {
            get { return _trytimes; }
            set { _trytimes = value; }
        }
    }
}
