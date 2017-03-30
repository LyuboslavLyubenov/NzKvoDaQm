namespace NzKvoDaQm.Models.EntityModels
{
    using System.Collections.Generic;
    
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
