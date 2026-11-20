using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Ingredient;

namespace CookBookApp.DTOs.Ingredient;

public class IngredientEditModel
{
    public int Id { get; set; }

    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;
}
