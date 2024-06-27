using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using UI.AutomationChallange.Configuration;
using UI.AutomationChallange.Tests;

namespace UI.AutomationChallange.PageObjects
{
    public class BaseWaits : LoginPage
    {

        public new By AcceptAllButtonBy = By.Id("cookiescript_accept");
        ConfigSettings _settings = new ConfigSettings();
        LoginPage basePage = new LoginPage();


#pragma warning disable CS8604 // Possible null reference argument.
        public WebDriverWait Wait => new(BaseTest.Driver, TimeSpan.FromSeconds(double.Parse(_settings.ImplicitWaitInMillSeconds)));
#pragma warning restore CS8604 // Possible null reference argument.

        public IWebElement WaitForElementIsVisible(By locateBy)
        {
            _settings.GetconfigValues();
            double ss = double.Parse(_settings.ImplicitWaitInMillSeconds);
            try
            {
                return Wait.Until(ExpectedConditions.ElementIsVisible(locateBy));
            }
            catch (Exception ex)
            {
                basePage.WriteExceptionInfo(ex);
                throw;
            }
        }

        public IWebElement WaitForElementClickable(By locateBy)
        {
            _settings.GetconfigValues();
            double ss = double.Parse(_settings.ImplicitWaitInMillSeconds);
            try
            {
                return Wait.Until(ExpectedConditions.ElementToBeClickable(locateBy));
            }
            catch (Exception ex)
            {
                basePage.WriteExceptionInfo(ex);
                throw;
            }
        }


        public IWebDriver WaitForFrameToBeAvailableAndSwitchToIt(By locateBy)
        {
            try
            {
                return Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locateBy));
            }
            catch (Exception ex)
            {
                basePage.WriteExceptionInfo(ex);
                throw;
            }
        }

    }
}
