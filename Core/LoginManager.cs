using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Core
{

    public class LoginManager
    {
        public List<User> usersList = new();
        static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        readonly string adress = Path.Combine(path, "Downloads", "test.txt");
        public bool RegisterUser(string username, string password, DateTime tid)
        {
            var userName = username.ToLower();
            if (!CheckSameUsers(userName) && ValidUsername(userName) && ValidPassword(password))
            {
                usersList.Add(new User(userName, password, tid));
                return true;
            }
            return false;
        }
        public bool Login(string username, string password)
        {
            var userName = username.ToLower();
            foreach (var user in usersList)
            {
                if (user._username.ToLower().Equals(userName) && user._password.Equals(password) && PassCheck(username))
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
        public bool PassCheck(string username)
        {
            foreach (var user in usersList)
            {
                if (user._username == username)
                {
                    if (user._registeredDate > DateTime.Now.AddDays(-365))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void SaveFile()
        {
            using FileStream ladda = new FileStream(adress, FileMode.Append, FileAccess.Write);
            var sw = new StreamWriter(ladda);
            foreach (var user in usersList)
            {
                sw.WriteLine($"{user._username},{user._password},{user._registeredDate}");
            }
            sw.Dispose();
        }
        public List<User> ReadFile()
        {
            if (File.Exists(adress))
            {
                using (StreamReader
                    reader = new StreamReader(adress, Encoding.Default,
                        false))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        var split = line.Split(',');
                        usersList.Add(new User(split[0], split[1], DateTime.Parse(split[2])));
                    }
                }
            }
            return usersList;
        }
    }
}

