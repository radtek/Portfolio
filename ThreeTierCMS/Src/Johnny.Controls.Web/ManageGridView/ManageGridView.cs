using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;

using Johnny.Component.Globalization;

namespace Johnny.Controls.Web.ManageGridView
{

    #region PagingStyle enum
    public enum PagingStyle
    {
        PrevNext,
        Numeric
    }
    #endregion

    [DefaultProperty("PageSize"),
    ToolboxData("<{0}:ManageGridView runat=server></{0}:ManageGridView>")]
    public class ManageGridView : GridView
    {
        private int RecordCount = 0;
        System.Web.UI.WebControls.TextBox pagenum = new System.Web.UI.WebControls.TextBox();

        #region CTOR(s)
        public ManageGridView()
        {
            //this.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //this.HeaderStyle.Font.Bold = true;
            //this.PagerStyle.HorizontalAlign = HorizontalAlign.Left;
            //this.BorderStyle = BorderStyle.Solid;
            //this.BorderWidth = Unit.Pixel(1);
            //this.CellPadding = 4;
            //this.AutoGenerateColumns = false;
            //this.Style.Add("margin-left", "2px");
            //this.PageSize = 10;
            //this.Width = Unit.Percentage(99);            
            PagingStyle = PagingStyle.PrevNext;
            ViewState["IsFirst"] = true;
        }
        #endregion

        #region Overriden Control Methods

        protected override void OnInit(EventArgs e)
        {            
            this.Columns[1].HeaderText = GlobalizationUtility.GetLabelText("ManageGridView_ID");
            base.OnInit(e);
        }

        #region DataSource
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
                if (value != null)
                {
                    if (DataSource is DataSet)
                        RecordCount = ((DataSet)DataSource).Tables[0].Rows.Count;
                    if (DataSource is ICollection)
                        RecordCount = ((ICollection)DataSource).Count;
                    if (DataSource is DataTable)
                        RecordCount = ((DataTable)DataSource).Rows.Count;
                    if (DataSource is DataView)
                        RecordCount = ((DataView)DataSource).Table.Rows.Count;
                }

            }
        }
        #endregion

        #region CreateChildControls
        protected override void CreateChildControls()
        {
            if (Convert.ToBoolean(ViewState["IsFirst"]))
            {
                //Insert Move Up Column
                TemplateField tempMoveUp = new TemplateField();
                tempMoveUp.HeaderText = WebControlLocalization.GetText("ManageGridView_MoveUp");
                tempMoveUp.HeaderStyle.Width = new Unit("50");
                Columns.Add(tempMoveUp);

                //Insert Move Down Column
                TemplateField tempMoveDown = new TemplateField();
                tempMoveDown.HeaderText = WebControlLocalization.GetText("ManageGridView_MoveDown");
                tempMoveDown.HeaderStyle.Width = new Unit("50");
                Columns.Add(tempMoveDown);

                //Insert Operation Column
                TemplateField tempOperation = new TemplateField();
                tempOperation.HeaderText = WebControlLocalization.GetText("ManageGridView_Operation");
                tempOperation.HeaderStyle.Width = new Unit("100");
                Columns.Add(tempOperation);


                ViewState["IsFirst"] = false;
            }
            base.CreateChildControls();
        }
        #endregion

        #region OnRowDataBound
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Alternating BackColor when mouse moving
                if (base.Rows.Count % 2 == 0)
                {
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='" + GetColorName(this.RowStyle.BackColor) + "';");
                }
                else
                {
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='" + GetColorName(this.AlternatingRowStyle.BackColor) + "';");

                }

                //Highlight Color
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='" + GetColorName(this.SelectedRowStyle.BackColor) + "';");

                //Confirm delete
                if (e.Row.Cells[this.Columns.Count - 1].Controls.Count > 0)
                {
                    //do nothing if in design mode, otherwise vs cannot render the control. The code below only excute in running time.
                    //Site != null && !Site.DesignMode ||
                    if (!this.DesignMode)
                    {
                        Label lblId = (Label)e.Row.FindControl("lblId");
                        if (lblId != null)
                        {
                            HyperLink btnEdit = (HyperLink)e.Row.Cells[this.Columns.Count - 1].FindControl("hyperLinkEdit");
                            if (btnEdit != null)
                            {
                                btnEdit.NavigateUrl = GetNavigationPageName() + "?action=modify&id=" + lblId.Text;
                            }
                            ImageButton btnDelete = (ImageButton)e.Row.Cells[this.Columns.Count - 1].FindControl("ImageButtonDelete");
                            if (btnDelete != null)
                            {
                                btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + String.Format(WebControlLocalization.GetText("ManageGridView_DeleteConfirmation"), lblId.Text) + "');");
                            }
                        }
                    }
                }
            }

            base.OnRowDataBound(e);
        }
        #endregion

        #region OnRowCreated
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //DisplaySortOrderImages(SortExpressionCust, e.Row);
                //this.CreateRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);

            }
            if (this.AllowPaging)
            {
                IEnumerable dataSource = this.DataSource as IEnumerable;
                if (dataSource != null)
                {
                    IEnumerator iEnum = dataSource.GetEnumerator();
                    int i = 0;
                    while (iEnum.MoveNext())
                    {
                        i++;
                    }
                    this.Page.Visible = (i > 0);
                }
            }

            //固定表头 ziyan 2006-12-16
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    foreach (TableCell cell in e.Row.Cells)
            //    {
            //        cell.CssClass = "fhc";
            //    }
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //insert ImageButton for move up
                ImageButton imgbtnup = new ImageButton();
                imgbtnup.ID = "ImageButtonUp";
                imgbtnup.ImageUrl = "../images/gridview/up.gif";
                imgbtnup.ToolTip = WebControlLocalization.GetText("ManageGridView_MoveUp");
                imgbtnup.CommandName = "MoveUp";
                imgbtnup.CommandArgument = (this.Rows.Count).ToString();
                e.Row.Cells[this.Columns.Count - 3].Controls.Add(imgbtnup);

                //insert ImageButton for move up
                ImageButton imgbtndown = new ImageButton();
                imgbtndown.ID = "ImageButtonDown";
                imgbtndown.ImageUrl = "../images/gridview/down.gif";
                imgbtndown.ToolTip = WebControlLocalization.GetText("ManageGridView_Down");
                imgbtndown.CommandName = "MoveDown";
                imgbtndown.CommandArgument = (this.Rows.Count).ToString();
                e.Row.Cells[this.Columns.Count - 2].Controls.Add(imgbtndown);

                //insert ImageButton for edit
                HyperLink link = new HyperLink();
                link.ID = "hyperLinkEdit";
                link.ImageUrl = "../images/gridview/edit.gif";
                link.ToolTip = WebControlLocalization.GetText("ManageGridView_Modify");
                e.Row.Cells[this.Columns.Count - 1].Controls.Add(link);

                System.Web.UI.WebControls.Literal space = new System.Web.UI.WebControls.Literal();
                space.Text = "&nbsp;&nbsp;";
                e.Row.Cells[this.Columns.Count - 1].Controls.Add(space);
                //insert ImageButton for delete
                ImageButton imgbtndelete = new ImageButton();
                imgbtndelete.ID = "ImageButtonDelete";
                imgbtndelete.ImageUrl = "../images/gridview/delete.gif";
                imgbtndelete.ToolTip = WebControlLocalization.GetText("ManageGridView_Delete");
                imgbtndelete.CommandName = "Delete";
                imgbtndelete.CommandArgument = (this.Rows.Count).ToString();
                e.Row.Cells[this.Columns.Count - 1].Controls.Add(imgbtndelete);
            }
            else if (e.Row.RowType == DataControlRowType.Pager && this.AllowPaging)
            {
                TableCell tc = new TableCell();
                e.Row.Controls.Clear();

                Table t = new Table();

                t.Width = Unit.Percentage(100);
                t.CellPadding = 0;
                t.CellSpacing = 0;
                // Build the table row
                TableRow row = new TableRow();
                t.Rows.Add(row);

                // Build the cell with the page index
                TableCell cellPageDesc = new TableCell();
                cellPageDesc.Style.Add("border", "0");
                cellPageDesc.Style.Add("text-align", "left");
                BuildCurrentPage(cellPageDesc);
                row.Cells.Add(cellPageDesc);

                // Build the cell with navigation bar
                TableCell cellNavBar = new TableCell();
                if (PagingStyle == PagingStyle.PrevNext)
                    BuildNextPrevUI(cellNavBar);
                else
                    BuildNumericPagesUI(cellNavBar);                
                cellNavBar.Style.Add("border", "0");
                cellNavBar.Style.Add("text-align", "right");
                row.Cells.Add(cellNavBar);

                tc.ColumnSpan = this.Columns.Count;
                tc.Controls.Add(t);
                // Add the table to the control tree
                e.Row.Controls.Add(tc);                
            }
            base.OnRowCreated(e);
        }
        #endregion

        #endregion

        #region Private Methods

        #region BuildCurrentPage
        /// <summary>
        /// Generates the HTML markup to describe the current page (0-based)
        /// </summary>
        /// <param name="cell">TableCell</param>
        private void BuildCurrentPage(TableCell cell)
        {
            // Use a standard template: Page X of Y
            if (PageIndex < 0 || PageIndex >= PageCount)
                cell.Text = EmptyDataText;
            else
                cell.Text = String.Format(WebControlLocalization.GetText("ManageGridView_CurrentPage"), (PageIndex + 1), PageCount, RecordCount, PageSize);
        }
        #endregion

        #region BuildNextPrevUI
        /// <summary>
        /// Generates the HTML markup for the Next/Prev navigation bar 
        /// </summary>
        /// <param name="cell"></param>
        private void BuildNextPrevUI(TableCell cell)
        {
            bool isValidPage = (PageIndex >= 0 && PageIndex <= PageCount - 1);
            bool canMoveBack = (PageIndex > 0);
            bool canMoveForward = (PageIndex < PageCount - 1);

            // Render the << button
            LinkButton first = new LinkButton();
            first.ID = "First";
            first.CommandName = "Page";
            first.CommandArgument = "First";
            first.Text = WebControlLocalization.GetText("ManageGridView_FirstPage");
            first.Enabled = isValidPage && canMoveBack;
            first.CssClass = "list_link";
            cell.Controls.Add(first);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the < button
            LinkButton prev = new LinkButton();
            prev.ID = "Prev";
            prev.CommandName = "Page";
            prev.CommandArgument = "Prev";
            prev.Text = WebControlLocalization.GetText("ManageGridView_PreviousPage");
            prev.Enabled = isValidPage && canMoveBack;
            prev.CssClass = "list_link";
            cell.Controls.Add(prev);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the > button
            LinkButton next = new LinkButton();
            next.ID = "Next";
            next.CommandName = "Page";
            next.CommandArgument = "Next";
            next.Text = WebControlLocalization.GetText("ManageGridView_NextPage");
            next.Enabled = isValidPage && canMoveForward;
            next.CssClass = "list_link";
            cell.Controls.Add(next);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            // Render the >> button
            LinkButton last = new LinkButton();
            last.ID = "Last";
            last.CommandName = "Page";
            last.CommandArgument = "Last";
            last.Text = WebControlLocalization.GetText("ManageGridView_LastPage");
            last.Enabled = isValidPage && canMoveForward;
            last.CssClass = "list_link";
            cell.Controls.Add(last);

            // Add a separator
            cell.Controls.Add(new LiteralControl("&nbsp;"));

            //Render a textbox            
            pagenum.ID = "pagenum";
            pagenum.Width = new Unit("20");
            pagenum.Height = new Unit("13");
            pagenum.Text = Convert.ToString(PageIndex + 1);
            pagenum.CssClass = "gridview_pagenum";
            cell.Controls.Add(pagenum);

            // Render navigation link
            LinkButton pageNavigator = new LinkButton();
            pageNavigator.ID = "pageNavigator";
            pageNavigator.Click += new EventHandler(pageNavigator_Click);
            pageNavigator.Text = WebControlLocalization.GetText("ManageGridView_GoPage");
            pageNavigator.CssClass = "list_link";
            cell.Controls.Add(pageNavigator);
            
        }

        private void pageNavigator_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (int.TryParse(pagenum.Text, out num))
            {
                if (num <= 0)
                    num = 1;
                if (num > PageCount)
                    num = PageCount;
            }
            else
            {
                num = 1;
            }
            pagenum.Text = num.ToString();
            LinkButton pageNavigator = (LinkButton)sender;
            pageNavigator.CommandName = "Page";
            pageNavigator.CommandArgument = num.ToString();
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
            if (PageCount <= 0 || PageIndex == -1)
            {
                pageList.Items.Add("No pages");
                pageList.Enabled = false;
                pageList.SelectedIndex = 0;
            }
            else // Populate the list
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    ListItem item = new ListItem(i.ToString(), (i - 1).ToString());
                    pageList.Items.Add(item);
                }
                pageList.SelectedIndex = PageIndex;
            }
            cell.Controls.Add(pageList);
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

        #region PageList_Click
        /// <summary>
        /// Event handler for any page selected from the drop-down page list  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageList_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.DropDownList pageList = (System.Web.UI.WebControls.DropDownList)sender;
            int pageIndex = Convert.ToInt32(pageList.SelectedItem.Value);
            this.PageIndex = pageIndex;
            //this.DataBind();

        }
        #endregion

        #region GetNavigationPageName
        /// <summary>
        /// Admin_ProvinceList.aspx  --> Admin_ProvinceAdd.aspx
        /// </summary>
        /// <returns></returns>
        private string GetNavigationPageName()
        {
            int length = this.Page.Request.Url.Segments.Length;
            string currentPageName = this.Page.Request.Url.Segments[length - 1];

            return currentPageName.Substring(0, currentPageName.Length - 9) + "add.aspx";
        }
        #endregion

        #endregion

        #region Properties

        #region PagingStyle
        /// <summary>
        /// The style of the navigation bar
        /// </summary>
        [Description("Indicates the style of the pager's navigation bar")]
        public PagingStyle PagingStyle
        {
            get { return (PagingStyle)ViewState["PagingStyle"]; }
            set { ViewState["PagingStyle"] = value; }
        }
        #endregion

        #region DeletePromptText
        /// <summary>
        /// 删除记录时的提示文字
        /// </summary>
        public string DeletePromptText
        {
            get { return WebControlLocalization.GetText("ManageGridView_DeleteConfirmation"); }
        }
        #endregion

        #endregion
    }
}
