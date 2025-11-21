using BlazorBlog.Models;
using BlazorBlog.Services.Interfaces;

namespace BlazorBlog.Services;

public class BlogService : IBlogService
{
    private readonly IPostService _postService;

    public BlogService(IPostService postService)
    {
        _postService = postService;
    }

    // For backend/admin use - real database posts
    public async Task<List<Post>> GetPostsAsync()
    {
        return await _postService.GetPublishedPostsAsync();
    }

    public async Task<Post?> GetPostByIdAsync(Guid id)
    {
        return await _postService.GetPostByIdAsync(id);
    }

    // For frontend display - BlogPost model (temporary mock data until we have real posts)
    public Task<List<BlogPost>> GetBlogPostsAsync()
    {
        var mockPosts = new List<BlogPost>
        {
            new BlogPost
            {
                Id = 1,
                Title = "欢迎来到IT技术分享社区",
                Content = "这是一个全新的技术博客平台，我们致力于为开发者提供高质量的技术文章和学习资源。",
                PublishDate = DateTime.Now.AddDays(-1),
                Author = "系统管理员",
                AuthorAvatar = "https://api.dicebear.com/7.x/avataaars/svg?seed=Admin",
                ImageUrl = "https://picsum.photos/seed/welcome/400/250",
                Tags = new() { "公告", "欢迎" },
                Views = 1000,
                Likes = 50
            }
        };
        return Task.FromResult(mockPosts);
    }
}
