using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using ExceptionTestProject.Model;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;

namespace ExceptionTestProject.Test
{
    public class MyTests
    {
        ExtentReports extent;
        ExtentTest test;
        FunctionClass methods;

        [OneTimeSetUp]
        public void setUp()
        {
            extent = new ExtentReports();
            var reporter = new ExtentSparkReporter("ExtentReport.html");
            extent.AttachReporter(reporter);
        }


        [SetUp]
        public void initFunctionClass()
        {
            methods = new FunctionClass();
            string testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
        }

        [Test, Category("regression")]
        public void DoWork_WhenInvalid_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
        }

        [Test, Category("regression")]
        public void DoWork_throwException_WithCorrectMessage()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
            Assert.That(ex.Message, Is.EqualTo("Invalid operation. The method call is invalid for the objects current state."));
        }

        [Test, Category("regression")]
        public void DoWork_WhenValid_DoesNotThrow()
        {
            Assert.Ignore("Method is not yet implemented.");
            Assert.DoesNotThrow(() => methods.DoWork(true));
        }

        [Test, Category("regression")]
        public void process_WhenNull_throwsArgumentNullException()
        {
            //Assert.Throws<ArgumentNullException>(() => methods.Process(null));
            Assert.That(() => methods.Process(null), Throws.TypeOf<ArgumentNullException>());
        }
        [Test, Category("regression")]
        public void process_WhenTooShort_ThrowsArgumentException()
        {
            //Assert.Throws<ArgumentException>(() => methods.Process("ab"));
            Assert.That(() => methods.Process("ab"), Throws.TypeOf<ArgumentException>());
        }
        [Test, Category("regression")]
        public void process_WhenGivenCorrectValue_DoesNotThrow()
        {
            Assert.Ignore("this is not part of the exercises");
            Assert.DoesNotThrow(() => methods.Process("Luhlelo"));
        }
        //Retry and Exceptions
        private static int _attempt = 0;
        [Test]
        [Retry(3)]
        [Category("smoke")]
        public void RetryExampleTest()
        {
            //Assert.Ignore("This is not part of testing");
            try
            {
                //Thsi test 2 fails on the first 2 attempt. and it passes on third attempt
                _attempt++;
                TestContext.Out.WriteLine($"Attemp {_attempt}");
                test.Info($"Attemp {_attempt}");
                if (_attempt < 3)
                {
                    Assert.Throws<ArgumentException>(() => new Exception("Temporary failure"));
                }
            }
            catch (Exception ex) { 
                test.Fail(ex);
                test.Fail(ex.Message);
                throw;
            }
            
        }

        [Test, Category("smoke")]
        [Retry(3)]
        public void RetryExampleTest2()
        {
            try
            {
                TestContext.Out.WriteLine($"Attempts: {TestContext.CurrentContext.CurrentRepeatCount + 1}");
                test.Info($"Attempts: {TestContext.CurrentContext.CurrentRepeatCount + 1}");
                Assert.Fail("For retry demo");
            }
            catch (Exception ex) {
                test.Fail($"Exception Type: {ex.GetType().Name}");
                test.Fail($"Message :{ex.Message}");
                test.Fail(ex.StackTrace);
                throw;
            }
            
        }



        //[TearDown]
        //public void FinaltestStatus()
        //{
        //    var results = TestContext.CurrentContext.Result;

        //    switch (results.Outcome.Status)
        //    {
        //        case TestStatus.Passed:
        //            break;
        //        case TestStatus.Failed:
        //            test.Fail(results.Message);
        //            test.Fail(results.StackTrace);
        //            break;
        //        case TestStatus.Skipped:
        //            test.Skip(results.Message);
        //            break;
        //        case TestStatus.Warning:
        //            test.Warning(results.Message);
        //            break;

        //    }

        //}
        [Test]
        public void FailingTest()
        {
            try
            {
                //test = extent.CreateTest("Failing test");
                Assert.Fail("Intentional failure");
            }
            catch (Exception ex) { 
                test.Fail(ex.ToString());
                test.Fail(ex.Message);
                test.Fail(ex.GetType().Name);
            }
            
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
        }
    }
}
