namespace CookBookApp.DTOs.Ingredient;

public class IngredientDeleteModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RecipesCount { get; set; }
}
