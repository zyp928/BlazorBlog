using BlazorBlog.Data;
using BlazorBlog.Models;
using BlazorBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Services;

public class TagService : ITagService
{
    private readonly BlogDbContext _context;

    public TagService(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags
            .Include(t => t.Posts)
            .OrderByDescending(t => t.PostsCount)
            .ToListAsync();
    }

    public async Task<Tag?> GetTagByIdAsync(Guid id)
    {
        return await _context.Tags
            .Include(t => t.Posts)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tag?> GetTagBySlugAsync(string slug)
    {
        return await _context.Tags
            .Include(t => t.Posts)
            .FirstOrDefaultAsync(t => t.Slug == slug);
    }

    public async Task<Tag> CreateTagAsync(Tag tag)
    {
        tag.Id = Guid.NewGuid();
        tag.CreatedAt = DateTime.UtcNow;
        tag.UpdatedAt = DateTime.UtcNow;

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return tag;
    }

    public async Task<Tag?> UpdateTagAsync(Tag tag)
    {
        var existingTag = await _context.Tags.FindAsync(tag.Id);
        if (existingTag == null)
            return null;

        existingTag.Name = tag.Name;
        existingTag.Slug = tag.Slug;
        existingTag.Color = tag.Color;
        existingTag.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingTag;
    }

    public async Task<bool> DeleteTagAsync(Guid id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            return false;

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<int> GetTotalTagsCountAsync()
    {
        return await _context.Tags
            .CountAsync();
    }
}
