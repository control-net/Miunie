using System.Collections.Generic;

namespace Miunie.Core
{
    public class TextChannelView
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MessageView> Messages { get; set; }
    }
}
