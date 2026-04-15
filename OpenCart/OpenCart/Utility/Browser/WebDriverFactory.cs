using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCart.Utility.Browser
{
    public class WebDriverFactory
    {
        IWebDriver driver;
        //public WebDriverFactory() 
        //{
        //    driver = new ChromeDriver();
        //}

        public WebDriverFactory()
        {
            driver = new RemoteWebDriver(
            new Uri("http://localhost:4444"),
            new ChromeOptions());
        }

        public IWebDriver getDriver()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            return driver;
        }

        public string GetBrowserName()
        {
            var capabilities = ((IHasCapabilities)driver).Capabilities;
            return capabilities.GetCapability("browserName").ToString();
        }
    }
}
