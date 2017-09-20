using System;

namespace Johnny.CMS.OM.Access
{
    /// <summary>
    /// Entity Class Adminrole
    /// </summary>
    [Serializable]
    public partial class AdminRole
    {
        #region declaration
        private string _TableName = "cms_adminrole";
        private string _PrimaryKey = "AdminRoleId";
        private bool _IsDesc = false;
        private int _adminroleid;
        private int _adminid;
        private int _roleid;
        private int _sequence;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AdminRole() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public AdminRole(int adminroleid, int adminid, int roleid, int sequence)
        {
            this._adminroleid = adminroleid;
            this._adminid = adminid;
            this._roleid = roleid;
            this._sequence = sequence;
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
        /// AdminRoleId
        /// </summary>
        public int AdminRoleId
        {
            get { return _adminroleid; }
            set { _adminroleid = value; }
        }
        /// <summary>
        /// Admin Id
        /// </summary>
        public int AdminId
        {
            get { return _adminid; }
            set { _adminid = value; }
        }
        /// <summary>
        /// Role Id
        /// </summary>
        public int RoleId
        {
            get { return _roleid; }
            set { _roleid = value; }
        }
        /// <summary>
        /// Sequence
        /// </summary>
        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }
        #endregion
    }

    public partial class AdminRole
    {
        private string _name;
        private string _rolename;

        #region constructors
        /// <summary>
        /// Full constructor
        /// </summary>
        public AdminRole(int adminroleid, int adminid, string name, int roleid, string rolename, int sequence)
        {
            this._adminroleid = adminroleid;
            this._adminid = adminid;
            this._name = name;
            this._roleid = roleid;
            this._rolename = rolename;
            this._sequence = sequence;
        }
        #endregion

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string RoleName
        {
            get { return _rolename; }
            set { _rolename = value; }
        }
    }
}
