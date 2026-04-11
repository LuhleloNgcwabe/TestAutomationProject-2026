using OpenCart.Utility.Browser;
using OpenQA.Selenium;

namespace OpenCart.App.PageObjects
{
    public class HomePage
    {
        public By lbl_MyAccount = By.XPath("//*[contains(text(),'My Account')]");
        public By lnk_register = By.XPath("//a[contains(text(),'Register')]");
        public By lnk_Login = By.XPath("//a[contains(text(),'Login')]");
        public void ClickRegister(IWebDriver driver)
        {
            DriverHelper.ClickElement(driver, lnk_register);
        }

        public void ClickLogin(IWebDriver driver) {
            DriverHelper.ClickElement(driver, lnk_Login);
        }

        public void ClickMyAccount(IWebDriver driver) { 
            DriverHelper.ClickElement(driver,lbl_MyAccount);
        }
    }
}
