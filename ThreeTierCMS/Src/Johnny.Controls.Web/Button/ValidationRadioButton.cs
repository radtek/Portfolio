using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web.Button
{
    public class ValidationRadioButton : System.Web.UI.HtmlControls.HtmlInputRadioButton
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            StringBuilder sb = new StringBuilder();
            sb.Append("<label for=\"" + this.ClientID + "\">" + this.Text + "</label>");
            writer.Write(sb.ToString());
        }

        /// <summary>
        /// Lable for radio
        /// </summary>
        public string Text
        {
            get
            {
                object o = ViewState["LabelText"];
                if (o != null)
                    return (string)o;
                else
                    return String.Empty;
            }
            set
            {
                ViewState["LabelText"] = value;
                ViewState.SetItemDirty("LabelText", true);
            }
        }
    }
}
