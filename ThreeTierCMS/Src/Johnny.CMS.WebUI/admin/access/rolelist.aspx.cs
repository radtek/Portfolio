using System;
using System.Web.UI.WebControls;

using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.access
{
    public partial class rolelist : AdminListBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.Access.Role model = new Johnny.CMS.OM.Access.Role();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Role_RoleName");
                myManageGridView.Columns[3].HeaderText = GetLabelText("Role_Description");
                getData();
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.Access.Role bll = new Johnny.CMS.BLL.Access.Role();
            myManageGridView.DataSource = bll.GetList();
            myManageGridView.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;
                TextBox updRoleName = (TextBox)row.FindControl("txtUptRoleName");
                TextBox uptDescription = (TextBox)row.FindControl("txtUptDescription");

                //check title
                if (!CheckInputEmptyAndLength(updRoleName, "E01501", "E01502", false))
                    return;

                if (!CheckInputLength(uptDescription, "E01502", false))
                    return;

                //update
                Johnny.CMS.OM.Access.Role model = new Johnny.CMS.OM.Access.Role();
                model.RoleId = DataConvert.GetInt32(strId);
                model.RoleName = updRoleName.Text;
                model.Description = uptDescription.Text;

                Johnny.CMS.BLL.Access.Role bll = new Johnny.CMS.BLL.Access.Role();
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
                    Johnny.CMS.BLL.Access.Role bll = new Johnny.CMS.BLL.Access.Role();
                    bll.Delete(DataConvert.GetInt32(strId));

                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }

        //protected void myManageGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    string strId = ((Label)myManageGridView.Rows[e.RowIndex].FindControl(STR_LABEL_ID)).Text;
        //    TextBox updRoleName = (TextBox)myManageGridView.Rows[e.RowIndex].FindControl("txtUptRoleName");
            
        //    //check role name
        //    if (!CheckInputEmptyAndLength(updRoleName, "E01501", "E01502"))
        //        return;

        //    //update
        //    MDLAccounts_Role model = new MDLAccounts_Role();            
        //    model.RoleId = DataConvert.GetInt32(strId);
        //    model.RoleName = updRoleName.Text;

        //    BLLRole bll = new BLLRole();
        //    bll.Update(model);

        //    SetMessage(GetMessage("C00003"));

        //    //update grid
        //    myManageGridView.EditIndex = -1;
        //    getData();
        //}     
    }
}