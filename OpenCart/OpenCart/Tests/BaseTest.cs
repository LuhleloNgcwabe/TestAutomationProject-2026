using OpenCart.Utility.Browser;
using OpenQA.Selenium;
using System.Security.Cryptography;
using Serilog;
using OpenCart.Utility;

namespace OpenCart.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void init()
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.Console()
             .WriteTo.File("../../../logs/myapp.txt", rollingInterval: RollingInterval.Day)
             .CreateLogger();

            LogStep.Info("Start logging for opencart Testing");
        }


        [SetUp]
        public void setUp()
        {
            LogStep.Info("Starting test: {TestName}", TestContext.CurrentContext.Test.Name);
            WebDriverFactory driverFactory = new WebDriverFactory();
            LogStep.Info("Start {browser} ", driverFactory.GetBrowserName());
            driver = driverFactory.getDriver();  
            driver.Url = "http://localhost/opencart/upload/";
        }

        public string RandomSting(int length)
        {
            char[] LowerCaseCharacter = RandomNumberGenerator.GetBytes(16).Select(b => (char)('a' + (b % 26))).ToArray();
            string random = new string(LowerCaseCharacter);
            return random.Substring(0, length);
        }

        [TearDown]
        public void tearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            testStatus(status);

            driver.Quit();
            driver.Dispose();
          
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            Log.CloseAndFlush();
        }

        public void testStatus(NUnit.Framework.Interfaces.TestStatus status)
        {
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                Log.Error("Test FAILED: {TestName}", TestContext.CurrentContext.Test.Name);
                TakeScreenshot();
            }
            else
            {
                Log.Information("Test PASSED: {TestName}", TestContext.CurrentContext.Test.Name);
                //TakeScreenshot();
            }
        }

        private void TakeScreenshot()
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string fileName = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                Directory.CreateDirectory("../../../Screenshot/");
                screenshot.SaveAsFile("../../../Screenshot/"+fileName);
                Log.Information("Screenshot saved: {File}", fileName);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to capture screenshot");
            }
        }

    }
}
