using CookBookApp.Core.Contracts;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.DTOs.Category;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Core.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    {
        return categoryRepository.ExistsAsync(id);
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
