using CookBookApp.DTOs.Category;
using CookBookApp.Models;

namespace CookBookApp.Data.Repository.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string name);

    Task<Category?> FindByIdAsync(int id);

    Task<CategoryDropDown[]> GetDropDownAsync();

    Task<Category?> GetFullCategory(int id);
}
