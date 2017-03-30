namespace NzKvoDaQm.Models.Attributes
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;

    internal class ValidateStepsForRecipeAttribute : ValidationAttribute
    {
        private readonly int minSteps;

        public ValidateStepsForRecipeAttribute(int minSteps)
        {
            if (minSteps <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minSteps));
            }

            this.minSteps = minSteps;
        }

        public override bool IsValid(object value)
        {
            var recipeSteps = (IList<RecipeStep>)value;
            return recipeSteps != null && recipeSteps.Count(s => s != null) >= this.minSteps;
        }
    }
}
