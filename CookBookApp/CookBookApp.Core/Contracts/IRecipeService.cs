using CookBookApp.Models;
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

    Task<Recipe?> GetFullRecipeAsync(int id);

    Task CreateRecipeAsync(RecipeInputModel model);

    Task EditRecipeAsync(RecipeInputModel model);
}
