using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Johnny.Controls.Web.Literal
{
    public class Literal: System.Web.UI.WebControls.Literal
    {
        public bool Mandatory
        {
            get; set;
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            if (Mandatory)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<span style=\"color:red\">*</span>");
                writer.Write(sb.ToString());
            }
            base.Render(writer);
        }
    }
}
