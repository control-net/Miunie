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

using GalaSoft.MvvmLight;
using Miunie.Core;
using Miunie.Core.Entities.Views;
using System.Collections.Generic;

namespace Miunie.WindowsApp.ViewModels
{
    public class ServersPageViewModel : ViewModelBase
    {
        private readonly MiunieBot _miunie;

        public ServersPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
        }

        public IEnumerable<GuildView> AvailableGuilds => _miunie.Impersonation.GetAvailableGuilds();
    }
}
