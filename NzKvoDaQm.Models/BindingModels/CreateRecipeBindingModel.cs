namespace NzKvoDaQm.Models.BindingModels
{

    using System.ComponentModel.DataAnnotations;

    using NzKvoDaQm.Models.Attributes;

    public class CreateRecipeBindingModel
    {
        [Required]
        public string Title { get; set; }

        public string[] Images { get; set; }

        [Required]
        [MinLengthCollection(1)]
        public string[] Ingredients { get; set; }

        [Required]
        [MinLengthCollection(1)]
        public string[] StepsTexts { get; set; }

        public int?[] StepsMinutes { get; set; }

        [Required]
        [MinValueValidator(1)]
        public int MinutesRequiredForCooking { get; set; }
    }
}
