using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Johnny.Library.Helper;

namespace Johnny.CMS.WebUI
{
    public partial class blogdetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int blogid = Convert.ToInt32(Request.QueryString["blogid"]);
            Johnny.CMS.BLL.SeH.Blog bll = new Johnny.CMS.BLL.SeH.Blog();
            Johnny.CMS.OM.SeH.Blog model = bll.GetModel(blogid);

            if (model != null)
            {
                lblTitle.Text = model.Title;
                lblUpdateTime.Text = DataConvert.GetString(model.UpdatedTime);
                lblDescription.Text = model.Content;
            }
         }
    }
}
