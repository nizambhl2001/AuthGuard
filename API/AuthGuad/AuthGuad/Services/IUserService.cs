using AuthGuad.Dto;
using AuthGuad.Models;

namespace AuthGuad.Services
{
    public interface IUserService
    {
        Task<User> Register(RegisterDto register);
    }
}
