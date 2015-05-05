using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcWebApplication.Startup))]
namespace MvcWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
