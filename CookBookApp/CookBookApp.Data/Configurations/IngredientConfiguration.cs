using CookBookApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBookApp.Data.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    private static readonly List<Ingredient> Ingredients =
    [
        new Ingredient { Id = 1, Name = "Egg" },
        new Ingredient { Id = 2, Name = "Flour" },
        new Ingredient { Id = 3, Name = "Milk" },
        new Ingredient { Id = 4, Name = "Sugar" },
        new Ingredient { Id = 5, Name = "Salt" },
        new Ingredient { Id = 6, Name = "Butter" },
        new Ingredient { Id = 7, Name = "Minced Beef" },
        new Ingredient { Id = 8, Name = "Onion" },
        new Ingredient { Id = 9, Name = "Garlic" },
        new Ingredient { Id = 10, Name = "Crushed Tomatoes" },
        new Ingredient { Id = 11, Name = "Spaghetti" },
        new Ingredient { Id = 12, Name = "Chocolate Chips" },
        new Ingredient { Id = 13, Name = "Baking Soda" },
        new Ingredient { Id = 14, Name = "Romaine Lettuce" },
        new Ingredient { Id = 15, Name = "Caesar Dressing" },
        new Ingredient { Id = 16, Name = "Parmesan Cheese" },
        new Ingredient { Id = 17, Name = "Croutons" },
        new Ingredient { Id = 18, Name = "Sourdough Bread" },
        new Ingredient { Id = 19, Name = "Avocado" },
        new Ingredient { Id = 20, Name = "Lemon Juice" },
        new Ingredient { Id = 21, Name = "Broccoli" },
        new Ingredient { Id = 22, Name = "Bell Pepper" },
        new Ingredient { Id = 23, Name = "Soy Sauce" },
        new Ingredient { Id = 24, Name = "Baking Powder" },
        new Ingredient { Id = 25, Name = "Cucumber" },
        new Ingredient { Id = 26, Name = "Tomato" },
        new Ingredient { Id = 27, Name = "Feta Cheese" },
        new Ingredient { Id = 28, Name = "Kalamata Olives" },
        new Ingredient { Id = 29, Name = "Chicken Breast" },
        new Ingredient { Id = 30, Name = "Olive Oil" }
    ];

    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasData(Ingredients);
    }
}
