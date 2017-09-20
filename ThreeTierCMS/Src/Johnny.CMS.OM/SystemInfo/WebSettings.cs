using System;

namespace Johnny.CMS.OM.SystemInfo
{
    /// <summary>
    /// Entity Class Websettings
    /// </summary>
    [Serializable]
    public class WebSettings
    {
        #region declaration
        private string _TableName = "cms_websettings";
        private string _PrimaryKey = "Id";
        private bool _IsDesc = false;
        private int _id;
        private string _websitename;
        private string _websitetitle;
        private string _shortdescription;
        private string _tel;
        private string _fax;
        private string _email;
        private string _websiteaddress;
        private string _websitepath;
        private int _filesize;
        private string _logopath;
        private string _bannerpath;
        private string _copyright;
        private string _metakeywords;
        private string _metadescription;
        private bool _isclosed;
        private string _closedinfo;
        private string _useragreement;
        private bool _logintype;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public WebSettings() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public WebSettings(int id, string websitename, string websitetitle, string shortdescription, string tel, string fax, string email, string websiteaddress, string websitepath, int filesize, string logopath, string bannerpath, string copyright, string metakeywords, string metadescription, bool isclosed, string closedinfo, string useragreement, bool logintype)
        {
            this._id = id;
            this._websitename = websitename;
            this._websitetitle = websitetitle;
            this._shortdescription = shortdescription;
            this._tel = tel;
            this._fax = fax;
            this._email = email;
            this._websiteaddress = websiteaddress;
            this._websitepath = websitepath;
            this._filesize = filesize;
            this._logopath = logopath;
            this._bannerpath = bannerpath;
            this._copyright = copyright;
            this._metakeywords = metakeywords;
            this._metadescription = metadescription;
            this._isclosed = isclosed;
            this._closedinfo = closedinfo;
            this._useragreement = useragreement;
            this._logintype = logintype;
        }
        #endregion

        #region property
        /// <summary>
        /// TableName
        /// </summary>
        public string TableName
        {
            get { return _TableName; }
        }
        /// <summary>
        /// PrimaryKey
        /// </summary>
        public string PrimaryKey
        {
            get { return _PrimaryKey; }
        }
        /// <summary>
        /// IsDesc
        /// </summary>
        public bool IsDesc
        {
            get { return _IsDesc; }
        }
        /// <summary>
        /// ��ţ��Զ���1��
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ��վ����
        /// </summary>
        public string WebsiteName
        {
            get { return _websitename; }
            set { _websitename = value; }
        }
        /// <summary>
        /// ��վ����
        /// </summary>
        public string WebsiteTitle
        {
            get { return _websitetitle; }
            set { _websitetitle = value; }
        }
        /// <summary>
        /// ��վ���
        /// </summary>
        public string ShortDescription
        {
            get { return _shortdescription; }
            set { _shortdescription = value; }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string WebsiteAddress
        {
            get { return _websiteaddress; }
            set { _websiteaddress = value; }
        }
        /// <summary>
        /// ��װ·��
        /// </summary>
        public string WebsitePath
        {
            get { return _websitepath; }
            set { _websitepath = value; }
        }
        /// <summary>
        /// �ϴ��ļ���С
        /// </summary>
        public int FileSize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }
        /// <summary>
        /// LOGO��ַ
        /// </summary>
        public string LogoPath
        {
            get { return _logopath; }
            set { _logopath = value; }
        }
        /// <summary>
        /// Banner��ַ
        /// </summary>
        public string BannerPath
        {
            get { return _bannerpath; }
            set { _bannerpath = value; }
        }
        /// <summary>
        /// ��Ȩ��Ϣ
        /// </summary>
        public string Copyright
        {
            get { return _copyright; }
            set { _copyright = value; }
        }
        /// <summary>
        /// ��վ�ؼ���
        /// </summary>
        public string MetaKeywords
        {
            get { return _metakeywords; }
            set { _metakeywords = value; }
        }
        /// <summary>
        /// ��վ����
        /// </summary>
        public string MetaDescription
        {
            get { return _metadescription; }
            set { _metadescription = value; }
        }
        /// <summary>
        /// �Ƿ�ر���վ
        /// </summary>
        public bool IsClosed
        {
            get { return _isclosed; }
            set { _isclosed = value; }
        }
        /// <summary>
        /// �ر���վ����
        /// </summary>
        public string ClosedInfo
        {
            get { return _closedinfo; }
            set { _closedinfo = value; }
        }
        /// <summary>
        /// �û�Э��
        /// </summary>
        public string UserAgreement
        {
            get { return _useragreement; }
            set { _useragreement = value; }
        }
        /// <summary>
        /// ��̨��½��ʽ
        /// </summary>
        public bool LoginType
        {
            get { return _logintype; }
            set { _logintype = value; }
        }
        #endregion
    }
}
