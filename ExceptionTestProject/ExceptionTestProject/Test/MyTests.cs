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
            reporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
            reporter.Config.DocumentTitle = "My Report";
            extent.AttachReporter(reporter);
            

        }


        [SetUp]
        public void initFunctionClass()
        {
            methods = new FunctionClass();
            string testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
            //Get categories
            var categories = TestContext.CurrentContext.Test.Properties["Category"];
            foreach (var category in categories)
            {
                test.AssignCategory(category.ToString());
            }

            //Get Authors
            var authors = TestContext.CurrentContext.Test.Properties["Author"];
            foreach (var author in authors)
            {
                test.AssignAuthor(author.ToString());
            }
            //Assign device
            test.AssignDevice("Levono Host12");
        }

        [Test, Category("regression"),Order(0),Author("Lufefe")]
        public void DoWork_WhenInvalid_ThrowsInvalidOperationException()
        {
             Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
        }

        [Test, Category("regression"), Order(0),Author("Lufefe")]
        public void DoWork_throwException_WithCorrectMessage()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
            Assert.That(ex.Message, Is.EqualTo("Invalid operation. The method call is invalid for the objects current state."));
        }

        [Test, Category("regression"), Order(0), Author("Lufefe")]
        public void DoWork_WhenValid_DoesNotThrow()
        {
            Assert.Ignore("Method is not yet implemented.");
            Assert.DoesNotThrow(() => methods.DoWork(true));
        }

        [Test, Category("regression"), Order(0), Author("Lufefe")]
        public void process_WhenNull_throwsArgumentNullException()
        {
            //Assert.Throws<ArgumentNullException>(() => methods.Process(null));
            Assert.That(() => methods.Process(null), Throws.TypeOf<ArgumentNullException>());
        }
        [Test, Category("regression"), Order(0), Author("Lufefe")]
        public void process_WhenTooShort_ThrowsArgumentException()
        {
            //Assert.Throws<ArgumentException>(() => methods.Process("ab"));
            Assert.That(() => methods.Process("ab"), Throws.TypeOf<ArgumentException>());
        }
        [Test, Category("regression"), Order(0), Author("Lufefe")]
        public void process_WhenGivenCorrectValue_DoesNotThrow()
        {
            Assert.Ignore("this is not part of the exercises");
            Assert.DoesNotThrow(() => methods.Process("Luhlelo"));
        }
        //Retry and Exceptions
        private static int _attempt = 0;
        [Test]
        [Retry(3)]
        [Category("smoke"),Author("Lina")]
        public void RetryExampleTest()
        {
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
                test.Fail(ex).AddScreenCaptureFromPath("screenshot.png");
                throw;
            }
            
        }

        [Test, Category("smoke"),Author("Lina"),Author("Luhlelo")]
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
                test.Fail(ex).AddScreenCaptureFromPath("screenshot.png");
                test.Fail($"Exception Type: {ex.GetType().Name}");
                throw;
            }
            
        }

        [Test,Category("Sanity"),Author("Luhlelo")]
        public void PlayAroundWithTheTool()
        {
            //var node = test.CreateNode("Test steps:");
            //node.Pass("This test passed");

        }
        [TestCase(""),]
        [TestCase("as")]
        public void InvalidInputs_throwException(string input)
        {
            Assert.Throws<ArgumentException>(() => methods.Process(input));
        }

        [TearDown]
        public void FinaltestStatus()
        {
            var results = TestContext.CurrentContext.Result;
            //Record Retries used
            int retries = TestContext.CurrentContext.CurrentRepeatCount;
            test.Info($"Retries used: {retries}");

            switch (results.Outcome.Status)
            {
                case TestStatus.Passed:
                    break;
                case TestStatus.Failed:
                    test.Fail(results.Message);
                    test.Fail(results.StackTrace);
                    break;
                case TestStatus.Skipped:
                    test.Skip(results.Message);
                    break;
                case TestStatus.Warning:
                    test.Warning(results.Message);
                    break;
            }

        }
     
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
        }
    }
}
