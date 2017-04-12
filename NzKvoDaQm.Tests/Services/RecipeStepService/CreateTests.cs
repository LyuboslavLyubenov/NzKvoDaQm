namespace NzKvoDaQm.Tests.Services.RecipeStepService
{

    using System;
    using System.Data.Entity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;
    using NzKvoDaQm.Services.Recipe;

    [TestClass]
    public class CreateTests
    {
        private IDbContext context;
        private IDbSet<RecipeStep> set;
        private IRecipeStepsService service;

        [TestInitialize]
        public void Initialize()
        {
            var dataGenerator = new TestDataGenerator();
            this.context = dataGenerator.GenerateContext();
            this.set = this.context.RecipeSteps;
            this.service = new RecipeStepsService(this.set, this.context);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CantCreateIfTextIsNull()
        {
            this.service.Create(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CantCreateIfTextIsWhitespace()
        {
            this.service.Create("            ", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantCreateIfTextDoesNotContainWords()
        {
            this.service.Create("               ,.,.,.,..,,.123^5%^", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CantCreateIfTimeToFinishInMinutesIsNotPositiveNumber()
        {
            this.service.Create("SimpleStep", -10);
        }

        [TestMethod]
        public void Created()
        {
            var text = "Step";
            var timeToFinishInMinutes = 10;
            var entity = this.service.Create(text, timeToFinishInMinutes);

            Assert.AreEqual(text, entity.Text);
            Assert.AreEqual(timeToFinishInMinutes, entity.TimeToFinishInMinutes);
        }

        [TestMethod]
        public void Created2()
        {
            var text = "Step1";
            var entity = this.service.Create(text, null);

            Assert.AreEqual(text, entity.Text);
            Assert.IsNull(entity.TimeToFinishInMinutes);
        }
    }
}
