using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Johnny.CMS.BLL;
using Johnny.Controls.Web.ManageGridView;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin
{
    public abstract class AdminListBase : AdminAuth
    {        
        private string managetable;
        private string managekey;
        private bool isdesc;
        protected string STR_LABEL_ID = "lblId";
        protected string STR_SEQUENCE_ID = "lblSequence";

        //protected enum ManageCmd
        //{
        //    IsVouch,    //推荐
        //    IsDisplay,  //显示
        //    IsAuditing  //审核
        //}        
                
        /// <summary>
        /// 绑定数据
        /// </summary>
        public abstract void getData();

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="strTable">表</param>
        /// <param name="strKey">主键</param>
        /// <param name="strId">值</param>
        protected void DeleteById(string strTable, string strKey, int strId)
        {
            CommonProcess cp = new CommonProcess();
            cp.DeleteById(strTable, strKey, strId);
            getData();
        }

        ///// <summary>
        ///// 设定isDisplay
        ///// </summary>
        ///// <param name="mcmd"></param>
        ///// <param name="lblText"></param>
        ///// <param name="strTable"></param>
        ///// <param name="strKey"></param>
        ///// <param name="strId"></param>
        //protected void SetIsDisplay(ManageCmd mcmd, string lblText, string strTable, string strKey, int strId)
        //{
        //    if (lblText == "True")
        //        lblText = "0";
        //    else
        //        lblText = "1";
        //    CommonProcess cp = new CommonProcess();

        //    switch (mcmd)
        //    { 
        //        case ManageCmd.IsAuditing:
        //            break;
        //        case ManageCmd.IsDisplay:                    
        //            cp.SetIsDisplay(lblText, strTable, strKey, strId);                    
        //            break;
        //        case ManageCmd.IsVouch:
        //            break;
        //        default:
        //            break;
        //    }

        //    getData();
        //}

        #region Properties
        protected string ManageTable
        {
            get { return managetable; }
            set { managetable = value; }
        }
        protected string ManageKey
        {
            get { return managekey; }
            set { managekey = value; }
        }
        protected bool IsDesc
        {
            get { return isdesc; }
            set { isdesc = value; }
        }
        #endregion

        //直接拷贝过去，放在自身页执行，也可。
        protected void myManageGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            //DeleteById(ManageTable, ManageKey, DataConvert.GetInt32(e.Item.Cells[0].Text));
        }

        protected void myManageGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            //if (e.CommandName == "SetDisplay")
            //{
            //    Label lb = (Label)e.Item.Cells[e.Item.Cells.Count - 3].FindControl("lblIsDisplay");
            //    string isDisplay = lb.Text;
            //    SetIsDisplay(ManageCmd.IsDisplay, lb.Text, ManageTable, ManageKey, DataConvert.GetInt32(e.Item.Cells[1].Text));
            //}
        }

        protected void myManageGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ManageGridView mygrid = (ManageGridView)sender;
            //DeleteById(ManageTable, ManageKey, DataConvert.GetInt32(((Label)mygrid.Rows[e.RowIndex].Cells[0].Controls[1]).Text));
        }

        protected void myManageGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ManageGridView mygrid = (ManageGridView)sender;
            mygrid.PageIndex = e.NewPageIndex;
            getData();
        }

        protected void myManageGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ManageGridView mygrid = (ManageGridView)sender;
            mygrid.EditIndex = -1;
            getData();
        }

        protected void myManageGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ManageGridView mygrid = (ManageGridView)sender;
            mygrid.EditIndex = e.NewEditIndex;
            getData();
        }

        protected void myManageGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MoveUp" || e.CommandName == "MoveDown" || e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ManageGridView mygrid = (ManageGridView)sender;
                Label lblId = mygrid.Rows[index].FindControl(STR_LABEL_ID) as Label;
                int Id = DataConvert.GetInt32(lblId.Text);
                //int Seq = Convert.ToInt32(myManageGridView.DataKeys[index].Value);
                Label lblSequence = mygrid.Rows[index].FindControl(STR_SEQUENCE_ID) as Label;
                int Seq = DataConvert.GetInt32(lblSequence.Text);
                CommonProcess cm = new CommonProcess();
                switch (e.CommandName)
                {
                    case "MoveUp":
                        cm.ExchangeSequence(ManageTable, ManageKey, Id, Seq, (isdesc == false) ? true : false);
                        break;
                    case "MoveDown":
                        cm.ExchangeSequence(ManageTable, ManageKey, Id, Seq, (isdesc == false) ? false : true);
                        break;
                    case "Delete":
                        DeleteById(ManageTable, ManageKey, Id);
                        break;
                    default:
                        break;
                }

                getData();
            }
        }

    }
}
