using System;
using ReactiveUI;

namespace Miunie.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _text = "0";

        public string Text 
        { 
            get { return _text; }
            set { this.RaiseAndSetIfChanged(ref _text, value); }
        }

        private int num = 1;
        public void OnClickCommand()
        {
            Text = $"{num}";
            num++;
        }
    }
}
