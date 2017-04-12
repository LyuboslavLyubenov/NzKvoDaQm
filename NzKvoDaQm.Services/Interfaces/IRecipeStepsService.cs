namespace NzKvoDaQm.Services.Interfaces
{

    using NzKvoDaQm.Models.EntityModels;

    public interface IRecipeStepsService : IEntityService<RecipeStep>
    {
        RecipeStep Create(string text, int? timeToFinishInMinutes);
    }

}