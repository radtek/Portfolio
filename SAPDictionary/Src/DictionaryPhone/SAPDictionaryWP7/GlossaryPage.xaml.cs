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
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;

namespace Johnny.WP7.Dictionary
{
    public partial class GlossaryPage : PhoneApplicationPage
    {
        private WordViewModel _wvm;
        private int _isthreecc = 0; //0:glossary 1:3cc

        // Constructor
        public GlossaryPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 1.0;

            ApplicationBarIconButton backButton = new ApplicationBarIconButton(new Uri("/Images/back.png", UriKind.Relative));
            backButton.Text = "Back";
            backButton.Click += new EventHandler(backButton_Click);

            ApplicationBar.Buttons.Add(backButton);

            if (App.ConfigSettings.StartView == "Search")
                pivotDictionary.SelectedIndex = 0;
            else
                pivotDictionary.SelectedIndex = 1;
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

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //http://pvgn50088790a:3399/CodeWebService.asmx
            //if (!App.ViewModel.IsDataLoaded)
            //{
            //    App.ViewModel.LoadData();
            //}

            _isthreecc = Convert.ToInt32(NavigationContext.QueryString["isthreecc"]);
            _wvm = new WordViewModel(_isthreecc, App.GetLang());
            _wvm.AllWordsFound += new WordViewModel.AllWordsFoundEventHandler(wvm_AllWordsFound);
            _wvm.WordsByAlphabetFound += new WordViewModel.WordsByAlphabetFoundEventHandler(wvm_WordsByAlphabetFound);

            if (_isthreecc == 1)
            {
                if (App.ConfigSettings.Lang == "en-US")
                    pivotDictionary.Title = "3-Character Classic";
                else
                    pivotDictionary.Title = "3-Charakter Klassisch";
            }
            else
            {
                if (App.ConfigSettings.Lang == "en-US")
                    pivotDictionary.Title = "Glossary";
                else
                    pivotDictionary.Title = "Glossar";
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

        void wvm_WordsByAlphabetFound(ObservableCollection<Word> words)
        {
            try
            {
                lstWords.Items.Clear();                

                if (words != null && words.Count > 0)
                {
                    App.IndexWords = words;

                    for (int ix = 0; ix < words.Count; ix++)
                    {
                        ListBoxItem item = new ListBoxItem();
                        if (String.IsNullOrEmpty(words[ix].Description_EN))
                            item.Content = words[ix].Name;
                        else
                            item.Content = words[ix].Name + " - " + words[ix].Description_EN;
                        item.Tag = words[ix];
                        lstWords.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }

        #region Search
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri(string.Format("/WordPage.xaml?wordname={0}&isthreecc={1}", myAutocompleteBox.Text.TrimStart(), _isthreecc), UriKind.Relative));
            }
            catch (Exception ex)
            {
                App.ShowMessageBox(ex);
            }
        }        
        #endregion

        #region Index
        private void lstWords_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBoxItem selecteditem = lstWords.SelectedItem as ListBoxItem;
                if (selecteditem != null)
                {
                    NavigationService.Navigate(new Uri(string.Format("/WordPage.xaml?wordname={0}&isthreecc={1}", (selecteditem.Tag as Word).Name, _isthreecc), UriKind.Relative));
                }
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
                HyperlinkButton button = sender as HyperlinkButton;
                if (button != null)
                {
                    btnA.FontWeight = FontWeights.Normal;
                    btnA.Foreground = new SolidColorBrush(Colors.White);
                    btnB.FontWeight = FontWeights.Normal;
                    btnB.Foreground = new SolidColorBrush(Colors.White);
                    btnC.FontWeight = FontWeights.Normal;
                    btnC.Foreground = new SolidColorBrush(Colors.White);
                    btnD.FontWeight = FontWeights.Normal;
                    btnD.Foreground = new SolidColorBrush(Colors.White);
                    btnE.FontWeight = FontWeights.Normal;
                    btnE.Foreground = new SolidColorBrush(Colors.White);
                    btnF.FontWeight = FontWeights.Normal;
                    btnF.Foreground = new SolidColorBrush(Colors.White);
                    btnG.FontWeight = FontWeights.Normal;
                    btnG.Foreground = new SolidColorBrush(Colors.White);
                    btnH.FontWeight = FontWeights.Normal;
                    btnH.Foreground = new SolidColorBrush(Colors.White);
                    btnI.FontWeight = FontWeights.Normal;
                    btnI.Foreground = new SolidColorBrush(Colors.White);
                    btnJ.FontWeight = FontWeights.Normal;
                    btnJ.Foreground = new SolidColorBrush(Colors.White);
                    btnK.FontWeight = FontWeights.Normal;
                    btnK.Foreground = new SolidColorBrush(Colors.White);
                    btnL.FontWeight = FontWeights.Normal;
                    btnL.Foreground = new SolidColorBrush(Colors.White);
                    btnM.FontWeight = FontWeights.Normal;
                    btnM.Foreground = new SolidColorBrush(Colors.White);
                    btnN.FontWeight = FontWeights.Normal;
                    btnN.Foreground = new SolidColorBrush(Colors.White);
                    btnO.FontWeight = FontWeights.Normal;
                    btnO.Foreground = new SolidColorBrush(Colors.White);
                    btnP.FontWeight = FontWeights.Normal;
                    btnP.Foreground = new SolidColorBrush(Colors.White);
                    btnQ.FontWeight = FontWeights.Normal;
                    btnQ.Foreground = new SolidColorBrush(Colors.White);
                    btnR.FontWeight = FontWeights.Normal;
                    btnR.Foreground = new SolidColorBrush(Colors.White);
                    btnS.FontWeight = FontWeights.Normal;
                    btnS.Foreground = new SolidColorBrush(Colors.White);
                    btnT.FontWeight = FontWeights.Normal;
                    btnT.Foreground = new SolidColorBrush(Colors.White);
                    btnU.FontWeight = FontWeights.Normal;
                    btnU.Foreground = new SolidColorBrush(Colors.White);
                    btnV.FontWeight = FontWeights.Normal;
                    btnV.Foreground = new SolidColorBrush(Colors.White);
                    btnW.FontWeight = FontWeights.Normal;
                    btnW.Foreground = new SolidColorBrush(Colors.White);
                    btnX.FontWeight = FontWeights.Normal;
                    btnX.Foreground = new SolidColorBrush(Colors.White);
                    btnY.FontWeight = FontWeights.Normal;
                    btnY.Foreground = new SolidColorBrush(Colors.White);
                    btnZ.FontWeight = FontWeights.Normal;
                    btnZ.Foreground = new SolidColorBrush(Colors.White);
                    button.FontWeight = FontWeights.Bold;
                    button.Foreground = new SolidColorBrush(Color.FromArgb(255, 102, 188, 227));

                    _wvm.FindWordsByAlphabet(System.Convert.ToChar(button.Content));
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