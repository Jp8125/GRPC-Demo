using ChatService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ChatDbContext _context;

        public ChatController(ChatDbContext context)
        {
            _context = context;
        }

        // GET api/<ChatController>/5
        [HttpGet()]
        public IActionResult Get(int senderId,int ReceiverId )
        {
            var chats = _context.Conversations;
            var messages = _context.Messages;
            var res = from c in chats
                      join m in messages on c.ConvoId equals m.ConvoId
                      where (c.SenderId == senderId && c.ReceiverId == ReceiverId || c.ReceiverId == senderId && c.SenderId == ReceiverId)
                      select new
                      {
                          m.MessageId,
                          c.SenderId,
                          c.ReceiverId,
                          m.Content
                      };
            return Ok(res);
        }
    }
}
