using CookBookApp.GCommon.Enums;
using CookBookApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static CookBookApp.GCommon.Constants.Roles;

namespace CookBookApp.Data.Seeding;

public class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider service)
    {
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var context = service.GetRequiredService<CookBookDbContext>();
        var cfg = service.GetRequiredService<IConfiguration>();

        foreach (var role in new[] { User, Administrator })
        {
            if (await roleManager.RoleExistsAsync(role) == false)
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = cfg["AdminUser:Email"];
        var adminPassword = cfg["AdminUser:Password"];

        if (string.IsNullOrEmpty(adminEmail) ||
            string.IsNullOrEmpty(adminPassword))
            throw new InvalidOperationException("Admin user credentials are not configured properly.");

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded == false)
                throw new InvalidOperationException("Failed to create admin user.");
        }

        if (await context.Recipes.AnyAsync(r => r.CreatedByUserId == adminUser.Id) == false)
        {
            AddRecipesToAdmin(context, adminUser);
            await context.SaveChangesAsync();
        }

        if (await userManager.IsInRoleAsync(adminUser, Administrator) == false)
            await userManager.AddToRoleAsync(adminUser, Administrator);
    }

    private static void AddRecipesToAdmin(CookBookDbContext context, ApplicationUser adminUser)
    {
        context.Recipes.AddRange(
        // 1. Breakfast
        // 1. Classic Pancakes (Breakfast)
        new Recipe
        {
            Name = "Classic Pancakes",
            Instructions = "Whisk flour, milk, eggs, sugar and salt into a smooth batter. Fry ladlefuls in melted butter until golden on both sides.",
            PrepTime = 10,
            CookTime = 15,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 1,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 2, Quantity = 250.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Flour
                new RecipeIngredient { IngredientId = 3, Quantity = 350.0m, MeasurementUnit = MeasurementUnit.Milliliter, Note = null }, // Milk
                new RecipeIngredient { IngredientId = 1, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.None, Note = null }, // Egg
                new RecipeIngredient { IngredientId = 4, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.Tablespoon, Note = null }, // Sugar
                new RecipeIngredient { IngredientId = 5, Quantity = 0.5m, MeasurementUnit = MeasurementUnit.Teaspoon, Note = null }, // Salt
                new RecipeIngredient { IngredientId = 6, Quantity = 30.0m, MeasurementUnit = MeasurementUnit.Gram, Note = "Melted" } // Butter
            }
        },

        // 2. Spaghetti Bolognese (Main Course)
        new Recipe
        {
            Name = "Spaghetti Bolognese",
            Instructions = "Brown the minced beef with onions and garlic. Add crushed tomatoes and simmer for 30 minutes. Serve over boiled spaghetti.",
            PrepTime = 15,
            CookTime = 30,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 2,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 11, Quantity = 400.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Spaghetti
                new RecipeIngredient { IngredientId = 7, Quantity = 500.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Minced Beef
                new RecipeIngredient { IngredientId = 8, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.None, Note = "Medium" }, // Onion
                new RecipeIngredient { IngredientId = 9, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.None, Note = "Cloves" }, // Garlic
                new RecipeIngredient { IngredientId = 10, Quantity = 800.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null } // Crushed Tomatoes
            }
        },

        // 3. Chocolate Chip Cookies (Dessert)
        new Recipe
        {
            Name = "Chocolate Chip Cookies",
            Instructions = "Cream butter and sugars, beat in eggs and vanilla. Mix in flour, baking soda, and salt, then fold in chocolate chips. Bake at 350°F for 10 minutes.",
            PrepTime = 15,
            CookTime = 10,
            Servings = 12,
            ImageUrl = null,
            CategoryId = 3,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 6, Quantity = 220.0m, MeasurementUnit = MeasurementUnit.Gram, Note = "Softened" }, // Butter
                new RecipeIngredient { IngredientId = 4, Quantity = 200.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Sugar
                new RecipeIngredient { IngredientId = 1, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.None, Note = null }, // Egg
                new RecipeIngredient { IngredientId = 2, Quantity = 300.0m, MeasurementUnit = MeasurementUnit.Gram, Note = "All-purpose" }, // Flour
                new RecipeIngredient { IngredientId = 13, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.Teaspoon, Note = null }, // Baking Soda
                new RecipeIngredient { IngredientId = 12, Quantity = 250.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null } // Chocolate Chips
            }
        },

        // 4. Classic Caesar Salad (Salad)
        new Recipe
        {
            Name = "Classic Caesar Salad",
            Instructions = "Toss chopped romaine lettuce with Caesar dressing, parmesan cheese, and garlic croutons. Serve immediately.",
            PrepTime = 15,
            CookTime = 0,
            Servings = 2,
            ImageUrl = null,
            CategoryId = 4,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 14, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.None, Note = "Head" }, // Romaine Lettuce
                new RecipeIngredient { IngredientId = 15, Quantity = 120.0m, MeasurementUnit = MeasurementUnit.Milliliter, Note = null }, // Caesar Dressing
                new RecipeIngredient { IngredientId = 16, Quantity = 50.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Parmesan Cheese
                new RecipeIngredient { IngredientId = 17, Quantity = 80.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null } // Croutons
            }
        },

        // 5. Avocado Toast (Breakfast)
        new Recipe
        {
            Name = "Avocado Toast",
            Instructions = "Toast a slice of sourdough bread. Mash ripe avocado with lemon juice, salt, and pepper, then spread evenly over the toast. Top with red pepper flakes.",
            PrepTime = 5,
            CookTime = 5,
            Servings = 1,
            ImageUrl = null,
            CategoryId = 1,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 18, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.None, Note = "Slice" }, // Sourdough Bread
                new RecipeIngredient { IngredientId = 19, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.None, Note = "Ripe" }, // Avocado
                new RecipeIngredient { IngredientId = 20, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.Tablespoon, Note = null }, // Lemon Juice
                new RecipeIngredient { IngredientId = 5, Quantity = 0.25m, MeasurementUnit = MeasurementUnit.Teaspoon, Note = null } // Salt
            }
        },

        // 6. Vegetable Stir Fry (Main Course)
        new Recipe
        {
            Name = "Vegetable Stir Fry",
            Instructions = "Toss broccoli, bell peppers, carrots, and snap peas in a hot wok with oil. Pour over soy sauce and garlic, and stir-fry until tender-crisp.",
            PrepTime = 15,
            CookTime = 10,
            Servings = 3,
            ImageUrl = null,
            CategoryId = 2,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 21, Quantity = 200.0m, MeasurementUnit = MeasurementUnit.Gram, Note = "Florets" }, // Broccoli
                new RecipeIngredient { IngredientId = 22, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.None, Note = null }, // Bell Pepper
                new RecipeIngredient { IngredientId = 30, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.Tablespoon, Note = null }, // Olive Oil
                new RecipeIngredient { IngredientId = 23, Quantity = 45.0m, MeasurementUnit = MeasurementUnit.Milliliter, Note = null }, // Soy Sauce
                new RecipeIngredient { IngredientId = 9, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.None, Note = "Cloves" } // Garlic
            }
        },

        // 7. Vanilla Cupcakes (Dessert)
        new Recipe
        {
            Name = "Vanilla Cupcakes",
            Instructions = "Mix flour, sugar, baking powder, butter, milk, and vanilla extract. Pour into cupcake liners and bake at 350°F for 18 minutes.",
            PrepTime = 20,
            CookTime = 18,
            Servings = 12,
            ImageUrl = null,
            CategoryId = 3,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 2, Quantity = 200.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Flour
                new RecipeIngredient { IngredientId = 4, Quantity = 150.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Sugar
                new RecipeIngredient { IngredientId = 24, Quantity = 1.5m, MeasurementUnit = MeasurementUnit.Teaspoon, Note = null }, // Baking Powder
                new RecipeIngredient { IngredientId = 6, Quantity = 100.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Butter
                new RecipeIngredient { IngredientId = 3, Quantity = 120.0m, MeasurementUnit = MeasurementUnit.Milliliter, Note = null }, // Milk
                new RecipeIngredient { IngredientId = 1, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.None, Note = null } // Egg
            }
        },

        // 8. Greek Salad (Salad)
        new Recipe
        {
            Name = "Greek Salad",
            Instructions = "Combine chopped cucumbers, tomatoes, red onions, kalamata olives, and feta cheese. Drizzle with olive oil and oregano.",
            PrepTime = 15,
            CookTime = 0,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 4,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 25, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.None, Note = null }, // Cucumber
                new RecipeIngredient { IngredientId = 26, Quantity = 3.0m, MeasurementUnit = MeasurementUnit.None, Note = null }, // Tomato
                new RecipeIngredient { IngredientId = 8, Quantity = 0.5m, MeasurementUnit = MeasurementUnit.None, Note = "Red" }, // Onion
                new RecipeIngredient { IngredientId = 28, Quantity = 100.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Kalamata Olives
                new RecipeIngredient { IngredientId = 27, Quantity = 150.0m, MeasurementUnit = MeasurementUnit.Gram, Note = null }, // Feta Cheese
                new RecipeIngredient { IngredientId = 30, Quantity = 3.0m, MeasurementUnit = MeasurementUnit.Tablespoon, Note = null } // Olive Oil
            }
        },

        // 9. Fluffy Scrambled Eggs (Breakfast)
        new Recipe
        {
            Name = "Fluffy Scrambled Eggs",
            Instructions = "Whisk eggs with a splash of milk, salt, and pepper. Cook in a skillet over medium-low heat with butter, stirring gently until soft curds form.",
            PrepTime = 3,
            CookTime = 5,
            Servings = 2,
            ImageUrl = null,
            CategoryId = 1,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 1, Quantity = 3.0m, MeasurementUnit = MeasurementUnit.None, Note = null }, // Egg
                new RecipeIngredient { IngredientId = 3, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.Tablespoon, Note = null }, // Milk
                new RecipeIngredient { IngredientId = 6, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.Tablespoon, Note = null }, // Butter
                new RecipeIngredient { IngredientId = 5, Quantity = 0.25m, MeasurementUnit = MeasurementUnit.Teaspoon, Note = null } // Salt
            }
        },

        // 10. Grilled Chicken Breast (Main Course)
        new Recipe
        {
            Name = "Grilled Chicken Breast",
            Instructions = "Season chicken breasts with olive oil, garlic powder, paprika, salt, and pepper. Grill over medium-high heat for 6-7 minutes per side until fully cooked.",
            PrepTime = 10,
            CookTime = 14,
            Servings = 2,
            ImageUrl = null,
            CategoryId = 2,
            CreatedByUserId = adminUser.Id,
            RecipesIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { IngredientId = 29, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.None, Note = null }, // Chicken Breast
                new RecipeIngredient { IngredientId = 30, Quantity = 2.0m, MeasurementUnit = MeasurementUnit.Tablespoon, Note = null }, // Olive Oil
                new RecipeIngredient { IngredientId = 9, Quantity = 1.0m, MeasurementUnit = MeasurementUnit.Teaspoon, Note = "Powder" }, // Garlic
                new RecipeIngredient { IngredientId = 5, Quantity = 0.5m, MeasurementUnit = MeasurementUnit.Teaspoon, Note = null } // Salt
            }
        });
    }
}
