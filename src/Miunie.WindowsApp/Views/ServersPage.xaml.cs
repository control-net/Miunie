using Miunie.Core.Views;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Miunie.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ServersPage : Page
    {
        public ServersPage()
        {
            InitializeComponent();
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(e.ClickedItem is GuildView guild)) { return; }

            //ConnectedAnimationService
            //    .GetForCurrentView()
            //    .PrepareToAnimate("ServerExpandAnimation", (UIElement) e.OriginalSource);

            Frame.Navigate(typeof(ImpersonationChatPage), guild.Id);
        }
    }
}
