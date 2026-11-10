using CookBookApp.Models;

namespace CookBookApp.Data.Repository.Contracts;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe?> GetByIdAsync(int id); 
}