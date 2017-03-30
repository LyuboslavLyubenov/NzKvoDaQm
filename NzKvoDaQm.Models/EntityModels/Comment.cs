namespace NzKvoDaQm.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NzKvoDaQm.Models.ViewModels;

    public class Comment : IContainComments
    {
        public int Id
        {
            get; set;
        }

        [Required]
        [MinLength(4)]
        public string Text
        {
            get; set;
        }

        [Required]
        public virtual ApplicationUser Author
        {
            get; set;
        }
        
        public virtual IList<Comment> Comments
        {
            get; set;
        }

        public Comment()
        {
            this.Comments = new List<Comment>();
        }
    }
}
