namespace NzKvoDaQm.Models.Attributes
{

    using System;
    using System.ComponentModel.DataAnnotations;
    public class MinValueValidatorAttribute : ValidationAttribute
    {
        private readonly int minValue;

        public MinValueValidatorAttribute(int minValue)
        {
            this.minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            dynamic number = value;
            return (number >= this.minValue);
        }
    }
}
