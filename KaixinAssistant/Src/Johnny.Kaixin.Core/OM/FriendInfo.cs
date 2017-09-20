using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class FriendInfo
    {
        // Fields
        private int _id;
        private string _name;
        private bool _online;
        private bool _isneighbor;

        //争车位
        private bool _full; //车位满        

        //买卖朋友
        private int _price;
        private bool _gender;  //true=male,false=female

        //咬人
        private string _status; 

        //花园
        private bool _gardenshare;  //我的爱心地块
        private bool _gardenharvest;  //可以偷的花园
        private bool _gardenfee;  //防偷地块（有成熟果实，但在防护期，不能偷）
        private bool _gardengrass; //锄草
        private bool _gardenvermin; //捉虫

        //牧场        
        private bool _ranchharvest;  //可收获
        private bool _ranchfood;  //加饲料
        private bool _ranchproduct;  //可生产
        private bool _ranchwater;  //需浇水

        //钓鱼
        private bool _decor;  //可钓

        //开心餐厅
        private bool _help;
        private bool _food;
        private bool _employ;
        private bool _appinstall;
        private int _power;

        // Methods
        public FriendInfo()
        {
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool Online
        {
            get { return _online; }
            set { _online = value; }
        }

        public bool IsNeighbor
        {
            get { return _isneighbor; }
            set { _isneighbor = value; }
        }
        public bool Full
        {
            get { return _full; }
            set { _full = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public bool GardenShare
        {
            get { return _gardenshare; }
            set { _gardenshare = value; }
        }

        public bool GardenHarvest
        {
            get { return _gardenharvest; }
            set { _gardenharvest = value; }
        }

        public bool GardenFee
        {
            get { return _gardenfee; }
            set { _gardenfee = value; }
        }

        public bool GardenGrass
        {
            get { return _gardengrass; }
            set { _gardengrass = value; }
        }

        public bool GardenVermin
        {
            get { return _gardenvermin; }
            set { _gardenvermin = value; }
        }

        public bool RanchHarvest
        {
            get { return _ranchharvest; }
            set { _ranchharvest = value; }
        }

        public bool RanchFood
        {
            get { return _ranchfood; }
            set { _ranchfood = value; }
        }

        public bool RanchProduct
        {
            get { return _ranchproduct; }
            set { _ranchproduct = value; }
        }

        public bool RanchWater
        {
            get { return _ranchwater; }
            set { _ranchwater = value; }
        }

        public bool Decor
        {
            get { return _decor; }
            set { _decor = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public bool Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public bool Help
        {
            get { return _help; }
            set { _help = value; }
        }

        public bool Food
        {
            get { return _food; }
            set { _food = value; }
        }

        public bool Employ
        {
            get { return _employ; }
            set { _employ = value; }
        }

        public bool AppInstall
        {
            get { return _appinstall; }
            set { _appinstall = value; }
        }

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        public void Clone(FriendInfo source)
        {
            this.Id = source.Id;
            this.Name = source.Name;
            this.Power = source.Power;
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(_name))
                return _name + "(" + _id.ToString() + ")";
            else
                return base.ToString();
        }

    }
}
