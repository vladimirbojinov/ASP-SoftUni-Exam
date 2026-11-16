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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            CategoryEditModel model = await categoryService.GetCategoryForEditAsync(id);
            return View(model);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryEditModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Invalid category data!");
            return View(model);
        }

        try
        {
            await categoryService.EditCategoryAsync(model);
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

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            CategoryDeleteModel model = await categoryService.GetCategoryForDeleteAsync(id);
            return View(model);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        try
        {
            await categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}
