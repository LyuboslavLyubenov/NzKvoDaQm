namespace NzKvoDaQm.Services.Recipe
{

    using System.Data.Entity;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeImagesService : EntityService<RecipeImage>, IRecipeImagesService
    {
        public RecipeImagesService(IDbSet<RecipeImage> set)
            : base(set)
        {
        }

        public RecipeImage Create(string imageBase64, ApplicationUser author)
        {
            throw new System.NotImplementedException();
        }
    }
}
