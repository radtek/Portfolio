using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class PlotInfo
    {      
        private int _water;
        private int _farmnum;
        private int _vermin;
        private long _cropsid;
        private int _fuid;
        private int _status;
        private int _grass;
        private int _shared;
        private string _pic;
        private string _fruitpic;
        private int _picwidth;
        private int _picheight;
        private int _cropsstatus;
        private int _grow;
        private int _totalgrow;
        private int _fruitnum;
        private int _seedid;
        private string _name;
        private string _wapcropspercent;
        private string _crops;
        private string _wapcrops;
        private string _farm;
        
        public PlotInfo()
        { }

        public int Water
        {
            get { return _water; }
            set { _water = value; }
        }

        public int FarmNum
        {
            get { return _farmnum; }
            set { _farmnum = value; }
        }

        public int Vermin
        {
            get { return _vermin; }
            set { _vermin = value; }
        }

        public long CropsId
        {
            get { return _cropsid; }
            set { _cropsid = value; }
        }

        public int Fuid
        {
            get { return _fuid; }
            set { _fuid = value; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int Grass
        {
            get { return _grass; }
            set { _grass = value; }
        }

        public int Shared
        {
            get { return _shared; }
            set { _shared = value; }
        }

        //public string Pic
        //{
        //    get { return _pic; }
        //    set { _pic = value; }
        //}

        //public string FruitPic
        //{
        //    get { return _fruitpic; }
        //    set { _fruitpic = value; }
        //}

        //public int PicWidth
        //{
        //    get { return _picwidth; }
        //    set { _picwidth = value; }
        //}

        //public int PicHeight
        //{
        //    get { return _picheight; }
        //    set { _picheight = value; }
        //}

        public int CropsStatus
        {
            get { return _cropsstatus; }
            set { _cropsstatus = value; }
        }

        //public int Grow
        //{
        //    get { return _grow; }
        //    set { _grow = value; }
        //}

        //public int TotalGrow
        //{
        //    get { return _totalgrow; }
        //    set { _totalgrow = value; }
        //}

        //public int FruitNum
        //{
        //    get { return _fruitnum; }
        //    set { _fruitnum = value; }
        //}

        public int SeedId
        {
            get { return _seedid; }
            set { _seedid = value; }
        }

        //public string Name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}

        //public string WapCropsPercent
        //{
        //    get { return _wapcropspercent; }
        //    set { _wapcropspercent = value; }
        //}

        public string Crops
        {
            get { return _crops; }
            set { _crops = value; }
        }

        //public string Wapcrops
        //{
        //    get { return _wapcrops; }
        //    set { _wapcrops = value; }
        //}

        public string Farm
        {
            get { return _farm; }
            set { _farm = value; }
        }

    }
}
