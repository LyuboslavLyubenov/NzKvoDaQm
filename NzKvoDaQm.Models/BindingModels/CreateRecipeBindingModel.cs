namespace NzKvoDaQm.Models.BindingModels
{
    using NzKvoDaQm.Models.EntityModels;

    public class CreateRecipeBindingModel
    {
        public string Title { get; set; }

        public string[] Images { get; set; }
        
        public string[] IngredientsNames { get; set; }

        public QuantityMeasurementType[] IngredientsMeasurementTypes { get; set; }

        public int[] IngredientsQuantities { get; set; }

        public string[] StepsTexts { get; set; }

        public int?[] StepsMinutes { get; set; }
        
        public int MinutesRequiredForCooking { get; set; }
    }
}
