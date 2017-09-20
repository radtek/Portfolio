using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Johnny.Library.Helper;

namespace Johnny.CMS.WebUI
{
    public partial class releasehistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int softwareid = Convert.ToInt32(Request.QueryString["softwareid"]);
            Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
            Johnny.CMS.OM.SeH.Software model = bll.GetModel(softwareid);

            if (model != null)
            {
                lblSoftwareName.Text = model.SoftwareName;

                Johnny.CMS.BLL.SeH.Release bllRelease = new Johnny.CMS.BLL.SeH.Release();
                IList<Johnny.CMS.OM.SeH.Release> list = bllRelease.GetList(softwareid);

                StringBuilder sb = new StringBuilder();
                foreach (Johnny.CMS.OM.SeH.Release release in list)
                {
                    sb.Append("<tr>");
				    sb.Append("    <td>");
				    sb.Append(string.Format("	<p class=\"date\">{0}</p>", DataConvert.GetShortDateString(release.ReleaseDate)));
				    sb.Append(string.Format("	<p class=\"release\">{0}</p>", release.ReleaseName));
				    sb.Append(string.Format("	{0}", release.Description));
                    sb.Append("    </td>");
                    sb.Append("</tr>");                
                }

                lblReleaseList.Text = StringHelper.htmlOutputText(sb.ToString());
            }            
        }
    }
}
