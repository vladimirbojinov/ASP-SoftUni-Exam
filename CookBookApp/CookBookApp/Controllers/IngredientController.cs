using CookBookApp.Core.Contracts;
using CookBookApp.DTOs.Ingredient;
using Microsoft.AspNetCore.Mvc;

namespace CookBookApp.Web.Controllers;

public class IngredientController(IIngredientService ingredientService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IngredientIndexModel[] ingredients = await ingredientService.GetIngredientForIndexAsync();

        return View(ingredients);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        IngredientCreateModel model = await ingredientService.GetIngredientForCreateAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(IngredientCreateModel model)
    {
        try
        {
            await ingredientService.CreateIngredientAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            IngredientEditModel model = await ingredientService.GetIngredientForEditAsync(id);
            return View(model);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(IngredientEditModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Invalid ingredient data!");
            return View(model);
        }

        try
        {
            await ingredientService.EditIngredientAsync(model);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
}
