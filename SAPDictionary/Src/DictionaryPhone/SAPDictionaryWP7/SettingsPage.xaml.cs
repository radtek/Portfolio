using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Johnny.WP7.Dictionary
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            // Add an Application Bar that has a 'done' confirmation button and 
            // a 'cancel' button
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 1.0;

            ApplicationBarIconButton doneButton = new ApplicationBarIconButton(new Uri("/Images/check.png", UriKind.Relative));
            doneButton.Text = "Done";
            doneButton.Click += new EventHandler(doneButton_Click);

            ApplicationBarIconButton cancelButton = new ApplicationBarIconButton(new Uri("/Images/cancel.png", UriKind.Relative));
            cancelButton.Text = "Cancel";
            cancelButton.Click += new EventHandler(cancelButton_Click);

            ApplicationBar.Buttons.Add(doneButton);
            ApplicationBar.Buttons.Add(cancelButton);

            chkEnableRemote.IsChecked = App.ConfigSettings.EnableRemote;
            switch (App.ConfigSettings.Lang)
            {                
                case "de-DE":
                    rdbGerman.IsChecked = true;
                    break;                
                case "en-US":
                    rdbEnglish.IsChecked = true;
                    break;
                default:
                    rdbEnglish.IsChecked = true;
                    break;
            }
            switch (App.ConfigSettings.StartView)
            {
                case "Search":
                    rdbSearch.IsChecked = true;
                    break;
                case "Index":
                    rdbIndex.IsChecked = true;
                    break;
                default:
                    rdbSearch.IsChecked = true;
                    break;
            }
        }

        void doneButton_Click(object sender, EventArgs e)
        {
            try
            {
                //enable remote
                App.ConfigSettings.EnableRemote = chkEnableRemote.IsChecked.Value;
                //lang
                if (rdbEnglish.IsChecked.Value == true)
                    App.ConfigSettings.Lang = "en-US";
                else if (rdbGerman.IsChecked.Value == true)
                    App.ConfigSettings.Lang = "de-DE";
                else
                    App.ConfigSettings.Lang = "en-US";
                //start view
                if (rdbSearch.IsChecked.Value == true)
                    App.ConfigSettings.StartView = "Search";
                else if (rdbIndex.IsChecked.Value == true)
                    App.ConfigSettings.StartView = "Index";
                else
                    App.ConfigSettings.StartView = "Search";
                
                NavigationService.Navigate(new Uri("/StartPage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }
    }
}