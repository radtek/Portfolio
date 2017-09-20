using System;
using System.IO;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Johnny.Controls.Web.WebPager
{
    #region PagingMode enum
    public enum PagingMode
    {
        Cached,
        NonCached
    }
    #endregion

    #region PagerStyle enum
    public enum PagerStyle
    {
        NextPrev,
        NumericPages
    }
    #endregion

    #region VirtualRecordCount class
    public class VirtualRecordCount
    {
        public int RecordCount;
        public int PageCount;
        public int RecordsInLastPage;
    }
    #endregion

    #region PageChangedEventArgs class
    public class PageChangedEventArgs : EventArgs
    {
        public int OldPageIndex;
        public int NewPageIndex;
    }
    #endregion

    #region SqlPager Control

    [DefaultProperty("SelectCommand")]
    [DefaultEvent("PageIndexChanged")]
    [ToolboxData("<{0}:SqlPager runat=\"server\" />")]
    public class SqlPager : WebControl, INamingContainer
    {
        #region  PRIVATE DATA MEMBERS
        // ***********************************************************************
        // PRIVATE members
        private PagedDataSource _dataSource;
        private Control _controlToPaginate;
        private string CacheKeyName
        {
            get { return Page.Request.FilePath + "_" + UniqueID + "_Data"; }
        }
        private string CurrentPageText = "总记录:<font color=#ff0000><B>{2}</B></font>&nbsp&nbsp页次:<font color=#ff0000><B>{0}</B></font>/<B>{1}</B>页&nbsp&nbsp<B>{3}</B>/页";
        private string NoPageSelectedText = "";
        private string QueryPageCommandText = "SELECT * FROM " +
            "(SELECT TOP {0} * FROM " +
            "(SELECT TOP {1} * FROM ({2}) AS t0 ORDER BY {3} {4}) AS t1 " +
            "ORDER BY {3} {5}) AS t2 " +
            "ORDER BY {3}";
        private string QueryCountCommandText = "SELECT COUNT(*) FROM ({0}) AS t0";
        // ***********************************************************************
        #endregion

        #region CTOR(s)
        // ***********************************************************************
        // Ctor
        public SqlPager()
            : base()
        {
            _dataSource = null;
            _controlToPaginate = null;

            PagingMode = PagingMode.Cached;
            PagerStyle = PagerStyle.NextPrev;
            CurrentPageIndex = 0;
            SelectCommand = "";
            ConnectionString = "";
            ItemsPerPage = 10;
            TotalPages = -1;
            TotalRecord = -1;
            CacheDuration = 60;
        }
        // ***********************************************************************
        #endregion

        #region PUBLIC PROGRAMMING INTERFACE
        // ***********************************************************************
        // METHOD ClearCache
        // Removes any data cached for paging
        public void ClearCache()
        {
            if (PagingMode == PagingMode.Cached)
                Page.Cache.Remove(CacheKeyName);
        }
        // ***********************************************************************

        // ***********************************************************************
        // EVENT PageIndexChanged
        // Fires when the pager is about to switch to a new page
        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);
        public event PageChangedEventHandler PageIndexChanged;
        protected virtual void OnPageIndexChanged(PageChangedEventArgs e)
        {
            if (PageIndexChanged != null)
                PageIndexChanged(this, e);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY CacheDuration
        [Description("Gets and sets for how many seconds the data should stay in the cache")]
        public int CacheDuration
        {
            get { return Convert.ToInt32(ViewState["CacheDuration"]); }
            set { ViewState["CacheDuration"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PagingMode
        [Description("Indicates whether the data are retrieved page by page or can be cached")]
        public PagingMode PagingMode
        {
            get { return (PagingMode)ViewState["PagingMode"]; }
            set { ViewState["PagingMode"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PagerStyle
        [Description("Indicates the style of the pager's navigation bar")]
        public PagerStyle PagerStyle
        {
            get { return (PagerStyle)ViewState["PagerStyle"]; }
            set { ViewState["PagerStyle"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ControlToPaginate
        [Description("Gets and sets the name of the control to paginate")]
        public string ControlToPaginate
        {
            get { return Convert.ToString(ViewState["ControlToPaginate"]); }
            set { ViewState["ControlToPaginate"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ItemsPerPage
        [Description("Gets and sets the number of items to display per page")]
        public int ItemsPerPage
        {
            get { return Convert.ToInt32(ViewState["ItemsPerPage"]); }
            set { ViewState["ItemsPerPage"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY CurrentPageIndex
        [Description("Gets and sets the index of the currently displayed page")]
        public int CurrentPageIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentPageIndex"]); }
            set { ViewState["CurrentPageIndex"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ConnectionString
        [Description("Gets and sets the connection string to access the database")]
        public string ConnectionString
        {
            get { return Convert.ToString(ViewState["ConnectionString"]); }
            set { ViewState["ConnectionString"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY SelectCommand
        [Description("Gets and sets the SQL query to get data")]
        public string SelectCommand
        {
            get { return Convert.ToString(ViewState["SelectCommand"]); }
            set { ViewState["SelectCommand"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY SortField
        [Description("Gets and sets the sort-by field. It is mandatory in NonCached mode.)")]
        public string SortField
        {
            get { return Convert.ToString(ViewState["SortKeyField"]); }
            set { ViewState["SortKeyField"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PageCount
        // Gets the number of displayable pages 
        [Browsable(false)]
        public int PageCount
        {
            get { return TotalPages; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY TotalPages
        // Gets and sets the number of pages to display 
        protected int TotalPages
        {
            get { return Convert.ToInt32(ViewState["TotalPages"]); }
            set { ViewState["TotalPages"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY TotalPages
        // Gets and sets the number of pages to display 
        protected int TotalRecord
        {
            get { return Convert.ToInt32(ViewState["TotalRecord"]); }
            set { ViewState["TotalRecord"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE DataBind
        // Fetches and stores the data
        public override void DataBind()
        {
            // Fires the data binding event
            base.DataBind();

            // Controls must be recreated after data binding 
            ChildControlsCreated = false;

            // Ensures the control exists and is a list control
            if (ControlToPaginate == "")
                return;
            _controlToPaginate = Page.FindControl(ControlToPaginate);
            if (_controlToPaginate == null)
                return;
            if (!(_controlToPaginate is BaseDataList || _controlToPaginate is ListControl))
                return;

            // Ensures enough info to connect and query is specified
            if (ConnectionString == "" || SelectCommand == "")
                return;

            // Fetch data
            if (PagingMode == PagingMode.Cached)
                FetchAllData();
            else
            {
                //if (SortField == "")
                //	return;
                FetchPageData();
            }

            // Bind data to the buddy control
            BaseDataList baseDataListControl = null;
            ListControl listControl = null;
            if (_controlToPaginate is BaseDataList)
            {
                baseDataListControl = (BaseDataList)_controlToPaginate;
                baseDataListControl.DataSource = _dataSource;
                baseDataListControl.DataBind();
                return;
            }
            if (_controlToPaginate is ListControl)
            {
                listControl = (ListControl)_controlToPaginate;
                listControl.Items.Clear();
                listControl.DataSource = _dataSource;
                listControl.DataBind();
                return;
            }
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE Render
        // Writes the content to be rendered on the client
        protected override void Render(HtmlTextWriter output)
        {
            // If in design-mode ensure that child controls have been created.
            // Child controls are not created at this time in design-mode because
            // there's no pre-render stage. Do so for composite controls like this 
            if (Site != null && Site.DesignMode)
                CreateChildControls();

            base.Render(output);
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE CreateChildControls
        // Outputs the HTML markup for the control
        protected override void CreateChildControls()
        {
            Controls.Clear();
            ClearChildViewState();

            BuildControlHierarchy();
        }
        // ***********************************************************************
        #endregion

        #region PRIVATE HELPER METHODS
        // ***********************************************************************
        // PRIVATE BuildControlHierarchy
        // Control the building of the control's hierarchy
        private void BuildControlHierarchy()
        {
            // Build the surrounding table (one row, two cells)
            Table t = new Table();
            t.Width = new Unit("100%");
            // Build the table row
            TableRow row = new TableRow();
            t.Rows.Add(row);

            // Build the cell with the page index
            TableCell cellPageDesc = new TableCell();
            cellPageDesc.HorizontalAlign = HorizontalAlign.Left;
            BuildCurrentPage(cellPageDesc);
            row.Cells.Add(cellPageDesc);

            // Build the cell with navigation bar
            TableCell cellNavBar = new TableCell();
            if (PagerStyle == PagerStyle.NextPrev)
                BuildNextPrevUI(cellNavBar);
            else
                BuildNumericPagesUI(cellNavBar);
            cellNavBar.HorizontalAlign = HorizontalAlign.Right;
            row.Cells.Add(cellNavBar);

            // Add the table to the control tree
            Controls.Add(t);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildNextPrevUI
        // Generates the HTML markup for the Next/Prev navigation bar
        private void BuildNextPrevUI(TableCell cell)
        {
            bool isValidPage = (CurrentPageIndex >= 0 && CurrentPageIndex <= TotalPages - 1);
            bool canMoveBack = (CurrentPageIndex > 0);
            bool canMoveForward = (CurrentPageIndex < TotalPages - 1);

            // Render the << button
            LinkButton first = new LinkButton();
            first.ID = "First";
            first.CausesValidation = false;
            first.Click += new EventHandler(first_Click);
            first.Text = " 首页 ";
            first.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(first);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the < button
            LinkButton prev = new LinkButton();
            prev.ID = "Prev";
            prev.CausesValidation = false;
            prev.Click += new EventHandler(prev_Click);
            prev.Text = " 上页 ";
            prev.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(prev);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the > button
            LinkButton next = new LinkButton();
            next.ID = "Next";
            next.CausesValidation = false;
            next.Click += new EventHandler(next_Click);
            next.Text = " 下页 ";
            next.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(next);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the >> button
            LinkButton last = new LinkButton();
            last.ID = "Last";
            last.CausesValidation = false;
            last.Click += new EventHandler(last_Click);
            last.Text = " 尾页 ";
            last.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(last);

            // Render a drop-down list  
            System.Web.UI.WebControls.DropDownList pageList = new System.Web.UI.WebControls.DropDownList();
            pageList.ID = "PageList";
            pageList.AutoPostBack = true;
            pageList.SelectedIndexChanged += new EventHandler(PageList_Click);
            pageList.Font.Name = Font.Name;
            pageList.Font.Size = Font.Size;
            pageList.ForeColor = ForeColor;

            // Embellish the list when there are no pages to list 
            if (TotalPages <= 0 || CurrentPageIndex == -1)
            {
                pageList.Items.Add("");
                pageList.Enabled = false;
                pageList.SelectedIndex = 0;
            }
            else // Populate the list
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    ListItem item = new ListItem(i.ToString(), (i - 1).ToString());
                    pageList.Items.Add(item);
                }
                pageList.SelectedIndex = CurrentPageIndex;
            }
            cell.Controls.Add(pageList);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildNumericPagesUI
        // Generates the HTML markup for the Numeric Pages button bar
        private void BuildNumericPagesUI(TableCell cell)
        {
            // Render a drop-down list  
            System.Web.UI.WebControls.DropDownList pageList = new System.Web.UI.WebControls.DropDownList();
            pageList.ID = "PageList";
            pageList.AutoPostBack = true;
            pageList.SelectedIndexChanged += new EventHandler(PageList_Click);
            pageList.Font.Name = Font.Name;
            pageList.Font.Size = Font.Size;
            pageList.ForeColor = ForeColor;

            // Embellish the list when there are no pages to list 
            if (TotalPages <= 0 || CurrentPageIndex == -1)
            {
                pageList.Items.Add("No pages");
                pageList.Enabled = false;
                pageList.SelectedIndex = 0;
            }
            else // Populate the list
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    ListItem item = new ListItem(i.ToString(), (i - 1).ToString());
                    pageList.Items.Add(item);
                }
                pageList.SelectedIndex = CurrentPageIndex;
            }
            cell.Controls.Add(pageList);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildCurrentPage
        // Generates the HTML markup to describe the current page (0-based)
        private void BuildCurrentPage(TableCell cell)
        {
            // Use a standard template: Page X of Y
            if (CurrentPageIndex < 0 || CurrentPageIndex >= TotalPages)
                cell.Text = NoPageSelectedText;
            else
                cell.Text = String.Format(CurrentPageText, (CurrentPageIndex + 1), TotalPages, TotalRecord, ItemsPerPage);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE ValidatePageIndex
        // Ensures the CurrentPageIndex is either valid [0,TotalPages) or -1
        private void ValidatePageIndex()
        {
            if (!(CurrentPageIndex >= 0 && CurrentPageIndex < TotalPages))
                CurrentPageIndex = -1;
            return;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE FetchAllData
        // Runs the query for all data to be paged and caches the resulting data
        private void FetchAllData()
        {
            // Looks for data in the ASP.NET Cache
            DataTable data;
            data = (DataTable)Page.Cache[CacheKeyName];

            if (data == null)
            {
                // Fix SelectCommand with order-by info
                AdjustSelectCommand(true);

                // If data expired or has never been fetched, go to the database
                SqlDataAdapter adapter = new SqlDataAdapter(SelectCommand, ConnectionString);
                data = new DataTable();
                adapter.Fill(data);
                Page.Cache.Insert(CacheKeyName, data, null,
                    DateTime.Now.AddSeconds(CacheDuration),
                    System.Web.Caching.Cache.NoSlidingExpiration);
            }

            // Configures the paged data source component
            if (_dataSource == null)
                _dataSource = new PagedDataSource();
            _dataSource.DataSource = data.DefaultView; // must be IEnumerable!
            _dataSource.AllowPaging = true;
            _dataSource.PageSize = ItemsPerPage;
            TotalPages = _dataSource.PageCount;
            TotalRecord = data.Rows.Count;
            // Ensures the page index is valid 
            ValidatePageIndex();
            if (CurrentPageIndex == -1)
            {
                _dataSource = null;
                return;
            }

            // Selects the page to view
            _dataSource.CurrentPageIndex = CurrentPageIndex;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE FetchPageData
        // Runs the query to get only the data that fit into the current page
        private void FetchPageData()
        {
            // Need a validated page index to fetch data.
            // Also need the virtual page count to validate the page index
            AdjustSelectCommand(false);
            VirtualRecordCount countInfo = CalculateVirtualRecordCount();
            TotalRecord = countInfo.RecordCount;
            TotalPages = countInfo.PageCount;
            // Validate the page number (ensures CurrentPageIndex is valid or -1)
            ValidatePageIndex();
            if (CurrentPageIndex == -1)
                return;

            // Prepare and run the command
            SqlCommand cmd = PrepareCommand(countInfo);
            if (cmd == null)
                return;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            // Configures the paged data source component
            if (_dataSource == null)
                _dataSource = new PagedDataSource();
            _dataSource.AllowCustomPaging = true;
            _dataSource.AllowPaging = true;
            _dataSource.CurrentPageIndex = 0;
            //_dataSource.PageSize = ItemsPerPage;
            _dataSource.PageSize = data.Rows.Count;
            _dataSource.VirtualCount = countInfo.RecordCount;
            _dataSource.DataSource = data.DefaultView;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE AdjustSelectCommand
        // Strips ORDER-BY clauses from SelectCommand and adds a new one based
        // on SortKeyField
        private void AdjustSelectCommand(bool addCustomSortInfo)
        {
            // Truncate where ORDER BY is found
            string temp = SelectCommand.ToLower();
            int pos = temp.IndexOf("order by");
            if (pos > -1)
                SelectCommand = SelectCommand.Substring(0, pos);

            // Add new ORDER BY info if SortKeyField is specified
            if (SortField != "" && addCustomSortInfo)
                SelectCommand += " ORDER BY " + SortField;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE CalculateVirtualRecordCount
        // Calculates record and page count for the specified query
        private VirtualRecordCount CalculateVirtualRecordCount()
        {
            VirtualRecordCount count = new VirtualRecordCount();

            // Calculate the virtual number of records from the query
            count.RecordCount = GetQueryVirtualCount();
            count.RecordsInLastPage = ItemsPerPage;

            // Calculate the correspondent number of pages
            int lastPage = count.RecordCount / ItemsPerPage;
            int remainder = count.RecordCount % ItemsPerPage;
            if (remainder > 0)
                lastPage++;
            count.PageCount = lastPage;

            // Calculate the number of items in the last page
            if (remainder > 0)
                count.RecordsInLastPage = remainder;
            return count;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE PrepareCommand
        // Prepares and returns the command object for the reader-based query
        private SqlCommand PrepareCommand(VirtualRecordCount countInfo)
        {
            // No sort field specified: figure it out
            if (SortField == "")
            {
                // Get metadata for all columns and choose either the primary key
                // or the 
                string text = "SET FMTONLY ON;" + SelectCommand + ";SET FMTONLY OFF;";
                SqlDataAdapter adapter = new SqlDataAdapter(text, ConnectionString);
                DataTable t = new DataTable();
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(t);
                DataColumn col = null;
                if (t.PrimaryKey.Length > 0)
                    col = t.PrimaryKey[0];
                else
                    col = t.Columns[0];
                SortField = col.ColumnName;
            }

            // Determines how many records are to be retrieved.
            // The last page could require less than other pages
            int recsToRetrieve = ItemsPerPage;
            if (CurrentPageIndex == countInfo.PageCount - 1)
                recsToRetrieve = countInfo.RecordsInLastPage;

            string cmdText = String.Format(QueryPageCommandText,
                recsToRetrieve,						// {0} --> page size
                ItemsPerPage * (CurrentPageIndex + 1),	// {1} --> size * index
                SelectCommand,						// {2} --> base query
                SortField,							// {3} --> key field in the query
                "ASC",								// Default to ascending order
                "DESC");

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            return cmd;
        }

        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE GetQueryVirtualCount
        // Run a query to get the record count
        private int GetQueryVirtualCount()
        {
            string cmdText = String.Format(QueryCountCommandText, SelectCommand);
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.Connection.Open();
            int recCount = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();

            return recCount;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE GoToPage
        // Sets the current page index
        private void GoToPage(int pageIndex)
        {
            // Prepares event data
            PageChangedEventArgs e = new PageChangedEventArgs();
            e.OldPageIndex = CurrentPageIndex;
            e.NewPageIndex = pageIndex;

            // Updates the current index
            CurrentPageIndex = pageIndex;

            // Fires the page changed event
            OnPageIndexChanged(e);

            // Binds new data
            DataBind();
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE first_Click
        // Event handler for the << button
        private void first_Click(object sender, EventArgs e)
        {
            GoToPage(0);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE prev_Click
        // Event handler for the < button
        private void prev_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex - 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE next_Click
        // Event handler for the > button
        private void next_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex + 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE last_Click
        // Event handler for the >> button
        private void last_Click(object sender, EventArgs e)
        {
            GoToPage(TotalPages - 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE PageList_Click
        // Event handler for any page selected from the drop-down page list 
        private void PageList_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.DropDownList pageList = (System.Web.UI.WebControls.DropDownList)sender;
            int pageIndex = Convert.ToInt32(pageList.SelectedItem.Value);
            GoToPage(pageIndex);
        }
        // ***********************************************************************
        #endregion
    }
    #endregion

    #region OraclePager Control

    [DefaultProperty("SelectCommand")]
    [DefaultEvent("PageIndexChanged")]
    [ToolboxData("<{0}:OraclePager runat=\"server\" />")]
    public class OraclePager : WebControl, INamingContainer
    {
        #region  PRIVATE DATA MEMBERS
        // ***********************************************************************
        // PRIVATE members
        private PagedDataSource _dataSource;
        private Control _controlToPaginate;
        private string CacheKeyName
        {
            get { return Page.Request.FilePath + "_" + UniqueID + "_Data"; }
        }
        private string CurrentPageText = "总记录:<font color=#ff0000><B>{2}</B></font>&nbsp&nbsp页次:<font color=#ff0000><B>{0}</B></font>/<B>{1}</B>页&nbsp&nbsp<B>{3}</B>/页";
        private string NoPageSelectedText = "";
        private string QueryPageCommandText = "SELECT * FROM " +
            "(SELECT t0.*,ROWNUM AS count_id FROM " +
            "({2}) t0) t1 " +
            "WHERE count_id BETWEEN {1}-{6}+1 " +
            "AND {1}";
        private string QueryCountCommandText = "SELECT COUNT(*) FROM ({0}) t0";
        // ***********************************************************************
        #endregion

        #region CTOR(s)
        // ***********************************************************************
        // Ctor
        public OraclePager()
            : base()
        {
            _dataSource = null;
            _controlToPaginate = null;

            PagingMode = PagingMode.Cached;
            PagerStyle = PagerStyle.NextPrev;
            CurrentPageIndex = 0;
            SelectCommand = "";
            ConnectionString = "";
            ItemsPerPage = 10;
            TotalPages = -1;
            TotalRecord = -1;
            CacheDuration = 60;
        }
        // ***********************************************************************
        #endregion

        #region PUBLIC PROGRAMMING INTERFACE
        // ***********************************************************************
        // METHOD ClearCache
        // Removes any data cached for paging
        public void ClearCache()
        {
            if (PagingMode == PagingMode.Cached)
                Page.Cache.Remove(CacheKeyName);
        }
        // ***********************************************************************

        // ***********************************************************************
        // EVENT PageIndexChanged
        // Fires when the pager is about to switch to a new page
        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);
        public event PageChangedEventHandler PageIndexChanged;
        protected virtual void OnPageIndexChanged(PageChangedEventArgs e)
        {
            if (PageIndexChanged != null)
                PageIndexChanged(this, e);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY CacheDuration
        [Description("Gets and sets for how many seconds the data should stay in the cache")]
        public int CacheDuration
        {
            get { return Convert.ToInt32(ViewState["CacheDuration"]); }
            set { ViewState["CacheDuration"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PagingMode
        [Description("Indicates whether the data are retrieved page by page or can be cached")]
        public PagingMode PagingMode
        {
            get { return (PagingMode)ViewState["PagingMode"]; }
            set { ViewState["PagingMode"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PagerStyle
        [Description("Indicates the style of the pager's navigation bar")]
        public PagerStyle PagerStyle
        {
            get { return (PagerStyle)ViewState["PagerStyle"]; }
            set { ViewState["PagerStyle"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ControlToPaginate
        [Description("Gets and sets the name of the control to paginate")]
        public string ControlToPaginate
        {
            get { return Convert.ToString(ViewState["ControlToPaginate"]); }
            set { ViewState["ControlToPaginate"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ItemsPerPage
        [Description("Gets and sets the number of items to display per page")]
        public int ItemsPerPage
        {
            get { return Convert.ToInt32(ViewState["ItemsPerPage"]); }
            set { ViewState["ItemsPerPage"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY CurrentPageIndex
        [Description("Gets and sets the index of the currently displayed page")]
        public int CurrentPageIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentPageIndex"]); }
            set { ViewState["CurrentPageIndex"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ConnectionString
        [Description("Gets and sets the connection string to access the database")]
        public string ConnectionString
        {
            get { return Convert.ToString(ViewState["ConnectionString"]); }
            set { ViewState["ConnectionString"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY SelectCommand
        [Description("Gets and sets the SQL query to get data")]
        public string SelectCommand
        {
            get { return Convert.ToString(ViewState["SelectCommand"]); }
            set { ViewState["SelectCommand"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY SortField
        [Description("Gets and sets the sort-by field. It is mandatory in NonCached mode.)")]
        public string SortField
        {
            get { return Convert.ToString(ViewState["SortKeyField"]); }
            set { ViewState["SortKeyField"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PageCount
        // Gets the number of displayable pages 
        [Browsable(false)]
        public int PageCount
        {
            get { return TotalPages; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY TotalPages
        // Gets and sets the number of pages to display 
        protected int TotalPages
        {
            get { return Convert.ToInt32(ViewState["TotalPages"]); }
            set { ViewState["TotalPages"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY TotalPages
        // Gets and sets the number of pages to display 
        protected int TotalRecord
        {
            get { return Convert.ToInt32(ViewState["TotalRecord"]); }
            set { ViewState["TotalRecord"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE DataBind
        // Fetches and stores the data
        public override void DataBind()
        {
            // Fires the data binding event
            base.DataBind();

            // Controls must be recreated after data binding 
            ChildControlsCreated = false;

            // Ensures the control exists and is a list control
            if (ControlToPaginate == "")
                return;
            _controlToPaginate = Page.FindControl(ControlToPaginate);
            if (_controlToPaginate == null)
                return;
            if (!(_controlToPaginate is BaseDataList || _controlToPaginate is ListControl))
                return;

            // Ensures enough info to connect and query is specified
            if (ConnectionString == "" || SelectCommand == "")
                return;

            // Fetch data
            if (PagingMode == PagingMode.Cached)
                FetchAllData();
            else
            {
                //if (SortField == "")
                //	return;
                FetchPageData();
            }

            // Bind data to the buddy control
            BaseDataList baseDataListControl = null;
            ListControl listControl = null;
            if (_controlToPaginate is BaseDataList)
            {
                baseDataListControl = (BaseDataList)_controlToPaginate;
                baseDataListControl.DataSource = _dataSource;
                baseDataListControl.DataBind();
                return;
            }
            if (_controlToPaginate is ListControl)
            {
                listControl = (ListControl)_controlToPaginate;
                listControl.Items.Clear();
                listControl.DataSource = _dataSource;
                listControl.DataBind();
                return;
            }
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE Render
        // Writes the content to be rendered on the client
        protected override void Render(HtmlTextWriter output)
        {
            // If in design-mode ensure that child controls have been created.
            // Child controls are not created at this time in design-mode because
            // there's no pre-render stage. Do so for composite controls like this 
            if (Site != null && Site.DesignMode)
                CreateChildControls();

            base.Render(output);
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE CreateChildControls
        // Outputs the HTML markup for the control
        protected override void CreateChildControls()
        {
            Controls.Clear();
            ClearChildViewState();

            BuildControlHierarchy();
        }
        // ***********************************************************************
        #endregion

        #region PRIVATE HELPER METHODS
        // ***********************************************************************
        // PRIVATE BuildControlHierarchy
        // Control the building of the control's hierarchy
        private void BuildControlHierarchy()
        {
            // Build the surrounding table (one row, two cells)
            Table t = new Table();
            t.Width = new Unit("100%");
            // Build the table row
            TableRow row = new TableRow();
            t.Rows.Add(row);

            // Build the cell with the page index
            TableCell cellPageDesc = new TableCell();
            cellPageDesc.HorizontalAlign = HorizontalAlign.Left;
            BuildCurrentPage(cellPageDesc);
            row.Cells.Add(cellPageDesc);

            // Build the cell with navigation bar
            TableCell cellNavBar = new TableCell();
            if (PagerStyle == PagerStyle.NextPrev)
                BuildNextPrevUI(cellNavBar);
            else
                BuildNumericPagesUI(cellNavBar);
            cellNavBar.HorizontalAlign = HorizontalAlign.Right;
            row.Cells.Add(cellNavBar);

            // Add the table to the control tree
            Controls.Add(t);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildNextPrevUI
        // Generates the HTML markup for the Next/Prev navigation bar
        private void BuildNextPrevUI(TableCell cell)
        {
            bool isValidPage = (CurrentPageIndex >= 0 && CurrentPageIndex <= TotalPages - 1);
            bool canMoveBack = (CurrentPageIndex > 0);
            bool canMoveForward = (CurrentPageIndex < TotalPages - 1);

            // Render the << button
            LinkButton first = new LinkButton();
            first.ID = "First";
            first.Click += new EventHandler(first_Click);
            first.Text = " 首页 ";
            first.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(first);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the < button
            LinkButton prev = new LinkButton();
            prev.ID = "Prev";
            prev.Click += new EventHandler(prev_Click);
            prev.Text = " 上页 ";
            prev.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(prev);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the > button
            LinkButton next = new LinkButton();
            next.ID = "Next";
            next.Click += new EventHandler(next_Click);
            next.Text = " 下页 ";
            next.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(next);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the >> button
            LinkButton last = new LinkButton();
            last.ID = "Last";
            last.Click += new EventHandler(last_Click);
            last.Text = " 尾页 ";
            last.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(last);

            // Render a drop-down list  
            System.Web.UI.WebControls.DropDownList pageList = new System.Web.UI.WebControls.DropDownList();
            pageList.ID = "PageList";
            pageList.AutoPostBack = true;
            pageList.SelectedIndexChanged += new EventHandler(PageList_Click);
            pageList.Font.Name = Font.Name;
            pageList.Font.Size = Font.Size;
            pageList.ForeColor = ForeColor;

            // Embellish the list when there are no pages to list 
            if (TotalPages <= 0 || CurrentPageIndex == -1)
            {
                pageList.Items.Add("");
                pageList.Enabled = false;
                pageList.SelectedIndex = 0;
            }
            else // Populate the list
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    ListItem item = new ListItem(i.ToString(), (i - 1).ToString());
                    pageList.Items.Add(item);
                }
                pageList.SelectedIndex = CurrentPageIndex;
            }
            cell.Controls.Add(pageList);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildNumericPagesUI
        // Generates the HTML markup for the Numeric Pages button bar
        private void BuildNumericPagesUI(TableCell cell)
        {
            // Render a drop-down list  
            System.Web.UI.WebControls.DropDownList pageList = new System.Web.UI.WebControls.DropDownList();
            pageList.ID = "PageList";
            pageList.AutoPostBack = true;
            pageList.SelectedIndexChanged += new EventHandler(PageList_Click);
            pageList.Font.Name = Font.Name;
            pageList.Font.Size = Font.Size;
            pageList.ForeColor = ForeColor;

            // Embellish the list when there are no pages to list 
            if (TotalPages <= 0 || CurrentPageIndex == -1)
            {
                pageList.Items.Add("No pages");
                pageList.Enabled = false;
                pageList.SelectedIndex = 0;
            }
            else // Populate the list
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    ListItem item = new ListItem(i.ToString(), (i - 1).ToString());
                    pageList.Items.Add(item);
                }
                pageList.SelectedIndex = CurrentPageIndex;
            }
            cell.Controls.Add(pageList);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildCurrentPage
        // Generates the HTML markup to describe the current page (0-based)
        private void BuildCurrentPage(TableCell cell)
        {
            // Use a standard template: Page X of Y
            if (CurrentPageIndex < 0 || CurrentPageIndex >= TotalPages)
                cell.Text = NoPageSelectedText;
            else
                cell.Text = String.Format(CurrentPageText, (CurrentPageIndex + 1), TotalPages, TotalRecord, ItemsPerPage);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE ValidatePageIndex
        // Ensures the CurrentPageIndex is either valid [0,TotalPages) or -1
        private void ValidatePageIndex()
        {
            if (!(CurrentPageIndex >= 0 && CurrentPageIndex < TotalPages))
                CurrentPageIndex = -1;
            return;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE FetchAllData
        // Runs the query for all data to be paged and caches the resulting data
        private void FetchAllData()
        {
            // Looks for data in the ASP.NET Cache
            DataTable data;

            data = (DataTable)Page.Cache[CacheKeyName];

            if (data == null)
            {
                // Fix SelectCommand with order-by info
                AdjustSelectCommand(true);

                // If data expired or has never been fetched, go to the database
                OracleDataAdapter adapter = new OracleDataAdapter(SelectCommand, ConnectionString);
                data = new DataTable();
                adapter.Fill(data);
                Page.Cache.Insert(CacheKeyName, data, null,
                    DateTime.Now.AddSeconds(CacheDuration),
                    System.Web.Caching.Cache.NoSlidingExpiration);
            }

            // Configures the paged data source component
            if (_dataSource == null)
                _dataSource = new PagedDataSource();
            _dataSource.DataSource = data.DefaultView; // must be IEnumerable!
            _dataSource.AllowPaging = true;
            _dataSource.PageSize = ItemsPerPage;
            TotalPages = _dataSource.PageCount;
            TotalRecord = data.Rows.Count;
            // Ensures the page index is valid 
            ValidatePageIndex();
            if (CurrentPageIndex == -1)
            {
                _dataSource = null;
                return;
            }

            // Selects the page to view
            _dataSource.CurrentPageIndex = CurrentPageIndex;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE FetchPageData
        // Runs the query to get only the data that fit into the current page
        private void FetchPageData()
        {
            // Need a validated page index to fetch data.
            // Also need the virtual page count to validate the page index
            AdjustSelectCommand(false);
            VirtualRecordCount countInfo = CalculateVirtualRecordCount();
            TotalRecord = countInfo.RecordCount;
            TotalPages = countInfo.PageCount;
            // Validate the page number (ensures CurrentPageIndex is valid or -1)
            ValidatePageIndex();
            if (CurrentPageIndex == -1)
                return;

            // Prepare and run the command
            OracleCommand cmd = PrepareCommand(countInfo);
            if (cmd == null)
                return;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            // Configures the paged data source component
            if (_dataSource == null)
                _dataSource = new PagedDataSource();
            _dataSource.AllowCustomPaging = true;
            _dataSource.AllowPaging = true;
            _dataSource.CurrentPageIndex = 0;
            //_dataSource.PageSize = ItemsPerPage;
            _dataSource.PageSize = data.Rows.Count;
            _dataSource.VirtualCount = countInfo.RecordCount;
            _dataSource.DataSource = data.DefaultView;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE AdjustSelectCommand
        // Strips ORDER-BY clauses from SelectCommand and adds a new one based
        // on SortKeyField
        private void AdjustSelectCommand(bool addCustomSortInfo)
        {
            // Truncate where ORDER BY is found
            string temp = SelectCommand.ToLower();
            int pos = temp.IndexOf("order by");
            if (pos > -1)
                SelectCommand = SelectCommand.Substring(0, pos);

            // Add new ORDER BY info if SortKeyField is specified
            if (SortField != "" && addCustomSortInfo)
                SelectCommand += " ORDER BY " + SortField;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE CalculateVirtualRecordCount
        // Calculates record and page count for the specified query
        private VirtualRecordCount CalculateVirtualRecordCount()
        {
            VirtualRecordCount count = new VirtualRecordCount();

            // Calculate the virtual number of records from the query
            count.RecordCount = GetQueryVirtualCount();
            count.RecordsInLastPage = ItemsPerPage;

            // Calculate the correspondent number of pages
            int lastPage = count.RecordCount / ItemsPerPage;
            int remainder = count.RecordCount % ItemsPerPage;
            if (remainder > 0)
                lastPage++;
            count.PageCount = lastPage;

            // Calculate the number of items in the last page
            if (remainder > 0)
                count.RecordsInLastPage = remainder;
            return count;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE PrepareCommand
        // Prepares and returns the command object for the reader-based query
        private OracleCommand PrepareCommand(VirtualRecordCount countInfo)
        {
            // No sort field specified: figure it out
            if (SortField == "")
            {
                //							// Get metadata for all columns and choose either the primary key
                //							// or the 
                //							//string text = "SET FMTONLY ON;" + SelectCommand + ";SET FMTONLY OFF;";
                //							string text = "SELECT * FROM (" + SelectCommand + ") WHERE 1=2";
                //							OracleDataAdapter adapter = new OracleDataAdapter(text, ConnectionString);
                //							DataTable t = new DataTable();
                //							adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                //							adapter.Fill(t);
                //							DataColumn col = null;
                //							if (t.PrimaryKey.Length >0)
                //								col = t.PrimaryKey[0];
                //							else
                //								col = t.Columns[0];
                //							SortField = col.ColumnName;
            }

            // Determines how many records are to be retrieved.
            // The last page could require less than other pages
            int recsToRetrieve = ItemsPerPage;
            if (CurrentPageIndex == countInfo.PageCount - 1)
                recsToRetrieve = countInfo.RecordsInLastPage;

            //为Oracle准备排序内容
            if (SortField != "")
            {
                SelectCommand += "order by " + SortField;
            }
            string cmdText = String.Format(QueryPageCommandText,
                recsToRetrieve,						// {0} --> page size
                ItemsPerPage * (CurrentPageIndex + 1),	// {1} --> size * index
                SelectCommand,						// {2} --> base query
                SortField,							// {3} --> key field in the query
                "ASC",								// Default to ascending order
                "DESC",
                ItemsPerPage);

            OracleConnection conn = new OracleConnection(ConnectionString);
            OracleCommand cmd = new OracleCommand(cmdText, conn);
            return cmd;
        }

        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE GetQueryVirtualCount
        // Run a query to get the record count
        private int GetQueryVirtualCount()
        {
            string cmdText = String.Format(QueryCountCommandText, SelectCommand);
            OracleConnection conn = new OracleConnection(ConnectionString);
            OracleCommand cmd = new OracleCommand(cmdText, conn);

            cmd.Connection.Open();
            string rec = cmd.ExecuteScalar().ToString();
            int recCount = Convert.ToInt32(rec);
            //			int recCount = Convert.ToInt32(cmd.ExecuteScalar()); 
            cmd.Connection.Close();

            return recCount;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE GoToPage
        // Sets the current page index
        private void GoToPage(int pageIndex)
        {
            // Prepares event data
            PageChangedEventArgs e = new PageChangedEventArgs();
            e.OldPageIndex = CurrentPageIndex;
            e.NewPageIndex = pageIndex;

            // Updates the current index
            CurrentPageIndex = pageIndex;

            // Fires the page changed event
            OnPageIndexChanged(e);

            // Binds new data
            DataBind();
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE first_Click
        // Event handler for the << button
        private void first_Click(object sender, EventArgs e)
        {
            GoToPage(0);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE prev_Click
        // Event handler for the < button
        private void prev_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex - 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE next_Click
        // Event handler for the > button
        private void next_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex + 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE last_Click
        // Event handler for the >> button
        private void last_Click(object sender, EventArgs e)
        {
            GoToPage(TotalPages - 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE PageList_Click
        // Event handler for any page selected from the drop-down page list 
        private void PageList_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.DropDownList pageList = (System.Web.UI.WebControls.DropDownList)sender;
            int pageIndex = Convert.ToInt32(pageList.SelectedItem.Value);
            GoToPage(pageIndex);
        }
        // ***********************************************************************
        #endregion
    }
    #endregion

    #region OleDbPager Control

    [DefaultProperty("SelectCommand")]
    [DefaultEvent("PageIndexChanged")]
    [ToolboxData("<{0}:OleDbPager runat=\"server\" />")]
    public class OleDbPager : WebControl, INamingContainer
    {
        #region  PRIVATE DATA MEMBERS
        // ***********************************************************************
        // PRIVATE members
        private PagedDataSource _dataSource;
        private Control _controlToPaginate;
        private string CacheKeyName
        {
            get { return Page.Request.FilePath + "_" + UniqueID + "_Data"; }
        }
        private string CurrentPageText = "总记录:<font color=#ff0000><B>{2}</B></font>&nbsp&nbsp页次:<font color=#ff0000><B>{0}</B></font>/<B>{1}</B>页&nbsp&nbsp<B>{3}</B>/页";
        private string NoPageSelectedText = "";
        private string QueryPageCommandText = "SELECT * FROM " +
            "(SELECT TOP {0} * FROM " +
            "(SELECT TOP {1} * FROM ({2}) AS t0 ORDER BY {3} {4}) AS t1 " +
            "ORDER BY {3} {5}) AS t2 " +
            "ORDER BY {3}";
        private string QueryCountCommandText = "SELECT COUNT(*) FROM ({0}) AS t0";
        // ***********************************************************************
        #endregion

        #region CTOR(s)
        // ***********************************************************************
        // Ctor
        public OleDbPager()
            : base()
        {
            _dataSource = null;
            _controlToPaginate = null;

            PagingMode = PagingMode.Cached;
            PagerStyle = PagerStyle.NextPrev;
            CurrentPageIndex = 0;
            SelectCommand = "";
            ConnectionString = "";
            ItemsPerPage = 10;
            TotalPages = -1;
            TotalRecord = -1;
            CacheDuration = 60;
        }
        // ***********************************************************************
        #endregion

        #region PUBLIC PROGRAMMING INTERFACE
        // ***********************************************************************
        // METHOD ClearCache
        // Removes any data cached for paging
        public void ClearCache()
        {
            if (PagingMode == PagingMode.Cached)
                Page.Cache.Remove(CacheKeyName);
        }
        // ***********************************************************************

        // ***********************************************************************
        // EVENT PageIndexChanged
        // Fires when the pager is about to switch to a new page
        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);
        public event PageChangedEventHandler PageIndexChanged;
        protected virtual void OnPageIndexChanged(PageChangedEventArgs e)
        {
            if (PageIndexChanged != null)
                PageIndexChanged(this, e);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY CacheDuration
        [Description("Gets and sets for how many seconds the data should stay in the cache")]
        public int CacheDuration
        {
            get { return Convert.ToInt32(ViewState["CacheDuration"]); }
            set { ViewState["CacheDuration"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PagingMode
        [Description("Indicates whether the data are retrieved page by page or can be cached")]
        public PagingMode PagingMode
        {
            get { return (PagingMode)ViewState["PagingMode"]; }
            set { ViewState["PagingMode"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PagerStyle
        [Description("Indicates the style of the pager's navigation bar")]
        public PagerStyle PagerStyle
        {
            get { return (PagerStyle)ViewState["PagerStyle"]; }
            set { ViewState["PagerStyle"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ControlToPaginate
        [Description("Gets and sets the name of the control to paginate")]
        public string ControlToPaginate
        {
            get { return Convert.ToString(ViewState["ControlToPaginate"]); }
            set { ViewState["ControlToPaginate"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ItemsPerPage
        [Description("Gets and sets the number of items to display per page")]
        public int ItemsPerPage
        {
            get { return Convert.ToInt32(ViewState["ItemsPerPage"]); }
            set { ViewState["ItemsPerPage"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY CurrentPageIndex
        [Description("Gets and sets the index of the currently displayed page")]
        public int CurrentPageIndex
        {
            get { return Convert.ToInt32(ViewState["CurrentPageIndex"]); }
            set { ViewState["CurrentPageIndex"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY ConnectionString
        [Description("Gets and sets the connection string to access the database")]
        public string ConnectionString
        {
            get { return Convert.ToString(ViewState["ConnectionString"]); }
            set { ViewState["ConnectionString"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY SelectCommand
        [Description("Gets and sets the SQL query to get data")]
        public string SelectCommand
        {
            get { return Convert.ToString(ViewState["SelectCommand"]); }
            set { ViewState["SelectCommand"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY SortField
        [Description("Gets and sets the sort-by field. It is mandatory in NonCached mode.)")]
        public string SortField
        {
            get { return Convert.ToString(ViewState["SortKeyField"]); }
            set { ViewState["SortKeyField"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY PageCount
        // Gets the number of displayable pages 
        [Browsable(false)]
        public int PageCount
        {
            get { return TotalPages; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY TotalPages
        // Gets and sets the number of pages to display 
        protected int TotalPages
        {
            get { return Convert.ToInt32(ViewState["TotalPages"]); }
            set { ViewState["TotalPages"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // PROPERTY TotalPages
        // Gets and sets the number of pages to display 
        protected int TotalRecord
        {
            get { return Convert.ToInt32(ViewState["TotalRecord"]); }
            set { ViewState["TotalRecord"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE DataBind
        // Fetches and stores the data
        public override void DataBind()
        {
            // Fires the data binding event
            base.DataBind();

            // Controls must be recreated after data binding 
            ChildControlsCreated = false;

            // Ensures the control exists and is a list control
            if (ControlToPaginate == "")
                return;
            _controlToPaginate = Page.FindControl(ControlToPaginate);
            if (_controlToPaginate == null)
                return;
            if (!(_controlToPaginate is BaseDataList || _controlToPaginate is ListControl))
                return;

            // Ensures enough info to connect and query is specified
            if (ConnectionString == "" || SelectCommand == "")
                return;

            // Fetch data
            if (PagingMode == PagingMode.Cached)
                FetchAllData();
            else
            {
                //if (SortField == "")
                //	return;
                FetchPageData();
            }

            // Bind data to the buddy control
            BaseDataList baseDataListControl = null;
            ListControl listControl = null;
            if (_controlToPaginate is BaseDataList)
            {
                baseDataListControl = (BaseDataList)_controlToPaginate;
                baseDataListControl.DataSource = _dataSource;
                baseDataListControl.DataBind();
                return;
            }
            if (_controlToPaginate is ListControl)
            {
                listControl = (ListControl)_controlToPaginate;
                listControl.Items.Clear();
                listControl.DataSource = _dataSource;
                listControl.DataBind();
                return;
            }
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE Render
        // Writes the content to be rendered on the client
        protected override void Render(HtmlTextWriter output)
        {
            // If in design-mode ensure that child controls have been created.
            // Child controls are not created at this time in design-mode because
            // there's no pre-render stage. Do so for composite controls like this 
            if (Site != null && Site.DesignMode)
                CreateChildControls();

            base.Render(output);
        }
        // ***********************************************************************

        // ***********************************************************************
        // OVERRIDE CreateChildControls
        // Outputs the HTML markup for the control
        protected override void CreateChildControls()
        {
            Controls.Clear();
            ClearChildViewState();

            BuildControlHierarchy();
        }
        // ***********************************************************************
        #endregion

        #region PRIVATE HELPER METHODS
        // ***********************************************************************
        // PRIVATE BuildControlHierarchy
        // Control the building of the control's hierarchy
        private void BuildControlHierarchy()
        {
            // Build the surrounding table (one row, two cells)
            Table t = new Table();
            t.Width = new Unit("100%");
            // Build the table row
            TableRow row = new TableRow();
            t.Rows.Add(row);

            // Build the cell with the page index
            TableCell cellPageDesc = new TableCell();
            cellPageDesc.HorizontalAlign = HorizontalAlign.Left;
            BuildCurrentPage(cellPageDesc);
            row.Cells.Add(cellPageDesc);

            // Build the cell with navigation bar
            TableCell cellNavBar = new TableCell();
            if (PagerStyle == PagerStyle.NextPrev)
                BuildNextPrevUI(cellNavBar);
            else
                BuildNumericPagesUI(cellNavBar);
            cellNavBar.HorizontalAlign = HorizontalAlign.Right;
            row.Cells.Add(cellNavBar);

            // Add the table to the control tree
            Controls.Add(t);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildNextPrevUI
        // Generates the HTML markup for the Next/Prev navigation bar
        private void BuildNextPrevUI(TableCell cell)
        {
            bool isValidPage = (CurrentPageIndex >= 0 && CurrentPageIndex <= TotalPages - 1);
            bool canMoveBack = (CurrentPageIndex > 0);
            bool canMoveForward = (CurrentPageIndex < TotalPages - 1);

            // Render the << button
            LinkButton first = new LinkButton();
            first.ID = "First";
            first.CausesValidation = false;
            first.Click += new EventHandler(first_Click);
            first.Text = " 首页 ";
            first.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(first);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the < button
            LinkButton prev = new LinkButton();
            prev.ID = "Prev";
            prev.CausesValidation = false;
            prev.Click += new EventHandler(prev_Click);
            prev.Text = " 上页 ";
            prev.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(prev);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the > button
            LinkButton next = new LinkButton();
            next.ID = "Next";
            next.CausesValidation = false;
            next.Click += new EventHandler(next_Click);
            next.Text = " 下页 ";
            next.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(next);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the >> button
            LinkButton last = new LinkButton();
            last.ID = "Last";
            last.CausesValidation = false;
            last.Click += new EventHandler(last_Click);
            last.Text = " 尾页 ";
            last.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(last);

            // Render a drop-down list  
            System.Web.UI.WebControls.DropDownList pageList = new System.Web.UI.WebControls.DropDownList();
            pageList.ID = "PageList";
            pageList.AutoPostBack = true;
            pageList.SelectedIndexChanged += new EventHandler(PageList_Click);
            pageList.Font.Name = Font.Name;
            pageList.Font.Size = Font.Size;
            pageList.ForeColor = ForeColor;

            // Embellish the list when there are no pages to list 
            if (TotalPages <= 0 || CurrentPageIndex == -1)
            {
                pageList.Items.Add("");
                pageList.Enabled = false;
                pageList.SelectedIndex = 0;
            }
            else // Populate the list
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    ListItem item = new ListItem(i.ToString(), (i - 1).ToString());
                    pageList.Items.Add(item);
                }
                pageList.SelectedIndex = CurrentPageIndex;
            }
            cell.Controls.Add(pageList);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildNumericPagesUI
        // Generates the HTML markup for the Numeric Pages button bar
        private void BuildNumericPagesUI(TableCell cell)
        {
            // Render a drop-down list  
            System.Web.UI.WebControls.DropDownList pageList = new System.Web.UI.WebControls.DropDownList();
            pageList.ID = "PageList";
            pageList.AutoPostBack = true;
            pageList.SelectedIndexChanged += new EventHandler(PageList_Click);
            pageList.Font.Name = Font.Name;
            pageList.Font.Size = Font.Size;
            pageList.ForeColor = ForeColor;

            // Embellish the list when there are no pages to list 
            if (TotalPages <= 0 || CurrentPageIndex == -1)
            {
                pageList.Items.Add("No pages");
                pageList.Enabled = false;
                pageList.SelectedIndex = 0;
            }
            else // Populate the list
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    ListItem item = new ListItem(i.ToString(), (i - 1).ToString());
                    pageList.Items.Add(item);
                }
                pageList.SelectedIndex = CurrentPageIndex;
            }
            cell.Controls.Add(pageList);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE BuildCurrentPage
        // Generates the HTML markup to describe the current page (0-based)
        private void BuildCurrentPage(TableCell cell)
        {
            // Use a standard template: Page X of Y
            if (CurrentPageIndex < 0 || CurrentPageIndex >= TotalPages)
                cell.Text = NoPageSelectedText;
            else
                cell.Text = String.Format(CurrentPageText, (CurrentPageIndex + 1), TotalPages, TotalRecord, ItemsPerPage);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE ValidatePageIndex
        // Ensures the CurrentPageIndex is either valid [0,TotalPages) or -1
        private void ValidatePageIndex()
        {
            if (!(CurrentPageIndex >= 0 && CurrentPageIndex < TotalPages))
                CurrentPageIndex = -1;
            return;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE FetchAllData
        // Runs the query for all data to be paged and caches the resulting data
        private void FetchAllData()
        {
            // Looks for data in the ASP.NET Cache
            DataTable data;
            data = (DataTable)Page.Cache[CacheKeyName];

            if (data == null)
            {
                // Fix SelectCommand with order-by info
                AdjustSelectCommand(true);

                // If data expired or has never been fetched, go to the database
                OleDbDataAdapter adapter = new OleDbDataAdapter(SelectCommand, ConnectionString);
                data = new DataTable();
                adapter.Fill(data);
                Page.Cache.Insert(CacheKeyName, data, null,
                    DateTime.Now.AddSeconds(CacheDuration),
                    System.Web.Caching.Cache.NoSlidingExpiration);
            }

            // Configures the paged data source component
            if (_dataSource == null)
                _dataSource = new PagedDataSource();
            _dataSource.DataSource = data.DefaultView; // must be IEnumerable!
            _dataSource.AllowPaging = true;
            _dataSource.PageSize = ItemsPerPage;
            TotalPages = _dataSource.PageCount;
            TotalRecord = data.Rows.Count;
            // Ensures the page index is valid 
            ValidatePageIndex();
            if (CurrentPageIndex == -1)
            {
                _dataSource = null;
                return;
            }

            // Selects the page to view
            _dataSource.CurrentPageIndex = CurrentPageIndex;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE FetchPageData
        // Runs the query to get only the data that fit into the current page
        private void FetchPageData()
        {
            // Need a validated page index to fetch data.
            // Also need the virtual page count to validate the page index
            AdjustSelectCommand(false);
            VirtualRecordCount countInfo = CalculateVirtualRecordCount();
            TotalRecord = countInfo.RecordCount;
            TotalPages = countInfo.PageCount;
            // Validate the page number (ensures CurrentPageIndex is valid or -1)
            ValidatePageIndex();
            if (CurrentPageIndex == -1)
                return;

            // Prepare and run the command
            OleDbCommand cmd = PrepareCommand(countInfo);
            if (cmd == null)
                return;
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            // Configures the paged data source component
            if (_dataSource == null)
                _dataSource = new PagedDataSource();
            _dataSource.AllowCustomPaging = true;
            _dataSource.AllowPaging = true;
            _dataSource.CurrentPageIndex = 0;
            //_dataSource.PageSize = ItemsPerPage;
            _dataSource.PageSize = data.Rows.Count;
            _dataSource.VirtualCount = countInfo.RecordCount;
            _dataSource.DataSource = data.DefaultView;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE AdjustSelectCommand
        // Strips ORDER-BY clauses from SelectCommand and adds a new one based
        // on SortKeyField
        private void AdjustSelectCommand(bool addCustomSortInfo)
        {
            // Truncate where ORDER BY is found
            string temp = SelectCommand.ToLower();
            int pos = temp.IndexOf("order by");
            if (pos > -1)
                SelectCommand = SelectCommand.Substring(0, pos);

            // Add new ORDER BY info if SortKeyField is specified
            if (SortField != "" && addCustomSortInfo)
                SelectCommand += " ORDER BY " + SortField;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE CalculateVirtualRecordCount
        // Calculates record and page count for the specified query
        private VirtualRecordCount CalculateVirtualRecordCount()
        {
            VirtualRecordCount count = new VirtualRecordCount();

            // Calculate the virtual number of records from the query
            count.RecordCount = GetQueryVirtualCount();
            count.RecordsInLastPage = ItemsPerPage;

            // Calculate the correspondent number of pages
            int lastPage = count.RecordCount / ItemsPerPage;
            int remainder = count.RecordCount % ItemsPerPage;
            if (remainder > 0)
                lastPage++;
            count.PageCount = lastPage;

            // Calculate the number of items in the last page
            if (remainder > 0)
                count.RecordsInLastPage = remainder;
            return count;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE PrepareCommand
        // Prepares and returns the command object for the reader-based query
        private OleDbCommand PrepareCommand(VirtualRecordCount countInfo)
        {
            // No sort field specified: figure it out
            if (SortField == "")
            {
                // Get metadata for all columns and choose either the primary key
                // or the 
                string text = "SET FMTONLY ON;" + SelectCommand + ";SET FMTONLY OFF;";
                OleDbDataAdapter adapter = new OleDbDataAdapter(text, ConnectionString);
                DataTable t = new DataTable();
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(t);
                DataColumn col = null;
                if (t.PrimaryKey.Length > 0)
                    col = t.PrimaryKey[0];
                else
                    col = t.Columns[0];
                SortField = col.ColumnName;
            }

            // Determines how many records are to be retrieved.
            // The last page could require less than other pages
            int recsToRetrieve = ItemsPerPage;
            if (CurrentPageIndex == countInfo.PageCount - 1)
                recsToRetrieve = countInfo.RecordsInLastPage;

            string cmdText = String.Format(QueryPageCommandText,
                recsToRetrieve,						// {0} --> page size
                ItemsPerPage * (CurrentPageIndex + 1),	// {1} --> size * index
                SelectCommand,						// {2} --> base query
                SortField,							// {3} --> key field in the query
                "ASC",								// Default to ascending order
                "DESC");

            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);
            return cmd;
        }

        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE GetQueryVirtualCount
        // Run a query to get the record count
        private int GetQueryVirtualCount()
        {
            string cmdText = String.Format(QueryCountCommandText, SelectCommand);
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);

            cmd.Connection.Open();
            int recCount = (int)cmd.ExecuteScalar();
            cmd.Connection.Close();

            return recCount;
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE GoToPage
        // Sets the current page index
        private void GoToPage(int pageIndex)
        {
            // Prepares event data
            PageChangedEventArgs e = new PageChangedEventArgs();
            e.OldPageIndex = CurrentPageIndex;
            e.NewPageIndex = pageIndex;

            // Updates the current index
            CurrentPageIndex = pageIndex;

            // Fires the page changed event
            OnPageIndexChanged(e);

            // Binds new data
            DataBind();
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE first_Click
        // Event handler for the << button
        private void first_Click(object sender, EventArgs e)
        {
            GoToPage(0);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE prev_Click
        // Event handler for the < button
        private void prev_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex - 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE next_Click
        // Event handler for the > button
        private void next_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex + 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE last_Click
        // Event handler for the >> button
        private void last_Click(object sender, EventArgs e)
        {
            GoToPage(TotalPages - 1);
        }
        // ***********************************************************************

        // ***********************************************************************
        // PRIVATE PageList_Click
        // Event handler for any page selected from the drop-down page list 
        private void PageList_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.DropDownList pageList = (System.Web.UI.WebControls.DropDownList)sender;
            int pageIndex = Convert.ToInt32(pageList.SelectedItem.Value);
            GoToPage(pageIndex);
        }
        // ***********************************************************************
        #endregion
    }
    #endregion
}
