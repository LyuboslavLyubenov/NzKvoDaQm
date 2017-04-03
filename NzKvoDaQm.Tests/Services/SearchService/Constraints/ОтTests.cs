using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NzKvoDaQm.Tests.Services.SearchService.Constraints
{

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;
    using NzKvoDaQm.Services.SearchConstraints;

    [TestClass]
    public class ОтTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DesiredUsernameCantBeNull()
        {
            new От(null);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DesiredUsernameCantBeWhitespace()
        {
            new От("              ");
            Assert.Fail();
        }

        [TestMethod]
        public void AllowingRecipeWithAuthorWithDesiredUsername()
        {
            var constraint = new От("иван");
            var recipe = new Recipe()
                         {
                             Author = new ApplicationUser()
                                      {
                                          UserName = "иван"
                                      }
                         };
            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void AllowingRecipeWithAuthorWithDesiredUsername2()
        {
            var constraint = new От("Ванката         ");
            var recipe = new Recipe()
                         {
                             Author = new ApplicationUser()
                                      {
                                          UserName = "Ванката"
                                      }
                         };
            Assert.IsTrue(constraint.IsAllowed(recipe));
        }

        [TestMethod]
        public void DenyRecipeWithAuthorWithDifferentThatDesiredUsername()
        {
            var constraint = new От("Иван");
            var recipe = new Recipe()
                         {
                             Author = new ApplicationUser()
                                      {
                                          UserName = "Георги"
                                      }
                         };
            Assert.IsFalse(constraint.IsAllowed(recipe));
        }
    }
}
