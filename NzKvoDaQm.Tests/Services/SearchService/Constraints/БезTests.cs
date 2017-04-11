namespace NzKvoDaQm.Tests.Services.SearchService.Constraints
{

    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Search.SearchConstraints;

    [TestClass]
    public class БезTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BannedWordsCantBeNull()
        {
            new Без(null);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BannedWordsCantBeEmptyString()
        {
            new Без("");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BannedWordsCantBeEmptyString2()
        {
            new Без("                     ");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BannedWordsMustContainWords()
        {
            new Без(",,,,,,,,,");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BannedWordsMustContainWords2()
        {
            new Без(" ,,,,,,  ,         ,,");
            Assert.Fail();
        }

        [TestMethod]
        public void BlockingSpecificWordsFromRecipeTitle()
        {
            var constraint = new Без("asd, иван");
            var recipe = new Recipe()
                         {
                             Title = "Иван"
                         };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void BlockingSpecificWordsFromRecipeTitle2()
        {
            var constraint = new Без("БАБА, яга, наморник");
            var recipe = new Recipe()
                         {
                             Title = "Баба"
                         };
            Assert.IsFalse(constraint.IsAllowed(recipe));
        }
    }
}
