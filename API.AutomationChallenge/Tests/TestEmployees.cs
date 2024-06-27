using FluentAssertions;
using System.Net;
using Xunit;

namespace API.AutomationChallenge.Tests
{
    public class TestEmployees : BaseTest
    {
        [Fact]
        public void GetEmployeesList()
        {
            //access 
            var responseList = empSrv.GetEmployeesList($"Basic {tokenUsr}");
            responseList.Content.Should().NotBeNull();
            Assert.Equal(HttpStatusCode.OK, responseList.StatusCode);
        }


        [Fact]
        public void AddEmployee()
        {
            var firstName = "<b>New";
            var lastName = "Employee";
            var dependants = 3;

            //add employee
            var responseCreate = empSrv.CreateEmployee(tokenUsr, firstName, lastName, dependants);
            Assert.Equal(HttpStatusCode.BadRequest, responseCreate.StatusCode);
        }

        [Fact]
        public void GetEmployeeDetails()
        {
            var employee = empSrv.CreateEmployee(tokenUsr).Data;

            //get employee details
            var employeeDetails = empSrv.GetEmployee(tokenUsr, employee.EmployeeId);
            employeeDetails.StatusCode.Should().Be(HttpStatusCode.OK);
            employeeDetails.Data.Should().BeEquivalentTo(employee);
        }


        [Fact]
        public void UpdateEmployee()
        {
            var initialEmployee = empSrv.CreateEmployee(tokenUsr).Data;

            //update employee
            var responseUpdate = empSrv.UpdateEmployee(tokenUsr, "firstname", "lastname", initialEmployee.EmployeeId);
            responseUpdate.StatusCode.Should().Be(HttpStatusCode.NoContent);

            //check employee is updated
            var updatedEmployee = empSrv.CreateEmployee(tokenUsr).Data;
            updatedEmployee.EmployeeId.Should().Be(initialEmployee.EmployeeId);
            updatedEmployee.FirstName.Should().NotBe(initialEmployee.FirstName);
        }

        [Fact]
        public void DeleteEmployee()
        {
            var employee = empSrv.CreateEmployee(tokenUsr).Data;

            //delete employee
            var response = empSrv.DeleteEmployee(tokenUsr, employee.EmployeeId);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            //check employee is not present anymore
            var getEmployee = empSrv.GetEmployee(tokenUsr, employee.EmployeeId);
            getEmployee.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
