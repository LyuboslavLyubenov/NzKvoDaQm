namespace NzKvoDaQm.Services.Interfaces
{
    using System.Collections.Generic;

    using NzKvoDaQm.Models.EntityModels;

    public interface ISearchService
    {
        IList<Recipe> GetRecipes(string query);
    }
}
