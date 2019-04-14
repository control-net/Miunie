using System;
using Miunie.Core.Logging;

namespace Miunie.WindowsApp
{
    public class UwpLogger : ILogger
    {
        public event EventHandler LogReceived;

        public void Log(string message)
        {
            LogReceived?.Invoke(this, new LogEventArgs
            {
                Message = message
            });
        }

        public void LogError(string message)
        {
            Log(message);
        }
    }

    public class LogEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
