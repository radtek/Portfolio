using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Johnny.Library.Helper;

namespace Johnny.CMS.WebUI
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int softwareid = Convert.ToInt32(Request.QueryString["softwareid"]);
            Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
            Johnny.CMS.OM.SeH.Software model = bll.GetModel(softwareid);

            if (model != null)
            {
                lblSoftwareName.Text = model.SoftwareName;
                lblDocumentTitle.Text = model.DocumentTitle;
                lblDocumentDescription.Text = model.DocumentDescription;
                lblDocumentUrl.Text = string.Format("<a href=\"{0}\">下载</a>", model.DownloadUrl);


                Johnny.CMS.BLL.SeH.Release bllRelease = new Johnny.CMS.BLL.SeH.Release();
                Johnny.CMS.OM.SeH.Release modelRelease = bllRelease.GetLatestModel(softwareid);

                if (modelRelease != null)
                {
                    lblReleaseName.Text = modelRelease.ReleaseName;
                    lblReleaseDescription.Text = StringHelper.htmlOutputText(modelRelease.Description);

                    lblDownloadUrl.Text = string.Format("<a href=\"{0}\">下载</a>", model.DownloadUrl);
                }
                
            }   
        }
    }
}
