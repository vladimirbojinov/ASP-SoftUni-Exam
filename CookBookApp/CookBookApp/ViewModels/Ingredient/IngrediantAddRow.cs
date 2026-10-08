using CookBookApp.Data.Enums;

namespace CookBookApp.Web.ViewModels.Ingredient;

public class IngredientAddRow
{
    public int Id { get; set; }

    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    public string? Note { get; set; }
}
