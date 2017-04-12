namespace NzKvoDaQm.Services.Recipe
{
    using System;
    using System.Data.Entity;
    using System.Text;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class RecipeImagesService : EntityService<RecipeImage>, IRecipeImagesService
    {
        public const int MaxImageSize = (3 * 1024 * 1024);

        public RecipeImagesService(IDbSet<RecipeImage> set, IDbContext context)
            : base(set, context)
        {
        }

        private bool IsValidBase64Image(string base64Image)
        {
            var commaIndex = base64Image.IndexOf(',');
            var imageData = base64Image.Substring(commaIndex + 1);

            try
            {
                Convert.FromBase64String(imageData);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public RecipeImage Create(string imageBase64, ApplicationUser author)
        {
            if (string.IsNullOrWhiteSpace(imageBase64))
            {
                throw new ArgumentNullException(nameof(imageBase64));
            }

            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            if (!imageBase64.StartsWith("data:image") || !this.IsValidBase64Image(imageBase64))
            {
                throw new ArgumentException("Invalid image");
            }
            
            var imageSize = Encoding.Unicode.GetByteCount(imageBase64);
            if (imageSize > MaxImageSize)
            {
                throw new ArgumentException("Image too large");
            }
            
            var recipeImage = new RecipeImage()
                              {
                                  Url = imageBase64,
                                  Author = author
                              };

            this.Set.Add(recipeImage);
            this.SaveChanges();

            return recipeImage;
        }
    }
}
