using CookBookApp.DTOs.Category;

namespace CookBookApp.Core.Contracts;

public interface ICategoryService
{
    Task<CategoryDropDown[]> GetCategoryDropDownAsync();

    Task<bool> CheckCategoryExistsAsync(int id);
}
