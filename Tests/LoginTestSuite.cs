using Core;
using System;
using System.IO;
using Xunit;

namespace Tests
{
    public class LoginTestSuite
    {
        private LoginManager _loginManager;
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string adress = Path.Combine(path, "Downloads", "test.txt");
        private DateTime tid = DateTime.Today;
        public LoginTestSuite()
        {
            _loginManager = new LoginManager();
        }
        [Fact]
        private void Test_RegisterUser()
        {
            Assert.True(_loginManager.RegisterUser("user", "pass123!", tid));
            Assert.False(_loginManager.RegisterUser("User", "pass123!", tid));
            Assert.False(_loginManager.RegisterUser("user", "pass123!", tid));
            Assert.False(_loginManager.RegisterUser("user", "pass", tid));
            Assert.False(_loginManager.RegisterUser("us‰r", "pass123!", tid));
        }
        [Fact]
        private void Test_UserLogin()
        {
            _loginManager.RegisterUser("user", "pass123!", tid);
            Assert.True(_loginManager.Login("user", "pass123!"));
            _loginManager.RegisterUser("user", "pass", tid);
            Assert.False(_loginManager.Login("user", "pass"));
        }
        [Fact]
        private void Test_CantRegisterSameUserTwice()
        {
            Assert.False(_loginManager.CheckSameUsers("user"));
            _loginManager.RegisterUser("user", "pass", tid);
            Assert.False(_loginManager.CheckSameUsers("user"));
        }
        [Fact]
        private void Test_LoginFalseUsername()
        {
            _loginManager.RegisterUser("user", "pass", tid);
            Assert.False(_loginManager.Login("user", "pass123"));
        }
        [Fact]
        private void Test_LoginFalsePassword()
        {
            _loginManager.RegisterUser("user", "pass", tid);
            Assert.False(_loginManager.Login("user123", "pass"));
        }
        [Fact]
        private void Test_ValidUsernameAllowedChar()
        {
            Assert.True(_loginManager.ValidUsername("user"));
            Assert.True(_loginManager.ValidUsername("123456789_-7"));
            Assert.False(_loginManager.ValidUsername("us‰r"));
            Assert.False(_loginManager.ValidUsername("u"));
            Assert.False(_loginManager.ValidUsername("12345678901234567"));
        }
        [Fact]
        private void Test_ValidPasswordAllowedChar()
        {
            Assert.True(_loginManager.ValidPassword("123456789_-7"));
            Assert.True(_loginManager.ValidPassword("pass123!"));
            Assert.False(_loginManager.ValidPassword("P≈SSW≈RD-3"));
            Assert.False(_loginManager.ValidPassword("nuh"));
            Assert.False(_loginManager.ValidPassword("12345678901234567"));
        }
        [Fact]
        private void Test_SaveInFile()
        {
            _loginManager.SaveUsernamePasswordInFile("user", "pass123!");
            Assert.True(File.Exists(adress));
            string control;
            using (var sr = new StreamReader(adress))
            {
                control = sr.ReadLine();
            }
            Assert.Equal("Username: user,Password: pass123!", control);
        }

        [Fact]
        public void Test_SavedPasswordFromFile()
        {
            _loginManager.SaveUsernamePasswordInFile("user", "pass123!");
            var usernameFromFile = _loginManager.ReadFromFile("user");
            var expected = "pass123!";
            Assert.Equal(expected, usernameFromFile);
            _loginManager.SaveUsernamePasswordInFile("user", "p≈ss123!");
            usernameFromFile = _loginManager.ReadFromFile("user");
            Assert.NotEqual(expected, usernameFromFile);
        }

        [Fact]
        void Test_InactiveDateForPassword()
        {
            DateTime fakeTime = DateTime.Today - TimeSpan.FromDays(365 * 1);
            Assert.NotEqual(fakeTime, DateTime.Now);
         
            Assert.False(_loginManager.IsActive("user","pass123!",fakeTime));
        }
    }
}
