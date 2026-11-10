using CookBookApp.Core.Contracts;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Ingredient;
using CookBookApp.Models;
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
        Recipe recipe = await recipeRepository.GetByIdAsync(recipeId)
            ?? throw new ArgumentException("Recipe not found!");

        Ingredient ingredient = await ingredientRepository.FindByIdAsync(newIngredient.Id)
            ?? throw new ArgumentException("Ingredient not found!");

        bool isIngredientDuplicate = await recipeIngredientRepository.GetAll()
            .AnyAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == newIngredient.Id);

        if (isIngredientDuplicate)
            throw new InvalidOperationException("Ingredient is already added to the recipe!");

        await recipeIngredientRepository.AddAsync(new RecipeIngredient
        {
            RecipeId = recipeId,
            IngredientId = newIngredient.Id,
            Quantity = newIngredient.Quantity,
            MeasurementUnit = newIngredient.MeasurementUnit,
            Note = newIngredient.Note?.Trim()
        });

        await recipeIngredientRepository.SaveChangesAsync();
    }

    public async Task<IngredientInputViewModel[]> GetRecipeIngredientAsync(int recipeId)
    {
        IngredientInputViewModel[] recipeIngredients = await recipeIngredientRepository
            .GetAllAsNoTracking()
            .Where(ri => ri.RecipeId == recipeId)
            .OrderBy(ri => ri.Ingredient.Name)
            .Select(ri => new IngredientInputViewModel
            {
                Id = ri.IngredientId,
                Name = ri.Ingredient.Name,
                Quantity = ri.Quantity,
                MeasurementUnit = ri.MeasurementUnit,
                Note = ri.Note
            })
            .ToArrayAsync();

        return recipeIngredients;
    }

    public async Task RemoveRecipeIngredientRowAsync(int recipeId, int ingredientId)
    {
        Recipe recipe = await recipeRepository.GetByIdAsync(recipeId)
            ?? throw new ArgumentException("Recipe not found!");

        Ingredient ingredient = await ingredientRepository.FindByIdAsync(ingredientId)
            ?? throw new ArgumentException("Ingredient not found!");

        RecipeIngredient? recipeIngredientToRemove = await recipeIngredientRepository
            .GetAllAsNoTracking()
            .Where(ri => ri.Recipe.Id == recipe.Id && ri.Ingredient.Id == ingredient.Id)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("The recipe didn't contain the specified ingredient!");

        recipeIngredientRepository.Remove(recipeIngredientToRemove);
        await recipeIngredientRepository.SaveChangesAsync();
    }
}
