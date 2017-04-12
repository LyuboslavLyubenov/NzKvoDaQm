namespace NzKvoDaQm.Services.Recipe
{

    using System;
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
        private readonly IRecipeIngredientsService ingredientsService;
        private readonly IRecipeImagesService recipeImagesesService;
        private readonly IRecipeStepsService recipeStepsesService;

        public RecipeService(
            IDbSet<Recipe> set, 
            IRecipeIngredientsService ingredientsService,
            IRecipeImagesService recipeImagesesService,
            IRecipeStepsService recipeStepsesService,
            IDbContext context)
            : base(set, context)
        {
            if (ingredientsService == null)
            {
                throw new ArgumentNullException(nameof(ingredientsService));
            }

            if (recipeImagesesService == null)
            {
                throw new ArgumentNullException(nameof(recipeImagesesService));
            }

            if (recipeStepsesService == null)
            {
                throw new ArgumentNullException(nameof(recipeStepsesService));
            }

            this.ingredientsService = ingredientsService;
            this.recipeImagesesService = recipeImagesesService;
            this.recipeStepsesService = recipeStepsesService;
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cant be empty");
            }
        }

        private void ValidateIngredients(string[] ingredients)
        {
            if (ingredients == null ||
                ingredients.Length == 0 ||
                ingredients.All(string.IsNullOrWhiteSpace) ||
                ingredients.Any(i => !Regex.IsMatch(i, "\\W|_")))
            {
                throw new ArgumentException("Ingredients empty or have invalid symbols in them");
            }
        }

        private void ValidateImages(string[] imagesData)
        {
            const int PhotoMaxSize = (3 * 1024 * 1024);

            if (imagesData != null && imagesData.Any(p => p.Length > PhotoMaxSize))
            {
                throw new ArgumentException("Image limit is " + (PhotoMaxSize / 1024) / 1024 + " mibs (mbs)");
            }
        }

        private void ValidateStepsTexts(string[] stepsTexts)
        {
            const int MinLength = 3;

            if (stepsTexts == null ||
                stepsTexts.Length == 0 ||
                stepsTexts.All(string.IsNullOrWhiteSpace) ||
                stepsTexts.Any(s => s.Trim().Length < MinLength))
            {
                throw new ArgumentException($"Steps must be at least {MinLength} symbols long");
            }
        }

        private void ValidateStepsMinutes(int?[] stepsMinutes)
        {
            if (stepsMinutes != null &&
                stepsMinutes.Any(s => s != null && s <= 0))
            {
                throw new ArgumentException("Minutes required for step must be positive number");
            }
        }

        private void ValidateMinutesRequiredForCooking(int minutesRequiredForCooking)
        {
            if (minutesRequiredForCooking <= 0)
            {
                throw new ArgumentException("Minutes required for cooking must be positive number");
            }
        }

        private void SaveImagesLocally(string[] imagesData, string recipeTitle, string path)
        {
            for (int i = 0; i < imagesData.Length; i++)
            {
                var image = ImageUtils.ConvertBase64ToImage(imagesData[i]);
                var imageName = $"{path} + {recipeTitle} + {i}.jpg";
                image.Save(imageName, ImageFormat.Jpeg);
            }
        }

        public Recipe Create(CreateRecipeBindingModel bindingModel, ApplicationUser author)
        {
            this.ValidateTitle(bindingModel.Title);
            //TODO: Validate ingredients
            this.ValidateImages(bindingModel.Images);
            this.ValidateStepsTexts(bindingModel.StepsTexts);
            this.ValidateStepsMinutes(bindingModel.StepsMinutes);
            this.ValidateMinutesRequiredForCooking(bindingModel.MinutesRequiredForCooking);

            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            var path = $"/../../NzKvoDaQm/App_Data/Recipe Images/{author.UserName}/{bindingModel.Title}/";
            this.SaveImagesLocally(bindingModel.Images, bindingModel.Title, path);
            
            var imagesPath = $"/../../NzKvoDaQm/App_Data/Recipe Images/{author.UserName}/{bindingModel.Title}";
            var images = Directory.GetFiles(imagesPath);
            var imagesUrls =
                images.Select(
                    i => $"App_Data/Recipe Images/{author.UserName}/{bindingModel.Title}/{Path.GetFileName(i)}");
            /*
            var recipe = new Recipe()
            {
                Title = bindingModel.Title,
                Ingredients = this.ingredientsService.Create(bindingModel.Ingredients),
                Images = this.recipeImagesService.Create(imagesUrls),
                Steps = this.recipeStepsService.Create(bindingModel.StepsTexts, bindingModel.StepsMinutes),
                MinutesRequiredToCook = bindingModel.MinutesRequiredForCooking,
                Author = author
            };
            
            this.Set.Add(recipe);
            
            return recipe;
            */
            throw new NotImplementedException();
        }
    }
}