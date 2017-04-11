namespace NzKvoDaQm.Services.Recipe
{

    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.Interfaces;

    public class IngredientTypesService : EntityService<IngredientType>, IIngredientTypesService
    {
        private readonly string[] allowedPictureExtensions;

        public IngredientTypesService(IDbSet<IngredientType> set, IDbContext context)
            : base(set, context)
        {
            this.allowedPictureExtensions = new string[]
                                       {
                                           "jpg",
                                           "jpeg",
                                           "png",
                                           "gif"
                                       };
        }

        public IngredientType Create(string name, string thumbnailUrl = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cant be empty");
            }

            if (Regex.IsMatch(name, "[^a-zA-Z]"))
            {
                throw new ArgumentException();
            }

            if (this.Get(it => it.Name == name).Any())
            {
                throw new InvalidOperationException("IngredientType exists with same name");
            }

            if (!string.IsNullOrWhiteSpace(thumbnailUrl) && this.allowedPictureExtensions.All(ext => !thumbnailUrl.ToUpper().EndsWith(ext.ToUpper())))
            {
                throw new ArgumentException("Invalid picture url");
            }

            var entity = new IngredientType()
                         {
                             Name = name,
                             ThumbnailUrl = thumbnailUrl
                         };
            this.Set.Add(entity);
            this.SaveChanges();
            return entity;
        }
    }
}