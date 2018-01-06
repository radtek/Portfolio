using System;
using System.Drawing;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace Johnny.Portfolio.CoursePlayer.iOS
{
    public partial class ThirdViewController : UIViewController
    {
        public ThirdViewController(IntPtr handle)
            : base(handle)
        {
            //Title = NSBundle.MainBundle.LocalizedString("Third", "Third");
            //TabBarItem.Image = UIImage.FromBundle("Images/playlist");
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

            // Perform any additional setup after loading the view, typically from a nib.
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