using System;
using GalaSoft.MvvmLight;
using Miunie.Core;

namespace Miunie.WindowsApp.ViewModels
{
    public class StatusPageViewModel : ViewModelBase
    {
        private string _connectedStatus;
        public string ConnectionStatus
        {

            get => _connectedStatus;
            set
            {
                if (value == _connectedStatus) return;
                _connectedStatus = value;
                RaisePropertyChanged(nameof(ConnectionStatus));
            }
        }

        private bool _actionButtonEnabled;
        public bool ActionButtonEnabled
        {

            get => _actionButtonEnabled;
            set
            {
                if (value == _actionButtonEnabled) return;
                _actionButtonEnabled = value;
                RaisePropertyChanged(nameof(ActionButtonEnabled));
            }
        }

        public string ActionButtonText => _miunie.IsRunning ? "Stop" : "Start";

        private readonly MiunieBot _miunie;

        public StatusPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            miunie.ConnectionStateChanged += MiunieOnConnectionStateChanged;
            ConnectionStatus = "Not connected";
            _actionButtonEnabled = true;
        }

        public async void ToggleBotStart()
        {
            if (_miunie.IsRunning)
            {
                _miunie.Stop();
            }
            else
            {
                await _miunie.StartAsync();
            }
        }

        private void MiunieOnConnectionStateChanged(object sender, EventArgs e)
        {
            ConnectionStatus = _miunie.IsRunning ? "Connected" : "Not connected";
            RaisePropertyChanged(nameof(ActionButtonText));
            if (!_actionButtonEnabled) { ActionButtonEnabled = true; }
        }
    }
}
