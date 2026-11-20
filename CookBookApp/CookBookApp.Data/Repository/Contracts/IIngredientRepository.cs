using CookBookApp.DTOs.Ingredient;
using CookBookApp.Models;

namespace CookBookApp.Data.Repository.Contracts;

public interface IIngredientRepository : IRepository<Ingredient>
{
    Task<Ingredient?> FindByIdAsync(int id);

    Task<IngredientDropDown[]> GetDropDownAsync();

    Task<bool> ExistsByNameAsync(string name);

    Task<Ingredient?> GetFullIngredientAsync(int id);
}
