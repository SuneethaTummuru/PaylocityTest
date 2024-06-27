using OpenQA.Selenium;
using UI.AutomationChallange.Tests;

namespace UI.AutomationChallange.PageObjects
{
    public partial class BasePage
    {

        public IWebDriver Driver => BaseTest.Driver;
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

            new BaseWaits().WaitForElementIsVisible(AcceptAllButtonBy);
        }

        public void AcceptCookies()
        {

            new BaseWaits().WaitForElementClickable(AcceptAllButtonBy).Click();
            new BaseWaits().WaitForElementClickable(BrandingLabel);
        }

    }
    public partial class BasePage
    {
        public By AcceptAllButtonBy = By.Id("cookiescript_accept");
        public By BrandingLabel = By.Id("branding");
    }
}
