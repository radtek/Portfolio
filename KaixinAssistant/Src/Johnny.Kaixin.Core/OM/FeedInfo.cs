using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FeedInfo
    {
        //<data>
        //  <ret>succ</ret>
        //  <grasstips>���ݣ�72��&lt;font color='#FF0000'&gt;(��Ӳ�)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;��Թ⻹��Լ288Сʱ&lt;/font&gt;</grasstips>
        //  <grass>72</grass>
        //  <animalstips>
        //  </animalstips>
        //</data>
        private string _ret;
        private string _grasstips;
        private int _grass;
        private string _animalstips;

        public FeedInfo()
        { }

        public string Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }

        public string GrassTips
        {
            get { return _grasstips; }
            set { _grasstips = value; }
        }

        public int Grass
        {
            get { return _grass; }
            set { _grass = value; }
        }

        public string AnimalsTips
        {
            get { return _animalstips; }
            set { _animalstips = value; }
        }

    }
}
