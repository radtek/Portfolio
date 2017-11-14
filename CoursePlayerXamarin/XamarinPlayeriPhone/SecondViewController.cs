using System;
using System.Drawing;
using System.Collections.Generic;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Timers;
using COL.Core;
using Xamarin;

namespace TabbedAppiPhone
{
    public partial class SecondViewController : UIViewController
    {
        UISlider sliderTimeline;
        UILabel lblCurrentTime;
        UIButton btnPlay;
        WhiteBoardCanvasView canvasWB;
        ScreenShotCanvasView canvasSS;
        COLDataSource _ds;
        private PlayerState playStatus = PlayerState.Stopped;

        Timer playerTimer = new Timer();
        Timer drawerTimer = new Timer();
        Timer imagerTimer = new Timer();

        public SecondViewController(IntPtr handle)
            : base(handle)
        {
            Xamarin.Forms.Forms.Init();
            Title = NSBundle.MainBundle.LocalizedString("Second", "Second");
            TabBarItem.Image = UIImage.FromBundle("Images/second");
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            try
            {
                // Perform any additional setup after loading the view, typically from a nib.
                Title = "Course Player";
                View.BackgroundColor = UIColor.White;

                sliderTimeline = new UISlider(new CGRect(0, 20, 320, 34));
                View.Add(sliderTimeline);

                int timeframe = 4*60*60-30*60;
                sliderTimeline.MinValue = 0f;
                sliderTimeline.MaxValue = Convert.ToSingle(timeframe);
                sliderTimeline.Value = 0f;
                sliderTimeline.ValueChanged += sliderTimeline_ValueChanged;
                sliderTimeline.TouchUpInside += slider_TouchUpInside;
                sliderTimeline.TouchDown += slider_TouchDown;

                lblCurrentTime = new UILabel(new CGRect(0, 54, 320, 20));
                lblCurrentTime.Text = "00:00:00";
                lblCurrentTime.TextAlignment = UITextAlignment.Center;
                View.Add(lblCurrentTime);

                btnPlay = UIButton.FromType(UIButtonType.RoundedRect);
                btnPlay.SetTitle("Play", UIControlState.Normal);
                btnPlay.Frame = new CGRect(0, 74, 320, 30);
                btnPlay.TouchUpInside += button_TouchUpInside;
                View.AddSubview(btnPlay);

                canvasSS = new ScreenShotCanvasView(new CGRect(0, 104, 320, 200));
                View.Add(canvasSS);

                canvasWB = new WhiteBoardCanvasView(new CGRect(0, 310, 320, 200));
                View.Add(canvasWB);

                _ds = new COLDataSource();
                _ds.LectureId = "204304";
            }
            catch (Exception ex)
            {
                Insights.Report(ex);
            }
        }

        void sliderTimeline_ValueChanged(object sender, EventArgs e)
        {
            lblCurrentTime.Text = GetReadableTimeText(sliderTimeline.Value);
        }

        void slider_TouchDown(object sender, EventArgs e)
        {
            if (playStatus == PlayerState.Playing)
            {
                playerTimer.Elapsed -= playerTimer_Elapsed;
                playerTimer.Enabled = false;
                imagerTimer.Elapsed -= imagerTimer_Elapsed;
                imagerTimer.Enabled = false;
            }
        }

        void slider_TouchUpInside(object sender, EventArgs e)
        {
            if (playStatus == PlayerState.Playing)
            {
                playerTimer.Elapsed += playerTimer_Elapsed;
                playerTimer.Enabled = true;
                playerTimer.Interval = 1000;
                playerTimer.Start();

                imagerTimer.Elapsed += imagerTimer_Elapsed;
                imagerTimer.Interval = 1000;
                imagerTimer.Enabled = true;
                imagerTimer.Start();
            }
        }

        void button_TouchUpInside(object sender, EventArgs e)
        {
            if (playStatus == PlayerState.Stopped)
            {
                btnPlay.SetTitle("Stop", UIControlState.Normal);
                playerTimer.Elapsed += playerTimer_Elapsed;
                playerTimer.Interval = 1000;             // Timer will tick every 1 seconds
                playerTimer.Enabled = true;                       // Enable the timer
                playerTimer.Start();

                drawerTimer.Elapsed += drawerTimer_Elapsed;
                drawerTimer.Interval = 2000;             // Timer will tick every 2 seconds
                drawerTimer.Enabled = true;                       // Enable the timer
                drawerTimer.Start();

                imagerTimer.Elapsed += imagerTimer_Elapsed;
                imagerTimer.Interval = 1000;
                imagerTimer.Enabled = true;
                imagerTimer.Start();

                playStatus = PlayerState.Playing;
            }
            else if (playStatus == PlayerState.Playing)
            {
                btnPlay.SetTitle("Play", UIControlState.Normal);                
                playerTimer.Elapsed -= playerTimer_Elapsed;
                drawerTimer.Elapsed -= drawerTimer_Elapsed;
                imagerTimer.Elapsed -= imagerTimer_Elapsed;
                playerTimer.Enabled = false;
                drawerTimer.Enabled = false;
                imagerTimer.Enabled = false;
                sliderTimeline.Value = 0f;
                lblCurrentTime.Text = "00:00:00";
                _ds.Close();
                canvasWB.Clear();
                canvasWB.SetNeedsDisplay();
                canvasSS.Clear();
                canvasSS.SetNeedsDisplay();
                playStatus = PlayerState.Stopped;
            }
        }

        void playerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(delegate
            {
                if (sliderTimeline.Value >= sliderTimeline.MaxValue)
                {
                    btnPlay.SetTitle("Play", UIControlState.Normal);
                    playerTimer.Elapsed -= playerTimer_Elapsed;
                    drawerTimer.Elapsed -= drawerTimer_Elapsed;
                    playerTimer.Enabled = false;
                    drawerTimer.Enabled = false;
                    sliderTimeline.Value = 0f;
                    lblCurrentTime.Text = "00:00:00";
                    _ds.Close();
                    canvasWB.Clear();
                    canvasWB.SetNeedsDisplay();
                }
                else
                {
                    sliderTimeline.Value++;
                    lblCurrentTime.Text = GetReadableTimeText(sliderTimeline.Value);
                }
            });
        }

        void drawerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(delegate
            {
                int second = Convert.ToInt32(sliderTimeline.Value);
                //int ts = 2367000/1000;
                WBData wb = _ds.GetWhiteBoardData(COL.Core.DataType.WB_1, second);
                canvasWB.WhiteBoardData = wb;
                canvasWB.CurrentMilliseconds = second * 1000;
                canvasWB.SetNeedsDisplay();
            });
        }

        void imagerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(delegate
            {
                int second = Convert.ToInt32(sliderTimeline.Value);
                //int ts = 2367000/1000;
                ScreenData screen = _ds.GetScreenshotData(COL.Core.DataType.ScreenShot, second);
                canvasSS.ScreenShotData = screen;
                canvasSS.SetNeedsDisplay();
            });
        }

        private string GetReadableTimeText(float input)
        {
            int totalseconds = Convert.ToInt32(input);
            int hours, minutes, seconds = 0;
            seconds = totalseconds % 60;
            hours = totalseconds / (60 * 60);
            minutes = (totalseconds - hours * 60 * 60) / 60;

            string outh, outm, outs = "";
            outh = hours < 10 ? "0" + hours.ToString() : hours.ToString();
            outm = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
            outs = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();


            return string.Format("{0}:{1}:{2}", outh, outm, outs);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}