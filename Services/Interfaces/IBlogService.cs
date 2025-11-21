using BlazorBlog.Models;

namespace BlazorBlog.Services.Interfaces;

public interface IBlogService
{
    // Backend operations - real database posts
    Task<List<Post>> GetPostsAsync();
    Task<Post?> GetPostByIdAsync(Guid id);
    
    // Frontend display - BlogPost model
    Task<List<BlogPost>> GetBlogPostsAsync();
}
