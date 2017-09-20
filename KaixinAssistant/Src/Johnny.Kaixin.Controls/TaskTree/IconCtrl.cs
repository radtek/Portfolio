using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.Reflection;
using System.Resources;

namespace Johnny.Kaixin.Controls.TaskTree
{
    public class IconCtrl
    {
        public static Icon GetIconFromResx(string iconname)
        {
            Icon icon;
            //get resource
            Assembly myAssem = Assembly.GetEntryAssembly();
            ResourceManager rm = new ResourceManager(typeof(Johnny.Kaixin.Controls.TaskTree.MyResource));

            if (rm == null)
                return null;

            switch (iconname)
            {
                case TaskConstants.ICON_TASKS:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_TASKS);
                    break;
                case TaskConstants.ICON_TASK:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_TASK);
                    break;
                case TaskConstants.ICON_ADDTASK:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_ADDTASK);
                    break;
                case TaskConstants.ICON_START:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_START);
                    break;
                case TaskConstants.ICON_PAUSE:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_PAUSE);
                    break;
                case TaskConstants.ICON_STOP:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_STOP);
                    break;
                case TaskConstants.ICON_DELETE:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_DELETE);
                    break;
                case TaskConstants.ICON_OPERATION:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_OPERATION);
                    break;
                case TaskConstants.ICON_TASKSTART:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_TASKSTART);
                    break;
                case TaskConstants.ICON_USER:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_USER);
                    break;
                case TaskConstants.ICON_XML:
                    icon = (Icon)rm.GetObject(TaskConstants.ICON_XML);
                    break;
                default:
                    icon = null;
                    break;
            }

            return icon;
        }
    }
}
