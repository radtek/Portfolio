using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

namespace Johnny.Controls.Web.Calendar
{
	/// <summary>
	/// ZteDateTextbox 的摘要说明。
	/// </summary>
	public class DateTextboxDesigner:System.Web.UI.Design.ControlDesigner
	{
		public DateTextboxDesigner():base()
		{
			
		}
		// Returns the html to use to represent the control at design time.
		public override string GetDesignTimeHtml()
		{
			DateTextBox ctl = (DateTextBox)Component;
			string html = base.GetDesignTimeHtml();		
			StringWriter sw = new StringWriter();
			HtmlTextWriter tw = new HtmlTextWriter(sw);

            System.Web.UI.WebControls.TextBox text1 = new System.Web.UI.WebControls.TextBox();
			text1 = ctl;
            text1.RenderBeginTag(tw);		   		
			return sw.ToString() ;                 
		}        
	}
}
