using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin
{
    public partial class topmenuadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("TopMenu_Title");
                litTopMenuName.Text = GetLabelText("Topmenu_TopMenuName");
                txtTopMenuName.ToolTip = GetLabelText("Topmenu_TopMenuName");
                litToolTip.Text = GetLabelText("Topmenu_ToolTip");
                txtToolTip.ToolTip = GetLabelText("Topmenu_ToolTip");
                litPageLink.Text = GetLabelText("Topmenu_PageLink");
                txtPageLink.ToolTip = GetLabelText("Topmenu_PageLink");

                if (Request.QueryString["action"] == "modify")
                {
                    //get TopMenuId
                    int TopMenuId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SystemInfo.TopMenu bll = new Johnny.CMS.BLL.SystemInfo.TopMenu();
                    Johnny.CMS.OM.SystemInfo.TopMenu model = new Johnny.CMS.OM.SystemInfo.TopMenu();
                    model = bll.GetModel(TopMenuId);

                    txtTopMenuName.Text = model.TopMenuName;
                    txtToolTip.Text = model.ToolTip;
                    txtPageLink.Text = model.PageLink;
                    //txtImage.Text = model.Image;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }

                //RFVldtTopMenuName.ErrorMessage = GetMessage("E00401", txtTopMenuName.MaxLength.ToString());
                //RFVldtPageLink.ErrorMessage = GetMessage("E00403", txtPageLink.MaxLength.ToString());
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (!CheckInputEmptyAndLength(txtTopMenuName, "E00401","E00402", false))
                return;
            if (!CheckInputEmptyAndLength(txtToolTip, "E00401", "E00402", false))
                return;
            if (!CheckInputEmptyAndLength(txtPageLink, "E00403", "E00404"))
                return;

            Johnny.CMS.BLL.SystemInfo.TopMenu bll = new Johnny.CMS.BLL.SystemInfo.TopMenu();
            Johnny.CMS.OM.SystemInfo.TopMenu model = new Johnny.CMS.OM.SystemInfo.TopMenu();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.TopMenuId = Convert.ToInt32(Request.QueryString["id"]);
                model.TopMenuName = txtTopMenuName.Text;
                model.ToolTip = txtToolTip.Text;
                model.PageLink = txtPageLink.Text;
                //model.Image = txtImage.Text;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.TopMenuName = txtTopMenuName.Text;
                model.ToolTip = txtToolTip.Text;
                model.PageLink = txtPageLink.Text;
                //model.Image = txtImage.Text;
                
                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtTopMenuName.Text = "";
                    txtToolTip.Text = "";
                    txtPageLink.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }

        
    }
}