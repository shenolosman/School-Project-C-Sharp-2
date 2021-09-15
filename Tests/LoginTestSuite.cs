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


        }
    }
}
