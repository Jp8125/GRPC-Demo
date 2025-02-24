using ChatService.Models;
using ChatService.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ChatService.GrpcServices
{
    public class ChatGrpcService:ChatGrpc.ChatGrpcBase
    {
        private readonly ChatDbContext _context;

        public ChatGrpcService(ChatDbContext context)
        {
            _context = context;
        }
        public override async Task<AddChatResponse> AddChat(AddChatRequest request, ServerCallContext context)
        {
            var isadded=_context.Conversations.Any(c=>c.SenderId==request.SenderId&&c.ReceiverId==request.RecieverId);
            var message = new Message();
            if (isadded)
            {
                var chat=await _context.Conversations.FirstOrDefaultAsync(c => c.SenderId == request.SenderId && c.ReceiverId == request.RecieverId);
                //var message= new Message() { ConvoId = chat.ConvoId, Content = request.Content };
                message.ConvoId = chat.ConvoId;
               
               
            }
            else
            {
                var chat = new Conversation() { SenderId = request.SenderId, ReceiverId = request.RecieverId };
                 await _context.Conversations.AddAsync(chat);
                 await _context.SaveChangesAsync();
                message.ConvoId = chat.ConvoId;

            }
            message.Content = request.Content;
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return new AddChatResponse() { ResMessage = "chat saved!" };
        }
    }
}
