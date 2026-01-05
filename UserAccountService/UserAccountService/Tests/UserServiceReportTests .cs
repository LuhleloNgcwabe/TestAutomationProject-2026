using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UserAccountService.Utility;

namespace UserAccountService.Tests
{
    [TestFixture,Explicit("run user Account tests only")]
    public class UserServiceReportTests : ExtentManager
    {
        private UserService service;
        [OneTimeSetUp]
        public void StartReport() => Init();

        [SetUp]
        public void CreateTest()
        {
            service = new UserService();
            service.Register("admin", "admin123");
        }

        [Test]
        public void Register_ValidUser_Success()
        {
            test.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            service.Register("user1", "password");
            Assert.Pass();
        }

        [TestCase("admin", "admin123", true)]
        [TestCase("admin", "wrong", false)]
        public void LoginTests(string user, string pass, bool expected)
        {
            test.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Assert.That(expected, Is.EqualTo(service.Login(user, pass)));
        }

        [Test]
        public void Login_UnregisteredUser_ThrowsException()
        {
            test.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Assert.Throws<Exception>(() => service.Login("x", "y"));
        }

        [OneTimeTearDown]
        public void EndReport() {
            Flush();
        }

    }

}
