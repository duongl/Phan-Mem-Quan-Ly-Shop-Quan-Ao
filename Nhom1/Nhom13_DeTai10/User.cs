using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom13_DeTai10
{
    public class User
    {
        private string userName;
        private string passWord;

        private bool accountType;

        public bool AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public User(string userName, string passWord, bool accountType)
        {
            this.UserName = userName;
            this.PassWord = passWord;
            this.AccountType = accountType;
        }
    }
}
