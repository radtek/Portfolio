using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Johnny.Library.Helper;

namespace Johnny.CMS.WebUI
{
    public partial class websites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int categoryid = 0;

            Johnny.CMS.BLL.SeH.Website bll = new Johnny.CMS.BLL.SeH.Website();
            IList<Johnny.CMS.OM.SeH.Website> list = bll.GetList();

            StringBuilder sb = new StringBuilder();
            int ix = 0;
            for (ix = 0; ix < list.Count; ix++)
            {
                if (ix != 0 && !categoryid.Equals(list[ix].WebsiteCategoryId))
                    sb.Append("<p>&nbsp;</p>");
                if (!categoryid.Equals(list[ix].WebsiteCategoryId))
                {
                    sb.Append("<div class=\"content-link\">");
                    sb.Append(string.Format("<h4>{0}</h4>", list[ix].WebsiteCategoryName));
                    sb.Append("<ul>");
                }
                sb.Append(string.Format("<li><a href=\"{0}\" target=\"_blank\" title=\"{1}\">{2}</a></li>", list[ix].URL, list[ix].Description, list[ix].WebsiteName));
                if (ix != 0 && (ix == list.Count - 1 || list[ix].WebsiteCategoryId != list[ix+1].WebsiteCategoryId))
                {
                    sb.Append("</ul>");
                    sb.Append("</div>");
                }
                categoryid = list[ix].WebsiteCategoryId;
            }

            //lblWebsites.Text = StringHelper.ConvertToHtmlTags(sb.ToString());
            lblWebsites.Text = sb.ToString();
        }
    }
}
