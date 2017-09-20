using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Johnny.Kaixin.Core
{
    public class PondInfo
    {
        private string _title;
        private int _rank;
        private string _ranktip;
        private long _cash;
        private string _cashtips;
        private string _fish;
        private string _fishtips;
        private bool _shakable;
        private bool _netable;
        private string _sicktips; //你鱼塘的鱼正在生病，需要交1000元治病
        private string _nnetfishtips; //该鱼塘的鱼在生病中，不可网
        private bool _bangkejing;
        private long _bangkejingfishid;


        private int _buyablecapacity;

        private Collection<FishInfo> _fishs;
        private Collection<FisherInfo> _fishers;

        public PondInfo()
        { }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public string RankTip
        {
            get { return _ranktip; }
            set { _ranktip = value; }
        }

        public long Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }

        public string CashTips
        {
            get { return _cashtips; }
            set { _cashtips = value; }
        }

        public string Fish
        {
            get { return _fish; }
            set { _fish = value; }
        }

        public string FishTips
        {
            get { return _fishtips; }
            set { _fishtips = value; }
        }

        public bool Shakable
        {
            get { return _shakable; }
            set { _shakable = value; }
        }

        public bool Netable
        {
            get { return _netable; }
            set { _netable = value; }
        }

        public bool BangKeJing
        {
            get { return _bangkejing; }
            set { _bangkejing = value; }
        }

        public long BangKeJingFishId
        {
            get { return _bangkejingfishid; }
            set { _bangkejingfishid = value; }
        }

        public string SickTips
        {
            get { return _sicktips; }
            set { _sicktips = value; }
        }

        public string NnetFishTips
        {
            get { return _nnetfishtips; }
            set { _nnetfishtips = value; }
        }
        
        public int BuyableCapacity
        {
            get 
            {
                if (!String.IsNullOrEmpty(_fish))
                {
                    string[] info = _fish.Split('/');
                    if (info.Length == 2)
                    {
                        return Convert.ToInt32(info[1]) - Convert.ToInt32(info[0]);
                    }
                }
                return 0;
            }
        }

        public Collection<FishInfo> Fishs
        {
            get { return _fishs; }
            set { _fishs = value; }
        }

        public Collection<FisherInfo> Fishers
        {
            get { return _fishers; }
            set { _fishers = value; }
        }
    }
}
