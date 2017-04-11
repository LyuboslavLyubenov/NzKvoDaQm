namespace NzKvoDaQm.Services.Recipe
{

    using System;
    using System.Data.Entity;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeStepService : EntityService<RecipeStep>, IRecipeStepService
    {
        public RecipeStepService(IDbSet<RecipeStep> set, IDbContext context)
            : base(set, context)
        {
        }

        public RecipeStep Create(string text, int? timeToFinishInMinutes)
        {
            throw new NotImplementedException();
        }
    }

}