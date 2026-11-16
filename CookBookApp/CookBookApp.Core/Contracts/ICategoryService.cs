using CookBookApp.DTOs.Category;
using CookBookApp.Models;

namespace CookBookApp.Core.Contracts;

public interface ICategoryService
{
    Task<CategoryIndexModel[]> GetCategoryForIndexAsync();

    Task<CategoryCreateModel> GetCategoryForCreateAsync();

    Task<CategoryEditModel> GetCategoryForEditAsync(int id);

    Task<CategoryDeleteModel> GetCategoryForDeleteAsync(int id);

    Task CreateCategoryAsync(CategoryCreateModel model);

    Task EditCategoryAsync(CategoryEditModel model);

    Task DeleteCategoryAsync(int id);
}
