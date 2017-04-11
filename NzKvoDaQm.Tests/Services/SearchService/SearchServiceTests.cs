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
    using NzKvoDaQm.Services.Search;

    [TestClass]
    public class SearchServiceTests
    {
        private IDbContext context;

        [TestInitialize]
        public void Initialize()
        {
            var dataGenerator = new TestDataGenerator();
            this.context = dataGenerator.GenerateContext();
        }

        [TestMethod]
        public void Nothing()
        {
        }

        [TestMethod]
        public void ReturnAllRecipesOnEmptyQueryString()
        {
            var service = new SearchService(this.context);

            var recipes = service.GetRecipes(null);

            Assert.AreEqual(this.context.Recipes.Count(), recipes.Count());
        }

        [TestMethod]
        public void ReturnAllRecipesOnEmptyQueryString2()
        {
            var service = new SearchService(this.context);

            var recipes = service.GetRecipes("                 ");

            Assert.AreEqual(this.context.Recipes.Count(), recipes.Count());
        }

        [TestMethod]
        public void ReturnEmptyArrayWhenNoMatches()
        {
            var service = new SearchService(this.context);
            var recipes = service.GetRecipes("addsadasdasdadsdaddsddsadsd");
            Assert.AreEqual(0, recipes.Count());
        }

        [TestMethod]
        public void ReturnRecipeWhenMatched1()
        {
            var service = new SearchService(this.context);
            var recipes = service.GetRecipes("Spaghetti");

            var expected = this.context.Recipes.First(r => r.Title.ToUpper().Contains("Spaghetti".ToUpper()));

            Assert.AreEqual(expected, recipes.First());
        }

        [TestMethod]
        public void ReturnRecipeWhenMatched2()
        {
            var service = new SearchService(this.context);
            var recipes = service.GetRecipes("Spaghetti filipino maika ti e gotina jena ;))))");
            var expected = this.context.Recipes.First(r => r.Title.ToUpper().Contains("Spaghetti".ToUpper()));

            Assert.AreEqual(expected, recipes.First());
        }

        [TestMethod]
        public void ReturnAllRecipesWhenQueryContainsOnlySpecificKeyword()
        {
            var service = new SearchService(this.context);

            var recipes = service.GetRecipes("всички");

            Assert.AreEqual(this.context.Recipes.Count(), recipes.Count());
        }

        [TestMethod]
        public void ReturnAllRecipesWhenQueryContainsOnlySpecificKeyword1()
        {
            var service = new SearchService(this.context);

            var recipes = service.GetRecipes("Всички");

            Assert.AreEqual(this.context.Recipes.Count(), recipes.Count());
        }
    }
}