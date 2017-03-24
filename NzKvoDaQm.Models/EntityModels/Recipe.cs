namespace NzKvoDaQm.Models.EntityModels
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using NzKvoDaQm.Models.ViewModels;

    public class Recipe
    {
        public int Id
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public virtual string[] ImagesUrls
        {
            get; set;
        }

        public virtual IList<string> Steps
        {
            get; set;
        }

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


        public virtual Refrigerator MyRefrigerator
        {
            get; set;
        }
    }

}
