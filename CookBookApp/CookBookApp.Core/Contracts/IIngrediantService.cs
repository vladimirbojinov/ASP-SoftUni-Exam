using CookBookApp.DTOs.Ingredient;

namespace CookBookApp.Core.Contracts;

public interface IIngredientService
{
    Task<IngredientIndexModel[]> GetIngredientForIndexAsync();

    Task<IngredientCreateModel> GetIngredientForCreateAsync();

    Task<IngredientEditModel> GetIngredientForEditAsync(int id);

    Task<IngredientDeleteModel> GetIngredientForDeleteAsync(int id);

    Task CreateIngredientAsync(IngredientCreateModel model);

    Task EditIngredientAsync(IngredientEditModel model);

    Task DeleteIngredientAsync(int id);

    Task<IngredientDropDown[]> GetDropDownAsync();
}
