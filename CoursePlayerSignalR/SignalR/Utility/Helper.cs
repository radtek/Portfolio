using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Utility
{
    public class Helper
    {
        public static WBLineStyle GetLineStyle(int color)
        {
            WBLineStyle linestyle = new WBLineStyle();
            switch (color)
            {
                case -1:
                    linestyle.Color = -1;
                    break;
                case -2:
                    linestyle.Color = -2;
                    break;
                case -3:
                    linestyle.Color = -3;
                    break;
                case -8:
                    linestyle.Color = -8;
                    break;
                case -9:
                    linestyle.Color = -9;
                    linestyle.Width = 8 * 10 / 12;
                    break;
                case -10:
                    linestyle.Color = -10;
                    linestyle.Width = 39 * 10 / 12;
                    break;
                default:
                    linestyle.Color = -10;
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