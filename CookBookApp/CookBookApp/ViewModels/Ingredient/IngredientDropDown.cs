namespace CookBookApp.Web.ViewModels.Ingredient;

using CookBookApp.Data.Enums;

public class IngredientDropDown
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    public string? Note { get; set; }
}