namespace NzKvoDaQm.Services
{

    using NzKvoDaQm.Models.EntityModels;

    public interface ISearchQuery
    {
        Recipe[] GetResults();
    }

}