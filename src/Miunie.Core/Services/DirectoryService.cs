using System.Threading.Tasks;
using Miunie.Core.Providers;

namespace Miunie.Core.Services
{
    public class DirectoryService
    {
        private readonly IListDirectoryProvider _directoryProvider;
        private readonly IDiscordMessages _messages;

        public DirectoryService(IDiscordMessages messages, IListDirectoryProvider directoryProvider)
        {
            _directoryProvider = directoryProvider;
            _messages = messages;
        }

        public async Task ListDirectoryAsync(MiunieChannel c, MiunieUser u)
        {
            var dl = await _directoryProvider.Of(u);
            await _messages.SendMessageAsync(c, dl);
        }
    }
}
