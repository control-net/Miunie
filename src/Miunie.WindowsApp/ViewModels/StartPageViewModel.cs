using GalaSoft.MvvmLight;
using Miunie.Core;
using Miunie.WindowsApp.Utilities;

namespace Miunie.WindowsApp.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        public TokenValidator TokenValidator { get; }

        private readonly MiunieBot _miunie;

        public StartPageViewModel(TokenValidator tokenValidator, MiunieBot miunie)
        {
            TokenValidator = tokenValidator;
            _miunie = miunie;
        }

        internal void ApplyToken(string token)
        {
            _miunie.BotConfiguration.DiscordToken = token;
        }
    }
}
