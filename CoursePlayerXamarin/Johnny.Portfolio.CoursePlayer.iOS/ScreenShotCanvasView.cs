using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Foundation;
using UIKit;
using CoreGraphics;
using Johnny.Portfolio.CoursePlayer.Core;
using Xamarin.Forms;
using System.IO;
using Johnny.Portfolio.CoursePlayer.Core.OM;

namespace Johnny.Portfolio.CoursePlayer.iOS
{
    public class ScreenShotCanvasView : UIView
    {
        private bool needclear = false;

        public ScreenShotCanvasView()
        {
            BackgroundColor = UIColor.Clear;
        }

        public ScreenShotCanvasView(CGRect cgrect)
		{
            base.Frame = cgrect;
			BackgroundColor = UIColor.Clear;
		}
		
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);
			
			var gctx = UIGraphics.GetCurrentContext();

            if (needclear)
            {
                gctx.ClearRect(rect);
                gctx.SetStrokeColor(UIColor.Blue.CGColor);
                gctx.SetLineWidth(2);
                gctx.StrokeRect(rect);
                needclear = false;
                return;
            }

            gctx.SetStrokeColor(UIColor.Blue.CGColor);
            gctx.SetLineWidth(2);
            gctx.StrokeRect(rect);

            if (SSData != null && SSData.Count > 0)
            {
                foreach (SSImage item in SSData)
                {
                    UIImage uiImage = ToImage(item.Image);
                    uiImage.Draw(GetRect(rect.Size.Width, rect.Size.Height, item.Row, item.Col));
                }
            }
		}

        public static UIImage ToImage(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            UIImage image = null;
            try
            {
                image = new UIImage(NSData.FromArray(data));
                data = null;
            }
            catch (Exception)
            {
                return null;
            }
            return image;
        }

        private CGRect GetRect(nfloat containerwidth, nfloat containerheight, int row, int col)
        {
            if (row < 0 || col < 0 || row > 7 || col > 7)
                return new CGRect();

            double left = containerwidth / 8 * col;
            double top = containerheight / 8 * row;
            double width = containerwidth / 8;
            double height = containerheight / 8;

            return new CGRect(left, top, width, height);

        }
        
        public List<SSImage> SSData { get; set; }

        public void Clear()
        {
            needclear = true;
        }
    }
}