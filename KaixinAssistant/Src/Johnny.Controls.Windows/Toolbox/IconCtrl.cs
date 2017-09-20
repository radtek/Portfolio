using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.Reflection;
using System.Resources;

namespace Johnny.Controls.Windows.Toolbox
{
    public class IconCtrl
    {
        public static Image GetIconFromResx(string iconname)
        {
            Image image;
            //get resource
            Assembly myAssem = Assembly.GetEntryAssembly();
            ResourceManager rm = new ResourceManager(typeof(Johnny.Controls.Windows.Toolbox.MyResource));

            if (rm == null)
                return null;

            switch (iconname)
            {
                case ToolboxConstants.ICON_PLUS:
                    image = (Image)rm.GetObject(ToolboxConstants.ICON_PLUS);
                    break;
                case ToolboxConstants.ICON_MINUS:
                    image = (Image)rm.GetObject(ToolboxConstants.ICON_MINUS);
                    break;
                default:
                    image = null;
                    break;
            }

            return image;
        }
    }
}
