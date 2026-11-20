using CookBookApp.Core.Contracts;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Ingredient;
using CookBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    public async Task CreateIngredientAsync(IngredientCreateModel model)
    {
        if (await ingredientRepository.ExistsByNameAsync(model.Name.Trim()))
            throw new InvalidOperationException("Ingredient already exists!");

        await ingredientRepository.AddAsync(new Ingredient()
        {
            Name = model.Name.Trim()
        });

        await ingredientRepository.SaveChangesAsync();
    }

    public async Task DeleteIngredientAsync(int id)
    {
        Ingredient ingredient = await ingredientRepository.FindByIdAsync(id)
            ?? throw new ArgumentException("Ingredient not found!");

        ingredientRepository.Remove(ingredient);
        await ingredientRepository.SaveChangesAsync();
    }

    public async Task EditIngredientAsync(IngredientEditModel model)
    {
        Ingredient ingredient = await ingredientRepository.FindByIdAsync(model.Id)
            ?? throw new ArgumentException("Ingredient not found!");

        if (await ingredientRepository.ExistsByNameAsync(model.Name.Trim()))
            throw new InvalidOperationException("Ingredient already exists!");

        ingredient.Name = model.Name.Trim();
        await ingredientRepository.SaveChangesAsync();
    }

    public async Task<IngredientCreateModel> GetIngredientForCreateAsync()
    {
        return new IngredientCreateModel();
    }

    public async Task<IngredientDeleteModel> GetIngredientForDeleteAsync(int id)
    {
        Ingredient ingredient = await ingredientRepository.GetFullIngredientAsync(id)
            ?? throw new ArgumentException("Ingredient not found!");

        return new IngredientDeleteModel()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            RecipesCount = ingredient.RecipesIngredients.Count
        };
    }

    public async Task<IngredientEditModel> GetIngredientForEditAsync(int id)
    {
        Ingredient ingredient = await ingredientRepository.FindByIdAsync(id)
            ?? throw new ArgumentException("Ingredient not found!");

        return new IngredientEditModel()
        {
            Id = ingredient.Id,
            Name = ingredient.Name
        };
    }

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
