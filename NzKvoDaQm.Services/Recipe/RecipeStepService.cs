namespace NzKvoDaQm.Services.Recipe
{

    using System;
    using System.Data.Entity;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeStepService : EntityService<RecipeStep>, IRecipeStepService
    {
        public RecipeStepService(IDbSet<RecipeStep> set)
            : base(set)
        {
        }

        public RecipeStep Create(string text, int? timeToFinishInMinutes)
        {
            throw new NotImplementedException();
        }
    }

}