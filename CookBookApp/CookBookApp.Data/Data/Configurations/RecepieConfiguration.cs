namespace CookBookApp.Data.Data.Configurations;

using CookBookApp.Data.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    List<Recipe> _recipes;

    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasData(_recipes);
    }
}
