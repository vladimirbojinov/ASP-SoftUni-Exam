using CookBookApp.Core.Contracts;
using CookBookApp.Data;
using CookBookApp.Data.Models;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Ingredient;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    public async Task<Ingredient?> GetIngredientAsync(int id)
    {
        return await ingredientRepository.FindByIdAsync(id);
    }

    public async Task<IngredientDropDown[]> GetIngredientDropDownAsync()
    {
        return await ingredientRepository
            .GetAllAsNoTracking()
            .OrderBy(i => i.Name)
            .Select(i => new IngredientDropDown()
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToArrayAsync();
    }
}
