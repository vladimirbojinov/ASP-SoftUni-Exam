using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Data;

public class CookBookDbContext(DbContextOptions<CookBookDbContext> options) : IdentityDbContext(options) 
{

}
