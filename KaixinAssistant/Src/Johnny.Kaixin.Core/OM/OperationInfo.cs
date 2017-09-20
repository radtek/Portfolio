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
//<option value="16777215" >��ɫ</option>
//<option value="13421772" >��ɫ</option>
//<option value="0" >��ɫ</option>
//<option value="16711680" selected>��ɫ</option>
//<option value="255" >��ɫ</option>
//<option value="16776960" >��ɫ</option>            

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
        [Category("����λ")]
        [Description("����ͣ�ڴ������к��ѵĳ�λ��")]
        [DisplayName("ͣ��������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> ParkWhiteList
        {
            get { return _parkwhitelist; }
            set { _parkwhitelist = value; }
        }

        [Category("����λ")]
        [Description("������ͣ�ڴ������к��ѵĳ�λ��")]
        [DisplayName("ͣ���ڰ�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> ParkBlackList
        {
            get { return _parkblacklist; }
            set { _parkblacklist = value; }
        }

        [Category("����λ")]
        [Description("���������к��ѵĳ���ͣ���ҵĳ�λ��ʱ������������")]
        [DisplayName("��������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PostList
        {
            get { return _postlist; }
            set { _postlist = value; }
        }

        [Category("����λ")]
        [Description("����˭����������Ϊfalse����ֻ�������еĺ��ѡ�")]
        [DisplayName("��������")]
        [DefaultValue(true)]
        public bool PostAll
        {
            get { return _postall; }
            set { _postall = value; }
        }

        [Category("����λ")]
        [Description("������ɫ")]
        [DisplayName("������ɫ")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public int CarColor
        {
            get { return _carColor; }
            set { _carColor = value; }
        }        
        
        #endregion

        #region Bite
        [Category("ҧ��")]
        [Description("����ҧ�������еĺ���")]
        [DisplayName("ҧ�˰�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BiteWhiteList
        {
            get { return _bitewhitelist; }
            set { _bitewhitelist = value; }
        }

        [Category("ҧ��")]
        [Description("��ҧ�������е���")]
        [DisplayName("ҧ�˺�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BiteBlackList
        {
            get { return _biteblacklist; }
            set { _biteblacklist = value; }
        }

        [Category("ҧ��")]
        [Description("�����ڴ������к��ѵļ�����Ϣ")]
        [DisplayName("��Ϣ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RecoverWhiteList
        {
            get { return _recoverwhitelist; }
            set { _recoverwhitelist = value; }
        }

        [Category("ҧ��")]
        [Description("���ڴ������к��ѵļ�����Ϣ")]
        [DisplayName("��Ϣ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RecoverBlackList
        {
            get { return _recoverblacklist; }
            set { _recoverblacklist = value; }
        }

        [Category("ҧ��")]
        [Description("����������еĺ������Ҽ���Ϣ�����δ���ô��������������κ������Ҽ���Ϣ��")]
        [DisplayName("������Ϣ����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> AllowRestList
        {
            get { return _allowrestlist; }
            set { _allowrestlist = value; }
        }

        [Category("ҧ��")]
        [Description("�Ƿ�ҧ���еĺ��ѡ���Ϊfalse����ֻҧ�������еĺ��ѡ�")]
        [DisplayName("ҧ������")]
        [DefaultValue(true)]
        public bool BiteAll
        {
            get { return _biteall; }
            set { _biteall = value; }
        }

        [Category("ҧ��")]
        [Description("Ҫ�����ĺ���")]
        [DisplayName("Ҫ�����ĺ���")]
        public int ProtectId
        {
            get { return _protectid; }
            set { _protectid = value; }
        }

        #endregion

        #region Slave

        [Category("��������")]
        [Description("����������еĺ���")]
        [DisplayName("���������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BuyWhiteList
        {
            get { return _buywhitelist; }
            set { _buywhitelist = value; }
        }

        [Category("��������")]
        [Description("������������еĺ���")]
        [DisplayName("���������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> BuyBlackList
        {
            get { return _buyblacklist; }
            set { _buyblacklist = value; }
        }
        
        #endregion

        #region House

        [Category("����")]
        [Description("��ס�����º��ѵķ�����")]
        [DisplayName("��ס������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StayWhiteList
        {
            get { return _staywhitelist; }
            set { _staywhitelist = value; }
        }

        [Category("����")]
        [Description("����ס�����º��ѵķ�����")]
        [DisplayName("��ס������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StayBlackList
        {
            get { return _stayblacklist; }
            set { _stayblacklist = value; }
        }

        [Category("����")]
        [Description("�����ѵ��Լ��ķ�����")]
        [DisplayName("���˰�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RobWhiteList
        {
            get { return _robwhitelist; }
            set { _robwhitelist = value; }
        }

        [Category("����")]
        [Description("�������º��ѵ��Լ��ķ�����")]
        [DisplayName("���˺�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> RobBlackList
        {
            get { return _robblacklist; }
            set { _robblacklist = value; }
        }

        #endregion

        #region Garden

        [Category("��԰")]
        [Description("͵�����������к��ѵĲ�԰")]
        [DisplayName("͵�԰�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealWhiteList
        {
            get { return _stealwhitelist; }
            set { _stealwhitelist = value; }
        }

        [Category("��԰")]
        [Description("��͵�����������к��ѵĲ�԰")]
        [DisplayName("͵�Ժ�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealBlackList
        {
            get { return _stealblacklist; }
            set { _stealblacklist = value; }
        }

        [Category("��԰")]
        [Description("����˭��͵����Ϊfalse����ֻ͵�������еĺ��ѡ�")]
        [DisplayName("͵������")]
        [DefaultValue(true)]
        public bool StealAll
        {
            get { return _stealall; }
            set { _stealall = value; }
        }

        [Category("��԰")]
        [Description("ȥ���������к��ѵĲ�԰���ְ��ĵؿ�")]
        [DisplayName("���ְ�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FarmWhiteList
        {
            get { return _farmwhitelist; }
            set { _farmwhitelist = value; }
        }

        [Category("��԰")]
        [Description("��ȥ���������к��ѵĲ�԰���ְ��ĵؿ�")]
        [DisplayName("���ֺ�����")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FarmBlackList
        {
            get { return _farmblacklist; }
            set { _farmblacklist = value; }
        }

        [Category("��԰")]
        [Description("����˭�����֡���Ϊfalse����ֻȥ�������к��ѵĻ�԰���֡�")]
        [DisplayName("ȥ���к��ѵĻ�԰����")]
        [DefaultValue(true)]
        public bool FarmAll
        {
            get { return _farmall; }
            set { _farmall = value; }
        }

        [Category("��԰")]
        [Description("��ú������͹�ʵ")]
        [DisplayName("Ҫ���͵ĺ���")]
        public int PresentId
        {
            get { return _presentid; }
            set { _presentid = value; }
        }
        #endregion

        #region Ranch
        [Category("����")]
        [Description("ȥ���������к��ѵ�������æ����ˮ�������ݣ�������")]
        [DisplayName("��æ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpRanchWhiteList
        {
            get { return _helpranchwhitelist; }
            set { _helpranchwhitelist = value; }
        }

        [Category("����")]
        [Description("��ȥ���������к��ѵ�������æ����ˮ�������ݣ�������")]
        [DisplayName("��æ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpRanchBlackList
        {
            get { return _helpranchblacklist; }
            set { _helpranchblacklist = value; }
        }

        [Category("����")]
        [Description("����˭����æ����Ϊfalse����ֻ��������еĺ��ѡ�")]
        [DisplayName("��æ������")]
        [DefaultValue(true)]
        public bool HelpRanchAll
        {
            get { return _helpranchall; }
            set { _helpranchall = value; }
        }

        [Category("����")]
        [Description("ȥ���������к��ѵ�����͵��ũ����Ʒ")]
        [DisplayName("͵��ũ����Ʒ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealProductWhiteList
        {
            get { return _stealproductwhitelist; }
            set { _stealproductwhitelist = value; }
        }

        [Category("����")]
        [Description("��ȥ���������к��ѵ�����͵��ũ����Ʒ")]
        [DisplayName("͵��ũ����Ʒ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> StealProductBlackList
        {
            get { return _stealproductblacklist; }
            set { _stealproductblacklist = value; }
        }

        [Category("����")]
        [Description("����˭��͵����Ϊfalse����ֻȥ�������к��ѵ�����͵��")]
        [DisplayName("͵���к��ѵ�����")]
        [DefaultValue(true)]
        public bool StealProductAll
        {
            get { return _stealproductall; }
            set { _stealproductall = value; }
        }

        [Category("����")]
        [Description("��ú�������ũ����Ʒ")]
        [DisplayName("Ҫ���͵ĺ���")]
        public int PresentProductId
        {
            get { return _presentproductid; }
            set { _presentproductid = value; }
        }
        #endregion

        #region Fish

        [Category("����")]
        [Description("ȥ���������к��ѵ���������")]
        [DisplayName("���������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FishingWhiteList
        {
            get { return _fishingwhitelist; }
            set { _fishingwhitelist = value; }
        }

        [Category("����")]
        [Description("��ȥ���������к��ѵ���������")]
        [DisplayName("���������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> FishingBlackList
        {
            get { return _fishingblacklist; }
            set { _fishingblacklist = value; }
        }

        [Category("����")]
        [Description("����˭��������ȥ������Ϊfalse����ֻȥ�������еĺ����������㡣")]
        [DisplayName("ȥ������������")]
        [DefaultValue(true)]
        public bool FishingAll
        {
            get { return _fishingall; }
            set { _fishingall = value; }
        }

        [Category("����")]
        [Description("ȥ���������к��ѵ�������æ����")]
        [DisplayName("��æ���������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpFishWhiteList
        {
            get { return _helpfishwhitelist; }
            set { _helpfishwhitelist = value; }
        }

        [Category("����")]
        [Description("��ȥ���������к��ѵ�������æ����")]
        [DisplayName("��æ���������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HelpFishBlackList
        {
            get { return _helpfishblacklist; }
            set { _helpfishblacklist = value; }
        }

        [Category("����")]
        [Description("����˭��������ȥ������Ϊfalse����ֻȥ�������еĺ����������㡣")]
        [DisplayName("ȥ������������")]
        [DefaultValue(true)]
        public bool HelpFishAll
        {
            get { return _helpfishall; }
            set { _helpfishall = value; }
        }

        [Category("����")]
        [Description("��ú���������")]
        [DisplayName("Ҫ���͵ĺ���")]
        public int PresentFishId
        {
            get { return _presentfishid; }
            set { _presentfishid = value; }
        }
        #endregion

        #region Cafe

        [Category("���Ĳ���")]
        [Description("��Ӷ���������еĺ���")]
        [DisplayName("��Ӷ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HireWhiteList
        {
            get { return _hirewhitelist; }
            set { _hirewhitelist = value; }
        }

        [Category("���Ĳ���")]
        [Description("��ֹ��Ӷ���������еĺ���")]
        [DisplayName("��Ӷ������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> HireBlackList
        {
            get { return _hireblacklist; }
            set { _hireblacklist = value; }
        }

        [Category("���Ĳ���")]
        [Description("����˭�����Թ�Ӷ����Ϊfalse����ֻ��Ӷ�������еĺ��ѡ�")]
        [DisplayName("��Ӷ�������")]
        [DefaultValue(true)]
        public bool HireAll
        {
            get { return _hireall; }
            set { _hireall = value; }
        }

        [Category("���Ĳ���")]
        [Description("�չ����������к��ѵ�ʳ��")]
        [DisplayName("�չ�������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PurchaseWhiteList
        {
            get { return _purchasewhitelist; }
            set { _purchasewhitelist = value; }
        }

        [Category("���Ĳ���")]
        [Description("��ֹ�չ����������к��ѵ�ʳ��")]
        [DisplayName("�չ�������")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<int> PurchaseBlackList
        {
            get { return _purchaseblacklist; }
            set { _purchaseblacklist = value; }
        }

        [Category("���Ĳ���")]
        [Description("�չ����к��ѵ�ʳ���Ϊfalse����ֻ�չ��������к��ѵ�ʳ�")]
        [DisplayName("�չ����к��ѵ�ʳ��")]
        [DefaultValue(true)]
        public bool PurchaseAll
        {
            get { return _purchaseall; }
            set { _purchaseall = value; }
        }

        [Category("���Ĳ���")]
        [Description("��ú�������ʳ��")]
        [DisplayName("Ҫ����ʳ��ĺ���")]
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
