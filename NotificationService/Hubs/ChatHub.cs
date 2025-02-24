using Microsoft.AspNetCore.SignalR;
using NotificationService.Protos;

namespace NotificationService.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ChatGrpc.ChatGrpcClient _chatgrpc;
        public Dictionary<int, string> connections=new Dictionary<int, string>();
        public ChatHub(ChatGrpc.ChatGrpcClient chatgrpc)
        {
            _chatgrpc = chatgrpc;
        }
        public override Task OnConnectedAsync()
        {
            var id = Convert.ToInt32(Context.GetHttpContext().Request.Query["id"]);
            connections[id] = Context.ConnectionId;
            return Task.CompletedTask; ;
        }
        public async Task SendMessages(int senderId,int RecieverId,string message)
        {
            var chatRequest = new AddChatRequest()
            {
                Content = message,
                SenderId = senderId,
                RecieverId = RecieverId
            };
            await _chatgrpc.AddChatAsync(chatRequest);
            await Clients.Client(connections[RecieverId]).SendAsync("recieve", senderId,RecieverId, message);
        }
    }
}
