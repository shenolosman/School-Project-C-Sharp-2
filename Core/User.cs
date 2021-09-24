using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
  public  class User
    {
        public string _username;
        public string _password;
        public DateTime _registeredDate;
        public User(string name,string pass,DateTime nowTime)
        {
            _username = name;
            _password = pass;
            _registeredDate =nowTime;
        }
    }
}
