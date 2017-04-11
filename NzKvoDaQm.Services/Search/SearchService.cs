namespace NzKvoDaQm.Services.Search
{

    using System.Linq;

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
        
        public IQueryable<Recipe> GetRecipes(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return this.Context.Recipes;
            }
            
            var searchQuery = new SearchQuery(this.Context, query);
            return searchQuery.GetResults();
        }
    }
}