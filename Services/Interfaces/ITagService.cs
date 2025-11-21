using BlazorBlog.Models;

namespace BlazorBlog.Services.Interfaces;

public interface ITagService
{
    Task<List<Tag>> GetAllTagsAsync();
    Task<Tag?> GetTagByIdAsync(Guid id);
    Task<Tag?> GetTagBySlugAsync(string slug);
    Task<Tag> CreateTagAsync(Tag tag);
    Task<Tag?> UpdateTagAsync(Tag tag);
    Task<bool> DeleteTagAsync(Guid id);
    Task<int> GetTotalTagsCountAsync();
}
