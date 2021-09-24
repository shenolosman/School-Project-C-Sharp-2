using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace Core
{
    public class LoginManager
    {
        public List<User> usersList = new();
        static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        readonly string adress = Path.Combine(path, "Downloads", "test.txt");
        public bool RegisterUser(string username, string password)
        {
           var  userName = username.ToLower();
            if (!CheckSameUsers(userName) && ValidUsername(userName) && ValidPassword(password))
            {
                User user = new User(userName, password);
                usersList.Add(user);
                return true;
            }
            return false;
        }
        public bool Login(string username, string password)
        {
            var userName = username.ToLower();
            foreach (var user in usersList)
            {
                if (user._username.ToLower().Equals(userName) && user._password.Equals(password))
                    return true;
            }
            return false;
        }
        public bool CheckSameUsers(string username)
        {
            var userName = username.ToLower();
            foreach (var user in usersList)
            {
                if (user._username.ToLower().Equals(userName))
                    return true;
            }
            return false;
        }
        public bool ValidUsername(string username)
        {
            var userName = username.ToLower();
            char[] notAllowed = "^åä¨ö'.,½€£${[]}\"~!”#¤%&/()=?*".ToCharArray();
            if (userName.IndexOfAny(notAllowed) == -1 && userName.Length is <= 16 and > 3)
            {
                return true;
            }
            return false;
        }
        public bool ValidPassword(string password)
        {
            var passwordChars = password.ToCharArray();
            const string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWYZ";
            const string numbers = "1234567890";
            const string specialChars = "!”#¤%&/()=?-_*’";
            if (!string.IsNullOrEmpty(password))
            {
                foreach (var x in passwordChars)
                {
                    if (
                        !(password.Length >= 8 && password.Length < 17)
                        ||
                        !(numbers.Contains(x) ||
                          letters.Contains(x) ||
                          specialChars.Contains(x)
                          )
                        )
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void SaveUsernamePasswordInFile(string username, string password)
        {
            using var sw = new StreamWriter(adress);
            sw.WriteLine("Username: {0},Password: {1}", username, password);
        }
        public string ReadFromFile(string user)
        {
            string text;
            using var sr = new StreamReader(adress);
            text = sr.ReadLine();
            return text.Substring(text.LastIndexOf(":") + 2);
        }
    }
}
