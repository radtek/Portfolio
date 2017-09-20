using System;
using System.Web.UI.WebControls;

using Johnny.CMS.BLL;
using Johnny.CMS.OM;
using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin
{
    public partial class menucategorylist : AdminListBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.SystemInfo.MenuCategory model = new Johnny.CMS.OM.SystemInfo.MenuCategory();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Menucategory_MenuCategoryName");
                getData();
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.SystemInfo.MenuCategory bll = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
            myManageGridView.DataSource = bll.GetList();
            myManageGridView.DataBind();
        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;
                TextBox uptName = (TextBox)row.FindControl("txtUptMenuCategoryName");

                //check name
                if (!CheckInputEmptyAndLength(uptName, "E00801", "E00802", false))
                    return;

                //update
                Johnny.CMS.OM.SystemInfo.MenuCategory model = new Johnny.CMS.OM.SystemInfo.MenuCategory();
                model.MenuCategoryId = DataConvert.GetInt32(strId);
                model.MenuCategoryName = uptName.Text;

                Johnny.CMS.BLL.SystemInfo.MenuCategory bll = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
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
                    Johnny.CMS.BLL.SystemInfo.MenuCategory bll = new Johnny.CMS.BLL.SystemInfo.MenuCategory();
                    bll.Delete(DataConvert.GetInt32(strId));

                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }
    }
}