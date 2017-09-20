using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.Component.Utility;
using Johnny.CMS.BLL;

namespace Johnny.CMS.admin.seh
{
    public partial class channeladd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Channel_Title");
                litChannelName.Text = GetLabelText("Channel_ChannelName");
                txtChannelName.ToolTip = GetLabelText("Channel_ChannelName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get PermissionCategoryId
                    int MenuCategoryId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.Channel bll = new Johnny.CMS.BLL.SeH.Channel();
                    Johnny.CMS.OM.SeH.Channel model = new Johnny.CMS.OM.SeH.Channel();
                    model = bll.GetModel(MenuCategoryId);

                    txtChannelName.Text = model.ChannelName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                }

                //RFVldtMenuCategoryName.ErrorMessage = GetMessage("E00801", txtMenuCategoryName.MaxLength.ToString());
            }
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //check category name
            if (!CheckInputEmptyAndLength(txtChannelName, "E00801", "E00802", false))
                return;

            Johnny.CMS.BLL.SeH.Channel bll = new Johnny.CMS.BLL.SeH.Channel();
            Johnny.CMS.OM.SeH.Channel model = new Johnny.CMS.OM.SeH.Channel();

            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.ChannelId = Convert.ToInt32(Request.QueryString["id"]);
                model.ChannelName = txtChannelName.Text;

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert
                model.ChannelName = txtChannelName.Text;
                
                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    txtChannelName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }        
    }
}