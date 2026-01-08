using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            test.AssignDevice("Lenovo");
            GetTestCategory();
            GetTestAuthor();
        }


        [Test, Author("Luhlelo"), Category("smoke")]
        public void AddTwoNumber()
        {
            TestContext.Out.WriteLine($"Sum {num} and {num2}");
            test.Log(Status.Info, $"Sum {num} and {num2}");
            Assert.That(Add(num,num2),Is.EqualTo(7));
            
        }
        [Test, Author("Zoe")]
        [Category("smoke")]
        public void AddTwoNumbers_Fail()
        {
            num = 3;
            test.Info("Intentionally failing test this test");
            AddTwoNumber();
        }

        [Test, Author("Zoe")]
        [Category("regression")]
        public void AddtwoNumber_Skipped()
        {
            test.Info("Skipping a test");
            Assert.Ignore("Purpose of this excercise is skip this test and show extent report");
            Add(num,num2);
        }
        [Test, Category("regresssion"), Author("Gcobisa")]
        [TestCase(5,5,ExpectedResult =10)]
        [TestCase(-10,5,ExpectedResult =-5)]
        
        public int SumNumberFromTestCase(int num,int num2)
        {
            TestContext.Out.WriteLine($"Sum {num} and {num2}");
            test.Log(Status.Info, $"Sum {num} and {num2}");
            return Add(num,num2);   
        }
        [Test, Author("Luhlelo")]
        [Category("Sanity")]
        public void Warning()
        {
            int expected = 4;
            int actual = 0;
            test.Info($"Expected value {expected} and Actual value {actual}");
            Assert.Warn("Values do not match, but this is only a warning");
        }

        [Test]
        [Category("Sanity"),Author("Luhlelo")]
        public void VallideAddvalue()
        {
            test.Info($"Sum {num} and {num2}");
            AddValues(num,num2);
            Assert.Warn("Add Value method have a bug");
        }

        [Test, Retry(3), Author("Luhlelo")]
        [Category("sanity")]
        public void TestRetryAttribute()
        {
            int randomNumber = new Random().Next(1, 4);
            int num2 = 2;
            test.Info($"Compare {randomNumber} and {num2}");
            Assert.That(randomNumber, Is.EqualTo(num2));
        }

        [TearDown]
        public void TearDown()
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
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int AddValues(int a, int b)
        {
            return a + a;
        }
        #endregion
    }
}
