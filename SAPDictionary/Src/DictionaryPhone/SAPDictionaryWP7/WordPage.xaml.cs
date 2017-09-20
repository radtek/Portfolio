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
    public partial class WordPage : PhoneApplicationPage
    {
        private WordViewModel _wvm;
        private bool _onload = false;
        private int _isthreecc = 0;               

        public WordPage()
        {
            InitializeComponent();

            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 1.0;

            //ApplicationBarIconButton previousButton = new ApplicationBarIconButton(new Uri("/Images/back.png", UriKind.Relative));
            //previousButton.Text = "previous";
            //previousButton.Click += new EventHandler(previousButton_Click);

            ApplicationBarIconButton backButton = new ApplicationBarIconButton(new Uri("/Images/back.png", UriKind.Relative));
            backButton.Text = "back";
            backButton.Click += new EventHandler(backButton_Click);

            //ApplicationBarIconButton nextButton = new ApplicationBarIconButton(new Uri("/Images/next.png", UriKind.Relative));
            //nextButton.Text = "next";
            //nextButton.Click += new EventHandler(nextButton_Click);

            //ApplicationBar.Buttons.Add(previousButton);
            ApplicationBar.Buttons.Add(backButton);
            //ApplicationBar.Buttons.Add(nextButton);
        }

        void previousButton_Click(object sender, EventArgs e)
        {
            try
            {
                _wvm.FindPrevious();
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
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

        void nextButton_Click(object sender, EventArgs e)
        {
            try
            {
                _wvm.FindNext();
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


                //ApplicationBarIconButton btn = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
                //btn.IsEnabled = false;


                _onload = true;
                if (NavigationContext.QueryString.Count > 0)
                {
                    if (NavigationContext.QueryString.Keys.Contains("wordname"))
                        myAutocompleteBox.Text = NavigationContext.QueryString["wordname"].ToString();
                    _isthreecc = Convert.ToInt32(NavigationContext.QueryString["isthreecc"]);
                    _wvm = new WordViewModel(_isthreecc,App.GetLang());
                    _wvm.AllWordsFound += new WordViewModel.AllWordsFoundEventHandler(wvm_AllWordsFound);
                    _wvm.TheWordFound += new WordViewModel.TheWordFoundEventHandler(wvm_TheWordFound);
                    btnSearch_Click(null, null);
                }
                _onload = false;
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }
        void wvm_AllWordsFound(ObservableCollection<Word> words)
        {
            try
            {
                if (words != null && words.Count > 0)
                {
                    myAutocompleteBox.ItemsSource = words;
                }
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        void wvm_TheWordFound(Word word)
        {
            try
            {
                if (word != null)
                {                   
                    string wordcontent = "";
                    if (_isthreecc == 1)
                    {
                        wordcontent = App.ThreeCCTemplate.Replace("$Name$", word.Name)
                            .Replace("$Description_EN$", word.Description_EN)
                            .Replace("$Detail_EN$", word.Detail_EN)
                            .Replace("$Description_CN$", word.Description_CN)
                            .Replace("$Detail_CN$", word.Detail_CN);
                    }
                    else
                    {
                        wordcontent = App.GlossaryTemplate.Replace("$Name$", word.Name)
                                                   .Replace("$Domain$", word.Domain)
                                                   .Replace("$Description$", word.Description);
                    }
                    webBrowserDescription.NavigateToString(wordcontent);
                    //myAutocompleteBox.Text = word.Name;                   
                }
                else
                {
                    webBrowserDescription.NavigateToString("Not Found!");
                }
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
                        _wvm.FindAllWords(myAutocompleteBox.Text.TrimStart(), 8);
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

                _wvm.FindTheWord(myAutocompleteBox.Text.TrimStart());
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }
        #endregion
        
    }
}
