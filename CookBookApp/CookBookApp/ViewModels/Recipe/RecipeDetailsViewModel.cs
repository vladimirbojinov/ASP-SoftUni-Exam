namespace CookBookApp.Web.ViewModels.Recipe;

using CookBookApp.Web.ViewModels.Ingredient;

public class RecipeDetailsViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Instructions { get; set; } = null!;

    public int PrepTime { get; set; }

    public int CookTime { get; set; }

    public int TotalTime { get; set; }

    public int Servings { get; set; }

    public List<IngredientDetailsViewModel> Ingredients { get; set; } 
        = new List<IngredientDetailsViewModel>();
}
