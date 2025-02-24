using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
using UserService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var res = from u in _context.Users
                      select new UserResponseDto()
                      {
                          UserId = u.UserId,
                          Email = u.Email,
                          Username = u.Username
                      };
            return Ok(res.ToList());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _context.Users.Find(id);
            if (res == null)
            {
                return NotFound(new { message = "user not found" });
            }
            return Ok(res) ;
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
