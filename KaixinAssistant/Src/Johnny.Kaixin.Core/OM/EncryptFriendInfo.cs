using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class EncryptFriendInfo
    {
        private string _id;
        private string _name;

        public EncryptFriendInfo()
        {
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
       
        public override string ToString()
        {
            if (!String.IsNullOrEmpty(_name))
                return _name + "(" + _id + ")";
            else
                return base.ToString();
        }

    }
}
