using GalaSoft.MvvmLight;
using Miunie.Core.Entities;
using Miunie.Core.Providers;
using System;

namespace Miunie.WindowsApp.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        private readonly ILanguageProvider _lang;

        public StartPageViewModel(ILanguageProvider lang)
        {
            _lang = lang;
        }

        internal string MiunieAboutText =>
            _lang.GetPhrase(PhraseKey.GPL3_NOTICE.ToString(), DateTime.Now, "https://github.com/control-net/Miunie");
    }
}
