using Microsoft.AspNetCore.Identity;

namespace CookBookApp.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Recipe> Recipes { get; set; } 
        = new List<Recipe>();
}
