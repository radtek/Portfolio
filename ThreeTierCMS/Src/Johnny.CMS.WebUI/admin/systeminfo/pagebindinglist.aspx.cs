using System;
using System.Web.UI.WebControls;

using Johnny.CMS.BLL;
using Johnny.CMS.OM;
using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin
{
    public partial class pagebindinglist : AdminListBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.SystemInfo.PageBinding model = new Johnny.CMS.OM.SystemInfo.PageBinding();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Pagebinding_MenuCategoryId");
                myManageGridView.Columns[3].HeaderText = GetLabelText("Pagebinding_Title");
                myManageGridView.Columns[4].HeaderText = GetLabelText("Pagebinding_ListMenuId");
                myManageGridView.Columns[5].HeaderText = GetLabelText("Pagebinding_AddMenuId");
                getData();                
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.SystemInfo.PageBinding bll = new Johnny.CMS.BLL.SystemInfo.PageBinding();
            myManageGridView.DataSource = bll.GetList();
            myManageGridView.DataBind();
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
                    Johnny.CMS.BLL.SystemInfo.PageBinding bll = new Johnny.CMS.BLL.SystemInfo.PageBinding();
                    bll.Delete(DataConvert.GetInt32(strId));

                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }
    }
}