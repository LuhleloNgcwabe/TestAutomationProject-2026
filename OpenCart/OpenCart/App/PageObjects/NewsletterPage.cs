using System;
using System.Collections.Generic;
using System.Text;
using OpenCart.Utility.Browser;
using OpenQA.Selenium;
namespace OpenCart.App.PageObjects
{
    internal class NewsletterPage:BasePage
    {
        public By chb_Subscribe = By.CssSelector("#input-newsletter");

        public void ClickSubscribeToNewsletter(IWebDriver driver)
        {
            DriverHelper.ClickElement(driver, chb_Subscribe);
        }
    }
}
