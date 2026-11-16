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

    {
        return await categoryRepository
            .GetAllAsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDropDown()
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToArrayAsync();
    }
}
