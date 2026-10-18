using CookBookApp.Core.Contracts;
using CookBookApp.Data;
using CookBookApp.DTOs.Category;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class CategoryService(CookBookDbContext dbContext) : ICategoryService
{
    public async Task<bool> CheckCategoryExistsAsync(int id)
    {
        return await dbContext.Categories.AnyAsync(c => c.Id == id);
    }

    public async Task<CategoryDropDown[]> GetCategoryDropDownAsync()
    {
        return await dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDropDown()
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToArrayAsync();
    }
}
