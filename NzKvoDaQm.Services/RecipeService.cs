namespace NzKvoDaQm.Services
{
    using System;
    using System.Linq;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.BindingModels;
    using NzKvoDaQm.Models.EntityModels;

    public class RecipeService : IRecipesService
    {
        public Recipe Create(CreateRecipeBindingModel bindingModel)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Recipe Get(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Recipe> Get(Predicate<Recipe> condition)
        {
            throw new NotImplementedException();
        }
    }

}