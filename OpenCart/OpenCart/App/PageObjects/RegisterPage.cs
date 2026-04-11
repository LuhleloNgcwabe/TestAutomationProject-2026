using OpenCart.Utility.Browser;
using OpenQA.Selenium;


namespace OpenCart.App.PageObjects
{
    public class RegisterPage
    {
        #region InputFields
        //public IWebDriver driver;
        public By txt_Firstname = By.XPath("//*[contains(@name,'firstname')]");
        public By txt_LastName = By.XPath("//*[contains(@name,'lastname')]");
        public By txt_Email = By.XPath("//*[contains(@name,'email')]");
        public By txt_Password = By.XPath("//*[@id='input-password']");
        public By ckb_AgreeTermsAndCond = By.XPath("//input[@name='agree']");
        public By btn_Continue = By.XPath("//*[@class='btn btn-primary']");
        public By msgConfirmation = By.XPath("//h1[contains(text(),'Your Account Has Been Created!')]");                                                                                           
        public By password_vallidationLabel = By.XPath("//*[contains(@class,'invalid-feedback d-block')]");


        public void EnterFirstName(IWebDriver driver, string name)
        {
            DriverHelper.SetElementText(driver, txt_Firstname, name);
        }

        public void EnterLastName(IWebDriver driver, string surname)
        {
            DriverHelper.SetElementText(driver, txt_LastName, surname);
        }

        public void EnterEmail(IWebDriver driver, string email)
        {
            DriverHelper.SetElementText(driver, txt_Email, email);
        }

        public void EnterPassword(IWebDriver driver, string password)
        {
            DriverHelper.SetElementText(driver, txt_Password, password);
        }

        public void AcceptTerms(IWebDriver driver)
        {
            DriverHelper.ClickElement(driver, ckb_AgreeTermsAndCond);
        }

        public void ClickContinue(IWebDriver driver)
        {
            DriverHelper.ClickElement(driver, btn_Continue);
        }

        public string GetConfirmationMessage(IWebDriver driver)
        {
            return DriverHelper.GetElement(driver, msgConfirmation).Text;
        }
        #endregion

        #region VallidationField
        //validation labels
        public By vl_Firstname = By.Id("error-firstname");
        public By vl_LastName = By.Id("error-lastname");
        public By vl_Email = By.Id("error-email");
        public By vl_Password = By.Id("error-password");
        public By vl_PrivacyPolicy = By.ClassName("alert-dismissible"); // Warning: You must agree to the Privacy Policy! 
        public string GetFirstNameValidationmessage(IWebDriver driver)
        {
            return DriverHelper.GetElement(driver, vl_Firstname).Text.Trim();
        }

        public string GetLastNameValidationmessage(IWebDriver driver)
        {
            return DriverHelper.GetElement(driver, vl_LastName).Text.Trim();
        }

        public string GetEmailValidationmessage(IWebDriver driver)
        {
            return DriverHelper.GetElement(driver, vl_Email).Text.Trim();
        }

        public string GetPasswordValidationMessage(IWebDriver driver)
        {
            return DriverHelper.GetElement(driver, vl_Password).Text.Trim();
        }

        public string GetPrivacyPolicyValidationMessage(IWebDriver driver)
        {
            return DriverHelper.GetElement(driver, vl_PrivacyPolicy).Text.Trim();
        }
        #endregion
    }
}
