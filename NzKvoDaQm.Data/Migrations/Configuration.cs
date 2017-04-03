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
                                       Name = "Кашкавал",
                                       ThumbnailUrl =
                                           "http://vignette3.wikia.nocookie.net/farmville2/images/5/5b/Swiss_Cheese.png/revision/latest?cb=20121212041608"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Свинско",
                                       ThumbnailUrl =
                                           "https://hydra-media.cursecdn.com/ark.gamepedia.com/thumb/c/cd/Cooked_Meat.png/128px-Cooked_Meat.png?version=5c2a063f4bf7cdfd7b7f6ba9ec1c5de7"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Домат",
                                       ThumbnailUrl = "http://www.dwibhashi.org/object_images/10030.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Картоф",
                                       ThumbnailUrl =
                                           "http://static.wixstatic.com/media/9120ab_8bf52939058941038208fefa27c5ec8b.png_256"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Пилешко",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/5326690dc566d737c5001936-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Спагети",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/4ef2825bc566d718c5000563-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Лук",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/5386898ac566d76d200135aa-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Чесън",
                                       ThumbnailUrl = "http://imagespng.com/Data/Logo/garlic_PNG12804.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Чушка",
                                       ThumbnailUrl =
                                           "http://www.emoji.co.uk/files/apple-emojis/food-drink-ios/369-hot-pepper.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Броколи",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/55bffe45777a426ca40228fc-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Хляб",
                                       ThumbnailUrl =
                                           "https://dtgxwmigmg3gc.cloudfront.net/files/519319f1c566d746a707f37f-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Олио",
                                       ThumbnailUrl = "http://www.swissbeveragefood.com/ressources/img/icons/oil.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Захар",
                                       ThumbnailUrl =
                                           "https://hydra-media.cursecdn.com/h1z1.gamepedia.com/8/80/Icon_Sugar.png?version=e23b7a3c3868053eb7cda50d28628774"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Босилек",
                                       ThumbnailUrl =
                                       "https://dtgxwmigmg3gc.cloudfront.net/files/55b6e0f152ba0b75870002f5-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Морков",
                                       ThumbnailUrl = "https://dtgxwmigmg3gc.cloudfront.net/files/54372113e1272f6e820006dc-icon-256x256.png"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Черен пипер",
                                       ThumbnailUrl = "http://www.naturalniprodukti.com/uploads/com_article/gallery/thumbs/500x500_6e876050eff76c7be2acc0e375dafb5d1898c2af.jpg"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Вода",
                                       ThumbnailUrl = "http://vignette2.wikia.nocookie.net/play-rust/images/f/fc/Small_Water_Bottle_icon.png/revision/20161117220421"
                                   },
                                   new ProductType()
                                   {
                                       Name = "Доматен сок",
                                       ThumbnailUrl = "http://images.bigoven.com/image/upload/t_recipe-256/roasted-balsamic-tomato-sauce-cce951.jpg"
                                   },
                               };
            context.ProductTypes.AddOrUpdate(productTypes);
            context.SaveChanges();
        }

        private void AddRecipeForSpaghettiWithTomatoSauceToDatabase(IDbContext context)
        {
            var spaghetti = new Ingredient()
            {
                Id = 1,
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "Спагети".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 500
            };

            var onions = new Ingredient()
            {
                Id = 2,
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "Лук".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 100
            };

            var garlic = new Ingredient()
            {
                Id = 3,
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "Чесън".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 20
            };
            var tomato = new Ingredient()
            {
                Id = 4,
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "Домат".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Gram,
                Quantity = 500
            };
            var tomatoSauce = new Ingredient()
            {
                Id = 5,
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "Доматен сок".ToUpper()),
                QuantityMeasurementType = QuantityMeasurementType.Milliliter,
                Quantity = 300
            };
            var water = new Ingredient()
            {
                Id = 6,
                ProductType = context.ProductTypes.First(p => p.Name.ToUpper() == "Вода".ToUpper()),
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
                                Id = 1,
                                Text = "Свари спагетите. След като се сварят ги изцедете и им сложете малко олио",
                                TimeToFinishInMinutes = 15
                            },
                            new RecipeStep()
                            {
                                Id = 2,
                                Text =
                                    "Обелете лука, измийте и обелете моркова и нарежете всичко на ситно. Сложете всичко да се запържва в тиган.",
                                TimeToFinishInMinutes = 5
                            },
                            new RecipeStep()
                            {
                                Id = 3,
                                Text =
                                    "Сложете във 200 мл. вода доматеното пюре и нарязаните домати, разбъркайте ги добре и сложете сместа към задушените зеленчуци.",
                            },
                            new RecipeStep()
                            {
                                Id = 4,
                                Text = "Оставете соса да къкри на тих огън",
                                TimeToFinishInMinutes = 20
                            },
                            new RecipeStep()
                            {
                                Id = 5,
                                Text = "Добавете сол, черен пипер и малко захар на вкус."
                            }
                        };

            var review = new Review()
            {
                Id = 1,
                Title = "Вкусничкоооо",
                Body = "Все пак си е моята рецепта ;))))))))))))))",
                Rating = RatingType.InLove,
                Author = context.Users.First()
            };

            var recipe = new Recipe()
            {
                Id = 1,
                Title = "Спагети с доматен сос",
                Images =
                    new[]
                        {
                            new RecipeImage()
                            {
                                Id = 1,
                                Url = "http://recepti.gotvach.bg/files/lib/600x350/spageti31.jpg"
                            },
                            new RecipeImage()
                            {
                                Id = 2,
                                Url = "http://gotvach.bg/files/recipes/photos/p_20160204_085608_12009.jpg"
                            },
                        },
                Ingredients = products,
                Steps = steps,
                Reviews =
                    new List<Review>()
                        {
                            review,
                        },
                Author = context.Users.ToList().Skip(1).First(),
                MinutesRequiredToCook = 50
            };

            context.Ingredients.AddOrUpdate(products);
            context.RecipeSteps.AddOrUpdate(steps);
            context.Reviews.AddOrUpdate(review);
            context.RecipeImages.AddOrUpdate(recipe.Images.ToArray());
            context.Recipes.AddOrUpdate(recipe);
            context.SaveChanges();
        }

        protected override void Seed(NzKvoDaQmContext context)
        {
            var author1 = new ApplicationUser()
            {
                Id = "12ee11f3-2dfc-4bf4-84fb-97c080c68c6c",
                Email = "testbrat1@email.com",
                EmailConfirmed = true,
                PasswordHash = "asdasdasdasdasda",
                UserName = "Test1Userame"
            };
            var author2 = new ApplicationUser()
            {
                Id = "d462ac0f-bb4d-4f2a-9fb4-745ffeefda6b",
                Email = "testbrat2@abv.bg",
                EmailConfirmed = true,
                PasswordHash = "sadaskdladlaskdalsdakdal",
                UserName = "Test2Username",
            };

            context.Users.AddOrUpdate(author1);
            context.Users.AddOrUpdate(author2);
            context.SaveChanges();

            this.AddProductTypesToDatabase(context);
            this.AddRecipeForSpaghettiWithTomatoSauceToDatabase(context);
        }
    }
}
