namespace NzKvoDaQm.Services.Interfaces
{

    using NzKvoDaQm.Models.EntityModels;

    public interface IRecipeStepService : IEntityService<RecipeStep>
    {
        RecipeStep Create(string text, int? timeToFinishInMinutes);
    }

}