namespace NzKvoDaQm.Services.Interfaces
{

    using NzKvoDaQm.Models.EntityModels;

    public interface IRecipeImagesService : IEntityService<RecipeImage>
    {
        RecipeImage Create(string imageBase64, ApplicationUser author);
    }
}
