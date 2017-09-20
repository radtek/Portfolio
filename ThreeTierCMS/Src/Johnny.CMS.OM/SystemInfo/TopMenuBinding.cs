using System;

namespace Johnny.CMS.OM.SystemInfo
{
    /// <summary>
    /// Entity Class Topmainmenu
    /// </summary>
    [Serializable]
    public partial class TopMenuBinding
    {
        #region declaration
        private string _TableName = "cms_topmenubinding";
        private string _PrimaryKey = "TopMenuId";
        private bool _IsDesc = false;
        private int _topmenuid;
        private int _menucategoryid;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public TopMenuBinding() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public TopMenuBinding(int topmenuid, int menucategoryid)
        {
            this._topmenuid = topmenuid;
            this._menucategoryid = menucategoryid;
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
        /// Top Menu
        /// </summary>
        public int TopMenuId
        {
            get { return _topmenuid; }
            set { _topmenuid = value; }
        }
        /// <summary>
        /// Menu Category
        /// </summary>
        public int MenuCategoryId
        {
            get { return _menucategoryid; }
            set { _menucategoryid = value; }
        }
        #endregion
    }

    public partial class TopMenuBinding
    {
        private string _topmenuname;
        private string _menucategoryname;

        #region constructors
        /// <summary>
        /// Full constructor
        /// </summary>
        public TopMenuBinding(int topmenuid, string topmenuname, int menucategoryid, string menucategoryname)
        {
            this._topmenuid = topmenuid;
            this._topmenuname = topmenuname;
            this._menucategoryid = menucategoryid;
            this._menucategoryname = menucategoryname;
        }
        #endregion

        public string TopMenuName
        {
            get { return _topmenuname; }
            set { _topmenuname = value; }
        }
        public string MenuCategoryName
        {
            get { return _menucategoryname; }
            set { _menucategoryname = value; }
        }
    }
}
