using CookBookApp.Data.Models;
using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;

namespace CookBookApp.Data.Repository;

public class CategoryRepository(CookBookDbContext dbContext)
    : BaseRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<Category?> FindByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }
}
