using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom13_DeTai10
{
    public class ListUser
    {
        private static ListUser instances;

        public static ListUser Instances
        {
            get 
            { 
                if(instances == null)
                {
                    instances = new ListUser();
                }
                return ListUser.instances; 
            }
            set { ListUser.instances = value; }
        }

        private List<User> listAccountUser;

        public List<User> ListAccountUser
        {
            get { return listAccountUser; }
            set { listAccountUser = value; }
        }

        private ListUser()
        {
            listAccountUser = new List<User>();
            // tài khoản Admin
            listAccountUser.Add(new User("truongan", "123456", true));
            listAccountUser.Add(new User("vietthanh", "123456",true));

            // tài khoản Nhân Viên
            listAccountUser.Add(new User("truonganNV", "123456789", false));
            listAccountUser.Add(new User("vietthanhNV", "123456789", false));
        }
    }
}
