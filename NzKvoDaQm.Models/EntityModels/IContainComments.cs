namespace NzKvoDaQm.Models.EntityModels
{

    using System.Collections.Generic;

    public interface IContainComments
    {
        IList<Comment> Comments { get; set; }
    }
}
