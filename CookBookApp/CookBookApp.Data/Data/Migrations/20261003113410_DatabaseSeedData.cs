using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CookBookApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Breakfast" },
                    { 2, "Main Course" },
                    { 3, "Dessert" },
                    { 4, "Salad" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Egg" },
                    { 2, "Flour" },
                    { 3, "Milk" },
                    { 4, "Sugar" },
                    { 5, "Butter" },
                    { 6, "Salt" },
                    { 7, "Spaghetti" },
                    { 8, "Minced Beef" },
                    { 9, "Tomato" },
                    { 10, "Olive Oil" },
                    { 11, "Cucumber" },
                    { 12, "Feta Cheese" },
                    { 13, "Dark Chocolate" },
                    { 14, "Canned Tomatoes" },
                    { 15, "Onion" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CookTime", "ImageUrl", "Instructions", "Name", "PrepTime", "Servings" },
                values: new object[,]
                {
                    { 1, 1, 15, null, "Whisk flour, milk, eggs, sugar and salt into a smooth batter. Fry ladlefuls in melted butter until golden on both sides.", "Classic Pancakes", 10, 4 },
                    { 2, 2, 60, null, "Fry the onion in olive oil, brown the beef, add the canned tomatoes and simmer for 45 minutes. Boil the spaghetti and serve with the sauce.", "Spaghetti Bolognese", 15, 4 },
                    { 3, 4, 1, null, "Dice the tomatoes and cucumbers, dress with olive oil and salt, then top with grated feta cheese.", "Shopska Salad", 10, 4 },
                    { 4, 3, 25, null, "Melt the chocolate with the butter, stir in sugar and eggs, fold in the flour. Bake at 180°C for about 25 minutes.", "Chocolate Brownies", 15, 12 }
                });

            migrationBuilder.InsertData(
                table: "RecipesIngredients",
                columns: new[] { "IngredientId", "RecipeId", "MeasurementUnit", "Note", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 14, null, 2m },
                    { 2, 1, 3, null, 200m },
                    { 3, 1, 1, null, 300m },
                    { 4, 1, 6, null, 1m },
                    { 5, 1, 3, "melted", 30m },
                    { 6, 1, 5, null, 1m },
                    { 6, 2, 5, null, 1m },
                    { 7, 2, 3, null, 400m },
                    { 8, 2, 3, null, 500m },
                    { 10, 2, 6, null, 2m },
                    { 14, 2, 1, "400 g, crushed", 1000m },
                    { 15, 2, 14, "finely chopped", 1m },
                    { 6, 3, 5, null, 1m },
                    { 9, 3, 14, "diced", 3m },
                    { 10, 3, 6, null, 2m },
                    { 11, 3, 14, "diced", 2m },
                    { 12, 3, 3, "grated", 150m },
                    { 1, 4, 14, null, 3m },
                    { 2, 4, 3, null, 100m },
                    { 4, 4, 3, null, 150m },
                    { 5, 4, 3, null, 150m },
                    { 13, 4, 3, "chopped", 200m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 14, 2 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
