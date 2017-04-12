namespace NzKvoDaQm.Services
{

    using System;
    using System.Data.Entity;
    using System.Linq;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Services.Interfaces;

    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        protected readonly IDbSet<T> Set;
        private readonly Func<int> saveChangesAction;

        protected EntityService(IDbSet<T> set, IDbContext context)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            this.Set = set;
            this.saveChangesAction = context.SaveChanges;
        }

        public virtual T Get(long id)
        {
            return this.Set.Find(id);
        }

        public virtual IQueryable<T> Get(Func<T, bool> condition)
        {
            return this.Set.Where(condition).AsQueryable();
        }

        public virtual bool Delete(T entity)
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

        protected int SaveChanges()
        {
            return this.saveChangesAction();
        }
    }
}