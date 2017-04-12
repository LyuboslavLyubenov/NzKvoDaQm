namespace NzKvoDaQm.Services.Recipe
{

    using System;
    using System.Data.Entity;
    using System.Text.RegularExpressions;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeStepsService : EntityService<RecipeStep>, IRecipeStepsService
    {
        public RecipeStepsService(IDbSet<RecipeStep> set, IDbContext context)
            : base(set, context)
        {
        }

        public RecipeStep Create(string text, int? timeToFinishInMinutes)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (!Regex.IsMatch(text, "[а-яА-Яa-zA-Z]"))
            {
                throw new ArgumentException("Text must contain words");
            }

            if (timeToFinishInMinutes != null && timeToFinishInMinutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeToFinishInMinutes));
            }

            var step = new RecipeStep()
                       {
                           Text = text,
                           TimeToFinishInMinutes = timeToFinishInMinutes
                       };
            this.Set.Add(step);
            this.SaveChanges();

            return step;
        }
    }
}