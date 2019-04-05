using System.Configuration;

namespace Miunie.Configuration
{
    class ConfigurationFileEditor
    {
        private readonly System.Configuration.Configuration file;

        internal ConfigurationFileEditor()
        {
            file = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        internal void Save()
        {
            file.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(file.AppSettings.SectionInformation.Name);
        }

        internal void WriteSetting(string key, string value)
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
            file.AppSettings.Settings.Add(key, value);
        }

        private void UpdateSetting(string key, string value)
        {
            file.AppSettings.Settings[key].Value = value;
        }

        private bool SettingExists(string key)
        {
            return !(file.AppSettings.Settings[key] is null);
        }
    }
}
