namespace NzKvoDaQm.Services.Interfaces
{

    using NzKvoDaQm.Models.EntityModels;

    public interface IIngredientTypesService : IEntityService<IngredientType>
    {
        IngredientType Create(string name, string thumbnailUrl = null);
    }
}