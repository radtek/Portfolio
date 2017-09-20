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

namespace Johnny.WP7.Dictionary
{
    public partial class StartPage : PhoneApplicationPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void btnSearchAndIndex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri(string.Format("/GlossaryPage.xaml?isthreecc=0"), UriKind.Relative));
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri(string.Format("/WordPage.xaml?isthreecc=0"), UriKind.Relative));
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        private void btnIndex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri(string.Format("/IndexPage.xaml"), UriKind.Relative));
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri(string.Format("/SettingsPage.xaml"), UriKind.Relative));
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        private void btnThreeCC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri(string.Format("/GlossaryPage.xaml?isthreecc=1"), UriKind.Relative));
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

    }
}