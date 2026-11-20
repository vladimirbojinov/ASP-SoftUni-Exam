using CookBookApp.DTOs.Ingredient;

namespace CookBookApp.Core.Contracts;

public interface IIngredientService
{
    Task<IngredientIndexModel[]> GetIngredientForIndexAsync();

    Task<IngredientCreateModel> GetIngredientForCreateAsync();

    Task CreateIngredientAsync(IngredientCreateModel model);
    Task<IngredientDropDown[]> GetDropDownAsync();
}
