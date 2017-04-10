namespace NzKvoDaQm.Services.SearchConstraints
{
    using NzKvoDaQm.Models.EntityModels;

    public interface ISearchConstraint
    {
        bool IsAllowed(Recipe recipe);
    }
}
