using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FeedInfo
    {
        //<data>
        //  <ret>succ</ret>
        //  <grasstips>牧草：72棵&lt;font color='#FF0000'&gt;(需加草)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;距吃光还有约288小时&lt;/font&gt;</grasstips>
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
