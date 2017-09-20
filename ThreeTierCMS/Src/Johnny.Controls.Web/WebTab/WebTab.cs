using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design;
using System.ComponentModel;

namespace Johnny.Controls.Web.WebTab
{
    [
        //    DefaultProperty("ID"),
    ToolboxData("<{0}:WebTab runat=server></{0}:WebTab>"),
    ]
    public class WebTab : WebControl
    {
        public WebTabPageCollection items = new WebTabPageCollection();
                
        #region CreateChildControls
        /// <summary>
        /// This method is called from base.Render(), and starts the build menu process.
        /// </summary>
        protected override void CreateChildControls()
        {
            BuildTab();
        }
        #endregion

        #region BuildTab
        /// <summary>
        /// BuildMenu builds the top-level menu.  It is called from the OnDataBinding method as well
        /// as from <see cref="CreateChildControls"/>.  It has code to check if the top-level menu should be
        /// laid out horizontally or vertically.
        /// </summary>
        protected virtual void BuildTab()
        {

            //<table style="BACKGROUND-IMAGE: url(images/menu_bg.jpg); WIDTH: 100%" cellSpacing=0 cellPadding=0 border=0>
            //  <TR>
            //    <TD>      
            //      <TABLE cellSpacing=0 cellPadding=0 border=0>       
            //        <TR>
            //          <TD width=6 height=38></TD>
            //          <TD class=button_down id=IDC_DownedBUtton 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?type=000000');">快捷方式</TD>
            //          <TD class=button_down 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?Type=000000000006');">新闻管理</TD>
            //          <TD class=button_down 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?Type=000000000003');">发布管理</TD>
            //          <TD class=button_down 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?Type=000000000005');">会员中心</TD>
            //          <TD class=button_down 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?Type=212263665712');">模型管理</TD>
            //          <TD class=button_down 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?Type=160066612604');">插件管理</TD>
            //          <TD class=button_down 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?Type=160066612603');">控制面版</TD>
            //         </TR>
            //      </TABLE>
            //     </TD>
            //  </TR>
            //  </TABLE>
            // iterate through the Items
            Table webTab = new Table();
            webTab.Attributes.Add("id", this.ClientID);
            webTab.CellPadding = 0;
            webTab.CellSpacing = 0;
            webTab.BorderWidth = 0;
            webTab.Width = new Unit("100%");
            webTab.Style.Add("background-image", "url(images/topmenu/topmenu_bg.jpg)");

            TableRow tr = new TableRow();
            TableCell tc= new TableCell();
            Table tbPages = new Table();
            tbPages.BorderWidth= new Unit("0");
            tbPages.CellPadding = 0;
            tbPages.CellSpacing = 0;
            TableRow trPages = new TableRow();
            TableCell tcSpace = new TableCell();
            tcSpace.Width = new Unit("6");
            tcSpace.Height = new Unit("38");
            trPages.Controls.Add(tcSpace);

            // Iterate through the top-level menu's menuitems, and add a <td> tag for each menuItem
            for (int ix = 0; ix < this.items.Count; ix++)
            {
                WebTabPage tabPage = this.items[ix];

                BuildTabPage(trPages, tabPage, ix);                               
            }

            tbPages.Controls.Add(trPages);
            tc.Controls.Add(tbPages);
            tr.Controls.Add(tc);
            webTab.Controls.Add(tr);

            Controls.Add(webTab);
        }
        #endregion

        #region private methods
        private void BuildTabPage(TableRow tr, WebTabPage item, int index)
        {
            //<TD class=button_down id=IDC_DownedBUtton 
            //          style="PADDING-LEFT: 14px; WIDTH: 82px; CURSOR: pointer; BACKGROUND-REPEAT: no-repeat; TEXT-ALIGN: left" 
            //          onclick="CheckBTN1(this,'menu.aspx?type=000000');">快捷方式</TD>
            
            //left image
            TableCell tcPage = new TableCell();
            tcPage.Text = item.Text;
            if (index == SelectedIndex)
                tcPage.CssClass = "button_select";
            else
                tcPage.CssClass = "button_down";
            tcPage.ID = "JohnnyTabPage" + index.ToString();
            tcPage.Width = new Unit("82");
            tcPage.ToolTip = item.ToolTip;
            tcPage.Style.Add("padding-left", "14px");
            tcPage.Style.Add("cursor", "pointer");
            tcPage.Style.Add("background-repeat", "no-repeat");
            tcPage.Style.Add("text-align", "left");
            tcPage.Attributes.Add("onclick", "CheckBTN1(this,'menu.aspx?topmenuid=" + item.TabPageID + "','" + item.Url + "')");
            tr.Controls.Add(tcPage);

        }        
        #endregion

        #region Properties

        #region ScriptPath
        public string ScriptPath
        {
            get
            {
                object o = ViewState["MenuScriptPath"];
                if (o != null)
                    return (string)o;
                else
                    return String.Empty;
            }
            set
            {
                ViewState["MenuScriptPath"] = value;
            }
        }
        #endregion

        #region SelectedIndex
        /// <summary>
        /// Gets or sets the MenuItem's ID.  It is not recommended that this be set directly.
        /// </summary>
        /// <remarks>The <b>ID</b> for each MenuItem is programmatically set in the <see cref="Menu"/> class's
        /// <see cref="Menu.BuildMenuItem"/> method.</remarks>
        [Browsable(false)]
        public virtual int SelectedIndex
        {
            get
            {
                object o = ViewState["SelectedIndex"];
                if (o != null)
                    return (int)o;
                else
                    return 0;
            }
            set
            {
                ViewState["SelectedIndex"] = value;
                ViewState.SetItemDirty("SelectedIndex", true);
            }
        }
        #endregion

        #endregion
    }
}
