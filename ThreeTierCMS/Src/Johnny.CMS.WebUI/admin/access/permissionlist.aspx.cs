using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;
using System.Collections.Generic;

namespace Johnny.CMS.admin.access
{
    public partial class permissionlist : AdminListBase
    {
        protected IList<Johnny.CMS.OM.Access.PermissionCategory> categoryList;

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.Access.Permission model = new Johnny.CMS.OM.Access.Permission();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Permission_PermissionCategoryId");
                myManageGridView.Columns[3].HeaderText = GetLabelText("Permission_PermissionName");
                getData();                
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.Access.PermissionCategory category = new Johnny.CMS.BLL.Access.PermissionCategory();
            categoryList = category.GetList();

            Johnny.CMS.BLL.Access.Permission permission = new Johnny.CMS.BLL.Access.Permission();
            myManageGridView.DataSource = permission.GetList();
            myManageGridView.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;
                TextBox updPermissionName = (TextBox)row.FindControl("txtUptPermissionName");
                DropDownList updPermissionCategoryId = (DropDownList)row.FindControl("ddlCategory");

                //check permission name
                if (!CheckInputEmptyAndLength(updPermissionName, "E01301", "E01302", false))
                    return;

                //update
                Johnny.CMS.OM.Access.Permission model = new Johnny.CMS.OM.Access.Permission();
                model.PermissionId = DataConvert.GetInt32(strId);
                model.PermissionName = updPermissionName.Text;
                model.PermissionCategoryId = DataConvert.GetInt32(updPermissionCategoryId.SelectedValue);

                Johnny.CMS.BLL.Access.Permission bll = new Johnny.CMS.BLL.Access.Permission();
                bll.Update(model);
            }

            SetMessage(GetMessage("C00003"));

            //update grid
            getData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                TableCell cell = row.Cells[0];
                Johnny.Controls.Web.CheckBox.CheckBox chkSelect = (Johnny.Controls.Web.CheckBox.CheckBox)cell.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    string strId = ((Label)row.FindControl("lblPermissionId")).Text;

                    //delete
                    Johnny.CMS.BLL.Access.Permission bll = new Johnny.CMS.BLL.Access.Permission();
                    bll.Delete(DataConvert.GetInt32(strId));

                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }

        protected void myManageGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl = (Label)e.Row.FindControl("lblPermissionCategoryId");
                if (lbl != null)
                {
                    lbl.Visible = false;
                    DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                    ddlCategory.DataSource = categoryList;
                    ddlCategory.DataTextField = "PermissionCategoryName";
                    ddlCategory.DataValueField = "PermissionCategoryId";
                    ddlCategory.DataBind();
                    foreach (ListItem item in ddlCategory.Items)
                    {
                        if (DataConvert.GetString(item.Value) == lbl.Text)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }        
    }
}