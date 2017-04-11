namespace NzKvoDaQm.Services.Recipe
{

    using System;
    using System.Data.Entity;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeIngredientsService : EntityService<Ingredient>, IRecipeIngredientsService
    {
        public RecipeIngredientsService(IDbSet<Ingredient> set, IDbContext context)
            : base(set, context)
        {
        }

        public Ingredient Create(IngredientType ingredientType, QuantityMeasurementType quantityMeasurementType, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}