using Miunie.Discord.Configuration;
using Xunit;
using Moq;
using Miunie.Core.Logging;

namespace Miunie.Configuration.XUnit.Tests
{
    public class ConfigurationFileTests
    {
        private const string ExpectedTestToken = "TestToken123";

        [Fact]
        public void ShouldRetrieveBotToken()
        {
            var config = CreateConfigInstance();
            var actual = config.GetBotToken();
            Assert.Equal(ExpectedTestToken, actual);
        }

        private static IBotConfiguration CreateConfigInstance()
        {
            var configManager = new ConfigManager(new Mock<ILogger>().Object);
            return new BotConfiguration(configManager);
        }
    }
}
