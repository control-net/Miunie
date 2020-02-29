using GalaSoft.MvvmLight;
using Miunie.Core;
using Miunie.Core.Providers;
using Miunie.WindowsApp.Utilities;
using System;

namespace Miunie.WindowsApp.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        public TokenValidator TokenValidator { get; }

        private readonly MiunieBot _miunie;
        private readonly ILanguageProvider _lang;

        public StartPageViewModel(TokenValidator tokenValidator, MiunieBot miunie, ILanguageProvider lang)
        {
            TokenValidator = tokenValidator;
            _miunie = miunie;
            _lang = lang;
        }

        internal void ApplyToken(string token)
        {
            _miunie.BotConfiguration.DiscordToken = token;
        }

        internal string MiunieAboutText => 
            _lang.GetPhrase(PhraseKey.GPL3_NOTICE.ToString(), DateTime.Now, "https://github.com/control-net/Miunie");
    }
}
