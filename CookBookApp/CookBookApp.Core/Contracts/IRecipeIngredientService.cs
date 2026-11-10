using CookBookApp.DTOs.Ingredient;

namespace CookBookApp.Core.Contracts;

public interface IRecipeIngredientService
{
    Task<IngredientInputViewModel[]> GetRecipeIngredientAsync(int recipeId);

    Task AddRecipeIngredientRowAsync(int recipeId, IngredientAddRow newIngredient);

    Task RemoveRecipeIngredientRowAsync(int recipeId, int ingredientId);
}