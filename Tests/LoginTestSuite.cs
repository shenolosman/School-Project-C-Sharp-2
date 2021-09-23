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
            _loginManager.LoginUser("user", "pwd");
            _loginManager.LoginUser("user", "pwd_diff");

            Assert.True(_loginManager.CheckSameUsers("user", "pwd"));
            Assert.False(_loginManager.CheckSameUsers("user", "pwd_diff"));
        }
    }
}
