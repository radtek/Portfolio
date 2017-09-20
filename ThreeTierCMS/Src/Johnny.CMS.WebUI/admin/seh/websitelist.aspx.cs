using System;
using System.Web.UI.WebControls;

using Johnny.Component.Utility;
using Johnny.Library.Helper;
using System.Collections.Generic;

namespace Johnny.CMS.admin.seh
{
    public partial class websitelist : AdminListBase
    {
        protected IList<Johnny.CMS.OM.SeH.WebsiteCategory> categoryList;

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.SeH.Website model = new Johnny.CMS.OM.SeH.Website();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Website_WebsiteCategoryId");
                myManageGridView.Columns[3].HeaderText = GetLabelText("Website_WebsiteName");
                myManageGridView.Columns[4].HeaderText = GetLabelText("Website_Description");
                getData();                
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.SeH.WebsiteCategory category = new Johnny.CMS.BLL.SeH.WebsiteCategory();
            categoryList = category.GetList();

            Johnny.CMS.BLL.SeH.Website website = new Johnny.CMS.BLL.SeH.Website();
            myManageGridView.DataSource = website.GetList();
            myManageGridView.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;
                TextBox updWebsiteName = (TextBox)row.FindControl("txtUptWebsiteName");
                DropDownList updWebsiteCategoryId = (DropDownList)row.FindControl("ddlCategory");

                //check website name
                if (!CheckInputEmptyAndLength(updWebsiteName, "E01301", "E01302", false))
                    return;

                //update
                Johnny.CMS.OM.SeH.Website model = new Johnny.CMS.OM.SeH.Website();
                model.WebsiteId = DataConvert.GetInt32(strId);
                model.WebsiteName = updWebsiteName.Text;
                model.WebsiteCategoryId = DataConvert.GetInt32(updWebsiteCategoryId.SelectedValue);

                Johnny.CMS.BLL.SeH.Website bll = new Johnny.CMS.BLL.SeH.Website();
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
                    string strId = ((Label)row.FindControl("lblId")).Text;

                    //delete
                    Johnny.CMS.BLL.SeH.Website bll = new Johnny.CMS.BLL.SeH.Website();
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

                Label lbl = (Label)e.Row.FindControl("lblWebsiteCategoryId");
                if (lbl != null)
                {
                    lbl.Visible = false;
                    DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                    ddlCategory.DataSource = categoryList;
                    ddlCategory.DataTextField = "WebsiteCategoryName";
                    ddlCategory.DataValueField = "WebsiteCategoryId";
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