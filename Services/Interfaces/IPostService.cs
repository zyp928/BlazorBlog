using BlazorBlog.Models;

namespace BlazorBlog.Services.Interfaces;

public interface IPostService
{
    Task<List<Post>> GetAllPostsAsync();
    Task<List<Post>> GetPublishedPostsAsync();
    Task<Post?> GetPostByIdAsync(Guid id);
    Task<Post?> GetPostBySlugAsync(string slug);
    Task<Post> CreatePostAsync(Post post);
    Task<Post?> UpdatePostAsync(Post post);
    Task<bool> DeletePostAsync(Guid id);
    Task<List<Post>> GetPostsByCategoryAsync(Guid categoryId);
    Task<List<Post>> GetPostsByTagAsync(Guid tagId);
    Task<List<Post>> GetPostsByUserAsync(Guid userId);
    Task<int> GetTotalPostsCountAsync();
    Task<int> GetPublishedPostsCountAsync();
}
