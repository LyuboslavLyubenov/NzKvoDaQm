namespace NzKvoDaQm.Services.Search.SearchConstraints
{

    using System;

    using NzKvoDaQm.Models.EntityModels;

    public class От : ISearchConstraint
    {
        private readonly string authorUsername;

        /// <summary>
        /// 
        /// </summary>
        public От(string authorUsername)
        {
            if (string.IsNullOrWhiteSpace(authorUsername))
            {
                throw new ArgumentNullException("authorUsername");
            }

            this.authorUsername = authorUsername.Trim();
        }

        public bool IsAllowed(Recipe recipe)
        {
            return recipe.Author.UserName == this.authorUsername;
        }
    }
}
