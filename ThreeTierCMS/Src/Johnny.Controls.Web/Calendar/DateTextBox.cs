using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;


namespace Johnny.Controls.Web.Calendar
{
  //  [DesignerAttribute(typeof(DateTextboxDesigner), typeof(IDesigner)),
  //DefaultProperty("Text"), ToolboxData("<{0}:WebCalendar runat=server></{0}:WebCalendar>")]
    public class DateTextBox : Johnny.Controls.Web.TextBox.TextBox
    {
        /// <summary>
        /// ��Ҫ������ʵ�����ڶ�����ѡ��ö��
        /// </summary>
        public enum LanguageType
        {
            Chinese,
            English
        }

        private LanguageType m_language; //������������ö�ٱ���

        /// <summary>
        /// ������������ʾ������ʾ���ͣ���/Ӣ��
        /// </summary>
        [
            Description("Language Choose")
        ]
        public LanguageType Language
        {
            get
            {
                return m_language;
            }
            set
            {
                m_language = value;
            }
        }

        private bool m_IsDisplayTime = true; //�Ƿ���ʾʱ��
        /// <summary>
        /// �����Ƿ���ʾʱ��
        /// </summary>
        public bool IsDisplayTime
        {
            get
            {
                return m_IsDisplayTime;
            }
            set
            {
                m_IsDisplayTime = value;
            }

        }

        /// <summary>
        /// ����ѡ������������/ʱ��
        /// </summary>
        public DateTime GetDateTime
        {
            get
            {
                if (this.Text.Trim() == string.Empty)
                {
                    return DateTime.MinValue;
                }
                return Convert.ToDateTime(this.Text.Trim());
            }
        }

        /// <summary>
        /// �����ı���󳤶�
        /// </summary>
        public override int MaxLength
        {
            get
            {
                if (this.IsDisplayTime)
                {
                    return 19;
                }
                else
                {
                    return 10;
                }
            }
        }

        /// <summary>
        /// �������������ؿͻ��˽ű�ע������
        /// </summary>
        private string ScriptName
        {
            get
            {
                return "DateSelector";
            }
        }

        /// <summary>
        /// ��дHtml�������
        /// </summary>
        /// <param name="writer">Ҫд������ HTML ��д��</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.ScriptName))
            {
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(),this.ScriptName,this.GetJavascript(),false);
                //this.Page.ClientScript.RegisterClientScriptInclude(this.ScriptName, "javascript/calendar.js");
                //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ScriptName, "<script language='javascript' src='javascript/calendar.js" + "'></script>");
            }
            base.Render(writer);            
        }

        /// <summary>
        /// ��д�����������
        /// </summary>
        /// <param name="writer">Ҫд������ HTML ��д��</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            //System.Web.UI.HtmlControls.HtmlGenericControl script = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            //script.Attributes.Add("type", "text/javascript");
            //script.Attributes.Add("scr", "·��");
            //script.InnerText = "�ű���Ϣ";
            //this.Header.Controls.Add(script);

            this.Attributes.Add("onfocus", "new Calendar().show(this);");
            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// ����ͻ��˽ű�
        /// </summary>
        /// <returns></returns>
        private string GetJavascript()
        {
            JavaScriptWriter js = new JavaScriptWriter(true);
            js.AddLine("document.write(\"<div id=meizzCalendarLayer style='position: absolute; z-index: 9999; width: 151; height:" + (IsDisplayTime ? "193":"171") + "; display: none'>\");");
            js.AddLine("document.write(\"<iframe id=meizzCalendarIframe scrolling=no frameborder=0 width=151px height=193px></iframe></div>\");");
            js.AddLine("function writeIframe()");
            js.AddLine("{");
            js.AddLine("    var strIframe = \"<html><head><meta http-equiv='Content-Type' content='text/html;'><style>\"+");
            js.AddLine("    \"*{font-size: 12px; font-family: Arial,Verdana}\"+");
            js.AddLine("    \".bg{  color: \"+ WebCalendar.lightColor +\"; cursor: default; background-color: \"+ WebCalendar.darkColor +\";}\"+");
            js.AddLine("    \"table#tableMain{ width: 149; height: " + (IsDisplayTime ? "180" : "162") + ";}\"+");
            js.AddLine("    \"table#tableWeek td{ color: \"+ WebCalendar.lightColor +\";}\"+");
            js.AddLine("    \"table#tableDay  td{ font-weight: bold;}\"+");
            js.AddLine("    \"td#meizzYearHead, td#meizzYearMonth{color: \"+ WebCalendar.wordColor +\"}\"+");
            js.AddLine("    \".out { text-align: center; border-top: 1px solid \"+ WebCalendar.DarkBorder +\"; border-left: 1px solid \"+ WebCalendar.DarkBorder +\";\"+");
            js.AddLine("    \"border-right: 1px solid \"+ WebCalendar.lightColor +\"; border-bottom: 1px solid \"+ WebCalendar.lightColor +\";}\"+");
            js.AddLine("    \".over{ text-align: center; border-top: 1px solid #FFFFFF; border-left: 1px solid #FFFFFF;\"+");
            js.AddLine("    \"border-bottom: 1px solid \"+ WebCalendar.DarkBorder +\"; border-right: 1px solid \"+ WebCalendar.DarkBorder +\"}\"+");
            js.AddLine("    \"</style></head><body onselectstart='return false' style='margin: 0px' oncontextmenu='return false'><form id=meizz name=meizz>\";");
            js.AddLine("");
            js.AddLine("    strIframe += \"<scr\"+\"ipt language=javascript>\"+");
            js.AddLine("    \"document.onkeydown=function(){ var evt = window.event || arguments.callee.caller.arguments[0]; switch(evt.keyCode){  case 27 : parent.hiddenCalendar(); break;\"+");
            js.AddLine("    \"case 37 : parent.prevM(); break; case 38 : parent.prevY(); break; case 39 : parent.nextM(); break; case 40 : parent.nextY(); break;\"+");
            js.AddLine("    \"case 84 : document.forms[0].today.click(); break;} window.event.keyCode = 0; window.event.returnValue= false;}</scr\"+\"ipt>\";");
            js.AddLine("");
            js.AddLine("    strIframe += \"<select name=tmpYearSelect  onblur='parent.hiddenSelect(this)' style='z-index:1;position:absolute;top:3;left:18;display:none'\"+");
            js.AddLine("    \" onchange='parent.WebCalendar.thisYear =this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>\"+");
            js.AddLine("    \"<select name=tmpMonthSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:3;left:78;display:none'\"+");
            js.AddLine("    \" onchange='parent.WebCalendar.thisMonth=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>\"+");
            js.AddLine("    \"<select name=tmpHourSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:171;left:1;display:none'\"+");
            js.AddLine("    \" onchange='parent.WebCalendar.thisHour=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>\"+");
            js.AddLine("    \"<select name=tmpMinuteSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:171;left:45;display:none'\"+");
            js.AddLine("    \" onchange='parent.WebCalendar.thisMinute=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>\"+");
            js.AddLine("    \"<select name=tmpSecondSelect onblur='parent.hiddenSelect(this)' style='z-index:1; position:absolute;top:171;left:95;display:none'\"+");
            js.AddLine("    \" onchange='parent.WebCalendar.thisSecond=this.value; parent.hiddenSelect(this); parent.writeCalendar();'></select>\"+");
            js.AddLine("");
            js.AddLine("    \"<table id=tableMain class=bg border=0 cellspacing=2 cellpadding=0>\"+");
            js.AddLine("    \"<tr><td bgcolor='\"+ WebCalendar.lightColor +\"'>\"+");
            js.AddLine("    \"    <table width=147 id=tableHead border=0 cellspacing=1 cellpadding=0><tr align=center>\"+");
            js.AddLine("    \"    <td width=16 height=19 class=bg title='" + ((Language == LanguageType.Chinese) ? "��ǰ�� 1 ��&#13;��ݼ�����" : "previous month &#13;shortcut key:<-") + "' style='cursor: hand' onclick='parent.prevM()'><b>&lt;</b></td>\"+");
            js.AddLine("    \"    <td width=63 id=meizzYearHead  title='" + ((Language == LanguageType.Chinese) ? "����˴�ѡ�����" : "click here and select the year") + "' onclick='parent.funYearSelect(parseInt(this.innerText, 10))'\"+");
            js.AddLine("    \"        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'\"+");
            js.AddLine("    \"        onmouseout='this.bgColor=parent.WebCalendar.lightColor; this.style.color=parent.WebCalendar.wordColor'></td>\"+");
            js.AddLine("    \"    <td width=52 id=meizzYearMonth title='" + ((Language == LanguageType.Chinese) ? "����˴�ѡ���·�" : "click here and select the month") + "' onclick='parent.funMonthSelect(parseInt(this.innerText, 10))'\"+");
            js.AddLine("    \"        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'\"+");
            js.AddLine("    \"        onmouseout='this.bgColor=parent.WebCalendar.lightColor; this.style.color=parent.WebCalendar.wordColor'></td>\"+");
            js.AddLine("    \"    <td width=16 class=bg title='" + ((Language == LanguageType.Chinese)? "��� 1 ��&#13;��ݼ�����" : "next month&#13;shortcut key:->") + "' onclick='parent.nextM()' style='cursor: hand'><b>&gt;</b></td></tr></table>\"+");
            js.AddLine("    \"</td></tr><tr><td height=20><table id=tableWeek border=1 width=147 cellpadding=0 cellspacing=0 \";");
            js.AddLine("");
            js.AddLine("    strIframe += \" borderColorLight='\"+ WebCalendar.darkColor +\"' borderColorDark='\"+ WebCalendar.lightColor +\"'>\"+");
            js.AddLine("    \"    <tr align=center>\"+");
            if(Language == LanguageType.Chinese)
                js.AddLine("\"<td height=20 title='������'>��</td><td title='����һ'>һ</td><td title='���ڶ�'>��</td><td title='������'>��</td><td title='������'>��</td><td title='������'>��</td><td title='������'>��</td></tr></table>\"+");
            else
                js.AddLine("\"<td height=20 title='Sunday'>S</td><td title='Monday'>M</td><td title='Tuesday'>T</td><td title='Wednesday'>W</td><td title='Thursday'>T</td><td title='Friday'>F</td><td title='Saturday'>S</td></tr></table>\"+");
            js.AddLine("    \"</td></tr><tr><td valign=top width=147 bgcolor='\"+ WebCalendar.lightColor +\"'>\"+");
            js.AddLine("    \"    <table id=tableDay height=120 width=147 border=0 cellspacing=1 cellpadding=0>\";");
            js.AddLine("         for(var x=0; x<5; x++){ strIframe += \"<tr>\";");
            js.AddLine("         for(var y=0; y<7; y++)  strIframe += \"<td class=out id='meizzDay\"+ (x*7+y) +\"'></td>\"; strIframe += \"</tr>\";}");
            js.AddLine("         strIframe += \"<tr>\";");
            js.AddLine("         for(var x=35; x<37; x++) strIframe += \"<td class=out id='meizzDay\"+ x +\"'></td>\";");
            js.AddLine("         strIframe +=\"<td colspan=5 class=out title=''><span onclick='parent.CalendarNull()' title='" + ((Language == LanguageType.Chinese)? "�������ÿ�'>�ÿ�" : "clear date text'>Clear") + "</span>&nbsp;&nbsp;\"+");
            js.AddLine("         \"<span onclick='parent.CalendarToday()' title='" + ((Language == LanguageType.Chinese)? "��ǰ����ʱ��'>��ǰ" : "get current date'>Now") + "</span>&nbsp;&nbsp;\" + ");
            js.AddLine("         \"<span onfocus='this.blur()' onclick='parent.hiddenCalendar()' title='" + ((Language == LanguageType.Chinese) ? "�ر�����'>�ر�" : "close the selection'>Close") + "</span></td></tr></table>\"+");
            js.AddLine("         \"</td></tr>\" +");

            if (IsDisplayTime)
            {
                js.AddLine("         \"<tr><td bgcolor='\"+ WebCalendar.lightColor +\"'><table border=0 cellpadding=0 cellspacing=1 width=147><tr align='center' height=18 bgcolor='\"+ WebCalendar.darkColor +\"'><td width=49 id=meizzHourHead  title='" + ((Language == LanguageType.Chinese) ? "����˴�ѡ��Сʱ" : "click here and select the hour") + "' onclick='parent.funHourSelect(parseInt(this.innerText, 10))'\"+");
                js.AddLine("    \"        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'\"+");
                js.AddLine("    \"        onmouseout='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.wordColor'></td>\"+");
                js.AddLine("    		 \"<td width=49 id=meizzMinuteHead  title='" + ((Language == LanguageType.Chinese) ? "����˴�ѡ�����" : "click here and select the minute") + "' onclick='parent.funMinuteSelect(parseInt(this.innerText, 10))'\"+");
                js.AddLine("    \"        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'\"+");
                js.AddLine("    \"        onmouseout='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.wordColor'></td>\"+");
                js.AddLine("    		 \"<td width=49 id=meizzSecondHead  title='" + ((Language == LanguageType.Chinese) ? "����˴�ѡ����" : "click here and select the second") + "' onclick='parent.funSecondSelect(parseInt(this.innerText, 10))'\"+");
                js.AddLine("    \"        onmouseover='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.lightColor'\"+");
                js.AddLine("    \"        onmouseout='this.bgColor=parent.WebCalendar.darkColor; this.style.color=parent.WebCalendar.wordColor'></td></tr>\"+");
            }
            js.AddLine("    		 \"</table></table></form></body></html>\";");
            js.AddLine("    with(WebCalendar.iframe)");
            js.AddLine("    {");
            js.AddLine("        document.writeln(strIframe); document.close();");
            js.AddLine("        for(var i=0; i<37; i++)");
            js.AddLine("        {");
            js.AddLine("            WebCalendar.dayObj[i] = eval(\"meizzDay\"+ i);");
            js.AddLine("            WebCalendar.dayObj[i].onmouseover = dayMouseOver;");
            js.AddLine("            WebCalendar.dayObj[i].onmouseout  = dayMouseOut;");
            js.AddLine("            WebCalendar.dayObj[i].onclick     = returnDate;");
            js.AddLine("        }");
            js.AddLine("    }");
            js.AddLine("}");

            #region ��������
            //��ʼ������������
            js.AddLine("function WebCalendar() //��ʼ������������");
            js.AddLine("{");
            js.AddLine("    this.daysMonth  = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);");
            js.AddLine("    this.day        = new Array(37);");//��������չʾ�õ�����
            js.AddLine("    this.dayObj     = new Array(37);");//��������չʾ�ؼ�����
            js.AddLine("    this.dateStyle  = null;");//�����ʽ������������
            js.AddLine("    this.objExport  = null;                     //�����ش�����ʾ�ؼ�");
            js.AddLine("    this.eventSrc   = null;                     //������ʾ�Ĵ����ؼ�");
            js.AddLine("    this.inputDate  = null;                     //ת��������������(d/m/yyyy)");
            js.AddLine("    this.thisYear   = new Date().getFullYear();");//������ı����ĳ�ʼֵ
            js.AddLine("    this.thisMonth  = new Date().getMonth()+ 1;");//�����µı����ĳ�ʼֵ
            js.AddLine("    this.thisDay    = new Date().getDate();");//�����յı����ĳ�ʼֵ
            js.AddLine("    this.thisHour   = new Date().getHours();");//����ʱ�ı����ĳ�ʼֵ
            js.AddLine("    this.thisMinute = new Date().getMinutes();");//����ֵı����ĳ�ʼֵ
            js.AddLine("    this.thisSecond = new Date().getSeconds();");//������ı����ĳ�ʼֵ
            js.AddLine("    this.today      = this.thisDay +\"/\"+ this.thisMonth +\"/\"+ this.thisYear;   //����(d/m/yyyy)");
            js.AddLine("    this.iframe     = document.getElementById(\"meizzCalendarIframe\"); //������ iframe ����");
            js.AddLine("    this.calendar   = document.getElementById(\"meizzCalendarLayer\");  //�����Ĳ�");
            js.AddLine("    this.dateReg    = \"\";           //������ʽ��֤������ʽ");
            js.AddLine("");
            js.AddLine("    this.yearFall   = 50;           //����������������ֵ");
            if (IsDisplayTime)
            {
                js.AddLine("this.timeShow   = true;");//�Ƿ񷵻�ʱ��
            }
            else
            {
                js.AddLine("this.timeShow = false;");//�Ƿ񷵻�ʱ��
            }
            js.AddLine("    this.format     = \"yyyy-mm-dd\"; ");//�ش����ڵĸ�ʽ
            js.AddLine("    this.darkColor  = \"#FF6347\";    ");//�ؼ��İ�ɫ
            js.AddLine("    this.lightColor = \"#FFFFFF\";    ");//�ؼ�����ɫ
            js.AddLine("    this.btnBgColor = \"#FFF5A0\";    ");//�ؼ��İ�ť����ɫ
            js.AddLine("    this.wordColor  = \"#000040\";    ");//�ؼ���������ɫ
            js.AddLine("    this.wordDark   = \"#DCDCDC\";    ");//�ؼ��İ�������ɫ
            js.AddLine("    this.dayBgColor = \"#FFFACD\";    ");//�������ֱ���ɫ
            js.AddLine("    this.todayColor = \"#FF9933\";    ");//�����������ϵı�ʾ����ɫ
            js.AddLine("    this.DarkBorder = \"#FFE4C4\";    ");//������ʾ��������ɫ
            js.AddLine("}   var WebCalendar = new WebCalendar();");

            //��������
            js.AddLine("function calendar()");
            js.AddLine("{");
            js.AddLine("    var evt = window.event || arguments.callee.caller.arguments[0];"); // ��ȡevent����
            js.AddLine("    var src = evt.srcElement || evt.target;"); // ��ȡ�����¼���Դ����
            js.AddLine("    var e = src;   writeIframe();");
            js.AddLine("    var o = WebCalendar.calendar.style; WebCalendar.eventSrc = e;");
            js.AddLine("	if (arguments.length == 0) WebCalendar.objExport = e;");
            js.AddLine("    else WebCalendar.objExport = eval(arguments[0]);");
            js.AddLine("");
            js.AddLine("    WebCalendar.iframe.tableWeek.style.cursor = \"default\";");
            js.AddLine("	var t = e.offsetTop,  h = e.clientHeight, l = e.offsetLeft, p = e.type;");
            js.AddLine("	while (e = e.offsetParent){t += e.offsetTop; l += e.offsetLeft;}");
            js.AddLine("    o.display = \"\"; WebCalendar.iframe.document.body.focus();");
            js.AddLine("    var cw = WebCalendar.calendar.clientWidth, ch = WebCalendar.calendar.clientHeight;");
            js.AddLine("    var dw = document.body.clientWidth, dl = document.body.scrollLeft, dt = document.body.scrollTop;");
            js.AddLine("    ");
            js.AddLine("    if (document.body.clientHeight + dt - t - h >= ch) o.top = (p==\"image\")? t + h : t + h + 6;");
            js.AddLine("    else o.top  = (t - dt < ch) ? ((p==\"image\")? t + h : t + h + 6) : t - ch;");
            js.AddLine("    if (dw + dl - l >= cw) o.left = l; else o.left = (dw >= cw) ? dw - cw + dl : dl;");
            js.AddLine("");
            js.AddLine(@"    if (!WebCalendar.timeShow) WebCalendar.dateReg = /^(\d{1,4})(-|\/|.)(\d{1,2})\2(\d{1,2})$/;");
            js.AddLine(@"    else WebCalendar.dateReg = /^(\d{1,4})(-|\/|.)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;");
            js.AddLine("");
            js.AddLine("    try{");
            js.AddLine("        if (WebCalendar.objExport.value.trim() != \"\"){");
            js.AddLine("            WebCalendar.dateStyle = WebCalendar.objExport.value.trim().match(WebCalendar.dateReg);");
            js.AddLine("            if (WebCalendar.dateStyle == null)");
            js.AddLine("            {");
            js.AddLine("                writeCalendar(); return false;");
            js.AddLine("            }");
            js.AddLine("            else");
            js.AddLine("            {");
            js.AddLine("                WebCalendar.thisYear   = parseInt(WebCalendar.dateStyle[1], 10);");
            js.AddLine("                WebCalendar.thisMonth  = parseInt(WebCalendar.dateStyle[3], 10);");
            js.AddLine("                WebCalendar.thisDay    = parseInt(WebCalendar.dateStyle[4], 10);");
            js.AddLine("                ");
            js.AddLine("                WebCalendar.inputDate  = parseInt(WebCalendar.thisDay, 10) +\"/\"+ parseInt(WebCalendar.thisMonth, 10) +\"/\"+ ");
            js.AddLine("                parseInt(WebCalendar.thisYear, 10); ");

            if (IsDisplayTime)
            {
                js.AddLine("                if (WebCalendar.timeShow)");
                js.AddLine("                {");
                js.AddLine("                  WebCalendar.thisHour   = parseInt(WebCalendar.dateStyle[5], 10);");
                js.AddLine("                  WebCalendar.thisMinute = parseInt(WebCalendar.dateStyle[6], 10);");
                js.AddLine("                  WebCalendar.thisSecond = parseInt(WebCalendar.dateStyle[7], 10);}");
            }
            js.AddLine("                writeCalendar();");
            js.AddLine("            }");
            js.AddLine("        }  else writeCalendar();");
            js.AddLine("    }  catch(e){writeCalendar();}");
            js.AddLine("}");

            //�·ݵ�������
            js.AddLine("function funMonthSelect() ");
            js.AddLine("{");
            js.AddLine("    var m = isNaN(parseInt(WebCalendar.thisMonth, 10)) ? new Date().getMonth() + 1 : parseInt(WebCalendar.thisMonth);");
            js.AddLine("    var e = WebCalendar.iframe.document.forms[0].tmpMonthSelect;");
            if (Language == LanguageType.Chinese)
                js.AddLine("    for (var i=1; i<13; i++) e.options.add(new Option(i +\"��\", i));");
            else
                js.AddLine("    for (var i=1; i<13; i++) e.options.add(new Option(EngMonth(i),i));");
            js.AddLine("    e.style.display = \"\"; e.value = m; e.focus(); window.status = e.style.top;");
            js.AddLine("}");
            js.AddLine("function funYearSelect() ");//��ݵ�������
            js.AddLine("{");
            js.AddLine("    var n = WebCalendar.yearFall;");
            js.AddLine("    var e = WebCalendar.iframe.document.forms[0].tmpYearSelect;");
            js.AddLine("    var y = isNaN(parseInt(WebCalendar.thisYear, 10)) ? new Date().getFullYear() : parseInt(WebCalendar.thisYear);");
            js.AddLine("        y = (y <= 1000)? 1000 : ((y >= 9999)? 9999 : y);");
            js.AddLine("    var min = (y - n >= 1000) ? y - n : 1000;");
            js.AddLine("    var max = (y + n <= 9999) ? y + n : 9999;");
            js.AddLine("        min = (max == 9999) ? max-n*2 : min;");
            js.AddLine("        max = (min == 1000) ? min+n*2 : max;");
            js.AddLine("    for (var i=min; i<=max; i++) e.options.add(new Option(i +\"" + ((LanguageType.Chinese == Language) ? "��" : " ") + "\", i));");
            js.AddLine("    e.style.display = \"\"; e.value = y; e.focus();");
            js.AddLine("}");
            js.AddLine("function prevM()  ");//��ǰ���·�
            js.AddLine("{");
            js.AddLine("    WebCalendar.thisDay = 1;");
            js.AddLine("    if (WebCalendar.thisMonth==1)");
            js.AddLine("    {");
            js.AddLine("        WebCalendar.thisYear--;");
            js.AddLine("        WebCalendar.thisMonth=13;");
            js.AddLine("    }");
            js.AddLine("    WebCalendar.thisMonth--; writeCalendar();");
            js.AddLine("}");
            js.AddLine("function nextM()  //�����·�");
            js.AddLine("{");
            js.AddLine("    WebCalendar.thisDay = 1;");
            js.AddLine("    if (WebCalendar.thisMonth==12)");
            js.AddLine("    {");
            js.AddLine("        WebCalendar.thisYear++;");
            js.AddLine("        WebCalendar.thisMonth=0;");
            js.AddLine("    }");
            js.AddLine("    WebCalendar.thisMonth++; writeCalendar();");
            js.AddLine("}");
            js.AddLine("function prevY(){WebCalendar.thisDay = 1; WebCalendar.thisYear--; writeCalendar();}//��ǰ�� Year");
            js.AddLine("function nextY(){WebCalendar.thisDay = 1; WebCalendar.thisYear++; writeCalendar();}//���� Year");
            js.AddLine("function hiddenSelect(e){for(var i=e.options.length; i>-1; i--)e.options.remove(i); e.style.display=\"none\";}");
            //js.AddLine("function getObjectById(id){ alert(document.all);if(document.all) return(eval(\"document.all.\"+ id)); return(eval(id)); }");
            js.AddLine("function hiddenCalendar(){document.getElementById(\"meizzCalendarLayer\").style.display = \"none\";};");
            js.AddLine("function appendZero(n){return((\"00\"+ n).substr((\"00\"+ n).length-2));}//�����Զ��������");
            js.AddLine(@"String.prototype.Trim=function(){return this.replace(/(^\s*)|(\s*$)/g,'');};");
            //js.AddLine(@"function String.prototype.trim(){return this.replace(/(^\s*)|(\s*$)/g,"""");}");
            js.AddLine("function dayMouseOver()");
            js.AddLine("{");
            js.AddLine("    this.className = \"over\";");
            js.AddLine("    this.style.backgroundColor = WebCalendar.darkColor;");
            js.AddLine("    if(WebCalendar.day[this.id.substr(8)].split(\"/\")[1] == WebCalendar.thisMonth)");
            js.AddLine("    this.style.color = WebCalendar.lightColor;");
            js.AddLine("}");
            js.AddLine("function dayMouseOut()");
            js.AddLine("{");
            js.AddLine("    this.className = \"out\"; var d =WebCalendar.day[this.id.substr(8)], a = d.split(\"/\");");
            js.AddLine("    this.style.removeAttribute('backgroundColor');");
            js.AddLine("    if(a[1] == WebCalendar.thisMonth && d != WebCalendar.today)");
            js.AddLine("    {");
            js.AddLine("        if(WebCalendar.dateStyle && a[0] == parseInt(WebCalendar.dateStyle[4], 10))");
            js.AddLine("        this.style.color = WebCalendar.lightColor;");
            js.AddLine("        this.style.color = WebCalendar.wordColor;");
            js.AddLine("    }");
            js.AddLine("}");
            js.AddLine("function writeCalendar() //��������ʾ�����ݵĴ������");
            js.AddLine("{");
            js.AddLine("    var y = WebCalendar.thisYear;");
            js.AddLine("    var m = WebCalendar.thisMonth; ");
            js.AddLine("    var d = WebCalendar.thisDay;");
            js.AddLine("    WebCalendar.daysMonth[1] = (0==y%4 && (y%100!=0 || y%400==0)) ? 29 : 28;");
            js.AddLine("    if (!(y<=9999 && y >= 1000 && parseInt(m, 10)>0 && parseInt(m, 10)<13 && parseInt(d, 10)>0)){");
            js.AddLine("        alert(\"�Բ����������˴�������ڣ�\" + y + \"/\" + m + \"/\" + d);");
            js.AddLine("        WebCalendar.thisYear   = new Date().getFullYear();");
            js.AddLine("        WebCalendar.thisMonth  = new Date().getMonth()+ 1;");
            js.AddLine("        WebCalendar.thisDay    = new Date().getDate(); }");
            js.AddLine("    y = WebCalendar.thisYear;");
            js.AddLine("    m = WebCalendar.thisMonth;");
            js.AddLine("    d = WebCalendar.thisDay;");
            js.AddLine("    WebCalendar.iframe.meizzYearHead.innerText  = y +\" " + ((LanguageType.Chinese == Language) ? "��" : " ") + "\";");
            //js.AddLine("    WebCalendar.iframe.meizzYearMonth.innerText = parseInt(m, 10) +\" ��\";");
            js.AddLine("    WebCalendar.iframe.meizzYearMonth.innerText = " + ((LanguageType.Chinese == Language) ? "parseInt(m, 10) +\" ��\"" : "EngMonth(parseInt(m, 10))") + ";");
            js.AddLine("    WebCalendar.daysMonth[1] = (0==y%4 && (y%100!=0 || y%400==0)) ? 29 : 28; //�������Ϊ29��");
            js.AddLine("    ");
            js.AddLine("    var w = new Date(y, m-1, 1).getDay();");
            js.AddLine("    var prevDays = m==1  ? WebCalendar.daysMonth[11] : WebCalendar.daysMonth[m-2];");
            js.AddLine("    for(var i=(w-1); i>=0; i--) ");//������ for ѭ��Ϊ����������Դ������ WebCalendar.day����ʽ�� d/m/yyyy
            js.AddLine("    {");
            js.AddLine("        WebCalendar.day[i] = prevDays +\"/\"+ (parseInt(m, 10)-1) +\"/\"+ y;");
            js.AddLine("        if(m==1) WebCalendar.day[i] = prevDays +\"/\"+ 12 +\"/\"+ (parseInt(y, 10)-1);");
            js.AddLine("        prevDays--;");
            js.AddLine("    }");
            js.AddLine("    for(var i=1; i<=WebCalendar.daysMonth[m-1]; i++) WebCalendar.day[i+w-1] = i +\"/\"+ m +\"/\"+ y;");
            js.AddLine("    for(var i=1; i<37-w-WebCalendar.daysMonth[m-1]+1; i++)");
            js.AddLine("    {");
            js.AddLine("        WebCalendar.day[WebCalendar.daysMonth[m-1]+w-1+i] = i +\"/\"+ (parseInt(m, 10)+1) +\"/\"+ y;");
            js.AddLine("        if(m==12) WebCalendar.day[WebCalendar.daysMonth[m-1]+w-1+i] = i +\"/\"+ 1 +\"/\"+ (parseInt(y, 10)+1);");
            js.AddLine("    }");
            js.AddLine("    for(var i=0; i<37; i++)    ");//���ѭ���Ǹ���Դ����д����������ʾ
            js.AddLine("    {");
            js.AddLine("        var a = WebCalendar.day[i].split(\"/\");");
            js.AddLine("        WebCalendar.dayObj[i].innerText    = a[0];");
            js.AddLine("        WebCalendar.dayObj[i].title        = a[2] +\"-\"+ appendZero(a[1]) +\"-\"+ appendZero(a[0]);");
            js.AddLine("        WebCalendar.dayObj[i].bgColor      = WebCalendar.dayBgColor;");
            js.AddLine("        WebCalendar.dayObj[i].style.color  = WebCalendar.wordColor;");
            js.AddLine("        if ((i<10 && parseInt(WebCalendar.day[i], 10)>20) || (i>27 && parseInt(WebCalendar.day[i], 10)<12))");
            js.AddLine("            WebCalendar.dayObj[i].style.color = WebCalendar.wordDark;");
            js.AddLine("        if (WebCalendar.inputDate==WebCalendar.day[i])    //�����������������������ϵ���ɫ");
            js.AddLine("        {WebCalendar.dayObj[i].bgColor = WebCalendar.darkColor; WebCalendar.dayObj[i].style.color = WebCalendar.lightColor;}");
            js.AddLine("        if (WebCalendar.day[i] == WebCalendar.today)      //���ý����������Ϸ�Ӧ��������ɫ");
            js.AddLine("        {WebCalendar.dayObj[i].bgColor = WebCalendar.todayColor; WebCalendar.dayObj[i].style.color = WebCalendar.lightColor;}");
            js.AddLine("    }");

            if (IsDisplayTime)
            {
                js.AddLine("    if(WebCalendar.timeShow)");
                js.AddLine("    {");
                js.AddLine("       var h = WebCalendar.thisHour;");
                js.AddLine("       var mi= WebCalendar.thisMinute; ");
                js.AddLine("       var s = WebCalendar.thisSecond;");
                js.AddLine("       if(!(h<=23 && h>=0 && mi <=59 && m >=0 && s <= 59 && s >= 0)){");
                js.AddLine("      	  WebCalendar.thisHour   = new Date().getHours();");
                js.AddLine("      		WebCalendar.thisMinute = new Date().getMinutes();");
                js.AddLine("      		WebCalendar.thisSecond = new Date().getSeconds();");
                js.AddLine("      		h = WebCalendar.thisHour;");
                js.AddLine("      		mi= WebCalendar.thisMinute; ");
                js.AddLine("    	  	s = WebCalendar.thisSecond;");
                js.AddLine("    	 }");
                js.AddLine("    	 WebCalendar.iframe.meizzHourHead.innerText    = h +\" " + ((LanguageType.Chinese == Language) ? "ʱ" : " hr") + "\";");
                js.AddLine("    	 WebCalendar.iframe.meizzMinuteHead.innerText  = mi +\" " + ((LanguageType.Chinese == Language) ? "��" : " mi") + "\";");
                js.AddLine("    	 WebCalendar.iframe.meizzSecondHead.innerText  = s +\" " + ((LanguageType.Chinese == Language) ? "��" : " s") + "\";");
                js.AddLine("    }");
            }
            js.AddLine("}");
            js.AddLine("function returnDate() //�������ڸ�ʽ�ȷ����û�ѡ��������");
            js.AddLine("{");
            js.AddLine("    if(WebCalendar.objExport)");
            js.AddLine("    {");
            js.AddLine("        var returnValue;");
            js.AddLine("        var a = (arguments.length==0) ? WebCalendar.day[this.id.substr(8)].split(\"/\") : arguments[0].split(\"/\");");
            js.AddLine(@"        var d = WebCalendar.format.match(/^(\w{4})(-|\/|.|)(\w{1,2})\2(\w{1,2})$/);");
            js.AddLine("        if(d==null){alert(\"���趨�����������ʽ���ԣ�\"); return false;}");
            js.AddLine("        var flag = d[3].length==2 || d[4].length==2; ");//�жϷ��ص����ڸ�ʽ�Ƿ�Ҫ����
            js.AddLine("        returnValue = flag ? a[2] +d[2]+ appendZero(a[1]) +d[2]+ appendZero(a[0]) : a[2] +d[2]+ a[1] +d[2]+ a[0];");

            if (IsDisplayTime)
            {
                js.AddLine("        if(WebCalendar.timeShow)");
                js.AddLine("        {");
                js.AddLine("            var h = WebCalendar.thisHour;");
                js.AddLine("            var m = WebCalendar.thisMinute;");
                js.AddLine("            var s = WebCalendar.thisSecond;");
                js.AddLine("            returnValue += flag ? \" \"+ appendZero(h) +\":\"+ appendZero(m) +\":\"+ appendZero(s) : \" \"+  h  +\":\"+ m +\":\"+ s;");
                js.AddLine("        }");
            }
            js.AddLine("        WebCalendar.objExport.value = returnValue;");
            js.AddLine("        hiddenCalendar();");
            js.AddLine("    }");
            js.AddLine("}");
            js.AddLine("document.onclick=function(e)");
            js.AddLine("{");
            js.AddLine("    var evt = e||window.event"); // ��ȡevent����     
            js.AddLine("    var src = evt.srcElement || evt.target; "); // ��ȡ�����¼���Դ����           
            js.AddLine("    if(WebCalendar.eventSrc != src) hiddenCalendar();");
            js.AddLine("};");

            if (IsDisplayTime)
            {
                js.AddLine("//���� Сʱ������");
                js.AddLine("function funHourSelect(strHour) //Сʱ��������");
                js.AddLine("{");
                js.AddLine("	if (!WebCalendar.timeShow){return;}");
                js.AddLine("	");
                js.AddLine("    var h = isNaN(parseInt(WebCalendar.thisHour, 10)) ? new Date().getHours() : parseInt(WebCalendar.thisHour);");
                js.AddLine("    var e = WebCalendar.iframe.document.forms[0].tmpHourSelect;");
                js.AddLine("    for (var i=0; i<24; i++) e.options.add(new Option(i+\"" + ((LanguageType.Chinese == Language) ? "ʱ" : " hr") + "\", i));");
                js.AddLine("    e.style.display = \"\"; e.value = h; e.focus();");
                js.AddLine("}");
                js.AddLine("");
                js.AddLine("function funMinuteSelect(strMinute) //���ӵ�������");
                js.AddLine("{");
                js.AddLine("	if (!WebCalendar.timeShow){return;}");
                js.AddLine("	");
                js.AddLine("    var mi = isNaN(parseInt(WebCalendar.thisMinute, 10)) ? new Date().getMinutes() : parseInt(WebCalendar.thisMinute);");
                js.AddLine("    var e = WebCalendar.iframe.document.forms[0].tmpMinuteSelect;");
                js.AddLine("    for (var i=0; i<60; i++) e.options.add(new Option(i+\"" + ((LanguageType.Chinese == Language) ? "��" : " mi") + "\", i));");
                js.AddLine("    e.style.display = \"\"; e.value = mi; e.focus();");
                js.AddLine("}");
                js.AddLine("");
                js.AddLine("function funSecondSelect(strSecond) //���������");
                js.AddLine("{");
                js.AddLine("	if (!WebCalendar.timeShow){return;}");
                js.AddLine("	");
                js.AddLine("    var m = isNaN(parseInt(WebCalendar.thisSecond, 10)) ? new Date().getSeconds() : parseInt(WebCalendar.thisSecond);");
                js.AddLine("    var e = WebCalendar.iframe.document.forms[0].tmpSecondSelect;");
                js.AddLine("    for (var i=0; i<60; i++) e.options.add(new Option(i+\"" + ((LanguageType.Chinese == Language) ? "��" : " s") + "\", i));");
                js.AddLine("    e.style.display = \"\"; e.value = m; e.focus();");
                js.AddLine("}");
            }

            js.AddLine("function CalendarToday()	//Today Button");
            js.AddLine("{");
            js.AddLine("  if(WebCalendar.objExport)");
            js.AddLine("  {");
            js.AddLine("    WebCalendar.thisYear		= new Date().getFullYear();");
            js.AddLine("	  WebCalendar.thisMonth	  = new Date().getMonth()+1;");
            js.AddLine("	  WebCalendar.thisDay		  = new Date().getDate();");
            js.AddLine("    returnValue = WebCalendar.thisYear + \"-\" + appendZero(WebCalendar.thisMonth) + \"-\" + appendZero(WebCalendar.thisDay);");

            if (IsDisplayTime)
            {
                js.AddLine("    if(WebCalendar.timeShow)");
                js.AddLine("    {");
                js.AddLine("      WebCalendar.thisHour		= new Date().getHours();");
                js.AddLine("	    WebCalendar.thisMinute	= new Date().getMinutes();");
                js.AddLine("	    WebCalendar.thisSecond	= new Date().getSeconds();");
                js.AddLine("      returnValue += \" \" + appendZero(WebCalendar.thisHour) + \":\" + appendZero(WebCalendar.thisMinute) + \":\" + appendZero(WebCalendar.thisSecond);");
                js.AddLine("    }");
            }
            js.AddLine("    WebCalendar.objExport.value = returnValue;");
            js.AddLine("    hiddenCalendar();");
            js.AddLine("  }");
            js.AddLine("}");
            js.AddLine("function CalendarNull()");
            js.AddLine("{");
            js.AddLine("	WebCalendar.objExport.value = '';");
            js.AddLine("  hiddenCalendar();");
            js.AddLine("}");

            js.AddLine("function EngMonth(iMonth)");
            js.AddLine("{");
            js.AddLine("if(iMonth == 1) return \"Jan.\";");
            js.AddLine("if(iMonth == 2) return \"Feb.\";");
            js.AddLine("if(iMonth == 3) return \"Mar.\";");
            js.AddLine("if(iMonth == 4) return \"Apr.\";");
            js.AddLine("if(iMonth == 5) return \"May.\";");
            js.AddLine("if(iMonth == 6) return \"Jun.\";");
            js.AddLine("if(iMonth == 7) return \"Jul.\";");
            js.AddLine("if(iMonth == 8) return \"Aug.\";");
            js.AddLine("if(iMonth == 9) return \"Sep.\";");
            js.AddLine("if(iMonth == 10) return \"Oct.\";");
            js.AddLine("if(iMonth == 11) return \"Nov.\";");
            js.AddLine("if(iMonth == 12) return \"Dec.\";");
            js.AddLine("}");
            #endregion

            return js.ToString();
        }
    }
}



