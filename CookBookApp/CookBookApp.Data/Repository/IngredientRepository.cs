using CookBookApp.Data.Models;
using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;

namespace CookBookApp.Data.Repository;

public class IngredientRepository(CookBookDbContext dbContext)
    : BaseRepository<Ingredient>(dbContext), IIngredientRepository
{
    public async Task<Ingredient?> FindByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }
}
