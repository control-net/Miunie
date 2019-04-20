using Miunie.Core.Logging;
using System;
using System.Collections.Generic;

namespace Miunie.Logger
{
    public class InMemoryLogger : ILogWriter, ILogReader
    {
        public event EventHandler LogRecieved;
        public List<string> Logs = new List<string>();

        public void Log(string message)
        {
            Logs.Add(message);
            LogRecieved?.Invoke(this, EventArgs.Empty);
        }

        public void LogError(string message) => Log($"ERROR: {message}");

        public IEnumerable<string> RetrieveLogs(int ammount = 5)
        {
            if(Logs.Count < ammount)
            {
                return Logs;
            }

            return Logs.GetRange(Logs.Count - ammount, ammount);
        }
    }
}
