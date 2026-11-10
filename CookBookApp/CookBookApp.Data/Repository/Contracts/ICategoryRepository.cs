using CookBookApp.Models;

namespace CookBookApp.Data.Repository.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> ExistsAsync(int id);

    Task<Category?> FindByIdAsync(int id);
}
