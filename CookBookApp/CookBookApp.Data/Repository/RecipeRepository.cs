using CookBookApp.Data.Models;
using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;

namespace CookBookApp.Data.Repository;

public class RecipeRepository(CookBookDbContext dbContext)
    : BaseRepository<Recipe>(dbContext), IRecipeRepository
{
    public async Task<Recipe?> FindByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }
}
