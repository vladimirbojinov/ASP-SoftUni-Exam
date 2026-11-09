using CookBookApp.Data.Models;

namespace CookBookApp.Data.Repository.Contracts;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe?> FindByIdAsync(int id); 
}