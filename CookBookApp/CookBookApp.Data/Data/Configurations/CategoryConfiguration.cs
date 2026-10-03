namespace CookBookApp.Data.Data.Configurations;

using CookBookApp.Data.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    List<Category> _categories;

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(_categories);
    }
}
