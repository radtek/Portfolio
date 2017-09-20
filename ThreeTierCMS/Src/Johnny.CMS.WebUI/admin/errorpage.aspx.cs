using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Johnny.CMS.admin
{
    public partial class errorpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["error"] != null)
                    lblMessage.Text = Application["error"].ToString();
                else
                    lblMessage.Text = "Error";
            }
        }
    }
}
