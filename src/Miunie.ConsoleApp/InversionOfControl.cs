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
using Miunie.ConsoleApp.Configuration;
using Miunie.Core.Infrastructure;
using Miunie.Core.Logging;
using Miunie.Infrastructure.OS;
using Miunie.InversionOfControl;

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
