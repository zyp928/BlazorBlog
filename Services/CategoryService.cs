using BlazorBlog.Data;
using BlazorBlog.Models;
using BlazorBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Services;

public class CategoryService : ICategoryService
{
    private readonly BlogDbContext _context;

    public CategoryService(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.SubCategories)
            .OrderBy(c => c.SortOrder)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.SubCategories)
            .Include(c => c.Posts)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetCategoryBySlugAsync(string slug)
    {
        return await _context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.SubCategories)
            .Include(c => c.Posts)
            .FirstOrDefaultAsync(c => c.Slug == slug);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        category.Id = Guid.NewGuid();
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(category.Id);
        if (existingCategory == null)
            return null;

        existingCategory.Name = category.Name;
        existingCategory.Slug = category.Slug;
        existingCategory.Description = category.Description;
        existingCategory.Icon = category.Icon;
        existingCategory.SortOrder = category.SortOrder;
        existingCategory.ParentId = category.ParentId;
        existingCategory.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<int> GetTotalCategoriesCountAsync()
    {
        return await _context.Categories
            .CountAsync();
    }
}
