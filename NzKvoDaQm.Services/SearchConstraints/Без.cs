namespace NzKvoDaQm.Services.SearchConstraints
{

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;

    public class Без : ISearchConstraint
    {
        private readonly IList<string> bannedWords;

        public Без(string bannedWords)
        {
            this.bannedWords = bannedWords.Split(
                new char[]
                {
                    ' ',
                    ','
                },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim().ToUpper())
                .ToList();
        }
        
        public bool IsAllowed(Recipe recipe)
        {
            var recipeTitleUpper = recipe.Title.ToUpper();
            return this.bannedWords.All(w => !recipeTitleUpper.Contains(w));
        }
    }
}
