namespace NzKvoDaQm.Models.EntityModels
{
    public class Product
    {
        public int Id
        {
            get; set;
        }

        public ProductType ProductType
        {
            get; set;
        }

        public QuantityMeasurementType QuantityMeasurementType { get; set; }

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