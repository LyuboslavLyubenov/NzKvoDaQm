namespace NzKvoDaQm.Services
{

    using NzKvoDaQm.Data;

    public class Service
    {
        protected IDbContext Context
        {
            get; private set;
        }

        protected Service()
        {
            this.Context = new NzKvoDaQmContext();
        }

        protected Service(IDbContext context)
        {
            this.Context = context;
        }
    }
}
