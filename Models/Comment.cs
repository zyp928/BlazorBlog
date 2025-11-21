namespace BlazorBlog.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public Guid? ParentId { get; set; } // 支持评论回复
    
    public string Content { get; set; } = string.Empty;
    
    // 评论状态
    public string Status { get; set; } = "published"; // published, pending, rejected
    public bool IsPinned { get; set; } // 置顶评论
    
    // 互动统计
    public int LikesCount { get; set; }
    
    // 审计字段
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    // 导航属性
    public Post Post { get; set; } = null!;
    public User User { get; set; } = null!;
    public Comment? ParentComment { get; set; }
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
