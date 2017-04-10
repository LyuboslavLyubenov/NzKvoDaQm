namespace NzKvoDaQm.Services
{

    using System;
    using System.Linq;

    using NzKvoDaQm.Models.BindingModels;
    using NzKvoDaQm.Models.EntityModels;

    public interface IRecipesService
    {
        Recipe Create(CreateRecipeBindingModel bindingModel);

        bool Delete(Recipe recipe);

        Recipe Get(long id);

        IQueryable<Recipe> Get(Predicate<Recipe> condition);
    }

}