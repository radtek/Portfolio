using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Johnny.Kaixin.Core
{
    public class GardenInfo
    {
        private int _rank;
        private string _ranktip;
        private string _name;
        private string _cashtip;
        private int _tcharms;
        private bool _hasMonitor;
        private Collection<PlotInfo> _plots;
        private int _panaxcount;
        private int _panaxbabycount;
        private int _clowningcount;
        private int _stramoniumcount;
        private int _yaoqiancount;
        private long _cash;

        public GardenInfo()
        {
            _panaxcount = 0;
            _panaxbabycount = 0;
            _clowningcount = 0;
            _stramoniumcount = 0;
            _yaoqiancount = 0;
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

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string CashTip
        {
            get { return _cashtip; }
            set { _cashtip = value; }
        }

        public int TCharms
        {
            get { return _tcharms; }
            set { _tcharms = value; }
        }

        public bool HasMonitor
        {
            get { return _hasMonitor; }
            set { _hasMonitor = value; }
        }

        public Collection<PlotInfo> Plots
        {
            get { return _plots; }
            set
            {
                _plots = value;
                if (_plots != null && _plots.Count > 0)
                {
                    _panaxcount = 0;
                    _panaxbabycount = 0;
                    _clowningcount = 0;
                    _stramoniumcount = 0;
                    _yaoqiancount = 0;
                    foreach (PlotInfo plot in _plots)
                    {
                        //是否正常生长阶段
                        if (plot.Status != 1)
                            continue;

                        //人参
                        if (plot.SeedId == 21)
                            _panaxcount++;
                        //人参(有人参娃娃)
                        else if (plot.SeedId == 25)
                            _panaxbabycount++;
                        //曼珠沙华
                        else if (plot.SeedId == 104)
                            _clowningcount++;
                        //曼陀罗
                        else if (plot.SeedId == 114)
                            _stramoniumcount++;
                        //摇钱树
                        else if (plot.SeedId == 102)
                            _yaoqiancount++;
                    }
                }
            }
        }

        //人参
        public int PanaxCount
        {
            get { return _panaxcount; }
            set { _panaxcount = value; }
        }

        //人参(有人参娃娃)
        public int PanaxBabyCount
        {
            get { return _panaxbabycount; }
            set { _panaxbabycount = value; }
        }

        //曼珠沙华
        public int ClowningCount
        {
            get { return _clowningcount; }
            set { _clowningcount = value; }
        }

        //曼陀罗
        public int StramoniumCount
        {
            get { return _stramoniumcount; }
            set { _stramoniumcount = value; }
        }
        //摇钱树
        public int YaoqianCount
        {
            get { return _yaoqiancount; }
            set { _yaoqiancount = value; }
        }        
        public long Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }
    }
}
