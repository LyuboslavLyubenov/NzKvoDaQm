namespace NzKvoDaQm.Data
{

    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Microsoft.AspNet.Identity.EntityFramework;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;

    public interface IDbContext : IDisposable
    {
        IDbSet<RecipeStep> RecipeSteps
        {
            get; set;
        }

        IDbSet<Comment> Comments
        {
            get; set;
        }

        IDbSet<Ingredient> Ingredients
        {
            get; set;
        }

        IDbSet<IngredientType> ProductTypes
        {
            get; set;
        }

        IDbSet<Recipe> Recipes
        {
            get; set;
        }

        IDbSet<RecipeImage> RecipeImages
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

        DbEntityEntry Entry(object entry);

        int SaveChanges();
    }
}
