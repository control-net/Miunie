using System.Threading.Tasks;
using DSharpPlus.CommandsNext;

namespace Miunie.Discord
{
    public class CommandHandler
    {
        private CommandsNextModule _commandService;

        public CommandHandler(CommandsNextModule commandService)
        {
            _commandService = commandService;
        }

        public async Task InitializeAsync()
        {
            /*
             TODO (Charly): Here we should register the commands,
             using the _commandService.RegisterCommands method
             https://dsharpplus.emzi0767.com/api/DSharpPlus.CommandsNext.CommandsNextModule.html#DSharpPlus_CommandsNext_CommandsNextModule_RegisterCommands_Assembly_
            */
        }
    }
}