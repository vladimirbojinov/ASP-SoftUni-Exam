using CookBookApp.Models;
using CookBookApp.DTOs.Ingredient;

namespace CookBookApp.Core.Contracts;

public interface IIngredientService
{
    Task<IngredientDropDown[]> GetDropDownAsync();

    Task<Ingredient?> GetIngredientAsync(int id);
}
