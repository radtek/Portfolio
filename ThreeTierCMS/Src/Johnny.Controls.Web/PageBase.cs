using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web
{
    /// <summary>
    /// 页面的基类。 /// 
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        public PageBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //   
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public String ModuleName
        {
            set { ViewState["ModuleName"] = value; }
            get { return ViewState["ModuleName"].ToString(); }
        }
        private string _Message;
        /// <summary>
        /// 向用户显示信息提示
        /// </summary>
        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        /// <summary>
        /// 检查是否有特定的权限
        /// </summary>
        /// <param name="sec">安全选项</param>
        /// <returns></returns>
        //  public bool CheckPermissionSuccess(Framework.SecurityOption sec)
        //  {
        //   //TODO:实现Framework.Security类，如浏览、修改、管理权限
        //   return Framework.Security.CheckValid(this.ModuleName,sec);
        //  }
        /// <summary>
        /// 页最顶端的PlaceHolder
        /// </summary>
        public System.Web.UI.WebControls.PlaceHolder plhTopHolder;
        /// <summary>
        /// 页最底端的PlaceHolder
        /// </summary>
        public System.Web.UI.WebControls.PlaceHolder plhBottomHolder;

        protected override void OnInit(EventArgs e)
        {
            //初始化控件
            plhTopHolder = new PlaceHolder();
            plhBottomHolder = new PlaceHolder();

            //添加顶端PlaceHolder
            Control form1 = this.FindControl("Form1");
            if (form1 != null) form1.Controls.AddAt(0, plhTopHolder);

            //添加页眉的用户自定义控件
            ITemplate Header = Page.LoadTemplate("~/Controls/Header.ascx");
            //this.plhTopHolder.Controls.Add(Header);

            //event
            this.Load += new EventHandler(PageBase_Load);
            this.Error += new EventHandler(PageBase_Error);
            this.PreRender += new EventHandler(PageBase_PreRender);

            base.OnInit(e);
        }
        private void PageBase_Load(object sender, EventArgs e)
        {
            //添加底端PlaceHolder
            Control form1 = this.FindControl("Form1");
            if (form1 != null) form1.Controls.Add(plhBottomHolder);
            //添加页脚的用户自定义控件
            //ITemplate Footer = Page.LoadTemplate("~/Controls/Footer.ascx");
            //this.plhBottomHolder.Controls.Add(Footer);

        }
        private void PageBase_Error(object sender, EventArgs e)
        {
#if !Debug
            //   Exception exc = Server.GetLastError();
            //   记录未处理的错误
            //   XMLLog.AddErrorLog(exc,userName);
            //   Server.Transfer("~/PageError.aspx?error=" + Server.HtmlEncode(exc.Message));
#endif
        }
        private void PageBase_PreRender(object sender, EventArgs e)
        {
            //添加信息提示
            if (this._Message != null && this._Message != String.Empty)
            {
                LiteralControl litMessage = new LiteralControl("<div class=\"CssMessage\"><p>" + Message + "</p></div>");
                plhTopHolder.Controls.Add(litMessage);
            }
        }
    }
}

