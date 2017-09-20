using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design;
using System.ComponentModel;

namespace Johnny.Controls.Web.ExtjsTab
{
    [
        //    DefaultProperty("ID"),
    ToolboxData("<{0}:ExtjsTab runat=server></{0}:ExtjsTab>"),
    ]
    public class ExtjsTab : WebControl
    {
        public ExtjsTabPageCollection TabPages = new ExtjsTabPageCollection();
                
        protected override void RenderContents(HtmlTextWriter output)
        {
            //<table cellspacing="0" class="nav main-nav">
            //  <tr>
            //    <td><a id="home-link" href="index.html"><span>首页</span></a></td>
            //    <td><a id="products-link" href="articlelist.html"><span>SAP</span></a></td>
            //    <td><a id="support-link" href="/support"><span>.NET</span></a></td>
            //    <td><a id="jobs-link" href="http://jobs.extjs.com"><span>Flex</span></a></td>
            //    <td class="active"><a id="mysoftware-link" href="mysoftware.html"><span>软件项目</span></a></td>
            //    <td><a id="company-link" href="/company"><span>网盘</span></a></td>
            //    <td><a id="blog-link" href="/blog"><span>Blog</span></a></td>
            //    <td><a id="store-link" href="/store"><span>关于站长</span></a></td>
            //  </tr>
            //</table>
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellspacing=\"0\" class=\"nav main-nav\">");
            sb.Append("<tr>");
            for (int ix = 0; ix < this.TabPages.Count; ix++)
            {
                if (this.TabPages[ix].Selected)
                    sb.AppendFormat("<td class=\"active\"><a id=\"{0}\" href=\"{1}\"><span>{2}</span></a></td>", this.TabPages[ix].TabPageID, this.TabPages[ix].Url, this.TabPages[ix].Text);
                else
                    sb.AppendFormat("<td><a id=\"{0}\" href=\"{1}\"><span>{2}</span></a></td>", this.TabPages[ix].TabPageID, this.TabPages[ix].Url, this.TabPages[ix].Text);
            }
            sb.Append("</tr>");
            sb.Append("</table>");

            output.Write(sb.ToString());
        }

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
