using System;

namespace Johnny.CMS.OM.SystemInfo
{
    /// <summary>
    /// Entity Class Pagebinding
    /// </summary>
    [Serializable]
    public partial class PageBinding
    {
        #region declaration
        private string _TableName = "cms_pagebinding";
        private string _PrimaryKey = "PageBindingId";
        private bool _IsDesc = false;
        private int _pagebindingid;
        private string _title;
        private int _menucategoryid;
        private int _listmenuid;
        private int _addmenuid;
        private int _sequence;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public PageBinding() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public PageBinding(int pagebindingid, string title, int menucategoryid, int listmenuid, int addmenuid, int sequence)
        {
            this._pagebindingid = pagebindingid;
            this._title = title;
            this._menucategoryid = menucategoryid;
            this._listmenuid = listmenuid;
            this._addmenuid = addmenuid;
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
        /// Page Binding Id
        /// </summary>
        public int PageBindingId
        {
            get { return _pagebindingid; }
            set { _pagebindingid = value; }
        }
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// Menu Category
        /// </summary>
        public int MenuCategoryId
        {
            get { return _menucategoryid; }
            set { _menucategoryid = value; }
        }
        /// <summary>
        /// List Page
        /// </summary>
        public int ListMenuId
        {
            get { return _listmenuid; }
            set { _listmenuid = value; }
        }
        /// <summary>
        /// Add Page
        /// </summary>
        public int AddMenuId
        {
            get { return _addmenuid; }
            set { _addmenuid = value; }
        }
        #endregion
    }

    public partial class PageBinding
    {
        private string _menucategoryname;
        private string _listmenuname;
        private string _listpagelink;
        private string _addmenuname;
        private string _addpagelink;

        #region constructors
        /// <summary>
        /// Full constructor
        /// </summary>
        public PageBinding(int pagebindingid, string title, int menucategoryid, string menucategoryname, int listmenuid, string listmenuname, string listpagelink, int addmenuid, string addmenuname, string addpagelink)
        {
            this._pagebindingid = pagebindingid;
            this._title = title;
            this._menucategoryid = menucategoryid;
            this._menucategoryname = menucategoryname;
            this._listmenuid = listmenuid;
            this._listmenuname = listmenuname;
            this._listpagelink = listpagelink;
            this._addmenuid = addmenuid;
            this._addmenuname = addmenuname;
            this._addpagelink = addpagelink;
        }        
        #endregion

        public string MenuCategoryName
        {
            get { return _menucategoryname; }
            set { _menucategoryname = value; }
        }

        /// <summary>
        /// List Menu Name
        /// </summary>
        public string ListMenuName
        {
            get { return _listmenuname; }
            set { _listmenuname = value; }
        }

        /// <summary>
        /// List Page Link
        /// </summary>
        public string ListPageLink
        {
            get { return _listpagelink; }
            set { _listpagelink = value; }
        }

        /// <summary>
        /// List Menu Name
        /// </summary>
        public string AddMenuName
        {
            get { return _addmenuname; }
            set { _addmenuname = value; }
        }

        /// <summary>
        /// Add Page Link
        /// </summary>
        public string AddPageLink
        {
            get { return _addpagelink; }
            set { _addpagelink = value; }
        }
    }
}
