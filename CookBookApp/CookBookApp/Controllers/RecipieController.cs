using CookBookApp.Core.Contracts;
using CookBookApp.DTOs.Ingredient;
using CookBookApp.DTOs.Recipe;
using Microsoft.AspNetCore.Mvc;
using static CookBookApp.GCommon.Constants.ApplicationConstants;

namespace CookBookApp.Web.Controllers;

public class RecipeController(
    IRecipeService recipeService,
    ICategoryService categoryService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        RecipeIndexModel[] recipes = await recipeService.GetRecipeIndexAsync();

        ViewData["CategoriesDropDown"] = await categoryService.GetCategoryDropDownAsync();

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
        if (ModelState.IsValid == false) return View(model);

        try
        {
            await recipeService.SaveRecipeAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch(InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            model.CategoryDropDown = await categoryService.GetCategoryDropDownAsync();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var model = await recipeService.GetRecipeDetailsAsync(id);

        if (model is null) return BadRequest();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await recipeService.GetRecipeForEditAsync(id);

        if (model is null) return BadRequest();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RecipeInputModel model)
    {
        if (ModelState.IsValid == false)
        {
            ModelState.AddModelError(string.Empty, "Invalid recipe model!");
            return View(model);
        }

        if (await recipeService.EditRecipeAsync(model) == false)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await recipeService.GetRecipeForDeleteAsync(id);

        if (model is null) return BadRequest();

        return View(model);
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        recipeService.DeleteRecipe(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Search(string? nameFilter, int? categoryId)
    {
        var filteredRecipes = await recipeService.SearchAsync(nameFilter, categoryId);

        ViewData["CategoriesDropDown"] = await categoryService.GetCategoryDropDownAsync();

        return View(nameof(Index), filteredRecipes);
    }

    [HttpPost]
    public async Task<IActionResult> AddRow(int recipeId, IngredientAddRow newIngredient)
    {
        if (ModelState.IsValid == false) return BadRequest();

        await recipeService.AddRecipeIngredientRowAsync(recipeId, newIngredient);

        return RedirectToAction(nameof(Edit), new { id = recipeId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveRow(int recipeId, int ingredientId)
    {
        await recipeService.DeleteRecipeIngredientRowAsync(recipeId, ingredientId);

        return RedirectToAction(nameof(Edit), new { id = recipeId });
    }
}
