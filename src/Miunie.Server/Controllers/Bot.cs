using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Miunie.Core;
using Miunie.Server.Models;

namespace Miunie.Server.Controllers
{
    public class Bot : Controller
    {
        private const string DefaultBotAvatar = @"img/miunie-scarf-transparent.png";

        private readonly MiunieBot _miunie;

        public Bot(BotEvents events, MiunieBot miunie)
        {
            // BotEvents just need to be initiated here.
            // That's why it's injected.

            _miunie = miunie;
        }

        public IActionResult Index()
        {
            var token = _miunie.BotConfiguration.DiscordToken;

            var model = new BotStatusViewModel
            {
                TokenHintStart = new string(token?.Take(3).ToArray()),
                TokenHintEnd = new string(token?.TakeLast(3).ToArray()),
                Status = (BotConnectionStatus)_miunie.MiunieDiscord.ConnectionState,
                BotAvatarUrl = _miunie.MiunieDiscord.GetBotAvatarUrl() ?? DefaultBotAvatar
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateToken([FromForm] string botToken)
        {
            _miunie.BotConfiguration.DiscordToken = botToken;
            return RedirectToAction("Index");
        }

        public IActionResult Start()
        {
            if (_miunie.MiunieDiscord.ConnectionState == ConnectionState.DISCONNECTED)
            {
                _ = _miunie.StartAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
