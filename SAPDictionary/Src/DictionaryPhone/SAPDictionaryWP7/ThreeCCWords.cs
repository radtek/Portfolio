using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Johnny.WP7.Dictionary
{
    public class ThreeCCWords : INotifyPropertyChanged
    {
        private ObservableCollection<ThreeCCWord> _listOfWords;
        public ObservableCollection<ThreeCCWord> ListOfWords
        {
            get { return _listOfWords; }
            set
            {
                _listOfWords = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ListOfWords"));
            }
        }

        public ThreeCCWords()
        {
            ListOfWords = new ObservableCollection<ThreeCCWord>();            
        }        

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ThreeCCWord
    {
        private string _name;
        private string _description_en;
        private string _description_cn;
        private string _detail_en;
        private string _detail_cn;

        public ThreeCCWord()
        {

        }

        public ThreeCCWord(string name, string description_en, string description_cn, string detail_en, string detail_cn)
        {
            _name = name;
            _description_en = description_en;
            _description_cn = description_cn;
            _detail_en = detail_en;
            _detail_cn = detail_cn;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description_EN
        {
            get { return _description_en; }
            set { _description_en = value; }
        }

        public string Description_CN
        {
            get { return _description_cn; }
            set { _description_cn = value; }
        }

        public string Detail_EN
        {
            get { return _detail_en; }
            set { _detail_en = value; }
        }

        public string Detail_CN
        {
            get { return _detail_cn; }
            set { _detail_cn = value; }
        }

        public override string ToString()
        {
            return Name;
        }

        //public string Description
    }
}
