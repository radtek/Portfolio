using System;
using System.Web.UI.WebControls;

using Johnny.CMS.BLL;
using Johnny.CMS.OM;
using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.seh
{
    public partial class blogcategorylist : AdminListBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.SeH.BlogCategory model = new Johnny.CMS.OM.SeH.BlogCategory();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Blogcategory_BlogCategoryName");
                getData();
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.SeH.BlogCategory bll = new Johnny.CMS.BLL.SeH.BlogCategory();
            myManageGridView.DataSource = bll.GetList();
            myManageGridView.DataBind();
        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in myManageGridView.Rows)
            {
                string strId = ((Label)row.FindControl(STR_LABEL_ID)).Text;
                TextBox uptBlogCategoryName = (TextBox)row.FindControl("txtUptBlogCategoryName");

                //validation
                if (!CheckInputEmptyAndLength(uptBlogCategoryName, "E00801", "E00802", false))
                    return;

                //update
                Johnny.CMS.OM.SeH.BlogCategory model = new Johnny.CMS.OM.SeH.BlogCategory();
                model.BlogCategoryId = DataConvert.GetInt32(strId);
                model.BlogCategoryName = uptBlogCategoryName.Text;

                Johnny.CMS.BLL.SeH.BlogCategory bll = new Johnny.CMS.BLL.SeH.BlogCategory();
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
                    Johnny.CMS.BLL.SeH.BlogCategory bll = new Johnny.CMS.BLL.SeH.BlogCategory();
                    bll.Delete(DataConvert.GetInt32(strId));

                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }
    }
}