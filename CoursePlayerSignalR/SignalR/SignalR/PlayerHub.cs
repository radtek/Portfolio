using COL.Core;
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
        private WBEvent lastPoint;
        private WBLineStyle currentLineStyle;
        //private bool needclear = false;
        //static Timer playerTimer = new Timer();
        static Dictionary<string, CourseTimer> dicTimer = new Dictionary<string, CourseTimer>();
        static Dictionary<string, int> dicCurrent = new Dictionary<string, int>();
        static Dictionary<string, bool> dicForceRefresh = new Dictionary<string, bool>();
        static Dictionary<string, COLDataSource> dicDS = new Dictionary<string, COLDataSource>();

        public void JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }
        //public void SendDraw(string drawObject, string sessionId, string groupName, string name)
        //{
        //    Clients.Group(groupName).HandleDraw(drawObject, sessionId, name);
        //}

        //public void SendDraw(string groupName, string x, string y, string drawtype)
        //{
        //    //Clients.Group(groupName).broadcastDraw(x, y, drawtype);
        //    // exclude self
        //    Clients.Others.broadcastDraw(x, y, drawtype);
        //}

        public void Play(string group, string second)
        {            
            int currenttime = Convert.ToInt32(second);
            //Draw(group, 7070, true, new COLDataSource());
            CourseTimer playerTimer = new CourseTimer(group);
            playerTimer.Elapsed += playerTimer_Elapsed;
            playerTimer.Interval = 1000;             // Timer will tick every 1 seconds
            playerTimer.Enabled = true;                       // Enable the timer
            playerTimer.Start();
            if (!dicTimer.ContainsKey(group))
            {
                dicTimer.Add(group, playerTimer);
                dicCurrent.Add(group, currenttime);
                dicForceRefresh.Add(group, false);
                dicDS.Add(group, new COLDataSource());
            }
        }
        public void Stop(string group)
        {
            CourseTimer timer;
            if (dicTimer.TryGetValue(group, out timer)) // Returns true.
            {
                timer.Stop();
                timer.Enabled = false;
                dicTimer.Remove(group);
                dicCurrent.Remove(group);
                dicForceRefresh.Remove(group);
                dicDS.Remove(group);
            }            
        }
        public void Jump(string group, string second)
        {
            int currenttime = Convert.ToInt32(second);
            dicCurrent[group] = currenttime;
            dicForceRefresh[group] = true;
        }
        private void playerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CourseTimer ct = sender as CourseTimer;
            Draw(ct.GroupName, dicCurrent[ct.GroupName], dicForceRefresh[ct.GroupName], dicDS[ct.GroupName]);
            dicCurrent[ct.GroupName] = dicCurrent[ct.GroupName] + 1;
        }

        private void Draw(string group, int currenttime, bool forcerefresh, COLDataSource ds)
        {
            ds.Root = AppDomain.CurrentDomain.BaseDirectory;
            ds.LectureId = "204304";

            DrawScreenShot(group, currenttime, forcerefresh, ds);
            DrawWhiteBoard(group, currenttime, forcerefresh, ds);            
        }

        private void DrawScreenShot(string group, int currenttime, bool forcerefresh, COLDataSource ds)
        {
            ScreenData screen = ds.GetScreenshotData(DataType.ScreenShot, currenttime);
            if (screen == null)
            {
                return;
            }
            if (screen.Images != null && screen.Images.Count > 0)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<ScreenImage> list = new List<ScreenImage>();

                foreach (KeyValuePair<int, byte[]> item in screen.Images)
                {
                    if (item.Value == null)
                    {
                        continue;
                    } 
                    //row 0~7, col 0~7
                    int row = item.Key / Constants.MAX_ROW_NO;
                    int col = item.Key % Constants.MAX_COL_NO;
                    list.Add(new ScreenImage { Row = row, Col = col, ImageStream = Convert.ToBase64String(item.Value) });
                }
                Clients.Group(group).broadcastDrawImage(jss.Serialize(list));                
            }

        }
        private void DrawWhiteBoard(string group, int currenttime, bool forcerefresh, COLDataSource ds)
        {
            bool drawLines = true;
            int currentMin = Helper.GetMinute(currenttime * 1000);
            if (previousMin == currentMin && forcerefresh == false)
            {
                // Whiteboard data is based on minute, not second.
                // Only draw when in different minute, or user changed the slider manually.
                drawLines = false;
            }
            else
            {
                forcerefresh = false;
                previousMin = currentMin;
                currentEventTs = -1;
            }

            WBData wbdata = ds.GetWhiteBoardData(DataType.WB_1, currenttime);
            if (wbdata == null)
            {
                return;
            }            

            if (drawLines && wbdata.WBLines != null && wbdata.WBLines.Count > 0)
            {
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<BoardLine> list = new List<BoardLine>();
                foreach (WBLine line in wbdata.WBLines)
                {
                    WBLineStyle linestyle = Helper.GetLineStyle(line.UColor);
                    Clients.Group(group).broadcastDrawWBLine(linestyle.Color, linestyle.Width, line.X0, line.Y0, line.X1, line.Y1);
                    //list.Add(new BoardLine { Color = linestyle.Color, Width = linestyle.Width, X0 = line.X0, Y0 = line.Y0, X1 = line.X1, Y1 = line.Y1 });
                }
                Clients.Group(group).broadcastDrawWBLineFinished();
                //Clients.All.broadcastDrawWBLine(jss.Serialize(list));
            }

            if (wbdata.WBEvents != null && wbdata.WBEvents.Count > 0)
            {
                Hashtable htgroup = GroupWBEventsBySecond(wbdata.WBEvents);

                int endMilliseconds = currenttime * 1000 % 60000;
                int ix;
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<BoardLine> list = new List<BoardLine>();
                for (ix = currentEventTs; ix <= endMilliseconds; ix++)
                {
                    List<WBEvent> wbevents = htgroup[(uint)ix] as List<WBEvent>;

                    if (wbevents == null)
                        continue;

                    foreach (WBEvent wbevent in wbevents)
                    {

                        if (wbevent.X >= 0)
                        {
                            if (lastPoint == null)
                                lastPoint = wbevent;
                            else
                            {
                                //currentLineStyle.Color.SetStroke();
                                //gctx.SetLineWidth(currentLineStyle.Width);
                                //gctx.MoveTo(lastPoint.X * xRate, lastPoint.Y * yRate);
                                //gctx.AddLineToPoint(wbevent.X * xRate, wbevent.Y * yRate);
                                //gctx.StrokePath();
                                Clients.Group(group).broadcastDrawWBEvent(currentLineStyle.Color, currentLineStyle.Width, lastPoint.X, lastPoint.Y, wbevent.X, wbevent.Y);
                                //list.Add(new BoardLine { Color = currentLineStyle.Color, Width = currentLineStyle.Width, X0 = lastPoint.X, Y0 = lastPoint.Y, X1 = wbevent.X, Y1 = wbevent.Y });
                                lastPoint = wbevent;
                            }
                        }
                        else
                        {
                            switch (wbevent.X)
                            {
                                case -100: //Pen Up
                                    currentLineStyle.Color = -8;
                                    lastPoint = null;
                                    break;
                                case -200: //Clear event
                                           //gctx.ClearRect(rect);
                                    Clients.Group(group).broadcastClearReat();
                                    lastPoint = null;
                                    break;
                                default:
                                    currentLineStyle = Helper.GetLineStyle(wbevent.X);
                                    break;
                            }
                            lastPoint = null;
                        }
                    }
                }
                Clients.Group(group).broadcastDrawWBEventFinished();
                // Clients.All.broadcastDrawWBEvent(jss.Serialize(list));
                currentEventTs = ix;
            }
        }
        private Hashtable GroupWBEventsBySecond(List<WBEvent> lstEvents)
        {
            Hashtable ht = new Hashtable();
            foreach (WBEvent item in lstEvents)
            {
                if (!ht.Contains(item.TimeStamp))
                {
                    List<WBEvent> newlist = new List<WBEvent>();
                    newlist.Add(item);
                    ht.Add(item.TimeStamp, newlist);
                }
                else
                {
                    List<WBEvent> existlist = ht[item.TimeStamp] as List<WBEvent>;
                    existlist.Add(item);
                }
            }

            return ht;
        }
    }
}