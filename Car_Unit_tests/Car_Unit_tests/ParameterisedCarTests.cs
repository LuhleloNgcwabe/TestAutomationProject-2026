using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using Car_Unit_tests.Model;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Car_Unit_tests
{
    [TestFixture, Category("Regression"),Author("Zane")]
    public class ParameterisedCarTests
    {
        ExtentReports extent;
        ExtentTest test;
        [OneTimeSetUp]
        public void Init_ExtentReport()
        {
            extent = new ExtentReports();
            var spark = new ExtentSparkReporter("ExtentReport_ParameterisedTests.html");
            extent.AttachReporter(spark);


            //Assigned device for all these tests
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Username", Environment.UserName);
            extent.AddSystemInfo("Process Count", Environment.ProcessorCount.ToString());
            TestContext.Progress.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            TestContext.Progress.WriteLine(Environment.CurrentDirectory);
        }

        [SetUp]
        public void SetUp()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Info("Set up: Start parameterised tesst");
            TestContext.Out.WriteLine("Set up: Start parameterised tesst");
            test.Info("Get information about test scripter");

            var authors = TestContext.CurrentContext.Test.Properties["Author"];
            foreach(var author in authors)
            {
                test.AssignAuthor(author.ToString());
            }

            var categoryList = TestContext.CurrentContext.Test.Properties["Category"];
            foreach (var category in categoryList) 
            { 
                test.AssignCategory(category.ToString());
            }

            test.AssignDevice(Environment.MachineName);
        }

        [Test,Author("Zoe")]
        public void AuthorIsAssignedForNormalTests()
        {
            //TestContext.Out.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            //TestContext.Out.WriteLine(Environment.CurrentDirectory);
            test.Skip("TO de deleted");
            Assert.Pass("A test that always pass");
        }
        
        [TestCase("VW", "T-cross", 2019, Author ="Luhlelo", Category ="smoke",Description ="My portential purchase")]
        [TestCase("Toyata", "Yaris", 2016, Author = "Luhlelo", Category = "smoke")]
        [TestCase("Handai", "alentra", 2013, Author = "Luhlelo", Category = "smoke")]
        
        public void Verify_Car_Details(string make, string model, int year)
        {
            //Arrange and Action
            TestContext.Out.WriteLine("Arrange and Action");
            Car Car = new Car(make, model, year);

            TestContext.Out.WriteLine("Assert: Verify car details");
            Assert.That(Car, Is.Not.Null);
            Assert.That(Car.Model, Is.EqualTo(model));
            Assert.That(Car.Make, Is.EqualTo(make));
            Assert.That(Car.Year, Is.EqualTo(year));
        }

        [TestCase(5, ExpectedResult = 5, Author="Zoe", Category ="system test")]
        [TestCase(50, ExpectedResult = 50, Author = "Zoe", Category = "system test")]
        [TestCase(45, ExpectedResult = 45, Author = "Zoe", Category = "system test")]
        public double Verify_Accelaration(double increment)
        {
            TestContext.Out.WriteLine("Arrange:Create Car object");
            Car Car = new Car("Handai", "Elentra", 2012);
            TestContext.Out.WriteLine("Action: Accelarate car");
            Car.Accelerate(increment);
            TestContext.Out.WriteLine("Assert: Verify accelaration speed");
            //Assert.That(Car.GetSpeed(),Is.EqualTo(increment),$"Speed must be equal to {increment}");
            return Car.GetSpeed();

        }

        [TestCase(70, 60, ExpectedResult = 10,Author ="Sese",Category ="sanity")]
        [TestCase(120, 60, ExpectedResult = 69, Author = "Sese", Category = "sanity,system test")]
        [TestCase(89, 90, ExpectedResult = 0, Author = "Sese", Category = "sanity")]
        public double verify_braking_reduce_speed(double currentSpeed, double decrement) {

            TestContext.Out.WriteLine("Arrang: Create a new car and put it in motion");
            Car Car = new Car("Toyota", "yaris", 2015);

            TestContext.Out.WriteLine("Action: make the car move and apply brake");
            Car.Accelerate(currentSpeed);
            Car.Brake(decrement);

            TestContext.Out.WriteLine("Assert: compare results");
            return Car.GetSpeed();
        }

        [Test, Description("sets the description property of the test.")]
        public void Verify_ToggleEngine_FromOffState()
        {
            //Arrange
            TestContext.Out.WriteLine("Arrange: Create a car with engine turned off");
            Car Car = new Car("Toyota", "Etios", 2015);
            TestContext.Out.WriteLine("Action: Turn the engine on");
            Car.ToggleEngine();
            TestContext.Out.WriteLine("Asset: Engine is turn on");
            Assert.That(Car.IsEngineRunning, Is.True, "Engine must be running, this must be true");
        }

        [Test]
        public void Verify_ToggleEngine_FromOnState()
        {
            //Arrange
            TestContext.Out.WriteLine("Arrange: Create a car with engine turned on");
            Car Car = new Car("Toyota", "Etios", 2015);
            Car.ToggleEngine();
            TestContext.Out.WriteLine("Action: Turn the engine off");
            Car.ToggleEngine();
            TestContext.Out.WriteLine("Asset: Engine is turn on");
            Assert.That(Car.IsEngineRunning, Is.False, "Engine must be off, this must be false");
        }

        [TestCase(50, 7)]
        [TestCase(40, 7)]
        [TestCase(20, 80)]
        public void GetSpeed_returns_CorrectSpeed(double initialSpeed, double acceleration)
        {
            double totSpeed = initialSpeed + acceleration;
            TestContext.Out.WriteLine($"Arrange: Create a car with initial speed of {initialSpeed}");
            Car Car = new Car("Toyata", "Etios", 2017);
            Car.Speed = initialSpeed;
            TestContext.Out.WriteLine("Action: apply Acceleration");
            Car.Accelerate(acceleration);
            TestContext.Out.WriteLine("Assert: Compare results");
            Assert.That(Car.GetSpeed(), Is.EqualTo(totSpeed));
        }

        [Test, Category("smoke")]
        public void TestDoesnthaveEnoughConditionToPass()
        {
            test.Info("Test needs to be updated");
            test.Log(Status.Info, "Test rarely pass, we are currently no sure of the cuase");
            Assert.Inconclusive("Test is inconclusive due to pending implementation.");
        }
        [TearDown]
        public void tearDown()
        {
            test.Info("Record test status");
            var result = TestContext.CurrentContext.Result;
            //var Message =TestContext.CurrentContext.Result.Message;

            switch (result.Outcome.Status)
            {
                case TestStatus.Passed:
                    break;

                case TestStatus.Failed:
                    test.Fail(result.Message);
                    test.Fail(result.StackTrace);
                    break;

                case TestStatus.Inconclusive:
                    test.Info("inconclusive results");
                    test.Warning("Warning");
                    break;

                case TestStatus.Skipped:
                    test.Skip(result.Message);
                    break;
                 case TestStatus.Warning:
                    test.Warning("Warning");
                        break;
            }
        }

        [Test]
        public void ExceptionTest()
        {
            Assert.Throws<NotImplementedException>(() => ThrowNotImplementedException(true), "Expecting NotImplementedException");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
            TestContext.Out.WriteLine("Runs after all test are complete");
        }

        public void ThrowNotImplementedException(bool isTrue)
        {
            if(isTrue)
            throw new NotImplementedException();
        }

        //private st loopThroughListt<T>(T list)
        //{
        //    foreach (var item in list) { 

        //    }
        //}

    }
}
