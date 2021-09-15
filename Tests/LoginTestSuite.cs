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
            loginManager.RegisterNewUser("user","pass");

            //Act


            //Assert


        }
        [Fact]
        public void UsernameAndPasswordRegistrationTest()
        {
            LoginManager loginManager = new LoginManager();
        }
    }
}
