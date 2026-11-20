using CookBookApp.Models;
using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Ingredient;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Data.Repository;

public class IngredientRepository(CookBookDbContext dbContext)
    : BaseRepository<Ingredient>(dbContext), IIngredientRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await DbSet.AnyAsync(i => i.Name.ToLower() == name.ToLower());
    }

    public async Task<Ingredient?> FindByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IngredientDropDown[]> GetDropDownAsync()
    {
        return await DbSet
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .Select(i => new IngredientDropDown()
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToArrayAsync();
    }

    public async Task<Ingredient?> GetFullIngredientAsync(int id)
    {
        return await DbSet
            .Include(i => i.RecipesIngredients)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
