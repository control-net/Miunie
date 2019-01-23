using System;
using System.Threading.Tasks;
using Miunie.Core;

namespace Miunie.ConsoleApp
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            await InversionOfControl.Container
                    .GetInstance<MiunieBot>()
                    .RunAsync();
        }
    }
}

