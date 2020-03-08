// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Configuration;
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

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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
    }
}
