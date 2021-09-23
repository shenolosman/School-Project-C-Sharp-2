using System;

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

        public bool ValidUsername()
        {

            return false;
        }

        public bool ValidPassword()
        {

            return false;
        }
    }
}
