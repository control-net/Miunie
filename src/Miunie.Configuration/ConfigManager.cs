using System.Configuration;
using System;
using System.Linq;
using Miunie.Core.Logging;

namespace Miunie.Configuration
{
    public class ConfigManager : IConfiguration
    {
        private readonly ILogger _logger;
        public ConfigManager(ILogger logger)
        {
            _logger = logger;
        }

        public string GetValueFor(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (value is null)
            {
                var editor = new ConfigurationFileEditor();

                var configErrorMessage = $"Configuration Error:\n" +
                    $"The configuration could not retrieve the item: '{key}'\n" +
                    $"Enter your desired value: (or type EXIT)";

                _logger.LogError(configErrorMessage);
                editor.WriteSetting(key, "REPLACE-ME");
                editor.Save();

                value = Console.ReadLine();

                if (value.Trim().ToLower() == "exit") { Environment.Exit(0); }
                if (value.Trim().ToLower() == "i'm feeling lucky")
                {
                    value = $"{RandomString(24)}.{RandomString(6)}.{RandomString(27)}";
                    _logger.Log($"Let's try {value}. I have a good feeling about this one.");
                }

                editor.WriteSetting(key, value);
                editor.Save();
            }

            return value;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
