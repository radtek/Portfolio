using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Johnny.CMS.WebUI
{
    public partial class about : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //about
            Johnny.CMS.BLL.SystemInfo.WebSettings bll = new Johnny.CMS.BLL.SystemInfo.WebSettings();
            Johnny.CMS.OM.SystemInfo.WebSettings model = bll.GetModel();

            if (model != null)
            {
                lblShortDescription.Text = model.ShortDescription;
            }

            //website
            Johnny.CMS.BLL.SeH.OpenSource bllOpenSource = new Johnny.CMS.BLL.SeH.OpenSource();
            IList<Johnny.CMS.OM.SeH.OpenSource> listOpenSource = bllOpenSource.GetList();

            StringBuilder sb = new StringBuilder();
            int ix = 0;
            for (ix = 0; ix < listOpenSource.Count; ix++)
            {
                if (ix % 4 == 0)
                    sb.Append("<ul>");
                sb.Append(string.Format("<li><a href=\"{0}\" target=\"_blank\" title=\"{1}\">{2}</a></li>", listOpenSource[ix].URL, listOpenSource[ix].Description, listOpenSource[ix].OpenSourceName));
                if (ix != 0 && (ix == listOpenSource.Count - 1 || ix % 3 == 0))
                    sb.Append("</ul>");
            }

            lblWebsites.Text = sb.ToString();

            //best practice
            sb.Length = 0;
            Johnny.CMS.BLL.SeH.BestPractice bllBestPractice = new Johnny.CMS.BLL.SeH.BestPractice();
            IList<Johnny.CMS.OM.SeH.BestPractice> listBestPractice = bllBestPractice.GetList();

            foreach (Johnny.CMS.OM.SeH.BestPractice bestpractice in listBestPractice)
            {
                sb.Append(string.Format("	<h4><a href=\"bestpracticedetails.aspx?bestpracticeid={0}\">{1}</a></h4>", bestpractice.BestPracticeId, bestpractice.BestPracticeName));
            }

            lblBestPractice.Text = sb.ToString();
        }
    }
}
