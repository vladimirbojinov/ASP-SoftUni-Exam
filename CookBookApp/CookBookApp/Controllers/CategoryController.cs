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
}
