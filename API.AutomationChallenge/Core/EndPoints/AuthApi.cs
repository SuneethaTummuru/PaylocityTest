using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;

namespace API.AutomationChallenge.Core.EndPoints
{
    class AuthApi : BaseApi
    {
        public AuthApi(string token) : base(token)
        { }

        public async Task<RestResponse> LoginTestAsync(string uname, string pwd)
        {
            var request = new RestRequest($"{client.Options.BaseUrl}Prod/Account/LogIn", Method.Get);
            request.AddJsonBody(new
            {
                uname,
                pwd
            });
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> RefreshToken(string accessToken, string refreshToken)
        {
            var request = new RestRequest($"{client.Options.BaseUrl}api/Auth/token/refresh", Method.Post);
            request.AddParameter("accessToken", accessToken, ParameterType.QueryString);
            request.AddParameter("refreshToken", refreshToken, ParameterType.QueryString);
            return await client.ExecuteAsync(request);
        }


        public async Task<RestResponse> LogoutAsync()
        {
            var request = new RestRequest($"{client.Options.BaseUrl}api/Auth/logout", Method.Post);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> LoginTestAsync(string token)
        {
            var request = new RestRequest($"{client.Options.BaseUrl}Prod/Account/LogIn", Method.Post);

            // Add the Basic Token to the Authorization header
            //request.AddHeader("Authorization", $"{token}");

            return await client.ExecuteAsync(request);
        }
    }
}
