using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Text;

namespace NunitPlusExtentReportPractise
{
    public class BaseTest
    {
        public ExtentReports extent = new();
        public ExtentTest test;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var reporter = new ExtentSparkReporter("ExtentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            TestContext.Out.WriteLine("Initialize extent report");
        }
    }
}
