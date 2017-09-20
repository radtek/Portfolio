using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Models
{
    public class WBLineStyle
    {
        public WBLineStyle()
        {
            Color = -8;
            Width = 1;
        }
        public int Color
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
    }
}