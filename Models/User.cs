namespace BlazorBlog.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? Nickname { get; set; }
    public string? Bio { get; set; }
    public string? GithubUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    
    // 用户状态
    public bool IsEmailVerified { get; set; }
    public bool IsPhoneVerified { get; set; }
    public bool IsActive { get; set; } = true;
    public string Role { get; set; } = "user"; // user, admin, moderator
    
    // 统计字段
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    public int PostsCount { get; set; }
    
    // 审计字段
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime? DeletedAt { get; set; } // 软删除
    
    // 导航属性
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    
    // 关注关系
    public ICollection<Follow> Followers { get; set; } = new List<Follow>(); // 粉丝
    public ICollection<Follow> Following { get; set; } = new List<Follow>(); // 关注的人
}
