using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.AutomationChallange.Utils
{
    internal class BenefitsCostCalculation
    {
        // Define constants
        const decimal PayPerPaycheck = 2000m;
        const int PaychecksPerYear = 26;
        const decimal EmployeeBenefitCost = 1000m;
        const decimal DependentBenefitCost = 500m;

        // Input number of dependents
        int numberOfDependents = 2; // Example input

        public decimal AnnualSalary()
        {
            decimal annualSalary = PayPerPaycheck * PaychecksPerYear;
            return annualSalary;
        }
        public decimal TotalBenefits()
        {
            decimal totalBenefitCost = EmployeeBenefitCost + (numberOfDependents * DependentBenefitCost);
            return totalBenefitCost;
        }

        public decimal NetAnnualSalary()
        {
            decimal annualSalary = AnnualSalary();
            decimal totalBenefitCost = TotalBenefits();
            decimal netAnnualSalary = annualSalary - totalBenefitCost;

            return netAnnualSalary;
        }

        public string BenefitCost()
        {
            decimal netAnnualSalary = NetAnnualSalary();
            decimal netPaycheck = netAnnualSalary / PaychecksPerYear;
            string[] parts = netPaycheck.ToString().Split('.');
            string valueAfterPeriod = parts[1];
            valueAfterPeriod = valueAfterPeriod.TrimEnd('M');
            valueAfterPeriod = valueAfterPeriod.TrimStart('0');
            string benefitCost = valueAfterPeriod.Substring(0, valueAfterPeriod.Length - 20); // Remove the last two digits
            benefitCost = benefitCost.Insert(2, ".");

            return benefitCost;
        }
        public string NetPaycheck()
        {
            decimal netAnnualSalary = NetAnnualSalary();
            decimal netPaycheck = netAnnualSalary / PaychecksPerYear;
            int decimalIndex = netPaycheck.ToString().IndexOf('.');
            string netPay = netPaycheck.ToString().Substring(0, decimalIndex + 3);


            return netPay;
        }
    }
}
