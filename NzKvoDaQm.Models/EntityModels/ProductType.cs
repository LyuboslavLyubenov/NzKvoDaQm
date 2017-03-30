namespace NzKvoDaQm.Models.EntityModels
{

    using System.ComponentModel.DataAnnotations;

    public class ProductType
    {
        public int Id
        {
            get; set;
        }

        [Required]
        [MinLength(4)]
        public string Name
        {
            get; set;
        }
        
        [Required]
        public string ThumbnailUrl
        {
            get; set;
        }
    }
}