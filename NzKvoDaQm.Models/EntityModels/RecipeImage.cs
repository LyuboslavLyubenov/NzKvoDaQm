namespace NzKvoDaQm.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class RecipeImage
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(3 * 1024 * 1024)]
        public string Url { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }
    }
}
