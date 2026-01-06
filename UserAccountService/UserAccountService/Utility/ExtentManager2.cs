using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace UserAccountService
{
    
    public class ExtentManager2
    {
        public ExtentReports extent = new();
        public ExtentTest test ;

        public void InitReport()
        {
            extent = new ExtentReports();
            var reporter = new ExtentSparkReporter("ExtentReport.html");
            extent.AttachReporter(reporter);
        }

        public void AssignTestStatus()
        {
            var result = TestContext.CurrentContext.Result;
            if (result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Skipped)
            {
                test.Skip(result.Message);
            }
            else if(result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail(result.Message);
            }
            else if(result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Warning)
            {
                test.Warning(result.Message);
            }
            else if(result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass(result.Message);
            }
        }

        public void Flush()
        {
            test.Info("Save test execution deatails");
            extent.Flush();
        }
    }
}
