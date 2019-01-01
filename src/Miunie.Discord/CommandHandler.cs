using System.Threading.Tasks;
using DSharpPlus.CommandsNext;

namespace Miunie.Discord
{
    public class CommandHandler
    {
        private CommandsNextModule _commandsNextModule;

        public CommandHandler(CommandsNextModule commandsNextModule)
        {
            _commandsNextModule = commandsNextModule;
        }

        public async Task InitializeAsync()
        {
            /*
             TODO (Charly): Here we should register the commands, converters...
             using the _commandService.RegisterCommands method
             https://dsharpplus.emzi0767.com/api/DSharpPlus.CommandsNext.CommandsNextModule.html#DSharpPlus_CommandsNext_CommandsNextModule_RegisterCommands_Assembly_
            */
        }
    }
}