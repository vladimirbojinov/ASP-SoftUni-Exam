using CookBookApp.Core.Contracts;
using CookBookApp.Data;
using CookBookApp.Data.Models;
using CookBookApp.DTOs.Ingredient;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class IngredientService(CookBookDbContext dbContext) : IIngredientService
{
    public async Task<Ingredient?> GetIngredientAsync(int id)
    {
        return await dbContext.Ingredients.FindAsync(id);
    }

    public async Task<IngredientDropDown[]> GetIngredientDropDownAsync()
    {
        return await dbContext.Ingredients
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .Select(i => new IngredientDropDown()
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToArrayAsync();
    }
}
