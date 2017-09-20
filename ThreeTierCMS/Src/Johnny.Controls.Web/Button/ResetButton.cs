using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web.Button
{
    public class ResetButton : System.Web.UI.HtmlControls.HtmlInputReset
    {
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

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            this.Attributes.Add("type", "reset");
            this.Attributes.Add("value", WebControlLocalization.GetText("ResetButton_Reset"));
            this.Attributes.Add("class", "btn_mouseout");
            this.Attributes.Add("onmouseover", "this.className='btn_mouseover'");
            this.Attributes.Add("onmouseout", "this.className='btn_mouseout'");
            this.Attributes.Add("onmousedown", "this.className='btn_mousedown'");
            this.Attributes.Add("onmouseup", "this.className='btn_mouseup'");
            if (this.ApplyOnClickEvent)
                this.Attributes.Add("onclick", "this.blur();");
            base.RenderAttributes(writer);
        }        
    }
}
