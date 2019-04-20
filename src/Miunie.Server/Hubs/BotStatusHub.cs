using Microsoft.AspNetCore.SignalR;
using Miunie.Core;
using System.Threading.Tasks;

namespace Miunie.Server.Hubs
{
    public class BotStatusHub : Hub
    {
        private readonly MiunieBot _miunie;

        public BotStatusHub(MiunieBot miunie)
        {
            _miunie = miunie;
        }

        public Task ToggleBotConnection()
        {
            if(_miunie.MiunieDiscord.ConnectionState == ConnectionState.DISCONNECTED)
            {
                _ = _miunie.StartAsync();
            }
            else if(_miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTED)
            {
                _miunie.Stop();
            }

            return Task.CompletedTask;
        }
    }
}
