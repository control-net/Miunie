using System;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Miunie.Discord.CommandModules;
using Miunie.Discord.Convertors;

namespace Miunie.Discord
{
    public class CommandServiceFactory
    {
        private readonly IServiceProvider _services;
        private readonly EntityConvertor _entityConvertor;

        public CommandServiceFactory(IServiceProvider services, EntityConvertor entityConvertor)
        {
            _services = services;
            _entityConvertor = entityConvertor;
        }

        public CommandsNextExtension Create(DiscordClient client)
        {
            var commandService = client.UseCommandsNext(new CommandsNextConfiguration
            {
                EnableMentionPrefix = true,
                Services = _services
            });

            commandService.RegisterCommands<ProfileCommand>();
            commandService.RegisterCommands<RemoteRepositoryCommand>();
            commandService.RegisterCommands<CurrencyCommand>();
            commandService.RegisterConverter(_entityConvertor.ChannelConvertor);
            commandService.RegisterConverter(_entityConvertor.UserConvertor);

            return commandService;
        }
    }
}
