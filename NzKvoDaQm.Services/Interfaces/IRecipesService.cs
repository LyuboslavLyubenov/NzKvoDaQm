namespace NzKvoDaQm.Services.Interfaces
{

    using NzKvoDaQm.Models.BindingModels;
    using NzKvoDaQm.Models.EntityModels;

    public interface IRecipesService : IEntityService<Recipe>
    {
        Recipe Create(CreateRecipeBindingModel bindingModel, ApplicationUser author);
    }
}