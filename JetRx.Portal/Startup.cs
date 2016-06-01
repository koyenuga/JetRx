using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JetRx.Portal.Startup))]
namespace JetRx.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
