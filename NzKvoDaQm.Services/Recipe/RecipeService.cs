namespace NzKvoDaQm.Services.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.BindingModels;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;
    using NzKvoDaQm.Services.Utils;

    public class RecipeService : EntityService<Recipe>, IRecipesService
    {
        private readonly IIngredientTypesService ingredientTypesService;

        private readonly IRecipeIngredientsService ingredientsService;
        private readonly IRecipeImagesService recipeImagesService;
        private readonly IRecipeStepsService recipeStepsService;

        public RecipeService(
            IDbSet<Recipe> set, 
            IIngredientTypesService ingredientTypesService,
            IRecipeIngredientsService ingredientsService,
            IRecipeImagesService recipeImagesService,
            IRecipeStepsService recipeStepsService,
            IDbContext context)
            : base(set, context)
        {
            if (ingredientTypesService == null)
            {
                throw new ArgumentNullException();
            }

            if (ingredientsService == null)
            {
                throw new ArgumentNullException(nameof(ingredientsService));
            }

            if (recipeImagesService == null)
            {
                throw new ArgumentNullException(nameof(recipeImagesService));
            }

            if (recipeStepsService == null)
            {
                throw new ArgumentNullException(nameof(recipeStepsService));
            }

            this.ingredientTypesService = ingredientTypesService;
            this.ingredientsService = ingredientsService;
            this.recipeImagesService = recipeImagesService;
            this.recipeStepsService = recipeStepsService;
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (!Regex.IsMatch(title, "[а-яА-Яa-zA-Z]"))
            {
                throw new ArgumentException("Title must contain words");
            }

            if (this.Get(r => r.Title == title) != null)
            {
                throw new InvalidOperationException("Recipe with same title already exists");
            }
        }

        private void ValidateSteps(string[] stepsTexts, int?[] stepsMinutes)
        {
            if (stepsTexts == null ||
                stepsMinutes == null ||
                stepsTexts.Count(st => !string.IsNullOrWhiteSpace(st)) != stepsMinutes.Length)
            {
                throw new ArgumentException("Invalid steps data");
            }
        }

        private void ValidateIngredients(string[] ingredientsNames,
                                         QuantityMeasurementType[] ingredientsMeasurementTypes,
                                         int[] ingredientsQuantities)
        {
            if (ingredientsNames.Length != ingredientsMeasurementTypes.Length ||
                ingredientsNames.Length != ingredientsQuantities.Length ||
                ingredientsMeasurementTypes.Length != ingredientsQuantities.Length)
            {
                throw new ArgumentException("Ingredients data not valid.");
            }
        }

        private void ValidateAuthor(ApplicationUser author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
        }

        private void ValidateCookingRequiredMinutesForMinutes(int minutes)
        {
            if (minutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes));
            }
        }
        
        private IList<Ingredient> CreateIngredients(
            string[] ingredientsNames, 
            QuantityMeasurementType[] ingredientsMeasurementTypes, 
            int[] ingredientsQuantities)
        {
            var ingredients = new List<Ingredient>();

            for (int i = 0; i < ingredientsNames.Length; i++)
            {
                var ingredientName = ingredientsNames[i];
                var ingredientMeasurementType = ingredientsMeasurementTypes[i];
                var ingredientQuantity = ingredientsQuantities[i];

                var ingredientType = this.ingredientTypesService.Get(it => it.Name == ingredientName)
                    .ToList()
                    .FirstOrDefault();

                if (ingredientName == null)
                {
                    ingredientType = this.ingredientTypesService.Create(ingredientName, null);
                }

                var ingredient = this.ingredientsService.Create(
                    ingredientType,
                    ingredientMeasurementType,
                    ingredientQuantity);

                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        private IList<RecipeImage> CreateImages(string[] imagesData, ApplicationUser author)
        {
            var images = new List<RecipeImage>();

            for (int i = 0; i < imagesData.Length; i++)
            {
                var imageData = imagesData[i];
                var image = this.recipeImagesService.Create(imageData, author);
                images.Add(image);
            }

            return images;
        }

        private IList<RecipeStep> CreateSteps(string[] stepsTexts, int?[] stepsMinutes)
        {
            var steps = new List<RecipeStep>();

            for (int i = 0; i < stepsTexts.Length; i++)
            {
                var text = stepsTexts[i];
                var minutes = stepsMinutes[i];
                var step = this.recipeStepsService.Create(text, minutes);
                steps.Add(step);
            }

            return steps;
        }
        
        public Recipe Create(CreateRecipeBindingModel bindingModel, ApplicationUser author)
        {
            this.ValidateTitle(bindingModel.Title);
            this.ValidateSteps(bindingModel.StepsTexts, bindingModel.StepsMinutes);
            this.ValidateIngredients(
                bindingModel.IngredientsNames, 
                bindingModel.IngredientsMeasurementTypes, 
                bindingModel.IngredientsQuantities);
            this.ValidateAuthor(author);
            this.ValidateCookingRequiredMinutesForMinutes(bindingModel.MinutesRequiredForCooking);
            
            var ingredients = this.CreateIngredients(
                bindingModel.IngredientsNames,
                bindingModel.IngredientsMeasurementTypes,
                bindingModel.IngredientsQuantities);
            var images = this.CreateImages(bindingModel.Images, author);
            var steps = this.CreateSteps(bindingModel.StepsTexts, bindingModel.StepsMinutes);
            
            var recipe = new Recipe()
            {
                Title = bindingModel.Title,
                Ingredients = ingredients,
                Images = images,
                Steps = steps,
                MinutesRequiredToCook = bindingModel.MinutesRequiredForCooking,
                Author = author
            };
            
            this.Set.Add(recipe);
            this.SaveChanges();

            return recipe;
        }
    }
}