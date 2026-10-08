namespace CookBookApp.Data;

using CookBookApp.Data.Configurations;
using CookBookApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class CookBookDbContext(DbContextOptions<CookBookDbContext> options) : IdentityDbContext(options) 
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<RecipeIngredient> RecipesIngredients { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new IngredientConfiguration());
        builder.ApplyConfiguration(new RecipeConfiguration());
        builder.ApplyConfiguration(new RecipeIngredientConfiguration());
    }
}
