using System;
using System.Collections.Generic;
using System.Text;

namespace Miunie.Core.Logging
{
    public interface ILogReader
    {
        event EventHandler LogRecieved;
        IEnumerable<string> RetrieveLogs(int ammount = 5);
    }
}
