using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Core
{
    /*

1. Kunna registrera ny användare & lösenord +

2. Kunna logga in med användare & lösenord +

3. Inte kunna registrera samma användare två gånger om +

4. Bara kunna registrera användarnamn med engelska bokstäver (a-z, A-Z) siffor (0-9) och specialtecken (-_) som är max 16 karaktärer långa

5. Bara kunna registrera lösenord med bokstäver (a-z, A-Z) siffor (0-9) och specialtecken (!”#¤%&/()=?-_*’) som är max 16 karaktärer långa

6. Bara kunna registrera lösenord med minst längd 8 och minst en siffra och ett specialtecken

7. Spara ner användare & lösenord (t.ex. till en .txt fil) vid registrering

8. Kolla nersparade användares lösenord vid inloggning

9. Inaktivera användarens lösenord efter ett år. Tips: spara ner datumen tillsammans med lösenorden för att inte ”glömma bort”

    */
    public class LoginManager
    {
        public string _username;
        public string _password;
        public DateTime _dateForInactive;
        public LoginManager()
        {
            _dateForInactive = DateTime.Today;
        }

        public void RegisterUser(string username, string password)
        {
            if (_username != username)
            {
                _username = username;
                _password = password;
            }
        }
        public bool LoginUser(string username, string password)
        {
            bool success = username == _username && password == _password;
            return success;
        }

        public bool CheckSameUsers(string username, string password)
        {
            if (_username == username && _password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidUsername(string username)
        {
            char[] notAllowed = "^åä¨ö'.,½€£${[]}\"~!”#¤%&/()=?*".ToCharArray();
            if (username.IndexOfAny(notAllowed) == -1 && (username.Length <= 16 && username.Length > 3))
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
    }
}
