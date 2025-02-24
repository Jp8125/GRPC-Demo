using System;
using System.Collections.Generic;

namespace ChatService.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int? ConvoId { get; set; }

    public string? Content { get; set; }

    public virtual Conversation? Convo { get; set; }
}
