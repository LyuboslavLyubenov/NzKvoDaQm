namespace NzKvoDaQm.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using NzKvoDaQm.Models.ViewModels;

    public class Review : IContainComments
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int Rating { get; set; }

        [InverseProperty("Reviews")]
        public virtual ApplicationUser Author { get; set; }
        
        public virtual IList<Comment> Comments { get; set; }
    }
}