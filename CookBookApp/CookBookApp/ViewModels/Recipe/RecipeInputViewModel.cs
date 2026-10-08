namespace CookBookApp.Web.ViewModels.Recipe;

using CookBookApp.Web.ViewModels.Category;
using CookBookApp.Web.ViewModels.Ingredient;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using static CookBookApp.Data.Common.ModelValidation.Recipe;

public class RecipeInputViewModel
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

    public List<IngredientInputViewModel> Ingredients { get; set; }
        = new List<IngredientInputViewModel>();

    public List<IngredientDropDown> IngredientDropDowns { get; set; }
        = new List<IngredientDropDown>();

    [Required]
    public int CategoryId { get; set; }

    public List<CategoryDropDown> CategoryDropDown { get; set; }
        = new List<CategoryDropDown>();
}
