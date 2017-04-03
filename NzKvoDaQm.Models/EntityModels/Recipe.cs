namespace NzKvoDaQm.Models.EntityModels
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NzKvoDaQm.Models.ViewModels;

    public class Recipe
    {
        public int Id
        {
            get; set;
        }

        [Required]
        [MinLength(4)]
        public string Title
        {
            get; set;
        }

        [Required]
        public virtual IList<RecipeImage> Images
        {
            get; set;
        }

        [Required]
        public virtual IList<RecipeStep> Steps
        {
            get; set;
        }

        [Range(5, int.MaxValue)]
        public int MinutesRequiredToCook
        {
            get; set;
        }

        public virtual IList<Review> Reviews
        {
            get; set;
        }

        [InverseProperty("Recipes")]
        public virtual ApplicationUser Author
        {
            get; set;
        }

        [Required]
        public virtual IList<Ingredient> Ingredients
        {
            get; set;
        }

        public Recipe()
        {
            this.Steps = new List<RecipeStep>();
            this.Reviews = new List<Review>();
            this.Ingredients = new List<Ingredient>();
            this.Images = new List<RecipeImage>();
        }
    }

}
