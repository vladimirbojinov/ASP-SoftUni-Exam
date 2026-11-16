using CookBookApp.Core.Contracts;
using CookBookApp.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace CookBookApp.Web.Controllers;

public class CategoryController(ICategoryService categoryService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        CategoryIndexModel[] recipes = await categoryService.GetCategoryForIndexAsync();

        return View(recipes);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        CategoryCreateModel model = await categoryService.GetCategoryForCreateAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        try
        {
            await categoryService.CreateCategoryAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
}
