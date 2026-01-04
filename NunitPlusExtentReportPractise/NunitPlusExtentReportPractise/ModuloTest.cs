using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NunitPlusExtentReportPractise.ChallengeExercise;
namespace NunitPlusExtentReportPractise
{
    [TestFixture]
    public class ModuloTest
    {
        ExtentReports extent = new();
        ExtentTest test;
        [OneTimeSetUp]
        public void initReport()
        {
            var reporter = new ExtentSparkReporter("ExtentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(reporter);
            TestContext.Out.WriteLine("Initialize extent report");
        }
        [SetUp]
        public void setUp() {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Info($"Start remainder tests");
            test.AssignAuthor("Luhlelo ngcwabe");
        }

        [TestCase(4, 2, ExpectedResult =0),Category("smoke")]
        [TestCase(8, 3, ExpectedResult = 2)]
        [TestCase(4, 3, ExpectedResult = 1)]
        [TestCase(20, 7, ExpectedResult = 6)]
        [TestCase(45, 4, ExpectedResult = 2)]
        [TestCase(36, 5, ExpectedResult = 1)]
        [TestCase(19, 10, ExpectedResult = 2)]
        [TestCase(56, 30, ExpectedResult = 26)]
        [TestCase(14, 2, ExpectedResult = 0)]
        [TestCase(23, 3, ExpectedResult = 2)]
        public int getRemainder_returnCorrectout(int a,int b)
        {
            TestContext.Out.WriteLine($"Start remainder tests");
            test.Info($"Find reminder of {a} modulo {b}");
            //test.AssignAuthor("Luhlelo Ngcwabe");
            return Modulo.getRemainder(a,b);
        }

        [TearDown]
        public void AfterEachTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            string message = TestContext.CurrentContext.Result.Message;
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail(message);
            }
            TestContext.Out.WriteLine("Test is completed");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    }
}
