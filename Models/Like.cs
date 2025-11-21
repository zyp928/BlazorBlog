namespace BlazorBlog.Models;

public class Like
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string TargetType { get; set; } = string.Empty; // post, comment
    public Guid TargetId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    // 导航属性
    public User User { get; set; } = null!;
}
