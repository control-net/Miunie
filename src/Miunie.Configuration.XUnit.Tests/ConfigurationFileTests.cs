using System;
using Miunie.Discord.Configuration;
using Xunit;

namespace Miunie.Configuration.XUnit.Tests
{
    // NOTE (Peter): The configuration file for the test is
    // "testhost.dll.config" and it is included through the
    // .csproj. However, a real application just needs an
    // "App.config" to be in the project directory (in our
    // case Miunie.ConsoleApp).

    // App.config is not included in the repo, since it
    // contains the bot token. However, there is an
    // "App.config.template" that provides the basic 
    // structure. Copy that file and rename it to
    // "App.config" to debug Miunie locally.

    // When the project is built, App.config turns into
    // [AssemblyName].config in our case the resulting
    // config file is "Miunie.ConsoleApp.dll.config"
    // this information is important only for people
    // deploying an instance of Miunie.

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
