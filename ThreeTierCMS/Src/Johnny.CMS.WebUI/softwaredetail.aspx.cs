using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Johnny.Library.Helper;

namespace Johnny.CMS.WebUI
{
    public partial class softwaredetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int softwareid = Convert.ToInt32(Request.QueryString["softwareid"]);
            Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
            Johnny.CMS.OM.SeH.Software model = bll.GetModel(softwareid);

            if (model != null)
            {
                lblSoftwareName.Text = model.SoftwareName;
                lblUpdateTime.Text = DataConvert.GetString(model.UpdatedTime);
                lblDescription.Text = model.Description;
            }
         }
    }
}
