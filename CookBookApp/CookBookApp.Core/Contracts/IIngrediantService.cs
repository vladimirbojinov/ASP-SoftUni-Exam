using CookBookApp.Data.Models;
using CookBookApp.DTOs.Ingredient;

namespace CookBookApp.Core.Contracts;

public interface IIngredientService
{
    Task<IngredientDropDown[]> GetIngredientDropDownAsync();

    Task<Ingredient?> GetIngredientAsync(int id);
}
