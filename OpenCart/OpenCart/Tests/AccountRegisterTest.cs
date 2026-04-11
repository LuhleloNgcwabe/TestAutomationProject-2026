using OpenCart.App.PageObjects;
using OpenCart.Utility;
using OpenCart.Utility.Browser;

namespace OpenCart.Tests
{
    [TestFixture]
    public class AccountRegisterTest:BaseTest
    {
        [Test]
        public void Test_Account_Registration()
        {
            string postfix = RandomSting(3);
            LogStep.Debug("Generated postfix: {postfix}", postfix);
            string name = "Vu" + postfix;
            string surname = "Fi" + postfix;
            string email = $"Vu{postfix}@gmail.com";
            string password = "1234567";

            LogStep.Debug("Test data -> Name: {name}, Surname: {surname}, Email: {email}",name, surname, email);
            LogStep.Info("Starting account registration");
            HomePage homePage = new HomePage();
            RegisterPage accountRegistration = new RegisterPage();

            
            LogStep.Info("Navigate to registration page");
            homePage.ClickMyAccount(driver);
            homePage.ClickRegister(driver);

           
            LogStep.Info("Fill registration form for {email}", email);
            accountRegistration.EnterFirstName(driver, name);
            accountRegistration.EnterLastName(driver, surname);
            accountRegistration.EnterEmail(driver, email);
            accountRegistration.EnterPassword(driver, password);

            LogStep.Info("Accept terms");
            accountRegistration.AcceptTerms(driver);

            LogStep.Info("Submit form");
            accountRegistration.ClickContinue(driver);
            
            string msg = accountRegistration.GetConfirmationMessage(driver);
            LogStep.Debug("Confrimation message: {msg}",msg);
            Assert.That(msg, Does.Contain("Your Account Has Been Created!").IgnoreCase, "Must contain Your Account Has Been Created!");

        }

        [Test]
        public void Verify_SubmittingAformWithOutFillingTheFields()
        { 
            //Arrange
            HomePage homePage = new HomePage();
            RegisterPage accountRegistration = new RegisterPage();
            string fname_valText = "First Name must be between 1 and 32 characters!";
            string lname_valText = "Last Name must be between 1 and 32 characters!";
            string email_valText = "E-Mail Address does not appear to be valid!";
            string password_valText = "Password must be between 6 and 40 characters!";

            //action Assert
            LogStep.Info("Navigate to registration page");
            homePage.ClickMyAccount(driver);
            homePage.ClickRegister(driver);
            LogStep.Info("Submit form");

            accountRegistration.ClickContinue(driver);
            
            //Assert
            DriverHelper.WaitForElementToBeVisible(driver, accountRegistration.vl_Firstname,  TimeSpan.FromSeconds(30));
            Assert.Multiple(() =>
            {
                Assert.That(accountRegistration.GetFirstNameValidationmessage(driver), Is.EqualTo(fname_valText));
                Assert.That(accountRegistration.GetLastNameValidationmessage(driver), Is.EqualTo(lname_valText));
                Assert.That(accountRegistration.GetEmailValidationmessage(driver), Is.EqualTo(email_valText));
                Assert.That(accountRegistration.GetPasswordValidationMessage(driver), Is.EqualTo(password_valText));

            });
        }
    }
}
