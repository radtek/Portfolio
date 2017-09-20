using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Johnny.Kaixin.WinUI
{
    public class FormItem
    {
        private string _id;
        private FrmToolBase _registeredForm = null;

        public FormItem(FrmToolBase registeredForm, string uniqueId)
        {
            this._registeredForm = registeredForm;
            _id = uniqueId;
        }

        public string Id
        {
            [DebuggerStepThrough]
            get { return _id; }

            set
            {
                if (!this._id.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this._id = value;
                    //this.OnPropertyChanged(NgimWindowItem.IdPropertyName);
                }
            }
        }

        public FrmToolBase RegisteredForm
        {
            get
            {
                return _registeredForm;
            }
        }
    }
}
