using CookBookApp.Core.Contracts;
using CookBookApp.Data.Models;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Ingredient;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class RecipeIngredientService(
        IRecipeIngredientRepository recipeIngredientRepository,
        IIngredientRepository ingredientRepository,
        IRecipeRepository recipeRepository
    ) : IRecipeIngredientService
{
    public async Task AddRecipeIngredientRowAsync(int recipeId, IngredientAddRow newIngredient)
    {
        Recipe? recipe = await recipeRepository.FindByIdAsync(recipeId);
        if (recipe is null) return;

        bool isIngredientDuplicate = await recipeIngredientRepository.GetAll()
            .AnyAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == newIngredient.Id);

        if (isIngredientDuplicate) return;

        await recipeIngredientRepository.AddAsync(new RecipeIngredient
        {
            RecipeId = recipeId,
            IngredientId = newIngredient.Id,
            Quantity = newIngredient.Quantity,
            MeasurementUnit = newIngredient.MeasurementUnit,
            Note = null ?? newIngredient.Note?.Trim()
        });

        await recipeRepository.SaveChangesAsync();
    }

    public async Task RemoveRecipeIngredientRowAsync(int recipeId, int ingredientId)
    {
        Recipe? recipe = await recipeRepository.FindByIdAsync(recipeId);
        if (recipe is null) return;

        Ingredient? ingredient = await ingredientRepository.FindByIdAsync(ingredientId);
        if (ingredient is null) return;

        RecipeIngredient recipeIngredientToRemove = await recipeIngredientRepository.GetAllAsNoTracking()
            .Where(ri => ri.Recipe.Id == recipe.Id && ri.Ingredient.Id == ingredient.Id)
            .FirstAsync();

        recipeIngredientRepository.Remove(recipeIngredientToRemove);
        await recipeIngredientRepository.SaveChangesAsync();
    }
}
