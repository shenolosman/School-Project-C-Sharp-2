using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{

    public class LoginManager
    {
        private List<User> _usersList;
        private readonly string _address;
        private ITime _date;
        public LoginManager(ITime date)
        {
            _usersList = new List<User>();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            _address = Path.Combine(path, "Downloads", "test.txt");
            _date = date;
        }
        public bool RegisterUser(string username, string password, ITime tid)
        {
            var userName = username.ToLower();
            if (!ValidUsername(userName) || !ValidPassword(password) || CheckSameUsers(userName))
                return false;

            _usersList.Add(new User(userName, password, tid.Today()));
            //SaveFile();
            return true;
        }
        public bool Login(string username, string password, ITime tid)
        {
            if (_usersList==null)
            {
                _usersList = ReadFile();
            }
            var userName = username.ToLower();
            foreach (var user in _usersList)
            {
                TimeSpan timeSpan = tid.Today() - user._registeredDate;
                int years = timeSpan.Days / 365;
                if (user._username.ToLower().Equals(userName) && user._password.Equals(password))
                {
                    if (years == 1)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
        private bool CheckSameUsers(string username)
        {
            var userName = username.ToLower();
            foreach (var user in _usersList)
            {
                if (user._username.ToLower().Equals(userName))
                    return true;
            }
            return false;
        }
        private bool ValidUsername(string username)
        {
            var userName = username.ToLower();
            char[] notAllowed = "^åä¨ö'.,½€£${[]}\"~!”#¤%&/()=?*".ToCharArray();
            if (userName.IndexOfAny(notAllowed) == -1 && userName.Length is <= 16 and >= 3)
            {
                return true;
            }
            return false;
        }
        private bool ValidPassword(string password)
        {
            var passwordChars = password.ToCharArray();
            const string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWYZ";
            const string numbers = "1234567890";
            const string specialChars = "!”#¤%&/()=?-_*’";
            bool isDigit = false;
            bool isSpecialChars = false;
            bool isLetters = false;
            if (string.IsNullOrEmpty(password)) return false;

            foreach (var character in passwordChars)
            {
                if (numbers.Contains(character))
                {
                    isDigit = true;
                }

                if (specialChars.Contains(character))
                {
                    isSpecialChars = true;
                }

                if (letters.Contains(character))
                {
                    isLetters = true;
                }
            }
            return isDigit& isSpecialChars & isLetters & (password.Length >= 8 & password.Length <= 16);
        }
        public void SaveFile()
        {
            using FileStream ladda = new FileStream(_address, FileMode.Append, FileAccess.Write);
            var sw = new StreamWriter(ladda);
            foreach (var user in _usersList)
            {
                sw.WriteLine($"{user._username},{user._password},{user._registeredDate}");
            }
            sw.Dispose();
        } public List<User> ReadFile()
        {
            if (File.Exists(_address))
            {
                using (StreamReader
                    reader = new StreamReader(_address, Encoding.Default,
                        false))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        var split = line.Split(',');
                        _usersList.Add(new User(split[0], split[1], DateTime.Parse(split[2])));
                    }
                }
            }
            return _usersList;
        }
    }
}

