using OpenCart.Utility.Browser;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCart.App.PageObjects
{
    public class BasePage
    {
        public By btn_Continue = By.XPath("//*[@class='btn btn-primary']");
        public By lst_BreadCrumbLabel = By.CssSelector(".breadcrumb-item:last-child");

        public void ClickContinue(IWebDriver driver)
        {
            DriverHelper.ClickElement(driver, btn_Continue);
        }

        public string  GetBreadCrumb(IWebDriver driver) { 
            return DriverHelper.GetElement(driver,lst_BreadCrumbLabel).Text.Trim();
        }
    }
}
