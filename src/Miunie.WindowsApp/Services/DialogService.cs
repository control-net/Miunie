using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Miunie.WindowsApp
{
    public class DialogService : IDialogService
    {
        public async Task ShowAboutPage()
        {
            var aboutText = "test";
            var dialog = new MessageDialog(aboutText, "Miunie About");

            dialog.Commands.Add(new UICommand("Close"));

            dialog.CancelCommandIndex = 0;
            await dialog.ShowAsync();
        }
    }
}
