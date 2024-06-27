using API.AutomationChallenge.Core.EndPoints;
using API.AutomationChallenge.Domain.Models;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace API.AutomationChallenge.Domain.Services
{
    class EmployeeService
    {
        AuthService authServ = new AuthService();
        public RestResponse GetEmployeesList(string token)
        {
            RestResponse loginResponse = authServ.LoginTokenRequest(token);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            var emplyeesApi = new EmployeesApi(token);
            var response = emplyeesApi.GetAEmployees();
            return response.Result;
        }
        public RestResponse CreateEmployee(string token, string firstName = null, string lastName = null, int dependants = 0)
        {
            var emplyeesApi = new EmployeesApi(token);
            var newEmployee = new
            {
                firstName,
                lastName,
                dependants

            };
            var response = emplyeesApi.CreateEmployee(newEmployee);
            return response.Result;
        }
        public RestResponse<Employee> GetEmployee(string token, int id)
        {
            var emplyeesApi = new EmployeesApi(token);
            var response = emplyeesApi.GetEmployee(id);
            return response.Result;
        }

        public RestResponse UpdateEmployee(string token, string firstName, string lastName, int dependant)
        {
            var emplyeesApi = new EmployeesApi(token);
            var editEmployee = new
            {
                firstName,
                lastName,
                dependant

            };

            var response = emplyeesApi.UpdateEmployee(editEmployee);
            return response.Result;

        }
        public RestResponse DeleteEmployee(string token, int vendorId)
        {
            var emplyeesApi = new EmployeesApi(token);
            var response = emplyeesApi.DeleteEmployee(vendorId);

            return response.Result;
        }
    }
}
