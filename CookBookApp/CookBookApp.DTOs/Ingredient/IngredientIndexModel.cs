namespace CookBookApp.DTOs.Ingredient;

public class IngredientIndexModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RecipesCount { get; set; }
}
