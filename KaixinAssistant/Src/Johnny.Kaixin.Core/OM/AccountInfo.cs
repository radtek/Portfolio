using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Johnny.Kaixin.Core
{    
    [DisplayName("�ʺ�")]
    [Serializable]
    public class AccountInfo
    {
        private string _email;
        private string _password;
        private string _username;
        private string _userid;
        private bool _gender;

        public AccountInfo()
        { }

        public AccountInfo(string email, string password, string username, string userid, bool gender)
        {
            _email = email;
            _password = password;
            _username = username;
            _userid = userid;
            _gender = gender;
        }

        [Category("�ʺ�")]
        [DisplayName("�ʼ���ַ")]
        [Description("��¼��������Email����")]
        [ReadOnly(true)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Category("�ʺ�")]
        [DisplayName("����")]
        [Description("��¼���������˺�����")]
        [ReadOnly(true)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Category("�ʺ�")]
        [DisplayName("����")]
        [Description("����")]
        [ReadOnly(true)]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        [Category("�ʺ�")]
        [DisplayName("�û�ID")]
        [Description("�û�ID")]
        [ReadOnly(true)]
        public string UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        [Category("�ʺ�")]
        [DisplayName("�Ա�")]
        [Description("�Ա�")]
        [ReadOnly(true)]
        public bool Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public AccountInfo Clone()
        {
            AccountInfo newAcc = new AccountInfo();
            newAcc.Email = this.Email;
            newAcc.Password = this.Password;
            newAcc.UserName = this.UserName;
            newAcc.UserId = this.UserId;
            newAcc.Gender = this.Gender;

            return newAcc;
        }

        public override string ToString()
        {
            if (_username != null && _username != string.Empty)
                return _username;
            else if (_email != null && _email != string.Empty)
                return _email;
            else
                return base.ToString();
        }
    }
}
