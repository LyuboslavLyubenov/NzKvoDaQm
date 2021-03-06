namespace NzKvoDaQm.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NzKvoDaQm.Models.ViewModels;

    public class Review : IContainComments
    {
        public long Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Title { get; set; }
        
        [MinLength(4)]
        public string Body { get; set; }
        
        [Required]
        public RatingType Rating { get; set; }

        [Required]
        [InverseProperty("Reviews")]
        public virtual ApplicationUser Author { get; set; }
        
        public virtual IList<Comment> Comments { get; set; }

        public Review()
        {
            this.Comments = new List<Comment>();
        }
    }

    public enum RatingType
    {
        Awful,
        Average,
        Good,
        Tasty,
        InLove
    }
}