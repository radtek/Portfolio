using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design;
using System.ComponentModel;

using Johnny.Component.Utility;

namespace Johnny.CMS.WebUI.controls
{
    public class Navigator : WebControl
    {
        protected const string HTML1 = "<table cellspacing=\"0\" class=\"sub-nav\"><tr>";
        protected const string HTML2 = "</tr></table>";

        private IList<Johnny.CMS.OM.SystemInfo.Navigator> _navlist = new List<Johnny.CMS.OM.SystemInfo.Navigator>();

        //#region CreateChildControls
        ///// <summary>
        ///// This method is called from base.Render(), and starts the build menu process.
        ///// </summary>
        //protected override void CreateChildControls()
        //{
        //    BuildNavigator();
        //}
        //#endregion

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(HTML1);

            // Call the inherited method
            base.Render(writer);

            //get whole page list from database
            GetWholeNode();

            IList<Johnny.CMS.OM.SystemInfo.Navigator> navigators = new List<Johnny.CMS.OM.SystemInfo.Navigator>();

            foreach (Johnny.CMS.OM.SystemInfo.Navigator item in _navlist)
            {
                if (item.Url == CurrentPageName)
                {
                    navigators.Add(item);
                    navigators = FindParent(item.ParentId, navigators);
                    break;
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int ix = navigators.Count - 1; ix >= 0; ix--)
            {
                sb.Append(string.Format("<td><a href=\"{0}\">{1}</a></td>", navigators[ix].Url, navigators[ix].NavigatorName));
                if (ix != 0)
                    sb.Append("<td class=\"spacer\"><img src=\"images/c-sep.gif\"></td>");
            }
            writer.Write(sb.ToString());
            // Write out a table row closure
            writer.Write(HTML2);

        }
        
        private void GetWholeNode()
        {
            _navlist = CacheUtility.GetCache("Navigator") as IList<Johnny.CMS.OM.SystemInfo.Navigator>;

            if (_navlist == null)
            {
                Johnny.CMS.BLL.SystemInfo.Navigator bll = new Johnny.CMS.BLL.SystemInfo.Navigator();
                try
                {
                    _navlist = bll.GetList();
                }
                catch (Exception ex)
                {
                }
                if (_navlist != null)
                {
                    //add file to cache
                    CacheUtility.InsertCache("Navigator", _navlist);
                }
            }
        }

        private IList<Johnny.CMS.OM.SystemInfo.Navigator> FindParent(int parentid, IList<Johnny.CMS.OM.SystemInfo.Navigator> list)
        {
            if (parentid == 0)
                return list;

            int currentparent = -1;
            foreach (Johnny.CMS.OM.SystemInfo.Navigator item in _navlist)
            {
                if (item.NavigatorId == parentid)
                {
                    currentparent = item.ParentId;
                    list.Add(item);          
                    break;
                }
            }

            if (currentparent > 0)
                return FindParent(currentparent, list);
            else
                return list;
        }

        #region CurrentPageName
        public string CurrentPageName
        {
            get
            {
                object o = ViewState["CurrentPageName"];
                if (o != null)
                    return (string)o;
                else
                    return String.Empty;
            }
            set
            {
                ViewState["CurrentPageName"] = value;
            }
        }
        #endregion
    }
}
