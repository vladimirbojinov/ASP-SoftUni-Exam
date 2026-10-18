using CookBookApp.GCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace CookBookApp.CookBookApp.DTOs.Ingredient;

public class IngredientInputViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    public string? Note { get; set; }    
}
