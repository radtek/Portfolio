using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnny.Controls.Web
{
    /// <summary>
    /// ҳ��Ļ��ࡣ /// 
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        public PageBase()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //   
        }
        /// <summary>
        /// ģ������
        /// </summary>
        public String ModuleName
        {
            set { ViewState["ModuleName"] = value; }
            get { return ViewState["ModuleName"].ToString(); }
        }
        private string _Message;
        /// <summary>
        /// ���û���ʾ��Ϣ��ʾ
        /// </summary>
        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        /// <summary>
        /// ����Ƿ����ض���Ȩ��
        /// </summary>
        /// <param name="sec">��ȫѡ��</param>
        /// <returns></returns>
        //  public bool CheckPermissionSuccess(Framework.SecurityOption sec)
        //  {
        //   //TODO:ʵ��Framework.Security�࣬��������޸ġ�����Ȩ��
        //   return Framework.Security.CheckValid(this.ModuleName,sec);
        //  }
        /// <summary>
        /// ҳ��˵�PlaceHolder
        /// </summary>
        public System.Web.UI.WebControls.PlaceHolder plhTopHolder;
        /// <summary>
        /// ҳ��׶˵�PlaceHolder
        /// </summary>
        public System.Web.UI.WebControls.PlaceHolder plhBottomHolder;

        protected override void OnInit(EventArgs e)
        {
            //��ʼ���ؼ�
            plhTopHolder = new PlaceHolder();
            plhBottomHolder = new PlaceHolder();

            //��Ӷ���PlaceHolder
            Control form1 = this.FindControl("Form1");
            if (form1 != null) form1.Controls.AddAt(0, plhTopHolder);

            //���ҳü���û��Զ���ؼ�
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
            //��ӵ׶�PlaceHolder
            Control form1 = this.FindControl("Form1");
            if (form1 != null) form1.Controls.Add(plhBottomHolder);
            //���ҳ�ŵ��û��Զ���ؼ�
            //ITemplate Footer = Page.LoadTemplate("~/Controls/Footer.ascx");
            //this.plhBottomHolder.Controls.Add(Footer);

        }
        private void PageBase_Error(object sender, EventArgs e)
        {
#if !Debug
            //   Exception exc = Server.GetLastError();
            //   ��¼δ����Ĵ���
            //   XMLLog.AddErrorLog(exc,userName);
            //   Server.Transfer("~/PageError.aspx?error=" + Server.HtmlEncode(exc.Message));
#endif
        }
        private void PageBase_PreRender(object sender, EventArgs e)
        {
            //�����Ϣ��ʾ
            if (this._Message != null && this._Message != String.Empty)
            {
                LiteralControl litMessage = new LiteralControl("<div class=\"CssMessage\"><p>" + Message + "</p></div>");
                plhTopHolder.Controls.Add(litMessage);
            }
        }
    }
}

