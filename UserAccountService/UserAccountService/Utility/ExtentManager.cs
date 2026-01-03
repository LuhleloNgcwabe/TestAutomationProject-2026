using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
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

        public static void Flush()
        {
            extent.Flush();
        }

    }
}
