namespace NzKvoDaQm.Services.Recipe
{
    using System.Data.Entity;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class IngredientTypesService : EntityService<IngredientType>, IIngredientTypesService
    {
        public IngredientTypesService(IDbSet<IngredientType> set)
            : base(set)
        {
        }

        public IngredientType Create(string name, string thumbnailUrl = null)
        {
            throw new System.NotImplementedException();
        }
    }
}