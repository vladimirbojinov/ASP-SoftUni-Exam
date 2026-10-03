namespace CookBookApp.Data.Common;

public static class ModelValidation
{
    public static class Category
    {
        public const int NameMinLength = 1;
        public const int NameMaxLength = 150;
    }

    public static class Ingredient
    {
        public const int NameMinLength = 1;
        public const int NameMaxLength = 150;
    }

    public static class Recipe
    {
        public const int NameMinLength = 1;
        public const int NameMaxLength = 150;

        public const int InstructionMinLength = 10;
        public const int InstructionMaxLength = 4000;

        public const int PrepTimeMinRange = 1;
        public const int PrepTimeMaxRange = 1440;

        public const int CookTimeMinRange = 1;
        public const int CookTimeMaxRange = 1440;

        public const int ServingsMinRange = 1;
        public const int ServingsMaxRange = 500;

        public const int ImageUrlMaxLength = 2048;
    }

    public static class RecipeIngredient
    {
        public const string QuantityType = "DECIMAL(10, 3)";
        public const decimal QuantityMinRange = 0.001m;
        public const decimal QuantityMaxRange = 100000m;

        public const int NoteMinLength = 10;
        public const int NoteMaxLength = 200;
    }
}
