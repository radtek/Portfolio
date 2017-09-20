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
        private EnumRunMode _runmode; //true ѭ��; false ��ʱ
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
        private bool _buyassetcheap; //true:�ȹ�����˵ģ�false:�ȹ�����
        private bool _giveupifratio;    //�ֽ�/���ʲ����ʵ����趨ֵʱֹͣ����
        private int _giveupratio;       //�ֽ�/���ʲ���
        private bool _giveupifminimum;  //��������ʱ����2�μ��Ժ󣩣������������趨ֵʱֹͣ����
        private int _giveupminimum;     //������С������
        private bool _giveupifmyasset;  //ӵ�е��ʲ�����趨ֵʱֹͣ����
        private int _giveupassetcount;  //�ʲ���Ŀ��        
        private bool _advancedpurchase;  //�߼�������������
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
            _nickname = "������ú����";

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
            _presentfoodmessage = "����ʳ������";
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

        [Category("�ʺ�")]
        [DisplayName("������")]
        [Description("����������Ӧ���û���")]
        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }

        [Category("�ʺ�")]
        [DisplayName("��Ҫִ�е��ʺ�")]
        [Description("��Ҫִ�д������˺�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<AccountInfo> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }

        #region ִ�з�ʽ

        [Category("ִ�з�ʽ")]
        [DisplayName("ִ�з�ʽ")]
        [Description("ѭ����ʱ")]
        [DefaultValue(EnumRunMode.MultiLoop)]
        public EnumRunMode RunMode
        {
            get { return _runmode; }
            set { _runmode = value; }
        }

        [Category("ִ�з�ʽ")]
        [DisplayName("ѭ�����")]
        [Description("ÿ��ִ�еļ��ʱ�䣨��λ�����ӣ�")]
        [DefaultValue(60)]
        public int RoundTime
        {
            get { return _roundtime; }
            set { _roundtime = value; }
        }

        [Category("ִ�з�ʽ")]
        [DisplayName("��ֹ���ƶ�ʱ��������")]
        [Description("��ֹ���ƶ�ʱ��������")]
        [DefaultValue(true)]
        public bool Forbidden
        {
            get { return _forbidden; }
            set { _forbidden = value; }
        }

        [Category("ִ�з�ʽ")]
        [DisplayName("��ִֹ����ʼʱ��")]
        [Description("ѭ��ִ��ʱ����ֹ�ڴ�ʱ�����ִ�С�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public TimeInfo ForbiddenStart
        {
            get { return _forbiddenstart; }
            set { _forbiddenstart = value; }
        }

        [Category("ִ�з�ʽ")]
        [DisplayName("��ִֹ�н���ʱ��")]
        [Description("ѭ��ִ��ʱ����ֹ�ڴ�ʱ�����ִ�С�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public TimeInfo ForbiddenEnd
        {
            get { return _forbiddenend; }
            set { _forbiddenend = value; }
        }

        [Category("ִ�з�ʽ")]
        [DisplayName("��ʱִ��")]
        [Description("ִ�е�ʱ���")]
        [DefaultValue(60)]
        public Collection<TimeInfo> StartTimes
        {
            get { return _starttimes; }
            set { _starttimes = value; }
        }
        #endregion

        #region ��Ϊ

        [Category("��Ϊ")]
        [DisplayName("ִ��ͣ��")]
        [Description("�Ƿ�ִ��ͣ������")]
        [DefaultValue(true)]
        public bool ExecutePark
        {
            get { return _executepark; }
            set { _executepark = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ��ҧ��")]
        [Description("�Ƿ�ִ��ҧ�˲���")]
        [DefaultValue(true)]
        public bool ExecuteBite
        {
            get { return _executebite; }
            set { _executebite = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ����������")]
        [Description("�Ƿ�ִ��������������")]
        [DefaultValue(true)]
        public bool ExecuteSlave
        {
            get { return _executeslave; }
            set { _executeslave = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ������")]
        [Description("�Ƿ�ִ�����Ӳ���")]
        [DefaultValue(true)]
        public bool ExecuteHouse
        {
            get { return _executehouse; }
            set { _executehouse = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ�л�԰")]
        [Description("�Ƿ�ִ�л�԰����")]
        [DefaultValue(true)]
        public bool ExecuteGarden
        {
            get { return _executegarden; }
            set { _executegarden = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ������")]
        [Description("�Ƿ�ִ����������")]
        [DefaultValue(true)]
        public bool ExecuteRanch
        {
            get { return _executeranch; }
            set { _executeranch = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ�е���")]
        [Description("�Ƿ�ִ�е������")]
        [DefaultValue(true)]
        public bool ExecuteFish
        {
            get { return _executefish; }
            set { _executefish = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ�г������")]
        [Description("�Ƿ�ִ�г���������")]
        [DefaultValue(true)]
        public bool ExecuteRich
        {
            get { return _executerich; }
            set { _executerich = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("ִ�п��Ĳ���")]
        [Description("�Ƿ�ִ�п��Ĳ�������")]
        [DefaultValue(true)]
        public bool ExecuteCafe
        {
            get { return _executecafe; }
            set { _executecafe = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("����������־")]
        [Description("�Ƿ���������־��ָ������")]
        [DefaultValue(false)]
        public bool SendLog
        {
            get { return _sendlog; }
            set { _sendlog = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("�����������ַ")]
        [Description("������־�������ַ")]
        public string ReceiverEmail
        {
            get { return _receiveremail; }
            set { _receiveremail = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("�����־���ļ�")]
        [Description("�������������־���浽�ļ���ȥ")]
        [DefaultValue(false)]
        public bool WriteLogToFile
        {
            get { return _writelogtofile; }
            set { _writelogtofile = value; }
        }

        [Category("��Ϊ")]
        [DisplayName("����ͼƬ��֤��")]
        [Description("����ͼƬ��֤�룬������ǰ�˺�")]
        [DefaultValue(false)]
        public bool SkipValidation
        {
            get { return _skipvalidation; }
            set { _skipvalidation = value; }
        }
        #endregion

        #region Park

        [Category("����λ")]
        [Description("���Լ��ĳ���ͣ�ڱ��˵ĳ�λ��׬ȡ��Ǯ")]
        [DisplayName("ռ��λ")]
        [DefaultValue(true)]
        public bool ParkMyCars
        {
            get { return _parkmycars; }
            set { _parkmycars = value; }
        }

        [Category("����λ")]
        [Description("������������ĺ��ѵĳ���ͣ���ҵĳ�λ�����������������������Լ����С�")]
        [DisplayName("����")]
        [DefaultValue(false)]
        public bool PostOthersCars
        {
            get { return _postotherscars; }
            set { _postotherscars = value; }
        }

        [Category("����λ")]
        [Description("���������˷���ı���")]
        [DisplayName("�μӱ���")]
        [DefaultValue(false)]
        public bool JoinMatch
        {
            get { return _joinmatch; }
            set { _joinmatch = value; }
        }

        [Category("����λ")]
        [Description("������·")]
        [DisplayName("������·")]
        [DefaultValue(1)]
        public int OriginateMatchId 
        {
            get { return _originatematchid; }
            set { _originatematchid = value; }
        }

        [Category("����λ")]
        [Description("����������")]
        [DisplayName("����������")]
        [DefaultValue(2)]
        public int OriginateTeamNum
        {
            get { return _originateteamnum; }
            set { _originateteamnum = value; }
        }

        [Category("����λ")]
        [Description("�����齨�Լ��ĳ��ӣ��������")]
        [DisplayName("�������")]
        [DefaultValue(false)]
        public bool OriginateMatch
        {
            get { return _originatematch; }
            set { _originatematch = value; }
        }

        [Category("����λ")]
        [Description("��ʱ�䳬���趨��ʱ��������ѱ����ʹﵽ10��ʱ��������")]
        [DisplayName("��������")]
        [DefaultValue(false)]
        public bool StartCar
        {
            get { return _startcar; }
            set { _startcar = value; }
        }

        [Category("����λ")]
        [Description("���μ��������ĺ��Ѽ���")]
        [DisplayName("����")]
        [DefaultValue(true)]
        public bool CheerUp
        {
            get { return _cheerup; }
            set { _cheerup = value; }
        }

        [Category("����λ")]
        [Description("����������ʱ���")]
        [DisplayName("����ʱ���")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.Drawing.Design.UITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public TimeInfo StartCarTime
        {
            get { return _startcartime; }
            set { _startcartime = value; }
        }

        #endregion

        #region Bite

        [Category("ҧ��")]
        [Description("ͬ��������Ҽ�����Ϣ")]
        [DisplayName("ͬ��������Ҽ�����Ϣ")]
        [DefaultValue(true)]
        public bool ApproveRecovery
        {
            get { return _approverecovery; }
            set { _approverecovery = value; }
        }

        [Category("ҧ��")]
        [Description("ҧ�����������Լ��ȼ�")]
        [DisplayName("ҧ����")]
        [DefaultValue(true)]
        public bool BiteOthers
        {
            get { return _biteothers; }
            set { _biteothers = value; }
        }

        [Category("ҧ��")]
        [Description("��û������ʱ�Զ��ҷ�����Ϣ")]
        [DisplayName("�Զ���Ϣ")]
        [DefaultValue(true)]
        public bool AutoRecover
        {
            get { return _autorecover; }
            set { _autorecover = value; }
        }

        [Category("ҧ��")]
        [Description("������������������£����������еĺ���")]
        [DisplayName("��������")]
        [DefaultValue(false)]
        public bool ProtectFriend
        {
            get { return _protectfriend; }
            set { _protectfriend = value; }
        }
        #endregion

        #region Slave

        [Category("��������")]
        [Description("����[���������]�еĺ���")]
        [DisplayName("���������е�ū��")]
        [DefaultValue(true)]
        public bool BuySlave
        {
            get { return _buyslave; }
            set { _buyslave = value; }
        }

        [Category("��������")]
        [Description("����ͼ�ū��")]
        [DisplayName("����ͼ�ū��")]
        [DefaultValue(false)]
        public bool BuyLowPriceSlave
        {
            get { return _buylowpriceslave; }
            set { _buylowpriceslave = value; }
        }

        [Category("��������")]
        [Description("�ֺ�����")]
        [DisplayName("�ֺ�����")]
        [DefaultValue(true)]
        public bool FawnMaster
        {
            get { return _fawnmaster; }
            set { _fawnmaster = value; }
        }

        [Category("��������")]
        [Description("�Զ�����߼��ķ�ʽ����ū��")]
        [DisplayName("����ū��")]
        [DefaultValue(true)]
        public bool PropitiateSlave
        {
            get { return _propitiateslave; }
            set { _propitiateslave = value; }
        }

        [Category("��������")]
        [Description("��ū��������׬Ǯ���е���ú��Ů��ȥ����������")]
        [DisplayName("��ū��")]
        [DefaultValue(true)]
        public bool AfflictSlave
        {
            get { return _afflictslave; }
            set { _afflictslave = value; }
        }

        [Category("��������")]
        [Description("�ͷų���2���ū����ֻ��ʧ50Ԫ��")]
        [DisplayName("�ͷ�ū��")]
        [DefaultValue(false)]
        public bool ReleaseSlave
        {
            get { return _releaseslave; }
            set { _releaseslave = value; }
        }

        [Category("��������")]
        [Description("���������ū��")]
        [DisplayName("ū��������")]
        [DefaultValue(9)]
        public int MaxSlaves
        {
            get { return _maxSlaves; }
            set { _maxSlaves = value; }
        }

        [Category("��������")]
        [Description("���������Զ���ū�������ô��ǳ�Ϊ�����֡�")]
        [DisplayName("�ǳ�")]
        [DefaultValue("������ú����")]
        public string NickName
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        #endregion

        #region House

        [Category("����")]
        [Description("��׬Ǯ")]
        [DisplayName("��")]
        [DefaultValue(true)]
        public bool DoJob
        {
            get { return _dojob; }
            set { _dojob = value; }
        }

        [Category("����")]
        [Description("ס������")]
        [DisplayName("ס����")]
        [DefaultValue(true)]
        public bool StayHouse
        {
            get { return _stayhouse; }
            set { _stayhouse = value; }
        }

        [Category("����")]
        [Description("�������еĺ���")]
        [DisplayName("�������еĺ���")]
        [DefaultValue(false)]
        public bool RobFriends
        {
            get { return _robfriends; }
            set { _robfriends = value; }
        }

        [Category("����")]
        [Description("��¶�޽�ͷ�ĺ���")]
        [DisplayName("��¶�޽�ͷ�ĺ���")]
        [DefaultValue(true)]
        public bool RobFreeFriends
        {
            get { return _robfreefriends; }
            set { _robfreefriends = value; }
        }

        [Category("����")]
        [Description("ͬʱ����������ס���Ҽ���ʱ�����ϵ�һ������ʹ�Լ���ס����")]
        [DisplayName("�������Ϻ���")]
        [DefaultValue(false)]
        public bool DriveFriends
        {
            get { return _drivefriends; }
            set { _drivefriends = value; }
        }
        #endregion

        #region Garden

        [Category("��԰")]
        [Description("���Լ��Ĳ�԰�н��н�ˮ��׽�棬���ݵȲ���")]
        [DisplayName("�ԼҸ���")]
        [DefaultValue(true)]
        public bool FarmSelf
        {
            get { return _farmself; }
            set { _farmself = value; }
        }

        [Category("��԰")]
        [Description("True:�ȼ��������������; False:�Զ���")]
        [DisplayName("��ι����ԼҲ��ֵ�����")]
        [DefaultValue(true)]
        public bool ExpensiveFarmSelf
        {
            get { return _expensivefarmself; }
            set { _expensivefarmself = value; }
        }

        [Category("��԰")]
        [Description("���ֺ������ӵ��Լ��ĵؿ�")]
        [DisplayName("�ԼҲ��ֵ�����")]
        [DefaultValue(1)]
        public int CustomFarmSelf
        {
            get { return _customfarmself; }
            set { _customfarmself = value; }
        }

        [Category("��԰")]
        [Description("ȥ���ѵİ��ĵؿ鲥��")]
        [DisplayName("���ְ��ĵؿ�")]
        [DefaultValue(false)]
        public bool FarmShared
        {
            get { return _farmshared; }
            set { _farmshared = value; }
        }

        [Category("��԰")]
        [Description("True:�ȼ��������������; False:�Զ���")]
        [DisplayName("��ι����ĵؿ鲥�ֵ�����")]
        [DefaultValue(true)]
        public bool ExpensiveFarmShared
        {
            get { return _expensivefarmshared; }
            set { _expensivefarmshared = value; }
        }

        [Category("��԰")]
        [Description("���ֺ������ӵ����ĵؿ�")]
        [DisplayName("���ĵؿ鲥�ֵ�����")]
        [DefaultValue(1)]
        public int CustomFarmShared
        {
            get { return _customfarmshared; }
            set { _customfarmshared = value; }
        }

        [Category("��԰")]
        [Description("�ջ��Լ���԰���ѳ���Ĺ�ʵ")]
        [DisplayName("�ջ��ʵ")]
        [DefaultValue(true)]
        public bool HarvestFruit
        {
            get { return _harvestfruit; }
            set { _harvestfruit = value; }
        }

        [Category("��԰")]
        [Description("��û������ʱ������ǰ�ȼ��������������")]
        [DisplayName("��������")]
        [DefaultValue(true)]
        public bool BuySeed
        {
            get { return _buyseed; }
            set { _buyseed = value; }
        }

        [Category("��԰")]
        [Description("��Ҫ������Ӹ���")]
        [DisplayName("����")]
        [DefaultValue(1)]
        public int BuySeedCount
        {
            get { return _buyseedcount; }
            set { _buyseedcount = value; }
        }

        [Category("��԰")]
        [Description("ȥ���ѵĻ�԰��æ��׬ȡ����")]
        [DisplayName("ȥ���ѵĻ�԰��æ")]
        [DefaultValue(true)]
        public bool HelpOthers
        {
            get { return _helpothers; }
            set { _helpothers = value; }
        }

        [Category("��԰")]
        [Description("͵���˲�԰�Ĺ�ʵ")]
        [DisplayName("͵��ʵ")]
        [DefaultValue(false)]
        public bool StealFruit
        {
            get { return _stealfruit; }
            set { _stealfruit = value; }
        }

        [Category("��԰")]
        [Description("��ָ���ĺ������͹�ʵ")]
        [DisplayName("���͹�ʵ")]
        [DefaultValue(false)]
        public bool PresentFruit
        {
            get { return _presentfruit; }
            set { _presentfruit = value; }
        }

        [Category("��԰")]
        [Description("���������ܼ�ֵ��ߵĹ�ʵ")]
        [DisplayName("���������ܼ�ֵ��ߵĹ�ʵ")]
        [DefaultValue(true)]
        public bool PresentFruitByPrice
        {
            get { return _presentfruitbyprice; }
            set { _presentfruitbyprice = value; }
        }

        [Category("��԰")]
        [Description("���͹�ʵʱ�Ƿ����ֵ")]
        [DisplayName("���͹�ʵʱ�Ƿ����ֵ")]
        [DefaultValue(true)]
        public bool PresentFruitCheckValue 
        {
            get { return _presentfruitcheckvalue; }
            set { _presentfruitcheckvalue = value; }
        }

        [Category("��԰")]
        [Description("������͵ļ�ֵ")]
        [DisplayName("������͵ļ�ֵ")]
        [DefaultValue(100)]
        public int PresentFruitValue 
        {
            get { return _presentfruitvalue; }
            set { _presentfruitvalue = value; }
        }

        [Category("��԰")]
        [Description("�Զ������͵Ĺ�ʵ")]
        [DisplayName("�Զ������͵Ĺ�ʵ")]
        [DefaultValue(11)]
        public int PresentFruitId
        {
            get { return _presentfruitid; }
            set { _presentfruitid = value; }
        }

        [Category("��԰")]
        [Description("���͹�ʵʱ�Ƿ�������")]
        [DisplayName("���͹�ʵʱ�Ƿ�������")]
        [DefaultValue(true)]
        public bool PresentFruitCheckNum
        {
            get { return _presentfruitchecknum; }
            set { _presentfruitchecknum = value; }
        }

        [Category("��԰")]
        [Description("���͵Ĺ�ʵ������������ڵ��ڸ�ֵ")]
        [DisplayName("���͵Ĺ�ʵ������������ڵ��ڸ�ֵ")]
        [DefaultValue(1000)]
        public int PresentFruitNum
        {
            get { return _presentfruitnum; }
            set { _presentfruitnum = value; }
        }

        [Category("��԰")]
        [Description("���۲ֿ��еĹ�ʵ")]
        [DisplayName("���۹�ʵ")]
        [DefaultValue(false)]
        public bool SellFruit
        {
            get { return _sellfruit; }
            set { _sellfruit = value; }
        }

        [Category("��԰")]
        [Description("ֻ�е��ֽ����ڷ�ֵʱ�ų���")]
        [DisplayName("ֻ�е��ֽ����ڷ�ֵʱ�ų���")]
        [DefaultValue(true)]
        public bool LowCash
        {
            get { return _lowcash; }
            set { _lowcash = value; }
        }

        [Category("��԰")]
        [Description("���۹�ʵ���ֽ�ֵ����λ����")]
        [DisplayName("���۹�ʵ���ֽ�ֵ����λ����")]
        [DefaultValue(100)]
        public int LowCashLimit
        {
            get { return _lowcashlimit; }
            set { _lowcashlimit = value; }
        }

        [Category("��԰")]
        [Description("�������й�ʵ")]
        [DisplayName("�������й�ʵ")]
        [DefaultValue(false)]
        public bool SellAllFruit
        {
            get { return _sellallfruit; }
            set { _sellallfruit = value; }
        }

        [Category("��԰")]
        [Description("�����۵Ĺ�ʵ��ֵ������ֵʱ��ֹͣ����")]
        [DisplayName("���۵Ķ�ȣ���λ����")]
        [DefaultValue(300)]
        public int MaxSellLimit
        {
            get { return _maxselllimit; }
            set { _maxselllimit = value; }
        }

        [Category("��԰")]
        [Description("��ֹ���۵Ĺ�ʵ�б�")]
        [DisplayName("��ֹ���۵Ĺ�ʵ�б�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> SellForbiddennFruitsList
        {
            get { return _sellforbiddennfruitslist; }
            set { _sellforbiddennfruitslist = value; }
        }

        [Category("��԰")]
        [Description("���Ȳ������е�����")]
        [DisplayName("���Ȳ������е�����")]
        [DefaultValue(false)]
        public bool SowMySeedsFirst
        {
            get { return _sowmyseedsfirst; }
            set { _sowmyseedsfirst = value; }
        }

        [Category("��԰")]
        [Description("͵δ֪��ʵ���³������ӣ����޷�ʶ�������֣��۸�Ȳ�����������͵�ԡ���")]
        [DisplayName("͵δ֪��ʵ")]
        [DefaultValue(true)]
        public bool StealUnknowFruit
        {
            get { return _stealunknowfruit; }
            set { _stealunknowfruit = value; }
        }

        [Category("��԰")]
        [Description("��ֹ͵���б��еĹ�ʵ")]
        [DisplayName("��ֹ͵�Թ�ʵ�б�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealForbiddenFruitsList
        {
            get { return _stealforbiddenfruitslist; }
            set { _stealforbiddenfruitslist = value; }
        }
        #endregion

        #region Ranch

        [Category("����")]
        [Description("�ջ��⣬���⣬ţ�⣬�����")]
        [DisplayName("�ջ���")]
        [DefaultValue(true)]
        public bool HarvestAnimal
        {
            get { return _harvestanimal; }
            set { _harvestanimal = value; }
        }

        [Category("����")]
        [Description("�ջ񼦵���ţ�̵�")]
        [DisplayName("�ջ�ũ����Ʒ")]
        [DefaultValue(true)]
        public bool HarvestProduct
        {
            get { return _harvestproduct; }
            set { _harvestproduct = value; }
        }

        [Category("����")]
        [Description("��ˮ����ˮ")]
        [DisplayName("��ˮ")]
        [DefaultValue(true)]
        public bool AddWater
        {
            get { return _addwater; }
            set { _addwater = value; }
        }

        [Category("����")]
        [Description("�����ѵ�ˮ����ˮ")]
        [DisplayName("�������ˮ")]
        [DefaultValue(false)]
        public bool HelpAddWater
        {
            get { return _helpaddwater; }
            set { _helpaddwater = value; }
        }

        [Category("����")]
        [Description("������������")]
        [DisplayName("������")]
        [DefaultValue(true)]
        public bool AddGrass
        {
            get { return _addgrass; }
            set { _addgrass = value; }
        }

        [Category("����")]
        [Description("�����������")]
        [DisplayName("�����������")]
        [DefaultValue(false)]
        public bool HelpAddGrass
        {
            get { return _helpaddgrass; }
            set { _helpaddgrass = value; }
        }

        [Category("����")]
        [Description("��������")]
        [DisplayName("��������")]
        [DefaultValue(true)]
        public bool BuyCalf
        {
            get { return _buycalf; }
            set { _buycalf = value; }
        }

        [Category("����")]
        [Description("���ȹ���۸���ߵ�����")]
        [DisplayName("���ȹ���۸���ߵ�����")]
        [DefaultValue(true)]
        public bool BuyCalfByPrice
        {
            get { return _buycalfbyprice; }
            set { _buycalfbyprice = value; }
        }

        [Category("����")]
        [Description("����ָ������")]
        [DisplayName("����ָ������")]
        [DefaultValue(1)]
        public int BuyCalfCustom
        {
            get { return _buycalfcustom; }
            set { _buycalfcustom = value; }
        }

        [Category("����")]
        [Description("͵ũ����Ʒ")]
        [DisplayName("͵ũ����Ʒ")]
        [DefaultValue(false)]
        public bool StealProduct
        {
            get { return _stealproduct; }
            set { _stealproduct = value; }
        }

        [Category("����")]
        [Description("�������ȥ����")]
        [DisplayName("����")]
        [DefaultValue(true)]
        public bool MakeProduct
        {
            get { return _makeproduct; }
            set { _makeproduct = value; }
        }

        [Category("����")]
        [Description("ȥ����������æ�������ȥ����")]
        [DisplayName("��æ����")]
        [DefaultValue(false)]
        public bool HelpMakeProduct
        {
            get { return _helpmakeproduct; }
            set { _helpmakeproduct = value; }
        }

        [Category("����")]
        [Description("����")]
        [DisplayName("����")]
        [DefaultValue(false)]
        public bool BreedAnimal
        {
            get { return _breedanimal; }
            set { _breedanimal = value; }
        }

        [Category("����")]
        [Description("ÿ��������ݵ�����")]
        [DisplayName("ÿ��������ݵ�����")]
        [DefaultValue(200)]
        public int FoodNum
        {
            get { return _foodnum; }
            set { _foodnum = value; }
        }

        [Category("����")]
        [Description("��ָ���ĺ�������ũ����Ʒ")]
        [DisplayName("����ũ����Ʒ")]
        [DefaultValue(false)]
        public bool PresentProduct
        {
            get { return _presentproduct; }
            set { _presentproduct = value; }
        }

        [Category("����")]
        [Description("���ռ۸��ɸߵ�������ũ����Ʒ")]
        [DisplayName("���ռ۸��ɸߵ�������ũ����Ʒ")]
        [DefaultValue(true)]
        public bool PresentProductByPrice
        {
            get { return _presentproductbyprice; }
            set { _presentproductbyprice = value; }
        }

        [Category("����")]
        [Description("����ũ����Ʒʱ�Ƿ����ֵ")]
        [DisplayName("����ũ����Ʒʱ�Ƿ����ֵ")]
        [DefaultValue(true)]
        public bool PresentProductCheckValue
        {
            get { return _presentproductcheckvalue; }
            set { _presentproductcheckvalue = value; }
        }

        [Category("����")]
        [Description("������ͼ�ֵ")]
        [DisplayName("������ͼ�ֵ")]
        [DefaultValue(100)]
        public int PresentProductValue
        {
            get { return _presentproductvalue; }
            set { _presentproductvalue = value; }
        }

        [Category("����")]
        [Description("����ָ����ũ����Ʒ")]
        [DisplayName("����ָ����ũ����Ʒ")]
        [DefaultValue(1)]
        public int PresentProductAid
        {
            get { return _presentproductaid; }
            set { _presentproductaid = value; }
        }

        [Category("����")]
        [Description("����ָ����ũ����Ʒ")]
        [DisplayName("����ָ����ũ����Ʒ")]
        [DefaultValue(0)]
        public int PresentProductType
        {
            get { return _presentproducttype; }
            set { _presentproducttype = value; }
        }

        [Category("����")]
        [Description("����ũ����Ʒʱ�Ƿ�������")]
        [DisplayName("����ũ����Ʒʱ�Ƿ�������")]
        [DefaultValue(true)]
        public bool PresentProductCheckNum
        {
            get { return _presentproductchecknum; }
            set { _presentproductchecknum = value; }
        }

        [Category("����")]
        [Description("�����������")]
        [DisplayName("�����������")]
        [DefaultValue(100)]
        public int PresentProductNum
        {
            get { return _presentproductnum; }
            set { _presentproductnum = value; }
        }

        [Category("����")]
        [Description("���۲ֿ�������ũ����Ʒ")]
        [DisplayName("����ũ����Ʒ")]
        [DefaultValue(false)]
        public bool SellProduct
        {
            get { return _sellproduct; }
            set { _sellproduct = value; }
        }

        [Category("����")]
        [Description("ֻ�е��ֽ����ڸ�ֵʱ�ų���")]
        [DisplayName("ֻ�е��ֽ����ڸ�ֵʱ�ų���")]
        [DefaultValue(true)]
        public bool SellProductLowCash
        {
            get { return _sellproductlowcash; }
            set { _sellproductlowcash = value; }
        }

        [Category("����")]
        [Description("����ũ����Ʒ���ֽ�ֵ����λ����")]
        [DisplayName("����ũ����Ʒ���ֽ�ֵ����λ����")]
        [DefaultValue(100)]
        public int SellProductLowCashLimit
        {
            get { return _sellproductlowcashlimit; }
            set { _sellproductlowcashlimit = value; }
        }

        [Category("����")]
        [Description("�������е�ũ����Ʒ")]
        [DisplayName("�������е�ũ����Ʒ")]
        [DefaultValue(false)]
        public bool SellAllProducts
        {
            get { return _sellallproducts; }
            set { _sellallproducts = value; }
        }

        [Category("����")]
        [Description("�����۵�ũ����Ʒ��ֵ������ֵʱ��ֹͣ����")]
        [DisplayName("���۵Ķ�ȣ���λ����")]
        [DefaultValue(300)]
        public int SellProductMaxLimit
        {
            get { return _sellproductmaxlimit; }
            set { _sellproductmaxlimit = value; }
        }

        [Category("����")]
        [Description("��ֹ���۵�ũ����Ʒ�б�")]
        [DisplayName("��ֹ���۵�ũ����Ʒ�б�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<ProductInfo> SellProductForbiddenList
        {
            get { return _sellproductforbiddenlist; }
            set { _sellproductforbiddenlist = value; }
        }

        [Category("����")]
        [Description("����������ܲ�")]
        [DisplayName("����ܲ�")]
        [DefaultValue(true)]
        public bool AddCarrot
        {
            get { return _addcarrot; }
            set { _addcarrot = value; }
        }

        [Category("����")]
        [Description("���������ܲ�")]
        [DisplayName("���������ܲ�")]
        [DefaultValue(false)]
        public bool HelpAddCarrot
        {
            get { return _helpaddcarrot; }
            set { _helpaddcarrot = value; }
        }

        [Category("����")]
        [Description("ÿ����Ӻ��ܲ�������")]
        [DisplayName("ÿ����Ӻ��ܲ�������")]
        [DefaultValue(200)]
        public int CarrotNum
        {
            get { return _carrotnum; }
            set { _carrotnum = value; }
        }

        [Category("����")]
        [Description("������������")]
        [DisplayName("������")]
        [DefaultValue(true)]
        public bool AddBamboo
        {
            get { return _addbamboo; }
            set { _addbamboo = value; }
        }

        [Category("����")]
        [Description("�����������")]
        [DisplayName("�����������")]
        [DefaultValue(false)]
        public bool HelpAddBamboo
        {
            get { return _helpaddbamboo; }
            set { _helpaddbamboo = value; }
        }

        [Category("����")]
        [Description("ÿ��������ӵ�����")]
        [DisplayName("ÿ��������ӵ�����")]
        [DefaultValue(200)]
        public int BambooNum
        {
            get { return _bamboonum; }
            set { _bamboonum = value; }
        }
        #endregion

        #region Fish

        [Category("����")]
        [Description("ת�̳齱")]
        [DisplayName("ת��")]
        [DefaultValue(true)]
        public bool Shake
        {
            get { return _shake; }
            set { _shake = value; }
        }

        [Category("����")]
        [Description("�β�")]
        [DisplayName("�β�")]
        [DefaultValue(true)]
        public bool TreatFish
        {
            get { return _treatfish; }
            set { _treatfish = value; }
        }

        [Category("����")]
        [Description("��������")]
        [DisplayName("��������")]
        [DefaultValue(true)]
        public bool UpdateFishPond
        {
            get { return _updatefishpond; }
            set { _updatefishpond = value; }
        }

        [Category("����")]
        [Description("�����Ǿ�������")]
        [DisplayName("�����Ǿ�������")]
        [DefaultValue(true)]
        public bool BangKeJing
        {
            get { return _bangkejing; }
            set { _bangkejing = value; }
        }
        
        [Category("����")]
        [Description("��������")]
        [DisplayName("��������")]
        [DefaultValue(true)]
        public bool BuyFish
        {
            get { return _buyfish; }
            set { _buyfish = value; }
        }

        [Category("����")]
        [Description("��������")]
        [DisplayName("��������")]
        [DefaultValue(20)]
        public int MaxFishes
        {
            get { return _maxfishes; }
            set { _maxfishes = value; }
        }
        
        [Category("����")]
        [Description("����ȼ����������������")]
        [DisplayName("����ȼ����������������")]
        [DefaultValue(true)]
        public bool BuyFishByRank
        {
            get { return _buyfishbyrank; }
            set { _buyfishbyrank = value; }
        }

        [Category("����")]
        [Description("����ָ��������")]
        [DisplayName("����ָ��������")]
        [DefaultValue(1)]
        public int BuyFishFishId
        {
            get { return _buyfishfishid; }
            set { _buyfishfishid = value; }
        }

        [Category("����")]
        [Description("ȥ������������")]
        [DisplayName("����")]
        [DefaultValue(true)]
        public bool Fishing
        {
            get { return _fishing; }
            set { _fishing = value; }
        }

        [Category("����")]
        [Description("����/�������")]
        [DisplayName("����/�������")]
        [DefaultValue(false)]
        public bool BuyUpdateTackle
        {
            get { return _buyupdatetackle; }
            set { _buyupdatetackle = value; }
        }

        [Category("����")]
        [Description("���������")]
        [DisplayName("���������")]
        [DefaultValue(5)]
        public int MaxTackles
        {
            get { return _maxtackles; }
            set { _maxtackles = value; }
        }

        [Category("����")]
        [Description("��������")]
        [DisplayName("����")]
        [DefaultValue(true)]
        public bool HarvestFish
        {
            get { return _harvestfish; }
            set { _harvestfish = value; }
        }

        [Category("����")]
        [Description("�Լ�����")]
        [DisplayName("�Լ�����")]
        [DefaultValue(false)]
        public bool NetSelfFish
        {
            get { return _netselffish; }
            set { _netselffish = value; }
        }

        [Category("����")]
        [Description("����˳��falseΪ���ռ۸��ɸߵ������㡣")]
        [DisplayName("����˳��")]
        [DefaultValue(false)]
        public bool NetSelfFishCheap
        {
            get { return _netselffishcheap; }
            set { _netselffishcheap = value; }
        }

        [Category("����")]
        [Description("ֻ�ճ���ȴ��ڴ�ֵ����")]
        [DisplayName("ֻ�ճ���ȴ��ڴ�ֵ����")]
        [DefaultValue(80)]
        public int NetSelfFishMature
        {
            get { return _netselffishmature; }
            set { _netselffishmature = value; }
        }
        
        [Category("����")]
        [Description("��æ����")]
        [DisplayName("��æ����")]
        [DefaultValue(true)]
        public bool HelpFish
        {
            get { return _helpfish; }
            set { _helpfish = value; }
        }
        
        [Category("����")]
        [Description("�������ָ���ĺ���")]
        [DisplayName("������")]
        [DefaultValue(false)]
        public bool PresentFish
        {
            get { return _presentfish; }
            set { _presentfish = value; }
        }

        [Category("����")]
        [Description("������������˵��㡣��Ϊtrue����������������")]
        [DisplayName("������������˵���")]
        [DefaultValue(false)]
        public bool PresentFishCheap
        {
            get { return _presentfishcheap; }
            set { _presentfishcheap = value; }
        }

        [Category("����")]
        [Description("������ʱ�Ƿ�����ļ�ֵ")]
        [DisplayName("������ʱ�Ƿ�����ļ�ֵ")]
        [DefaultValue(true)]
        public bool PresentFishCheckValue
        {
            get { return _presentfishcheckvalue; }
            set { _presentfishcheckvalue = value; }
        }

        [Category("����")]
        [Description("���͵���ļ�ֵ������ڵ��ڸ�ֵ")]
        [DisplayName("���͵���ļ�ֵ������ڵ��ڸ�ֵ")]
        [DefaultValue(10000)]
        public int PresentFishValue
        {
            get { return _presentfishvalue; }
            set { _presentfishvalue = value; }
        }

        [Category("����")]
        [Description("��ֹ���͵����б�")]
        [DisplayName("��ֹ���͵����б�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PresentFishForbiddenList
        {
            get { return _presentfishforbiddenlist; }
            set { _presentfishforbiddenlist = value; }
        }
        
        [Category("����")]
        [Description("���۲ֿ��е���")]
        [DisplayName("������")]
        [DefaultValue(false)]
        public bool SellFish
        {
            get { return _sellfish; }
            set { _sellfish = value; }
        }

        [Category("����")]
        [Description("ֻ�е��ֽ����ڸ�ֵʱ�ų���")]
        [DisplayName("ֻ�е��ֽ����ڸ�ֵʱ�ų���")]
        [DefaultValue(false)]
        public bool SellFishLowCash
        {
            get { return _sellfishlowcash; }
            set { _sellfishlowcash = value; }
        }

        [Category("����")]
        [Description("��������ֽ�ֵ����λ����")]
        [DisplayName("��������ֽ�ֵ����λ����")]
        [DefaultValue(10)]
        public int SellFishLowCashLimit
        {
            get { return _sellfishlowcashlimit; }
            set { _sellfishlowcashlimit = value; }
        }

        [Category("����")]
        [Description("�������е���")]
        [DisplayName("�������е���")]
        [DefaultValue(false)]
        public bool SellAllFish
        {
            get { return _sellallfish; }
            set { _sellallfish = value; }
        }

        [Category("����")]
        [Description("������ʱ�Ƿ�����ļ�ֵ")]
        [DisplayName("������ʱ�Ƿ�����ļ�ֵ")]
        [DefaultValue(false)]
        public bool SellFishCheckValue
        {
            get { return _sellfishcheckvalue; ; }
            set { _sellfishcheckvalue = value; }
        }

        [Category("����")]
        [Description("���۵���ļ�ֵ����С�ڸ�ֵ")]
        [DisplayName("���۵���ļ�ֵ����С�ڸ�ֵ")]
        [DefaultValue(10000)]
        public int SellFishValue
        {
            get { return _sellfishvalue; }
            set { _sellfishvalue = value; }
        }

        [Category("����")]
        [Description("�����۵����ֵ������ֵʱ��ֹͣ����")]
        [DisplayName("���۵Ķ�ȣ���λ����")]
        [DefaultValue(20)]
        public int SellFishMaxLimit
        {
            get { return _sellfishmaxlimit; }
            set { _sellfishmaxlimit = value; }
        }

        [Category("����")]
        [Description("��ֹ���۵����б�")]
        [DisplayName("��ֹ���۵����б�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> SellFishForbiddenList
        {
            get { return _sellfishforbiddenlist; }
            set { _sellfishforbiddenlist = value; }
        }

        #endregion

        #region Rich

        [Category("�������")]
        [Description("�����ʲ�")]
        [DisplayName("�����ʲ�")]
        [DefaultValue(true)]
        public bool SellAsset
        {
            get { return _sellasset; }
            set { _sellasset = value; }
        }

        [Category("�������")]
        [Description("�����ʲ�")]
        [DisplayName("�����ʲ�")]
        [DefaultValue(true)]
        public bool BuyAsset
        {
            get { return _buyasset; }
            set { _buyasset = value; }
        }

        [Category("�������")]
        [Description("����˳��Ϊ���۸��ɵ͵���")]
        [DisplayName("����˳��Ϊ���۸��ɵ͵���")]
        [DefaultValue(false)]
        public bool BuyAssetCheap
        {
            get { return _buyassetcheap; }
            set { _buyassetcheap = value; }
        }

        [Category("�������")]
        [Description("�ֽ�/���ʲ��ȵ����趨ֵʱֹͣ����")]
        [DisplayName("�ֽ�/���ʲ��ȵ����趨ֵʱֹͣ����")]
        [DefaultValue(true)]
        public bool GiveUpIfRatio
        {
            get { return _giveupifratio; }
            set { _giveupifratio = value; }
        }

        [Category("�������")]
        [Description("�ֽ�/���ʲ���")]
        [DisplayName("�ֽ�/���ʲ���")]
        [DefaultValue(50)]
        public int GiveUpRatio
        {
            get { return _giveupratio; }
            set { _giveupratio = value; }
        }

        [Category("�������")]
        [Description("��������ʱ����2�μ��Ժ󣩣������������趨ֵʱֹͣ����")]
        [DisplayName("�����������趨ֵʱֹͣ����")]
        [DefaultValue(true)]
        public bool GiveUpIfMinimum
        {
            get { return _giveupifminimum; }
            set { _giveupifminimum = value; }
        }

        [Category("�������")]
        [Description("��������ʱ����2�μ��Ժ󣩣���С������")]
        [DisplayName("������С������")]
        [DefaultValue(5)]
        public int GiveUpMinimum
        {
            get { return _giveupminimum; }
            set { _giveupminimum = value; }
        }

        [Category("�������")]
        [Description("ӵ�е��ʲ�����趨ֵʱֹͣ����")]
        [DisplayName("ӵ�е��ʲ�����趨ֵʱֹͣ����")]
        [DefaultValue(false)]
        public bool GiveUpIfMyAsset
        {
            get { return _giveupifmyasset; }
            set { _giveupifmyasset = value; }
        }

        [Category("�������")]
        [Description("�ʲ���Ŀ��")]
        [DisplayName("�ʲ���Ŀ��")]
        [DefaultValue(3)]
        public int GiveUpAssetCount
        {
            get { return _giveupassetcount; }
            set { _giveupassetcount = value; }
        }

        [Category("�������")]
        [Description("�߼�������������")]
        [DisplayName("�߼�������������")]
        [DefaultValue(false)]
        public bool AdvancedPurchase
        {
            get { return _advancedpurchase; }
            set { _advancedpurchase = value; }
        }

        [Category("�������")]
        [Description("�ʲ������б�")]
        [DisplayName("�ʲ������б�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BuyAssetsList
        {
            get { return _buyassetslist; }
            set { _buyassetslist = value; }
        }
        #endregion

        #region Cafe

        [Category("���Ĳ���")]
        [Description("װ�̺�������̨")]
        [DisplayName("װ�̺�������̨")]
        [DefaultValue(true)]
        public bool BoxClean
        {
            get { return _boxclean; }
            set { _boxclean = value; }
        }

        [Category("���Ĳ���")]
        [Description("����")]
        [DisplayName("����")]
        [DefaultValue(true)]
        public bool Cook
        {
            get { return _cook; }
            set { _cook = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ȶһ����ѳ���")]
        [DisplayName("���ȶһ����ѳ���")]
        [DefaultValue(true)]
        public bool CookTomatoFirst
        {
            get { return _cooktomatofirst; }
            set { _cooktomatofirst = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ȶһ����������")]
        [DisplayName("���ȶһ����������")]
        [DefaultValue(false)]
        public bool CookMedlarFirst
        {
            get { return _cookmedlarfirst; }
            set { _cookmedlarfirst = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ȶһ�������բз")]
        [DisplayName("���ȶһ�������բз")]
        [DefaultValue(false)]
        public bool CookCrabFirst
        {
            get { return _cookcrabfirst; }
            set { _cookcrabfirst = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ȶһ����ܹ�����")]
        [DisplayName("���ȶһ����ܹ�����")]
        [DefaultValue(false)]
        public bool CookPineappleFirst
        {
            get { return _cookpineapplefirst; }
            set { _cookpineapplefirst = value; }
        }
        
        [Category("���Ĳ���")]
        [Description("�����ֲ�")]
        [DisplayName("�����ֲ�")]
        [DefaultValue(4)]
        public int CookDishId
        {
            get { return _cookdishid; }
            set { _cookdishid = value; }
        }

        [Category("���Ĳ���")]
        [Description("�ֽ����ָ��ֵʱ������")]
        [DisplayName("�ֽ����ָ��ֵʱ������")]
        [DefaultValue(true)]
        public bool CookLowCash
        {
            get { return _cooklowcash; }
            set { _cooklowcash = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ڸ�ֵʱ������")]
        [DisplayName("����ֽ�ֵ")]
        [DefaultValue(2000)]
        public long CookLowCashLimit
        {
            get { return _cooklowcashlimit; }
            set { _cooklowcashlimit = value; }
        }

        [Category("���Ĳ���")]
        [Description("��Ա")]
        [DisplayName("��Ա")]
        [DefaultValue(true)]
        public bool Hire
        {
            get { return _hire; }
            set { _hire = value; }
        }

        [Category("���Ĳ���")]
        [Description("���Ա����")]
        [DisplayName("���Ա����")]
        [DefaultValue(12)]
        public int MaxEmployees
        {
            get { return _maxemployees; }
            set { _maxemployees = value; }
        }        
        
        [Category("���Ĳ���")]
        [Description("��æ����")]
        [DisplayName("��æ����")]
        [DefaultValue(true)]
        public bool HelpFriend
        {
            get { return _helpfriend; }
            set { _helpfriend = value; }
        }

        [Category("���Ĳ���")]
        [Description("����ʳ��")]
        [DisplayName("����ʳ��")]
        [DefaultValue(false)]
        public bool PresentFood
        {
            get { return _presentfood; }
            set { _presentfood = value; }
        }

        [Category("���Ĳ���")]
        [Description("��ֹ���͵�ʳ���б�")]
        [DisplayName("��ֹ���͵�ʳ���б�")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PresentForbiddenFoodList
        {
            get { return _presentforbiddenfoodlist; }
            set { _presentforbiddenfoodlist = value; }
        }

        [Category("���Ĳ���")]
        [Description("���������ܼ�ֵ��ߵ�ʳ��")]
        [DisplayName("���������ܼ�ֵ��ߵ�ʳ��")]
        [DefaultValue(true)]
        public bool PresentFoodByCount
        {
            get { return _presentfoodbycount; }
            set { _presentfoodbycount = value; }
        }

        [Category("���Ĳ���")]
        [Description("�Զ������͵�ʳ��")]
        [DisplayName("�Զ������͵�ʳ��")]
        [DefaultValue(4)]
        public int PresentFoodDishId
        {
            get { return _presentfooddishid; }
            set { _presentfooddishid = value; }
        }

        [Category("���Ĳ���")]
        [Description("����ʳ��ʱ���͸����ѵ���Ϣ")]
        [DisplayName("����ʳ��ʱ���͸����ѵ���Ϣ")]
        [DefaultValue("����ʳ������")]
        public string PresentFoodMessage
        {
            get { return _presentfoodmessage; }
            set { _presentfoodmessage = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ͱ���")]
        [DisplayName("���ͱ���")]
        [DefaultValue(50)]
        public int PresentFoodRatio
        {
            get { return _presentfoodratio; }
            set { _presentfoodratio = value; }
        }

        [Category("���Ĳ���")]
        [Description("�ֽ����ָ��ֵʱ������")]
        [DisplayName("�ֽ����ָ��ֵʱ������")]
        [DefaultValue(true)]
        public bool PresentLowCash
        {
            get { return _presentlowcash; }
            set { _presentlowcash = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ڸ�ֵʱ������")]
        [DisplayName("����ֽ�ֵ")]
        [DefaultValue(2000)]
        public long PresentLowCashLimit
        {
            get { return _presentlowcashlimit; }
            set { _presentlowcashlimit = value; }
        }

        [Category("���Ĳ���")]
        [Description("��̨�ϵ�ʳ������������ָ��ֵʱ������")]
        [DisplayName("��̨�ϵ�ʳ������������ָ��ֵʱ������")]
        [DefaultValue(true)]
        public bool PresentFoodLowCount
        {
            get { return _presentfoodlowcount; }
            set { _presentfoodlowcount = value; }
        }

        [Category("���Ĳ���")]
        [Description("��Ͳ�̨�ϵ�ʳ��������")]
        [DisplayName("��Ͳ�̨�ϵ�ʳ��������")]
        [DefaultValue(2)]
        public int PresentFoodLowCountLimit
        {
            get { return _presentfoodlowcountlimit; }
            set { _presentfoodlowcountlimit = value; }
        }

        [Category("���Ĳ���")]
        [Description("�չ�ʳ��")]
        [DisplayName("�չ�ʳ��")]
        [DefaultValue(false)]
        public bool PurchaseFood
        {
            get { return _purchasefood; }
            set { _purchasefood = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ݽ��ױ��еĹ���۽��н���")]
        [DisplayName("���ݽ��ױ��еĹ���۽��н���")]
        [DefaultValue(true)]
        public bool PurchaseFoodByRefPrice
        {
            get { return _purchasefoodbyrefprice; }
            set { _purchasefoodbyrefprice = value; }
        }

        [Category("���Ĳ���")]
        [Description("����ʳ��")]
        [DisplayName("����ʳ��")]
        [DefaultValue(false)]
        public bool SellFood
        {
            get { return _sellfood; }
            set { _sellfood = value; }
        }

        [Category("���Ĳ���")]
        [Description("���ݽ��ױ��еĳ��ۼ۽��н���")]
        [DisplayName("���ݽ��ױ��еĳ��ۼ۽��н���")]
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
