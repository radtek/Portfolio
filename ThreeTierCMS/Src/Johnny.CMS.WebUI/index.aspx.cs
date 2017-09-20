using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Johnny.Library.Helper;

namespace Johnny.CMS.WebUI
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //blog
            Johnny.CMS.BLL.SeH.Blog bll = new Johnny.CMS.BLL.SeH.Blog();
            IList<Johnny.CMS.OM.SeH.Blog> list = bll.GetList();

            StringBuilder sb = new StringBuilder();
            foreach (Johnny.CMS.OM.SeH.Blog blog in list)
            {
                sb.Append(string.Format("<li><a href=\"blogdetail.aspx?blogid={0}\">{1}({2})</a></li>", blog.BlogId, blog.Title, DataConvert.GetShortDateString(blog.UpdatedTime)));
            }

            lblBlogList.Text = sb.ToString();

            //website
            sb.Length = 0;
            Johnny.CMS.BLL.SeH.Website bllWebsite = new Johnny.CMS.BLL.SeH.Website();
            IList<Johnny.CMS.OM.SeH.Website> listWebsite = bllWebsite.GetList(35);

            int ix = 0;
            for (ix = 0; ix < listWebsite.Count; ix++)
            {
                if (ix % 12 == 0)
                    sb.Append("<ul>");
                sb.Append(string.Format("<li><a href=\"{0}\" target=\"_blank\" title=\"{1}\">{2}</a></li>", listWebsite[ix].URL, listWebsite[ix].Description, listWebsite[ix].WebsiteName));
                if (ix == listWebsite.Count - 1)
                    sb.Append("<li><b><a href=\"websites.aspx\">更多 <img class=\"arrowimage\" src=\"images/arrow.png\" /></a></b></li>");
                if (ix != 0 && (ix == listWebsite.Count - 1 || ix % 11 == 0))
                    sb.Append("</ul>");
            }

            lblWebsites.Text = sb.ToString();

            //software
            sb.Length = 0;
            Johnny.CMS.BLL.SeH.Software bllSoftware = new Johnny.CMS.BLL.SeH.Software();
            IList<Johnny.CMS.OM.SeH.Software> listSoftware = bllSoftware.GetList(2);

            for (ix = 0; ix < listSoftware.Count; ix++)
            {
                sb.Append("<div class=\"home-block-inner\">");
                sb.Append(string.Format("   <a href=\"softwaredetail.aspx?softwareid={0}\"><img class=\"shot\" src=\"{1}\" /></a>", listSoftware[ix].SoftwareId, listSoftware[ix].Image));
                sb.Append(string.Format("   <h3><a href=\"softwaredetail.aspx?softwareid={0}\">{1}</a><span style=\"padding-left:10px\"><img style=\"position:relative;top:-7px;left:4px\" src=\"images/new.gif\"></span></h3>", listSoftware[ix].SoftwareId, listSoftware[ix].SoftwareName));
                sb.Append(string.Format("   <p style=\"margin:0 0 8px 0\">{0}</p>", listSoftware[ix].ShortDescription));
                sb.Append("   <ul>");
                sb.Append(string.Format("       <li><span>{0}</span></li>", listSoftware[ix].Feature1));
                sb.Append(string.Format("       <li><span>{0}</span></li>", listSoftware[ix].Feature2));
                sb.Append(string.Format("       <li><span>{0}</span></li>", listSoftware[ix].Feature3));
                sb.Append(string.Format("       <li><span>{0}</span></li>", listSoftware[ix].Feature4));
                sb.Append("   </ul>");
                sb.Append(string.Format("   <a href=\"softwaredetail.aspx?softwareid={0}\"><img class=\"learn-more\" src=\"images/learn-more.png\" /></a>", listSoftware[ix].SoftwareId));
                sb.Append("   <div style=\"clear:both\"></div>");
                sb.Append("</div>");
                if (ix == 0)
                {
                    lblSoftware1.Text = sb.ToString();
                    sb.Length = 0;
                }
                else
                    lblSoftware2.Text = sb.ToString();
            }
        }
    }
}
