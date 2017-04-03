namespace NzKvoDaQm.Services
{
    using System.Collections.Generic;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class SearchService : Service, ISearchService
    {
        public SearchService() : base()
        {
        }

        public SearchService(IDbContext context) : base(context)
        {
        }
        
        public IList<Recipe> GetRecipes(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new Recipe[0];
            }
            
            var searchQuery = new SearchQuery(this.Context, query);
            return searchQuery.GetResults();
        }
    }
}