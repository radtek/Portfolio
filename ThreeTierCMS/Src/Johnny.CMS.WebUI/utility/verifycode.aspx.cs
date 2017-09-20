using System;
using System.Web;

using Johnny.Library.Captcha;

namespace Johnny.CMS.WebUI.utility
{
    public partial class verifycode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BufferOutput = false;
                CreateCheckCodeImage();
            }
            finally
            {
            }
        }

        private void CreateCheckCodeImage()
        {
            string checkCode = "";

            CaptchaImage ci = new CaptchaImage(ref checkCode, 65, 23, 5);

            Session["CheckCode"] = checkCode.ToLower();

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ci.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
        }
    }
}
