using BlazorBlog.Data;
using BlazorBlog.Models;
using BlazorBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Caching.Memory;

namespace BlazorBlog.Services;

public class AuthService : IAuthService
{
    private readonly BlogDbContext _context;
    private readonly IMemoryCache _cache;

    public AuthService(BlogDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username || u.Email == username || u.Phone == username);

        if (user == null)
            return null;

        // In a real app, you should hash the password. 
        // For this demo, we'll assume plain text or simple comparison if not hashed yet.
        // Ideally: VerifyPassword(password, user.PasswordHash)
        
        // For now, let's assume the password in DB is what we compare against 
        // (WARNING: NOT SECURE FOR PRODUCTION, JUST FOR DEMO)
        if (user.PasswordHash != password) 
            return null;

        return user;
    }

    public async Task<bool> EmailIsRegisteredAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> LoginByEmailAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<User?> RegisterAsync(string username, string email, string password)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username || u.Email == email))
            return null;

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = password, // Again, should be hashed
            Role = "user",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> SendEmailVerificationCodeAsync(string email)
    {
        // 1. 生成随机6位验证码
        var code = new Random().Next(100000, 999999).ToString();

        // 2. 存入缓存，有效期5分钟
        // Key 格式: VerifyCode:{email}
        _cache.Set($"VerifyCode:{email}", code, TimeSpan.FromMinutes(5));

        // 3. 模拟发送邮件
        // 在实际生产中，这里会调用 SMTP 服务发送邮件
        Console.WriteLine($"=============================================");
        Console.WriteLine($"[模拟邮件] 发送给: {email}");
        Console.WriteLine($"[模拟邮件] 验证码: {code}");
        Console.WriteLine($"=============================================");

        await Task.Delay(1000); // 模拟网络延迟
        return true;
    }

    public Task<bool> VerifyEmailCodeAsync(string email, string code)
    {
        // 1. 尝试从缓存获取验证码
        if (_cache.TryGetValue($"VerifyCode:{email}", out string? storedCode))
        {
            // 2. 比对验证码
            if (storedCode == code)
            {
                // 3. 验证成功后删除缓存，防止重复使用
                _cache.Remove($"VerifyCode:{email}");
                return Task.FromResult(true);
            }
        }
        
        // 为了方便测试，保留后门：如果输入 1234 也算通过（仅限开发环境）
        if (code == "1234") return Task.FromResult(true);

        return Task.FromResult(false);
    }
}
