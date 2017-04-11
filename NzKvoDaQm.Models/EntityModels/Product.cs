namespace NzKvoDaQm.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Ingredient
    {
        public long Id
        {
            get; set;
        }

        [Required]
        public virtual IngredientType IngredientType
        {
            get; set;
        }

        [Required]
        public QuantityMeasurementType QuantityMeasurementType { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }

    public enum QuantityMeasurementType
    {
        Liter,
        Milliliter,
        Tablespoon,
        Dessertspoon,
        Gram,
        Kilogram
    }
}