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

        public override int GetHashCode()
        {
            return new
                   {
                       Id,
                       IngredientType,
                       QuantityMeasurementType,
                       Quantity
                   }.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(Ingredient))
            {
                var ingredient = (Ingredient)obj;
                return this.IngredientType == ingredient.IngredientType
                       && this.QuantityMeasurementType == ingredient.QuantityMeasurementType
                       && this.Quantity == ingredient.Quantity;
            }

            return base.Equals(obj);
        }
    }

    public enum QuantityMeasurementType
    {
        Liters,
        Milliliters,
        Tablespoons,
        Dessertspoons,
        Grams,
        Kilograms
    }
}