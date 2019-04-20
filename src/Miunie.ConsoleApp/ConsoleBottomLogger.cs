using System;
using Miunie.Core.Logging;

namespace Miunie.ConsoleApp
{
    public class ConsoleBottomLogger : ILogWriter
    {
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

        public static void ClearConsoleLine(int top)
        {
            var currentTop = Console.CursorTop;
            var currentLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(currentLeft, currentTop);
        }
    }
}
