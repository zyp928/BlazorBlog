using BlazorBlog.Models;

namespace BlazorBlog.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<Category?> GetCategoryBySlugAsync(string slug);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category?> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Guid id);
    Task<int> GetTotalCategoriesCountAsync();
}
