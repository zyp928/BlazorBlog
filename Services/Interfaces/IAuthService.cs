using BlazorBlog.Models;

namespace BlazorBlog.Services.Interfaces;

public interface IAuthService
{
    Task<User?> LoginAsync(string username, string password);
    Task<User?> LoginByEmailAsync(string email);
    Task<bool> EmailIsRegisteredAsync(string email);
    Task<User?> RegisterAsync(string username, string email, string password);
    Task<bool> SendEmailVerificationCodeAsync(string email);
    Task<bool> VerifyEmailCodeAsync(string email, string code);
}
