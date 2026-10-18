namespace CookBookApp.Data.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Ingredient;

[Index(nameof(Name), IsUnique = true)]
public class Ingredient
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<RecipeIngredient> RecipesIngredients { get; set; }
        = new HashSet<RecipeIngredient>();
}
