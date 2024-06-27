using API.AutomationChallenge.Core.EndPoints;
using API.AutomationChallenge.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace API.AutomationChallenge.Domain.Services
{
    class AuthService
    {
        public AuthToken LoginUser(string userName, string pwd)
        {
            var response = LoginUserRequest(userName, pwd);
            var authToken = GetToken(response);
            return authToken;
        }

        public RestResponse LoginUserRequest(string userName, string pwd)
        {
            var authAPI = new AuthApi(null);
            var response = authAPI.LoginTestAsync(userName, pwd);
            return response.Result;

        }

        public RestResponse LoginTokenRequest(string Token)
        {
            var authAPI = new AuthApi(null);
            var response = authAPI.LoginTestAsync(Token);
            return response.Result;

        }

        public AuthToken GetToken(RestResponse responseLogin)
        {
            try
            {
                var content = (JObject)JsonConvert.DeserializeObject(responseLogin.Content);
                var authToken = content.ToObject<AuthToken>();
                return authToken;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string? GetContent(RestResponse responseLogin1)
        {
            return responseLogin1.Content;
        }

        public void authenticateWithtoken()
        {
        }
    }
}
