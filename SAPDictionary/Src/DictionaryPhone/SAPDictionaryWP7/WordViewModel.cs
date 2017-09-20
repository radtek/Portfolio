using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Johnny.WP7.Dictionary
{
    public class WordViewModel
    {
        private int _isthreecc;
        private string _lang;
        private Word _theword;

        public delegate void TheWordFoundEventHandler(Word word);
        public event TheWordFoundEventHandler TheWordFound;

        public delegate void AllWordsFoundEventHandler(ObservableCollection<Word> words);
        public event AllWordsFoundEventHandler AllWordsFound;

        public delegate void WordsByAlphabetFoundEventHandler(ObservableCollection<Word> words);
        public event WordsByAlphabetFoundEventHandler WordsByAlphabetFound;
           
        public WordViewModel(int threecc, string lang)
        {
            _isthreecc = threecc;
            _lang = lang;
        }

        #region FindTheWord
        public void FindTheWord(string name)
        {
            if (_isthreecc==1)
            {
                List<Word> list = (Application.Current as App).ThreeCCDB.SelectList<Word>(string.Format("select Name,Description_EN,Description_CN,Detail_EN,Detail_CN from ThreeCharClassic where Name='{0}' ", name.ToUpper()));
                if (list == null || list.Count == 0)
                    _theword = null;
                else
                    _theword = list[0];                
            }
            else
            {
                List<Word> list = (Application.Current as App).GlossaryDB.SelectList<Word>(string.Format("select Name, Domain, Description from SAPDictionary where Name like '{0}' and Language='{1}' ", name, _lang));
                if (list == null || list.Count == 0)
                {
                    if (!EnableRemote)
                    {
                        _theword = null;
                    }
                    else
                    {
                        SAPDictionaryService.SAPDictionarySoapClient client = new SAPDictionaryService.SAPDictionarySoapClient();
                        client.FindTheWordAsync(name, _lang);
                        client.FindTheWordCompleted += new EventHandler<SAPDictionaryService.FindTheWordCompletedEventArgs>(client_FindTheWordCompleted);
                    }
                }
                else
                {
                    _theword = list[0];
                }
            }
            if (TheWordFound != null)
                TheWordFound(_theword);
        }

        void client_FindTheWordCompleted(object sender, SAPDictionaryService.FindTheWordCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string[] arrword = e.Result.ToArray();
                if (arrword.Length == 3)
                    _theword = new Word(arrword[0], arrword[1], arrword[2]);
                else
                    _theword = null;
                if (TheWordFound != null)
                    TheWordFound(_theword);
            }
        }
        #endregion

        #region FindAllWords
        public void FindAllWords(string name, int top)
        {
            if (_isthreecc == 1)
            {
                ObservableCollection<Word> list = (Application.Current as App).ThreeCCDB.SelectObservableCollection<Word>(string.Format("select Name,Description_EN,Description_CN,Detail_EN,Detail_CN from ThreeCharClassic where Name like '{0}%' ", name));
                if (list == null || list.Count == 0)
                    AllWordsFound(null);
                else
                {
                    int count = Math.Min(list.Count, top);
                    ObservableCollection<Word> retlist = new ObservableCollection<Word>();
                    for (int ix = 0; ix < count; ix++)
                    {
                        retlist.Add(list[ix]);
                    }
                    AllWordsFound(retlist);
                }
            }
            else
            {
                ObservableCollection<Word> list = (Application.Current as App).GlossaryDB.SelectObservableCollection<Word>(string.Format("select Name from SAPDictionary where Name like '{0}%' and Language='{1}' ", name, _lang));
                if (list == null || list.Count == 0)
                {
                    if (!EnableRemote)
                    {
                        if (AllWordsFound != null)
                            AllWordsFound(null);
                    }
                    else
                    {
                        SAPDictionaryService.SAPDictionarySoapClient client = new SAPDictionaryService.SAPDictionarySoapClient();
                        client.FindAllWordsAsync(name, _lang, top);
                        client.FindAllWordsCompleted += new System.EventHandler<SAPDictionaryService.FindAllWordsCompletedEventArgs>(client_FindAllWordsCompleted);
                    }
                }
                else
                {
                    int count = Math.Min(list.Count, top);
                    ObservableCollection<Word> retlist = new ObservableCollection<Word>();
                    for (int ix = 0; ix < count; ix++)
                    {
                        retlist.Add(list[ix]);
                    }
                    AllWordsFound(retlist);
                }
            }
        }

        void client_FindAllWordsCompleted(object sender, SAPDictionaryService.FindAllWordsCompletedEventArgs e)
        {
            if (e.Error == null)
            {                
                if (AllWordsFound != null)
                {
                    string[] arrword = e.Result.ToArray();
                    ObservableCollection<Word> words = new ObservableCollection<Word>();
                    for (int ix = 0; ix < arrword.Length; ix++)
                    {
                        words.Add(new Word(arrword[ix]));
                    }
                    AllWordsFound(words);
                }
            }
        }
        #endregion

        #region FindWordsByAlphabet
        public void FindWordsByAlphabet(char letter)
        {
            if (_isthreecc == 1)
            {
                ObservableCollection<Word> list = (Application.Current as App).ThreeCCDB.SelectObservableCollection<Word>(string.Format("select Name,Description_EN,Description_CN,Detail_EN,Detail_CN from ThreeCharClassic where Name like '{0}%' ", letter));
                if (list == null || list.Count == 0)
                    WordsByAlphabetFound(null);
                else
                    WordsByAlphabetFound(list);
            }
            else
            {
                ObservableCollection<Word> list = (Application.Current as App).GlossaryDB.SelectObservableCollection<Word>(string.Format("select Name from SAPDictionary where Name like '{0}%' and Language='{1}' ", letter, _lang));
                if (list == null || list.Count == 0)
                {
                    if (!EnableRemote)
                    {
                        if (WordsByAlphabetFound != null)
                            WordsByAlphabetFound(null);
                    }
                    else
                    {
                        SAPDictionaryService.SAPDictionarySoapClient client = new SAPDictionaryService.SAPDictionarySoapClient();
                        client.FindWordsByAlphabetAsync(letter, _lang);
                        client.FindWordsByAlphabetCompleted += new EventHandler<SAPDictionaryService.FindWordsByAlphabetCompletedEventArgs>(client_FindWordsByAlphabetCompleted);
                    }
                }
                else
                {
                    if (WordsByAlphabetFound != null)
                        WordsByAlphabetFound(list);
                }
            }
        }

        void client_FindWordsByAlphabetCompleted(object sender, SAPDictionaryService.FindWordsByAlphabetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (WordsByAlphabetFound != null)
                {
                    string[] arrword = e.Result.ToArray();
                    ObservableCollection<Word> words = new ObservableCollection<Word>();
                    for (int ix = 0; ix < arrword.Length; ix++)
                    {
                        words.Add(new Word(arrword[ix]));
                    }
                    WordsByAlphabetFound(words);
                }
            }
        }

        #endregion        

        public void FindPrevious()
        {
            if (_theword == null || App.IndexWords == null || App.IndexWords.Count == 0)
                _theword = null;
            else
            {
                int index = App.IndexWords.IndexOf(App.IndexWords.Where(w => w.Name == _theword.Name).Single());
                if (index == -1)
                    _theword = null;
                else if (index <= 0)
                    _theword = null;
                else
                    _theword = App.IndexWords[index - 1];
            }
            if (_theword != null)
                FindTheWord(_theword.Name);
        }

        public void FindNext()
        {
            if (_theword == null || App.IndexWords == null || App.IndexWords.Count == 0)
                _theword = null;
            else
            {
                int index = App.IndexWords.IndexOf(App.IndexWords.Where(w => w.Name == _theword.Name).Single());
                if (index == -1)
                    _theword = null;
                else if (index >= App.IndexWords.Count - 1)
                    _theword = null;
                else
                    _theword = App.IndexWords[index + 1];
            }
            if (_theword != null)
                FindTheWord(_theword.Name);
        }

        public bool EnableRemote
        {
            get { return App.ConfigSettings.EnableRemote; }
        }  
    }
}
