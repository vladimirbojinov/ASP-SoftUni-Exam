namespace CookBookApp.Data.Configurations;

using CookBookApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    private static List<Recipe> Recipes =
    [
        new Recipe
        {
            Id = 1,
            Name = "Classic Pancakes",
            Instructions = "Whisk flour, milk, eggs, sugar and salt into a smooth batter. Fry ladlefuls in melted butter until golden on both sides.",
            PrepTime = 10,
            CookTime = 15,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 1,
        },
        new Recipe
        {
            Id = 2,
            Name = "Spaghetti Bolognese",
            Instructions = "Fry the onion in olive oil, brown the beef, add the canned tomatoes and simmer for 45 minutes. Boil the spaghetti and serve with the sauce.",
            PrepTime = 15,
            CookTime = 60,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 2,
        },
        new Recipe
        {
            Id = 3,
            Name = "Shopska Salad",
            Instructions = "Dice the tomatoes and cucumbers, dress with olive oil and salt, then top with grated feta cheese.",
            PrepTime = 10,
            CookTime = 1,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 4,
        },
        new Recipe
        {
            Id = 4,
            Name = "Chocolate Brownies",
            Instructions = "Melt the chocolate with the butter, stir in sugar and eggs, fold in the flour. Bake at 180°C for about 25 minutes.",
            PrepTime = 15,
            CookTime = 25,
            Servings = 12,
            ImageUrl = null,
            CategoryId = 3,
        }
    ];

    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasData(Recipes);
    }
}
