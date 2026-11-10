using CookBookApp.Models;

namespace CookBookApp.Data.Repository.Contracts;

public interface IIngredientRepository : IRepository<Ingredient>
{
    Task<Ingredient?> FindByIdAsync(int id);
}
