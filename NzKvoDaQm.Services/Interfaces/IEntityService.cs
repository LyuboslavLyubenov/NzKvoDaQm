namespace NzKvoDaQm.Services.Interfaces
{

    using System;
    using System.Linq;

    public interface IEntityService<T>
    {
        T Get(long id);

        IQueryable<T> Get(Func<T, bool> condition);

        bool Delete(T entity);
    }
}
