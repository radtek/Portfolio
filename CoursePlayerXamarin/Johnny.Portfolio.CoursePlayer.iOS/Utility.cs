using System;
using UIKit;

namespace Johnny.Portfolio.CoursePlayer.iOS
{
    public class Utility
    {
        public static WBLineStyle GetLineStyle(int color)
        {
            WBLineStyle linestyle = new WBLineStyle();
            switch (color)
            {
                case -1:
                    linestyle.Color = UIColor.Red;
                    break;
                case -2:
                    linestyle.Color = UIColor.Blue;
                    break;
                case -3:
                    linestyle.Color = UIColor.Green;
                    break;
                case -8:
                    linestyle.Color = UIColor.Black;
                    break;
                case -9:
                    linestyle.Color = UIColor.White;
                    linestyle.Width = 8 * 10 / 12;
                    break;
                case -10:
                    linestyle.Color = UIColor.White;
                    linestyle.Width = 39 * 10 / 12;
                    break;
                default:
                    linestyle.Color = UIColor.White;
                    break;
            }

            return linestyle;
        }

        public static int GetMinute(int ts)
        {
            if (ts <= 0)
                return -1;

            TimeSpan tspan = TimeSpan.FromMilliseconds(ts);
            return (int)tspan.TotalMinutes;
        }
    }
}