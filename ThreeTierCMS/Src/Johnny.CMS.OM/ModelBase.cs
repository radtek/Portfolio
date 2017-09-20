using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.CMS.OM
{

    [Serializable]
    public abstract class ModelBase
    {
        public ModelBase()
        {
        }

        protected bool isdisplay;
        protected string updateby;
        protected DateTime updatetime;
        protected string addby;
        protected DateTime addtime;

        public ModelBase(bool isdisplay, string updateby, DateTime updatetime, string addby, DateTime addtime)
        {
            this.isdisplay = isdisplay;
            this.updateby = updateby;
            this.updatetime = updatetime;
            this.addby = addby;
            this.addtime = addtime;
        }

        public bool IsDisplay
        {
            get { return isdisplay; }
            set { isdisplay = value; }
        }

        public string UpdateBy
        {
            get { return updateby; }
            set { updateby = value; }
        }

        public DateTime UpdateTime
        {
            get { return updatetime; }
            set { updatetime = value; }
        }

        public string AddBy
        {
            get { return addby; }
            set { addby = value; }
        }

        public DateTime AddTime
        {
            get { return addtime; }
            set { addtime = value; }
        }
    }
}
