using System;
using System.Collections.Generic;

namespace ChatService.Models;

public partial class Conversation
{
    public int ConvoId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
