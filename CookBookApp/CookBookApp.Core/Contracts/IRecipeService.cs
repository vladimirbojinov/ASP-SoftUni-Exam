using CookBookApp.Data.Models;
using CookBookApp.DTOs.Recipe;

namespace CookBookApp.Core.Contracts;

public interface IRecipeService
{
    Task<RecipeIndexModel[]> GetRecipeIndexAsync();

    Task<RecipeDetailsModel?> GetRecipeDetailsAsync(int id);

    Task<RecipeInputModel> GetRecipeForCreationAsync();

    Task<RecipeInputModel?> GetRecipeForEditAsync(int id);

    Task<RecipeDeleteModel?> GetRecipeForDeleteAsync(int id);

    Task<RecipeIndexModel[]> SearchAsync(string? nameFilter, int? categoryId);

    Task DeleteRecipe(int id);

    Task<Recipe?> GetRecipeAsync(int id);

    Task SaveRecipeAsync(RecipeInputModel model);

    Task<bool> EditRecipeAsync(RecipeInputModel model);
}
