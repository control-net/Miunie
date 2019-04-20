using Microsoft.AspNetCore.SignalR;
using Miunie.Core;
using Miunie.Server.Hubs;
using System;

namespace Miunie.Server
{
    public class BotEvents
    {
        private const string DefaultBotAvatar = @"img/miunie-scarf-transparent.png";

        private readonly MiunieBot _miunie;
        private readonly IHubContext<BotStatusHub> _botStatusHubContext;

        public BotEvents(MiunieBot miunie, IHubContext<BotStatusHub> botStatusHubContext)
        {
            _miunie = miunie;
            _botStatusHubContext = botStatusHubContext;

            miunie.MiunieDiscord.ConnectionChanged += MiunieDiscord_ConnectionChanged;
        }

        private async void MiunieDiscord_ConnectionChanged(object sender, EventArgs e)
        {
            await _botStatusHubContext.Clients.All.SendCoreAsync("BotConnectionStateChanged", new object[] {
                _miunie.MiunieDiscord.ConnectionState.ToString(),
                _miunie.MiunieDiscord.GetBotAvatarUrl() ?? DefaultBotAvatar
            });
        }
    }
}
