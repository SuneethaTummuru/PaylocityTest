using API.AutomationChallenge.Domain.Models;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.AutomationChallenge.Core.EndPoints
{
    class EmployeesApi : BaseApi
    {
        string _token;
        public EmployeesApi(string token) : base(token)
        {
            _token = token;
        }
        public async Task<RestResponse> GetAEmployees()
        {
            var request = new RestRequest($"{client.Options.BaseUrl}Prod/api/employees", Method.Get);
            //request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Authorization", $"{_token}");
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> CreateEmployee(object employee)
        {
            var request = new RestRequest($"{client.Options.BaseUrl}Prod/api/employeesr", Method.Post);
            var jsonuser = JsonSerializer.Serialize(employee);

            request.AddJsonBody(jsonuser);
            return await client.ExecuteAsync(request);
        }
        public async Task<RestResponse<Employee>> GetEmployee(int id)
        {
            var request = new RestRequest($"{client.Options.BaseUrl}Prod/api/employees/{id}", Method.Get);
            return await client.ExecuteAsync<Employee>(request);
        }

        public async Task<RestResponse> UpdateEmployee(object body)
        {
            var request = new RestRequest($"{client.Options.BaseUrl}Prod/api/employees", Method.Put);
            request.AddJsonBody(body);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> DeleteEmployee(int empId)
        {
            var request = new RestRequest($"{client.Options.BaseUrl}Prod/api/employees/{empId}", Method.Delete);
            request.AddParameter("userId", empId, ParameterType.QueryString);
            return await client.ExecuteAsync(request);
        }
    }
}
