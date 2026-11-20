using CookBookApp.DTOs.Ingredient;

namespace CookBookApp.Core.Contracts;

public interface IIngredientService
{
    Task<IngredientIndexModel[]> GetIngredientForIndexAsync();

    Task<IngredientDropDown[]> GetDropDownAsync();
}
