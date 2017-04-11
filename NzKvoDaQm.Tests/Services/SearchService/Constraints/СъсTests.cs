using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NzKvoDaQm.Tests.Services.SearchService.Constraints
{

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Search.SearchConstraints;

    [TestClass]
    public class СъсTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IngredientsCantBeNull()
        {
            new Със(null);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IngredientsCantBeEmptyStringOrWhitespaces()
        {
            new Със("             ");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IngredientsCantBeEmpty()
        {
            new Със("         ,,,,,,  , ,, , ,,,,,,,,, , ,    ");
            Assert.Fail();
        }

        [TestMethod]
        public void AllowRecipeContainingRequiredIngredients()
        {
            var constraint = new Със("Пържола");
            var recipe = new Recipe()
                         {
                             Ingredients = new []
                                           {
                                               new Ingredient()
                                               {
                                                   IngredientType = new IngredientType()
                                                                 {
                                                                     Name = "Пържола"
                                                                 }
                                               }, 
                                           }
                         };
            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowRecipeContainingRequiredIngredients2()
        {
            var constraint = new Със("Пържола, Сирене, Кайма, Олио");
            var recipe = new Recipe()
            {
                Ingredients = new[]
                                           {
                                               new Ingredient()
                                               {
                                                   IngredientType = new IngredientType()
                                                                 {
                                                                     Name = "Кайма"
                                                                 }
                                               },
                                           }
            };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowRecipeContainingRequiredIngredients3()
        {
            var constraint = new Със("         Пържола,         Сирене   ,  Кайма, Олио           ");
            var recipe = new Recipe()
            {
                Ingredients = new[]
                                           {
                                               new Ingredient()
                                               {
                                                   IngredientType = new IngredientType()
                                                                 {
                                                                     Name = "Сирене"
                                                                 }
                                               },
                                           }
            };
            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeNotContainingRequiredIngredients()
        {
            var constraint = new Със("Свинско, Сирене, Кори");
            var recipe = new Recipe()
            {
                Ingredients = new[]
                                           {
                                               new Ingredient()
                                               {
                                                   IngredientType = new IngredientType()
                                                                 {
                                                                     Name = "Леща"
                                                                 }
                                               },
                                           }
            };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }
    }
}