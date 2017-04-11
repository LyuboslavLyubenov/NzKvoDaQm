namespace NzKvoDaQm.Services
{

    using System;
    using System.Data.Entity;
    using System.Linq;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Services.Interfaces;

    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        public event EventHandler OnSetModification = delegate
            { }; 

        protected readonly IDbSet<T> Set;

        protected EntityService(IDbSet<T> set)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            this.Set = set;
        }

        public T Get(long id)
        {
            return this.Set.Find(id);
        }

        public IQueryable<T> Get(Func<T, bool> condition)
        {
            return this.Set.Where(condition).AsQueryable();
        }

        public bool Delete(T entity)
        {
            try
            {
                this.Set.Remove(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}