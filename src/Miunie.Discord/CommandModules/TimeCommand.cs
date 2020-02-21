using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Discord.Convertors;
using System.Threading.Tasks;

namespace Miunie.Discord.CommandModules
{
    public class TimeCommand : ModuleBase<SocketCommandContext>
    {
        private readonly TimeService _service;
        private readonly EntityConvertor _entityConvertor;

        public TimeCommand(TimeService service, EntityConvertor entityConvertor)
        {
            _service = service;
            _entityConvertor = entityConvertor;
        }

        [Command("time for")]
        public async Task ShowProfileAsync(SocketGuildUser user)
        {
            var m = _entityConvertor.ConvertUser(user);
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputCurrentTimeForUserAsync(m, channel);
        }
    }
}
