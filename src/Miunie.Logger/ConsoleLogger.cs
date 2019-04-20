using System;
using Miunie.Core.Logging;

namespace Miunie.Logger
{
    public class ConsoleLogger : ILogWriter
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:G} - {message}");
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now:G} - {message}");
            Console.ResetColor();
        }
    }
}
