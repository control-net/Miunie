using System.Configuration;
using System;
using System.Linq;

namespace Miunie.ConsoleApp.Configuration
{
    public class ConfigManager
    {
        public string GetValueFor(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (!(value is null)) { return value; }

            Console.WriteLine(ConsoleStrings.CONFIG_ERROR, key);

            var editor = GenerateNewConfigFile(key);

            value = GetTokenFromUser();
            editor.WriteSetting(key, value);
            editor.Save();

            return value;
        }

        private ConfigurationFileEditor GenerateNewConfigFile(string expectedKey)
        {
            var editor = new ConfigurationFileEditor();
            editor.WriteSetting(expectedKey, "REPLACE-ME");
            editor.Save();
            return editor;
        }

        private string GetTokenFromUser()
        {
            var value = Console.ReadLine();
            if (value is null || value.Trim().ToLower() == "exit") { Environment.Exit(0); }

            if (value.Trim().ToLower() == "i'm feeling lucky")
            {
                value = $"{RandomString(24)}.{RandomString(6)}.{RandomString(27)}";
                Console.WriteLine(ConsoleStrings.RANDOM_TOKEN_MESSAGE, value);
            }

            return value;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
