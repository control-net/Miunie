using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Miunie.Discord
{
    public class CommandHandler
    {
        private readonly CommandService _commandService;
        private readonly IDiscord _discord;
        private readonly IServiceProvider _services;
        private readonly ILogWriter _logger;

        public CommandHandler(IDiscord discord, IServiceProvider services, ILogWriter logger)
        {
            _discord = discord;
            _commandService = new CommandService();
            _services = services;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            _discord.Client.MessageReceived += HandleCommandAsync;
            await _commandService.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);
        }
        private async Task HandleCommandAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg))
            {
                return;
            }

            var argPos = 0;
            if (msg.HasMentionPrefix(_discord.Client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_discord.Client, msg);
                await TryRunAsBotCommand(context, argPos).ConfigureAwait(false);
            }
        }

        private async Task TryRunAsBotCommand(SocketCommandContext context, int argPos)
        {
            var result = await _commandService.ExecuteAsync(context, argPos, _services);

            if (!result.IsSuccess)
            {
                _logger.Log($"Command execution failed. Reason: {result.ErrorReason}.");
            }
        }
    }
}
