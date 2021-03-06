using System;
using System.Collections.ObjectModel;

namespace Johnny.Kaixin.WinUI
{
    public class FormCollection : Collection<FormItem>
    {
        public FormCollection()
        { }

        public FormItem this[string id]
        {
            get
            {
                foreach (FormItem frm in this)
                {
                    if (string.Equals(frm.Id, id))
                        return frm;
                }

                return null;
            }
        }

        public FormItem this[FrmToolBase registeredForm]
        {
            get
            {
                foreach (FormItem fi in this)
                {
                    if (fi.RegisteredForm == registeredForm)
                        return fi;
                }

                return null;
            }
        }    
    }
}
