using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccountService.Utility
{
    
    public class ExtentManager
    {
        public static ExtentReports extent;
        public static ThreadLocal<ExtentTest> test = new();

        public static void Init()
        {
            var reporter = new ExtentSparkReporter("ExtentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            
        }

        public void MapCategory()
        {
            var categories = TestContext.CurrentContext.Test.Properties["Category"];
            foreach (var category in categories)
            {
                test.Value?.AssignCategory(category.ToString());
            }
        }

        public void TestStatus()
        {
            var result = TestContext.CurrentContext.Result;

            if (result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                test.Value?.Fail(result.Message);
            else
            if (result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Skipped)
                test.Value?.Skip(result.Message ?? "Test skipped");

            else
                test.Value?.Pass("Test Passed");
        }

        public static void Flush()
        {
            extent.Flush();
        }

    }
}
