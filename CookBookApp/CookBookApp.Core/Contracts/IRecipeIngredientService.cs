using CookBookApp.DTOs.Ingredient;

namespace CookBookApp.Core.Contracts;

public interface IRecipeIngredientService
{
    Task AddRecipeIngredientRowAsync(int recipeId, IngredientAddRow newIngredient);

    Task RemoveRecipeIngredientRowAsync(int recipeId, int ingredientId);
}