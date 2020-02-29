using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Miunie.Core
{
    public class SocketTextChannelCastException : Exception
    {
        public SocketTextChannelCastException([CallerMemberName] string caller = "") : base($"Invalid SocketTextChannel cast. Attempted by: {caller}")
        {

        }
    }
}
