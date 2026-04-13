using OpenCart.Utility.Browser;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCart.App.PageObjects
{
    public class AccountPage :BasePage
    {
        public By lnk_newsletter = By.PartialLinkText("unsubscribe to newsletter");

        public void ClickNewsletterLink(IWebDriver driver)
        {
            DriverHelper.ClickElement(driver, lnk_newsletter);
        }
    }
}
