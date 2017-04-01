﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NzKvoDaQm.Tests.Services
{

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Moq;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;
    using NzKvoDaQm.Services;


    [TestClass]
    public class SearchServiceTests
    {
        public ProductType[] ProductTypes
        {
            get; set;
        }

        public Ingredient[] Ingredients
        {
            get; set;
        }

        public RecipeStep[] Steps
        {
            get; set;
        }

        public ApplicationUser Author
        {
            get; set;
        }

        public ApplicationUser Author2
        {
            get; set;
        }

        public Recipe[] Recipes
        {
            get; set;
        }

        public NzKvoDaQmContext Context
        {
            get; set;
        }

        private void InitializeModels()
        {
            this.ProductTypes = new ProductType[]
                                {
                                    new ProductType()
                                    {
                                        Name = "Spaghetti",
                                        ThumbnailUrl = "www.PTXURL.com"
                                    },
                                    new ProductType()
                                    {
                                        Name = "Tomato sauce",
                                        ThumbnailUrl = "Nz.com"
                                    },
                                    new ProductType()
                                    {
                                        Name = "Pastry",
                                        ThumbnailUrl = "pastry.com/img.jpg"
                                    },
                                };
            this.Ingredients = new Ingredient[]
                           {
                               new Ingredient()
                               {
                                   ProductType = this.ProductTypes[0],
                                   QuantityMeasurementType = QuantityMeasurementType.Gram,
                                   Quantity = 200
                               },
                               new Ingredient()
                               {
                                   ProductType = this.ProductTypes[1],
                                   QuantityMeasurementType = QuantityMeasurementType.Milliliter,
                                   Quantity = 500
                               },
                               new Ingredient()
                               {
                                   ProductType = this.ProductTypes[2],
                                   QuantityMeasurementType = QuantityMeasurementType.Gram,
                                   Quantity = 500
                               }
                           };

            this.Steps = new RecipeStep[]
                        {
                            new RecipeStep()
                            {
                                Text = "Step1"
                            },
                            new RecipeStep()
                            {
                                Text = "Step2",
                                TimeToFinishInMinutes = 20
                            }
                        };
            this.Author = new ApplicationUser()
            {
                Email = "brat@mail.bg",
                PasswordHash = "PasswordHash",
                UserName = "bratazsymgotin"
            };
            this.Author2 = new ApplicationUser()
            {
                Email = "gotvacha@got.in",
                PasswordHash = "nemiznaeshpassa",
                UserName = "Gotvacha"
            };
            this.Recipes = new Recipe[]
                           {
                               new Recipe()
                               {
                                   Title = "Spaghetti with tomato sauce",
                                   ImagesUrls = new string[]
                                                {
                                                    "abvgd",
                                                    "badjak.com"
                                                },
                                   Products = this.Ingredients.Take(2).ToArray(),
                                   Steps = Steps,
                                   Author = Author,
                                   MinutesRequiredToCook = 30
                               },
                               new Recipe()
                               {
                                   Title = "Spaghetti bolonese",
                                   ImagesUrls = new string[]
                                                {
                                                    "asdasdad",
                                                    "adasdad.asdad"
                                                },
                                   Products = this.Ingredients.Take(1).ToArray(),
                                   Steps = this.Steps,
                                   Author = this.Author,
                                   MinutesRequiredToCook = 50
                               },
                               new Recipe()
                               {
                                   Title = "Spaghetti test2",
                                   ImagesUrls = new string[]
                                                {
                                                    "asdasdassd"
                                                },
                                   Products = this.Ingredients.ToArray(),
                                   Steps = this.Steps,
                                   Author = this.Author2,
                                   MinutesRequiredToCook = 120
                               }, 
                               new Recipe()
                               {
                                   Title = "Баница със сиреньееее",
                                   ImagesUrls = new string[]
                                                {
                                                    "asdsadasd",
                                                    "asdadas"
                                                },
                                   Products = this.Ingredients.Skip(2).Take(1).ToArray(),
                                   Steps = this.Steps,
                                   Author = this.Author2
                               },
                           };
        }

        private void InitializeContext()
        {
            var productTypesSet = new Mock<DbSet<ProductType>>().SetupData(this.ProductTypes);
            var productsSet = new Mock<DbSet<Ingredient>>().SetupData(this.Ingredients);
            var recipeStepsSet = new Mock<DbSet<RecipeStep>>().SetupData(this.Steps);
            var usersSet = new Mock<DbSet<ApplicationUser>>().SetupData(
                new[]
                {
                    this.Author
                });
            var recipes = new Mock<DbSet<Recipe>>().SetupData(this.Recipes);

            var context = new Mock<NzKvoDaQmContext>();

            context.Setup(c => c.ProductTypes)
                .Returns(productTypesSet.Object);
            context.Setup(c => c.Products)
                .Returns(productsSet.Object);
            context.Setup(c => c.RecipeSteps)
                .Returns(recipeStepsSet.Object);
            context.Setup(c => c.Users)
                .Returns(usersSet.Object);
            context.Setup(c => c.Recipes)
                .Returns(recipes.Object);

            this.Context = context.Object;
        }

        [TestInitialize]
        public void Initialize()
        {
            InitializeModels();
            InitializeContext();
        }

        [TestMethod]
        public void Nothing()
        {
        }

        [TestMethod]
        public void ReturningEmptyArrayOnEmptyQueryString()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes(null);

            Assert.AreEqual(0, recipes.Count);
        }

        [TestMethod]
        public void ReturningEmptyArrayOnEmptyQueryString2()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("                 ");

            Assert.AreEqual(0, recipes.Count);
        }

        [TestMethod]
        public void ReturningEmptyArrayWhenNoMatches()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("addsadasdasdadsdaddsddsadsd");

            Assert.AreEqual(0, recipes.Count);
        }

        [TestMethod]
        public void ReturningRecipeWhenMatched1()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti");

            Assert.AreEqual(this.Recipes[0], recipes[0]);
        }

        [TestMethod]
        public void ReturningRecipeWhenMatched2()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti filipino maika ti e gotina jena ;))))");

            Assert.AreEqual(this.Recipes[0], recipes[0]);
        }

        [TestMethod]
        public void ExcludeRecipesContainingForbiddenWordInTitle()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti без:\"tomato\"");

            Assert.IsFalse(recipes.Contains(this.Recipes[0]));
        }

        [TestMethod]
        public void ExcludeRecipesContainingForbiddenWordInTitle2()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti без:\"picccc\"");
            var recipesContainingSpaghettiInTitle = service.GetRecipes("Spaghetti");

            var isCorrectResult = !recipes.Except(recipesContainingSpaghettiInTitle)
                                       .Any();

            Assert.IsTrue(isCorrectResult);
        }

        [TestMethod]
        public void ExcludeRecipesContainingForbiddenWordInTitle3()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti иван:spaghetti spaghetti:asd без:\" \"");
            var recipesContainingSpaghettiInTitle = service.GetRecipes("Spaghetti");

            var isCorrectResult = !recipes.Except(recipesContainingSpaghettiInTitle)
                                       .Any();

            Assert.IsTrue(isCorrectResult);
        }

        [TestMethod]
        public void ExcludeRecipesContainingForbiddenWordInTitle4()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti без:\"tomato, test2\"");
            
            Assert.IsFalse(recipes.Contains(this.Recipes[0]));
            Assert.IsFalse(recipes.Contains(this.Recipes[2]));
        }
        
        [TestMethod]
        public void DontExcludeAnyRecipeWhenForbiddenWordInTitleParameterIsEmptyOrWhiteSpace()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti без:\"\"");
            var recipesContainingSpaghettiInTitle = service.GetRecipes("Spaghetti");

            var isCorrectResult = !recipes.Except(recipesContainingSpaghettiInTitle)
                                       .Any();

            Assert.IsTrue(isCorrectResult);
        }

        [TestMethod]
        public void DontExcludeAnyRecipeWhenForbiddenWordInTitleParameterIsEmptyOrWhiteSpace2()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Spaghetti без:\"                          \"");
            var recipesContainingSpaghettiInTitle = service.GetRecipes("Spaghetti");

            var isCorrectResult = !recipes.Except(recipesContainingSpaghettiInTitle)
                                       .Any();

            Assert.IsTrue(isCorrectResult);
        }

        [TestMethod]
        public void ReturnAll()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Всички");

            var isReturnedAll = !recipes.Except(this.Recipes)
                                     .Any();
            Assert.IsTrue(isReturnedAll);
        }

        [TestMethod]
        public void ReturnUnderSpecificTimeNeededToCook()
        {
            var service = new SearchService(this.Context);

            var recipes = service.GetRecipes("Всички под:\"50 мин\"");


        }
    }
}
