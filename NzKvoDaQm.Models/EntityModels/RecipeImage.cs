namespace NzKvoDaQm.Models.EntityModels
{

    using NzKvoDaQm.Models.Attributes;

    public class RecipeImage
    {
        public int Id { get; set; }

        [ValidateUrl]
        public string Url { get; set; }
    }
}
