using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Johnny.Library.Helper;

namespace Johnny.CMS.WebUI
{
    public partial class blog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Johnny.CMS.BLL.SeH.Blog bll = new Johnny.CMS.BLL.SeH.Blog();
            IList<Johnny.CMS.OM.SeH.Blog> list = bll.GetList();

            StringBuilder sb = new StringBuilder();
            foreach (Johnny.CMS.OM.SeH.Blog blog in list)
            {
                sb.Append("<div>");
                sb.Append(string.Format("    <a href=\"blogdetail.aspx?blogid={0}\">{1}</a>", blog.BlogId, blog.Title));
                sb.Append(string.Format("    <h4>{0} by {1} | <a href=\"http://www.extjs.com/blog/2009/12/17/ext-js-3-1-massive-memory-improvements-treegrid-and-more%e2%80%a6/#comments\">评论(51) &#187;</a></h4>", blog.UpdatedTime, blog.UpdatedByName));
                sb.Append("    <div>");
                sb.Append(string.Format("        <p>{0}</p>", blog.Content));
				sb.Append("        <div style=\"clear:both;\"></div>");
				sb.Append("    </div>");
			    sb.Append("</div>");
			    sb.Append("<hr />");               
            }

            lblBlogList.Text = StringHelper.htmlOutputText(sb.ToString());
        }
    }
}
