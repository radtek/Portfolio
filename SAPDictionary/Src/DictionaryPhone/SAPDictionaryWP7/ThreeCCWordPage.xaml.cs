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
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;

namespace Johnny.WP7.Dictionary
{
    public partial class ThreeCCWordPage : PhoneApplicationPage
    {
        private ThreeCCViewModel tccvm = new ThreeCCViewModel();
        private bool _onload = false;

        public ThreeCCWordPage()
        {
            InitializeComponent();

            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 1.0;

            ApplicationBarIconButton previousButton = new ApplicationBarIconButton(new Uri("/Images/back.png", UriKind.Relative));
            previousButton.Text = "previous";
            previousButton.Click += new EventHandler(backButton_Click);

            ApplicationBarIconButton backButton = new ApplicationBarIconButton(new Uri("/Images/cancel.png", UriKind.Relative));
            backButton.Text = "back";
            backButton.Click += new EventHandler(backButton_Click);

            ApplicationBarIconButton nextButton = new ApplicationBarIconButton(new Uri("/Images/next.png", UriKind.Relative));
            nextButton.Text = "next";
            nextButton.Click += new EventHandler(backButton_Click);


            ApplicationBar.Buttons.Add(backButton);                       
        }       
        
        void backButton_Click(object sender, EventArgs e)
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

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _onload = true;
                if (NavigationContext.QueryString.Count > 0)
                {
                    myAutocompleteBox.Text = NavigationContext.QueryString["wordname"].ToString();
                    btnSearch_Click(null, null);
                }
                _onload = false;
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        #region myAutocompleteBox
        private void myAutocompleteBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (myAutocompleteBox.SelectedItem != null && !string.IsNullOrEmpty(((Word)myAutocompleteBox.SelectedItem).Name))
                {
                    btnSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        private void myAutocompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_onload)
                    return;

                if (myAutocompleteBox.SelectedItem == null || string.IsNullOrEmpty(((Word)myAutocompleteBox.SelectedItem).Name))
                {
                    if (!String.IsNullOrEmpty(myAutocompleteBox.Text) && !String.IsNullOrEmpty(myAutocompleteBox.Text.Trim()))
                    {
                        List<Word> list = tccvm.FindAllWords(myAutocompleteBox.Text.TrimStart(), 8);
                        if (list != null && list.Count > 0)
                        {
                            Words wordlist = new Words();
                            for (int ix = 0; ix < list.Count; ix++)
                            {
                                wordlist.ListOfWords.Add(list[ix]);
                            }
                            myAutocompleteBox.ItemsSource = wordlist.ListOfWords;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        #endregion

        #region btnSearch
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(myAutocompleteBox.Text.Trim()))
                {                    
                    myAutocompleteBox.Focus();
                    return;
                }

                Word word = tccvm.FindTheWord(myAutocompleteBox.Text.TrimStart());
                if (word!=null)
                {
                    string wordcontent = App.ThreeCCTemplate.Replace("$Name$", word.Name)
                        .Replace("$Description_EN$", word.Description_EN)
                        .Replace("$Detail_EN$", word.Detail_EN)
                        .Replace("$Description_CN$", word.Description_CN)
                        .Replace("$Detail_CN$", word.Detail_CN);
                    webBrowserDescription.NavigateToString(wordcontent);
                }
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }
        #endregion
        
    }
}
