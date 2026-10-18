using CookBookApp.CookBookApp.DTOs.Ingredient;
using CookBookApp.DTOs.Category;
using CookBookApp.DTOs.Ingredient;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Recipe;

namespace CookBookApp.DTOs.Recipe;

public class RecipeInputModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(InstructionMaxLength, MinimumLength = InstructionMinLength)]
    public string Instructions { get; set; } = null!;

    [Range(PrepTimeMinRange, PrepTimeMaxRange)]
    public int PrepTime { get; set; }

    [Range(CookTimeMinRange, CookTimeMaxRange)]
    public int CookTime { get; set; }

    [Range(ServingsMinRange, ServingsMaxRange)]
    public int Servings { get; set; }

    [Url]
    [StringLength(ImageUrlMaxLength)]
    public string? ImageUrl { get; set; }

    [ValidateNever]
    public IngredientAddRow NewIngredient { get; set; }
        = new IngredientAddRow();

    public IngredientInputViewModel[] Ingredients { get; set; } = [];

    public IngredientDropDown[] IngredientDropDowns { get; set; } = [];

    [Required]
    public int CategoryId { get; set; }

    public CategoryDropDown[] CategoryDropDown { get; set; } = [];
}
