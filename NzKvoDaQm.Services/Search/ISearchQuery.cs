namespace NzKvoDaQm.Services
{

    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;

    public interface ISearchQuery
    {
        IQueryable<Recipe> GetResults();
    }

}