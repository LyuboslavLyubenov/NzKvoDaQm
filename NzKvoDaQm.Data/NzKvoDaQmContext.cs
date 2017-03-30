namespace NzKvoDaQm.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;

    public class NzKvoDaQmContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public NzKvoDaQmContext()
            : base("name=NzKvoDaQmContext")
        {
        }

        public static NzKvoDaQmContext Create()
        {
            return new NzKvoDaQmContext();
        }

        public virtual IDbSet<RecipeStep> RecipeSteps
        {
            get; set;
        }

        public virtual IDbSet<Comment> Comments
        {
            get; set;
        }

        public virtual IDbSet<Product> Products
        {
            get; set;
        }

        public virtual IDbSet<ProductType> ProductTypes
        {
            get; set;
        }

        public virtual IDbSet<Recipe> Recipes
        {
            get; set;
        }
        
        public virtual IDbSet<Review> Reviews
        {
            get; set;
        }
    }
}