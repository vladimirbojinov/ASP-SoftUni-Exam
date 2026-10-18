using CookBookApp.CookBookApp.DTOs.Ingredient;

using CookBookApp.Data;
using CookBookApp.Data.Models;
using CookBookApp.DTOs.Ingredient;
using CookBookApp.DTOs.Recipe;

using CookBookApp.GCommon.Enums;

using static CookBookApp.GCommon.Constants.ApplicationConstants;

using CookBookApp.Core.Contracts;
namespace CookBookApp.Core.Services;

using Microsoft.EntityFrameworkCore;


public class RecipeService(
    CookBookDbContext dbContext,
    ICategoryService categoryService,
    IIngredientService ingredientService
) : IRecipeService
{
    public async Task<Recipe?> GetRecipeAsync(int id)
    {
        return await dbContext.Recipes
            .Include(r => r.Category)
            .Include(r => r.RecipesIngredients)
            .ThenInclude(r => r.Ingredient)
            .SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task SaveRecipeAsync(RecipeInputModel model)
    {
        if (await categoryService.CheckCategoryExistsAsync(model.CategoryId) == false)
            throw new InvalidOperationException("Category wasn't selected!");

        Recipe recipe = new()
        {
            Name = model.Name,
            Instructions = model.Instructions.Trim(),
            PrepTime = model.PrepTime,
            CookTime = model.CookTime,
            Servings = model.Servings,
            ImageUrl = model.ImageUrl,
            CategoryId = model.CategoryId
        };

        await dbContext.AddAsync(recipe);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> EditRecipeAsync(RecipeInputModel model)
    {
        Recipe? recipe = await GetRecipeAsync(model.Id);
        if (recipe is null) return false;

        recipe.Instructions = string.Join('\n',
            recipe.Instructions
            .Split('\n', StringSplitOptions.TrimEntries)
            .Where(line => line.Length > 0));

        recipe.Name = model.Name.Trim();
        recipe.PrepTime = model.PrepTime;
        recipe.CookTime = model.CookTime;
        recipe.Servings = model.Servings;
        recipe.ImageUrl = model.ImageUrl?.Trim();

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<RecipeIndexModel[]> SearchAsync(string? nameFilter, int? categoryId)
    {
        var recipesQuery = dbContext.Recipes.AsQueryable();

        if (string.IsNullOrEmpty(nameFilter) == false)
        {
            recipesQuery = dbContext.Recipes
                .Where(r => r.Name.Contains(nameFilter));
        }

        if (categoryId != 0)
        {
            recipesQuery = recipesQuery
                .Where(r => r.CategoryId == categoryId);
        }

        var filteredRecipes = await recipesQuery
            .OrderBy(r => r.Name)
            .Select(r => new RecipeIndexModel()
            {
                Id = r.Id,
                Name = r.Name,
                ImageUrl = r.ImageUrl ?? DefaultImageUrl,
                PrepTime = r.PrepTime,
                CookTime = r.CookTime,
                Servings = r.Servings,
                IngredientCount = r.RecipesIngredients.Count(),
                CategoryName = r.Category.Name
            })
            .ToArrayAsync();

        return filteredRecipes;
    }

    public async Task DeleteRecipe(int id)
    {
        dbContext.Recipes.Where(r => r.Id == id).ExecuteDelete();
    }

    public async Task<RecipeDetailsModel?> GetRecipeDetailsAsync(int id)
    {
        Recipe? recipe = await GetRecipeAsync(id);

        if (recipe is null) return null;

        return new RecipeDetailsModel()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            ImageUrl = recipe.ImageUrl ?? DefaultImageUrl,
            CategoryName = recipe.Category.Name,
            Instructions = recipe.Instructions,
            PrepTime = recipe.PrepTime,
            CookTime = recipe.CookTime,
            TotalTime = recipe.PrepTime + recipe.CookTime,
            Servings = recipe.Servings,
            Ingredients = recipe.RecipesIngredients
                .Select(ri => new IngredientDetailsModel()
                {
                    Name = ri.Ingredient.Name.Trim(),
                    Quantity = ri.Quantity.ToString("0.###"),
                    MeasurementUnit = DisplayAbbreviatedUnit(ri.MeasurementUnit),
                    Note = ri.Note
                }).ToArray()
        };
    }

    public async Task<RecipeIndexModel[]> GetRecipeIndexAsync()
    {
        return await dbContext.Recipes
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .Select(r => new RecipeIndexModel()
            {
                Id = r.Id,
                Name = r.Name,
                ImageUrl = r.ImageUrl ?? DefaultImageUrl,
                PrepTime = r.PrepTime,
                CookTime = r.CookTime,
                Servings = r.Servings,
                IngredientCount = r.RecipesIngredients.Count,
                CategoryName = r.Category.Name
            })
            .ToArrayAsync();
    }

    public async Task<RecipeInputModel> GetRecipeForCreationAsync()
    {
        return new RecipeInputModel()
        {
            PrepTime = 1,
            CookTime = 1,
            Servings = 1,
            CategoryDropDown = await categoryService.GetCategoryDropDownAsync()
        };
    }

    public async Task<RecipeInputModel?> GetRecipeForEditAsync(int id)
    {
        Recipe? recipe = await GetRecipeAsync(id);

        if (recipe is null) return null;

        RecipeInputModel model = new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Instructions = recipe.Instructions,
            PrepTime = recipe.PrepTime,
            CookTime = recipe.CookTime,
            Servings = recipe.Servings,
            ImageUrl = recipe.ImageUrl,
            Ingredients = recipe.RecipesIngredients
                .Select(ri => new IngredientInputViewModel()
                {
                    Id = ri.IngredientId,
                    Name = ri.Ingredient.Name.Trim(),
                    Quantity = ri.Quantity,
                    MeasurementUnit = ri.MeasurementUnit,
                    Note = ri.Note,
                })
                .OrderBy(vm => vm.Name)
                .ToArray(),
            IngredientDropDowns = await ingredientService.GetIngredientDropDownAsync(),
            CategoryId = recipe.CategoryId,
            CategoryDropDown = await categoryService.GetCategoryDropDownAsync()
        };

        return model;
    }

    public async Task<RecipeDeleteModel> GetRecipeForDeleteAsync(int id)
    {
        Recipe? recipe = await GetRecipeAsync(id);

        if (recipe is null) return null;

        var model = new RecipeDeleteModel()
        {
            Id = id,
            Name = recipe.Name,
        };

        return model;
    }

    public async Task AddRecipeIngredientRowAsync(int recipeId, IngredientAddRow newIngredient)
    {
        Recipe? recipe = await GetRecipeAsync(recipeId);
        if (recipe is null) return;

        bool isIngredientDuplicate = await dbContext.RecipesIngredients
            .AnyAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == newIngredient.Id);

        if (isIngredientDuplicate) return;

        await dbContext.RecipesIngredients.AddAsync(new RecipeIngredient
        {
            RecipeId = recipeId,
            IngredientId = newIngredient.Id,
            Quantity = newIngredient.Quantity,
            MeasurementUnit = newIngredient.MeasurementUnit,
            Note = null ?? newIngredient.Note?.Trim()
        });

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRecipeIngredientRowAsync(int recipeId, int ingredientId)
    {
        Recipe? recipe = await GetRecipeAsync(recipeId);
        if (recipe is null) return;

        Ingredient? ingredient = await ingredientService.GetIngredientAsync(ingredientId);
        if (ingredient is null) return;

        await dbContext.RecipesIngredients
            .Where(ri => ri.Recipe.Id == recipe.Id && ri.Ingredient.Id == ingredient.Id)
            .ExecuteDeleteAsync();

        await dbContext.SaveChangesAsync();
    }

    private string? DisplayAbbreviatedUnit(MeasurementUnit measurementUnit)
    {
        return measurementUnit switch
        {
            MeasurementUnit.Milliliter => "mL",
            MeasurementUnit.Liter => "l",
            MeasurementUnit.Gram => "g",
            MeasurementUnit.Kilogram => "kg",
            MeasurementUnit.Teaspoon => "tsp",
            MeasurementUnit.Tablespoon => "tbsp",
            MeasurementUnit.Cup => "c",
            MeasurementUnit.Pint => "pt",
            MeasurementUnit.Quart => "qt",
            MeasurementUnit.Gallon => "gal",
            MeasurementUnit.Ounce => "oz",
            MeasurementUnit.Fluidounce => "fl oz",
            MeasurementUnit.Pound => "lb",
            _ => null
        };
    }
}
