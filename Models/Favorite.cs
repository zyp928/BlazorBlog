namespace BlazorBlog.Models;

public class Favorite
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    // 导航属性
    public User User { get; set; } = null!;
    public Post Post { get; set; } = null!;
}
