using CookBookApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBookApp.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private static readonly List<Category> Categories =
    [
        new Category { Id = 1, Name = "Breakfast" },
        new Category { Id = 2, Name = "Main Course" },
        new Category { Id = 3, Name = "Dessert" },
        new Category { Id = 4, Name = "Salad" }
    ];

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(Categories);
    }
}
