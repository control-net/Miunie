using System;
using System.Threading.Tasks;
using Miunie.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Miunie.ConsoleApp
{
    internal static class Program
    {
        private static async Task Main()
        {
            PrintHeader();
            await ActivatorUtilities
                .CreateInstance<MiunieBot>(InversionOfControl.Provider)
                .RunAsync();
        }

        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(ConsoleStrings.MIUNIE_ASCII_HEADER);
            Console.ResetColor();
        }
    }
}
