using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Car_Unit_tests.Model;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework.Interfaces;


namespace Car_Unit_tests
{
    [TestFixture]
    public class CarTest_withExtentReport
    {
        ExtentReports extent = new();
        ExtentTest test;
        Car ToyataSUV;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Action to run before all test are executed
            //test.Info("One time set");
            extent = new ExtentReports();
            var spark = new ExtentSparkReporter("spark.html");
            extent.AttachReporter(spark);


            //Add system information
            string machineName = Environment.MachineName;
            string osVersion = Environment.OSVersion.ToString();
            string userName = Environment.UserName;
            string processorCount = Environment.ProcessorCount.ToString();


            extent.AddSystemInfo("Machine", machineName);
            extent.AddSystemInfo("OS", osVersion);
            extent.AddSystemInfo("Username", userName);
            extent.AddSystemInfo("Process Count", processorCount);
        }

        [SetUp]

        public void Setup()
        {
            ToyataSUV = new Car("Toyota","SUV",2016);
            test.Info("Set up");
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            var categories = TestContext.CurrentContext.Test.Properties["Category"];
            foreach (var category in categories) {
                test.AssignCategory(category.ToString());
            }

            var Authors = TestContext.CurrentContext.Test.Properties["Author"];

            foreach (var author in Authors)
            {
                test.AssignAuthor(author.ToString());
            }
            test.AssignDevice(Environment.MachineName);
            
        }
        [Test,Author("Zoe")]
        [Category("smoke")]
        public void Verify_Car_details()
        {
            test.Info("Get car details");
            Assert.That(ToyataSUV.GetCarInfo(), Does.EndWith("SUV").IgnoreCase);
        }
        
        [Test,Author("Luhlelo")]
        public void New_Toyota_SUV_Is_Created()
        {
            //Car ToyataSUV = new Car("Toyota", "SUV", 2016);
            Assert.That(ToyataSUV.Make, Is.EqualTo("Toyota"), "Make must be Toyota"); 
            Assert.That(ToyataSUV.Model, Is.EqualTo("SUV"), "Model must be SUV");
            Assert.That(ToyataSUV.Year, Is.EqualTo(2016), "year of a car must be 2016");
            test.Info(TestContext.CurrentContext.Test.FullName + ": Is complete");
        }

        [Test,Author("Zoe")]
        [Category("smoke")]
        public void StartEngine_switches_Engine_On()
        {
            ToyataSUV.StartEngine();
            Assert.That(ToyataSUV.IsEngineRunning, Is.True);
            test.Info(TestContext.CurrentContext.Test.FullName + ": Is complete");
        }

        [Test,Author("Zoe")]
        [Category("smoke")]
        public void StopEngine_switches_Engine_off()
        {
            ToyataSUV.StopEngine();
            Assert.That(ToyataSUV.IsEngineRunning, Is.False);
            test.Info(TestContext.CurrentContext.Test.FullName + ": Is complete");
        }

        [Test, Author("Luhlelo")]

        public void Car_accelerate_By_5_Speed_increase_By_5()
        {
            ToyataSUV.Accelerate(5);
            Assert.That(ToyataSUV.GetSpeed(), Is.EqualTo(5), "Speed must increase by 5");
        }
        [Test,Order(0),Author("Luhlelo")]
        public void Brake_ReduceSpeedBy30_speedDropsBy30()
        {
            test.Info("Start engine and increase speed by 40");
            ToyataSUV.StartEngine();
            ToyataSUV.Accelerate(40);
            test.Info("Applied action, Reduce Speed");
            ToyataSUV.Brake(30);
            test.Info($"Assert action, compare expected speed to actual speed.");
            Assert.That(ToyataSUV.Speed, Is.EqualTo(10));
        }

        [Test, Author("Zoe")]
        [Category("smoke")]
        public void Speed_OfaNotMovingCar_is_Zero() 
        {
            test.Info("Assert: Speed of not a moving car is zero");
            Assert.That(ToyataSUV.GetSpeed(), Is.EqualTo(0));
        }

        [Test, Author("Zoe")]
        [Category("smoke")]
        public void CarHonk_makes_BeepBeepSound()
        {
            //Arrange
            test.Info("Prepare a car for a hooty sound");
            ToyataSUV.StartEngine();
            test.Info("make a car hooty");
            Assert.That(ToyataSUV.Honk(),Does.Contain("Beep").IgnoreCase);
            
        }
        [Test, Author("Luhlelo")]
        public void failingTests()
        {
            Assert.Ignore("This is for test purposes");
            Assert.Fail("Intentional failing the test");
        }

        [Test, Author("Zoe")]
        [Category("smoke")]
        public void NumberTest_ThreeIslessThan10_True()
        {
            int num = 3;
            if (num < 10)
            {
                throw new Exception("Number is less than 10");
            }
            Assert.That(num, Is.EqualTo(10));
        }

        [TearDown]
        public void TearDown()
        {
            // Get the test status
            var results = TestContext.CurrentContext.Result;
            string message = TestContext.CurrentContext.Result.Message;
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
            test.Info("Tear down");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
            // no action at moment
            //Action to do after all test are executed
        }

    }
}
