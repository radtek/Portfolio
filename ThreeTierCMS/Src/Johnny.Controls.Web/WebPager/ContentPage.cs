using System;
using System.IO;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Johnny.Controls.Web.WebPager
{
    //#region PagingMode enum
    //public enum PagingMode
    //{
    //    Cached,
    //    NonCached
    //}
    //#endregion

    //#region PageChangedEventArgs class
    //public class PageChangedEventArgs : EventArgs
    //{
    //    public int OldPageIndex;
    //    public int NewPageIndex;
    //}
    //#endregion

    #region ContentChangedEventArgs class
    public class ContentChangedEventArgs : EventArgs
    {
        public string Content;
    }
    #endregion

    #region ContentPager Control
    [DefaultEvent("PageIndexChanged")]
    [ToolboxData("<{0}:ContentPager runat=\"server\" />")]
    public class ContentPager : WebControl, INamingContainer
    {
        #region  PRIVATE DATA MEMBERS
        // ***********************************************************************
        // PRIVATE members
        private string CacheKeyName
        {
            get { return Page.Request.FilePath + "_" + UniqueID + "_Data"; }
        }
        // ***********************************************************************
        #endregion

        #region CTOR(s)
        // ***********************************************************************
        // Ctor
        public ContentPager()
            : base()
        {
            PagingMode = PagingMode.Cached;
            CurrentPageIndex = 0;
            TotalPages = -1;
            SourceContent = "";
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
        // EVENT SourceContentChanged
        // Fires when the content is about to change
        public delegate void ContentChangedEventHandler(object sender, ContentChangedEventArgs e);
        public event ContentChangedEventHandler SourceContentChanged;
        protected virtual void OnSourceContentChanged(ContentChangedEventArgs e)
        {
            if (SourceContentChanged != null)
                SourceContentChanged(this, e);
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
        // PROPERTY SourceContent
        [Description("Gets and sets the source content")]
        public string SourceContent
        {
            get { return Convert.ToString(ViewState["SourceContent"]); }
            set { ViewState["SourceContent"] = value; }
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
        // PROPERTY TotalPages
        // Gets and sets the number of pages to display 
        protected int TotalPages
        {
            get { return Convert.ToInt32(ViewState["TotalPages"]); }
            set { ViewState["TotalPages"] = value; }
        }
        // ***********************************************************************

        // ***********************************************************************
        // Fetches and stores the content
        public void GetContent()
        {
            // Controls must be recreated after content change 
            ChildControlsCreated = false;

            string[] result = Regex.Split(SourceContent, @"\[page\]", RegexOptions.IgnoreCase);
            TotalPages = result.Length;

            bool isValidPage = (CurrentPageIndex >= 0 && CurrentPageIndex <= TotalPages - 1);
            // Prepares event data
            ContentChangedEventArgs es = new ContentChangedEventArgs();
            if (isValidPage) es.Content = result[CurrentPageIndex];
            else es.Content = "";

            // Fires the content changed event
            OnSourceContentChanged(es);
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
            //			// Build the surrounding table (one row, two cells)
            //			Table t = new Table();
            //			t.Width = new Unit("100%");
            //			// Build the table row
            //			TableRow row = new TableRow();
            //			t.Rows.Add(row);
            //
            //			// Build the cell with the page index
            //			TableCell cellPageDesc = new TableCell();
            //			cellPageDesc.HorizontalAlign = HorizontalAlign.Left;
            //			row.Cells.Add(cellPageDesc);
            //
            //			// Build the cell with navigation bar
            //			TableCell cellNavBar = new TableCell();
            //			BuildNumericListUI(cellNavBar);
            //			row.Cells.Add(cellNavBar);
            //
            //			// Add the table to the control tree
            //			Controls.Add(t);

            // Build the surrounding table (one row, two cells)
            Table t = new Table();
            t.Width = 263;
            t.BorderWidth = 0;
            t.HorizontalAlign = HorizontalAlign.Right;
            t.CellPadding = 0;
            t.CellSpacing = 0;
            t.BackImageUrl = "images/GlobalNews/bg_ym.gif";

            // Build the table row
            TableRow row = new TableRow();
            t.Rows.Add(row);

            // Build the cell with navigation bar
            TableCell cellNavBar = new TableCell();
            cellNavBar.Height = 22;
            cellNavBar.HorizontalAlign = HorizontalAlign.Center;
            cellNavBar.CssClass = "content_cn_12px_b";
            BuildNumericListUI(cellNavBar);
            row.Cells.Add(cellNavBar);

            // Add the table to the control tree
            Controls.Add(t);
        }
        // ***********************************************************************

        // ***********************************************************************
        private void BuildNumericListUI(TableCell cell)
        {
            bool isValidPage = (CurrentPageIndex >= 0 && CurrentPageIndex <= TotalPages - 1);
            bool canMoveBack = (CurrentPageIndex > 0);
            bool canMoveForward = (CurrentPageIndex < TotalPages - 1);

            // Render the < button
            LinkButton prev = new LinkButton();
            prev.ID = "Prev";
            prev.ForeColor = Color.White;
            prev.CausesValidation = false;
            prev.Click += new EventHandler(prev_Click);
            prev.Text = " 上一页 ";
            prev.Enabled = isValidPage && canMoveBack;
            cell.Controls.Add(prev);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            for (int ix = 0; ix < TotalPages; ix++)
            {
                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn_" + ix.ToString();
                lbtn.ForeColor = Color.White;
                lbtn.Click += new EventHandler(lbtn_Click);
                //lbtn.CssClass = "content_cn_12px_b";
                if (ix == CurrentPageIndex)
                {
                    lbtn.ForeColor = Color.Red;
                    lbtn.Enabled = false;
                }
                else
                {
                    lbtn.ForeColor = ForeColor;
                    lbtn.Enabled = true;
                }
                lbtn.ToolTip = " 第" + (ix + 1).ToString() + "页 ";
                lbtn.Text = " " + (ix + 1).ToString() + " ";
                lbtn.Style.Add("TEXT-DECORATION", "none");

                cell.Controls.Add(lbtn);
            }

            // Render the > button
            LinkButton next = new LinkButton();
            next.ID = "Next";
            next.ForeColor = Color.White;
            next.CausesValidation = false;
            next.Click += new EventHandler(next_Click);
            next.Text = " 下一页 ";
            next.Enabled = isValidPage && canMoveForward;
            cell.Controls.Add(next);
        }

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

            //get current content
            GetContent();

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
        private void lbtn_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = int.Parse((sender as LinkButton).Text) - 1;
            GoToPage(CurrentPageIndex);
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
        #endregion
    }
    #endregion
}