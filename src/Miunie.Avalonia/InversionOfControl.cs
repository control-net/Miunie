// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Microsoft.Extensions.DependencyInjection;
using Miunie.Avalonia.Utilities;
using Miunie.Core.Infrastructure;
using Miunie.Core.Logging;
using Miunie.InversionOfControl;
using Miunie.Logger;
using Miunie.SystemInfrastructure;
using System.Net.Http;

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
                .AddSingleton<HttpClient>()
                .AddSingleton<UrlImageConverter>()
                .AddMiunieTypes()
                .BuildServiceProvider();
    }
}
