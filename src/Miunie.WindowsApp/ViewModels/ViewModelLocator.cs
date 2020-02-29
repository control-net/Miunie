using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Extensions.DependencyInjection;
using Miunie.Core;
using Miunie.Core.Logging;
using Miunie.WindowsApp.Utilities;

namespace Miunie.WindowsApp.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
            }

            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<StartPageViewModel>();
            SimpleIoc.Default.Register<StatusPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
            SimpleIoc.Default.Register<ServersPageViewModel>();
            SimpleIoc.Default.Register<ImpersonationChatPageViewModel>();
            SimpleIoc.Default.Register(() => ActivatorUtilities.CreateInstance<MiunieBot>(InversionOfControl.Provider));
            SimpleIoc.Default.Register(() => InversionOfControl.Provider.GetRequiredService<ILogReader>());
            SimpleIoc.Default.Register<TokenValidator>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
        }

        public StartPageViewModel StartPageInstance 
            => ServiceLocator.Current.GetInstance<StartPageViewModel>();

        public StatusPageViewModel StatusPageInstance
            => ServiceLocator.Current.GetInstance<StatusPageViewModel>();

        public SettingsPageViewModel SettingsPageInstance
            => ServiceLocator.Current.GetInstance<SettingsPageViewModel>();

        public ServersPageViewModel ServersPageInstance
            => ServiceLocator.Current.GetInstance<ServersPageViewModel>();

        public ImpersonationChatPageViewModel ImpersonationChatPageInstance
            => ServiceLocator.Current.GetInstance<ImpersonationChatPageViewModel>();
    }
}
