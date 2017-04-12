namespace NzKvoDaQm.Tests
{

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Moq;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Models.ViewModels;

    /// <summary>
    /// 
    /// </summary>
    public class TestDataGenerator
    {
        public TestDataGenerator()
        {
        }

        private IList<IngredientType> GenerateIngredientsTypes()
        {
            return new List<IngredientType>()
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
                                    new IngredientType()
                                    {
                                        Name = "Bacon",
                                        ThumbnailUrl = "http://bacon.com"
                                    },
                                    new IngredientType()
                                    {
                                        Name = "Chichen breast",
                                        ThumbnailUrl = "http://chicken-breast.com"
                                    },
                                    new IngredientType()
                                    {
                                        Name = "Sausage",
                                        ThumbnailUrl = "http://sausage.com"
                                    },
                                    new IngredientType()
                                    {
                                        Name = "Eggs",
                                        ThumbnailUrl = "Eggs"
                                    },
                                    new IngredientType()
                                    {
                                        Name = "Ice cream",
                                        ThumbnailUrl = "http://ice-cream.com"
                                    },
                                };
        }

        private IList<Ingredient> GenerateIngredientsUsedInRecipes(IList<IngredientType> ingredientTypes)
        {
            return new List<Ingredient>()
                   {
                       new Ingredient()
                       {
                           IngredientType = ingredientTypes[0],
                           QuantityMeasurementType = QuantityMeasurementType.Gram,
                           Quantity = 200
                       },
                       new Ingredient()
                       {
                           IngredientType = ingredientTypes[1],
                           QuantityMeasurementType = QuantityMeasurementType.Milliliter,
                           Quantity = 500
                       },
                       new Ingredient()
                       {
                           IngredientType = ingredientTypes[2],
                           QuantityMeasurementType = QuantityMeasurementType.Gram,
                           Quantity = 500
                       }
                   };
        }

        private IList<RecipeStep> GenerateRecipeSteps()
        {
            return new List<RecipeStep>()
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
        }

        private IList<ApplicationUser> GenerateAuthors()
        {
            return new List<ApplicationUser>()
                   {
                       new ApplicationUser()
                       {
                           Email = "brat@mail.bg",
                           PasswordHash = "PasswordHash",
                           UserName = "bratazsymgotin"
                       },
                       new ApplicationUser()
                       {
                           Email = "gotvacha@got.in",
                           PasswordHash = "nemiznaeshpassa",
                           UserName = "Gotvacha"
                       }
                   };
        }

        private IList<RecipeImage> GenerateRecipeImages()
        {
            return new List<RecipeImage>()
                   {
                       new RecipeImage()
                       {
                           Url = "abvgd"
                       },
                       new RecipeImage()
                       {
                           Url = "dasdad"
                       }
                   };
        }

        private IList<Recipe> GenerateRecipes(
            IList<Ingredient> ingredients, 
            IList<RecipeStep> steps, 
            IList<RecipeImage> images, 
            IList<ApplicationUser> authors)
        {
            return new List<Recipe>()
                   {
                       new Recipe()
                       {
                           Title = "Spaghetti with tomato sauce",
                           Images = images,
                           Ingredients = ingredients.Take(2)
                               .ToArray(),
                           Steps = steps,
                           Author = authors[0],
                           MinutesRequiredToCook = 30
                       },
                       new Recipe()
                       {
                           Title = "Spaghetti bolonese",
                           Images = images.Take(1).ToList(),
                           Ingredients = ingredients.Take(1)
                               .ToArray(),
                           Steps = steps,
                           Author = authors[0],
                           MinutesRequiredToCook = 50
                       },
                       new Recipe()
                       {
                           Title = "Spaghetti test2",
                           Images = images.Skip(1).Take(1).ToList(),
                           Ingredients = ingredients.ToArray(),
                           Steps = steps,
                           Author = authors[1],
                           MinutesRequiredToCook = 120
                       },
                       new Recipe()
                       {
                           Title = "Баница със сиреньееее",
                           Images = images,
                           Ingredients = ingredients.Skip(2)
                               .Take(1)
                               .ToArray(),
                           Steps = steps,
                           Author = authors[1]
                       },
                   };
        }

        public IDbContext GenerateContext()
        {
            var ingredientTypes = this.GenerateIngredientsTypes();
            var ingredients = this.GenerateIngredientsUsedInRecipes(ingredientTypes);
            var steps = this.GenerateRecipeSteps();
            var images = this.GenerateRecipeImages();
            var authors = this.GenerateAuthors();
            var recipes = this.GenerateRecipes(ingredients, steps, images, authors);

            var productTypesSet = new Mock<DbSet<IngredientType>>().SetupData(ingredientTypes);
            var productsSet = new Mock<DbSet<Ingredient>>().SetupData(ingredients);
            var imagesSet = new Mock<DbSet<RecipeImage>>().SetupData(images);
            var recipeStepsSet = new Mock<DbSet<RecipeStep>>().SetupData(steps);
            var usersSet = new Mock<DbSet<ApplicationUser>>().SetupData(authors);
            var recipesSet = new Mock<DbSet<Recipe>>().SetupData(recipes);

            var context = new Mock<NzKvoDaQmContext>();

            context.Setup(c => c.IngredientTypes)
                .Returns(productTypesSet.Object);
            context.Setup(c => c.Ingredients)
                .Returns(productsSet.Object);
            context.Setup(c => c.RecipeImages)
                .Returns(imagesSet.Object);
            context.Setup(c => c.RecipeSteps)
                .Returns(recipeStepsSet.Object);
            context.Setup(c => c.Users)
                .Returns(usersSet.Object);
            context.Setup(c => c.Recipes)
                .Returns(recipesSet.Object);

            return context.Object;
        }
    }
}
