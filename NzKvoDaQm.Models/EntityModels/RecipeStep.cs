namespace NzKvoDaQm.Models.EntityModels
{

    using System.ComponentModel.DataAnnotations;

    public class RecipeStep
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Text { get; set; }
        
        public int? TimeToFinishInMinutes { get; set; }
    }
}
