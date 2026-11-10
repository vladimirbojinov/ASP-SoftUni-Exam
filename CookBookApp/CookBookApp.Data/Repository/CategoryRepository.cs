using CookBookApp.Models;
using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Data.Repository;

public class CategoryRepository(CookBookDbContext dbContext)
    : BaseRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<bool> ExistsAsync(int id)
    {
        return await DbSet.AnyAsync(c => c.Id == id);
    }

    public async Task<Category?> FindByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }
}
