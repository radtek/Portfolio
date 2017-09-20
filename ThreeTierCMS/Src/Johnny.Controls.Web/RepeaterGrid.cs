using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

namespace Johnny.Controls.Web
{
    //#region PagingMode enum
    //public enum PagingMode
    //{
    //    Cached,
    //    NonCached
    //}
    //#endregion

    //#region PagerStyle enum
    //public enum NavPagerStyle
    //{
    //    PrevNext,
    //    Numeric
    //}
    //#endregion

    //#region PageChangedEventArgs class
    //public class PageChangedEventArgs : EventArgs
    //{
    //    public int OldPageIndex;
    //    public int NewPageIndex;
    //}
    //#endregion

    /// <summary>
    /// Summary description for DataGrid.
    /// </summary>
    [DefaultProperty("Text"),
    ToolboxData("<{0}:RepeaterGrid runat=server></{0}:RepeaterGrid>")]
    [DefaultEvent("PageIndexChanged")]
    public class RepeaterGrid : System.Web.UI.WebControls.Repeater , INamingContainer
    {
        private string CurrentPageText = "总记录:<font color=#ff0000><B>{2}</B></font>&nbsp&nbsp页次:<font color=#ff0000><B>{0}</B></font>/<B>{1}</B>页&nbsp&nbsp<B>{3}</B>/页";
        private string NoPageSelectedText = "";
        private IList dataSource;
        private int currentPageIndex;

        virtual public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set { currentPageIndex = value; }
        }

        public RepeaterGrid()
            : base()
        {
            NavPagerStyle = NavPagerStyle.PrevNext;
            CurrentPageIndex = 0;
            ItemsPerPage = 10;
            TotalPages = -1;
            TotalRecord = -1;
            //this.Init += new System.EventHandler(this.DataGrid_Init);
        }

        #region Initialize
        private void DataGrid_Init(object sender, EventArgs e)
        {
            //this.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventArgs (this.DataGrid_ItemDataBound);
        }

        /// <summary>
        /// DataGrid_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    //删除确认            
            //    LinkButton delBttn = (LinkButton)e.Item.Cells[this.Columns.Count - 1].Controls[0];
            //    delBttn.Attributes.Add("onclick", "javascript:return confirm('确定删除[ " + e.Item.Cells[0].Text + " ]?');");
            //}
        }
        #endregion

        #region Define MenuItemClick Event
        // EVENT PageSelectedChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);

        /// <summary>
        /// Fires when the pager is about to switch to a new page
        /// </summary>
        public event PageChangedEventHandler PageSelectedChanged;

        /// <summary>
        /// Raises the PageSelectedChanged event.  This allows you to provide a custom handler for the event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPageSelectedChanged(PageChangedEventArgs e)
        {
            if (PageSelectedChanged != null)
                PageSelectedChanged(this, e);
        }
        #endregion

        #region Override methods and properties
        /// <summary>
        /// DataSource,treate value as IList
        /// </summary>
        public override object DataSource
        {
            set
            {
                //This try catch block is to avoid issues with the VS.NET designer
                //The designer will try and bind a datasource which does not derive from ILIST
                try
                {
                    dataSource = (IList)value;
                    TotalRecord = dataSource.Count;
                }
                catch
                {
                    dataSource = null;
                    TotalRecord = 0;
                }
            }
        }

        /// <summary>
        /// Writes the content to be rendered on the client
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            // If in design-mode ensure that child controls have been created.
            // Child controls are not created at this time in design-mode because
            // there's no pre-render stage. Do so for composite controls like this 
            //if (Site != null && Site.DesignMode)
            //CreateChildControls();
            //base.DataBind();
            if (TotalPages > 0)
            {
                BuildPagerControl();
            }
            base.Render(writer);

        }

        /// <summary>
        ///Outputs the HTML markup for the control 
        /// </summary>
        protected override void CreateChildControls()
        {
            //Controls.Clear();

            ClearChildViewState();
            if (TotalPages > 0)
            {
                BuildPagerControl();
            }
        }

        /// <summary>
        /// Occurs when databind
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDataBinding(EventArgs e)
        {

            //Work out which items we want to render to the page
            int start = CurrentPageIndex * ItemsPerPage;
            int size = Math.Min(ItemsPerPage, TotalRecord - start);
            TotalPages = (TotalRecord - 1) / ItemsPerPage + 1;

            IList page = new ArrayList();

            //Add the relevant items from the datasource
            for (int i = 0; i < size; i++)
                page.Add(dataSource[start + i]);

            //set the base objects datasource
            base.DataSource = page;
            base.OnDataBinding(e);
        }
        #endregion

        #region RepeaterGrid Properties

        #region NavPagerStyle
        /// <summary>
        /// The style of the navigation bar
        /// </summary>
        [Description("Indicates the style of the pager's navigation bar")]
        public NavPagerStyle NavPagerStyle
        {
            get { return (NavPagerStyle)ViewState["NavPagerStyle"]; }
            set { ViewState["NavPagerStyle"] = value; }
        }
        #endregion

        #region ItemsPerPage
        /// <summary>
        /// The record count of one page
        /// </summary>
        [Description("Gets and sets the number of items to display per page")]
        public int ItemsPerPage
        {
            get { return Convert.ToInt32(ViewState["ItemsPerPage"]); }
            set { ViewState["ItemsPerPage"] = value; }
        }
        #endregion

        #region NoncePageIndex
        /// <summary>
        /// Current page index
        /// </summary>
        [Description("Gets and sets the index of the currently displayed page")]
        public int NoncePageIndex
        {
            get { return Convert.ToInt32(ViewState["NoncePageIndex"]); }
            set { ViewState["NoncePageIndex"] = value; }
        }
        #endregion

        #region TotalPages
        /// <summary>
        /// The number of pages to display  
        /// </summary>
        protected int TotalPages
        {
            get { return Convert.ToInt32(ViewState["TotalPages"]); }
            set { ViewState["TotalPages"] = value; }
        }
        #endregion

        #region TotalRecord
        /// <summary>
        /// The count of the total records.
        /// </summary>
        protected int TotalRecord
        {
            get { return Convert.ToInt32(ViewState["TotalRecord"]); }
            set { ViewState["TotalRecord"] = value; }
        }
        #endregion

        #endregion

        #region Private methods

        #region BuildPagerControl
        /// <summary>
        /// Build the Pager control
        /// </summary>
        private void BuildPagerControl()
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
            if (NavPagerStyle == NavPagerStyle.PrevNext)
                BuildNextPrevUI(cellNavBar);
            else
                BuildNumericPagesUI(cellNavBar);
            cellNavBar.HorizontalAlign = HorizontalAlign.Right;
            row.Cells.Add(cellNavBar);

            // Add the table to the control tree
            Controls.Add(t);
        }
        #endregion

        #region BuildCurrentPage
        /// <summary>
        /// Generates the HTML markup to describe the current page (0-based)
        /// </summary>
        /// <param name="cell">TableCell</param>
        private void BuildCurrentPage(TableCell cell)
        {
            // Use a standard template: Page X of Y
            if (CurrentPageIndex < 0 || CurrentPageIndex >= TotalPages)
                cell.Text = NoPageSelectedText;
            else
                cell.Text = String.Format(CurrentPageText, (CurrentPageIndex + 1), TotalPages, TotalRecord, ItemsPerPage);
        }
        #endregion

        #region BuildNextPrevUI
        /// <summary>
        /// Generates the HTML markup for the Next/Prev navigation bar 
        /// </summary>
        /// <param name="cell"></param>
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
            //pageList.Font.Name = Font.Name;
            //pageList.Font.Size = Font.Size;
            //pageList.ForeColor = ForeColor;

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
        #endregion

        #region BuildNumericPagesUI
        /// <summary>
        /// Generates the HTML markup for the Numeric Pages button bar 
        /// </summary>
        /// <param name="cell"></param>
        private void BuildNumericPagesUI(TableCell cell)
        {
            // Render a drop-down list  
            System.Web.UI.WebControls.DropDownList pageList = new System.Web.UI.WebControls.DropDownList();
            pageList.ID = "PageList";
            pageList.AutoPostBack = true;
            pageList.SelectedIndexChanged += new EventHandler(PageList_Click);
            //pageList.Font.Name = Font.Name;
            //pageList.Font.Size = Font.Size;
            //pageList.ForeColor = ForeColor;

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
        #endregion

        #region Page Change Event
        /// <summary>
        /// Event handler for the << button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void first_Click(object sender, EventArgs e)
        {
            GoToPage(0);
        }

        /// <summary>
        /// Event handler for the < button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prev_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex - 1);
        }

        /// <summary>
        /// Event handler for the > button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentPageIndex + 1);
        }

        /// <summary>
        /// Event handler for the >> button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void last_Click(object sender, EventArgs e)
        {
            GoToPage(TotalPages - 1);
        }

        /// <summary>
        /// Event handler for any page selected from the drop-down page list  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageList_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.DropDownList pageList = (System.Web.UI.WebControls.DropDownList)sender;
            int pageIndex = Convert.ToInt32(pageList.SelectedItem.Value);
            GoToPage(pageIndex);
        }

        /// <summary>
        /// Sets the current page index 
        /// </summary>
        /// <param name="pageIndex"></param>
        private void GoToPage(int pageIndex)
        {
            // Prepares event data
            PageChangedEventArgs e = new PageChangedEventArgs();
            e.OldPageIndex = CurrentPageIndex;
            e.NewPageIndex = pageIndex;

            // Updates the current index
            CurrentPageIndex = pageIndex;

            // Fires the page changed event
            OnPageSelectedChanged(e);

            // Binds new data
            OnDataBinding(e);
        }

        #endregion

        #endregion

    }
}