using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using UI.AutomationChallange.Configuration;
using UI.AutomationChallange.PageObjects;

namespace UI.AutomationChallange.Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected string? OpenPageUrl = null;
        public static IWebDriver Driver { get; set; } = null!;
        public static object? nulll { get; private set; }
        private ChromeOptions Options { get; set; } = new ChromeOptions();

        private string rootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        ConfigSettings configSettings = new ConfigSettings();


        [OneTimeSetUp]
        public void BrowserInitialize()
        {
            configSettings.GetconfigValues();
            switch (configSettings.BrowserType)
            {
                case "Chrome":

                    //Set browser test arguments
                    Options.AddArguments("test-type");
                    Options.AddArguments("enable-automation");
                    Options.AddArguments("test-type=browser");
                    Options.AddArguments("disable-infobars");
                    Options.AddArguments("start-maximized");
                    //Options.AddArguments("incognito"); since tests are not working on incognito window
                    Options.AddArguments("disable-gpu"); //required for windows


                    Driver = new ChromeDriver(rootPath + @"\Drivers\", Options);
                    break;
                case "IE":
                    Driver = new InternetExplorerDriver(rootPath + @"\Drivers\");
                    break;
                case "FF":
                    Driver = new FirefoxDriver(rootPath + @"\Drivers\");
                    break;
                default:
                    Driver = new ChromeDriver(rootPath + @"\Drivers\", Options);
                    break;
            }
            InitializeUrl();
            Driver.Navigate().GoToUrl(OpenPageUrl);
            Driver.Manage().Window.Maximize();
            new LoginPage().VerifyPageLoad();
            new LoginPage().LoginToBenefitsDashboard();
        }
        public void InitializeUrl()
        {
            switch (configSettings.Environment)
            {
                case "Dev":
                    OpenPageUrl = "https://dev.wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/Account/Login";
                    break;
                case "Stage":
                    OpenPageUrl = "https://stage.wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/Account/Login";
                    break;
                case "Test":
                    OpenPageUrl = "https://wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/Account/Login";
                    break;
                case "Uat":
                    OpenPageUrl = "https://uat.wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/Account/Login";
                    break;
                case "Prod":
                    OpenPageUrl = "https://prod.wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/Account/Login";
                    break;
                default:
                    OpenPageUrl = "https://wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/Account/Login";
                    break;

            }

        }

        [SetUp]
        public void BeforeMethod()
        {
            new LoginPage().WriteTimeStampedLine("Staring Method: " + TestContext.CurrentContext.Test.Name);
        }

        [OneTimeTearDown]

        public void TearDown()
        {
            new LoginPage().WriteTimeStampedLine("Test suit run ended.");

            try
            {
                Driver.Quit();
            }
            catch (Exception ex)
            {

                new LoginPage().WriteExceptionInfo(ex);

                throw;
            }

            Driver.Quit();

        }
        [TearDown]
        public void CloseAllBrowserWindows()
        {
            #region OnFailure

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var timestamp = DateTime.Now.ToString("dd.MM-HH.mm.ss");
                try
                {
                    var screen = (ITakesScreenshot)Driver;

                    screen.GetScreenshot().SaveAsFile(rootPath + @"\ScreenShots\" + TestContext.CurrentContext.Test.Name + "_" + timestamp + ".png");
                    TestContext.AddTestAttachment(rootPath + @"\ScreenShots\" + TestContext.CurrentContext.Test.Name + "_" + timestamp + ".jpg");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to create screenshot. \n" +
                                  ex.Message);
                }

            }
            #endregion

            try
            {
                if (!(Driver.WindowHandles.Count > 0)) return;

                foreach (var handle in Driver.WindowHandles)
                {
                    Driver.SwitchTo().Window(handle);
                    Driver.Close();
                }
            }
            catch (Exception ex)
            {
                new LoginPage().WriteExceptionInfo(ex);
                throw;
            }
        }
    }
}
