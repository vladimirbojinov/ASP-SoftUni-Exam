namespace CookBookApp.Data.Data.Models;

using System.ComponentModel.DataAnnotations;
using static CookBookApp.Data.Common.ModelValidation.Ingredient;

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
