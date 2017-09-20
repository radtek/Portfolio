using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Johnny.WP7.Dictionary
{
    public class Words : INotifyPropertyChanged
    {
        private ObservableCollection<Word> _listOfWords;
        public ObservableCollection<Word> ListOfWords
        {
            get { return _listOfWords; }
            set
            {
                _listOfWords = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ListOfWords"));
            }
        }

        public Words()
        {
            ListOfWords = new ObservableCollection<Word>();
        }        

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Word
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        //three chracter classic
        public string Description_EN { get; set; }
        public string Description_CN { get; set; }
        public string Detail_EN { get; set; }
        public string Detail_CN { get; set; }

        public Word()
        { }

        public Word(string name)
        {
            Name = name;
        }

        public Word(string name, string domain, string description)
        {
            Name = name;
            Domain = domain;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
