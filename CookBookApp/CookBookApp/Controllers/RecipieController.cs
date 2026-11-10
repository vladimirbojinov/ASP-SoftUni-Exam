using CookBookApp.Core.Contracts;
using CookBookApp.DTOs.Ingredient;
using CookBookApp.DTOs.Recipe;
using Microsoft.AspNetCore.Mvc;

namespace CookBookApp.Web.Controllers;

public class RecipeController(
    IRecipeService recipeService,
    IRecipeIngredientService recipeIngredientService,
    ICategoryService categoryService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        RecipeIndexModel[] recipes = await recipeService.GetRecipeIndexAsync();

        ViewData["CategoriesDropDown"] = await categoryService.GetDropDownAsync();

        return View(recipes);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View(
            await recipeService.GetRecipeForCreationAsync()
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(RecipeInputModel model)
    {
        if (ModelState.IsValid == false)
        {
            model.CategoryDropDown = await categoryService.GetDropDownAsync();
            return View(model);
        }

        try
        {
            await recipeService.CreateRecipeAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            model.CategoryDropDown = await categoryService.GetDropDownAsync();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        RecipeDetailsModel? model = await recipeService.GetRecipeDetailsAsync(id);

        if (model is null) return NotFound();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        RecipeInputModel? model = await recipeService.GetRecipeForEditAsync(id);

        if (model is null) return BadRequest();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RecipeInputModel model)
    {
        if (ModelState.IsValid == false)
        {
            ModelState.AddModelError(string.Empty, "Invalid recipe model!");

            model.CategoryDropDown = await categoryService.GetDropDownAsync();
            model.IngredientDropDowns = await ingredientService.GetIngredientDropDownAsync();
            model.Ingredients = await recipeIngredientService.GetRecipeIngredientAsync(model.Id);

            return View(model);
        }

        try
        {
            await recipeService.EditRecipeAsync(model);
        }
        catch (InvalidOperationException ex)
        {
            model.CategoryDropDown = await categoryService.GetDropDownAsync();
            model.IngredientDropDowns = await ingredientService.GetIngredientDropDownAsync();
            model.Ingredients = await recipeIngredientService.GetRecipeIngredientAsync(model.Id);

            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        RecipeDeleteModel? model = await recipeService.GetRecipeForDeleteAsync(id);

        if (model is null) return NotFound("Recipe not found!");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        await recipeService.DeleteRecipe(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Search(string? nameFilter, int? categoryId)
    {
        RecipeIndexModel[] filteredRecipes = await recipeService.SearchAsync(nameFilter, categoryId);

        ViewData["CategoriesDropDown"] = await categoryService.GetDropDownAsync();

        return View(nameof(Index), filteredRecipes);
    }

    [HttpPost]
    public async Task<IActionResult> AddRow(int recipeId, IngredientAddRow newIngredient)
    {
        if (ModelState.IsValid == false)
        {
            ModelState.AddModelError(string.Empty, "Invalid ingredient model!");
            RecipeInputModel? recipeModel = await recipeService.GetRecipeForEditAsync(recipeId);

            if (recipeModel is null) return NotFound();
            return View(nameof(Edit), recipeModel);
        }

        try
        {
            await recipeIngredientService.AddRecipeIngredientRowAsync(recipeId, newIngredient);
            return RedirectToAction(nameof(Edit), new { id = recipeId });
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            RecipeInputModel? recipeModel = await recipeService.GetRecipeForEditAsync(recipeId);

            if (recipeModel is null) return NotFound();
            return View(nameof(Edit), recipeModel);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveRow(int recipeId, int ingredientId)
    {
        await recipeIngredientService.RemoveRecipeIngredientRowAsync(recipeId, ingredientId);

        return RedirectToAction(nameof(Edit), new { id = recipeId });
    }
}
