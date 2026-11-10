using CookBookApp.DTOs.Category;

namespace CookBookApp.Core.Contracts;

public interface ICategoryService
{
    Task<CategoryDropDown[]> GetDropDownAsync();

    Task<bool> ExistsAsync(int id);
}
