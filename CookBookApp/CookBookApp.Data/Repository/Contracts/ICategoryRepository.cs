using CookBookApp.Data.Models;

namespace CookBookApp.Data.Repository.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> FindByIdAsync(int id);
}
