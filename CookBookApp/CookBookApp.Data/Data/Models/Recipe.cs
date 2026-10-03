namespace CookBookApp.Data.Data.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CookBookApp.Data.Common.ModelValidation.Recipe;

public class Recipe
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(InstructionMaxLength)]
    public string Instructions { get; set; } = null!;

    [Required]
    public int PrepTime { get; set; }

    [Required]
    public int CookTime { get; set; }

    [Required]
    public int Servings { get; set; }

    [StringLength(ImageUrlMaxLength)]
    public string? ImageUrl { get; set; }

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Category Category { get; set; } = null!;

    public ICollection<RecipeIngredient> RecipesIngredients { get; set; } 
        = new HashSet<RecipeIngredient>();
}
