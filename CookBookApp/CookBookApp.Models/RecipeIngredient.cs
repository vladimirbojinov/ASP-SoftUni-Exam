namespace CookBookApp.Data.Models;

using CookBookApp.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CookBookApp.Data.Common.ModelValidation.RecipeIngredient;

[PrimaryKey(nameof(RecipeId), nameof(IngredientId))]
public class RecipeIngredient
{
    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    [Column(TypeName = QuantityType)]
    public decimal Quantity { get; set; }

    public MeasurementUnit MeasurementUnit { get; set; }

    [StringLength(NoteMaxLength)]
    public string? Note { get; set; }

    public Recipe Recipe { get; set; } = null!;

    public Ingredient Ingredient { get; set; } = null!;
}
