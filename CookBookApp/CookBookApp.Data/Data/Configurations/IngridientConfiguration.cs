namespace CookBookApp.Data.Data.Configurations;

using CookBookApp.Data.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    List<Ingredient> _ingredients;

    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasData(_ingredients);
    }
}
