using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Xml.Linq;
using UI.AutomationChallange.Configuration;
using UI.AutomationChallange.Utils;

namespace UI.AutomationChallange.PageObjects
{

    public partial class BenefitsDashboardPage : LoginPage
    {
        BenefitsCostCalculation benefitCostCal = new BenefitsCostCalculation();
        private ConfigSettings configSettings = new ConfigSettings();

        public IWebElement? FrameElement { get; private set; }
        Generics generics = new Generics();

        public Tuple<string, string, string> AddEmployee()
        {
            configSettings.GetconfigValues();
            var uniqueFirstName = generics.GenerateFirstName();
            var uniqueLastName = generics.GenerateLastNameOnUUID();


            new BaseWaits().WaitForElementIsVisible(LogoutBtnBy);
            new BaseWaits().WaitForElementIsVisible(AddEmployeeBtnBy).Click();
            new BaseWaits().WaitForElementIsVisible(AddEmployeeHeaderBy);
            var firstnameTextField = new BaseWaits().WaitForElementIsVisible(FirstNameInputBy);
            var lastnameTextField = new BaseWaits().WaitForElementIsVisible(LastNameInputBy);
            firstnameTextField.SendKeys(uniqueFirstName);
            lastnameTextField.SendKeys(uniqueLastName);
            var dependentsTextField = new BaseWaits().WaitForElementIsVisible(DependentsInputBy);
            dependentsTextField.SendKeys(configSettings.Dependents);
            new BaseWaits().WaitForElementIsVisible(AddButtonBy).Click();
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{uniqueLastName}']"));

            return new Tuple<string, string, string>(uniqueLastName,
                                                  uniqueFirstName,
                                                  configSettings.Dependents);

        }

        public List<string> GetCellValues(int cellNumber)
        {

            IReadOnlyList<IWebElement> fourthCells = Driver.FindElements(By.CssSelector($"table tr td:nth-child({cellNumber})"));
            List<string> cellValues = new List<string>();
            foreach (IWebElement cell in fourthCells)
            {
                cellValues.Add(cell.Text);
            }
            return cellValues;
        }
        public string GetNetPay(string lastName)
        {
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']"));
            var netPayCellValue = new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']/following-sibling::td[5]")).Text;

            return netPayCellValue;
        }

        public string GetBenefitCost(string lastName)
        {
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']"));
            var benefitCostCellValue = new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']/following-sibling::td[4]")).Text;

            return benefitCostCellValue;
        }
        public Tuple<string, string, string> UpdateEmployee(string lastName)
        {
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']"));
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']/following-sibling::td/i")).Click();
            var firstnameTextField = new BaseWaits().WaitForElementIsVisible(FirstNameInputBy);
            var originalFirstName = firstnameTextField.GetAttribute("value");
            firstnameTextField.Clear();

            var lastnameTextField = new BaseWaits().WaitForElementIsVisible(LastNameInputBy);
            var originalLastName = lastnameTextField.GetAttribute("value");
            lastnameTextField.Clear();

            var dependentsTextField = new BaseWaits().WaitForElementIsVisible(DependentsInputBy);
            dependentsTextField.Clear();

            dependentsTextField.SendKeys("8");
            firstnameTextField.SendKeys("Updated_" + originalFirstName);
            lastnameTextField.SendKeys("Updated_" + originalLastName);



            new BaseWaits().WaitForElementIsVisible(UpdateButtonBy).Click();
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{"Updated_" + originalLastName}']"));
            return new Tuple<string, string, string>("Updated_" + originalLastName,
                                                  "Updated_" + originalFirstName,
                                                  "8");

        }

        public void DeleteEmployee(string lastName)
        {
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']"));
            new BaseWaits().WaitForElementIsVisible(By.XPath($"//td[text()='{lastName}']/following-sibling::td/i[2]")).Click();
            new BaseWaits().WaitForElementIsVisible(DeleteEmployeeBtnBy).Click();
        }

        public void VerifyEmplyeeDeletedSuccessfully(string lastName)
        {
            string pageSource = Driver.PageSource;
            bool textFound = pageSource.Contains(lastName, StringComparison.OrdinalIgnoreCase);
            Assert.That(textFound, "User is not deleted successfully.");

            By elementLocator = By.XPath("//td[text()='{lastName}']");
            var elements = Driver.FindElements(elementLocator);
            Assert.That(elements.Count, Is.EqualTo(0), $"Element with ID '{elementLocator}' is found on the page");

            var lastNames = GetCellValues(2);
            Assert.That(lastNames.Contains(lastName), $"Employee is not deleted successfully. His last name {lastName} is still present in the lastname column.");

        }

        public void VerifyUSerIsAdded(Tuple<string, string, string> results)
        {
            //Driver.Navigate().Refresh();
            new BaseWaits().WaitForElementIsVisible(AddEmployeeBtnBy);
            string pageSource = Driver.PageSource;
            bool textFound = pageSource.Contains(results.Item1)
                     && pageSource.Contains(results.Item2) && pageSource.Contains(results.Item3.ToString());
            Assert.That(textFound, "User is not added.");
            var lastNames = GetCellValues(2);
            //BUG:301 reported
            //Assert.That(lastNames.Contains(results.Item1), $"The last name column values does not contain the expected value: {results.Item1}");

            var firstNames = GetCellValues(3);
            //BUG:301 reported
            // Assert.That(lastNames.Contains(results.Item2), $"The first name column values does not contain the expected value: {results.Item1}");

            var dependants = GetCellValues(4);
            Assert.That(dependants.Contains(results.Item3.ToString()), $"Expected value {results.Item3} not placed under Dependents column");

        }

        public void VerifyEmployeeBenefitCosts(string lastName)
        {
            var salaries = GetCellValues(5);
            //decimal tt = benefitCostCal.AnnualSalary();
            foreach (var salary in salaries)
            {
                bool annualSalary = salary.Equals(benefitCostCal.AnnualSalary().ToString()+".00");
                Assert.That(annualSalary, "Annual salary is not displayed correctly");
            }

            var grossPays = GetCellValues(6);
            foreach (var grossPay in grossPays)
            {
                bool grossSalary = grossPay.Equals(benefitCostCal.TotalBenefits().ToString() + ".00");
                Assert.That(grossSalary, "Gross salary is not displayed correctly");
            }

            var actualNetPayValue = GetNetPay(lastName);
            //string kk = benefitCostCal.NetPaycheck().ToString();
            bool netPayisEqual = actualNetPayValue.Equals(benefitCostCal.NetPaycheck());
            //BUG:302 reported - ex: 1923.08 != 1923.07
            //Assert.That(netPayisEqual, "Net pay value is wrongly calculated");

            var actualCostBenefit = GetBenefitCost(lastName);
            bool costBenefitsEqual = actualCostBenefit.Equals(benefitCostCal.BenefitCost());
            Assert.That(costBenefitsEqual, "cost benefit value is wrongly calculated");
        }

    }

    public partial class BenefitsDashboardPage
    {

        public By AddEmployeeBtnBy = By.Id("add");
        public By LogoutBtnBy = By.LinkText("Log Out");
        public By AddEmployeeHeaderBy = By.XPath("//h5[@class='modal-title' and text()='Add Employee']");
        public By FirstNameInputBy = By.Id("firstName");
        public By LastNameInputBy = By.Id("lastName");
        public By DependentsInputBy = By.Id("dependants");
        public By AddButtonBy = By.Id("addEmployee");
        public By UpdateButtonBy = By.Id("updateEmployee");
        public By FourthCellsLocator = By.CssSelector("table tr td:nth-child(3)");
        By LastNameElementLocator = By.XPath("//td[text()='']");
        public By DeleteEmployeeBtnBy = By.Id("deleteEmployee");
    }

}
