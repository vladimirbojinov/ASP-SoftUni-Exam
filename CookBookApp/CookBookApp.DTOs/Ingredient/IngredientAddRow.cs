using CookBookApp.GCommon.Enums;

namespace CookBookApp.DTOs.Ingredient;

public class IngredientAddRow
{
    public int Id { get; set; }

    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    public string? Note { get; set; }
}
