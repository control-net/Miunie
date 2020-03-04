using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{    
    public class MiscCommands : ModuleBase<SocketCommandContext>
    {
        private readonly MiscService _service;
        private readonly EntityConvertor _entityConvertor;

        public MiscCommands(MiscService service, EntityConvertor entityConvertor)
        {
            _service = service;
            _entityConvertor = entityConvertor;
        }

        [Command("what do you think?")]
        public async Task SendRandomYesNoMaybeAnswer()
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.SendRandomYesNoAnswerAsync(c);
        }
    }
}
