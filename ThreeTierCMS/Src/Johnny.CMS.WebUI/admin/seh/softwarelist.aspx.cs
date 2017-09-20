using System;
using System.Web.UI.WebControls;

using Johnny.CMS.BLL;
using Johnny.CMS.OM;
using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.seh
{
    public partial class softwarelist : AdminListBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Johnny.CMS.OM.SeH.Software model = new Johnny.CMS.OM.SeH.Software();
            base.ManageTable = model.TableName;
            base.ManageKey = model.PrimaryKey;
            base.IsDesc = model.IsDesc;

            if (!IsPostBack)
            {
                myManageGridView.Columns[2].HeaderText = GetLabelText("Software_SoftwareName");
                getData();                
            }
        }

        public override void getData()
        {
            Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
            myManageGridView.DataSource = bll.GetList(null);
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
                    Johnny.CMS.BLL.SeH.Software bll = new Johnny.CMS.BLL.SeH.Software();
                    bll.Delete(DataConvert.GetInt32(strId));

                }
            }

            SetMessage(GetMessage("C00005"));

            //update grid
            getData();
        }
    }
}