using CookBookApp.Data.Models;
using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;

namespace CookBookApp.Data.Repository;

public class RecipeIngredientRepository(CookBookDbContext dbContext)
    : BaseRepository<RecipeIngredient>(dbContext), IRecipeIngredientRepository
{
}
