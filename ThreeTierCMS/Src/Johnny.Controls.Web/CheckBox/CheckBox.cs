using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Johnny.Component.Globalization;

namespace Johnny.Controls.Web.CheckBox
{
    /// <summary>
    /// ��ȫѡ/ȫ��ѡ��ѡ��ؼ�
    /// </summary>
    public class CheckBox : System.Web.UI.WebControls.CheckBox
    {
        public CheckBox()
        {
            
        }

        /// <summary>
        /// ��ȡ�����ñ��󶨵�����
        /// </summary>
        public string BindedValue
        {
            get
            {
                object obj1 = this.ViewState["BindedValue"];
                return (obj1 == null) ? string.Empty : (string)obj1;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.ViewState["BindedValue"] = value;
                }
            }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ����ڵ�������
        /// </summary>
        public string Group
        {
            get
            {
                object obj1 = this.ViewState["Group"];
                return (obj1 == null) ? string.Empty : (string)obj1;
            }
            set
            {
                this.ViewState["Group"] = value;
            }
        }
        /// <summary>
        /// ��ȡ������һ��ֵ����ֵ��ʶ�ؼ��Ƿ�Ϊ������
        /// </summary>
        public bool IsParent
        {
            get
            {
                object obj1 = this.ViewState["IsParent"];
                return (obj1 == null) ? false : (bool)obj1;
            }
            set
            {
                this.ViewState["IsParent"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Visible && this.Enabled && !string.IsNullOrEmpty(this.Group))
            {
                if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(typeof(CheckBox), "WebControls.CheckBox.js"))
                {
                    //this.Page.ClientScript.RegisterClientScriptResource(typeof(CheckBox), "WebControls.CheckBox.js");
                }
                string text1 = "document.getElementById(\"" + this.ClientID + "\")";
                this.Page.ClientScript.RegisterArrayDeclaration(this.Group, text1);
                if (this.IsParent)
                    this.Page.ClientScript.RegisterHiddenField("zrWebCheckBoxParentId", this.ClientID);
            }
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (IsParent)
                this.Text = GlobalizationUtility.GetLabelText("ManageGridView_CheckAll");

            if (this.Enabled && this.Visible && !string.IsNullOrEmpty(this.Group))
            {
                if (!string.IsNullOrEmpty(this.BindedValue))
                {
                    writer.AddAttribute("value", this.BindedValue);
                }
                //writer.AddAttribute("isparent", this.IsParent.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "javascript:zrWebCheckBox_Check(this, '" + this.Group + "','" + this.IsParent.ToString() + "')");
            }
            base.Render(writer);
        }
    }
}