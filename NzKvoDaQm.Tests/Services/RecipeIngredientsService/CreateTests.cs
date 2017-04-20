namespace NzKvoDaQm.Tests.Services.RecipeStepsService
{

    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Recipe;

    [TestClass]
    public class CreateTests
    {
        private IDbContext context;
        private IDbSet<Ingredient> set;
        private RecipeIngredientsService service;

        [TestInitialize]
        public void Initialize()
        {
            var dataGenerator = new TestDataGenerator();
            this.context = dataGenerator.GenerateContext();
            this.set = this.context.Ingredients;
            this.service = new RecipeIngredientsService(this.set, this.context);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CantCreateIfIngredientTypeIsNull()
        {
            this.service.Create(null, QuantityMeasurementType.Grams, 10);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CantCreateIfQuantityIsNotPositiveNumber()
        {
            this.service.Create(new IngredientType(), QuantityMeasurementType.Grams, -10);
        }

        [TestMethod]
        public void Create()
        {
            var ingredientType = this.context.IngredientTypes.First();
            var entity = this.service.Create(ingredientType, QuantityMeasurementType.Grams, 100);

            Assert.AreEqual(ingredientType, entity.IngredientType);
            Assert.AreEqual(QuantityMeasurementType.Grams, entity.QuantityMeasurementType);
            Assert.AreEqual(100, entity.Quantity);
        }
    }
}
