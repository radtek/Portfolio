using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web.DropDownList
{
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        public bool ValidateOneSelected
        {
            get;
            set;
        }
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            if (ValidateOneSelected)
            {
                sb.Append("validate-one-selected");
            }

            this.Attributes.Add("pattern", sb.ToString());

            if (!String.IsNullOrEmpty(this.ToolTip))
                this.Attributes.Add("tip", this.ToolTip);
            base.AddAttributesToRender(writer);
        }
    }
}
