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

namespace Miunie.WindowsApp.Models
{
    public class ObservableImage : ObservableObject
    {
        private string _proxyUrl;
        private int? _width;
        private int? _height;

        public string ProxyUrl
        {
            get => _proxyUrl;
            set
            {
                _proxyUrl = value;
                RaisePropertyChanged(nameof(ProxyUrl));
            }
        }

        public int? Width
        {
            get => _width;
            set
            {
                _width = value;
                RaisePropertyChanged(nameof(Width));
            }
        }

        public int? Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged(nameof(Height));
            }
        }
    }
}
