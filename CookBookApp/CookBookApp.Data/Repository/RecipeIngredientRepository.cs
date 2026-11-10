using CookBookApp.Data.Repository.Common;
using CookBookApp.Data.Repository.Contracts;
using CookBookApp.Models;

namespace CookBookApp.Data.Repository;

public class RecipeIngredientRepository(CookBookDbContext dbContext)
    : BaseRepository<RecipeIngredient>(dbContext), IRecipeIngredientRepository
{
}
