using CookBookApp.Core.Contracts;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Ingredient;
using CookBookApp.Models;

namespace CookBookApp.Core.Services;

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    public async Task<Ingredient?> GetIngredientAsync(int id)
    {
        return await ingredientRepository.FindByIdAsync(id);
    }

    public async Task<IngredientDropDown[]> GetDropDownAsync()
    {
        return await ingredientRepository.GetDropDownAsync();
    }
}
