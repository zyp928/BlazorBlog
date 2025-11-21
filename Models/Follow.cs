namespace BlazorBlog.Models;

public class Follow
{
    public Guid Id { get; set; }
    public Guid FollowerId { get; set; } // 关注者
    public Guid FollowingId { get; set; } // 被关注者
    
    public DateTime CreatedAt { get; set; }
    
    // 导航属性
    public User Follower { get; set; } = null!;
    public User FollowingUser { get; set; } = null!;
}
