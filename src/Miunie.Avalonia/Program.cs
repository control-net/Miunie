using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;

namespace Miunie.Avalonia
{
    class Program
    {
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug()
                .UseReactiveUI();
        }
    }
}
