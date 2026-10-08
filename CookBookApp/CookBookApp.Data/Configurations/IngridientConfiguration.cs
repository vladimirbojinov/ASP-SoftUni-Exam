namespace CookBookApp.Data.Configurations;

using CookBookApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    private static List<Ingredient> Ingredients =>
    [
        new Ingredient { Id = 1,  Name = "Egg" },
        new Ingredient { Id = 2,  Name = "Flour" },
        new Ingredient { Id = 3,  Name = "Milk" },
        new Ingredient { Id = 4,  Name = "Sugar" },
        new Ingredient { Id = 5,  Name = "Butter" },
        new Ingredient { Id = 6,  Name = "Salt" },
        new Ingredient { Id = 7,  Name = "Spaghetti" },
        new Ingredient { Id = 8,  Name = "Minced Beef" },
        new Ingredient { Id = 9,  Name = "Tomato" },
        new Ingredient { Id = 10, Name = "Olive Oil" },
        new Ingredient { Id = 11, Name = "Cucumber" },
        new Ingredient { Id = 12, Name = "Feta Cheese" },
        new Ingredient { Id = 13, Name = "Dark Chocolate" },
        new Ingredient { Id = 14, Name = "Canned Tomatoes" },
        new Ingredient { Id = 15, Name = "Onion" }
    ];

    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasData(Ingredients);
    }
}
