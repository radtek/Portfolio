using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class WaterInfo
    {
        //<data>
        //  <ret>succ</ret>
        //  <watertips>ˮ����100��&lt;br&gt;&lt;font color='#666666'&gt;��ȹ⻹��Լ400Сʱ&lt;/font&gt;</watertips>
        //  <tips>
        //  </tips>
        //</data>
        private string _ret;
        private string _watertips;
        private string _tips;

        public WaterInfo()
        { }

        public string Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }

        public string WaterTips
        {
            get { return _watertips; }
            set { _watertips = value; }
        }

        public string Tips
        {
            get { return _tips; }
            set { _tips = value; }
        }

    }
}
