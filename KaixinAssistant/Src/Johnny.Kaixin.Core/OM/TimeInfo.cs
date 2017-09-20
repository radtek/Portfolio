using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class TimeInfo
    {
        private int _hour;
        private int _minute;

        public TimeInfo()
        { }

        public TimeInfo(int hour, int minute)
        {
            _hour = hour;
            _minute = minute;
        }

        public int CompareTo(TimeInfo other)
        {
            if (this._hour - other._hour > 0)
                return 1;
            else if (this._hour - other._hour == 0)
            {
                if (this._minute - other._minute > 0)
                    return 1;
                else if (this._minute - other._minute == 0)
                    return 0;
                else
                    return -1;
            }
            else
                return -1;
        }

        public int LeftMinutes(DateTime dt)
        {
            int cmpmin = CompareTo(new TimeInfo(dt.Hour, dt.Minute));
            if (cmpmin < 0)
                return (24 - dt.Hour) * 60 - dt.Minute + this._hour * 60 + this._minute;
            else if (cmpmin == 0)
                return 0;
            else
                return (this._hour - dt.Hour) * 60 - dt.Minute + this._minute;
        }

        public int Hour
        {
            get { return _hour; }
            set { _hour = value; }
        }

        public int Minute
        {
            get { return _minute; }
            set { _minute = value; }
        }

        public override string ToString()
        {
            if (_hour >= 0 && _minute >= 0)
                return _hour + ":" + _minute;
            else
                return base.ToString();
        }
    }
}
