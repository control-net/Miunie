using System.Configuration;
using System;
using System.Linq;

namespace Miunie.Configuration
{
    public class ConfigManager : IConfiguration
    {
        public string GetValueFor(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (value is null)
            {
                var editor = new ConfigurationFileEditor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Configuration Error:");
                Console.WriteLine($"The configuration could not retrieve the item: '{key}'");

                editor.WriteSetting(key, "REPLACE-ME");
                editor.Save();

                Console.WriteLine("Enter your desired value: (or type EXIT)");
                value = Console.ReadLine();

                if (value.Trim().ToLower() == "exit") { Environment.Exit(0); }
                if (value.Trim().ToLower() == "i'm feeling lucky")
                {
                    var rng = new Random();
                    value = $"{RandomString(24)}.{RandomString(6)}.{RandomString(27)}";
                    Console.WriteLine($"Let's try {value}. I have a good feeling about this one.");
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
