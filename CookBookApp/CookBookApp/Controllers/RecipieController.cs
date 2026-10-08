namespace CookBookApp.Web.Controllers;

using CookBookApp.Data;
using CookBookApp.Data.Enums;
using CookBookApp.Data.Models;
using CookBookApp.Web.ViewModels.Category;
using CookBookApp.Web.ViewModels.Ingredient;
using CookBookApp.Web.ViewModels.Recipe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CookBookApp.Web.Common.ApplicationConstants;

public class RecipeController(CookBookDbContext dbContext) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        RecipeIndexViewModel[] recipes = dbContext.Recipes
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .Select(r => new RecipeIndexViewModel()
            {
                Id = r.Id,
                Name = r.Name,
                ImageUrl = r.ImageUrl ?? DefaultImageUrl,
                PrepTime = r.PrepTime,
                CookTime = r.CookTime,
                Servings = r.Servings,
                IngredientCount = r.RecipesIngredients.Count,
                CategoryName = r.Category.Name,
            })
            .ToArray();

        return View(recipes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        RecipeInputViewModel inputRecipe = new()
        {
            PrepTime = 1,
            CookTime = 1,
            Servings = 1,
            CategoryDropDown = GetCategoryDropDown()
        };

        return View(inputRecipe);
    }

    [HttpPost]
    public IActionResult Create(RecipeInputViewModel inputRecipe)
    {
        Category? category = dbContext.Categories.Find(inputRecipe.CategoryId);
        if (category is null) ModelState.AddModelError("CategoryId", "Please select a category!");

        if (ModelState.IsValid == false)
        {
            inputRecipe.CategoryDropDown = GetCategoryDropDown();

            return View(inputRecipe);
        }

        Recipe recipe = new()
        {
            Name = inputRecipe.Name,
            CategoryId = category!.Id,
            Instructions = inputRecipe.Instructions.Trim(),
            PrepTime = inputRecipe.PrepTime,
            CookTime = inputRecipe.CookTime,
            Servings = inputRecipe.Servings,
            ImageUrl = inputRecipe.ImageUrl
        };

        dbContext.Add(recipe);
        dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        Recipe? recipe = GetRecipe(id);

        if (recipe is null) return BadRequest();

        RecipeDetailsViewModel recipeDetails = new()
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
                .Select(ri => new IngredientDetailsViewModel()
                {
                    Name = ri.Ingredient.Name.Trim(),
                    Quantity = ri.Quantity.ToString("0.###"),
                    MeasurementUnit = DisplayAbbreviatedUnit(ri.MeasurementUnit),
                    Note = ri.Note,
                })
                .ToList()
        };

        return View(recipeDetails);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Recipe? recipe = GetRecipe(id);

        if (recipe is null) return BadRequest();
        List<CategoryDropDown> categoryDropdown = GetCategoryDropDown();

        RecipeInputViewModel recipeToEdit = new()
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
                .ToList(),
            IngredientDropDowns = GetIngredientsDropDown(),
            CategoryId = recipe.CategoryId,
            CategoryDropDown = categoryDropdown
        };

        return View(recipeToEdit);
    }

    [HttpPost]
    public IActionResult Edit(RecipeInputViewModel inputRecipe)
    {
        Recipe? recipe = GetRecipe(inputRecipe.Id);
        if (recipe is null) return BadRequest();

        if (ModelState.IsValid == false)
        {
            ModelState.AddModelError(string.Empty, "Invalid recipe model!");
            return View(inputRecipe);
        }

        recipe.Instructions = string.Join('\n',
            recipe.Instructions
            .Split('\n', StringSplitOptions.TrimEntries)
            .Where(line => line.Length > 0));

        recipe.Name = inputRecipe.Name.Trim();
        recipe.PrepTime = inputRecipe.PrepTime;
        recipe.CookTime = inputRecipe.CookTime;
        recipe.Servings = inputRecipe.Servings;
        recipe.ImageUrl = inputRecipe.ImageUrl?.Trim();

        dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Recipe? recipe = GetRecipe(id);
        if (recipe is null) return BadRequest();

        RecipeIndexViewModel recipeDetails = new()
        {
            Id = recipe.Id,
            Name = recipe.Name
        };

        return View(recipeDetails);
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        dbContext.Recipes
            .Where(r => r.Id == id)
            .ExecuteDelete();

        dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult AddRow(int recipeId, IngredientAddRow newIngredient)
    {
        Recipe? recipe = GetRecipe(recipeId);
        if (recipe is null) return BadRequest();

        bool isIngredientDuplicate = dbContext.RecipesIngredients
            .Any(ri => ri.RecipeId == recipeId && ri.IngredientId == newIngredient.Id);

        if (isIngredientDuplicate)
            ModelState.AddModelError("NewIngredient.Id", "This ingredient is already in the recipe.");

        if (ModelState.IsValid == false)
        {
            ModelState.AddModelError(string.Empty, "Invalid model error!");

            RecipeInputViewModel inputRecipe = new()
            {
                Id = recipeId,
                Name = recipe.Name,
                Instructions = recipe.Instructions.Trim(),
                PrepTime = recipe.PrepTime,
                CookTime = recipe.CookTime,
                Servings = recipe.Servings,
                ImageUrl = recipe.ImageUrl?.Trim(),
                Ingredients = recipe.RecipesIngredients
                    .Select(ri => new IngredientInputViewModel()
                    {
                        Id = ri.IngredientId,
                        Name = ri.Ingredient.Name.Trim(),
                        Quantity = ri.Quantity,
                        MeasurementUnit = ri.MeasurementUnit,
                        Note = ri.Note,
                    })
                .ToList(),
                IngredientDropDowns = GetIngredientsDropDown(),
            };

            RecipeInputViewModel model = inputRecipe;
            return View(nameof(Edit), model);
        }

        dbContext.RecipesIngredients.Add(new RecipeIngredient
        {
            RecipeId = recipeId,
            IngredientId = newIngredient.Id,
            Quantity = newIngredient.Quantity,
            MeasurementUnit = newIngredient.MeasurementUnit,
            Note = null ?? newIngredient.Note?.Trim()
        });

        dbContext.SaveChanges();
        return RedirectToAction(nameof(Edit), new { id = recipeId });
    }

    [HttpPost]
    public IActionResult RemoveRow(int recipeId, int ingredientId)
    {
        Recipe? recipe = GetRecipe(recipeId);
        if (recipe is null) return BadRequest();

        Ingredient? ingredient = GetIngredient(ingredientId);
        if (ingredient is null) return BadRequest();

        dbContext.RecipesIngredients
            .Where(ri => ri.Recipe.Id == recipe.Id && ri.Ingredient.Id == ingredient.Id)
            .ExecuteDelete();

        dbContext.SaveChanges();
        return RedirectToAction(nameof(Edit), new { id = recipeId });
    }

    private Recipe? GetRecipe(int id)
    {
        return dbContext.Recipes
            .Include(r => r.Category)
            .Include(r => r.RecipesIngredients)
            .ThenInclude(r => r.Ingredient)
            .SingleOrDefault(r => r.Id == id);
    }

    private Ingredient? GetIngredient(int ingredientId)
    {
        return dbContext.Ingredients.Find(ingredientId);
    }

    private List<CategoryDropDown> GetCategoryDropDown()
    {
        return dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDropDown()
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToList();
    }

    private List<IngredientDropDown> GetIngredientsDropDown()
    {
        return dbContext.Ingredients
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .Select(i => new IngredientDropDown()
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToList();
    }

    private static string? DisplayAbbreviatedUnit(MeasurementUnit measurementUnit)
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
