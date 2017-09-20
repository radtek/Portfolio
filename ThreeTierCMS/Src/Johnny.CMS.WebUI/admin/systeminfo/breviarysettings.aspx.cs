using System;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.ObjectModel;

using Johnny.Component.Utility;
using Johnny.Library.Helper;

namespace Johnny.CMS.admin.systeminfo
{
    public partial class breviarysettings : AdminAuth
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                Johnny.CMS.BLL.SystemInfo.BreviarySettings bll = new Johnny.CMS.BLL.SystemInfo.BreviarySettings();
                Johnny.CMS.OM.SystemInfo.BreviarySettings model = new Johnny.CMS.OM.SystemInfo.BreviarySettings();
                model = bll.GetModel(1);

                txtWidth.Text = DataConvert.GetString(model.Width);
                txtHeight.Text = DataConvert.GetString(model.Height);
                if (model.PlusWatermark)
                    rdbPlusWatermark1.Checked = true;
                else
                    rdbPlusWatermark0.Checked = true;
                if (model.WatermarkType)
                    rdbWatermarkType0.Checked = true;
                else
                    rdbWatermarkType0.Checked = true;
                txtWatermarkImage.Text = model.WatermarkImage;
                txtImageTransparent.Text = DataConvert.GetString(model.ImageTransparent);
                txtWatermarkText.Text = model.WatermarkText;
                txtTextTransparent.Text = DataConvert.GetString(model.TextTransparent);
                CreateddlWatermarkPosition();
                foreach (ListItem item in ddlWatermarkPosition.Items)
                {
                    if (DataConvert.GetInt32(item.Value) == model.WatermarkPosition)
                    {
                        item.Selected = true;
                        break;
                    }
                }
               
                btnAdd.ButtonType = Johnny.Controls.Web.Button.Button.EnumButtonType.Save;
            }
        }

        private void CreateddlWatermarkPosition()
        {
            ddlWatermarkPosition.Items.Add(new ListItem("--选择水印位置--", "0"));
            ddlWatermarkPosition.Items.Add(new ListItem("左上", "1"));
            ddlWatermarkPosition.Items.Add(new ListItem("左中", "2"));
            ddlWatermarkPosition.Items.Add(new ListItem("左下", "3"));
            ddlWatermarkPosition.Items.Add(new ListItem("中上", "4"));
            ddlWatermarkPosition.Items.Add(new ListItem("正中", "5"));
            ddlWatermarkPosition.Items.Add(new ListItem("中下", "6"));
            ddlWatermarkPosition.Items.Add(new ListItem("右上", "7"));
            ddlWatermarkPosition.Items.Add(new ListItem("右中", "8"));
            ddlWatermarkPosition.Items.Add(new ListItem("右下", "9"));
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            SetMessage("");

            //validation
            if (!DataValidation.IsNaturalNumberRange(txtWidth.Text, 1, 1680))
                return;
            if (!DataValidation.IsNaturalNumberRange(txtHeight.Text, 1, 1024))
                return;
            if (!CheckInputLength(txtWatermarkImage, "E00104"))
                return;
            if (!DataValidation.IsNaturalNumberRange(txtImageTransparent.Text, 0, 100))
                return;
            if (!CheckInputLength(txtWatermarkImage, "E00104", false))
                return;
            if (!DataValidation.IsNaturalNumberRange(txtTextTransparent.Text, 0, 100))
                return;

            Johnny.CMS.BLL.SystemInfo.BreviarySettings bll = new Johnny.CMS.BLL.SystemInfo.BreviarySettings();
            Johnny.CMS.OM.SystemInfo.BreviarySettings model = new Johnny.CMS.OM.SystemInfo.BreviarySettings();

            model.Width = DataConvert.GetInt32(txtWidth.Text);
            model.Height = DataConvert.GetInt32(txtHeight.Text);
            model.PlusWatermark = rdbPlusWatermark1.Checked;
            model.WatermarkType = rdbWatermarkType0.Checked;
            model.WatermarkImage = txtWatermarkImage.Text;
            model.ImageTransparent = DataConvert.GetInt32(txtImageTransparent.Text);
            model.WatermarkText = txtWatermarkText.Text;
            model.TextTransparent = DataConvert.GetInt32(txtTextTransparent.Text);
            model.WatermarkPosition = DataConvert.GetInt32(ddlWatermarkPosition.SelectedValue);

            bll.AddOrUpdate(model);
            SetMessage(GetMessage("C00003"));
        }
    }
}