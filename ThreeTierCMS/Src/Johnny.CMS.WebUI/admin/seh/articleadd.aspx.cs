using System;
using System.Web.UI.WebControls;

using Johnny.CMS.OM;
using Johnny.CMS.BLL;
using Johnny.Library.Helper;
using Johnny.Component.Utility;

namespace Johnny.CMS.admin.seh
{
    public partial class articleadd : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                litPageTitle.Text = GetLabelText("Article_Title");
                litChannel.Text = GetLabelText("Article_ChannelId");
                ddlChannel.ToolTip = GetLabelText("Article_ChannelId");
                litTitle.Text = GetLabelText("Article_Title");
                txtTitle.ToolTip = GetLabelText("Article_Title");
                litSubTitle.Text = GetLabelText("Article_SubTitle");
                txtSubTitle.ToolTip = GetLabelText("Article_SubTitle");
                litKeyWord.Text = GetLabelText("Article_KeyWord");
                txtKeyWord.ToolTip = GetLabelText("Article_KeyWord");
                litSubContent.Text = GetLabelText("Article_SubContent");
                txtSubContent.ToolTip = GetLabelText("Article_SubContent");
                litContent.Text = GetLabelText("Article_Content");
                litHits.Text = GetLabelText("Article_Hits");
                txtHits.ToolTip = GetLabelText("Article_Hits");
                litFlags.Text = GetLabelText("Article_Flags");
                litFlagsHint.Text = GetLabelText("Article_Flags");
                chkIsTop.Text = GetLabelText("Article_IsTop");
                chkIsElite.Text = GetLabelText("Article_IsElite");
                chkIsPic.Text = GetLabelText("Article_IsPic");
                chkIsPageType.Text = GetLabelText("Article_IsPageType");
                litIsVerified.Text = GetLabelText("Article_IsVerified");
                rdbVerified1.Text = GetLabelText("Common_Yes");
                rdbVerified0.Text = GetLabelText("Common_No");
                litRdbVerifiedTip.Text = GetLabelText("Article_IsVerified");
                litCreatedTime.Text = GetLabelText("Common_CreatedTime");
                litCreatedByName.Text = GetLabelText("Common_CreatedByName");
                litUpdatedTime.Text = GetLabelText("Common_UpdatedTime");
                litUpdatedByName.Text = GetLabelText("Common_UpdatedByName");

                if (Request.QueryString["action"] == "modify")
                {
                    //get MenuId
                    int MenuId = Convert.ToInt32(Request.QueryString["id"]);

                    Johnny.CMS.BLL.SeH.Article bll = new Johnny.CMS.BLL.SeH.Article();
                    Johnny.CMS.OM.SeH.Article model = new Johnny.CMS.OM.SeH.Article();
                    model = bll.GetModel(MenuId);

                    CreateddlChannel();
                    foreach (ListItem item in ddlChannel.Items)
                    {
                        if (DataConvert.GetInt32(item.Value) == model.ChannelId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    txtTitle.Text = model.Title;
                    txtSubTitle.Text = model.SubTitle;
                    txtKeyWord.Text = model.KeyWord;
                    txtSubContent.Text = model.SubContent;
                    fckContent.Value = StringHelper.htmlOutputText(model.Content);
                    txtHits.Text = DataConvert.GetString(model.Hits);
                    chkIsTop.Checked = model.IsTop;
                    chkIsElite.Checked = model.IsElite;
                    chkIsPic.Checked = model.IsPic;
                    chkIsPageType.Checked = model.IsPageType;
                    if (model.IsVerified)
                        rdbVerified1.Checked = true;
                    else
                        rdbVerified0.Checked = true;
                    
                    lblCreatedTime.Text = DataConvert.GetLongDateString(model.CreatedTime);
                    lblCreatedByName.Text = model.CreatedByName;
                    lblUpdatedTime.Text = DataConvert.GetLongDateString(model.UpdatedTime);
                    lblUpdatedByName.Text = model.UpdatedByName;

                    btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
                    //btnAdd.Text = CONST_BUTTONTEXT_SAVE;
                }
                else
                {
                    CreateddlChannel();
                    rdbVerified1.Checked = true;
                    txtHits.Text = "0";
                    chkIsTop.Checked = false;
                    chkIsElite.Checked = false;
                    chkIsPic.Checked = false;
                    chkIsPageType.Checked = false;
                }

                //RFVldtMenuName.Text = GetMessage("E00901", txtMenuName.MaxLength.ToString());
                //RFVldtPageLink.Text = GetMessage("E00903", txtPageLink.MaxLength.ToString());
            }
        }

        private void CreateddlChannel()
        {
            Johnny.CMS.BLL.SeH.Channel bll = new Johnny.CMS.BLL.SeH.Channel();
            ddlChannel.DataSource = bll.GetList();
            ddlChannel.DataTextField = "ChannelName";
            ddlChannel.DataValueField = "ChannelId";
            ddlChannel.DataBind();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            //validation
            if (!CheckInputEmptyAndLength(txtTitle, "E00901", "E00902", false))
                return;
            if (!CheckInputLength(txtSubTitle, "E00903", false))
                return;
            if (!CheckInputLength(txtKeyWord, "E00903", false))
                return;
            if (!CheckInputLength(txtSubContent, "E00903", false))
                return;
            if (!CheckInputEmptyAndLength(txtHits, "E00901", "E00902", false))
                return;

            Johnny.CMS.BLL.SeH.Article bll = new Johnny.CMS.BLL.SeH.Article();
            Johnny.CMS.OM.SeH.Article model = new Johnny.CMS.OM.SeH.Article();
            if (Request.QueryString["action"] == "modify")
            {
                //update
                model.ArticleId = Convert.ToInt32(Request.QueryString["id"]);
                model.ChannelId = DataConvert.GetInt32(ddlChannel.SelectedValue);
                model.Title = txtTitle.Text;
                model.SubTitle = txtSubTitle.Text;
                model.KeyWord = txtKeyWord.Text;
                model.SubContent = txtSubContent.Text;
                model.Content = StringHelper.htmlInputText(fckContent.Value);
                model.FirstImage = DefaultImg(firstImageUrl(fckContent.Value));
                model.CopyFrom = "";
                model.Hits = DataConvert.GetInt32(txtHits.Text);
                model.IsTop = chkIsTop.Checked;
                model.IsElite = chkIsElite.Checked;
                model.IsPic = chkIsPic.Checked;
                model.IsPageType = chkIsPageType.Checked;
                model.IsVerified = rdbVerified1.Checked;
                model.UpdatedTime = System.DateTime.Now;
                model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
                model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

                bll.Update(model);
                SetMessage(GetMessage("C00003"));
            }
            else
            {
                //insert                
                model.ChannelId = DataConvert.GetInt32(ddlChannel.SelectedValue);
                model.Title = txtTitle.Text;
                model.SubTitle = txtSubTitle.Text;
                model.KeyWord = txtKeyWord.Text;
                model.SubContent = txtSubContent.Text;
                model.Content = StringHelper.htmlInputText(fckContent.Value);
                model.FirstImage = DefaultImg(firstImageUrl(fckContent.Value));
                model.CopyFrom = "";
                model.Hits = DataConvert.GetInt32(txtHits.Text);
                model.IsTop = chkIsTop.Checked;
                model.IsElite = chkIsElite.Checked;
                model.IsPic = chkIsPic.Checked;
                model.IsPageType = chkIsPageType.Checked;
                model.IsVerified = rdbVerified1.Checked;
                model.CreatedTime = System.DateTime.Now;
                model.CreatedById = DataConvert.GetInt32(Session["UserId"]);
                model.CreatedByName = DataConvert.GetString(Session["UserName"]);
                model.UpdatedTime = System.DateTime.Now;
                model.UpdatedById = DataConvert.GetInt32(Session["UserId"]);
                model.UpdatedByName = DataConvert.GetString(Session["UserName"]);

                if (bll.Add(model) > 0)
                {
                    SetMessage(GetMessage("C00001"));
                    ddlChannel.SelectedIndex = 0;
                    txtTitle.Text = "";
                    txtSubTitle.Text = "";
                    txtKeyWord.Text = "";
                    txtSubContent.Text = "";
                    fckContent.Value = "";
                    txtHits.Text = "0";
                    chkIsTop.Checked = false;
                    chkIsElite.Checked = false;
                    chkIsPic.Checked = false;
                    chkIsPageType.Checked = false;
                    rdbVerified1.Checked = true;
                    lblCreatedTime.Text = "";
                    lblCreatedByName.Text = "";
                    lblUpdatedTime.Text = "";
                    lblUpdatedByName.Text = "";
                }
                else
                    SetMessage(GetMessage("C00002"));
            }
        }

        protected string firstImageUrl(string ftbtext)
        {
            if (fckContent.Value.IndexOf("<IMG") > -1)
            {
                string s1 = ftbtext.Substring(ftbtext.IndexOf("<IMG") + 4);
                string s2 = s1.Substring(s1.IndexOf("src=\"") + 5);
                return s2.Substring(0, s2.IndexOf("\""));
            }
            else
            {
                return "";
            }
        }

        public static string DefaultImg(string img)
        {
            if (string.IsNullOrEmpty(img))
                return "aaa";//ConfigurationManager.AppSettings["DefaultImg"];
            else
                img = img.Replace("'", "");
            return img;
        }
    }
}