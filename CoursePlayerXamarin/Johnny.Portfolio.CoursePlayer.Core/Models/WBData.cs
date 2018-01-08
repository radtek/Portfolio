using System.Collections.Generic;

namespace Johnny.Portfolio.CoursePlayer.Core.Models
{
    public class WBData
    {
        public WBData(List<WBLine> lines, List<WBEvent> events)
        {
            WBLines = lines;
            WBEvents = events;
        }

        public List<WBLine> WBLines { get; set; }
        public List<WBEvent> WBEvents { get; set; }
    }
}
