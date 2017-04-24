namespace NzKvoDaQm.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NzKvoDaQm.Models.ViewModels;

    public class Comment
    {
        public long Id
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
    }
}
