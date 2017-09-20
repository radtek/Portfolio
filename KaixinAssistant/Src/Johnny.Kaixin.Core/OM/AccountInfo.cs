using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Johnny.Kaixin.Core
{    
    [DisplayName("帐号")]
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

        [Category("帐号")]
        [DisplayName("邮件地址")]
        [Description("登录开心网的Email邮箱")]
        [ReadOnly(true)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Category("帐号")]
        [DisplayName("密码")]
        [Description("登录开心网的账号密码")]
        [ReadOnly(true)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Category("帐号")]
        [DisplayName("姓名")]
        [Description("姓名")]
        [ReadOnly(true)]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        [Category("帐号")]
        [DisplayName("用户ID")]
        [Description("用户ID")]
        [ReadOnly(true)]
        public string UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        [Category("帐号")]
        [DisplayName("性别")]
        [Description("性别")]
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
