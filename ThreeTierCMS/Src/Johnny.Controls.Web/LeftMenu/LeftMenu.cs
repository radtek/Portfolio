using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design;
using System.ComponentModel;

namespace Johnny.Controls.Web.LeftMenu
{
    [
//    DefaultProperty("ID"),
    ToolboxData("<{0}:LeftMenu runat=server></{0}:LeftMenu>"),
    ]
    public class LeftMenu : WebControl
    {
        public MainMenuItemCollection items = new MainMenuItemCollection();

        protected override void Render(HtmlTextWriter writer)
        {
            if (Site != null && Site.DesignMode)
                CreateChildControls();

            base.Render(writer);
        }

        #region CreateChildControls
        /// <summary>
        /// This method is called from base.Render(), and starts the build menu process.
        /// </summary>
        protected override void CreateChildControls()
        {            
            BuildMenu();
        }
        #endregion

        #region BuildMenu
        /// <summary>
        /// BuildMenu builds the top-level menu.  It is called from the OnDataBinding method as well
        /// as from <see cref="CreateChildControls"/>.  It has code to check if the top-level menu should be
        /// laid out horizontally or vertically.
        /// </summary>
        protected virtual void BuildMenu()
        {
            // iterate through the Items
            Table menu = new Table();
            menu.Attributes.Add("id", this.ClientID);

            menu.Width = new Unit("100%");
            //menu.Height = new Unit("100%");
            menu.CellPadding = 0;
            menu.CellSpacing = 0;
           
            // Iterate through the top-level menu's menuitems, and add a <td> tag for each menuItem
            for (int ix = 0; ix < this.items.Count; ix++)
            {
                MainMenuItem mainMenu = this.items[ix];

                TableRow tr = new TableRow();

                BuildMainItems(tr, mainMenu, ix);
                
                menu.Controls.Add(tr);

                if (mainMenu.SubItems.Count > 0)
                {
                    TableRow tr2 = new TableRow();
                    BuildSubItems(tr2, mainMenu,ix);
                    menu.Controls.Add(tr2);
                }
            }

            Controls.Add(menu);
        }
        #endregion

        #region private methods
        private void BuildMainItems(TableRow tr, MainMenuItem item, int index)
        {
            //<tr height="29" style="background-image: url(images/menu_bg_01.gif);">
            //    <td width="26" align="center"><img src="images/menu_dot1.gif" width="8" height="11" alt="展开/隐藏" id="arrow_3" /></td>
            //    <td width="89">系统设定</td>
            //    <td width="50" align="center"><img src="images/menu_+.gif" width="8" height="11" alt="展开/隐藏" onclick="show_hide('profile_3', 'arrow_3')"  style="cursor:pointer;" /></td>
            //</tr>
            tr.Height = 29;
            tr.Attributes.Add("onclick", "show_hide('divSubMenu" + index.ToString() + "', 'arrow" + index.ToString() + "')");
            tr.Style.Add("cursor", "hand");
            tr.Style.Add("background-image", "url(images/leftmenu/menu_category_bg.gif)");

            //left image
            TableCell tcleft = new TableCell();
            tcleft.Width = 26;
            tcleft.HorizontalAlign = HorizontalAlign.Center;
            HtmlImage leftimg = new HtmlImage();
            leftimg.Src = "images/leftmenu/menu_expand.gif";
            //leftimg.Width = 11;
            //leftimg.Height = 8;
            leftimg.Alt = "隐藏/展开";
            leftimg.ID = "arrow" + index.ToString();
            tcleft.Controls.Add(leftimg);
            tr.Controls.Add(tcleft);

            //text
            TableCell tctext = new TableCell();
            tctext.Text = item.Text;
            tctext.Width = new Unit("89");
            tr.Controls.Add(tctext);

            //right image
            TableCell tcright = new TableCell();
            tcright.Width = 50;
            //tcright.HorizontalAlign = HorizontalAlign.Center;
            //HtmlImage rightimg = new HtmlImage();
            //rightimg.Src = "images/menu_+.gif";
            //rightimg.Width = 8;
            //rightimg.Height = 11;
            //rightimg.Alt = "隐藏/展开";
            //tcright.Controls.Add(rightimg);
            tr.Controls.Add(tcright);

        }
        private void BuildSubItems(TableRow tr, MainMenuItem item, int index)
        {
            //<tr>
            //    <td colspan=3>
            //    <div id="profile_3" style="display:block;">
            //    <table width="98%" border="0" cellspacing="2" cellpadding="2">
            //    <tr><td width='20'><img src='images/menu_dot_2.gif' alt='' border='0' align='right'></td><td align='left'><a class='menulist' href='Admin_Display.aspx' target='mainFrame'>探针</a></td></tr>
            //    <tr><td width='20'><img src='images/menu_dot_2.gif' alt='' border='0' align='right'></td><td align='left'><a class='menulist' href='Admin_ProvinceList.aspx' target='mainFrame'>省份</a></td></tr>
            //    <tr><td width='20'><img src='images/menu_dot_2.gif' alt='' border='0' align='right'></td><td align='left'><a class='menulist' href='Admin_CityList.aspx' target='mainFrame'>城市</a></td></tr>
            //    <tr><td width='20'><img src='images/menu_dot_2.gif' alt='' border='0' align='right'></td><td align='left'><a class='menulist' href='Admin_AdvertisementList.aspx' target='mainFrame'>广告</a></td></tr>
            //    <tr><td width='20'><img src='images/menu_dot_2.gif' alt='' border='0' align='right'></td><td align='left'><a class='menulist' href='Admin_MenuList.aspx' target='mainFrame'>菜单</a></td></tr>
            //    <tr><td width='20'><img src='images/menu_dot_2.gif' alt='' border='0' align='right'></td><td align='left'><a class='menulist' href='Admin_MenuCategoryList.aspx' target='mainFrame'>菜单类别</a></td></tr>
            //    </table>
            //    </div> 
            //    </td>
            //</tr>

            TableCell tc = new TableCell();
            tc.ColumnSpan = 3;

            Panel divPanel = new Panel();
            divPanel.Attributes.Add("id", "divSubMenu" + index.ToString());
            divPanel.Style.Add("display", "block");
            
            Table tb = new Table();
            tb.Width = new Unit("100%");
            tb.BorderWidth = new Unit("0");
            tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8FDFF");
            tb.CellPadding = 2;
            tb.CellSpacing = 2;

            for (int ix = 0; ix < item.SubItems.Count; ix++)
            {
                TableRow subtr = new TableRow();

                //left image
                TableCell tcleft = new TableCell();
                tcleft.Width = new Unit("20");
                HtmlImage leftimg = new HtmlImage();
                leftimg.Src = "images/leftmenu/menu_item.gif";
                leftimg.Border = 0;
                leftimg.Align = "right";
                tcleft.Controls.Add(leftimg);
                subtr.Controls.Add(tcleft);
                //right anchor
                TableCell tcright = new TableCell();
                tcright.HorizontalAlign = HorizontalAlign.Left;
                HtmlAnchor anchor = new HtmlAnchor();
                anchor.HRef = item.SubItems[ix].Url;
                anchor.Target = "mainFrame";
                anchor.InnerText = item.SubItems[ix].Text;
                anchor.Title = item.SubItems[ix].ToolTip;
                anchor.Attributes.Add("class", "menulist");
                tcright.Controls.Add(anchor);
                subtr.Controls.Add(tcright);

                tb.Controls.Add(subtr);
            }

            divPanel.Controls.Add(tb);
            tc.Controls.Add(divPanel);
            tr.Controls.Add(tc);
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
        #endregion
    }
}
