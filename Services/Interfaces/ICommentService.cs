using BlazorBlog.Models;

namespace BlazorBlog.Services.Interfaces;

public interface ICommentService
{
    Task<List<Comment>> GetAllCommentsAsync();
    Task<List<Comment>> GetCommentsByPostAsync(Guid postId);
    Task<Comment?> GetCommentByIdAsync(Guid id);
    Task<Comment> CreateCommentAsync(Comment comment);
    Task<Comment?> UpdateCommentAsync(Comment comment);
    Task<bool> DeleteCommentAsync(Guid id);
    Task<bool> ApproveCommentAsync(Guid id);
    Task<bool> RejectCommentAsync(Guid id);
    Task<int> GetTotalCommentsCountAsync();
    Task<int> GetPendingCommentsCountAsync();
}
