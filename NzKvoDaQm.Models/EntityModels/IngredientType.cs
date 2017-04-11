namespace NzKvoDaQm.Models.EntityModels
{

    using System.ComponentModel.DataAnnotations;

    public class IngredientType
    {
        public long Id
        {
            get; set;
        }

        [Required]
        public string Name
        {
            get; set;
        }
        
        public string ThumbnailUrl
        {
            get; set;
        }
    }
}