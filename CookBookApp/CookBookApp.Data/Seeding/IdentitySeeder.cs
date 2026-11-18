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

        if (adminUser.Recipes.Count != 0 == false)
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
        new Recipe
        {
            Name = "Classic Pancakes",
            Instructions = "Whisk flour, milk, eggs, sugar and salt into a smooth batter. Fry ladleful in melted butter until golden on both sides.",
            PrepTime = 10,
            CookTime = 15,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 1,
            CreatedByUserId = adminUser.Id
        },

        // 2. Main Course
        new Recipe
        {
            Name = "Spaghetti Bolognese",
            Instructions = "Brown the minced beef with onions and garlic. Add crushed tomatoes and simmer for 30 minutes. Serve over boiled spaghetti.",
            PrepTime = 15,
            CookTime = 30,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 2,
            CreatedByUserId = adminUser.Id
        },

        // 3. Dessert
        new Recipe
        {
            Name = "Chocolate Chip Cookies",
            Instructions = "Cream butter and sugars, beat in eggs and vanilla. Mix in flour, baking soda, and salt, then fold in chocolate chips. Bake at 350°F for 10 minutes.",
            PrepTime = 15,
            CookTime = 10,
            Servings = 12,
            ImageUrl = null,
            CategoryId = 3,
            CreatedByUserId = adminUser.Id
        },

        // 4. Salad
        new Recipe
        {
            Name = "Classic Caesar Salad",
            Instructions = "Toss chopped romaine lettuce with Caesar dressing, parmesan cheese, and garlic croutons. Serve immediately.",
            PrepTime = 15,
            CookTime = 0,
            Servings = 2,
            ImageUrl = null,
            CategoryId = 4,
            CreatedByUserId = adminUser.Id
        },

        // 5. Breakfast
        new Recipe
        {
            Name = "Avocado Toast",
            Instructions = "Toast a slice of sourdough bread. Mash ripe avocado with lemon juice, salt, and pepper, then spread evenly over the toast. Top with red pepper flakes.",
            PrepTime = 5,
            CookTime = 5,
            Servings = 1,
            ImageUrl = null,
            CategoryId = 1,
            CreatedByUserId = adminUser.Id
        },

        // 6. Main Course
        new Recipe
        {
            Name = "Vegetable Stir Fry",
            Instructions = "Toss broccoli, bell peppers, carrots, and snap peas in a hot wok with oil. Pour over soy sauce and garlic, and stir-fry until tender-crisp.",
            PrepTime = 15,
            CookTime = 10,
            Servings = 3,
            ImageUrl = null,
            CategoryId = 2,
            CreatedByUserId = adminUser.Id
        },

        // 7. Dessert
        new Recipe
        {
            Name = "Vanilla Cupcakes",
            Instructions = "Mix flour, sugar, baking powder, butter, milk, and vanilla extract. Pour into cupcake liners and bake at 350°F for 18 minutes.",
            PrepTime = 20,
            CookTime = 18,
            Servings = 12,
            ImageUrl = null,
            CategoryId = 3,
            CreatedByUserId = adminUser.Id
        },

        // 8. Salad
        new Recipe
        {
            Name = "Greek Salad",
            Instructions = "Combine chopped cucumbers, tomatoes, red onions, kalamata olives, and feta cheese. Drizzle with olive oil and oregano.",
            PrepTime = 15,
            CookTime = 0,
            Servings = 4,
            ImageUrl = null,
            CategoryId = 4,
            CreatedByUserId = adminUser.Id
        },

        // 9. Breakfast
        new Recipe
        {
            Name = "Fluffy Scrambled Eggs",
            Instructions = "Whisk eggs with a splash of milk, salt, and pepper. Cook in a skillet over medium-low heat with butter, stirring gently until soft curds form.",
            PrepTime = 3,
            CookTime = 5,
            Servings = 2,
            ImageUrl = null,
            CategoryId = 1,
            CreatedByUserId = adminUser.Id
        },

        // 10. Main Course
        new Recipe
        {
            Name = "Grilled Chicken Breast",
            Instructions = "Season chicken breasts with olive oil, garlic powder, paprika, salt, and pepper. Grill over medium-high heat for 6-7 minutes per side until fully cooked.",
            PrepTime = 10,
            CookTime = 14,
            Servings = 2,
            ImageUrl = null,
            CategoryId = 2,
            CreatedByUserId = adminUser.Id
        });
    }
}
