using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace NunitPlusExtentReportPractise
{
    [TestFixture]
    public class BasicMathTests
    {
        public ExtentReports extent;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var reporter = new ExtentSparkReporter("ExtentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            TestContext.Out.WriteLine("Initialize extent report");
        }


        [SetUp]
        public void Setup()
        {
            //Arrange
            TestContext.Out.WriteLine("Test set up is finished");
        }

        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 5)]
        [TestCase(3, 5, 8)]
        public void Add_TwoNumbers_ReturnsCorrectSum(int a, int b, int answer)
        {
            extent.CreateTest(TestContext.CurrentContext.Test.Name);
            // Act
            TestContext.Out.WriteLine($"Add {a} and {b}");
            int result = a + b;
            // Assert
            TestContext.Out.WriteLine($"Verify that answer is {answer}!!");
            Assert.That(answer, Is.EqualTo(result));
        }
        
        [TearDown]
        public void TearDown()
        {
            TestContext.Out.WriteLine("Test is completed");
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            TestContext.Out.WriteLine("Save all recorded data to a report file");
            extent.Flush();
        }
    }
}
