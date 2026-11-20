using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Category;
using CookBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Data.Repository;

public class CategoryRepository(CookBookDbContext dbContext)
    : BaseRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await DbSet.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await DbSet.AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }

    public async Task<CategoryDropDown[]> GetDropDownAsync()
    {
        return await DbSet
            .Select(c => new CategoryDropDown()
            {
                Id = c.Id,
                Name = c.Name,
            })
            .OrderBy(c => c.Name)
            .ToArrayAsync();
    }

    public async Task<Category?> FindByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<Category?> GetFullCategoryAsync(int id)
    {
        return await DbSet
            .Include(c => c.Recipes)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
