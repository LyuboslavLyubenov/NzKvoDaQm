namespace NzKvoDaQm.Services
{

    using System;
    using System.Data.Entity;
    using System.Linq;

    using NzKvoDaQm.Services.Interfaces;

    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly IDbSet<T> set;

        protected EntityService(IDbSet<T> set)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            this.set = set;
        }

        public T Get(long id)
        {
            return this.set.Find(id);
        }

        public IQueryable<T> Get(Func<T, bool> condition)
        {
            return this.set.Where(condition).AsQueryable();
        }

        public bool Delete(T entity)
        {
            try
            {
                this.set.Remove(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }

}