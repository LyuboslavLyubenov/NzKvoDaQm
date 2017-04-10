namespace NzKvoDaQm.Services.SearchConstraints
{
    using System;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;

    public class Със : ISearchConstraint
    {
        private string[] ingredientsToUpperNames;

        /// <summary>
        /// Ограничава какви продукти да включват рецептите
        /// </summary>
        /// <param name="ingredients"></param>
        public Със(string ingredients)
        {
            if (string.IsNullOrWhiteSpace(ingredients))
            {
                throw new ArgumentNullException();
            }

            this.ingredientsToUpperNames = ingredients.Split(
                new char[]
                {
                    ' ',
                    ','
                },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(
                    i => i.Trim()
                             .ToUpper())
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .ToArray();

            if (this.ingredientsToUpperNames.Length == 0)
            {
                throw new ArgumentNullException();
            }
        }

        public bool IsAllowed(Recipe recipe)
        {
            var recipeIngredientsNamesToUpper = recipe.Ingredients.Select(i => i.IngredientType.Name.ToUpper());
            return recipeIngredientsNamesToUpper.Any(i => this.ingredientsToUpperNames.Contains(i));
        }
    }
}
