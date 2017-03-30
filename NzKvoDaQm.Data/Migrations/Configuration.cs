namespace NzKvoDaQm.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;

    internal sealed class Configuration : DbMigrationsConfiguration<NzKvoDaQm.Data.NzKvoDaQmContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "NzKvoDaQm.Data.NzKvoDaQmContext";
        }

        private void AddProductTypesToDatabase(IDbContext context)
        {
            var productTypes = new ProductType[]
                               {
                                   new ProductType()
                                   {
                                       Name = "��������",
                                       ThumbnailUrl =
                                           "http://vignette3.wikia.nocookie.net/farmville2/images/5/5b/Swiss_Cheese.png/revision/latest?cb=20121212041608"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�������",
                                       ThumbnailUrl =
                                           "https://hydra-media.cursecdn.com/ark.gamepedia.com/thumb/c/cd/Cooked_Meat.png/128px-Cooked_Meat.png?version=5c2a063f4bf7cdfd7b7f6ba9ec1c5de7"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�����",
                                       ThumbnailUrl = "http://www.dwibhashi.org/object_images/10030.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "������",
                                       ThumbnailUrl =
                                           "http://static.wixstatic.com/media/9120ab_8bf52939058941038208fefa27c5ec8b.png_256"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�������",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/5326690dc566d737c5001936-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�������",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/4ef2825bc566d718c5000563-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "���",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/5386898ac566d76d200135aa-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�����",
                                       ThumbnailUrl = "http://imagespng.com/Data/Logo/garlic_PNG12804.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�����",
                                       ThumbnailUrl =
                                           "http://www.emoji.co.uk/files/apple-emojis/food-drink-ios/369-hot-pepper.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�������",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/55bffe45777a426ca40228fc-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "����",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/519319f1c566d746a707f37f-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "����",
                                       ThumbnailUrl = "http://www.swissbeveragefood.com/ressources/img/icons/oil.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�����",
                                       ThumbnailUrl =
                                           "https://hydra-media.cursecdn.com/h1z1.gamepedia.com/8/80/Icon_Sugar.png?version=e23b7a3c3868053eb7cda50d28628774"
                                   },
                                   new ProductType()
                                   {
                                       Name = "�������",
                                       ThumbnailUrl =
                                       "https://dtgxwmigmg3gc.cloudfront.net/files/55b6e0f152ba0b75870002f5-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "������",
                                       ThumbnailUrl = "https://dtgxwmigmg3gc.cloudfront.net/files/54372113e1272f6e820006dc-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "����� �����",
                                       ThumbnailUrl = "http://www.naturalniprodukti.com/uploads/com_article/gallery/thumbs/500x500_6e876050eff76c7be2acc0e375dafb5d1898c2af.jpg"
                                   },
                                   new ProductType()
                                   {
                                       Name = "����",
                                       ThumbnailUrl = "http://vignette2.wikia.nocookie.net/play-rust/images/f/fc/Small_Water_Bottle_icon.png/revision/20161117220421"
                                   },
                                   new ProductType()
                                   {
                                       Name = "������� ���",
                                       ThumbnailUrl = "http://images.bigoven.com/image/upload/t_recipe-256/roasted-balsamic-tomato-sauce-cce951.jpg"
                                   }, 
                               };
            context.ProductTypes.AddOrUpdate(productTypes);
            context.SaveChanges();
        }

        private void AddRecipeForSpaghettiWithTomatoSauceToDatabase(IDbContext context)
        {
            var spaghetti = new Product()
            {
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "�������".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 500
            };

            var onions = new Product()
            {
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "���".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 100
            };

            var garlic = new Product()
            {
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "�����".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 20
            };
            var tomato = new Product()
            {
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "�����".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 500
            };
            var tomatoSauce = new Product()
            {
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "������� ���".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Milliliter,
                Quantity = 300
            };
            var water = new Product()
            {
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "����".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Liter,
                Quantity = 2
            };

            var products = new[]
                           {
                               spaghetti,
                               onions,
                               garlic,
                               tomato,
                               tomatoSauce,
                               water
                           };
            var steps = new RecipeStep[]
                        {
                            new RecipeStep()
                            {
                                Text = "����� ���������. ���� ���� �� ������ �� �������� � �� ������� ����� ����",
                                TimeToFinishInMinutes = 15
                            },
                            new RecipeStep()
                            {
                                Text =
                                    "������� ����, ������� � ������� ������� � �������� ������ �� �����. ������� ������ �� �� �������� � �����.",
                                TimeToFinishInMinutes = 5
                            },
                            new RecipeStep()
                            {
                                Text =
                                    "������� ��� 200 ��. ���� ���������� ���� � ���������� ������, ����������� �� ����� � ������� ������ ��� ���������� ���������.",
                            },
                            new RecipeStep()
                            {
                                Text = "�������� ���� �� ����� �� ��� ����",
                                TimeToFinishInMinutes = 20
                            },
                            new RecipeStep()
                            {
                                Text = "�������� ���, ����� ����� � ����� ����� �� ����."
                            }
                        };

            var review = new Review()
            {
                Title = "������������",
                Body = "��� ��� �� � ����� ������� ;))))))))))))))",
                Rating = RatingType.InLove,
                Author = context.Users.First()
            };

            var recipe = new Recipe()
            {
                Title = "������� � ������� ���",
                ImagesUrls =
                    new[]
                        {
                            "http://recepti.gotvach.bg/files/lib/600x350/spageti31.jpg",
                            "http://gotvach.bg/files/recipes/photos/p_20160204_085608_12009.jpg"
                        },
                Products = products,
                Steps = steps,
                Reviews =
                    new List<Review>()
                        {
                            review,
                        },
                Author = context.Users.ToList().Skip(1).First(),
                MinutesRequiredToCook = 50
            };

            context.Products.AddOrUpdate(products);
            context.RecipeSteps.AddOrUpdate(steps);
            context.Reviews.AddOrUpdate(review);
            context.Recipes.AddOrUpdate(recipe);
            context.SaveChanges();
        }
        
        protected override void Seed(NzKvoDaQmContext context)
        {
            this.AddProductTypesToDatabase(context);
            this.AddRecipeForSpaghettiWithTomatoSauceToDatabase(context);
        }
    }
}
