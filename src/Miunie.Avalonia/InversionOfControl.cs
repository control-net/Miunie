using Microsoft.Extensions.DependencyInjection;
using Miunie.Core.Infrastructure;
using Miunie.Core.Logging;
using Miunie.InversionOfControl;
using Miunie.Logger;
using Miunie.SystemInfrastructure;

namespace Miunie.Avalonia
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
                .AddSingleton<ConsoleLogger>()
                .AddSingleton<InMemoryLogger>()
                .AddSingleton<ILogReader>(s => s.GetRequiredService<InMemoryLogger>())
                .AddSingleton<ILogWriter>(s => s.GetRequiredService<ConsoleLogger>())
                .AddTransient<IDateTime, SystemDateTime>()
                .AddSingleton<IFileSystem, SystemFileSystem>()
                .AddMiunieTypes()
                .BuildServiceProvider();
    }
}
