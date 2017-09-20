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
using System.Collections.Generic;
using System.Linq;

namespace Johnny.WP7.Dictionary
{
    public class ThreeCCViewModel
    {

        public ThreeCCViewModel()
        { }

        public Word FindTheWord(string name)
        {
            List<Word> list = (Application.Current as App).ThreeCCDB.SelectList<Word>(string.Format("select Name,Description_EN,Description_CN,Detail_EN,Detail_CN from ThreeCharClassic where Name='{0}' ", name.ToUpper()));
            if (list == null || list.Count == 0)
                return null;
            else
                return list[0];
        }

        public List<Word> FindAllWords(string name, int top)
        {
            List<Word> list = (Application.Current as App).ThreeCCDB.SelectList<Word>(string.Format("select Name,Description_EN,Description_CN,Detail_EN,Detail_CN from ThreeCharClassic where Name like '{0}%' ", name));
            if (list == null || list.Count == 0)
                return null;
            else
            {
                int count = Math.Min(list.Count, top);
                List<Word> retlist = new List<Word>();
                for (int ix = 0; ix < count; ix++)
                {
                    retlist.Add(list[ix]);
                }
                return retlist;
            }
        }

        public List<Word> FindWordsByAlphabet(char letter)
        {
            List<Word> list = (Application.Current as App).ThreeCCDB.SelectList<Word>(string.Format("select Name,Description_EN,Description_CN,Detail_EN,Detail_CN from ThreeCharClassic where Name like '{0}%' ", letter));
            if (list == null || list.Count == 0)
                return null;
            else
                return list;
        }

    }
}
