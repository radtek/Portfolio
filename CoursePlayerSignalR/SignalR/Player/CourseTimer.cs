using COL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;

namespace SignalR
{
    public class CourseTimer: Timer
    {
        private string groupname;

        public CourseTimer(string groupname)
        {
            this.groupname = groupname;
        }        

        public String GroupName
        {
            get { return this.groupname; }
            set { this.groupname = value; }
        }
    }
}