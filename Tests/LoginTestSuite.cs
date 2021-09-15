using Core;
using System;
using Xunit;

namespace Tests
{
    public class LoginTestSuite
    {
        [Fact]
        private void TestRegisterUser()
        {
            //Arrange
            LoginManager loginManager = new();


            //Act
            loginManager.RegisterNewUser("user", "pass");

            //Assert
            Assert.True(loginManager.CheckSameUsers("user","pass"));

        }

        [Fact]
        private void TestCantRegisterSameUserTwice()
        {
            LoginManager loginManager = new();

            loginManager.RegisterNewUser("user","pwd");
            loginManager.RegisterNewUser("user","pwd_diff");

           Assert.True(loginManager.CheckSameUsers("user", "pwd"));
           Assert.False(loginManager.CheckSameUsers("user", "pwd_diff"));
        }
    }
}
