using CoursePlayer.Core;
using CoursePlayer.Core.Models;
using Microsoft.AspNet.SignalR;
using SignalR.Models;
using SignalR.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Script.Serialization;

namespace SignalR
{
    public class PlayerHub : Hub
    {
        //COLDataSource _ds = new COLDataSource();
        //static int currenttime = 0;
       // static bool forcerefresh = false;
        private int currentEventTs = -1;
        private int previousMin;
        //private WBEvent lastPoint;
        //private WBLineStyle currentLineStyle;
        //private bool needclear = false;
        //static Timer playerTimer = new Timer();
        //static Dictionary<string, CourseTimer> dicTimer = new Dictionary<string, CourseTimer>();
        //static Dictionary<string, int> dicCurrent = new Dictionary<string, int>();
        //static Dictionary<string, bool> dicForceRefresh = new Dictionary<string, bool>();

        public void JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }

        public void UpdateTime(string group, string second)
        {
            int currenttime = Convert.ToInt32(second);
            List <SSImage> images = CourseApi.GetScreenshotData(currenttime);
            List<ScreenImage> list = new List<ScreenImage>();

            // convert image from byte[] to base64 string.
            foreach (SSImage item in images)
            {
                if (item.Image == null)
                {
                    continue;
                }
                list.Add(new ScreenImage { Row = item.Row, Col = item.Col, ImageStream = Convert.ToBase64String(item.Image) });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Clients.Group(group).broadcastDrawScreenshot(jss.Serialize(list));

            WBData wbData = CourseApi.GetWhiteboardData(currenttime);
            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            Clients.Group(group).broadcastDrawWhiteboard(jss2.Serialize(wbData));
        }
    }
}