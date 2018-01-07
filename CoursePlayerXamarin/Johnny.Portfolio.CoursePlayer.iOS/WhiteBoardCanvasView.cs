using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Foundation;
using UIKit;
using CoreGraphics;
using Johnny.Portfolio.CoursePlayer.Core;

namespace Johnny.Portfolio.CoursePlayer.iOS
{
    public class WhiteBoardCanvasView : UIView
    {
        private WBEvent lastPoint;
        private WBLineStyle currentLineStyle;
        private int currentEventTs = -1;
        private int previousMin;
        private bool needclear = false;

        public WhiteBoardCanvasView()
        {
            BackgroundColor = UIColor.Clear;
            //Opaque = false;
            currentLineStyle = new WBLineStyle();
        }
        public WhiteBoardCanvasView(CGRect cgrect)
		{
            base.Frame = cgrect;
			BackgroundColor = UIColor.Clear;
			//Opaque = false;
            currentLineStyle = new WBLineStyle();
		}
		
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);
			
			var gctx = UIGraphics.GetCurrentContext ();

            if (needclear)
            {
                gctx.ClearRect(rect);
                gctx.SetStrokeColor(UIColor.Green.CGColor);
                gctx.SetLineWidth(2);
                gctx.StrokeRect(rect);
                needclear = false;
                return;
            }

            gctx.SetStrokeColor(UIColor.Green.CGColor);
            gctx.SetLineWidth(2);
            gctx.StrokeRect(rect);            

            if (WhiteBoardData != null)
            {
                var xRate = rect.Size.Width / 9600;
                var yRate = rect.Size.Height / 4800;

                UIColor.Clear.SetFill();
                UIColor.Black.SetStroke();

                int currentMin = Utility.GetMinute(CurrentMilliseconds);
                if (previousMin != currentMin)
                {
                    previousMin = currentMin;
                    currentEventTs = -1;
                }

                if (WhiteBoardData.WBLines != null && WhiteBoardData.WBLines.Count > 0)
                {
                    foreach (WBLine line in WhiteBoardData.WBLines)
                    {
                        WBLineStyle linestyle = Utility.GetLineStyle(line.UColor);
                        linestyle.Color.SetStroke();
                        gctx.SetLineWidth(linestyle.Width);
                        gctx.MoveTo(line.X0 * xRate, line.Y0 * yRate);
                        gctx.AddLineToPoint(line.X1 * xRate, line.Y1 * yRate);
                        gctx.StrokePath();
                    }
                }
                
                if (WhiteBoardData.WBEvents != null && WhiteBoardData.WBEvents.Count > 0)
                {
                    Hashtable group = GroupWBEventsBySecond(WhiteBoardData.WBEvents);

                    int endMilliseconds = CurrentMilliseconds % 60000;
                    int ix;
                    for (ix = currentEventTs; ix <= endMilliseconds; ix++)
                    {
                        List<WBEvent> wbevents = group[(uint)ix] as List<WBEvent>;

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
                                    currentLineStyle.Color.SetStroke();
                                    gctx.SetLineWidth(currentLineStyle.Width);

                                    gctx.MoveTo(lastPoint.X * xRate, lastPoint.Y * yRate);
                                    gctx.AddLineToPoint(wbevent.X * xRate, wbevent.Y * yRate);
                                    gctx.StrokePath();
                                    lastPoint = wbevent;
                                }
                            }
                            else
                            {
                                switch (wbevent.X)
                                {
                                    case -100: //Pen Up
                                        currentLineStyle.Color = UIColor.Black;
                                        lastPoint = null;
                                        break;
                                    case -200: //Clear event
                                        gctx.ClearRect(rect);
                                        lastPoint = null;
                                        break;
                                    default:
                                        currentLineStyle = Utility.GetLineStyle(wbevent.X);
                                        break;
                                }
                                lastPoint = null;
                            }
                        }
                    }
                }
            }
		}

        public void Clear()
        {
            needclear = true;
        }

        public WBData WhiteBoardData { get; set; }
        public int CurrentMilliseconds { get; set; }

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