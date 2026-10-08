namespace CookBookApp.Web.ViewModels.Recipe;

public class RecipeIndexViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public int PrepTime { get; set; }

    public int CookTime { get; set; }

    public int Servings { get; set; }

    public int IngredientCount { get; set; }

    public string CategoryName { get; set; } = null!;
}
