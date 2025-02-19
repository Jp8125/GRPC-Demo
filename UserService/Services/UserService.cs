using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using UserService.Models;
using UserService.Protos;

namespace UserService.Services
{
    public class UserService:UserGrpcServices.UserGrpcServicesBase
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }
        public override async Task<UserResponse> GetUser(UserRequest request, ServerCallContext context)
        {
            var user=await _context.Users.FindAsync(request.UserId);
            if (user != null)
            {
                var userRes = new UserResponse()
                {
                    Email = user.Email,
                    Id = user.UserId,
                    Name = user.Username
                };
                return userRes;
            }   
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }
           
        }
        public override async Task<UserListResponse> GetAllUsers(EmptyRequest request, ServerCallContext context)
        {
            var users =await _context.Users.ToListAsync();
            var res = new UserListResponse();
            res.Users.AddRange(users.Select(u => new UserResponse
            {
                Id = u.UserId,
                Name = u.Username,
                Email = u.Email
            }));
            return res;
        }
        public override async Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
        {
            var user = new User() { Email = request.Email, Username = request.Name };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            var res = new AddUserResponse() { Id = user.UserId, Name = user.Username };
            return res;
        }

    }
}
