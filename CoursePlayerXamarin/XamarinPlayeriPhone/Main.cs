using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin;

namespace TabbedAppiPhone
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            //Insights.Initialize("ab3df3055f099f605c77f40d3a72fe7f9d8864db");
            //Insights.Identify("Johnny", "Email", "jojozhuang@gmail.com");
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}