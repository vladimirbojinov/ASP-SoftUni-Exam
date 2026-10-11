using CookBookApp.Data.Enums;

namespace CookBookApp.CookBookApp.DTOs.Ingredient;

public class IngredientDropDown
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    public string? Note { get; set; }
}