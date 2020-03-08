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

using System.Configuration;

namespace Miunie.ConsoleApp.Configuration
{
    public class ConfigurationFileEditor
    {
        private readonly System.Configuration.Configuration _file;

        public ConfigurationFileEditor()
        {
            _file = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        public void Save()
        {
            _file.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_file.AppSettings.SectionInformation.Name);
        }

        public void WriteSetting(string key, string value)
        {
            if (SettingExists(key))
            {
                UpdateSetting(key, value);
            }
            else
            {
                CreateSetting(key, value);
            }
        }

        private void CreateSetting(string key, string value)
        {
            _file.AppSettings.Settings.Add(key, value);
        }

        private void UpdateSetting(string key, string value)
        {
            _file.AppSettings.Settings[key].Value = value;
        }

        private bool SettingExists(string key)
        {
            return !(_file.AppSettings.Settings[key] is null);
        }
    }
}
