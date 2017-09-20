using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web.Button
{
    public class Button : System.Web.UI.WebControls.Button
    {
        public enum EnumButtonType
        { 
            Add,
            Save,
            Delete,
            ResetPassword
        }

        private EnumButtonType _buttontype = EnumButtonType.Add;
        /// <summary>
        /// 设置按钮类型
        /// </summary>
        public EnumButtonType ButtonType
        {
            get { return (EnumButtonType)ViewState["ButtonType"]; }
            set { ViewState["ButtonType"] = value; }
            //get
            //{
            //    return _buttontype;
            //}
            //set
            //{
            //    _buttontype = value;
            //}
        }       

        private bool _applyonclickevent = true;
        /// <summary>
        /// 是否添加客户端onclick事件
        /// </summary>
        public bool ApplyOnClickEvent
        {
            get
            {
                return _applyonclickevent;
            }
            set
            {
                _applyonclickevent = value;
            }

        }

        /// <summary>
        /// 重写属性输出函数
        /// </summary>
        /// <param name="writer">要写出到的 HTML 编写器</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.Attributes.Add("class", "btn_mouseout");
            this.Attributes.Add("onmouseover", "this.className='btn_mouseover'");
            this.Attributes.Add("onmouseout", "this.className='btn_mouseout'");
            this.Attributes.Add("onmousedown", "this.className='btn_mousedown'");
            this.Attributes.Add("onmouseup", "this.className='btn_mouseup'");
            switch (this.ButtonType)
            {
                case EnumButtonType.Add:
                    this.Text = WebControlLocalization.GetText("Button_Add");
                    if (this.ApplyOnClickEvent)
                        this.Attributes.Add("onclick", "this.blur();");
                    break;
                case EnumButtonType.Save:
                    this.Text = WebControlLocalization.GetText("Button_Save");
                    if (this.ApplyOnClickEvent)
                        this.Attributes.Add("onclick", "this.blur();");
                    break;
                case EnumButtonType.Delete:
                    this.Text = WebControlLocalization.GetText("Button_Delete");
                    if (this.ApplyOnClickEvent)
                        this.Attributes.Add("onclick", "this.blur();if (zrWebCheckBox_CheckHasData('chkGroup1')) {return confirm('您确定要删除所选内容吗？');}else {alert('您还未选择任何记录！');return false;}");
                    break;
                case EnumButtonType.ResetPassword:
                    this.Text = WebControlLocalization.GetText("Button_ResetPassword");
                    if (this.ApplyOnClickEvent)
                        this.Attributes.Add("onclick", "this.blur();return confirm('" + WebControlLocalization.GetText("Button_ResetConfirmation") + "');");
                    break;

            }
            base.AddAttributesToRender(writer);
        }
    }
}
