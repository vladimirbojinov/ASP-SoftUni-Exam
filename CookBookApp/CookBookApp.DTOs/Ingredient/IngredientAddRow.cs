using CookBookApp.GCommon.Enums;
using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Ingredient;

namespace CookBookApp.DTOs.Ingredient;

public class IngredientAddRow
{
    [Required]
    public int Id { get; set; }

    [Range(MinQuantity, MaxQuantity)]
    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    public string? Note { get; set; }
}
