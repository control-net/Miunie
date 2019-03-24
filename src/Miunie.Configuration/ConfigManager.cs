using System.Configuration;

namespace Miunie.Configuration
{
    public class ConfigManager : IConfiguration
    {
        public string GetValueFor(string key) => ConfigurationManager.AppSettings[key];
    }
}

