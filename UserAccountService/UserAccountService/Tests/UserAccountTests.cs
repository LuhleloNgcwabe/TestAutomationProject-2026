using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Text;
using UserAccountService.Utility;

namespace UserAccountService.Tests
{
    [TestFixture,Explicit("exlude this test fixture to Run practiseTest class")]
    public class UserAccountTests: ExtentManager
    {
        string username;
        [OneTimeSetUp]
        public void StartReport() => Init();
        [SetUp]
        public void initTestData()
        {
            username = "admin";
            TestContext.Out.WriteLine($"Start {TestContext.CurrentContext.Test.Name} test");
            test.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Value.Log(Status.Info, $"start {TestContext.CurrentContext.Test.Name} test.");
            MapCategory();
        }

        [Test, Category("UsernameVallidation")]
        public void Username_ShouldNotBeEmpty()
        {
            test.Value?.Log(Status.Info, $"Varify usename is not empty. username value {username}");
            Assert.That(!username.Equals(""));
        }

        [TestCase("admin",Category ="Regression")]
        [TestCase("user1")]
        [TestCase("guest")]
        public void Username_ShouldNotBeEmpty_parameterised(string username)
        {
            test.Value?.Log(Status.Info, $"Varify usename is not empty. username value {username}");
            Assert.That(!username.Equals(""));
        }

        [Test]
        [Category("Smoke")]
        [Property("Tag", "UserValidation")]
        public void Smoke_UsernameValidation()
        {
            test.Value?.Log(Status.Info, "valllidate user name");
            TestContext.Out.WriteLine("valllidate user name");
            Assert.That(true);
        }

        [Test, Category("Sanity")]
        [Retry(2)]
        public void Flaky_Test_Simulation()
        {
            Random rnd = new Random();
            test.Value?.Log(Status.Info, "Simulate flaky test choose random number");
            Assert.That(rnd.Next(1, 3),Is.EqualTo(2));
        }

        [Test,Category("Smoke")]
        [Ignore("Testing skip status")]
        public void CreatingUser_WithEmptyName_ShouldFail()
        {
            Assert.Ignore("Message inside test");
            test.Value?.Log(Status.Info,"Runs exception handling test");
            Assert.Throws<ArgumentException>(() => throw new ArgumentException());
        }

        [TearDown]
        public void tearDownTestData() {
            TestStatus();
            Flush();
            test.Value?.Log(Status.Info, $"Clean up");
            username = "";
        }
    }
}
