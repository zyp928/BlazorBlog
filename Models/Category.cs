namespace BlazorBlog.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; } // Bootstrap icon class
    public Guid? ParentId { get; set; } // 支持层级分类
    
    // 统计
    public int PostsCount { get; set; }
    public int SortOrder { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // 导航属性
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
