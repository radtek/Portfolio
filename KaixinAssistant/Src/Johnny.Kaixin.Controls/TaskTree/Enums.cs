using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Controls.TaskTree
{
    [Flags]
    [Serializable]
    public enum NodeType
    {
        Base = 0,
        Task,
        Operation
    }

    public enum IconType
    {
        Tasks = 0,
        Task,
        AddTask,
        Start,
        Pause,
        Stop,
        Delete,
        Operation,
        TaskStart,
        Refresh,
        User,
        Xml
    } 
}
