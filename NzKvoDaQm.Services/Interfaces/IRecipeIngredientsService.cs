namespace NzKvoDaQm.Services.Interfaces
{

    using NzKvoDaQm.Models.EntityModels;

    public interface IRecipeIngredientsService : IEntityService<Ingredient>
    {
        Ingredient Create(IngredientType ingredientType, QuantityMeasurementType quantityMeasurementType, int quantity);
    }
}