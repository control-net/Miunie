using System.Configuration;
using Miunie.Configuration.Entities;

namespace Miunie.Configuration
{
    internal class ConfigurationFileEditor
    {
        private readonly System.Configuration.Configuration _file;

        internal ConfigurationFileEditor()
        {
            _file = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        internal void Save()
        {
            _file.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_file.AppSettings.SectionInformation.Name);
        }

        internal void WriteSetting(KeyValuePair setting)
        {
            if(SettingExists(setting))
            {
                UpdateSetting(setting);
            }
            else
            {
                CreateSetting(setting);
            }
        }

        private void CreateSetting(KeyValuePair setting)
        {
            _file.AppSettings.Settings.Add(setting.Key, setting.Value);
        }

        private void UpdateSetting(KeyValuePair setting)
        {
            _file.AppSettings.Settings[setting.Key].Value = setting.Value;
        }

        private bool SettingExists(KeyValuePair setting)
        {
            return !(_file.AppSettings.Settings[setting.Key] is null);
        }
    }
}