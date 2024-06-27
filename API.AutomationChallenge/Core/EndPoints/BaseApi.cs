using API.AutomationChallenge.Domain.Utils;
using RestSharp.Authenticators;
using RestSharp;

namespace API.AutomationChallenge.Core.EndPoints
{
    class BaseApi
    {
        internal RestClient client;
        public BaseApi(string? token = null)
        {
            RestClientOptions options = new(EnvConfig.Load().BaseApiUrl);

            if (!string.IsNullOrEmpty(token))
                options.Authenticator = new JwtAuthenticator(token);
            client = new RestClient(options);
        }
    }
}
