using System;
using Miunie.Discord.Configuration;
using Xunit;

namespace Miunie.Configuration.XUnit.Tests
{
    public class ConfigurationFileTests
    {
        private const string ExpectedTestToken = "TestToken123";

        [Fact]
        public void ShouldRetreiveBotToken()
        {
            var config = CreateConfigInstance();
            var actual = config.GetBotToken();
            Assert.Equal(ExpectedTestToken, actual);
        }

        private IBotConfiguration CreateConfigInstance()
        {
            var fileEditor = new ConfigurationFileEditor();
            var configManager = new ConfigManager(fileEditor);
            return new BotConfiguration(configManager);
        }
    }
}
