namespace NzKvoDaQm.Tests.Services.SearchService
{

    using System.Data.Entity;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;
    using NzKvoDaQm.Services;

    [TestClass]
    public class SearchServiceTests
    {
        public IngredientType[] IngredientTypes
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
            this.IngredientTypes = new IngredientType[]
                                {
                                    new IngredientType()
                                    {
                                        Name = "Spaghetti",
                                        ThumbnailUrl = "www.PTXURL.com"
                                    },
                                    new IngredientType()
                                    {
                                        Name = "Tomato sauce",
                                        ThumbnailUrl = "Nz.com"
                                    },
                                    new IngredientType()
                                    {
                                        Name = "Pastry",
                                        ThumbnailUrl = "pastry.com/img.jpg"
                                    },
                                };
            this.Ingredients = new Ingredient[]
                           {
                               new Ingredient()
                               {
                                   IngredientType = this.IngredientTypes[0],
                                   QuantityMeasurementType = QuantityMeasurementType.Gram,
                                   Quantity = 200
                               },
                               new Ingredient()
                               {
                                   IngredientType = this.IngredientTypes[1],
                                   QuantityMeasurementType = QuantityMeasurementType.Milliliter,
                                   Quantity = 500
                               },
                               new Ingredient()
                               {
                                   IngredientType = this.IngredientTypes[2],
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
                                   Images = new []
                                                {
                                                    new RecipeImage()
                                                    {
                                                        Url = "abvgd"
                                                    },
                                                    new RecipeImage()
                                                    {
                                                        Url = "dasdad"
                                                    }
                                                },
                                   Ingredients = this.Ingredients.Take(2).ToArray(),
                                   Steps = this.Steps,
                                   Author = this.Author,
                                   MinutesRequiredToCook = 30
                               },
                               new Recipe()
                               {
                                   Title = "Spaghetti bolonese",
                                   Images = new []
                                                {
                                                    new RecipeImage()
                                                    {
                                                        Url = "asdsadasda"
                                                    },
                                                },
                                   Ingredients = this.Ingredients.Take(1).ToArray(),
                                   Steps = this.Steps,
                                   Author = this.Author,
                                   MinutesRequiredToCook = 50
                               },
                               new Recipe()
                               {
                                   Title = "Spaghetti test2",
                                   Images = new []
                                                {
                                                    new RecipeImage()
                                                    {
                                                        Url = "asdsadasda"
                                                    },
                                                },
                                   Ingredients = this.Ingredients.ToArray(),
                                   Steps = this.Steps,
                                   Author = this.Author2,
                                   MinutesRequiredToCook = 120
                               }, 
                               new Recipe()
                               {
                                   Title = "Баница със сиреньееее",
                                   Images = new []
                                                {
                                                    new RecipeImage()
                                                    {
                                                        Url = "asdsadasda"
                                                    },
                                                },
                                   Ingredients = this.Ingredients.Skip(2).Take(1).ToArray(),
                                   Steps = this.Steps,
                                   Author = this.Author2
                               },
                           };
        }

        private void InitializeContext()
        {
            var productTypesSet = new Mock<DbSet<IngredientType>>().SetupData(this.IngredientTypes);
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
            context.Setup(c => c.Ingredients)
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
            this.InitializeModels();
            this.InitializeContext();
        }

        [TestMethod]
        public void Nothing()
        {
        }

        [TestMethod]
        public void ReturningEmptyArrayOnEmptyQueryString()
        {
            var service = new NzKvoDaQm.Services.SearchService(this.Context);

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
