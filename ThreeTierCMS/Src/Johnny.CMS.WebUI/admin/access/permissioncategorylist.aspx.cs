using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.access
{
    public partial class permissioncategorylist : AdminListBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.Access.PermissionCategory model = new Johnny.CMS.OM.Access.PermissionCategory();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Permissioncategory_PermissionCategoryName");
                getData();
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.Access.PermissionCategory bll = new Johnny.CMS.BLL.Access.PermissionCategory();
            myManageGridView.DataSource = bll.GetList();
            myManageGridView.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;
                TextBox uptPermissionCategoryName = (TextBox)row.FindControl("txtUptPermissionCategoryName");
                
                //check title
                if (!CheckInputEmptyAndLength(uptPermissionCategoryName, "E01201", "E01202", false))
                    return;

                //update
                Johnny.CMS.OM.Access.PermissionCategory model = new Johnny.CMS.OM.Access.PermissionCategory();
                model.PermissionCategoryId = DataConvert.GetInt32(strId);
                model.PermissionCategoryName = uptPermissionCategoryName.Text;

                Johnny.CMS.BLL.Access.PermissionCategory bll = new Johnny.CMS.BLL.Access.PermissionCategory();
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
                    string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;

                    //delete
                    Johnny.CMS.BLL.Access.PermissionCategory bll = new Johnny.CMS.BLL.Access.PermissionCategory();
                    bll.Delete(DataConvert.GetInt32(strId));
                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }
    }
}