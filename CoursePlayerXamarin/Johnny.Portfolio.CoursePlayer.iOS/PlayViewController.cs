﻿using System;
using System.Drawing;
using System.Collections.Generic;

using Foundation;
using UIKit;
using CoreGraphics;
using System.Timers;
using Johnny.Portfolio.CoursePlayer.Core;
using Xamarin;
using Johnny.Portfolio.CoursePlayer.Core.OM;

namespace Johnny.Portfolio.CoursePlayer.iOS
{
    public partial class PlayViewController : UIViewController
    {
        UIButton btnPlay;
        UISlider sliderTimeline;
        UILabel lblCurrentTime;

        WhiteBoardCanvasView canvasWB;
        ScreenShotCanvasView canvasSS;

        CourseApi _api;
        private PlayerState playStatus = PlayerState.Stopped;

        Timer timerVideo = new Timer();
        Timer timerSS = new Timer();
        Timer timerWB = new Timer();

        public PlayViewController(IntPtr handle)
            : base(handle)
        {
            Xamarin.Forms.Forms.Init();
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
                View.BackgroundColor = UIColor.White;

                btnPlay = UIButton.FromType(UIButtonType.RoundedRect);
                btnPlay.SetTitle("Play", UIControlState.Normal);
                btnPlay.Frame = new CGRect(0, 20, 320, 30);
                btnPlay.TouchUpInside += BtnPlay_TouchUpInside;
                View.AddSubview(btnPlay);

                sliderTimeline = new UISlider(new CGRect(0, 40, 320, 34));
                View.Add(sliderTimeline);

                int timeframe = 4*60*60-30*60;
                sliderTimeline.MinValue = 0f;
                sliderTimeline.MaxValue = Convert.ToSingle(timeframe);
                sliderTimeline.Value = 0f;
                sliderTimeline.ValueChanged += SliderTimeline_ValueChanged;
                sliderTimeline.TouchUpInside += SliderTimeline_TouchUpInside;
                sliderTimeline.TouchDown += SliderTimeline_TouchDown;

                lblCurrentTime = new UILabel(new CGRect(0, 74, 320, 20))
                {
                    Text = "00:00:00",
                    TextAlignment = UITextAlignment.Center
                };
                View.Add(lblCurrentTime);

                canvasSS = new ScreenShotCanvasView(new CGRect(0, 104, 320, 200));
                View.Add(canvasSS);

                canvasWB = new WhiteBoardCanvasView(new CGRect(0, 310, 320, 200));
                View.Add(canvasWB);

                _api = new CourseApi();
            }
            catch (Exception ex)
            {
                Insights.Report(ex);
            }
        }

        void SliderTimeline_ValueChanged(object sender, EventArgs e)
        {
            lblCurrentTime.Text = GetReadableTimeText(sliderTimeline.Value);
        }

        void SliderTimeline_TouchDown(object sender, EventArgs e)
        {
            if (playStatus == PlayerState.Playing)
            {
                // disable all events when touching down
                timerVideo.Elapsed -= TimerVideo_Elapsed;
                timerVideo.Enabled = false;
                timerSS.Elapsed -= TimerSS_Elapsed;
                timerSS.Enabled = false;
                timerWB.Elapsed -= TimerWB_Elapsed;
                timerWB.Enabled = false;
            }
        }

        void SliderTimeline_TouchUpInside(object sender, EventArgs e)
        {
            if (playStatus == PlayerState.Playing)
            {
                StartPlayer();
            }
        }

        void BtnPlay_TouchUpInside(object sender, EventArgs e)
        {
            if (playStatus == PlayerState.Stopped)
            {
                StartPlayer();
            }
            else if (playStatus == PlayerState.Playing)
            {
                StopPlayer();
            }
        }

        void TimerVideo_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(delegate
            {
                if (sliderTimeline.Value >= sliderTimeline.MaxValue)
                {
                    StopPlayer();
                }
                else
                {
                    sliderTimeline.Value++;
                    lblCurrentTime.Text = GetReadableTimeText(sliderTimeline.Value);
                }
            });
        }

        void TimerSS_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(delegate
            {
                int second = Convert.ToInt32(sliderTimeline.Value);
                List<SSImage> ssData = _api.GetScreenshotData(second);
                canvasSS.SSData = ssData;
                canvasSS.SetNeedsDisplay();
            });
        }

        void TimerWB_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(delegate
            {
                int second = Convert.ToInt32(sliderTimeline.Value);
                WBData wbData = _api.GetWhiteboardData(second);
                canvasWB.WhiteBoardData = wbData;
                canvasWB.CurrentSecond = second;
                canvasWB.SetNeedsDisplay();
            });
        }

        private void StartPlayer() {
            btnPlay.SetTitle("Stop", UIControlState.Normal);
            btnPlay.SetTitleColor(UIColor.Red, UIControlState.Normal);

            // enable all events
            timerVideo.Elapsed += TimerVideo_Elapsed;
            timerVideo.Interval = 1000; // Timer will tick every 1 seconds
            timerVideo.Enabled = true;  // Enable the timer
            timerVideo.Start();

            timerSS.Elapsed += TimerSS_Elapsed;
            timerSS.Interval = 1000; // Timer will tick every 1 seconds
            timerSS.Enabled = true;  // Enable the timer
            timerSS.Start();

            timerWB.Elapsed += TimerWB_Elapsed;
            timerWB.Interval = 1000; // Timer will tick every 2 seconds
            timerWB.Enabled = true;  // Enable the timer
            timerWB.Start();

            playStatus = PlayerState.Playing;
        }

        private void StopPlayer() {
            btnPlay.SetTitle("Play", UIControlState.Normal);
            btnPlay.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            timerVideo.Elapsed -= TimerVideo_Elapsed;
            timerSS.Elapsed -= TimerSS_Elapsed;
            timerWB.Elapsed -= TimerWB_Elapsed;
            timerVideo.Enabled = false;
            timerSS.Enabled = false;
            timerWB.Enabled = false;
            sliderTimeline.Value = 0f;
            lblCurrentTime.Text = "00:00:00";
            _api.Close();
            canvasWB.Clear();
            canvasWB.SetNeedsDisplay();
            canvasSS.Clear();
            canvasSS.SetNeedsDisplay();
            playStatus = PlayerState.Stopped;
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