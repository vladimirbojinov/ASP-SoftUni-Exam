namespace CookBookApp.Data.Configurations;

using CookBookApp.Data.Enums;
using CookBookApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    private static List<RecipeIngredient> RecipesIngredients =>
    [
        // 1 - Classic Pancakes
        new RecipeIngredient { RecipeId = 1, IngredientId = 2,  Quantity = 200m,  MeasurementUnit = MeasurementUnit.Gram,       Note = null,        },
        new RecipeIngredient { RecipeId = 1, IngredientId = 3,  Quantity = 300m,  MeasurementUnit = MeasurementUnit.Milliliter, Note = null,        },
        new RecipeIngredient { RecipeId = 1, IngredientId = 1,  Quantity = 2m,    MeasurementUnit = MeasurementUnit.None,      Note = null,        },
        new RecipeIngredient { RecipeId = 1, IngredientId = 4,  Quantity = 1m,    MeasurementUnit = MeasurementUnit.Tablespoon, Note = null,        },
        new RecipeIngredient { RecipeId = 1, IngredientId = 5,  Quantity = 30m,   MeasurementUnit = MeasurementUnit.Gram,       Note = "melted",    },
        new RecipeIngredient { RecipeId = 1, IngredientId = 6,  Quantity = 1m,    MeasurementUnit = MeasurementUnit.Teaspoon,      Note = null,    },
 
        // 2 - Spaghetti Bolognese
        new RecipeIngredient { RecipeId = 2, IngredientId = 7,  Quantity = 400m,  MeasurementUnit = MeasurementUnit.Gram,       Note = null,             },
        new RecipeIngredient { RecipeId = 2, IngredientId = 8,  Quantity = 500m,  MeasurementUnit = MeasurementUnit.Gram,       Note = null,             },
        new RecipeIngredient { RecipeId = 2, IngredientId = 15, Quantity = 1m,    MeasurementUnit = MeasurementUnit.None,      Note = "finely chopped",  },
        new RecipeIngredient { RecipeId = 2, IngredientId = 14, Quantity = 1000m,    MeasurementUnit = MeasurementUnit.Milliliter,        Note = "400 g, crushed",   },
        new RecipeIngredient { RecipeId = 2, IngredientId = 10, Quantity = 2m,    MeasurementUnit = MeasurementUnit.Tablespoon, Note = null,              },
        new RecipeIngredient { RecipeId = 2, IngredientId = 6,  Quantity = 1m,    MeasurementUnit = MeasurementUnit.Teaspoon,   Note = null,             },
 
        // 3 - Shopska Salad
        new RecipeIngredient { RecipeId = 3, IngredientId = 9,  Quantity = 3m,    MeasurementUnit = MeasurementUnit.None,      Note = "diced",     },
        new RecipeIngredient { RecipeId = 3, IngredientId = 11, Quantity = 2m,    MeasurementUnit = MeasurementUnit.None,      Note = "diced",    },
        new RecipeIngredient { RecipeId = 3, IngredientId = 12, Quantity = 150m,  MeasurementUnit = MeasurementUnit.Gram,       Note = "grated",     },
        new RecipeIngredient { RecipeId = 3, IngredientId = 10, Quantity = 2m,    MeasurementUnit = MeasurementUnit.Tablespoon, Note = null,         },
        new RecipeIngredient { RecipeId = 3, IngredientId = 6,  Quantity = 1m,    MeasurementUnit = MeasurementUnit.Teaspoon,      Note = null,         },
 
        // 4 - Chocolate Brownies
        new RecipeIngredient { RecipeId = 4, IngredientId = 13, Quantity = 200m,  MeasurementUnit = MeasurementUnit.Gram,       Note = "chopped",  },
        new RecipeIngredient { RecipeId = 4, IngredientId = 5,  Quantity = 150m,  MeasurementUnit = MeasurementUnit.Gram,       Note = null,        },
        new RecipeIngredient { RecipeId = 4, IngredientId = 4,  Quantity = 150m,  MeasurementUnit = MeasurementUnit.Gram,       Note = null,        },
        new RecipeIngredient { RecipeId = 4, IngredientId = 1,  Quantity = 3m,    MeasurementUnit = MeasurementUnit.None,      Note = null,       },
        new RecipeIngredient { RecipeId = 4, IngredientId = 2,  Quantity = 100m,  MeasurementUnit = MeasurementUnit.Gram,       Note = null,        }
    ];

    public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.HasData(RecipesIngredients);
    }
}
