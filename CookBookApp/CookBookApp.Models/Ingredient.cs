using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Ingredient;

namespace CookBookApp.Models;

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
