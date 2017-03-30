namespace NzKvoDaQm.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<NzKvoDaQm.Data.NzKvoDaQmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NzKvoDaQm.Data.NzKvoDaQmContext";
        }

        protected override void Seed(NzKvoDaQm.Data.NzKvoDaQmContext context)
        {
            var productTypes = new ProductType[]
                               {
                                   new ProductType(), 
                               };

            context.ProductTypes.AddOrUpdate();
        }
    }
}
