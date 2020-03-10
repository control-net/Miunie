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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Miunie.ConsoleApp
{
    internal class ConsoleMenu<T>
    {
        private readonly IEnumerable<T> _items;

        private readonly Func<T, string> _formatter;

        private string _title;

        private int _selectedIndex;

        internal ConsoleMenu(IEnumerable<T> items, Func<T, string> formatter)
        {
            _items = items;
            _formatter = formatter;
        }

        internal void SetTitle(string title)
        {
            _title = title;
        }

        internal T Present()
        {
            if (_items.Count() == 1)
            {
                return _items.First();
            }

            _selectedIndex = 0;
            ConsoleKeyInfo keyPressed;

            do
            {
                PrintTitle();
                PrintItems();

                keyPressed = Console.ReadKey(true);
                HandleMovementKey(keyPressed);

                Console.Clear();
            }
            while (keyPressed.Key != ConsoleKey.Enter);

            return _items.ElementAt(_selectedIndex);
        }

        private void HandleMovementKey(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                if (++_selectedIndex == _items.Count()) { _selectedIndex = 0; }
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                if (--_selectedIndex == -1) { _selectedIndex = _items.Count() - 1; }
            }
        }

        private void PrintItems()
        {
            var items = _items.Select((item, i) => (i == _selectedIndex ? _formatter.Invoke(item).Replace("#", "=>") : _formatter.Invoke(item)) + string.Empty);
            Console.WriteLine(string.Join("\n", items));
        }

        private void PrintTitle()
        {
            if (!string.IsNullOrWhiteSpace(_title))
            {
                Console.WriteLine(_title);
                Console.WriteLine();
            }
        }
    }
}
