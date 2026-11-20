using CookBookApp.Core.Contracts;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Ingredient;
using CookBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    {
        return await ingredientRepository.FindByIdAsync(id);
    public async Task<IngredientIndexModel[]> GetIngredientForIndexAsync()
    {
        return await ingredientRepository
            .GetAllAsNoTracking()
            .OrderBy(i => i.Name)
            .Select(i => new IngredientIndexModel()
            {
                Id = i.Id,
                Name = i.Name,
                RecipesCount = i.RecipesIngredients.Count
            })
            .ToArrayAsync();
    }

    public async Task<IngredientDropDown[]> GetDropDownAsync()
    {
        return await ingredientRepository.GetDropDownAsync();
    }
}
