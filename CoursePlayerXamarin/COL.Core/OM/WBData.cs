using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Core
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
