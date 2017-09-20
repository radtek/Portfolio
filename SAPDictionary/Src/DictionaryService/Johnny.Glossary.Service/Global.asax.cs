using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Configuration;

namespace Johnny.Glossary.Service 
{
	/// <summary>
	///		Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(Object sender, EventArgs e)
		{
            SQLiteHelper.SetConnectionString("Data Source=" + Server.MapPath("~") + "demo.db");
            //SQLiteHelper.SetConnectionString(ConfigurationManager.ConnectionStrings["dictionarydb"].ConnectionString);
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
	}
}

