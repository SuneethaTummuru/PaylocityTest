using OpenQA.Selenium;
using UI.AutomationChallange.Configuration;
using UI.AutomationChallange.Tests;

namespace UI.AutomationChallange.PageObjects
{
    public partial class LoginPage
    {

        public IWebDriver Driver => BaseTest.Driver;
        ConfigSettings configSettings = new ConfigSettings();

        public void WriteExceptionInfo(Exception ex)
        {
            WriteTimeStampedLine($"Current Url: {BaseTest.Driver.Url} ");

            WriteTimeStampedLine($"ex Source: {ex.Source} \n" +
                                 //$"Base ex: {ex.GetBaseException()} \n" +
                                 $"ex Message: {ex.Message} \n" +
                                 $"Inner ex Message: {ex.InnerException?.Message} \n" +
                                 $"Stack: {ex.StackTrace}");
        }

        public void WriteTimeStampedLine(string message)
        {
            Console.WriteLine($"{GetTimeStamp()}" +
                              $"{message} \n");
        }
        public string GetTimeStamp()
        {
            return DateTime.Now.ToString(//"yyyy-MM-dd " +
                "HH_mm_ss_fff ");
        }

        public void VerifyPageLoad()
        {

            new BaseWaits().WaitForElementIsVisible(LoginButtonBy);
            new BaseWaits().WaitForElementIsVisible(BrandingLabel);
        }

        public void LoginToBenefitsDashboard()
        {
            configSettings.GetconfigValues();
            var usernameTextField = new BaseWaits().WaitForElementIsVisible(UserNameTextFieldBy);
            var passwordTextField = new BaseWaits().WaitForElementIsVisible(PasswordTextFieldBy);
            usernameTextField.SendKeys(configSettings.Username);
            passwordTextField.SendKeys(configSettings.Password);
            new BaseWaits().WaitForElementIsVisible(LoginButtonBy).Click();
        }

    }
    public partial class LoginPage
    {
        public By LoginButtonBy = By.ClassName("btn-primary");
        public By BrandingLabel = By.LinkText("Paylocity Benefits Dashboard");

        public By UserNameTextFieldBy = By.Id("Username");
        public By PasswordTextFieldBy = By.Id("Password");

    }
}
