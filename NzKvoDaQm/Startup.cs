using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NzKvoDaQm.Startup))]
namespace NzKvoDaQm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
