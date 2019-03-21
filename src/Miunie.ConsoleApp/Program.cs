using System.Threading.Tasks;
using Miunie.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Miunie.ConsoleApp
{
    internal static class Program
    {
        private static async Task Main()
             => await ActivatorUtilities
                 .CreateInstance<MiunieBot>(InversionOfControl.Provider)
                 .RunAsync();
    }
}
