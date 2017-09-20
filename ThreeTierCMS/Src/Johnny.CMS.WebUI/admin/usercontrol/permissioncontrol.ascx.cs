using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Johnny.CMS.admin.usercontrol
{
    public partial class permissioncontrol : System.Web.UI.UserControl
    {
        public int RoleId;
        public int PermissionCategoryId;
        public string PermissionCategoryName;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblCategory.Text = PermissionCategoryName;
            CreatelstAccount(lstLeft, PermissionCategoryId);
            CreateRightAccount(lstRight, PermissionCategoryId);
            btnSelect.Attributes.Add("onclick", "SelectOne('" + lstLeft.ClientID + "','" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            btnSelectAll.Attributes.Add("onclick", "SelectAll('" + lstLeft.ClientID + "','" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            btnUnselect.Attributes.Add("onclick", "UnSelectOne('" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            btnUnselectAll.Attributes.Add("onclick", "UnSelectAll('" + lstRight.ClientID + "','" + hdnSelected.ClientID + "')");
            hdnSelected.Value = "";
            foreach (ListItem item in lstRight.Items)
            {
                hdnSelected.Value = hdnSelected.Value + item.Value + ",";
            }
        }

        //所有权限
        private void CreatelstAccount(ListBox listcontrol, int PermissionCategoryId)
        {
            Johnny.CMS.BLL.Access.Permission bll = new Johnny.CMS.BLL.Access.Permission();
            listcontrol.DataSource = bll.GetList(PermissionCategoryId);
            listcontrol.DataTextField = "PermissionName";
            listcontrol.DataValueField = "PermissionId";
            listcontrol.DataBind();
        }

        //拥有权限
        private void CreateRightAccount(HtmlSelect listcontrolright, int TopMenuId)
        {
            Johnny.CMS.BLL.Access.RolePermission bll = new Johnny.CMS.BLL.Access.RolePermission();
            listcontrolright.DataSource = bll.GetList(RoleId, PermissionCategoryId);
            listcontrolright.DataTextField = "PermissionName";
            listcontrolright.DataValueField = "PermissionId";
            listcontrolright.DataBind();
        }
    }
}