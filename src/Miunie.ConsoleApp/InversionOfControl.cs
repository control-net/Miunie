using Microsoft.Extensions.DependencyInjection;
using Miunie.ConsoleApp.Configuration;
using Miunie.Core.Infrastructure;
using Miunie.Core.Logging;
using Miunie.InversionOfControl;
using Miunie.SystemInfrastructure;

namespace Miunie.ConsoleApp
{
    public static class InversionOfControl
    {
        private static ServiceProvider _provider;

        public static ServiceProvider Provider => GetOrInitProvider();

        private static ServiceProvider GetOrInitProvider()
        {
            if (_provider is null)
            {
                InitializeProvider();
            }

            return _provider;
        }

        private static void InitializeProvider()
            => _provider = new ServiceCollection()
                .AddSingleton<ILogWriter, ConsoleBottomLogger>()
                .AddTransient<IDateTime, SystemDateTime>()
                .AddSingleton<IFileSystem, SystemFileSystem>()
                .AddSingleton<ConfigManager>()
                .AddSingleton<ConfigurationFileEditor>()
                .AddMiunieTypes()
                .BuildServiceProvider();
    }
}
