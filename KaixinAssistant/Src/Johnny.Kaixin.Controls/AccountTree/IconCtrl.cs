using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.Reflection;
using System.Resources;

namespace Johnny.Kaixin.Controls.AccountTree
{
    public class IconCtrl
    {        
        public static Icon GetIconFromResx(string iconname)
        {
            Icon icon;
            //get resource
            Assembly myAssem = Assembly.GetEntryAssembly();
            ResourceManager rm = new ResourceManager(typeof(Johnny.Kaixin.Controls.AccountTree.MyResource));

            if (rm == null)
                return null;

            switch (iconname)
            {
                case TreeConstants.ICON_USERS:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_USERS);
                    break;
                case TreeConstants.ICON_USER:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_USER);
                    break;
                case TreeConstants.ICON_ADDUSER:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_ADDUSER);
                    break;
                case TreeConstants.ICON_KEYS:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_KEYS);
                    break;
                case TreeConstants.ICON_DELETE:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_DELETE);
                    break;
                case TreeConstants.ICON_WEBBROWSER:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_WEBBROWSER);
                    break;
                case TreeConstants.ICON_OPERATIONCONFIG:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_OPERATIONCONFIG);
                    break;
                case TreeConstants.ICON_ROOT:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_ROOT);
                    break;
                case TreeConstants.ICON_XML:
                    icon = (Icon)rm.GetObject(TreeConstants.ICON_XML);
                    break;
                default:
                    icon = null;
                    break;
            }

            return icon;
        }
    }
}
