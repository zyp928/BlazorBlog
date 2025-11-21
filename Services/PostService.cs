using BlazorBlog.Data;
using BlazorBlog.Models;
using BlazorBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Services;

public class PostService : IPostService
{
    private readonly BlogDbContext _context;

    public PostService(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.DeletedAt == null)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Post>> GetPublishedPostsAsync()
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.Status == "published" && p.DeletedAt == null)
            .OrderByDescending(p => p.PublishedAt)
            .ToListAsync();
    }

    public async Task<Post?> GetPostByIdAsync(Guid id)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);
    }

    public async Task<Post?> GetPostBySlugAsync(string slug)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.DeletedAt == null);
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        post.Id = Guid.NewGuid();
        post.CreatedAt = DateTime.UtcNow;
        post.UpdatedAt = DateTime.UtcNow;

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return post;
    }

    public async Task<Post?> UpdatePostAsync(Post post)
    {
        var existingPost = await _context.Posts.FindAsync(post.Id);
        if (existingPost == null || existingPost.DeletedAt != null)
            return null;

        existingPost.Title = post.Title;
        existingPost.Slug = post.Slug;
        existingPost.Content = post.Content;
        existingPost.Summary = post.Summary;
        existingPost.CoverImageUrl = post.CoverImageUrl;
        existingPost.Status = post.Status;
        existingPost.UpdatedAt = DateTime.UtcNow;

        if (post.Status == "published" && existingPost.PublishedAt == null)
        {
            existingPost.PublishedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return existingPost;
    }

    public async Task<bool> DeletePostAsync(Guid id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null || post.DeletedAt != null)
            return false;

        // Soft delete
        post.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Post>> GetPostsByCategoryAsync(Guid categoryId)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.Categories.Any(c => c.Id == categoryId) && p.DeletedAt == null)
            .OrderByDescending(p => p.PublishedAt)
            .ToListAsync();
    }

    public async Task<List<Post>> GetPostsByTagAsync(Guid tagId)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.Tags.Any(t => t.Id == tagId) && p.DeletedAt == null)
            .OrderByDescending(p => p.PublishedAt)
            .ToListAsync();
    }

    public async Task<List<Post>> GetPostsByUserAsync(Guid userId)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.UserId == userId && p.DeletedAt == null)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetTotalPostsCountAsync()
    {
        return await _context.Posts
            .Where(p => p.DeletedAt == null)
            .CountAsync();
    }

    public async Task<int> GetPublishedPostsCountAsync()
    {
        return await _context.Posts
            .Where(p => p.Status == "published" && p.DeletedAt == null)
            .CountAsync();
    }
}
