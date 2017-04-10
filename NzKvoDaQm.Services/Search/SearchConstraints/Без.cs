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
            if (string.IsNullOrWhiteSpace(bannedWords))
            {
                throw new ArgumentException();
            }

            this.bannedWords = bannedWords.Split(
                new char[]
                {
                    ' ',
                    ','
                },
                StringSplitOptions.RemoveEmptyEntries)
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w.Trim().ToUpper())
                .ToList();

            if (this.bannedWords.Count == 0)
            {
                throw new ArgumentException("Must contain words");
            }
        }
        
        public bool IsAllowed(Recipe recipe)
        {
            var recipeTitleUpper = recipe.Title.ToUpper();
            return this.bannedWords.All(w => !recipeTitleUpper.Contains(w));
        }
    }
}
