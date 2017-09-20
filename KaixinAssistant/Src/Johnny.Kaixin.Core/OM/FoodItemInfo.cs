using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FoodItemInfo
    {
         //<foods>
         //   <item>
         //     <furl>http://img.kaixin001.com.cn/i2/house/ranch/animals/food_1.swf</furl>
         //     <tips>胡萝卜：58棵&lt;font color='#FF0000'&gt;(需添加)&lt;/font&gt;&lt;br&gt;&lt;font color='#666666'&gt;距吃光还有约232小时&lt;/font&gt;</tips>
         //     <lx>530</lx>
         //     <ly>220</ly>
         //     <seedid>1</seedid>
         //     <grass>58</grass>
         //     <tname>&lt;img src='http://img.kaixin001.com.cn/i2/house/ranch/animals/huluobo.png' hspace='0' vspace='0'&gt;</tname>
         //   </item>
         // </foods>
        private string _tips;
        private int _seedid;
        private int _grass;

        public FoodItemInfo()
        { }

        public string Tips
        {
            get { return _tips; }
            set { _tips = value; }
        }

        public int SeedId
        {
            get { return _seedid; }
            set { _seedid = value; }
        }

        public int Grass
        {
            get { return _grass; }
            set { _grass = value; }
        }
    }
}
