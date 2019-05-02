namespace Miunie.Server.Models
{
    public class BotStatusViewModel
    {
        public BotConnectionStatus Status { get; set; }
        public string TokenHintStart { get; set; }
        public string TokenHintEnd { get; set; }
        public string BotAvatarUrl { get; set; }
    }
}
