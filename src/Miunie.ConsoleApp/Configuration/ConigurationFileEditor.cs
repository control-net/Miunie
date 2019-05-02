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
