namespace NzKvoDaQm.Data
{

    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;

    public interface IDbContext
    {
        IDbSet<Comment> Comments
        {
            get; set;
        }

        IDbSet<Product> Products
        {
            get; set;
        }

        IDbSet<ProductType> ProductTypes
        {
            get; set;
        }

        IDbSet<Recipe> Recipes
        {
            get; set;
        }

        IDbSet<Refrigerator> Refrigerators
        {
            get; set;
        }

        IDbSet<Review> Reviews
        {
            get; set;
        }

        IDbSet<IdentityRole> Roles
        {
            get; set;
        }

        IDbSet<ApplicationUser> Users
        {
            get; set;
        }
    }
}
