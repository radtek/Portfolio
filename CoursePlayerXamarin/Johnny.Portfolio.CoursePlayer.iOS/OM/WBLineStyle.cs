using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Johnny.Portfolio.CoursePlayer.iOS
{
    public class WBLineStyle
    {
        public WBLineStyle()
        {
            Color = UIColor.Black;
            Width = 1;
        }
        public UIColor Color
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