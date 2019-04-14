using Microsoft.Extensions.DependencyInjection;
using Miunie.Core.Infrastructure;
using Miunie.Core.Logging;
using Miunie.InversionOfControl;
using Miunie.SystemInfrastructure;
using Miunie.WindowsApp.Infrastructure;

namespace Miunie.WindowsApp
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
                .AddSingleton<ILogger, UwpLogger>()
                .AddTransient<IDateTime, SystemDateTime>()
                .AddSingleton<IFileSystem, UwpFileSystem>()
                .AddMiunieTypes()
                .BuildServiceProvider();
    }
}
