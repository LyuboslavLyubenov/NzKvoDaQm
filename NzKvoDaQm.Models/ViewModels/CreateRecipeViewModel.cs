namespace NzKvoDaQm.Models.ViewModels
{

    using System.Web;

    using NzKvoDaQm.Models.EntityModels;

    public class CreateRecipeViewModel
    {
        public string Title { get; set; }

        public string[] Images { get; set; }

        public string[] Ingredients { get; set; }

        public string[] StepsTexts { get; set; }

        public string[] StepsMinutes { get; set; }

        public string MinutesRequiredForCooking { get; set; }
    }
}
