using Microsoft.Extensions.Configuration;

namespace API.AutomationChallenge.Domain.Utils
{
    class Config
    {
        public string Environment { get; set; }

        public static Config Load()
        {
            //load global configuration
            var config = new Config();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables() // load Azure pipeline variables
                .Build();
            builder.Bind(config);
            return config;
        }
    }
    class EnvConfig
    {
        public string BaseApiUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public static EnvConfig Load()
        {
            var globalConfig = Config.Load();
            var environment = globalConfig.Environment;

            //load environment configuration
            var config = new EnvConfig();
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"config.{environment}.json")
                .Build();

            builder.Bind(config);
            return config;
        }
    }
}
