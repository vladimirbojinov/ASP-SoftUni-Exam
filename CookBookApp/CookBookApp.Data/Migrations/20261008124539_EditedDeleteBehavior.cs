using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBookApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipesIngredients_Ingredients_IngredientId",
                table: "RecipesIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesIngredients_Recipes_RecipeId",
                table: "RecipesIngredients");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesIngredients_Ingredients_IngredientId",
                table: "RecipesIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesIngredients_Recipes_RecipeId",
                table: "RecipesIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipesIngredients_Ingredients_IngredientId",
                table: "RecipesIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesIngredients_Recipes_RecipeId",
                table: "RecipesIngredients");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesIngredients_Ingredients_IngredientId",
                table: "RecipesIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesIngredients_Recipes_RecipeId",
                table: "RecipesIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
