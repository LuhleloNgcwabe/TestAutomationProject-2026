using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace NunitPlusExtentReportPractise
{
    [TestFixture]
    public class BasicMathTests
    {
        public ExtentReports extent;
        public ExtentTest test;
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
        
        [Test]
        public void Addition_WithLogging()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Info("Starting addition test");
            int result = 3+2;
            Assert.That(5, Is.EqualTo(result));
            test.Pass("Addition vallidated Successfully");
        }

        [TestCase(1,5, ExpectedResult =5)]
        [TestCase(6, 5, ExpectedResult = 30)]
        [TestCase(12, 5, ExpectedResult = 60)]
        public int multiplication_WithLogging(int a, int b)
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Info("Starting multiplication test");
            int product = a * b;
            test.Info($"Multiply {a} by {b} and Expected answer is {product}");
            test.Pass("Multiplication vallidated Successfully");
            return product;
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
