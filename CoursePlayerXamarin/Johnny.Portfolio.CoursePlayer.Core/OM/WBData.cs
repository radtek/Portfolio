using System.Collections.Generic;

namespace Johnny.Portfolio.CoursePlayer.Core
{
    public class WBData
    {
        private List<WBLine> _wblines;
        private List<WBEvent> _wbevents;
        
        public WBData()
        {
            _wblines = new List<WBLine>();
            _wbevents = new List<WBEvent>();
        }

        public List<WBLine> WBLines
        {
            get { return _wblines; }
            set { _wblines = value; }
        }

        public List<WBEvent> WBEvents
        {
            get { return _wbevents; }
            set { _wbevents = value; }
        }
    }
}
