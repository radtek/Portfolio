using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web.TextBox
{
    public class TextBox: System.Web.UI.WebControls.TextBox
    {
        public bool Required
        {
            get;
            set;
        }
        public bool ValidateEmail
        {
            get;
            set;
        }
        public bool ValidateDateCN
        {
            get;
            set;
        }
        public bool ValidateNumber
        {
            get;
            set;
        }
        public bool ValidateIntRange02147483647
        {
            get;
            set;
        }
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            if (Required)
            {
                sb.Append("required ");
            }

            if (ValidateEmail)
            {
                sb.Append("validate-email ");
            }

            if (ValidateDateCN)
            {
                sb.Append("validate-date-cn ");
            }

            if (ValidateNumber)
            {
                sb.Append("validate-number ");
            }
            
            if (ValidateIntRange02147483647)
            {
                sb.Append("validate-int-range-0-2147483647");
            }
            
            sb.Append(string.Format("max-length-{0}", this.MaxLength));

            this.Attributes.Add("pattern", sb.ToString());
            
            this.Attributes.Add("tip", this.ToolTip);
            base.AddAttributesToRender(writer);
        }
    }
}
