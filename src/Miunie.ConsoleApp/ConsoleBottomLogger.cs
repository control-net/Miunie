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

using Miunie.Core.Logging;
using System;

namespace Miunie.ConsoleApp
{
    public class ConsoleBottomLogger : ILogWriter
    {
        public static void ClearConsoleLine(int top)
        {
            var currentTop = Console.CursorTop;
            var currentLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(currentLeft, currentTop);
        }

        public void Log(string message)
        {
            PrintToTop(message);
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintToTop(message);
            Console.ResetColor();
        }

        public void PrintToTop(string message)
        {
            ClearLogArea();
            var prevCursorLeft = Console.CursorLeft;
            var prevCursorTop = Console.CursorTop;
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.Write($"{DateTime.Now:G} - {message}");
            Console.SetCursorPosition(prevCursorLeft, prevCursorTop);
        }

        public void ClearLogArea()
        {
            for (var i = 4; i > 0; i--)
            {
                ClearConsoleLine(Console.WindowHeight - i);
            }
        }
    }
}
