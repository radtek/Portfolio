using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Johnny.CMS.WebUI
{
    public partial class software : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
            IList<Johnny.CMS.OM.SeH.Software> list = bll.GetList(null);

            StringBuilder sb = new StringBuilder();
            foreach(Johnny.CMS.OM.SeH.Software software in list)
            {
                sb.Append("<div class=\"big-box\">");
                sb.Append("    <div class=\"big-box-inner\" style=\"height:140\" >");
        	    sb.Append("        <div class=\"big-box-text\">");
            	sb.Append(string.Format("            <h4>{0}</h4>", software.SoftwareName));
				sb.Append(string.Format("            <p>{0}</p>", software.ShortDescription));			
				sb.Append(string.Format("            <a class=\"big-box-link\" href=\"softwaredetail.aspx?softwareid={0}\">详细信息 <img class=\"arrow\" src=\"images/arrow.png\" /></a>", software.SoftwareId));
                sb.Append(string.Format("            <a class=\"big-box-link\" href=\"download.aspx?softwareid={0}\">下载 <img class=\"arrow\" src=\"images/arrow.png\" /></a>", software.SoftwareId));
                sb.Append(string.Format("            <a class=\"big-box-link\" href=\"{0}\">帮助文档 <img class=\"arrow\" src=\"images/arrow.png\" /></a>", software.DownloadUrl));
                sb.Append(string.Format("            <a class=\"big-box-link\" href=\"releasehistory.aspx?softwareid={0}\">发布历史 <img class=\"arrow\" src=\"images/arrow.png\" /></a>", software.SoftwareId));
			    sb.Append("        </div>");
			    sb.Append(string.Format("        <div class=\"big-box-image\"><a href=\"softwaredetail.aspx?softwareid={0}\"><img src=\"{1}\" /></a></div>", software.SoftwareId, software.Image));
		        sb.Append("    </div>");
	            sb.Append("</div>");               
            }

            //lblWebsites.Text = StringHelper.ConvertToHtmlTags(sb.ToString());
            lblWebsites.Text = sb.ToString();
        }
    }
}
