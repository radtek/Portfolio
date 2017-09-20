using System;

namespace Johnny.CMS.OM.Access
{
    /// <summary>
    /// Entity Class Rolepermission
    /// </summary>
    [Serializable]
    public partial class RolePermission
    {
        #region declaration
        private string _TableName = "cms_rolepermission";
        private string _PrimaryKey = "RoleId";
        private bool _IsDesc = false;
        private int _roleid;
        private int _permissionid;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public RolePermission() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public RolePermission(int roleid, int permissionid)
        {
            this._roleid = roleid;
            this._permissionid = permissionid;
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
        /// Role
        /// </summary>
        public int RoleId
        {
            get { return _roleid; }
            set { _roleid = value; }
        }
        /// <summary>
        /// Permission
        /// </summary>
        public int PermissionId
        {
            get { return _permissionid; }
            set { _permissionid = value; }
        }
        #endregion
    }

    public partial class RolePermission
    {
        private string _rolename;
        private string _permissionname;

        #region constructors
        /// <summary>
        /// Full constructor
        /// </summary>
        public RolePermission(int roleid, string rolename, int permissionid, string permissionname)
        {
            this._roleid = roleid;
            this._rolename = rolename;
            this._permissionid = permissionid;
            this._permissionname = permissionname;
        }
        #endregion

        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName
        {
            get { return _rolename; }
            set { _rolename = value; }
        }

        /// <summary>
        /// Permission Name
        /// </summary>
        public string PermissionName
        {
            get { return _permissionname; }
            set { _permissionname = value; }
        }
    }
}
