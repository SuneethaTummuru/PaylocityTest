using NUnit.Framework;
using UI.AutomationChallange.PageObjects;
using UI.AutomationChallange.Utils;

namespace UI.AutomationChallange.Tests
{

    [TestFixture]
    public class BeneftsDashboardTests : BaseTest
    {
        BenefitsCostCalculation benefitCostCal = new BenefitsCostCalculation();
        [Test]
        public void AddEditDeleteEmployee()
        {
            //Add employee
             Tuple<string, string, string> results = new BenefitsDashboardPage().AddEmployee();
             Console.WriteLine($"LastName: {results.Item1}, FirstName: {results.Item2}, Dependant: {results.Item3}");


             //assertions if user is added
             new BenefitsDashboardPage().VerifyUSerIsAdded(results);

             //Assertions on employee benefit costs 
             new BenefitsDashboardPage().VerifyEmployeeBenefitCosts(results.Item1); 

             //Update employee details
             var newResults = new BenefitsDashboardPage().UpdateEmployee(results.Item1);

             //assertions if user is updated
             new BenefitsDashboardPage().VerifyUSerIsAdded(newResults);

             //Assertions on employee benefit costs after updates 
             new BenefitsDashboardPage().VerifyEmployeeBenefitCosts(results.Item1);

            //Delete an employee 
            new BenefitsDashboardPage().DeleteEmployee(results.Item1);

            //Verify employee deleted successfully
            new BenefitsDashboardPage().VerifyEmplyeeDeletedSuccessfully(results.Item1);


        }

    }
}
