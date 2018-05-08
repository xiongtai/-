using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNet
{
    public class UserInfoService : IUserInfoSevice
    {
        public string UserName { get; set; }
        public Person Person { get; set; }
        public string ShowMsg()
        {
            return "Hello World " + UserName+" Age is "+Person.Age;
        }

        public UserInfoService()
        {
            this.Person = new Person() { Age = 102 };
        }
    }
}
