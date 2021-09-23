using Core;
using System;
using Xunit;

namespace Tests
{
    public class LoginTestSuite
    {
        private LoginManager _loginManager;
        public LoginTestSuite()
        {
            _loginManager = new LoginManager();
        }
        [Fact]
        private void Test_RegisterUser()
        {
            _loginManager.RegisterUser("user", "pass");
            Assert.Equal("user", _loginManager._username);
            Assert.Equal("pass", _loginManager._password);

        }
        [Fact]
        private void Test_UserLogin()
        {
            _loginManager.LoginUser("user", "pass");
            Assert.True(_loginManager.LoginUser("user", "pass"));
        }

        [Fact]
        private void Test_CantRegisterSameUserTwice()
        {
            _loginManager.RegisterUser("user", "pass");
            _loginManager.RegisterUser("user", "pass2");

            Assert.True(_loginManager.CheckSameUsers("user", "pass"));
            Assert.False(_loginManager.CheckSameUsers("user", "pass2"));
        }

        [Fact]
        private void Test_LoginFalseUsername()
        {
            _loginManager.RegisterUser("user", "pass");
            Assert.False(_loginManager.LoginUser("user", "pass123"));
        }
        [Fact]
        private void Test_LoginFalsePassword()
        {
            _loginManager.RegisterUser("user", "pass");
            Assert.False(_loginManager.LoginUser("user123", "pass"));
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
    }
}
