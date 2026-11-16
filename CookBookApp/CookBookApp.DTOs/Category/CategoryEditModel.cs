using System.ComponentModel.DataAnnotations;
using static CookBookApp.GCommon.Constants.ModelValidationConstants.Category;

namespace CookBookApp.DTOs.Category;

public class CategoryEditModel
{
    public int Id { get; set; }

    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;
}
