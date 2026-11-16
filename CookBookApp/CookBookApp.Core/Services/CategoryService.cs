using CookBookApp.Core.Contracts;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Category;
using CookBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task CreateCategoryAsync(CategoryCreateModel model)
    {
        if (await categoryRepository.ExistsByNameAsync(model.Name.Trim()))
            throw new InvalidOperationException("Category already exists!");

        await categoryRepository.AddAsync(new Category()
        {
            Name = model.Name.Trim()
        });

        await categoryRepository.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        Category category = await categoryRepository.FindByIdAsync(id)
            ?? throw new ArgumentException("Category not found!");

        categoryRepository.Remove(category);
        await categoryRepository.SaveChangesAsync();
    }

    public async Task EditCategoryAsync(CategoryEditModel model)
    {
        Category category = await categoryRepository.FindByIdAsync(model.Id)
            ?? throw new ArgumentException("Category not found!");

        if (await categoryRepository.ExistsByNameAsync(model.Name.Trim()))
            throw new InvalidOperationException("Category already exists!");

        category.Name = model.Name.Trim();
        await categoryRepository.SaveChangesAsync();
    }

    public async Task<CategoryCreateModel> GetCategoryForCreateAsync()
    {
        return new CategoryCreateModel();
    }

    public async Task<CategoryDeleteModel> GetCategoryForDeleteAsync(int id)
    {
        Category category = await categoryRepository.GetFullCategory(id)
            ?? throw new ArgumentException("Category not found!");

        return new CategoryDeleteModel()
        {
            Id = category.Id,
            Name = category.Name,
            RecipesCount = category.Recipes.Count
        };
    }

    public async Task<CategoryEditModel> GetCategoryForEditAsync(int id)
    {
        Category category = await categoryRepository.FindByIdAsync(id)
            ?? throw new ArgumentException("Category not found!");

        return new CategoryEditModel()
        {
            Name = category.Name
        };
    }

    public async Task<CategoryIndexModel[]> GetCategoryForIndexAsync()
    {
        return await categoryRepository
            .GetAllAsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategoryIndexModel()
            {
                Id = c.Id,
                Name = c.Name,
                RecipesCount = c.Recipes.Count
            })
            .ToArrayAsync();
    }
}
