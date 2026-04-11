using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace OpenCart.Utility.Browser
{
    public static class DriverHelper
    {
        public static void ClickElement(IWebDriver driver, By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement element= wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
            element.Click();
        }

        public static void SetElementText(IWebDriver driver, By locator, string text)
        {
            IWebElement element = GetElement(driver, locator);
            ((IJavaScriptExecutor)driver)
               .ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
            element.Clear();
            element.SendKeys(text);
        }


        public static IWebElement GetElement(IWebDriver driver, By locator)
        {
            IWebElement element = WaitForElementToBeVisible(driver,locator,TimeSpan.FromSeconds(30));
            return element;
        }

        public static IWebElement WaitForElementToBeVisible(IWebDriver driver, By by, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

    }
}
