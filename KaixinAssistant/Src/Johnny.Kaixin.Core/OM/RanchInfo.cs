using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Johnny.Kaixin.Core
{
    public class RanchInfo
    {
        private int _rank;
        private string _ranktip;
        private string _name;
        private long _cash;
        private string _cashtip;
        private int _tcharms;
        private int _water;
        private string _watertips;
        private int _grass;
        private string _grasstips;
        
        private Collection<AnimalInfo> _animals;
        private Collection<AnimalProductInfo> _animalProducts;
        private Collection<FoodItemInfo> _foods;

        public RanchInfo()
        { }

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

        public long Cash
        {
            get { return _cash; }
            set { _cash = value; }
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

        public int Water
        {
            get { return _water; }
            set { _water = value; }
        }

        public string WaterTips
        {
            get { return _watertips; }
            set { _watertips = value; }
        }

        public int Grass
        {
            get { return _grass; }
            set { _grass = value; }
        }

        public string GrassTips
        {
            get { return _grasstips; }
            set { _grasstips = value; }
        }

        public Collection<AnimalInfo> Animals
        {
            get { return _animals; }
            set { _animals = value; }
        }

        public Collection<AnimalProductInfo> AnimalProducts
        {
            get { return _animalProducts; }
            set { _animalProducts = value; }
        }

        public Collection<FoodItemInfo> Foods
        {
            get { return _foods; }
            set { _foods = value; }
        }
    }
}
