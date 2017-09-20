using System;

namespace Johnny.CMS.OM.Access
{
    /// <summary>
    /// Entity Class AdminLoginLog
    /// </summary>
    [Serializable]
    public class AdminLoginLog
    {
        #region declaration
        private string _TableName = "cms_adminloginlog";
        private string _PrimaryKey = "Id";
        private bool _IsDesc = true;
        private int _id;
        private string _name;
        private string _password;
        private DateTime _logintime;
        private DateTime _logouttime;
        private string _loginip;
        private string _hostername;
        private string _loginstatus;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AdminLoginLog() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public AdminLoginLog(int id, string name, string password, DateTime logintime, DateTime logouttime, string loginip, string hostername, string loginstatus)
        {
            this._id = id;
            this._name = name;
            this._password = password;
            this._logintime = logintime;
            this._logouttime = logouttime;
            this._loginip = loginip;
            this._hostername = hostername;
            this._loginstatus = loginstatus;
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
        /// ����Ա��
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public DateTime LoginTime
        {
            get { return _logintime; }
            set { _logintime = value; }
        }
        /// <summary>
        /// �ǳ�ʱ��
        /// </summary>
        public DateTime LogoutTime
        {
            get { return _logouttime; }
            set { _logouttime = value; }
        }
        /// <summary>
        /// ��¼ʱIP��ַ
        /// </summary>
        public string LoginIP
        {
            get { return _loginip; }
            set { _loginip = value; }
        }
        /// <summary>
        /// ��¼ʱ������
        /// </summary>
        public string HosterName
        {
            get { return _hostername; }
            set { _hostername = value; }
        }
        /// <summary>
        /// ��¼״��
        /// </summary>
        public string LoginStatus
        {
            get { return _loginstatus; }
            set { _loginstatus = value; }
        }
        #endregion
    }
}
