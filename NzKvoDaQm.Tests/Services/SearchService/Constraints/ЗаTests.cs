namespace NzKvoDaQm.Tests.Services.SearchService.Constraints
{

    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.SearchConstraints;

    [TestClass]
    public class ЗаTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeNumberMustBeGreaterThanZero()
        {
            new За("-1 минути");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeNumberMustBeInteger()
        {
            new За("1.25 часa");
            Assert.Fail();
        }

        [TestMethod]
        public void AllowRecipeUnderSpecificMinutesForCooking()
        {
            var constraint = new За("15 мин");
            var recipe = new Recipe()
                         {
                             MinutesRequiredToCook = 10
                         };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }
        
        [TestMethod]
        public void AllowRecipeUnderSpecificMinutesForCooking2()
        {
            var constraint = new За("15 минути");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 10
            };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowRecipeUnderSpecificMinutesForCooking3()
        {
            var constraint = new За("      15           минути      ");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 10
            };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowRecipeUnderSpecificMinutesForCooking4()
        {
            var constraint = new За("15 МИН");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 10
            };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeAboveSpecificMinutesForCooking()
        {
            var constraint = new За("20 мин");
            var recipe = new Recipe()
                         {
                             MinutesRequiredToCook = 22
                         };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeAboveSpecificMinutesForCooking2()
        {
            var constraint = new За("20 минути");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 22
            };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeAboveSpecificMinutesForCooking3()
        {
            var constraint = new За("       20                     минути        ");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 22
            };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeAboveSpecificMinutesForCooking4()
        {
            var constraint = new За("20 МИНУТИ");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 22
            };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowRecipeAboveSpecificHoursForCooking()
        {
            var constraint = new За("2 часа");
            var recipe = new Recipe()
                         {
                             MinutesRequiredToCook = 30
                         };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowRecipeAboveSpecificHoursForCooking2()
        {
            var constraint = new За("2              часа           ");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 30
            };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowRecipeAboveSpecificHoursForCooking3()
        {
            var constraint = new За("2 ЧАСА");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 30
            };

            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeAboveSpecificHoursForCooking()
        {
            var constraint = new За("1 часа");
            var recipe = new Recipe()
                         {
                             MinutesRequiredToCook = 999
                         };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeAboveSpecificHoursForCooking2()
        {
            var constraint = new За("1                      часа");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 999
            };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeAboveSpecificHoursForCooking3()
        {
            var constraint = new За("1 ЧАСА");
            var recipe = new Recipe()
            {
                MinutesRequiredToCook = 999
            };

            Assert.IsFalse(constraint.IsAllowed(recipe));
        }
    }
}
