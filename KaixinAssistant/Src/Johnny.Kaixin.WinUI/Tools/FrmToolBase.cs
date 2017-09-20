using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.WinUI
{
    public class FrmToolBase : FrmBaseCloseMenu
    {
        public delegate void MessageChangedEventHandler(string caption, string key, string message);
        public event MessageChangedEventHandler MessageChanged;

        public FrmToolBase()
        {
            //this.MessageChanged += new MessageChangedEventHandler(SetMessageByParam);
            //this.FormClosing
        }

        public void SetMessageByParam(string caption, string key, string msg)
        {
            if (MessageChanged != null)
                MessageChanged(caption, key, msg);
        }
    }
}
