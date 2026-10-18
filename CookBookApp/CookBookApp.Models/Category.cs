namespace CookBookApp.Data.Models;

using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Category;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Recipe> Recipes { get; set; } 
        = new HashSet<Recipe>();
}
