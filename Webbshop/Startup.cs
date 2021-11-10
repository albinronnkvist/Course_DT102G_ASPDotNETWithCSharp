using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webbshop.Startup))]
namespace Webbshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
