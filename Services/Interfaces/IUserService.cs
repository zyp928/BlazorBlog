using BlazorBlog.Models;

namespace BlazorBlog.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> ToggleUserStatusAsync(Guid id);
    Task<int> GetTotalUsersCountAsync();
}
