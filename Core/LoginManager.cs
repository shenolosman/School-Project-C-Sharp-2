using System;

namespace Core
{
    public class LoginManager
    {
        private string _username;
        private string _password;
        public void RegisterNewUser(string username, string password)
        {
            if (_username!=username)
            {
                _username = username;
                _password = password;
            }
        }

        public bool CheckSameUsers(string username, string password)
        {
            if (_username==username && _password==password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
