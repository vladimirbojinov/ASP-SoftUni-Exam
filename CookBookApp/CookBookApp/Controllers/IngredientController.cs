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
}
