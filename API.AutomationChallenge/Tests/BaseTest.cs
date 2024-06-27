using API.AutomationChallenge.Domain.Services;
using API.AutomationChallenge.Domain.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace API.AutomationChallenge.Tests
{
    [Collection("Setup Workspace")]
    public class BaseTest
    {
        internal readonly AuthService authSrv = new();
        internal readonly EmployeeService empSrv = new();
        internal static EnvConfig env = EnvConfig.Load();
        internal string tokenUsr = env.Token;

        public void ValidateResponseSchema(RestResponse response, string schemaFile)
        {
            var content = JsonConvert.DeserializeObject(response.Content);
            JSchema schema = Helper.GetSchema(schemaFile);

            IList<string> messages;
            object? content1 = content;
            if (content1.GetType() == typeof(JObject))
            {
                JObject contentTocheck = (JObject)content;
                Assert.True(contentTocheck.IsValid(schema, out messages), "Error messages: " + String.Join("", messages.ToArray()));
            }
            else
            {
                JArray contentTocheck = (JArray)content;
                Assert.True(contentTocheck.IsValid(schema, out messages), "Error messages: " + String.Join("", messages.ToArray()));
            }

        }
    }
}
