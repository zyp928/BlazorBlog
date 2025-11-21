using BlazorBlog.Data;
using BlazorBlog.Models;
using BlazorBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Services;

public class CommentService : ICommentService
{
    private readonly BlogDbContext _context;

    public CommentService(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Post)
            .Include(c => c.ParentComment)
            .Where(c => c.DeletedAt == null)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsByPostAsync(Guid postId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Replies)
            .Where(c => c.PostId == postId && c.DeletedAt == null)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(Guid id)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Post)
            .Include(c => c.ParentComment)
            .Include(c => c.Replies)
            .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        comment.Id = Guid.NewGuid();
        comment.CreatedAt = DateTime.UtcNow;
        comment.UpdatedAt = DateTime.UtcNow;
        comment.Status = "pending"; // Default to pending for moderation

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> UpdateCommentAsync(Comment comment)
    {
        var existingComment = await _context.Comments.FindAsync(comment.Id);
        if (existingComment == null || existingComment.DeletedAt != null)
            return null;

        existingComment.Content = comment.Content;
        existingComment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingComment;
    }

    public async Task<bool> DeleteCommentAsync(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null || comment.DeletedAt != null)
            return false;

        comment.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ApproveCommentAsync(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null || comment.DeletedAt != null)
            return false;

        comment.Status = "approved";
        comment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RejectCommentAsync(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null || comment.DeletedAt != null)
            return false;

        comment.Status = "rejected";
        comment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<int> GetTotalCommentsCountAsync()
    {
        return await _context.Comments
            .Where(c => c.DeletedAt == null)
            .CountAsync();
    }

    public async Task<int> GetPendingCommentsCountAsync()
    {
        return await _context.Comments
            .Where(c => c.Status == "pending" && c.DeletedAt == null)
            .CountAsync();
    }
}
