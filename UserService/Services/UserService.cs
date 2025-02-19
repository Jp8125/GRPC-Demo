using Grpc.Core;
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
        public override Task<UserListResponse> GetAllUsers(EmptyRequest request, ServerCallContext context)
        {
            return base.GetAllUsers(request, context);
        }

    }
}
