namespace NzKvoDaQm.Tests
{

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

        private IngredientType[] GenerateIngredientsTypes()
        {
            return new IngredientType[]
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

        private Ingredient[] GenerateIngredientsUsedInRecipes(IngredientType[] ingredientTypes)
        {
            return new Ingredient[]
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

        private RecipeStep[] GenerateRecipeSteps()
        {
            return new RecipeStep[]
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

        private ApplicationUser[] GenerateAuthors()
        {
            return new[]
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

        private Recipe[] GenerateRecipes(Ingredient[] ingredients, RecipeStep[] steps, ApplicationUser[] authors)
        {
            return new Recipe[]
                   {
                       new Recipe()
                       {
                           Title = "Spaghetti with tomato sauce",
                           Images = new[]
                                    {
                                        new RecipeImage()
                                        {
                                            Url = "abvgd"
                                        },
                                        new RecipeImage()
                                        {
                                            Url = "dasdad"
                                        }
                                    },
                           Ingredients = ingredients.Take(2)
                               .ToArray(),
                           Steps = steps,
                           Author = authors[0],
                           MinutesRequiredToCook = 30
                       },
                       new Recipe()
                       {
                           Title = "Spaghetti bolonese",
                           Images = new[]
                                    {
                                        new RecipeImage()
                                        {
                                            Url = "asdsadasda"
                                        },
                                    },
                           Ingredients = ingredients.Take(1)
                               .ToArray(),
                           Steps = steps,
                           Author = authors[0],
                           MinutesRequiredToCook = 50
                       },
                       new Recipe()
                       {
                           Title = "Spaghetti test2",
                           Images = new[]
                                    {
                                        new RecipeImage()
                                        {
                                            Url = "asdsadasda"
                                        },
                                    },
                           Ingredients = ingredients.ToArray(),
                           Steps = steps,
                           Author = authors[1],
                           MinutesRequiredToCook = 120
                       },
                       new Recipe()
                       {
                           Title = "Баница със сиреньееее",
                           Images = new[]
                                    {
                                        new RecipeImage()
                                        {
                                            Url = "asdsadasda"
                                        },
                                    },
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
            var authors = this.GenerateAuthors();
            var recipes = this.GenerateRecipes(ingredients, steps, authors);

            var productTypesSet = new Mock<DbSet<IngredientType>>().SetupData(ingredientTypes);
            var productsSet = new Mock<DbSet<Ingredient>>().SetupData(ingredients);
            var recipeStepsSet = new Mock<DbSet<RecipeStep>>().SetupData(steps);
            var usersSet = new Mock<DbSet<ApplicationUser>>().SetupData(authors);
            var recipesSet = new Mock<DbSet<Recipe>>().SetupData(recipes);

            var context = new Mock<NzKvoDaQmContext>();

            context.Setup(c => c.ProductTypes)
                .Returns(productTypesSet.Object);
            context.Setup(c => c.Ingredients)
                .Returns(productsSet.Object);
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
