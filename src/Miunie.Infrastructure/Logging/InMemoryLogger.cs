// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Miunie.Core.Logging;
using System;
using System.Collections.Generic;

namespace Miunie.Infrastructure.Logging
{
    public class InMemoryLogger : ILogWriter, ILogReader
    {
        public event EventHandler LogRecieved;

        public List<string> Logs { get; private set; } = new List<string>();

        public void Log(string message)
        {
            Logs.Add(message);
            LogRecieved?.Invoke(this, EventArgs.Empty);
        }

        public void LogError(string message) => Log($"ERROR: {message}");

        public IEnumerable<string> RetrieveLogs(int ammount = 5)
        {
            if (Logs.Count < ammount)
            {
                return Logs;
            }

            return Logs.GetRange(Logs.Count - ammount, ammount);
        }
    }
}
