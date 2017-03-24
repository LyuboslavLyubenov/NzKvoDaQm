namespace NzKvoDaQm.Models.EntityModels
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using NzKvoDaQm.Models.ViewModels;

    public class Comment : IContainComments
    {
        public int Id
        {
            get; set;
        }

        public string Text
        {
            get; set;
        }

        public virtual ApplicationUser Author
        {
            get; set;
        }
        
        public virtual IList<Comment> Comments
        {
            get; set;
        }
    }
}
