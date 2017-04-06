namespace NzKvoDaQm.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;

    public interface ISearchService
    {
        IQueryable<Recipe> GetRecipes(string query);
    }
}
