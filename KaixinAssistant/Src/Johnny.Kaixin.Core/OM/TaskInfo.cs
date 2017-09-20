using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.ObjectModel;

namespace Johnny.Kaixin.Core
{
    public class TaskInfo
    {
        private string _taskid;
        private string _taskname;
        private string _groupname;
        private EnumRunMode _runmode; //true 循环; false 定时
        private int _roundtime;
        private bool _forbidden;
        private TimeInfo _forbiddenstart;
        private TimeInfo _forbiddenend;
        private Collection<TimeInfo> _starttimes;
        private bool _executepark;
        private bool _executebite;
        private bool _executeslave;
        private bool _executehouse;
        private bool _executegarden;
        private bool _executeranch;
        private bool _executefish;
        private bool _executerich;
        private bool _executecafe;
        private bool _sendlog;
        private string _receiveremail;
        private bool _writelogtofile;
        private bool _skipvalidation;
        //park
        private bool _parkmycars;
        private bool _postotherscars;
        private bool _joinmatch;
        private bool _originatematch;
        private int _originatematchid;
        private int _originateteamnum;
        private bool _startcar;
        private bool _cheerup;
        private TimeInfo _startcartime;

        //bite
        private bool _approverecovery;
        private bool _biteothers;
        private bool _autorecover;
        private bool _protectfriend;

        //slave
        private bool _buyslave;
        private bool _buylowpriceslave;
        private bool _fawnmaster;
        private bool _propitiateslave;
        private bool _afflictslave;
        private bool _releaseslave;
        private int _maxSlaves;
        private string _nickname;

        //house
        private bool _dojob;
        private bool _stayhouse;
        private bool _robfriends;
        private bool _robfreefriends;
        private bool _drivefriends;

        //garden
        private bool _farmself;
        private bool _expensivefarmself;
        private int _customfarmself;
        private bool _farmshared;
        private bool _expensivefarmshared;
        private int _customfarmshared;
        private bool _harvestfruit;
        private bool _buyseed;
        private int _buyseedcount;
        private bool _helpothers;
        private bool _stealfruit;
        private bool _presentfruit;
        private bool _presentfruitbyprice;
        private bool _presentfruitcheckvalue;
        private int _presentfruitvalue;
        private int _presentfruitid;
        private bool _presentfruitchecknum;
        private int _presentfruitnum;
        private bool _sellfruit;
        private bool _lowcash;
        private int _lowcashlimit;
        private bool _sellallfruit;
        private int _maxselllimit;
        private Collection<int> _sellforbiddennfruitslist;
        private bool _sowmyseedsfirst;
        private bool _stealunknowfruit;
        private Collection<int> _stealforbiddenfruitslist;

        //ranch
        private bool _harvestproduct;
        private bool _harvestanimal;
        private bool _addwater;
        private bool _helpaddwater;
        private bool _addgrass;
        private bool _helpaddgrass;
        private bool _buycalf;        
        private bool _buycalfbyprice;
        private int _buycalfcustom;
        private bool _stealproduct;
        private bool _makeproduct;
        private bool _helpmakeproduct;
        private bool _breedanimal;
        private int _foodnum;
        private bool _presentproduct;
        private bool _presentproductbyprice;
        private bool _presentproductcheckvalue;
        private int _presentproductvalue;
        private int _presentproductaid;
        private int _presentproducttype;
        private bool _presentproductchecknum;
        private int _presentproductnum;
        private bool _sellproduct;
        private bool _sellproductlowcash;
        private int _sellproductlowcashlimit;
        private bool _sellallproducts;
        private int _sellproductmaxlimit;
        private Collection<ProductInfo> _sellproductforbiddenlist;
        private bool _addcarrot;
        private bool _helpaddcarrot;
        private int _carrotnum;
        private bool _addbamboo;
        private bool _helpaddbamboo;
        private int _bamboonum;

        //fish
        private bool _shake;
        private bool _treatfish;
        private bool _updatefishpond;
        private bool _bangkejing;
        private bool _buyfish;
        private int _maxfishes;
        private bool _buyfishbyrank;
        private int _buyfishfishid;
        private bool _fishing;
        private bool _buyupdatetackle;
        private int _maxtackles;
        private bool _harvestfish;
        private bool _netselffish;
        private bool _netselffishcheap;
        private int _netselffishmature;
        private bool _helpfish;
        private bool _presentfish;
        private bool _presentfishcheap;
        private bool _presentfishcheckvalue;
        private int _presentfishvalue;
        private Collection<int> _presentfishforbiddenlist;
        private bool _sellfish;
        private bool _sellfishlowcash;
        private int _sellfishlowcashlimit;
        private bool _sellallfish;
        private bool _sellfishcheckvalue;
        private int _sellfishvalue;
        private int _sellfishmaxlimit;
        private Collection<int> _sellfishforbiddenlist;

        //rich
        private bool _sellasset;
        private bool _buyasset;
        private bool _buyassetcheap; //true:先购买便宜的，false:先购买贵的
        private bool _giveupifratio;    //现金/总资产比率低于设定值时停止购买
        private int _giveupratio;       //现金/总资产比
        private bool _giveupifminimum;  //连续购买时（第2次及以后），购买数低于设定值时停止购买
        private int _giveupminimum;     //连续最小购买数
        private bool _giveupifmyasset;  //拥有的资产项超过设定值时停止购买
        private int _giveupassetcount;  //资产项目数        
        private bool _advancedpurchase;  //高级购买数量控制
        private Collection<int> _buyassetslist;

        //cafe
        private bool _boxclean;
        private bool _cook;
        private bool _cooktomatofirst;
        private bool _cookmedlarfirst;
        private bool _cookcrabfirst;
        private bool _cookpineapplefirst;
        private int _cookdishid;
        private bool _cooklowcash;
        private long _cooklowcashlimit;
        private bool _hire;
        private int _maxemployees;
        private bool _helpfriend;
        private bool _presentfood;
        private Collection<int> _presentforbiddenfoodlist;
        private bool _presentfoodbycount;
        private int _presentfooddishid;
        private string _presentfoodmessage;
        private int _presentfoodratio;
        private bool _presentlowcash;
        private long _presentlowcashlimit;
        private bool _presentfoodlowcount;
        private int _presentfoodlowcountlimit;
        private bool _purchasefood;
        private bool _purchasefoodbyrefprice;
        private bool _sellfood;
        private bool _sellfoodbyrefprice;

        private Collection<AccountInfo> _accounts;

        public TaskInfo()
        {
            _runmode = EnumRunMode.MultiLoop;
            _roundtime = 60;
            _forbidden = true;
            _forbiddenstart = new TimeInfo(0, 0);
            _forbiddenend = new TimeInfo(8, 0);
            _starttimes = new Collection<TimeInfo>();
            _executepark = true;
            _executebite = true;
            _executeslave = true;
            _executehouse = true;
            _executegarden = true;
            _executeranch = true;
            _executefish = true;
            _executerich = true;
            _sendlog = false;
            _receiveremail = "";
            _writelogtofile = false;
            _skipvalidation = false;

            _accounts = new Collection<AccountInfo>();
            _parkmycars = true;
            _postotherscars = false;
            _joinmatch = true;
            _originatematch = true;
            _originatematchid = 1;
            _originateteamnum = 2;
            _startcar = false;
            _cheerup = true;
            _startcartime = new TimeInfo(8, 30);

            _approverecovery = true;
            _biteothers = true;
            _autorecover = true;
            _protectfriend = false;

            _buyslave = true;
            _buylowpriceslave = false;
            _fawnmaster = true;
            _propitiateslave = true;
            _afflictslave = true;
            _releaseslave = false;
            _maxSlaves = 9;
            _nickname = "阿桂，挖煤啦！";

            _dojob = true;
            _stayhouse = true;
            _robfriends = false;
            _robfreefriends = true;
            _drivefriends = false;

            _farmself = true;
            _expensivefarmself = true;
            _customfarmself = 1;
            _farmshared = false;
            _expensivefarmshared = true;
            _customfarmshared = 1;
            _harvestfruit = true;
            _buyseed = true;
            _buyseedcount = 1;
            _helpothers = true;
            _stealfruit = false;
            _presentfruit = false;
            _presentfruitbyprice = true;
            _presentfruitcheckvalue = true;
            _presentfruitvalue = 100;
            _presentfruitid = 11;
            _presentfruitchecknum = true;
            _presentfruitnum = 1000;
            _sellfruit = false;
            _lowcash = true;
            _lowcashlimit = 100;
            _sellallfruit = false;
            _maxselllimit = 300;
            _sellforbiddennfruitslist = new Collection<int>();
            _sowmyseedsfirst = false;
            _stealunknowfruit = true;
            _stealforbiddenfruitslist = new Collection<int>();

            _harvestproduct = true;
            _harvestanimal = true;
            _addwater = true;
            _helpaddwater = false;
            _addgrass = true;
            _helpaddgrass = false;
            _buycalf = true;
            _buycalfbyprice = true;
            _buycalfcustom = 1;
            _stealproduct = false;
            _makeproduct = true;
            _helpmakeproduct = false;
            _breedanimal = false;
            _foodnum = 200;
            _presentproduct = false;
            _presentproductbyprice = true;
            _presentproductcheckvalue = true;
            _presentproductvalue = 100;
            _presentproductaid = 1;
            _presentproducttype = 0;
            _presentproductchecknum = true;
            _presentproductnum = 100;
            _sellproduct = false;
            _sellproductlowcash = true;
            _sellproductlowcashlimit = 100;
            _sellallproducts = false;
            _sellproductmaxlimit = 300;
            _sellproductforbiddenlist = new Collection<ProductInfo>();
            _addcarrot = true;
            _helpaddcarrot = false;
            _carrotnum = 200;
            _addbamboo = true;
            _helpaddbamboo = false;
            _bamboonum = 200;

            _shake = true;
            _treatfish = true;
            _updatefishpond = true;
            _bangkejing = true;
            _buyfish = true;
            _maxfishes = 20;
            _buyfishbyrank = true;
            _buyfishfishid = 1;
            _fishing = true;
            _buyupdatetackle = false;
            _maxtackles = 5;
            _harvestfish = true;
            _netselffish = false;
            _netselffishcheap = false;
            _netselffishmature = 80;
            _helpfish = true;
            _presentfish = false;
            _presentfishcheap = false;
            _presentfishcheckvalue = true;
            _presentfishvalue = 10000;
            _presentfishforbiddenlist = new Collection<int>();
            _sellfish = false;
            _sellfishlowcash = false;
            _sellfishlowcashlimit = 10;
            _sellallfish = false;
            _sellfishcheckvalue = false;
            _sellfishvalue = 10000;
            _sellfishmaxlimit = 20;
            _sellfishforbiddenlist = new Collection<int>();

            _sellasset = true;
            _buyasset = true;
            _buyassetcheap = false;
            _giveupifratio = true;
            _giveupratio = 50;
            _giveupifminimum = true;
            _giveupminimum = 5;
            _giveupifmyasset = false;
            _giveupassetcount = 3;
            _advancedpurchase = false;
            _buyassetslist = new Collection<int>();

            _boxclean = true;
            _cook = true;
            _cooktomatofirst = true;
            _cookmedlarfirst = false;
            _cookcrabfirst = false;
            _cookpineapplefirst = false;
            _cooklowcash = true;
            _cooklowcashlimit = 2000;
            _hire = true;
            _maxemployees = 12;
            _helpfriend = true;
            _presentfood = false;
            _presentforbiddenfoodlist = new Collection<int>();
            _presentfoodbycount = true;
            _presentfooddishid = 4;
            _presentfoodmessage = "送你食物啦！";
            _presentfoodratio = 50;
            _presentlowcash = true;
            _presentlowcashlimit = 2000;
            _presentfoodlowcount = true;
            _presentfoodlowcountlimit = 2;
            _purchasefood = false;
            _purchasefoodbyrefprice = true;
            _sellfood = false;
            _sellfoodbyrefprice = true;
        }

        [Browsable(false)]
        public string TaskId
        {
            get { return _taskid; }
            set { _taskid = value; }
        }

        [Browsable(false)]
        public string TaskName
        {
            get { return _taskname; }
            set { _taskname = value; }
        }

        [Category("帐号")]
        [DisplayName("组名称")]
        [Description("与此任务相对应的用户组")]
        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }

        [Category("帐号")]
        [DisplayName("需要执行的帐号")]
        [Description("需要执行此任务账号")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<AccountInfo> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }

        #region 执行方式

        [Category("执行方式")]
        [DisplayName("执行方式")]
        [Description("循环或定时")]
        [DefaultValue(EnumRunMode.MultiLoop)]
        public EnumRunMode RunMode
        {
            get { return _runmode; }
            set { _runmode = value; }
        }

        [Category("执行方式")]
        [DisplayName("循环间隔")]
        [Description("每次执行的间隔时间（单位：分钟）")]
        [DefaultValue(60)]
        public int RoundTime
        {
            get { return _roundtime; }
            set { _roundtime = value; }
        }

        [Category("执行方式")]
        [DisplayName("禁止在制定时间内运行")]
        [Description("禁止在制定时间内运行")]
        [DefaultValue(true)]
        public bool Forbidden
        {
            get { return _forbidden; }
            set { _forbidden = value; }
        }

        [Category("执行方式")]
        [DisplayName("禁止执行起始时间")]
        [Description("循环执行时，禁止在此时间段内执行。")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public TimeInfo ForbiddenStart
        {
            get { return _forbiddenstart; }
            set { _forbiddenstart = value; }
        }

        [Category("执行方式")]
        [DisplayName("禁止执行结束时间")]
        [Description("循环执行时，禁止在此时间段内执行。")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public TimeInfo ForbiddenEnd
        {
            get { return _forbiddenend; }
            set { _forbiddenend = value; }
        }

        [Category("执行方式")]
        [DisplayName("定时执行")]
        [Description("执行的时间点")]
        [DefaultValue(60)]
        public Collection<TimeInfo> StartTimes
        {
            get { return _starttimes; }
            set { _starttimes = value; }
        }
        #endregion

        #region 行为

        [Category("行为")]
        [DisplayName("执行停车")]
        [Description("是否执行停车操作")]
        [DefaultValue(true)]
        public bool ExecutePark
        {
            get { return _executepark; }
            set { _executepark = value; }
        }

        [Category("行为")]
        [DisplayName("执行咬人")]
        [Description("是否执行咬人操作")]
        [DefaultValue(true)]
        public bool ExecuteBite
        {
            get { return _executebite; }
            set { _executebite = value; }
        }

        [Category("行为")]
        [DisplayName("执行朋友买卖")]
        [Description("是否执行朋友买卖操作")]
        [DefaultValue(true)]
        public bool ExecuteSlave
        {
            get { return _executeslave; }
            set { _executeslave = value; }
        }

        [Category("行为")]
        [DisplayName("执行买房子")]
        [Description("是否执行买房子操作")]
        [DefaultValue(true)]
        public bool ExecuteHouse
        {
            get { return _executehouse; }
            set { _executehouse = value; }
        }

        [Category("行为")]
        [DisplayName("执行花园")]
        [Description("是否执行花园操作")]
        [DefaultValue(true)]
        public bool ExecuteGarden
        {
            get { return _executegarden; }
            set { _executegarden = value; }
        }

        [Category("行为")]
        [DisplayName("执行牧场")]
        [Description("是否执行牧场操作")]
        [DefaultValue(true)]
        public bool ExecuteRanch
        {
            get { return _executeranch; }
            set { _executeranch = value; }
        }

        [Category("行为")]
        [DisplayName("执行钓鱼")]
        [Description("是否执行钓鱼操作")]
        [DefaultValue(true)]
        public bool ExecuteFish
        {
            get { return _executefish; }
            set { _executefish = value; }
        }

        [Category("行为")]
        [DisplayName("执行超级大亨")]
        [Description("是否执行超级大亨操作")]
        [DefaultValue(true)]
        public bool ExecuteRich
        {
            get { return _executerich; }
            set { _executerich = value; }
        }

        [Category("行为")]
        [DisplayName("执行开心餐厅")]
        [Description("是否执行开心餐厅操作")]
        [DefaultValue(true)]
        public bool ExecuteCafe
        {
            get { return _executecafe; }
            set { _executecafe = value; }
        }

        [Category("行为")]
        [DisplayName("发送运行日志")]
        [Description("是否发送运行日志到指定邮箱")]
        [DefaultValue(false)]
        public bool SendLog
        {
            get { return _sendlog; }
            set { _sendlog = value; }
        }

        [Category("行为")]
        [DisplayName("接收者邮箱地址")]
        [Description("接收日志的邮箱地址")]
        public string ReceiverEmail
        {
            get { return _receiveremail; }
            set { _receiveremail = value; }
        }

        [Category("行为")]
        [DisplayName("输出日志到文件")]
        [Description("把任务的运行日志保存到文件中去")]
        [DefaultValue(false)]
        public bool WriteLogToFile
        {
            get { return _writelogtofile; }
            set { _writelogtofile = value; }
        }

        [Category("行为")]
        [DisplayName("忽略图片验证码")]
        [Description("忽略图片验证码，跳过当前账号")]
        [DefaultValue(false)]
        public bool SkipValidation
        {
            get { return _skipvalidation; }
            set { _skipvalidation = value; }
        }
        #endregion

        #region Park

        [Category("争车位")]
        [Description("把自己的车子停在别人的车位以赚取金钱")]
        [DisplayName("占车位")]
        [DefaultValue(true)]
        public bool ParkMyCars
        {
            get { return _parkmycars; }
            set { _parkmycars = value; }
        }

        [Category("争车位")]
        [Description("若贴条名单里的好友的车子停在我的车位，则对它进行贴条。收入归自己所有。")]
        [DisplayName("贴条")]
        [DefaultValue(false)]
        public bool PostOthersCars
        {
            get { return _postotherscars; }
            set { _postotherscars = value; }
        }

        [Category("争车位")]
        [Description("加入其他人发起的比赛")]
        [DisplayName("参加比赛")]
        [DefaultValue(false)]
        public bool JoinMatch
        {
            get { return _joinmatch; }
            set { _joinmatch = value; }
        }

        [Category("争车位")]
        [Description("比赛线路")]
        [DisplayName("比赛线路")]
        [DefaultValue(1)]
        public int OriginateMatchId 
        {
            get { return _originatematchid; }
            set { _originatematchid = value; }
        }

        [Category("争车位")]
        [Description("参赛车队数")]
        [DisplayName("参赛车队数")]
        [DefaultValue(2)]
        public int OriginateTeamNum
        {
            get { return _originateteamnum; }
            set { _originateteamnum = value; }
        }

        [Category("争车位")]
        [Description("若已组建自己的车队，发起比赛")]
        [DisplayName("发起比赛")]
        [DefaultValue(false)]
        public bool OriginateMatch
        {
            get { return _originatematch; }
            set { _originatematch = value; }
        }

        [Category("争车位")]
        [Description("当时间超过设定的时间点后或者已被加油达到10次时启动赛车")]
        [DisplayName("启动赛车")]
        [DefaultValue(false)]
        public bool StartCar
        {
            get { return _startcar; }
            set { _startcar = value; }
        }

        [Category("争车位")]
        [Description("给参加拉力赛的好友加油")]
        [DisplayName("加油")]
        [DefaultValue(true)]
        public bool CheerUp
        {
            get { return _cheerup; }
            set { _cheerup = value; }
        }

        [Category("争车位")]
        [Description("启动赛车的时间点")]
        [DisplayName("启动时间点")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public TimeInfo StartCarTime
        {
            get { return _startcartime; }
            set { _startcartime = value; }
        }

        #endregion

        #region Bite

        [Category("咬人")]
        [Description("同意别人在我家里休息")]
        [DisplayName("同意别人在我家里休息")]
        [DefaultValue(true)]
        public bool ApproveRecovery
        {
            get { return _approverecovery; }
            set { _approverecovery = value; }
        }

        [Category("咬人")]
        [Description("咬别人以提升自己等级")]
        [DisplayName("咬别人")]
        [DefaultValue(true)]
        public bool BiteOthers
        {
            get { return _biteothers; }
            set { _biteothers = value; }
        }

        [Category("咬人")]
        [Description("当没有体力时自动找房间休息")]
        [DisplayName("自动休息")]
        [DefaultValue(true)]
        public bool AutoRecover
        {
            get { return _autorecover; }
            set { _autorecover = value; }
        }

        [Category("咬人")]
        [Description("在自身有体力的情况下，保护名单中的好友")]
        [DisplayName("保护好友")]
        [DefaultValue(false)]
        public bool ProtectFriend
        {
            get { return _protectfriend; }
            set { _protectfriend = value; }
        }
        #endregion

        #region Slave

        [Category("朋友买卖")]
        [Description("购买[购买白名单]中的好友")]
        [DisplayName("购买名单中的奴隶")]
        [DefaultValue(true)]
        public bool BuySlave
        {
            get { return _buyslave; }
            set { _buyslave = value; }
        }

        [Category("朋友买卖")]
        [Description("购买低价奴隶")]
        [DisplayName("购买低价奴隶")]
        [DefaultValue(false)]
        public bool BuyLowPriceSlave
        {
            get { return _buylowpriceslave; }
            set { _buylowpriceslave = value; }
        }

        [Category("朋友买卖")]
        [Description("讨好主人")]
        [DisplayName("讨好主人")]
        [DefaultValue(true)]
        public bool FawnMaster
        {
            get { return _fawnmaster; }
            set { _fawnmaster = value; }
        }

        [Category("朋友买卖")]
        [Description("自动以最高级的方式安抚奴隶")]
        [DisplayName("安抚奴隶")]
        [DefaultValue(true)]
        public bool PropitiateSlave
        {
            get { return _propitiateslave; }
            set { _propitiateslave = value; }
        }

        [Category("朋友买卖")]
        [Description("整奴隶来给我赚钱，男的挖煤，女的去歌厅卖唱。")]
        [DisplayName("整奴隶")]
        [DefaultValue(true)]
        public bool AfflictSlave
        {
            get { return _afflictslave; }
            set { _afflictslave = value; }
        }

        [Category("朋友买卖")]
        [Description("释放超过2天的奴隶（只损失50元）")]
        [DisplayName("释放奴隶")]
        [DefaultValue(false)]
        public bool ReleaseSlave
        {
            get { return _releaseslave; }
            set { _releaseslave = value; }
        }

        [Category("朋友买卖")]
        [Description("最多允许几个奴隶")]
        [DisplayName("奴隶数上限")]
        [DefaultValue(9)]
        public int MaxSlaves
        {
            get { return _maxSlaves; }
            set { _maxSlaves = value; }
        }

        [Category("朋友买卖")]
        [Description("若设置了自动买奴隶，则用此昵称为其名字。")]
        [DisplayName("昵称")]
        [DefaultValue("阿桂，挖煤啦！")]
        public string NickName
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        #endregion

        #region House

        [Category("买房子")]
        [Description("打工赚钱")]
        [DisplayName("打工")]
        [DefaultValue(true)]
        public bool DoJob
        {
            get { return _dojob; }
            set { _dojob = value; }
        }

        [Category("买房子")]
        [Description("住进房子")]
        [DisplayName("住房子")]
        [DefaultValue(true)]
        public bool StayHouse
        {
            get { return _stayhouse; }
            set { _stayhouse = value; }
        }

        [Category("买房子")]
        [Description("抢名单中的好友")]
        [DisplayName("抢名单中的好友")]
        [DefaultValue(false)]
        public bool RobFriends
        {
            get { return _robfriends; }
            set { _robfriends = value; }
        }

        [Category("买房子")]
        [Description("抢露宿街头的好友")]
        [DisplayName("抢露宿街头的好友")]
        [DefaultValue(true)]
        public bool RobFreeFriends
        {
            get { return _robfreefriends; }
            set { _robfreefriends = value; }
        }

        [Category("买房子")]
        [Description("同时有两个好友住在我家里时，驱赶第一个，以使自己能住进来")]
        [DisplayName("允许驱赶好友")]
        [DefaultValue(false)]
        public bool DriveFriends
        {
            get { return _drivefriends; }
            set { _drivefriends = value; }
        }
        #endregion

        #region Garden

        [Category("花园")]
        [Description("在自己的菜园中进行浇水，捉虫，除草等操作")]
        [DisplayName("自家耕种")]
        [DefaultValue(true)]
        public bool FarmSelf
        {
            get { return _farmself; }
            set { _farmself = value; }
        }

        [Category("花园")]
        [Description("True:等级所允许买的最贵的; False:自定义")]
        [DisplayName("如何购买自家播种的种子")]
        [DefaultValue(true)]
        public bool ExpensiveFarmSelf
        {
            get { return _expensivefarmself; }
            set { _expensivefarmself = value; }
        }

        [Category("花园")]
        [Description("播种何种种子到自己的地块")]
        [DisplayName("自家播种的种子")]
        [DefaultValue(1)]
        public int CustomFarmSelf
        {
            get { return _customfarmself; }
            set { _customfarmself = value; }
        }

        [Category("花园")]
        [Description("去好友的爱心地块播种")]
        [DisplayName("播种爱心地块")]
        [DefaultValue(false)]
        public bool FarmShared
        {
            get { return _farmshared; }
            set { _farmshared = value; }
        }

        [Category("花园")]
        [Description("True:等级所允许买的最贵的; False:自定义")]
        [DisplayName("如何购买爱心地块播种的种子")]
        [DefaultValue(true)]
        public bool ExpensiveFarmShared
        {
            get { return _expensivefarmshared; }
            set { _expensivefarmshared = value; }
        }

        [Category("花园")]
        [Description("播种何种种子到爱心地块")]
        [DisplayName("爱心地块播种的种子")]
        [DefaultValue(1)]
        public int CustomFarmShared
        {
            get { return _customfarmshared; }
            set { _customfarmshared = value; }
        }

        [Category("花园")]
        [Description("收获自己菜园中已成熟的果实")]
        [DisplayName("收获果实")]
        [DefaultValue(true)]
        public bool HarvestFruit
        {
            get { return _harvestfruit; }
            set { _harvestfruit = value; }
        }

        [Category("花园")]
        [Description("当没有种子时，购买当前等级允许的最贵的种子")]
        [DisplayName("购买种子")]
        [DefaultValue(true)]
        public bool BuySeed
        {
            get { return _buyseed; }
            set { _buyseed = value; }
        }

        [Category("花园")]
        [Description("需要买的种子个数")]
        [DisplayName("个数")]
        [DefaultValue(1)]
        public int BuySeedCount
        {
            get { return _buyseedcount; }
            set { _buyseedcount = value; }
        }

        [Category("花园")]
        [Description("去好友的花园帮忙以赚取魅力")]
        [DisplayName("去好友的花园帮忙")]
        [DefaultValue(true)]
        public bool HelpOthers
        {
            get { return _helpothers; }
            set { _helpothers = value; }
        }

        [Category("花园")]
        [Description("偷别人菜园的果实")]
        [DisplayName("偷果实")]
        [DefaultValue(false)]
        public bool StealFruit
        {
            get { return _stealfruit; }
            set { _stealfruit = value; }
        }

        [Category("花园")]
        [Description("向指定的好友赠送果实")]
        [DisplayName("赠送果实")]
        [DefaultValue(false)]
        public bool PresentFruit
        {
            get { return _presentfruit; }
            set { _presentfruit = value; }
        }

        [Category("花园")]
        [Description("优先赠送总价值最高的果实")]
        [DisplayName("优先赠送总价值最高的果实")]
        [DefaultValue(true)]
        public bool PresentFruitByPrice
        {
            get { return _presentfruitbyprice; }
            set { _presentfruitbyprice = value; }
        }

        [Category("花园")]
        [Description("赠送果实时是否检查价值")]
        [DisplayName("赠送果实时是否检查价值")]
        [DefaultValue(true)]
        public bool PresentFruitCheckValue 
        {
            get { return _presentfruitcheckvalue; }
            set { _presentfruitcheckvalue = value; }
        }

        [Category("花园")]
        [Description("最低赠送的价值")]
        [DisplayName("最低赠送的价值")]
        [DefaultValue(100)]
        public int PresentFruitValue 
        {
            get { return _presentfruitvalue; }
            set { _presentfruitvalue = value; }
        }

        [Category("花园")]
        [Description("自定义赠送的果实")]
        [DisplayName("自定义赠送的果实")]
        [DefaultValue(11)]
        public int PresentFruitId
        {
            get { return _presentfruitid; }
            set { _presentfruitid = value; }
        }

        [Category("花园")]
        [Description("赠送果实时是否检查数量")]
        [DisplayName("赠送果实时是否检查数量")]
        [DefaultValue(true)]
        public bool PresentFruitCheckNum
        {
            get { return _presentfruitchecknum; }
            set { _presentfruitchecknum = value; }
        }

        [Category("花园")]
        [Description("赠送的果实的数量必须大于等于该值")]
        [DisplayName("赠送的果实的数量必须大于等于该值")]
        [DefaultValue(1000)]
        public int PresentFruitNum
        {
            get { return _presentfruitnum; }
            set { _presentfruitnum = value; }
        }

        [Category("花园")]
        [Description("出售仓库中的果实")]
        [DisplayName("出售果实")]
        [DefaultValue(false)]
        public bool SellFruit
        {
            get { return _sellfruit; }
            set { _sellfruit = value; }
        }

        [Category("花园")]
        [Description("只有当现金少于阀值时才出售")]
        [DisplayName("只有当现金少于阀值时才出售")]
        [DefaultValue(true)]
        public bool LowCash
        {
            get { return _lowcash; }
            set { _lowcash = value; }
        }

        [Category("花园")]
        [Description("出售果实的现金阀值（单位：万）")]
        [DisplayName("出售果实的现金阀值（单位：万）")]
        [DefaultValue(100)]
        public int LowCashLimit
        {
            get { return _lowcashlimit; }
            set { _lowcashlimit = value; }
        }

        [Category("花园")]
        [Description("出售所有果实")]
        [DisplayName("出售所有果实")]
        [DefaultValue(false)]
        public bool SellAllFruit
        {
            get { return _sellallfruit; }
            set { _sellallfruit = value; }
        }

        [Category("花园")]
        [Description("当出售的果实价值超过该值时，停止出售")]
        [DisplayName("出售的额度（单位：万）")]
        [DefaultValue(300)]
        public int MaxSellLimit
        {
            get { return _maxselllimit; }
            set { _maxselllimit = value; }
        }

        [Category("花园")]
        [Description("禁止出售的果实列表")]
        [DisplayName("禁止出售的果实列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> SellForbiddennFruitsList
        {
            get { return _sellforbiddennfruitslist; }
            set { _sellforbiddennfruitslist = value; }
        }

        [Category("花园")]
        [Description("优先播种已有的种子")]
        [DisplayName("优先播种已有的种子")]
        [DefaultValue(false)]
        public bool SowMySeedsFirst
        {
            get { return _sowmyseedsfirst; }
            set { _sowmyseedsfirst = value; }
        }

        [Category("花园")]
        [Description("偷未知果实（新出的种子，尚无法识别其名字，价格等参数，但可以偷窃。）")]
        [DisplayName("偷未知果实")]
        [DefaultValue(true)]
        public bool StealUnknowFruit
        {
            get { return _stealunknowfruit; }
            set { _stealunknowfruit = value; }
        }

        [Category("花园")]
        [Description("禁止偷在列表中的果实")]
        [DisplayName("禁止偷窃果实列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealForbiddenFruitsList
        {
            get { return _stealforbiddenfruitslist; }
            set { _stealforbiddenfruitslist = value; }
        }
        #endregion

        #region Ranch

        [Category("牧场")]
        [Description("收获鸡肉，猪肉，牛肉，羊肉等")]
        [DisplayName("收获动物")]
        [DefaultValue(true)]
        public bool HarvestAnimal
        {
            get { return _harvestanimal; }
            set { _harvestanimal = value; }
        }

        [Category("牧场")]
        [Description("收获鸡蛋，牛奶等")]
        [DisplayName("收获农副产品")]
        [DefaultValue(true)]
        public bool HarvestProduct
        {
            get { return _harvestproduct; }
            set { _harvestproduct = value; }
        }

        [Category("牧场")]
        [Description("给水缸添水")]
        [DisplayName("添水")]
        [DefaultValue(true)]
        public bool AddWater
        {
            get { return _addwater; }
            set { _addwater = value; }
        }

        [Category("牧场")]
        [Description("给好友的水缸添水")]
        [DisplayName("帮好友添水")]
        [DefaultValue(false)]
        public bool HelpAddWater
        {
            get { return _helpaddwater; }
            set { _helpaddwater = value; }
        }

        [Category("牧场")]
        [Description("给动物添牧草")]
        [DisplayName("添牧草")]
        [DefaultValue(true)]
        public bool AddGrass
        {
            get { return _addgrass; }
            set { _addgrass = value; }
        }

        [Category("牧场")]
        [Description("帮好友添牧草")]
        [DisplayName("帮好友添牧草")]
        [DefaultValue(false)]
        public bool HelpAddGrass
        {
            get { return _helpaddgrass; }
            set { _helpaddgrass = value; }
        }

        [Category("牧场")]
        [Description("购买幼崽")]
        [DisplayName("购买幼崽")]
        [DefaultValue(true)]
        public bool BuyCalf
        {
            get { return _buycalf; }
            set { _buycalf = value; }
        }

        [Category("牧场")]
        [Description("优先购买价格最高的幼崽")]
        [DisplayName("优先购买价格最高的幼崽")]
        [DefaultValue(true)]
        public bool BuyCalfByPrice
        {
            get { return _buycalfbyprice; }
            set { _buycalfbyprice = value; }
        }

        [Category("牧场")]
        [Description("购买指定幼崽")]
        [DisplayName("购买指定幼崽")]
        [DefaultValue(1)]
        public int BuyCalfCustom
        {
            get { return _buycalfcustom; }
            set { _buycalfcustom = value; }
        }

        [Category("牧场")]
        [Description("偷农副产品")]
        [DisplayName("偷农副产品")]
        [DefaultValue(false)]
        public bool StealProduct
        {
            get { return _stealproduct; }
            set { _stealproduct = value; }
        }

        [Category("牧场")]
        [Description("将动物赶去生产")]
        [DisplayName("生产")]
        [DefaultValue(true)]
        public bool MakeProduct
        {
            get { return _makeproduct; }
            set { _makeproduct = value; }
        }

        [Category("牧场")]
        [Description("去好友牧场帮忙将动物赶去生产")]
        [DisplayName("帮忙生产")]
        [DefaultValue(false)]
        public bool HelpMakeProduct
        {
            get { return _helpmakeproduct; }
            set { _helpmakeproduct = value; }
        }

        [Category("牧场")]
        [Description("配种")]
        [DisplayName("配种")]
        [DefaultValue(false)]
        public bool BreedAnimal
        {
            get { return _breedanimal; }
            set { _breedanimal = value; }
        }

        [Category("牧场")]
        [Description("每次添加牧草的数量")]
        [DisplayName("每次添加牧草的数量")]
        [DefaultValue(200)]
        public int FoodNum
        {
            get { return _foodnum; }
            set { _foodnum = value; }
        }

        [Category("牧场")]
        [Description("向指定的好友赠送农副产品")]
        [DisplayName("赠送农副产品")]
        [DefaultValue(false)]
        public bool PresentProduct
        {
            get { return _presentproduct; }
            set { _presentproduct = value; }
        }

        [Category("牧场")]
        [Description("按照价格由高到低赠送农副产品")]
        [DisplayName("按照价格由高到低赠送农副产品")]
        [DefaultValue(true)]
        public bool PresentProductByPrice
        {
            get { return _presentproductbyprice; }
            set { _presentproductbyprice = value; }
        }

        [Category("牧场")]
        [Description("赠送农副产品时是否检查价值")]
        [DisplayName("赠送农副产品时是否检查价值")]
        [DefaultValue(true)]
        public bool PresentProductCheckValue
        {
            get { return _presentproductcheckvalue; }
            set { _presentproductcheckvalue = value; }
        }

        [Category("牧场")]
        [Description("最低赠送价值")]
        [DisplayName("最低赠送价值")]
        [DefaultValue(100)]
        public int PresentProductValue
        {
            get { return _presentproductvalue; }
            set { _presentproductvalue = value; }
        }

        [Category("牧场")]
        [Description("赠送指定的农副产品")]
        [DisplayName("赠送指定的农副产品")]
        [DefaultValue(1)]
        public int PresentProductAid
        {
            get { return _presentproductaid; }
            set { _presentproductaid = value; }
        }

        [Category("牧场")]
        [Description("赠送指定的农副产品")]
        [DisplayName("赠送指定的农副产品")]
        [DefaultValue(0)]
        public int PresentProductType
        {
            get { return _presentproducttype; }
            set { _presentproducttype = value; }
        }

        [Category("牧场")]
        [Description("赠送农副产品时是否检查数量")]
        [DisplayName("赠送农副产品时是否检查数量")]
        [DefaultValue(true)]
        public bool PresentProductCheckNum
        {
            get { return _presentproductchecknum; }
            set { _presentproductchecknum = value; }
        }

        [Category("牧场")]
        [Description("最低赠送数量")]
        [DisplayName("最低赠送数量")]
        [DefaultValue(100)]
        public int PresentProductNum
        {
            get { return _presentproductnum; }
            set { _presentproductnum = value; }
        }

        [Category("牧场")]
        [Description("出售仓库中所有农副产品")]
        [DisplayName("出售农副产品")]
        [DefaultValue(false)]
        public bool SellProduct
        {
            get { return _sellproduct; }
            set { _sellproduct = value; }
        }

        [Category("牧场")]
        [Description("只有当现金少于该值时才出售")]
        [DisplayName("只有当现金少于该值时才出售")]
        [DefaultValue(true)]
        public bool SellProductLowCash
        {
            get { return _sellproductlowcash; }
            set { _sellproductlowcash = value; }
        }

        [Category("牧场")]
        [Description("出售农副产品的现金阀值（单位：万）")]
        [DisplayName("出售农副产品的现金阀值（单位：万）")]
        [DefaultValue(100)]
        public int SellProductLowCashLimit
        {
            get { return _sellproductlowcashlimit; }
            set { _sellproductlowcashlimit = value; }
        }

        [Category("牧场")]
        [Description("出售所有的农副产品")]
        [DisplayName("出售所有的农副产品")]
        [DefaultValue(false)]
        public bool SellAllProducts
        {
            get { return _sellallproducts; }
            set { _sellallproducts = value; }
        }

        [Category("牧场")]
        [Description("当出售的农副产品价值超过该值时，停止出售")]
        [DisplayName("出售的额度（单位：万）")]
        [DefaultValue(300)]
        public int SellProductMaxLimit
        {
            get { return _sellproductmaxlimit; }
            set { _sellproductmaxlimit = value; }
        }

        [Category("牧场")]
        [Description("禁止出售的农副产品列表")]
        [DisplayName("禁止出售的农副产品列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<ProductInfo> SellProductForbiddenList
        {
            get { return _sellproductforbiddenlist; }
            set { _sellproductforbiddenlist = value; }
        }

        [Category("牧场")]
        [Description("给动物添胡萝卜")]
        [DisplayName("添胡萝卜")]
        [DefaultValue(true)]
        public bool AddCarrot
        {
            get { return _addcarrot; }
            set { _addcarrot = value; }
        }

        [Category("牧场")]
        [Description("帮好友添胡萝卜")]
        [DisplayName("帮好友添胡萝卜")]
        [DefaultValue(false)]
        public bool HelpAddCarrot
        {
            get { return _helpaddcarrot; }
            set { _helpaddcarrot = value; }
        }

        [Category("牧场")]
        [Description("每次添加胡萝卜的数量")]
        [DisplayName("每次添加胡萝卜的数量")]
        [DefaultValue(200)]
        public int CarrotNum
        {
            get { return _carrotnum; }
            set { _carrotnum = value; }
        }

        [Category("牧场")]
        [Description("给动物添竹子")]
        [DisplayName("添竹子")]
        [DefaultValue(true)]
        public bool AddBamboo
        {
            get { return _addbamboo; }
            set { _addbamboo = value; }
        }

        [Category("牧场")]
        [Description("帮好友添竹子")]
        [DisplayName("帮好友添竹子")]
        [DefaultValue(false)]
        public bool HelpAddBamboo
        {
            get { return _helpaddbamboo; }
            set { _helpaddbamboo = value; }
        }

        [Category("牧场")]
        [Description("每次添加竹子的数量")]
        [DisplayName("每次添加竹子的数量")]
        [DefaultValue(200)]
        public int BambooNum
        {
            get { return _bamboonum; }
            set { _bamboonum = value; }
        }
        #endregion

        #region Fish

        [Category("钓鱼")]
        [Description("转盘抽奖")]
        [DisplayName("转盘")]
        [DefaultValue(true)]
        public bool Shake
        {
            get { return _shake; }
            set { _shake = value; }
        }

        [Category("钓鱼")]
        [Description("治病")]
        [DisplayName("治病")]
        [DefaultValue(true)]
        public bool TreatFish
        {
            get { return _treatfish; }
            set { _treatfish = value; }
        }

        [Category("钓鱼")]
        [Description("扩容鱼塘")]
        [DisplayName("扩容鱼塘")]
        [DefaultValue(true)]
        public bool UpdateFishPond
        {
            get { return _updatefishpond; }
            set { _updatefishpond = value; }
        }

        [Category("钓鱼")]
        [Description("给蚌壳精输真气")]
        [DisplayName("给蚌壳精输真气")]
        [DefaultValue(true)]
        public bool BangKeJing
        {
            get { return _bangkejing; }
            set { _bangkejing = value; }
        }
        
        [Category("钓鱼")]
        [Description("购买鱼苗")]
        [DisplayName("购买鱼苗")]
        [DefaultValue(true)]
        public bool BuyFish
        {
            get { return _buyfish; }
            set { _buyfish = value; }
        }

        [Category("钓鱼")]
        [Description("鱼数上限")]
        [DisplayName("鱼数上限")]
        [DefaultValue(20)]
        public int MaxFishes
        {
            get { return _maxfishes; }
            set { _maxfishes = value; }
        }
        
        [Category("钓鱼")]
        [Description("购买等级允许买的最贵的鱼苗")]
        [DisplayName("购买等级允许买的最贵的鱼苗")]
        [DefaultValue(true)]
        public bool BuyFishByRank
        {
            get { return _buyfishbyrank; }
            set { _buyfishbyrank = value; }
        }

        [Category("钓鱼")]
        [Description("购买指定的鱼苗")]
        [DisplayName("购买指定的鱼苗")]
        [DefaultValue(1)]
        public int BuyFishFishId
        {
            get { return _buyfishfishid; }
            set { _buyfishfishid = value; }
        }

        [Category("钓鱼")]
        [Description("去好友鱼塘钓鱼")]
        [DisplayName("钓鱼")]
        [DefaultValue(true)]
        public bool Fishing
        {
            get { return _fishing; }
            set { _fishing = value; }
        }

        [Category("钓鱼")]
        [Description("购买/升级鱼竿")]
        [DisplayName("购买/升级鱼竿")]
        [DefaultValue(false)]
        public bool BuyUpdateTackle
        {
            get { return _buyupdatetackle; }
            set { _buyupdatetackle = value; }
        }

        [Category("钓鱼")]
        [Description("鱼竿数上限")]
        [DisplayName("鱼竿数上限")]
        [DefaultValue(5)]
        public int MaxTackles
        {
            get { return _maxtackles; }
            set { _maxtackles = value; }
        }

        [Category("钓鱼")]
        [Description("拉杆收鱼")]
        [DisplayName("拉杆")]
        [DefaultValue(true)]
        public bool HarvestFish
        {
            get { return _harvestfish; }
            set { _harvestfish = value; }
        }

        [Category("钓鱼")]
        [Description("自家收鱼")]
        [DisplayName("自家收鱼")]
        [DefaultValue(false)]
        public bool NetSelfFish
        {
            get { return _netselffish; }
            set { _netselffish = value; }
        }

        [Category("钓鱼")]
        [Description("收鱼顺序，false为按照价格由高到底收鱼。")]
        [DisplayName("收鱼顺序")]
        [DefaultValue(false)]
        public bool NetSelfFishCheap
        {
            get { return _netselffishcheap; }
            set { _netselffishcheap = value; }
        }

        [Category("钓鱼")]
        [Description("只收成熟度大于此值的鱼")]
        [DisplayName("只收成熟度大于此值的鱼")]
        [DefaultValue(80)]
        public int NetSelfFishMature
        {
            get { return _netselffishmature; }
            set { _netselffishmature = value; }
        }
        
        [Category("钓鱼")]
        [Description("帮忙收鱼")]
        [DisplayName("帮忙收鱼")]
        [DefaultValue(true)]
        public bool HelpFish
        {
            get { return _helpfish; }
            set { _helpfish = value; }
        }
        
        [Category("钓鱼")]
        [Description("赠送鱼给指定的好友")]
        [DisplayName("赠送鱼")]
        [DefaultValue(false)]
        public bool PresentFish
        {
            get { return _presentfish; }
            set { _presentfish = value; }
        }

        [Category("钓鱼")]
        [Description("优先赠送最便宜的鱼。若为true，则优先赠送最贵的")]
        [DisplayName("优先赠送最便宜的鱼")]
        [DefaultValue(false)]
        public bool PresentFishCheap
        {
            get { return _presentfishcheap; }
            set { _presentfishcheap = value; }
        }

        [Category("钓鱼")]
        [Description("赠送鱼时是否检查鱼的价值")]
        [DisplayName("赠送鱼时是否检查鱼的价值")]
        [DefaultValue(true)]
        public bool PresentFishCheckValue
        {
            get { return _presentfishcheckvalue; }
            set { _presentfishcheckvalue = value; }
        }

        [Category("钓鱼")]
        [Description("赠送的鱼的价值必须大于等于该值")]
        [DisplayName("赠送的鱼的价值必须大于等于该值")]
        [DefaultValue(10000)]
        public int PresentFishValue
        {
            get { return _presentfishvalue; }
            set { _presentfishvalue = value; }
        }

        [Category("钓鱼")]
        [Description("禁止赠送的鱼列表")]
        [DisplayName("禁止赠送的鱼列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PresentFishForbiddenList
        {
            get { return _presentfishforbiddenlist; }
            set { _presentfishforbiddenlist = value; }
        }
        
        [Category("钓鱼")]
        [Description("出售仓库中的鱼")]
        [DisplayName("出售鱼")]
        [DefaultValue(false)]
        public bool SellFish
        {
            get { return _sellfish; }
            set { _sellfish = value; }
        }

        [Category("钓鱼")]
        [Description("只有当现金少于该值时才出售")]
        [DisplayName("只有当现金少于该值时才出售")]
        [DefaultValue(false)]
        public bool SellFishLowCash
        {
            get { return _sellfishlowcash; }
            set { _sellfishlowcash = value; }
        }

        [Category("钓鱼")]
        [Description("出售鱼的现金阀值（单位：万）")]
        [DisplayName("出售鱼的现金阀值（单位：万）")]
        [DefaultValue(10)]
        public int SellFishLowCashLimit
        {
            get { return _sellfishlowcashlimit; }
            set { _sellfishlowcashlimit = value; }
        }

        [Category("钓鱼")]
        [Description("出售所有的鱼")]
        [DisplayName("出售所有的鱼")]
        [DefaultValue(false)]
        public bool SellAllFish
        {
            get { return _sellallfish; }
            set { _sellallfish = value; }
        }

        [Category("钓鱼")]
        [Description("出售鱼时是否检查鱼的价值")]
        [DisplayName("出售鱼时是否检查鱼的价值")]
        [DefaultValue(false)]
        public bool SellFishCheckValue
        {
            get { return _sellfishcheckvalue; ; }
            set { _sellfishcheckvalue = value; }
        }

        [Category("钓鱼")]
        [Description("出售的鱼的价值必须小于该值")]
        [DisplayName("出售的鱼的价值必须小于该值")]
        [DefaultValue(10000)]
        public int SellFishValue
        {
            get { return _sellfishvalue; }
            set { _sellfishvalue = value; }
        }

        [Category("钓鱼")]
        [Description("当出售的鱼价值超过该值时，停止出售")]
        [DisplayName("出售的额度（单位：万）")]
        [DefaultValue(20)]
        public int SellFishMaxLimit
        {
            get { return _sellfishmaxlimit; }
            set { _sellfishmaxlimit = value; }
        }

        [Category("钓鱼")]
        [Description("禁止出售的鱼列表")]
        [DisplayName("禁止出售的鱼列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> SellFishForbiddenList
        {
            get { return _sellfishforbiddenlist; }
            set { _sellfishforbiddenlist = value; }
        }

        #endregion

        #region Rich

        [Category("超级大亨")]
        [Description("出售资产")]
        [DisplayName("出售资产")]
        [DefaultValue(true)]
        public bool SellAsset
        {
            get { return _sellasset; }
            set { _sellasset = value; }
        }

        [Category("超级大亨")]
        [Description("购买资产")]
        [DisplayName("购买资产")]
        [DefaultValue(true)]
        public bool BuyAsset
        {
            get { return _buyasset; }
            set { _buyasset = value; }
        }

        [Category("超级大亨")]
        [Description("购买顺序为：价格由低到高")]
        [DisplayName("购买顺序为：价格由低到高")]
        [DefaultValue(false)]
        public bool BuyAssetCheap
        {
            get { return _buyassetcheap; }
            set { _buyassetcheap = value; }
        }

        [Category("超级大亨")]
        [Description("现金/总资产比低于设定值时停止购买")]
        [DisplayName("现金/总资产比低于设定值时停止购买")]
        [DefaultValue(true)]
        public bool GiveUpIfRatio
        {
            get { return _giveupifratio; }
            set { _giveupifratio = value; }
        }

        [Category("超级大亨")]
        [Description("现金/总资产比")]
        [DisplayName("现金/总资产比")]
        [DefaultValue(50)]
        public int GiveUpRatio
        {
            get { return _giveupratio; }
            set { _giveupratio = value; }
        }

        [Category("超级大亨")]
        [Description("连续购买时（第2次及以后），购买数低于设定值时停止购买")]
        [DisplayName("购买数低于设定值时停止购买")]
        [DefaultValue(true)]
        public bool GiveUpIfMinimum
        {
            get { return _giveupifminimum; }
            set { _giveupifminimum = value; }
        }

        [Category("超级大亨")]
        [Description("连续购买时（第2次及以后），最小购买数")]
        [DisplayName("连续最小购买数")]
        [DefaultValue(5)]
        public int GiveUpMinimum
        {
            get { return _giveupminimum; }
            set { _giveupminimum = value; }
        }

        [Category("超级大亨")]
        [Description("拥有的资产项超过设定值时停止购买")]
        [DisplayName("拥有的资产项超过设定值时停止购买")]
        [DefaultValue(false)]
        public bool GiveUpIfMyAsset
        {
            get { return _giveupifmyasset; }
            set { _giveupifmyasset = value; }
        }

        [Category("超级大亨")]
        [Description("资产项目数")]
        [DisplayName("资产项目数")]
        [DefaultValue(3)]
        public int GiveUpAssetCount
        {
            get { return _giveupassetcount; }
            set { _giveupassetcount = value; }
        }

        [Category("超级大亨")]
        [Description("高级购买数量控制")]
        [DisplayName("高级购买数量控制")]
        [DefaultValue(false)]
        public bool AdvancedPurchase
        {
            get { return _advancedpurchase; }
            set { _advancedpurchase = value; }
        }

        [Category("超级大亨")]
        [Description("资产购买列表")]
        [DisplayName("资产购买列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BuyAssetsList
        {
            get { return _buyassetslist; }
            set { _buyassetslist = value; }
        }
        #endregion

        #region Cafe

        [Category("开心餐厅")]
        [Description("装盘和清理灶台")]
        [DisplayName("装盘和清理灶台")]
        [DefaultValue(true)]
        public bool BoxClean
        {
            get { return _boxclean; }
            set { _boxclean = value; }
        }

        [Category("开心餐厅")]
        [Description("炒菜")]
        [DisplayName("炒菜")]
        [DefaultValue(true)]
        public bool Cook
        {
            get { return _cook; }
            set { _cook = value; }
        }

        [Category("开心餐厅")]
        [Description("优先兑换番茄炒蛋")]
        [DisplayName("优先兑换番茄炒蛋")]
        [DefaultValue(true)]
        public bool CookTomatoFirst
        {
            get { return _cooktomatofirst; }
            set { _cooktomatofirst = value; }
        }

        [Category("开心餐厅")]
        [Description("优先兑换枸杞银耳羹")]
        [DisplayName("优先兑换枸杞银耳羹")]
        [DefaultValue(false)]
        public bool CookMedlarFirst
        {
            get { return _cookmedlarfirst; }
            set { _cookmedlarfirst = value; }
        }

        [Category("开心餐厅")]
        [Description("优先兑换清蒸大闸蟹")]
        [DisplayName("优先兑换清蒸大闸蟹")]
        [DefaultValue(false)]
        public bool CookCrabFirst
        {
            get { return _cookcrabfirst; }
            set { _cookcrabfirst = value; }
        }

        [Category("开心餐厅")]
        [Description("优先兑换菠萝古老肉")]
        [DisplayName("优先兑换菠萝古老肉")]
        [DefaultValue(false)]
        public bool CookPineappleFirst
        {
            get { return _cookpineapplefirst; }
            set { _cookpineapplefirst = value; }
        }
        
        [Category("开心餐厅")]
        [Description("炒哪种菜")]
        [DisplayName("炒哪种菜")]
        [DefaultValue(4)]
        public int CookDishId
        {
            get { return _cookdishid; }
            set { _cookdishid = value; }
        }

        [Category("开心餐厅")]
        [Description("现金低于指定值时不炒菜")]
        [DisplayName("现金低于指定值时不炒菜")]
        [DefaultValue(true)]
        public bool CookLowCash
        {
            get { return _cooklowcash; }
            set { _cooklowcash = value; }
        }

        [Category("开心餐厅")]
        [Description("低于该值时不炒菜")]
        [DisplayName("最低现金阀值")]
        [DefaultValue(2000)]
        public long CookLowCashLimit
        {
            get { return _cooklowcashlimit; }
            set { _cooklowcashlimit = value; }
        }

        [Category("开心餐厅")]
        [Description("雇员")]
        [DisplayName("雇员")]
        [DefaultValue(true)]
        public bool Hire
        {
            get { return _hire; }
            set { _hire = value; }
        }

        [Category("开心餐厅")]
        [Description("最大员工数")]
        [DisplayName("最大员工数")]
        [DefaultValue(12)]
        public int MaxEmployees
        {
            get { return _maxemployees; }
            set { _maxemployees = value; }
        }        
        
        [Category("开心餐厅")]
        [Description("帮忙好友")]
        [DisplayName("帮忙好友")]
        [DefaultValue(true)]
        public bool HelpFriend
        {
            get { return _helpfriend; }
            set { _helpfriend = value; }
        }

        [Category("开心餐厅")]
        [Description("赠送食物")]
        [DisplayName("赠送食物")]
        [DefaultValue(false)]
        public bool PresentFood
        {
            get { return _presentfood; }
            set { _presentfood = value; }
        }

        [Category("开心餐厅")]
        [Description("禁止赠送的食物列表")]
        [DisplayName("禁止赠送的食物列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PresentForbiddenFoodList
        {
            get { return _presentforbiddenfoodlist; }
            set { _presentforbiddenfoodlist = value; }
        }

        [Category("开心餐厅")]
        [Description("优先赠送总价值最高的食物")]
        [DisplayName("优先赠送总价值最高的食物")]
        [DefaultValue(true)]
        public bool PresentFoodByCount
        {
            get { return _presentfoodbycount; }
            set { _presentfoodbycount = value; }
        }

        [Category("开心餐厅")]
        [Description("自定义赠送的食物")]
        [DisplayName("自定义赠送的食物")]
        [DefaultValue(4)]
        public int PresentFoodDishId
        {
            get { return _presentfooddishid; }
            set { _presentfooddishid = value; }
        }

        [Category("开心餐厅")]
        [Description("赠送食物时发送给好友的信息")]
        [DisplayName("赠送食物时发送给好友的信息")]
        [DefaultValue("送你食物啦！")]
        public string PresentFoodMessage
        {
            get { return _presentfoodmessage; }
            set { _presentfoodmessage = value; }
        }

        [Category("开心餐厅")]
        [Description("赠送比率")]
        [DisplayName("赠送比率")]
        [DefaultValue(50)]
        public int PresentFoodRatio
        {
            get { return _presentfoodratio; }
            set { _presentfoodratio = value; }
        }

        [Category("开心餐厅")]
        [Description("现金低于指定值时不赠送")]
        [DisplayName("现金低于指定值时不赠送")]
        [DefaultValue(true)]
        public bool PresentLowCash
        {
            get { return _presentlowcash; }
            set { _presentlowcash = value; }
        }

        [Category("开心餐厅")]
        [Description("低于该值时不赠送")]
        [DisplayName("最低现金阀值")]
        [DefaultValue(2000)]
        public long PresentLowCashLimit
        {
            get { return _presentlowcashlimit; }
            set { _presentlowcashlimit = value; }
        }

        [Category("开心餐厅")]
        [Description("餐台上的食物种类数低于指定值时不赠送")]
        [DisplayName("餐台上的食物种类数低于指定值时不赠送")]
        [DefaultValue(true)]
        public bool PresentFoodLowCount
        {
            get { return _presentfoodlowcount; }
            set { _presentfoodlowcount = value; }
        }

        [Category("开心餐厅")]
        [Description("最低餐台上的食物种类数")]
        [DisplayName("最低餐台上的食物种类数")]
        [DefaultValue(2)]
        public int PresentFoodLowCountLimit
        {
            get { return _presentfoodlowcountlimit; }
            set { _presentfoodlowcountlimit = value; }
        }

        [Category("开心餐厅")]
        [Description("收购食物")]
        [DisplayName("收购食物")]
        [DefaultValue(false)]
        public bool PurchaseFood
        {
            get { return _purchasefood; }
            set { _purchasefood = value; }
        }

        [Category("开心餐厅")]
        [Description("根据交易表中的购入价进行交易")]
        [DisplayName("根据交易表中的购入价进行交易")]
        [DefaultValue(true)]
        public bool PurchaseFoodByRefPrice
        {
            get { return _purchasefoodbyrefprice; }
            set { _purchasefoodbyrefprice = value; }
        }

        [Category("开心餐厅")]
        [Description("出售食物")]
        [DisplayName("出售食物")]
        [DefaultValue(false)]
        public bool SellFood
        {
            get { return _sellfood; }
            set { _sellfood = value; }
        }

        [Category("开心餐厅")]
        [Description("根据交易表中的出售价进行交易")]
        [DisplayName("根据交易表中的出售价进行交易")]
        [DefaultValue(true)]
        public bool SellFoodByRefPrice
        {
            get { return _sellfoodbyrefprice; }
            set { _sellfoodbyrefprice = value; }
        }
        #endregion

        #region override
        public override string ToString()
        {
            if (_taskname != null && _taskname != string.Empty)
                return _taskname;
            else
                return base.ToString();
        }
        #endregion
    }
}
