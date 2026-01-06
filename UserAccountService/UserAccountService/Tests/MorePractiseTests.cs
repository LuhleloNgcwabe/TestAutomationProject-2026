using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UserAccountService.Tests
{
    [TestFixture]
    public class MorePractiseTests:ExtentManager2
    {
        int num, num2;
        [OneTimeSetUp]
        public void SetUpReport()
        {
            InitReport();
        }

        [SetUp]
        public void SetUp()
        {
            num = 2;
            num2 = 5;
            TestContext.Out.WriteLine($"Starting {TestContext.CurrentContext.Test.Name} test");
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Info($"Starting {TestContext.CurrentContext.Test.Name} test");
        }

        [Test]
        public void addTwoNumber()
        {
            TestContext.Out.WriteLine($"Sum {num} and {num2}");
            test.Log(Status.Info, $"Sum {num} and {num2}");
            Assert.That(add(num,num2),Is.EqualTo(7));
            
        }
        [Test]
        public void addTwoNumbers_Fail()
        {
            num = 3;
            test.Info("Intentionally failing test this test");
            addTwoNumber();
        }

        [Test]
        public void AddtwoNumber_Skipped()
        {
            test.Info("Skipping a test");
            Assert.Ignore("Purpose of this excercise is skip this test and show extent report");
            add(num,num2);
        }

        [TestCase(5,5,ExpectedResult =10)]
        [TestCase(-10,5,ExpectedResult =-5)]
        public int sumNumberFromTestCase(int num,int num2)
        {
            TestContext.Out.WriteLine($"Sum {num} and {num2}");
            test.Log(Status.Info, $"Sum {num} and {num2}");
            return add(num,num2);
            
        }
        [Test]
        public void warning()
        {
            int expected = 4;
            int actual = 0;
            test.Info($"Expected value {expected} and Actual value {actual}");
            Assert.Warn("Values do not match, but this is only a warning");
        }

        [Test]
        public void vallideAddvalue()
        {
            test.Info($"Sum {num} and {num2}");
            addValues(num,num2);
            Assert.Warn("Add Value method have a bug");
        }
        [TearDown]
        public void tearDown()
        {
            AssignTestStatus();
            TestContext.Out.WriteLine("Reset instance number variable 0");
            test.Info("Reset instance number variable 0");
            num = 0;
            num2 = 0;
        }

        [OneTimeTearDown]
        public void AfterEachTest()
        {
            Flush();
        }

        #region testFeatures
        public int add(int a, int b)
        {
            return a + b;
        }

        public int addValues(int a, int b)
        {
            return a + a;
        }
        #endregion
    }
}
