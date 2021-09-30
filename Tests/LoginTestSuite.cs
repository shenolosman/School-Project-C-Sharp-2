using Core;
using System;
using System.IO;
using Xunit;

namespace Tests
{
    public class LoginTestSuite
    {
        private readonly LoginManager _loginManager;
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
            Assert.False(_loginManager.RegisterUser("usär", "pass123!", tid));
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
            Assert.False(_loginManager.ValidUsername("usär"));
            Assert.False(_loginManager.ValidUsername("u"));
            Assert.False(_loginManager.ValidUsername("12345678901234567"));
        }
        [Fact]
        private void Test_ValidPasswordAllowedChar()
        {
            Assert.True(_loginManager.ValidPassword("123456789_-7"));
            Assert.True(_loginManager.ValidPassword("pass123!"));
            Assert.False(_loginManager.ValidPassword("PÅSSWÅRD-3"));
            Assert.False(_loginManager.ValidPassword("nuh"));
            Assert.False(_loginManager.ValidPassword("12345678901234567"));
        }
        [Fact]
        private void Test_SaveInFile()
        {
            _loginManager.RegisterUser("user", "pass123!", tid);

            _loginManager.RegisterUser("user2", "passqwe123!", tid);
            _loginManager.RegisterUser("usERWer", "pass12asd3!", tid);
            _loginManager.RegisterUser("u12ser", "pass112323!", tid);
            _loginManager.SaveFile();
            Assert.True(File.Exists(adress));
            string control;
            using (var sr = new StreamReader(adress))
            {
                control = sr.ReadLine();
            }
            Assert.Equal("user,pass123!,2021-09-29 00:00:00", control);
        }
        [Fact]
        void Test_InactiveDateForPassword()
        {
            DateTime fakeTime = DateTime.Today - TimeSpan.FromDays(365 * 1);
            Assert.NotEqual(fakeTime, DateTime.Now);

            _loginManager.RegisterUser("user", "password1!", tid);
            _loginManager.RegisterUser("user", "password12!", fakeTime);
            _loginManager.SaveFile();

            Assert.True(_loginManager.Login("user", "password1!"));
            Assert.False(_loginManager.Login("user", "password12!"));
        }
        [Fact]
        void ReadFile()
        {
            var userlist = _loginManager.ReadFile();
            var user = userlist.Find(x => x._username == "user2");
            Assert.Equal("user2", user._username);
        }
    }
}
