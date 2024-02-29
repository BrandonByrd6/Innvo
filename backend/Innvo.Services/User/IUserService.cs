
using Innvo.Models.User;

namespace Innvo.Services.User;

public interface IUserService
{
    Task<bool> RegisterUserAsync(UserRegister model);
    Task<UserDetail?> GetUserByIdAsync(int userId);
    Task<bool> DeleteUserAsync(int userId);
}