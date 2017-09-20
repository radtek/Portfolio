using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Text;

namespace Johnny.Controls.Web
{
    #region PagingMode enum
    public enum PagingMode
    {
        Cached,
        NonCached
    }
    #endregion

    #region PagerStyle enum
    public enum NavPagerStyle
    {
        PrevNext,
        Numeric
    }
    #endregion

    #region PageChangedEventArgs class
    public class PageChangedEventArgs : EventArgs
    {
        public int OldPageIndex;
        public int NewPageIndex;
    }
    #endregion

    #region ManageClickEventArgs class
    public class ManageClickEventArgs : EventArgs
    {
        public int Id;
    }
    #endregion

    /// <summary>
    /// Summary description for DataGrid.
    /// </summary>
    [DefaultProperty("Text"),
    ToolboxData("<{0}:ManageGrid runat=server></{0}:ManageGrid>")]
    [DefaultEvent("PageSelectedChanged")]
    public class ManageGrid : System.Web.UI.WebControls.DataGrid, INamingContainer
    {
        private string CurrentPageText = "Total:<font color=#ff0000><B>{2}</B></font>&nbsp&nbspNo.:<font color=#ff0000><B>{0}</B></font>/<B>{1}</B>&nbsp&nbsp<B>{3}</B>/Page";
        private string NoPageSelectedText = "";
        private IList dataSource;
        
        public ManageGrid()
            : base()
        {
            NavPagerStyle = NavPagerStyle.PrevNext;
            NoncePageIndex = 0;
            ItemsPerPage = 10;
            TotalPages = -1;
            TotalRecord = -1;
            this.Init += new System.EventHandler(this.DataGrid_Init);
        }

        #region Initialize
        private void DataGrid_Init(object sender, EventArgs e)
        {
            this.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid_ItemDataBound);
        }

        /// <summary>
        /// DataGrid_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //删除确认            
                LinkButton delBttn = (LinkButton)e.Item.Cells[this.Columns.Count - 1].Controls[0];
                delBttn.Attributes.Add("onclick", "javascript:return confirm('Are you sure to delete [ " + e.Item.Cells[0].Text + " ]?');");

                //交替色
                if (base.Items.Count % 2 == 0)
                {
                    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" + GetColorName(NormalBackColor) + "';");
                }
                else
                {
                    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" + GetColorName(AlternatingBackColor) + "';");
                   
                }

                //高亮色
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='" + GetColorName(HighlightColor) + "';");

                ////是否显示
                //Label lblIsDisplay = (Label)e.Item.Cells[this.Columns.Count - 3].FindControl("lblIsDisplay");
                //e.Item.FindControl("lblIsDisplay").Visible = false;
                //LinkButton lnkIsDisplay = (LinkButton)e.Item.FindControl("lnkbSetIsDisplay");
                //lnkIsDisplay.CommandName = "SetDisplay";
                //bool isDisplay = Convert.ToBoolean(lblIsDisplay.Text);
                //if (isDisplay)
                //    lnkIsDisplay.Text = "<font color=\"#009900\"><b>√</b></font>";
                //else
                //    lnkIsDisplay.Text = "<font color=\"#FF0000\"><b>×</b></font>";                
            }

        }

        #endregion

        #region Define PageSelectedChanged Event
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
        ///Outputs the HTML markup for the control 
        /// </summary>
        protected override void CreateChildControls()
        {
            Controls.Clear();

            ClearChildViewState();

            base.CreateChildControls();            

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
            int start = NoncePageIndex * ItemsPerPage;
            int size = Math.Min(ItemsPerPage, TotalRecord - start);
            TotalPages = (TotalRecord - 1) / ItemsPerPage + 1;

            IList page = new ArrayList();

            //Add the relevant items from the datasource
            for (int i = 0; i < size; i++)
                page.Add(dataSource[start + i]);

            //set the base objects datasource
            base.DataSource = page;
            base.OnDataBinding(e);
            //Controls.Clear();
            BuildPagerControl();
        }
        #endregion                

        #region ManageGrid Properties

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

        #region NormalBackColor
        /// <summary>
        /// The normal grid item backcolor.
        /// </summary>
        public Color NormalBackColor
        {
            get
            {
                object o = ViewState["NormalBackColor"];

                if (o != null)
                    return (Color)o;
                else
                    return Color.Empty;
            }
            set
            {
                ViewState["NormalBackColor"] = value;
            }
        }
        #endregion

        #region AlternatingBackColor
        /// <summary>
        /// The alternating grid item backcolor.
        /// </summary>
        public Color AlternatingBackColor
        {
            get
            {
                object o = ViewState["AlternatingBackColor"];

                if (o != null)
                    return (Color)o;
                else
                    return Color.Empty;
            }
            set
            {
                ViewState["AlternatingBackColor"] = value;
            }
        }
        #endregion

        #region HighlightColor
        /// <summary>
        /// The color when mouse move over
        /// </summary>
        public Color HighlightColor
        {
            get
            {
                object o = ViewState["HighlightColor"];

                if (o != null)
                    return (Color)o;
                else
                    return Color.Empty;
            }
            set
            {
                ViewState["HighlightColor"] = value;
            }
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
            if (NoncePageIndex < 0 || NoncePageIndex >= TotalPages)
                cell.Text = NoPageSelectedText;
            else
                cell.Text = String.Format(CurrentPageText, (NoncePageIndex + 1), TotalPages, TotalRecord, ItemsPerPage);
        }
        #endregion

        #region BuildNextPrevUI
        /// <summary>
        /// Generates the HTML markup for the Next/Prev navigation bar 
        /// </summary>
        /// <param name="cell"></param>
        private void BuildNextPrevUI(TableCell cell)
        {
            bool isValidPage = (NoncePageIndex >= 0 && NoncePageIndex <= TotalPages - 1);
            bool canMoveBack = (NoncePageIndex > 0);
            bool canMoveForward = (NoncePageIndex < TotalPages - 1);

            // Render the << button
            LinkButton first = new LinkButton();
            first.ID = "First";
            first.CausesValidation = false;
            first.Click += new EventHandler(first_Click);
            first.Text = " First ";
            first.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(first);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the < button
            LinkButton prev = new LinkButton();
            prev.ID = "Prev";
            prev.CausesValidation = false;
            prev.Click += new EventHandler(prev_Click);
            prev.Text = " Previous ";
            prev.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(prev);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the > button
            LinkButton next = new LinkButton();
            next.ID = "Next";
            next.CausesValidation = false;
            next.Click += new EventHandler(next_Click);
            next.Text = " Next ";
            next.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(next);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the >> button
            LinkButton last = new LinkButton();
            last.ID = "Last";
            last.CausesValidation = false;
            last.Click += new EventHandler(last_Click);
            last.Text = " Last ";
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
            if (TotalPages <= 0 || NoncePageIndex == -1)
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
                pageList.SelectedIndex = NoncePageIndex;
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
            pageList.Font.Name = Font.Name;
            pageList.Font.Size = Font.Size;
            pageList.ForeColor = ForeColor;

            // Embellish the list when there are no pages to list 
            if (TotalPages <= 0 || NoncePageIndex == -1)
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
                pageList.SelectedIndex = NoncePageIndex;
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
            GoToPage(NoncePageIndex - 1);
        }
        
        /// <summary>
        /// Event handler for the > button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Click(object sender, EventArgs e)
        {
            GoToPage(NoncePageIndex + 1);
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
            e.OldPageIndex = NoncePageIndex;
            e.NewPageIndex = pageIndex;

            // Updates the current index
            NoncePageIndex = pageIndex;

            // Fires the page changed event
            OnPageSelectedChanged(e);

            // Binds new data
            OnDataBinding(e);
        }

        #endregion

        #region GetColorName
        /// <summary>
        /// get color
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private string GetColorName(Color c)
        {           
            string ret = "";

            if (c.IsEmpty)
            {
                ret = Color.Empty.ToString();
            }
            else if (c.IsKnownColor || c.IsNamedColor || c.IsSystemColor)
            {
                ret = c.Name.ToString();
            }
            else
            {
                // ffEBF3FD -> #EBF3FD
                ret = "#" + c.Name.Substring(2, c.Name.Length - 2);
            }

            return ret;
        }
        #endregion

        #endregion

    }
}