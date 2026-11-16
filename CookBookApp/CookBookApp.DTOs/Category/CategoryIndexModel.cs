namespace CookBookApp.DTOs.Category;

public class CategoryIndexModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RecipesCount { get; set; }
}
