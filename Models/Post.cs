namespace BlazorBlog.Models;

public class Post
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    // 文章内容
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    
    // 文章元数据
    public string Status { get; set; } = "draft"; // draft, published, archived
    public string Visibility { get; set; } = "public"; // public, private, unlisted
    public bool IsFeatured { get; set; }
    public bool IsOriginal { get; set; } = true;
    public string? SourceUrl { get; set; }
    
    // SEO 优化
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    
    // 统计字段
    public int ViewsCount { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int FavoritesCount { get; set; }
    public int SharesCount { get; set; }
    
    // 审计字段
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    // 导航属性
    public User User { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
