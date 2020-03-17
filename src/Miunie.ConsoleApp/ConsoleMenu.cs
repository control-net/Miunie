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

        private readonly int _outstandingLines = 7;

        private string _title;

        private int _selectedIndex;

        private int _selectedPageIndex;

        private int _pageSize;

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

            SetPageSize();

            _selectedIndex = 0;
            ConsoleKeyInfo keyPressed;

            do
            {
                SetPageSize();
                PrintTitle();
                PrintItems();

                keyPressed = Console.ReadKey(true);
                HandleMovementKey(keyPressed);

                Console.Clear();
            }
            while (keyPressed.Key != ConsoleKey.Enter);

            _selectedIndex = (_pageSize * _selectedPageIndex) + _selectedIndex;
            return _items.ElementAt(_selectedIndex);
        }

        private void HandleMovementKey(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.UpArrow)
            {
                if (--_selectedIndex == -1)
                {
                    _selectedIndex = Paginator.GroupAt(_items, _selectedPageIndex, _pageSize).Count() - 1;
                }
            }

            if (keyPressed.Key == ConsoleKey.DownArrow)
            {
                if (++_selectedIndex >= Paginator.GroupAt(_items, _selectedPageIndex, _pageSize).Count())
                {
                    _selectedIndex = 0;
                }
            }

            if (keyPressed.Key == ConsoleKey.LeftArrow)
            {
                if (_selectedPageIndex != 0)
                {
                    _selectedPageIndex--;
                    _selectedIndex = 0;
                }
            }

            if (keyPressed.Key == ConsoleKey.RightArrow)
            {
                if (_selectedPageIndex != Paginator.GetPageCount(_items.Count(), _pageSize) - 1)
                {
                    _selectedPageIndex++;
                    _selectedIndex = 0;
                }
            }
        }

        private void PrintItems()
        {
            var itemsInPage = Paginator.GroupAt(_items, _selectedPageIndex, _pageSize);
            var items = itemsInPage.Select((item, i) => (i == _selectedIndex ? "=>" : string.Empty) + _formatter.Invoke(item));
            Console.WriteLine(string.Join("\n", items));
            PrintMenuInfo();
        }

        private void PrintTitle()
        {
            if (!string.IsNullOrWhiteSpace(_title))
            {
                Console.WriteLine(_title);
                Console.WriteLine();
            }
        }

        private void PrintMenuInfo()
        {
            PrintToBottom(Paginator.GetPageFooter(_selectedPageIndex, _items.Count(), _pageSize) + "\n\n" + ConsoleStrings.USE_ARROWKEYS);
        }

        private void PrintToBottom(string message)
        {
            var prevCursorLeft = Console.CursorLeft;
            var prevCursorTop = Console.CursorTop;
            var lowestLine = Console.WindowHeight - 4;
            if (lowestLine < 0)
            {
                lowestLine = 0;
            }

            Console.SetCursorPosition(0, lowestLine);
            Console.Write(message);
            Console.SetCursorPosition(prevCursorLeft, prevCursorTop);
        }

        private void SetPageSize()
        {
            _pageSize = Console.WindowHeight;
            _pageSize -= _outstandingLines;
            _pageSize = Math.Clamp(_pageSize, 2, _items.Count());
        }
    }
}
