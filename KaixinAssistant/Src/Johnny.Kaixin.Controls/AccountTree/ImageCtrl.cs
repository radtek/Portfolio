using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.Reflection;
using System.Resources;

namespace Johnny.Kaixin.Controls.AccountTree
{
    public class ImageCtrl
    {        
        public static Image GetIconFromResx(string imagename)
        {
            Image img;
            //get resource
            Assembly myAssem = Assembly.GetEntryAssembly();
            ResourceManager rm = new ResourceManager(typeof(Johnny.Kaixin.Controls.AccountTree.MyResource));

            if (rm == null)
                return null;

            switch (imagename)
            {
                case TreeConstants.IMAGE_REFRESH:
                    img = (Image)rm.GetObject(TreeConstants.IMAGE_REFRESH);
                    break;                
                default:
                    img = null;
                    break;
            }

            return img;
        }
    }
}
