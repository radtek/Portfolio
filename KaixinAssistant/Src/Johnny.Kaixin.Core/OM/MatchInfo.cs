using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class MatchInfo
    {
        private int _matchid;
        private string _name;
        private string _shortname;
        private int _distance;

        public MatchInfo()
        { }

        public int MatchId
        {
            get { return _matchid; }
            set { _matchid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; }
        }

        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override string ToString()
        {
            return _shortname + "(" + _distance.ToString() + "km)";
        }
    }
}
