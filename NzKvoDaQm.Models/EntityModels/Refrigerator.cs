namespace NzKvoDaQm.Models.EntityModels
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using NzKvoDaQm.Models.ViewModels;

    public class Refrigerator
    {
        public int Id
        {
            get; set;
        }
        
        public virtual IList<Product> Products
        {
            get; set;
        }
    }
}
