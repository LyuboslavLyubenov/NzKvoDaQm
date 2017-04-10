namespace NzKvoDaQm.Models.EntityModels
{

    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        public virtual IList<Recipe> Recipes
        {
            get; set;
        }

        public virtual IList<Review> Reviews
        {
            get; set;
        }

        public virtual IList<Ingredient> IngredientsInFridge
        {
            get; set;
        }

        public ApplicationUser()
        {
            this.Recipes = new List<Recipe>();
            this.Reviews = new List<Review>();
            this.IngredientsInFridge = new List<Ingredient>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}