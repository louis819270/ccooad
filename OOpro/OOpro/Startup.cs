using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OOpro.Startup))]
namespace OOpro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
