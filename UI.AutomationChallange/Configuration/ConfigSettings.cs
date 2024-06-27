using Microsoft.Extensions.Configuration;

namespace UI.AutomationChallange.Configuration
{
    public class ConfigSettings
    {
        string pathToSettingsFile = Path.GetFullPath(@"..\..\..\") + @"Configuration\AppSettings.Test.json";
        public string? BrowserType { get; set; }
        public string? Environment { get; set; }
        public string? ImplicitWaitInMillSeconds { get; set; }
        public string? ExternalServiceUrl { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Dependents { get; set; }


        public void GetconfigValues()
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile(pathToSettingsFile).Build();
            BrowserType = builder["Browser"];
            Environment = builder["Environment"];
            ImplicitWaitInMillSeconds = builder["ImplicitWaitInMillSeconds"];
            ExternalServiceUrl = builder["ExternalServiceUrl"];
            Username = builder["Username"];
            Password = builder["Password"];
            Dependents = builder["Dependents"];
        }
    }
}
