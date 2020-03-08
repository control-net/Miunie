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

using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Miunie.Avalonia.Utilities
{
    public class UrlImageConverter
    {
        private readonly HttpClient _httpClient;

        public UrlImageConverter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Bitmap> UrlToBitmapAsync(string url)
        {
            Bitmap bitmap;
            var byteArray = await _httpClient.GetByteArrayAsync(url);
            using (var ms = new MemoryStream(byteArray))
            {
                bitmap = new Bitmap(ms);
            }

            return bitmap;
        }
    }
}
