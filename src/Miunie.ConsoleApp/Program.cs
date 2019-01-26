using System;
using System.Threading.Tasks;
using Miunie.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Miunie.ConsoleApp
{
    class Program
    {
        private static async Task Main(string[] args)
             => await ActivatorUtilities
                 .CreateInstance<MiunieBot>(InversionOfControl.Provider)
                 .RunAsync();
    }
}

