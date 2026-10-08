namespace CookBookApp.Web.ViewModels.Ingredient;

public class IngredientDetailsViewModel
{
    public string Name { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public string? MeasurementUnit { get; set; }

    public string? Note { get; set; }
}
