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
        public bool _isActive;
        public User(string name,string pass)
        {
            _username = name;
            _password = pass;
            _registeredDate = DateTime.Today;
            _isActive = true;
        }
    }
}
