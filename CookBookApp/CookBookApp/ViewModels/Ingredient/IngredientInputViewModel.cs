namespace CookBookApp.Web.ViewModels.Ingredient;

using CookBookApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

public class IngredientInputViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    public string? Note { get; set; }    
}
