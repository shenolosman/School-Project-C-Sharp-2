using Core;
using System;
using System.IO;
using Xunit;

namespace Tests
{
    public class LoginTestSuite
    {
        private LoginManager _loginManager;
        private string _address;
        private MockTime _mockTime;

        public LoginTestSuite()
        {
            _mockTime = new MockTime();
            _loginManager = new LoginManager(_mockTime);
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            _address = Path.Combine(path, "Downloads", "test.txt");
        }
        [Fact]
        private void Test_RegisterUser() //1
        {
            var pass = _loginManager.RegisterUser("user", "pass123_!", _mockTime);
            var fail = _loginManager.RegisterUser("us�r", "pas��${[]}s123!", _mockTime);
            Assert.True(pass);
            Assert.False(fail);
        }
        [Fact]
        private void Test_UserLgin() //2
        {
            var fail = _loginManager.Login("user", "pass123_!", _mockTime);
            _loginManager.RegisterUser("user", "pass123_!", _mockTime);
            var pass = _loginManager.Login("user", "pass123_!", _mockTime);
            Assert.False(fail);
            Assert.True(pass);
        }
        [Fact]
        private void Test_CantRegisterSameUserTwice() //3
        {
            var pass = _loginManager.RegisterUser("user", "pass123!", _mockTime);
            var fail = _loginManager.RegisterUser("user", "pass23!", _mockTime);
            Assert.True(pass);
            Assert.False(fail);
        }
        [Fact]
        private void Test_ValidUsernameAllowedChar()//4
        {
            var till�tnabokstaver = _loginManager.RegisterUser("user", "passw0rd_!", _mockTime);
            var till�tnabokstaver_annanvariant = _loginManager.RegisterUser("123456789_-7", "passw0rd_!", _mockTime);
            var otill�tnabokstaver = _loginManager.RegisterUser("Us�rM�dSv�nka�", "passw0rd_!", _mockTime);
            var l�ngtext = _loginManager.RegisterUser("u", "passw0rd_!", _mockTime);
            Assert.True(till�tnabokstaver);
            Assert.True(till�tnabokstaver_annanvariant);
            Assert.False(otill�tnabokstaver);
            Assert.False(l�ngtext);
        }
        [Fact]
        private void Test_ValidPasswordAllowedChar() //5 && 6
        {
            var till�tnabokstaver = _loginManager.RegisterUser("user", "passw0rd_!", _mockTime); 
            var alltvariant = _loginManager.RegisterUser("user1", "Lsenrd12�%&_!", _mockTime);
            var otill�tnabokstaver = _loginManager.RegisterUser("user2", "12345678a${[]", _mockTime);
            var mer�n16 = _loginManager.RegisterUser("user3", "p�ssw0rd_{[12!p�ssw0rd_{[12!", _mockTime);
            var mindre�n8 = _loginManager.RegisterUser("user4", "123456", _mockTime);
            var barasiffror = _loginManager.RegisterUser("user5", "1234567890", _mockTime);
            var barabokstaver = _loginManager.RegisterUser("user6", "abcdeABCDE", _mockTime);
            var barasrpecialtecken = _loginManager.RegisterUser("user7", "!�#�%&/()=?-_*", _mockTime);
            Assert.True(till�tnabokstaver); 
            Assert.True(alltvariant);
            Assert.False(otill�tnabokstaver);
            Assert.False(mer�n16);
            Assert.False(mindre�n8);
            Assert.False(barasiffror);
            Assert.False(barabokstaver);
            Assert.False(barasrpecialtecken);
        }
        [Fact]
        private void Test_SaveInFile() //7
        {
            _loginManager.RegisterUser("user", "pass123!", _mockTime);
            _loginManager.SaveFile();
            Assert.True(File.Exists(_address));
            _loginManager.ReadFile();
            Assert.True(_loginManager.Login("user", "pass123!", _mockTime));
        }
        [Fact]
        void ReadFile() //8
        {
            _loginManager.RegisterUser("user", "password1!", _mockTime);
            var login = _loginManager.Login("user", "password1!", _mockTime);
            Assert.True(login);
            _loginManager.SaveFile();
            LoginManager lg = new LoginManager(new MockTime());
            lg.ReadFile();
            Assert.True(File.Exists(_address));
            login = _loginManager.Login("user", "password1!", _mockTime);
            Assert.True(login);
        }
        [Fact]
        void Test_InactiveDateForPassword()//9
        {
            _loginManager.RegisterUser("user", "password1!", _mockTime);
            var login = _loginManager.Login("user", "password1!", _mockTime);
            Assert.True(login);
            _mockTime.SetDateTo(DateTime.Today + TimeSpan.FromDays(400));
            login = _loginManager.Login("user", "password1!", _mockTime);
            Assert.False(login);
        }
    }
}
