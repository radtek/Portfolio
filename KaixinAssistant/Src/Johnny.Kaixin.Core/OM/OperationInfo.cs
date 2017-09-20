using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Johnny.Kaixin.Core
{
    public class OperationInfo
    {
        // Credentials
        private AccountInfo _account;
        private string _email;
        private string _password;

        // Park
        private Collection<int> _parkwhitelist;
        private Collection<int> _parkblacklist;
        private Collection<int> _postlist;
        private bool _postall;
        private int _carColor;

        // Bite
        private Collection<int> _bitewhitelist;
        private Collection<int> _biteblacklist;
        private Collection<int> _recoverwhitelist;
        private Collection<int> _recoverblacklist;
        private Collection<int> _allowrestlist;
        private bool _biteall;
        private int _protectid;

        // Slave
        private Collection<int> _buywhitelist;
        private Collection<int> _buyblacklist;

        // House
        private Collection<int> _staywhitelist;
        private Collection<int> _stayblacklist;
        private Collection<int> _robwhitelist;        
        private Collection<int> _robblacklist;

        // Garden
        private Collection<int> _stealwhitelist;
        private Collection<int> _stealblacklist;
        private bool _stealall;
        private Collection<int> _farmwhitelist;
        private Collection<int> _farmblacklist;
        private bool _farmall;
        private int _presentid;

        // Ranch
        private Collection<int> _helpranchwhitelist;
        private Collection<int> _helpranchblacklist;
        private bool _helpranchall;
        private Collection<int> _stealproductwhitelist;
        private Collection<int> _stealproductblacklist;
        private bool _stealproductall;
        private int _presentproductid;

        // Fish
        private Collection<int> _fishingwhitelist;
        private Collection<int> _fishingblacklist;
        private bool _fishingall;
        private Collection<int> _helpfishwhitelist;
        private Collection<int> _helpfishblacklist;
        private bool _helpfishall;
        private int _presentfishid;

        // Cafe
        private Collection<int> _hirewhitelist;
        private Collection<int> _hireblacklist;
        private bool _hireall;
        private Collection<int> _purchasewhitelist;
        private Collection<int> _purchaseblacklist;
        private bool _purchaseall;
        private int _presentfoodid;

        public OperationInfo()
        {
            _account = new AccountInfo();
            _parkwhitelist = new Collection<int>();
            _parkblacklist = new Collection<int>();
            _postlist = new Collection<int>();
            _postall = true;
            _carColor = 16711680;
//<option value="16777215" >白色</option>
//<option value="13421772" >银色</option>
//<option value="0" >黑色</option>
//<option value="16711680" selected>红色</option>
//<option value="255" >蓝色</option>
//<option value="16776960" >黄色</option>            

            _bitewhitelist = new Collection<int>();
            _biteblacklist = new Collection<int>();
            _recoverwhitelist = new Collection<int>();
            _recoverblacklist = new Collection<int>();
            _allowrestlist = new Collection<int>();
            _biteall = true;            

            _buywhitelist = new Collection<int>();
            _buyblacklist = new Collection<int>();            

            //hosue
            _staywhitelist = new Collection<int>();
            _stayblacklist = new Collection<int>();
            _robwhitelist = new Collection<int>();
            _robblacklist = new Collection<int>();

            //garden
            _stealwhitelist = new Collection<int>();
            _stealblacklist = new Collection<int>();
            _stealall = true;
            _farmwhitelist = new Collection<int>();
            _farmblacklist = new Collection<int>();
            _farmall = true;

            //ranch
            _helpranchwhitelist = new Collection<int>();
            _helpranchblacklist = new Collection<int>();
            _helpranchall = true;
            _stealproductwhitelist = new Collection<int>();
            _stealproductblacklist = new Collection<int>();
            _stealproductall = true;

            //fish
            _fishingwhitelist = new Collection<int>();
            _fishingblacklist = new Collection<int>();
            _fishingall = true;
            _helpfishwhitelist = new Collection<int>();
            _helpfishblacklist = new Collection<int>();
            _helpfishall = true;

            //cafe
            _hirewhitelist = new Collection<int>();
            _hireblacklist = new Collection<int>();
            _hireall = true;
            _purchasewhitelist = new Collection<int>();
            _purchaseblacklist = new Collection<int>();
            _purchaseall = true;
        }

        [Browsable(false)]
        public AccountInfo Account
        {
            get { return _account; }
            set { _account = value; }
        }

        [Browsable(false)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        [Browsable(false)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #region Park
        [Category("争车位")]
        [Description("优先停在此名单中好友的车位里")]
        [DisplayName("停车白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> ParkWhiteList
        {
            get { return _parkwhitelist; }
            set { _parkwhitelist = value; }
        }

        [Category("争车位")]
        [Description("尽量不停在此名单中好友的车位里")]
        [DisplayName("停车黑白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> ParkBlackList
        {
            get { return _parkblacklist; }
            set { _parkblacklist = value; }
        }

        [Category("争车位")]
        [Description("若此名单中好友的车子停在我的车位里时，对其贴条。")]
        [DisplayName("贴条名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PostList
        {
            get { return _postlist; }
            set { _postlist = value; }
        }

        [Category("争车位")]
        [Description("不论谁都贴条。若为false，则只贴名单中的好友。")]
        [DisplayName("贴所有人")]
        [DefaultValue(true)]
        public bool PostAll
        {
            get { return _postall; }
            set { _postall = value; }
        }

        [Category("争车位")]
        [Description("汽车颜色")]
        [DisplayName("汽车颜色")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public int CarColor
        {
            get { return _carColor; }
            set { _carColor = value; }
        }        
        
        #endregion

        #region Bite
        [Category("咬人")]
        [Description("优先咬此名单中的好友")]
        [DisplayName("咬人白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BiteWhiteList
        {
            get { return _bitewhitelist; }
            set { _bitewhitelist = value; }
        }

        [Category("咬人")]
        [Description("不咬此名单中的人")]
        [DisplayName("咬人黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BiteBlackList
        {
            get { return _biteblacklist; }
            set { _biteblacklist = value; }
        }

        [Category("咬人")]
        [Description("优先在此名单中好友的家中休息")]
        [DisplayName("休息白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RecoverWhiteList
        {
            get { return _recoverwhitelist; }
            set { _recoverwhitelist = value; }
        }

        [Category("咬人")]
        [Description("不在此名单中好友的家中休息")]
        [DisplayName("休息黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RecoverBlackList
        {
            get { return _recoverblacklist; }
            set { _recoverblacklist = value; }
        }

        [Category("咬人")]
        [Description("允许此名单中的好友在我家休息。如果未设置此名单，则允许任何人在我家休息。")]
        [DisplayName("允许休息名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> AllowRestList
        {
            get { return _allowrestlist; }
            set { _allowrestlist = value; }
        }

        [Category("咬人")]
        [Description("是否咬所有的好友。若为false，则只咬白名单中的好友。")]
        [DisplayName("咬所有人")]
        [DefaultValue(true)]
        public bool BiteAll
        {
            get { return _biteall; }
            set { _biteall = value; }
        }

        [Category("咬人")]
        [Description("要保护的好友")]
        [DisplayName("要保护的好友")]
        public int ProtectId
        {
            get { return _protectid; }
            set { _protectid = value; }
        }

        #endregion

        #region Slave

        [Category("朋友买卖")]
        [Description("购买此名单中的好友")]
        [DisplayName("购买白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BuyWhiteList
        {
            get { return _buywhitelist; }
            set { _buywhitelist = value; }
        }

        [Category("朋友买卖")]
        [Description("不购买此名单中的好友")]
        [DisplayName("购买黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BuyBlackList
        {
            get { return _buyblacklist; }
            set { _buyblacklist = value; }
        }
        
        #endregion

        #region House

        [Category("买房子")]
        [Description("居住在以下好友的房子里")]
        [DisplayName("居住白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StayWhiteList
        {
            get { return _staywhitelist; }
            set { _staywhitelist = value; }
        }

        [Category("买房子")]
        [Description("不居住在以下好友的房子里")]
        [DisplayName("居住黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StayBlackList
        {
            get { return _stayblacklist; }
            set { _stayblacklist = value; }
        }

        [Category("买房子")]
        [Description("抢好友到自己的房子里")]
        [DisplayName("抢人白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RobWhiteList
        {
            get { return _robwhitelist; }
            set { _robwhitelist = value; }
        }

        [Category("买房子")]
        [Description("不抢以下好友到自己的房子里")]
        [DisplayName("抢人黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RobBlackList
        {
            get { return _robblacklist; }
            set { _robblacklist = value; }
        }

        #endregion

        #region Garden

        [Category("花园")]
        [Description("偷窃以下名单中好友的菜园")]
        [DisplayName("偷窃白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealWhiteList
        {
            get { return _stealwhitelist; }
            set { _stealwhitelist = value; }
        }

        [Category("花园")]
        [Description("不偷窃以下名单中好友的菜园")]
        [DisplayName("偷窃黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealBlackList
        {
            get { return _stealblacklist; }
            set { _stealblacklist = value; }
        }

        [Category("花园")]
        [Description("不论谁都偷。若为false，则只偷白名单中的好友。")]
        [DisplayName("偷所有人")]
        [DefaultValue(true)]
        public bool StealAll
        {
            get { return _stealall; }
            set { _stealall = value; }
        }

        [Category("花园")]
        [Description("去以下名单中好友的菜园播种爱心地块")]
        [DisplayName("播种白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FarmWhiteList
        {
            get { return _farmwhitelist; }
            set { _farmwhitelist = value; }
        }

        [Category("花园")]
        [Description("不去以下名单中好友的菜园播种爱心地块")]
        [DisplayName("播种黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FarmBlackList
        {
            get { return _farmblacklist; }
            set { _farmblacklist = value; }
        }

        [Category("花园")]
        [Description("不论谁都播种。若为false，则只去白名单中好友的花园播种。")]
        [DisplayName("去所有好友的花园播种")]
        [DefaultValue(true)]
        public bool FarmAll
        {
            get { return _farmall; }
            set { _farmall = value; }
        }

        [Category("花园")]
        [Description("向该好友赠送果实")]
        [DisplayName("要赠送的好友")]
        public int PresentId
        {
            get { return _presentid; }
            set { _presentid = value; }
        }
        #endregion

        #region Ranch
        [Category("牧场")]
        [Description("去以下名单中好友的牧场帮忙（添水，添牧草，生产）")]
        [DisplayName("帮忙白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpRanchWhiteList
        {
            get { return _helpranchwhitelist; }
            set { _helpranchwhitelist = value; }
        }

        [Category("牧场")]
        [Description("不去以下名单中好友的牧场帮忙（添水，添牧草，生产）")]
        [DisplayName("帮忙黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpRanchBlackList
        {
            get { return _helpranchblacklist; }
            set { _helpranchblacklist = value; }
        }

        [Category("牧场")]
        [Description("不论谁都帮忙。若为false，则只帮白名单中的好友。")]
        [DisplayName("帮忙所有人")]
        [DefaultValue(true)]
        public bool HelpRanchAll
        {
            get { return _helpranchall; }
            set { _helpranchall = value; }
        }

        [Category("牧场")]
        [Description("去以下名单中好友的牧场偷窃农副产品")]
        [DisplayName("偷窃农副产品白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealProductWhiteList
        {
            get { return _stealproductwhitelist; }
            set { _stealproductwhitelist = value; }
        }

        [Category("牧场")]
        [Description("不去以下名单中好友的牧场偷窃农副产品")]
        [DisplayName("偷窃农副产品黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealProductBlackList
        {
            get { return _stealproductblacklist; }
            set { _stealproductblacklist = value; }
        }

        [Category("牧场")]
        [Description("不论谁都偷。若为false，则只去白名单中好友的牧场偷。")]
        [DisplayName("偷所有好友的牧场")]
        [DefaultValue(true)]
        public bool StealProductAll
        {
            get { return _stealproductall; }
            set { _stealproductall = value; }
        }

        [Category("牧场")]
        [Description("向该好友赠送农副产品")]
        [DisplayName("要赠送的好友")]
        public int PresentProductId
        {
            get { return _presentproductid; }
            set { _presentproductid = value; }
        }
        #endregion

        #region Fish

        [Category("钓鱼")]
        [Description("去以下名单中好友的鱼塘钓鱼")]
        [DisplayName("钓鱼白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FishingWhiteList
        {
            get { return _fishingwhitelist; }
            set { _fishingwhitelist = value; }
        }

        [Category("钓鱼")]
        [Description("不去以下名单中好友的鱼塘钓鱼")]
        [DisplayName("钓鱼黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FishingBlackList
        {
            get { return _fishingblacklist; }
            set { _fishingblacklist = value; }
        }

        [Category("钓鱼")]
        [Description("不论谁的鱼塘都去掉。若为false，则只去白名单中的好友鱼塘钓鱼。")]
        [DisplayName("去所有鱼塘钓鱼")]
        [DefaultValue(true)]
        public bool FishingAll
        {
            get { return _fishingall; }
            set { _fishingall = value; }
        }

        [Category("钓鱼")]
        [Description("去以下名单中好友的鱼塘帮忙收鱼")]
        [DisplayName("帮忙收鱼白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpFishWhiteList
        {
            get { return _helpfishwhitelist; }
            set { _helpfishwhitelist = value; }
        }

        [Category("钓鱼")]
        [Description("不去以下名单中好友的鱼塘帮忙收鱼")]
        [DisplayName("帮忙收鱼黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpFishBlackList
        {
            get { return _helpfishblacklist; }
            set { _helpfishblacklist = value; }
        }

        [Category("钓鱼")]
        [Description("不论谁的鱼塘都去掉。若为false，则只去白名单中的好友鱼塘钓鱼。")]
        [DisplayName("去所有鱼塘钓鱼")]
        [DefaultValue(true)]
        public bool HelpFishAll
        {
            get { return _helpfishall; }
            set { _helpfishall = value; }
        }

        [Category("钓鱼")]
        [Description("向该好友赠送鱼")]
        [DisplayName("要赠送的好友")]
        public int PresentFishId
        {
            get { return _presentfishid; }
            set { _presentfishid = value; }
        }
        #endregion

        #region Cafe

        [Category("开心餐厅")]
        [Description("雇佣以下名单中的好友")]
        [DisplayName("雇佣白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HireWhiteList
        {
            get { return _hirewhitelist; }
            set { _hirewhitelist = value; }
        }

        [Category("开心餐厅")]
        [Description("禁止雇佣以下名单中的好友")]
        [DisplayName("雇佣黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HireBlackList
        {
            get { return _hireblacklist; }
            set { _hireblacklist = value; }
        }

        [Category("开心餐厅")]
        [Description("不论谁都可以雇佣。若为false，则只雇佣白名单中的好友。")]
        [DisplayName("雇佣任意好友")]
        [DefaultValue(true)]
        public bool HireAll
        {
            get { return _hireall; }
            set { _hireall = value; }
        }

        [Category("开心餐厅")]
        [Description("收购以下名单中好友的食物")]
        [DisplayName("收购白名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PurchaseWhiteList
        {
            get { return _purchasewhitelist; }
            set { _purchasewhitelist = value; }
        }

        [Category("开心餐厅")]
        [Description("禁止收购以下名单中好友的食物")]
        [DisplayName("收购黑名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PurchaseBlackList
        {
            get { return _purchaseblacklist; }
            set { _purchaseblacklist = value; }
        }

        [Category("开心餐厅")]
        [Description("收购所有好友的食物。若为false，则只收购白名单中好友的食物。")]
        [DisplayName("收购所有好友的食物")]
        [DefaultValue(true)]
        public bool PurchaseAll
        {
            get { return _purchaseall; }
            set { _purchaseall = value; }
        }

        [Category("开心餐厅")]
        [Description("向该好友赠送食物")]
        [DisplayName("要赠送食物的好友")]
        public int PresentFoodId
        {
            get { return _presentfoodid; }
            set { _presentfoodid = value; }
        }
        #endregion

        #region override
        public override string ToString()
        {
            if (_account.UserName != null && _account.UserName != string.Empty)
                return _account.UserName;
            else
                return base.ToString();
        }
        #endregion

    }
}
