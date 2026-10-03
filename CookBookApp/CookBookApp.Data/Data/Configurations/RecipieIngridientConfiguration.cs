namespace CookBookApp.Data.Data.Configurations;

using CookBookApp.Data.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    List<RecipeIngredient> _recipesIngredients;

    public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.HasData(_recipesIngredients);
    }
}
