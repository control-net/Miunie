using System;
using Miunie.Core.Logging;

namespace Miunie.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            if (message.StartsWith("ERROR"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            
            Console.WriteLine($"{DateTime.Now:G} - {message}");
            
            Console.ResetColor();
        }
    }
}