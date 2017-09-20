using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class BreedableInfo
    {
        //<item>
        //  <uid>6209015</uid>
        //  <animalsid>2441805</animalsid>
        //  <aid>1</aid>
        //  <status>1</status>
        //  <btime>0000-00-00 00:00:00</btime>
        //  <buid>0</buid>
        //  <bnum>0</bnum>
        //  <ctime>2009-05-04 22:37:53</ctime>
        //  <gtime>2009-05-05 14:37:53</gtime>
        //  <ftime>2009-05-07 18:37:53</ftime>
        //  <grow>4</grow>
        //  <ptime>2009-05-07 17:56:04</ptime>
        //  <pnum>0</pnum>
        //  <ptype>0</ptype>
        //  <daynum>3</daynum>
        //  <fstatus>0</fstatus>
        //  <bskey>ranch_cock</bskey>
        //  <pic>http://img.kaixin001.com.cn//i2/house/ranch/animals/hen.swf</pic>
        //  <tipstext>&lt;font color='#666666'&gt;产蛋期&lt;/font&gt;&lt;br&gt;还可配种5次</tipstext>
        //</item>
        //<item>
        //  <uid>2588258</uid>
        //  <animalsid>52480032</animalsid>
        //  <aid>3</aid>
        //  <status>1</status>
        //  <btime>0000-00-00 00:00:00</btime>
        //  <buid>0</buid>
        //  <bnum>0</bnum>
        //  <ctime>2009-05-15 16:25:12</ctime>
        //  <gtime>2009-05-17 04:25:12</gtime>
        //  <ftime>2009-05-21 16:25:12</ftime>
        //  <grow>9</grow>
        //  <ptime>2009-05-21 17:24:13</ptime>
        //  <pnum>0</pnum>
        //  <ptype>0</ptype>
        //  <daynum>2</daynum>
        //  <fstatus>0</fstatus>
        //  <puid>0</puid>
        //  <bskey>ranch_bull</bskey>
        //  <pic>http://img.kaixin001.com.cn/i2/house/ranch/animals/cow9.swf</pic>
        //  <tipstext>&lt;font color='#666666'&gt;挤奶期&lt;/font&gt;&lt;br&gt;还可配种5次</tipstext>
        //</item>
        private int _uid;
        private int _animalsid;
        private int _aid;
        private int _status;
        private string _btime;
        private int _buid;
        private int _bnum;
        private string _ctime;
        private string _gtime;
        private string _ftime;
        private int _grow;
        private string _ptime;
        private int _pnum;
        private int _ptype;
        private int _daynum;
        private int _fstatus;
        private int _puid;
        private string _bskey;
        private string _pic;
        private string _tipstext;

        public BreedableInfo()
        { }

        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public int AnimalSid
        {
            get { return _animalsid; }
            set { _animalsid = value; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string BTime
        {
            get { return _btime; }
            set { _btime = value; }
        }

        public int BUid
        {
            get { return _buid; }
            set { _buid = value; }
        }

        public int BNum
        {
            get { return _bnum; }
            set { _bnum = value; }
        }

        public string CTime
        {
            get { return _ctime; }
            set { _ctime = value; }
        }

        public string GTime
        {
            get { return _gtime; }
            set { _gtime = value; }
        }

        public string FTime
        {
            get { return _ftime; }
            set { _ftime = value; }
        }

        public int Grow
        {
            get { return _grow; }
            set { _grow = value; }
        }

        public string PTime
        {
            get { return _ptime; }
            set { _ptime = value; }
        }

        public int PNum
        {
            get { return _pnum; }
            set { _pnum = value; }
        }

        public int Ptype
        {
            get { return _ptype; }
            set { _ptype = value; }
        }

        public int DayNum
        {
            get { return _daynum; }
            set { _daynum = value; }
        }

        public int FStatus
        {
            get { return _fstatus; }
            set { _fstatus = value; }
        }

        public int Puid
        {
            get { return _puid; }
            set { _puid = value; }
        }

        public string BsKey
        {
            get { return _bskey; }
            set { _bskey = value; }
        }

        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        public string TipsText
        {
            get { return _tipstext; }
            set { _tipstext = value; }
        }
    }
}
