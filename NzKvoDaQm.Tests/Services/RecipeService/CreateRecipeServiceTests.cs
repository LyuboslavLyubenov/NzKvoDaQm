namespace NzKvoDaQm.Tests.Services.RecipeService
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.BindingModels;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;
    using NzKvoDaQm.Services.Recipe;

    [TestClass]
    public class CreateRecipeServiceTests
    {
        private IRecipesService recipesService;
        private IDbContext context;
        private ApplicationUser author;

        private CreateRecipeBindingModel CreateValidRecipeBindingModel()
        {
            return new CreateRecipeBindingModel()
            {
                Title = "Sample Title",
                Images = new string[]
                                {
                                    "data:image/png;base64,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"
                                },
                IngredientsNames = new []
                                   {
                                       "Лук",
                                       "Картофи"
                                   },
                IngredientsMeasurementTypes = new []
                                              {
                                                  QuantityMeasurementType.Gram, 
                                                  QuantityMeasurementType.Gram, 
                                              },
                IngredientsQuantities = new []
                                        {
                                            100,
                                            200  
                                        },
                StepsTexts = new[] { "Step 1", "Step 2", "Step 3" },
                StepsMinutes = new int?[] { null, 10, null },
                MinutesRequiredForCooking = 40
            };
        }

        private void ConfigureIngredientsTypesMock(Mock<IIngredientTypesService> ingredientsTypesService)
        {
            ingredientsTypesService
                .Setup(it => it.Create(It.IsRegex("[A-Za-zА-Яа-я]"), It.IsAny<string>()))
                .Returns(
                    (string name, string thumbnailUrl) => new IngredientType()
                                                          {
                                                              Name = name,
                                                              ThumbnailUrl = thumbnailUrl
                                                          });
        }

        private void ConfigureIngredientsMock(Mock<IRecipeIngredientsService> ingredientsService)
        {
            ingredientsService
                .Setup(
                    i => i.Create(
                        It.IsNotNull<IngredientType>(),
                        It.IsAny<QuantityMeasurementType>(),
                        It.IsInRange<int>(0, int.MaxValue, Range.Exclusive)))
                .Returns(
                    (IngredientType it, QuantityMeasurementType qmt, int quantity) => new Ingredient()
                                                                                      {
                                                                                          IngredientType = it,
                                                                                          QuantityMeasurementType = qmt,
                                                                                          Quantity = quantity
                                                                                      });
        }

        private void ConfigureRecipeImagesService(Mock<IRecipeImagesService> recipeImagesService)
        {
            recipeImagesService.Setup(ri => ri.Create(It.IsNotNull<string>(), It.IsNotNull<ApplicationUser>()))
                .Returns(
                    (string imageBase64, ApplicationUser author) => new RecipeImage()
                                                                    {
                                                                        Url = imageBase64,
                                                                        Author = author
                                                                    });
        }

        private void ConfigureRecipeStepsService(Mock<IRecipeStepsService> recipeStepsService)
        {
            recipeStepsService.Setup(rs => rs.Create(It.IsNotNull<string>(), It.IsAny<int?>()))
                .Returns(
                    (string text, int? timeToFinishInMinutes) => new RecipeStep()
                                                                 {
                                                                     Text = text,
                                                                     TimeToFinishInMinutes = timeToFinishInMinutes
                                                                 });
        }

        [TestInitialize]
        public void Initialize()
        {
            var dataGenerator = new TestDataGenerator();
            this.context = dataGenerator.GenerateContext();
            
            var ingredientsTypesService = new Mock<IIngredientTypesService>();
            var ingredientsService = new Mock<IRecipeIngredientsService>();
            var recipeImagesService = new Mock<IRecipeImagesService>();
            var recipeStepsService = new Mock<IRecipeStepsService>();

            this.ConfigureIngredientsTypesMock(ingredientsTypesService);
            this.ConfigureIngredientsMock(ingredientsService);
            this.ConfigureRecipeImagesService(recipeImagesService);
            this.ConfigureRecipeStepsService(recipeStepsService);

            this.recipesService = 
                new RecipeService(
                    this.context.Recipes,
                    ingredientsTypesService.Object, 
                    ingredientsService.Object, 
                    recipeImagesService.Object,
                    recipeStepsService.Object,
                    this.context);

            this.author = this.context.Users.First();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CantCreateIfTitleIsNullOrWhiteSpace()
        {
            var bindingModel = this.CreateValidRecipeBindingModel();
            bindingModel.Title = null;

            var result = this.recipesService.Create(bindingModel, this.author);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CantCreateIfTitleIsNullOrWhiteSpace2()
        {
            var bindingModel = this.CreateValidRecipeBindingModel();
            bindingModel.Title = "                               ";

            var result = this.recipesService.Create(bindingModel, this.author);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CantCreateIfMinuteRequiredForCookingIsNegative()
        {
            var bindingModel = this.CreateValidRecipeBindingModel();
            bindingModel.MinutesRequiredForCooking = -10;

            var result = this.recipesService.Create(bindingModel, this.author);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CantCreateIfRecipeExistsWithSameName()
        {
            var bindingModel = this.CreateValidRecipeBindingModel();
            bindingModel.Title = this.context.Recipes.First()
                .Title;
            this.recipesService.Create(bindingModel, this.author);
            this.recipesService.Create(bindingModel, this.author);
        }

        [TestMethod]
        public void Create()
        {
            var bindingModel = this.CreateValidRecipeBindingModel();
            var recipe = this.recipesService.Create(bindingModel, this.author);

            var isAddedToDatabase = this.context.Recipes.Any(r => r.Title == recipe.Title);
            Assert.IsTrue(isAddedToDatabase);
        }
    }
}