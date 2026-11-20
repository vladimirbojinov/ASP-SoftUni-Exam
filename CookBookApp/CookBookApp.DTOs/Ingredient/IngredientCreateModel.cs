using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Ingredient;

namespace CookBookApp.DTOs.Ingredient;

public class IngredientCreateModel
{
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;
}
