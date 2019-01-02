using System.Configuration;
using Miunie.Configuration.Entities;

namespace Miunie.Configuration
{
    public class ConfigManager : IConfiguration
    {
        private readonly ConfigurationFileEditor _editor;

        private ConfigManager(ConfigurationFileEditor editor)
        {
            _editor = editor;
        }

        public string GetValueFor(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public void SetValueFor(string key, string value)
        {
            StoreSetting(new KeyValuePair(key, value));
        }

        private void StoreSetting(KeyValuePair setting)
        {
            _editor.WriteSetting(setting);
            _editor.Save();
        }
    }
}