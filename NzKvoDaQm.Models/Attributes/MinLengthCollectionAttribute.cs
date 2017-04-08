namespace NzKvoDaQm.Models.Attributes
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MinLengthCollectionAttribute : ValidationAttribute
    {
        private readonly int minLength;

        public MinLengthCollectionAttribute(int minLength)
        {
            if (minLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength));
            }

            this.minLength = minLength;
        }

        public override bool IsValid(object value)
        {
            var collection = (ICollection)value;
            return (collection != null && collection.Count >= this.minLength);
        }
    }
}
