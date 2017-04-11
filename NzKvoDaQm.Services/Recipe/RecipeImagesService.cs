namespace NzKvoDaQm.Services.Recipe
{

    using System.Data.Entity;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeImagesService : EntityService<RecipeImage>, IRecipeImagesService
    {
        public RecipeImagesService(IDbSet<RecipeImage> set, IDbContext context)
            : base(set, context)
        {
        }

        public RecipeImage Create(string imageBase64, ApplicationUser author)
        {
            throw new System.NotImplementedException();
        }
    }
}
