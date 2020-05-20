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

using System.Linq;

namespace Miunie.ConsoleApp.Arguments
{
    internal static class ArgumentsParser
    {
        internal static RuntimeArguments Parse(string[] args)
        {
            return new RuntimeArguments
            {
                Headless = args.Contains(ConsoleStrings.HEADLESS_FLAG),
                Token = args.FirstOrDefault(arg => arg.StartsWith(ConsoleStrings.TOKENEQUALS_FLAG))?.Substring(8)
            };
        }
    }
}
