namespace NzKvoDaQm.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Microsoft.AspNet.Identity.EntityFramework;

    using NzKvoDaQm.Data.Migrations;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;

    public class NzKvoDaQmContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public NzKvoDaQmContext()
            : base("name=NzKvoDaQmContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NzKvoDaQmContext, Configuration>());
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

        public virtual IDbSet<Ingredient> Ingredients
        {
            get; set;
        }

        public virtual IDbSet<IngredientType> IngredientTypes
        {
            get; set;
        }

        public virtual IDbSet<Recipe> Recipes
        {
            get; set;
        }

        public virtual IDbSet<RecipeImage> RecipeImages
        {
            get; set;
        }

        public virtual IDbSet<Review> Reviews
        {
            get; set;
        }
    }
}