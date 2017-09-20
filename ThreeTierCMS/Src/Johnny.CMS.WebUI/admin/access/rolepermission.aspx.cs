using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections;

using Johnny.Library.Helper;
using Johnny.Component.Utility;
using Johnny.CMS.admin.usercontrol;

namespace Johnny.CMS.admin.access
{
    public partial class rolepermission : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                lblRoleName.Text = GetLabelText("Rolepermission_RoleId");
                CreateddlRole();
                CreatePermisssionList();                
            }
        }

        //角色
        private void CreateddlRole()
        {
            Johnny.CMS.BLL.Access.Role bll = new Johnny.CMS.BLL.Access.Role();
            ddlRoles.DataSource = bll.GetList();
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleId";
            ddlRoles.DataBind();
        }

        //动态绑定权限列表
        private void CreatePermisssionList()
        {
            Johnny.CMS.BLL.Access.PermissionCategory category = new Johnny.CMS.BLL.Access.PermissionCategory();
            IList<Johnny.CMS.OM.Access.PermissionCategory> categoryModel = category.GetList();
            string hdnSeletedClientID = "";
            string hdnRightClientID = "";
            foreach (Johnny.CMS.OM.Access.PermissionCategory item in categoryModel)
            {
                permissioncontrol permissionCtrl = (permissioncontrol)LoadControl("..\\usercontrol\\permissioncontrol.ascx");
                permissionCtrl.RoleId = DataConvert.GetInt32(ddlRoles.SelectedValue);
                permissionCtrl.PermissionCategoryId = item.PermissionCategoryId;
                permissionCtrl.PermissionCategoryName = item.PermissionCategoryName;
                PlaceHolder1.Controls.Add(permissionCtrl);
                HtmlInputHidden hdnValue = (HtmlInputHidden)permissionCtrl.FindControl("hdnSelected");
                HtmlSelect hdnRightList = (HtmlSelect)permissionCtrl.FindControl("lstRight");
                hdnSeletedClientID = hdnSeletedClientID + hdnValue.ClientID + ",";
                hdnRightClientID = hdnRightClientID + hdnRightList.ClientID + ",";
            }
            btnSave.Attributes.Add("onclick", "return Save('" + ddlRoles.ClientID + "','" + hdnSeletedClientID + "','" + hdnAllSelected.ClientID + "');");
            btnReset.Attributes.Add("onclick", "listClear('" + hdnRightClientID + "','" + hdnSeletedClientID + "','" + hdnAllSelected.ClientID + "');return false;");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] accouts = hdnAllSelected.Value.Split(',');
            int RoleId = DataConvert.GetInt32(ddlRoles.SelectedValue);

            Johnny.CMS.BLL.Access.RolePermission bll = new Johnny.CMS.BLL.Access.RolePermission();
            bll.Delete(RoleId);

            for (int ix = 0; ix < accouts.Length; ix++)
            {
                if (accouts[ix] != string.Empty)
                {
                    Johnny.CMS.OM.Access.RolePermission model = new Johnny.CMS.OM.Access.RolePermission();
                    model.RoleId = RoleId;
                    model.PermissionId = DataConvert.GetInt32(accouts[ix]);
                    if (bll.Add(model) > 0)
                    {
                        SetMessage(GetMessage("C00003"));
                    }
                    else
                        SetMessage(GetMessage("C00004"));
                    
                }
            }
            CreatePermisssionList();
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePermisssionList();
        }
    }
}