namespace NzKvoDaQm.Models.EntityModels
{

    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id
        {
            get; set;
        }

        [Required]
        public ProductType ProductType
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
        Kilogram,
        Cup,
        Pound
    }

}