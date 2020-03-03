using System;

namespace Miunie.Core.Entities.Views
{
    public class MessageView
    {
        public string AuthorName { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string Content { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
