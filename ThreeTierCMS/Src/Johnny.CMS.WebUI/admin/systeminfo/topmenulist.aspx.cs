using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin
{
    public partial class topmenulist : AdminListBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.SystemInfo.TopMenu model = new Johnny.CMS.OM.SystemInfo.TopMenu();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Topmenu_TopMenuName");
                myManageGridView.Columns[3].HeaderText = GetLabelText("Topmenu_ToolTip");
                myManageGridView.Columns[4].HeaderText = GetLabelText("Topmenu_PageLink");
                getData();                
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.SystemInfo.TopMenu bll = new Johnny.CMS.BLL.SystemInfo.TopMenu();
            myManageGridView.DataSource = bll.GetList();
            myManageGridView.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;
                TextBox uptTopMenuName = (TextBox)row.FindControl("txtUptTopMenuName");
                TextBox uptTips = (TextBox)row.FindControl("txtUptTips");
                TextBox uptPageLink = (TextBox)row.FindControl("txtUptPageLink");

                if (!CheckInputEmptyAndLength(uptTopMenuName, "E00401", "E00402", false))
                    return;
                if (!CheckInputEmptyAndLength(uptTips, "E00401", "E00402", false))
                    return;
                if (!CheckInputEmptyAndLength(uptPageLink, "E00403", "E00404"))
                    return;

                //update
                Johnny.CMS.OM.SystemInfo.TopMenu model = new Johnny.CMS.OM.SystemInfo.TopMenu();
                model.TopMenuId = DataConvert.GetInt32(strId);
                model.TopMenuName = uptTopMenuName.Text;
                model.ToolTip = uptTips.Text;
                model.PageLink = uptPageLink.Text;

                Johnny.CMS.BLL.SystemInfo.TopMenu bll = new Johnny.CMS.BLL.SystemInfo.TopMenu();
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
                    Johnny.CMS.BLL.SystemInfo.TopMenu bll = new Johnny.CMS.BLL.SystemInfo.TopMenu();
                    bll.Delete(DataConvert.GetInt32(strId));

                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }
    }
}