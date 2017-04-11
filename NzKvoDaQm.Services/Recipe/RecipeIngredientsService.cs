namespace NzKvoDaQm.Services.Recipe
{

    using System;
    using System.Data.Entity;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeIngredientsService : EntityService<Ingredient>, IRecipeIngredientsService
    {
        public RecipeIngredientsService(IDbSet<Ingredient> set)
            : base(set)
        {
        }

        public Ingredient Create(IngredientType ingredientType, QuantityMeasurementType quantityMeasurementType, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}