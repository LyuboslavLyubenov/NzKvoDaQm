using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NzKvoDaQm.Tests.Services.IngredientTypesService
{

    using System;
    using System.Data.Entity;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;
    using NzKvoDaQm.Services.Recipe;

    [TestClass]
    public class CreateTests
    {
        private IIngredientTypesService service;
        private IDbSet<IngredientType> set;

        [TestInitialize]
        public void Initialize()
        {
            var dataGenerator = new TestDataGenerator();
            var context = dataGenerator.GenerateContext();
            var set = context.IngredientTypes;
            this.service = new IngredientTypesService(set, context);
            this.set = set;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantCreateIfNameIsNull()
        {
            this.service.Create(null, "http://fakewebsite.com/fakeimage.jpg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantCreateIfNameIsWhitespace()
        {
            this.service.Create("            ", "http://fakewebsite.com/fakeimage.jpg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameMustNotContainSymbols()
        {
            this.service.Create("!#$!%,.,.", "http://fakewebsite.com/fakeimage.jpg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameMustNotContainNumbers()
        {
            this.service.Create("Onion123123123", "http://fakewebsite.com/fakeimage.jpg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantCreateIfThumbnailUrlIsNotValidPicture()
        {
            this.service.Create("Onion", "http://fakewebsite.com/fakeimage.jpg.doc");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantCreateIfThumbnailUrlIsNotValidPicture2()
        {
            this.service.Create("Onion", "http://fakewebsite.com/fakeimage");
        }

        [TestMethod]
        public void Created()
        {
            var name = "Onion";
            var thumbnailUrl = "http://fakewebsite.com/fakeimage.jpg";
            var entity = this.service.Create(name, thumbnailUrl);
            var isInDatabase = this.set.FirstOrDefault(it => it.Name == name && it.ThumbnailUrl == thumbnailUrl) != null;
            Assert.IsTrue(isInDatabase);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CantCreateIfIngredientTypeExsitsWithSameName()
        {
            var name = "Onion";
            var thumbnailUrl = "http://fakewebsite.com/fakeimage.jpg";
            this.service.Create(name, thumbnailUrl);
            this.service.Create(name, null);
        }
    }
}
